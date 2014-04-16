using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

using System.IO;

using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;

using NPOI.HSSF.Util;


public partial class report_PriceQueryForm : System.Web.UI.UserControl
{

    protected String strStoreName = String.Empty;
    protected String strPriceDate = String.Empty;
    protected String strAvgPriceDate = String.Empty;
    protected String strStorePriceDate = String.Empty;
    protected String strBatchID = "1";
    protected String strPriceTime = String.Empty;

    protected DataTable batchTable = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.ddpStore.Visible = true;
            
            //LoadStoreInfo();
        }
    }

    public bool StoreVisible
    {
        set
        {
            this.ddpStore.Visible = value;
            this.txtPrompt.Visible = value;
        }
    }

    public void LoadInitPrice()
    {
        LoadPriceInfo(null, 1);
    }

    public void LoadStoreInfo()
    {
        LoadStoreInfo(null);
    }

    public void LoadStoreInfo(String strGUID)
    {
        List<model.UserInfo> stores = OR.Control.DAL.GetModelList<model.UserInfo>("UserRole=@role", new SqlParameter("@role", (int)model.UserRole.数据上报用户));
        this.ddpStore.DataSource = stores;
        this.ddpStore.DataBind();

        if (!String.IsNullOrEmpty(strGUID))
        {
            for (int i = 0; i < this.ddpStore.Items.Count; i++)
            {
                if (strGUID.Equals(this.ddpStore.Items[i].Value))
                {
                    this.ddpStore.Items[i].Selected = true;
                    this.txtStoreName.Text = this.ddpStore.Items[i].Text;
                    break;
                }
            }
        }
    }



    int rowIndex = 0;

    protected void LoadPriceInfo(String strDate, int batchID)
    {
        this.strBatchID = batchID.ToString();

        String strBatchSQL = "SELECT distinct BatchID FROM StorePrice WHERE PriceDate = @pdate AND StoreGUID = @pguid Order by BatchID";

        DateTime date = DateTime.Now.Date;

        if (!String.IsNullOrEmpty(strDate))
        {
            Boolean ret = DateTime.TryParse(strDate, out date);

            if (!ret)
            {
                date = DateTime.Now.Date;
            }
        }

        this.strPriceDate = date.ToString("yyyy-MM-dd");

        SqlParameter pguid = new SqlParameter("@pguid", this.ddpStore.SelectedValue);
        SqlParameter pdate = new SqlParameter("@pdate", date);
        SqlParameter pbatch = new SqlParameter("@pbatch", batchID);

        batchTable = OR.DB.SQLHelper.Query(strBatchSQL, pguid, pdate).Tables[0];

        this.Repeater2.DataSource = batchTable;
        this.Repeater2.DataBind();

        String strSQL = @"
WITH
AvgPriceDate as ( select MAX(PriceDate) maxDate From PriceRecord where ItemType in (1,15,16) and PriceDate<@pdate), 
StorePriceTmp as ( SELECT *	FROM StorePrice	WHERE PriceDate = @pdate AND StoreGUID = @pguid)

SELECT i.ItemID, i.ItemName, i.ItemOrder, i.ItemLevel,  i.ItemUnit, 
       a.PriceDate avgPriceDate, Price03 avgPrice, s.PriceDate storePriceDate, s.Price01 StorePrice, s.BatchID, s.Created
FROM PriceItems i
LEFT JOIN (Select * From PriceRecord where ItemType in (1,15,16) and PriceDate=(select maxDate From AvgPriceDate)) a ON a.ItemID = i.ItemID
LEFT JOIN ( Select * From StorePriceTmp where BatchID = @pbatch ) s ON i.ItemID = s.ItemID
WHERE i.ReportStatus = 1 AND i.STATUS = 1 ORDER BY i.ItemOrder";

        DataTable data = OR.DB.SQLHelper.Query(strSQL, pguid, pdate, pbatch).Tables[0];

        rowIndex = 1;

        this.Repeater1.DataSource = data;
        this.Repeater1.DataBind();

        this.element_id.Text = date.ToString("yyyy-MM-dd");

        if (data.Rows.Count > 0 )
        {
            if (data.Rows[0]["Created"] != System.DBNull.Value)
            {
                strPriceTime = Convert.ToDateTime(data.Rows[0]["Created"].ToString()).ToString("yyyy年MM月dd日 HH时mm分ss秒");
                strStorePriceDate = Convert.ToDateTime(data.Rows[0]["storePriceDate"].ToString()).ToString("yyyy-MM-dd");
            }
            else
            {
                strPriceTime = date.ToString("yyyy年MM月dd日");
            }

            if (data.Rows[0]["avgPriceDate"] != System.DBNull.Value)
            {
                strAvgPriceDate = Convert.ToDateTime(data.Rows[0]["avgPriceDate"].ToString()).ToString("yyyy-MM-dd");
            }
           
        }
        else
        {
            strPriceTime = date.ToString("yyyy年MM月dd日");
        }

        this.currentBatchID.Value = batchID.ToString();
        this.totalBatchCount.Value = batchTable.Rows.Count.ToString();

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "setCanendar();", true);

    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        LoadPriceInfo(this.element_id.Text, 1);
    }

    protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            System.Data.DataRowView item = e.Item.DataItem as System.Data.DataRowView;

            LinkButton lnkPage = e.Item.FindControl("lnkPage") as LinkButton;

            if (item["BatchID"].ToString() == strBatchID)
            {
                lnkPage.CssClass = "num on";
            }
            else
            {
                lnkPage.CssClass = "num";
            }

            lnkPage.CommandName = "page";
            lnkPage.CommandArgument = item["BatchID"].ToString();
        }
        else if (e.Item.ItemType == ListItemType.Header)
        {
            LinkButton lnkFirst = e.Item.FindControl("lnkFirst") as LinkButton;
            LinkButton lnkPrevious = e.Item.FindControl("lnkPrevious") as LinkButton;

            lnkFirst.CommandName = "page1";
            lnkFirst.CommandArgument = "first";

            lnkPrevious.CommandName = "page1";
            lnkPrevious.CommandArgument = "previous";
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {

            LinkButton lnkNext = e.Item.FindControl("lnkNext") as LinkButton;
            LinkButton lnkLast = e.Item.FindControl("lnkLast") as LinkButton;

            lnkNext.CommandName = "page1";
            lnkNext.CommandArgument = "next";

            lnkLast.CommandName = "page1";
            lnkLast.CommandArgument = "last";
        }
    }

    protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "page")
        {
            this.strPriceDate = this.element_id.Text;
            this.strBatchID = e.CommandArgument.ToString();

            LoadPriceInfo(this.strPriceDate, Convert.ToInt32(this.strBatchID));
        }
        else if (e.CommandName.ToString() == "page1")
        {
            this.strPriceDate = this.element_id.Text;

            int batchID = Convert.ToInt32(this.currentBatchID.Value);
            int totalBatchCount = Convert.ToInt32(this.totalBatchCount.Value);

            if (e.CommandArgument.ToString() == "first")
            {
                LoadPriceInfo(this.strPriceDate, 1);
            }
            else if (e.CommandArgument.ToString() == "previous")
            {
                if (batchID > 1)
                {
                    LoadPriceInfo(this.strPriceDate, batchID - 1);
                }
                else
                {
                    LoadPriceInfo(this.strPriceDate, 1);
                }
            }
            else if (e.CommandArgument.ToString() == "next")
            {
                if (batchID < totalBatchCount)
                {
                    LoadPriceInfo(this.strPriceDate, batchID + 1);
                }
                else
                {
                    LoadPriceInfo(this.strPriceDate, totalBatchCount);
                }
            }
            else if (e.CommandArgument.ToString() == "last")
            {
                LoadPriceInfo(this.strPriceDate, totalBatchCount);
            }
        }
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            System.Data.DataRowView item = e.Item.DataItem as System.Data.DataRowView;

            if (item != null)
            {
                Label l1 = e.Item.FindControl("txtItemOrder") as Label;
                Label l2 = e.Item.FindControl("txtItemName") as Label;
                Label l3 = e.Item.FindControl("txtItemLevel") as Label;
                Label l4 = e.Item.FindControl("txtItemUnit") as Label;
                Label l5 = e.Item.FindControl("txtPrice03") as Label;
                Label l6 = e.Item.FindControl("txtStorePrice") as Label;

                l1.Text = (rowIndex++).ToString(); // item.Row["ItemOrder"].ToString();
                l2.Text = item.Row["ItemName"].ToString();
                l3.Text = item.Row["ItemLevel"].ToString();
                l4.Text = item.Row["ItemUnit"].ToString();
                l5.Text = String.IsNullOrEmpty(item.Row["avgPrice"].ToString()) ? "-" : item.Row["avgPrice"].ToString();
                l6.Text = item.Row["StorePrice"].ToString();
            }
        }
    }


    protected void btnDownload_Click(object sender, EventArgs e)
    {

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();

        ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

        IRow row = sheet1.CreateRow(0);

        HSSFCellUtil.CreateCell(row, 0, "序号");
        HSSFCellUtil.CreateCell(row, 1, "品类");
        HSSFCellUtil.CreateCell(row, 2, "规格等级");
        HSSFCellUtil.CreateCell(row, 3, "价格单位");
        HSSFCellUtil.CreateCell(row, 4, "市平均价");
        HSSFCellUtil.CreateCell(row, 5, "本店零售价");


        for (int i = 0; i < this.Repeater1.Items.Count; i++)
        {
            RepeaterItem tableRow = this.Repeater1.Items[i];

            if (tableRow.ItemType == ListItemType.Item || tableRow.ItemType == ListItemType.AlternatingItem)
            {
                Label l1 = tableRow.FindControl("txtItemOrder") as Label;
                Label l2 = tableRow.FindControl("txtItemName") as Label;
                Label l3 = tableRow.FindControl("txtItemLevel") as Label;
                Label l4 = tableRow.FindControl("txtItemUnit") as Label;
                Label l5 = tableRow.FindControl("txtPrice03") as Label;
                Label l6 = tableRow.FindControl("txtStorePrice") as Label;

                IRow xlsRow = sheet1.CreateRow(i + 1);

                HSSFCellUtil.CreateCell(xlsRow, 0, l1.Text);
                HSSFCellUtil.CreateCell(xlsRow, 1, l2.Text);
                HSSFCellUtil.CreateCell(xlsRow, 2, l3.Text);
                HSSFCellUtil.CreateCell(xlsRow, 3, l4.Text);
                HSSFCellUtil.CreateCell(xlsRow, 4, l5.Text);
                HSSFCellUtil.CreateCell(xlsRow, 5, l6.Text);
            }
        }

        String filePath = Server.MapPath("~/temp/" + Guid.NewGuid() + ".xls");
        String fileName = this.ddpStore.SelectedItem.Text + "_" + this.element_id.Text + "_菜价.xls";

        FileStream file = new FileStream(filePath, FileMode.Create);
        hssfworkbook.Write(file);
        file.Close();

        FileStream fs = null;

        try
        {
            fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            Response.Expires = 0;
            Response.Buffer = false;

            Response.ContentType = "application/octet-stream";

            if (Request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                Response.AppendHeader("Content-Disposition", "attachment;FileName=" + fileName);
            }
            else
            {
                Response.AppendHeader("Content-Disposition", "attachment;FileName=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            }

            Response.AppendHeader("Content-Length", fs.Length.ToString());

            fs.Close();

            Response.WriteFile(filePath);
        }
        catch (Exception es)
        {
            Response.Write(es.Message);
        }
        finally
        {
            System.IO.File.Delete(filePath);
        }

        Response.End();
    }

}