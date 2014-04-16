using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

public partial class Sync_SyncItemTrend : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;

        master.PageTitle = "同步价格走势";

        if (!IsPostBack)
        {
            String strPriceDate = Request.QueryString["PriceDate"];

            if (String.IsNullOrEmpty(strPriceDate))
            {
                strPriceDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            this.txtPriceDate.Text = strPriceDate;

            LoadPriceByDate(Convert.ToDateTime(strPriceDate));
        }
    }

    DataTable dtPriceField = null;
    DataTable dsYesterday = null;

    protected void LoadPriceByDate(DateTime date)
    {
        String strSQL = @"SELECT r.PriceID, i.ItemID, @date PriceDate, i.ItemName, i.ItemUnit, i.itemLevel, i.ItemType
	                        ,ISNULL(r.Price01, 0) Price01, ISNULL(r.Price02,0) Price02, ISNULL(r.Price03,0) Price03, ISNULL(r.Price04,0) Price04, r.STATUS
                        FROM dbo.PriceItems i LEFT JOIN dbo.RemotePriceRecord r ON i.ItemID = r.ItemID AND r.PriceDate=@date
                        WHERE i.ItemType in (9,10,11,12,13,14) And i.Status=1 Order By i.ItemOrder";

        DataSet ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@date", date.Date));

        strSQL = @"SELECT r.PriceID, i.ItemID, @date PriceDate, i.ItemName, i.ItemUnit, i.itemLevel, i.ItemType
	                        ,ISNULL(r.Price01, 0) Price01, ISNULL(r.Price02,0) Price02, ISNULL(r.Price03,0) Price03, ISNULL(r.Price04,0) Price04
                        FROM dbo.PriceItems i LEFT JOIN dbo.PriceRecord r ON i.ItemID = r.ItemID AND r.PriceDate=@date
                        WHERE i.ItemType in (9,10,11,12,13,14) And i.Status=1 Order By i.ItemOrder";

        dsYesterday = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@date", date.Date.AddDays(-1))).Tables[0];

        List<model.RemotePriceRecord> data = OR.Control.DAL.DataTableToModel<model.RemotePriceRecord>(ds.Tables[0]);

        dtPriceField = OR.DB.SQLHelper.Query("Select * From Dict_RemotePriceField").Tables[0];

        this.GridView1.DataSource = data;
        this.GridView1.DataBind();
    }

    protected void btnSync_Click(object sender, EventArgs e)
    {

        /*
         * 各参数所需内容：
         *      指标： Z000001 批发价
         *             Z000002 成交量
         *             Z000003 零售价
         *      频率： F000007 日报
         *      来源： S000001 北京市价格监测中心
         *      环节： H000004 批发  
         *             H000005 零售
         *             H000006 零售农贸
         *             H000007 零售超市
         * 
         * 批发成交量
         * 批发批发价
         * 
         * 
         * 
         * 每个条目需要拿四个数据：成交量，批发价，农贸零售价，超市零售价
         * 
         */


        List<model.PriceItems> items = OR.Control.DAL.GetModelList<model.PriceItems>("Status=1 AND ItemType in (9,10,11,12,13,14) Order By ItemOrder");

        DataTable dt = OR.DB.SQLHelper.Query("Select * From Dict_RemotePriceField").Tables[0];

        foreach (model.PriceItems item in items)
        {

            String unit = String.Empty;

            List<model.dict.Dict_RemotePriceField> fields = OR.Control.DAL.DataRowsToList<model.dict.Dict_RemotePriceField>(dt.Select("ItemID=" + item.ItemID));

            decimal pValue1 = 0;
            decimal pValue2 = 0;
            decimal pValue3 = 0;
            decimal pValue4 = 0;

            decimal rate1 = 1;
            decimal rate2 = 1;
            decimal rate3 = 1;
            decimal rate4 = 1;

            foreach (model.dict.Dict_RemotePriceField field in fields)
            {
                String stageCode = field.StageCode;
                String targetCode = field.TargetCode;

                String ret = String.Empty;
                String value = String.Empty;

                value = SyncService.GetPointValue(field.ItemCode, this.txtPriceDate.Text, targetCode, stageCode, out ret, false, out unit);

                switch (field.PriceField)
                {
                    case 1:
                        if (ret == "000")
                        {
                            Decimal.TryParse(value, out pValue1);
                        }
                        rate1 = field.Rate;
                        break;
                    case 2:
                        if (ret == "000")
                        {
                            Decimal.TryParse(value, out pValue2);
                        }
                        rate2 = field.Rate;
                        break;
                    case 3:
                        if (ret == "000")
                        {
                            Decimal.TryParse(value, out pValue3);
                        }
                        rate3 = field.Rate;
                        break;
                    case 4:
                        if (ret == "000")
                        {
                            Decimal.TryParse(value, out pValue4);
                        }
                        rate4 = field.Rate;
                        break;
                    default:
                        break;
                }
            }

            model.RemotePriceRecord price = OR.Control.DAL.GetModel<model.RemotePriceRecord>("ItemID=@ItemId And PriceDate=@date",
                new SqlParameter("@ItemId", item.ItemID),
                new SqlParameter("@date", DateTime.Parse(this.txtPriceDate.Text).Date));

            if (price == null)
            {
                price = new model.RemotePriceRecord();
                price.ItemID = item.ItemID;
                price.PriceDate = DateTime.Parse(this.txtPriceDate.Text).Date;

                price.ItemName = item.ItemName;

                price.ItemLevel = item.ItemLevel;
                price.ItemType = item.ItemType;
                price.ItemUnit = item.ItemUnit;

                price.Status = (int)model.dict.DictStatus.Pendding;

                price.Price01 = pValue1 * rate1;
                price.Price02 = pValue2 * rate2;
                price.Price03 = pValue3 * rate3;
                price.Price04 = pValue4 * rate4;

                OR.Control.DAL.Add<model.RemotePriceRecord>(price, false);
            }
            else
            {
                price.ItemLevel = item.ItemLevel;
                price.ItemName = item.ItemName;
                price.ItemUnit = item.ItemUnit;

                price.Price01 = pValue1 * rate1;
                price.Price02 = pValue2 * rate2;
                price.Price03 = pValue3 * rate3;
                price.Price04 = pValue4 * rate4;

                price.Status = (int)model.dict.DictStatus.Pendding;

                OR.Control.DAL.Update<model.RemotePriceRecord>(price);
            }
        }

        LoadPriceByDate(Convert.ToDateTime(this.txtPriceDate.Text));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        OR.DB.SQLHelper.ExecuteSql("Delete From RemotePriceRecord WHERE ItemType in (9,10,11,12,13,14) AND PriceDate=@date",
            new SqlParameter("@date", Convert.ToDateTime(this.txtPriceDate.Text).Date));

        LoadPriceByDate(Convert.ToDateTime(this.txtPriceDate.Text));
    }

    protected void txtQuery_Click(object sender, EventArgs e)
    {
        LoadPriceByDate(Convert.ToDateTime(this.txtPriceDate.Text));
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            model.RemotePriceRecord r = e.Row.DataItem as model.RemotePriceRecord;

            HyperLink HyperLink1 = e.Row.FindControl("HyperLink1") as HyperLink;
            HyperLink HyperLink2 = e.Row.FindControl("HyperLink2") as HyperLink;
            HyperLink HyperLink3 = e.Row.FindControl("HyperLink3") as HyperLink;
            HyperLink HyperLink4 = e.Row.FindControl("HyperLink4") as HyperLink;

            Boolean c1 = hasCombined(r.ItemID, 1);
            Boolean c2 = hasCombined(r.ItemID, 2);
            Boolean c3 = hasCombined(r.ItemID, 3);
            Boolean c4 = hasCombined(r.ItemID, 4);

            HyperLink1.Text = (c1 ? "* " : "") + r.Price01.ToString();
            HyperLink2.Text = (c2 ? "* " : "") + r.Price02.ToString();
            HyperLink3.Text = (c3 ? "* " : "") + r.Price03.ToString();
            HyperLink4.Text = (c4 ? "* " : "") + r.Price04.ToString();

            if (c1)
                HyperLink1.Font.Bold = true;
            if (c2)
                HyperLink2.Font.Bold = true;
            if (c3)
                HyperLink3.Font.Bold = true;
            if (c4)
                HyperLink4.Font.Bold = true;

            HyperLink1.NavigateUrl = String.Format("javascript:FieldManage('{0}',1)", r.ItemID);
            HyperLink2.NavigateUrl = String.Format("javascript:FieldManage('{0}',2)", r.ItemID);
            HyperLink3.NavigateUrl = String.Format("javascript:FieldManage('{0}',3)", r.ItemID);
            HyperLink4.NavigateUrl = String.Format("javascript:FieldManage('{0}',4)", r.ItemID);

            if (r.Status == (int)model.dict.DictStatus.Pendding)
            {
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else if (r.Status == (int)model.dict.DictStatus.Approved)
            {
                e.Row.ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Font.Bold = true;
            }

            Label txtYesterday1 = e.Row.FindControl("txtYesterday1") as Label;
            Label txtYesterday2 = e.Row.FindControl("txtYesterday2") as Label;
            Label txtYesterday3 = e.Row.FindControl("txtYesterday3") as Label;
            Label txtYesterday4 = e.Row.FindControl("txtYesterday4") as Label;

            if (r.PriceID == 0)
            {
                txtYesterday1.Text = "&nbsp;";
                txtYesterday2.Text = "&nbsp;";
                txtYesterday3.Text = "&nbsp;";
                txtYesterday4.Text = "&nbsp;";
            }
            else
            {
                DataRow[] row = dsYesterday.Select("ItemID=" + r.ItemID.ToString());

                if (row.Length == 0)
                {
                    return;
                }

                model.PriceRecord y = OR.Control.DAL.DataRowToModel<model.PriceRecord>(row[0]);

                compareValue(y.Price01, r.Price01, txtYesterday1);
                compareValue(y.Price02, r.Price02, txtYesterday2);
                compareValue(y.Price03, r.Price03, txtYesterday3);
                compareValue(y.Price04, r.Price04, txtYesterday4);

            }
        }
    }

    protected void compareValue(decimal yValue, decimal tValue, Label txtLabel)
    {
        if (yValue == 0 || tValue == 0)
        {
            txtLabel.Text = "&nbsp;";
            return;
        }

        decimal r = Math.Abs(yValue - tValue) / yValue;

        txtLabel.Text = String.Format("({0:P2})", r);

        if (r > decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["PriceThreshold"]))
        {
            txtLabel.ForeColor = System.Drawing.Color.Red;
            txtLabel.Font.Bold = true;
        }
    }

    protected Boolean hasCombined(Int32 itemID, Int32 priceField)
    {
        DataRow[] row = dtPriceField.Select("ItemID=" + itemID.ToString() + " And PriceField=" + priceField.ToString());

        return row.Length > 0;
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        String strSQL = @"SELECT r.PriceID, i.ItemID, @date PriceDate, i.ItemName, i.ItemUnit, r.itemLevel, i.ItemType
	                        , ISNULL(r.Price01, 0) Price01, ISNULL(r.Price02,0) Price02, ISNULL(r.Price03,0) Price03, ISNULL(r.Price04,0) Price04, r.STATUS
                        FROM dbo.PriceItems i LEFT JOIN dbo.RemotePriceRecord r ON i.ItemID = r.ItemID AND PriceDate=@date
                        WHERE i.ItemType in (9,10,11,12,13,14) And i.Status=1";

        DataSet ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@date", Convert.ToDateTime(this.txtPriceDate.Text)));

        List<model.RemotePriceRecord> srcData = OR.Control.DAL.DataTableToModel<model.RemotePriceRecord>(ds.Tables[0]);

        foreach (model.RemotePriceRecord data in srcData)
        {
            model.PriceRecord price = OR.Control.DAL.GetModel<model.PriceRecord>("ItemID=@id AND PriceDate=@date",
                new SqlParameter("@id", data.ItemID),
                new SqlParameter("@date", data.PriceDate));

            Boolean newEntity = false;

            if (price == null)
            {
                price = new model.PriceRecord();

                price.ItemID = data.ItemID;
                price.PriceDate = data.PriceDate;

                newEntity = true;
            }

            price.ItemLevel = data.ItemLevel;
            price.ItemName = data.ItemName;
            price.ItemType = data.ItemType;
            price.ItemUnit = data.ItemUnit;
            price.Price01 = data.Price01;
            price.Price02 = data.Price02;
            price.Price03 = data.Price03;
            price.Price04 = data.Price04;

            if (newEntity)
            {
                OR.Control.DAL.Add<model.PriceRecord>(price, false);
            }
            else
            {
                OR.Control.DAL.Update<model.PriceRecord>(price);
            }

            data.Status = (int)model.dict.DictStatus.Approved;

            OR.Control.DAL.Update<model.RemotePriceRecord>(data);
        }

        LoadPriceByDate(Convert.ToDateTime(this.txtPriceDate.Text));
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        LoadPriceByDate(Convert.ToDateTime(this.txtPriceDate.Text));
    }
}