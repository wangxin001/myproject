using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Price_VegetableInput : System.Web.UI.Page
{
    String strPriceDate = string.Empty;

    protected String priceType = String.Empty;
    protected model.PriceType pt = model.PriceType.Price_VegetablePrice;

    protected void Page_Load(object sender, EventArgs e)
    {
        priceType = Request.QueryString["PriceType"];

        if (String.IsNullOrEmpty(priceType))
        {
            priceType = ((int)model.PriceType.Price_VegetablePrice).ToString();
        }

        pt = (model.PriceType)Convert.ToInt32(priceType);

        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "录入今日" + EnumUtil.GetDescription(pt);

        strPriceDate = Request.QueryString["PriceDate"];

        if (!IsPostBack)
        {
            strPriceDate = Request.QueryString["PriceDate"];

            this.txtPriceDate.Text = strPriceDate;

            LoadPriceByDate(Convert.ToDateTime(strPriceDate).Date);
        }
    }

    protected void LoadPriceByDate(DateTime date)
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append(" select i.ItemID,i.ItemName,i.ItemType,i.ItemLevel,i.ItemUnit,@PriceDate PriceDate,p.PriceID, p.Price01,p.Price02,p.Price03,p.Price04 ");
        strSQL.Append(" From PriceItems i left join (select PriceID,ItemID,Price01,Price02,Price03,Price04 From PriceRecord ");
        strSQL.Append(" where PriceDate = @PriceDate) p ");
        strSQL.Append(" on i.ItemID = p.ItemID where ItemType=" + ((int)pt).ToString() + " ");
        strSQL.Append(" and i.Status=" + ((int)model.Status.Normal).ToString() + " order by ItemOrder ");

        DataSet ds = OR.DB.SQLHelper.Query(strSQL.ToString(), new SqlParameter("@PriceDate", date.Date));

        this.GridView1.DataSource = ds.Tables[0];
        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow data = ((DataRowView)e.Row.DataItem).Row;

            TextBox price01 = e.Row.Cells[3].FindControl("txtPrice01") as TextBox;
            TextBox price02 = e.Row.Cells[4].FindControl("txtPrice02") as TextBox;
            TextBox price03 = e.Row.Cells[5].FindControl("txtPrice03") as TextBox;
            TextBox price04 = e.Row.Cells[6].FindControl("txtPrice04") as TextBox;

            TextBox txtItemID = e.Row.Cells[7].FindControl("txtItemID") as TextBox;
            TextBox txtPriceID = e.Row.Cells[8].FindControl("txtPriceID") as TextBox;

            price01.Text = (data["Price01"] == DBNull.Value ? "" : data["Price01"].ToString());
            price02.Text = (data["Price02"] == DBNull.Value ? "" : data["Price02"].ToString());
            price03.Text = (data["Price03"] == DBNull.Value ? "" : data["Price03"].ToString());
            price04.Text = (data["Price04"] == DBNull.Value ? "" : data["Price04"].ToString());

            txtItemID.Text = data["ItemID"].ToString();
            txtPriceID.Text = (data["PriceID"] == DBNull.Value ? "" : data["PriceID"].ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        String url = "./VegetablePrice.aspx?PriceDate=" + this.txtPriceDate.Text + "&PriceType=" + ((int)pt).ToString();
        Response.Redirect(url);
        Response.End();
    }

    protected void btnInput_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            GridViewRow row = this.GridView1.Rows[i];

            // 数据行才做此处理
            if (row.RowType == DataControlRowType.DataRow)
            {
                // 逐行进行处理
                TextBox price01 = row.Cells[4].FindControl("txtPrice01") as TextBox;
                TextBox price02 = row.Cells[5].FindControl("txtPrice02") as TextBox;
                TextBox price03 = row.Cells[6].FindControl("txtPrice03") as TextBox;
                TextBox price04 = row.Cells[7].FindControl("txtPrice04") as TextBox;

                TextBox txtItemID = row.Cells[8].FindControl("txtItemID") as TextBox;
                TextBox txtPriceID = row.Cells[9].FindControl("txtPriceID") as TextBox;

                String itemID = txtItemID.Text;

                model.PriceItems item = OR.Control.DAL.GetModelByKey<model.PriceItems>(Convert.ToInt32(itemID));

                model.PriceRecord record = OR.Control.DAL.GetModel<model.PriceRecord>("ItemID=@id AND PriceDate=@date",
                    new SqlParameter("@id", item.ItemID), new SqlParameter("@date", Convert.ToDateTime(strPriceDate)));

                Boolean newEntity = false;

                if (record==null)
                {
                    record = new model.PriceRecord();
                    newEntity = true;
                }

                record.ItemID = item.ItemID;
                record.PriceDate = Convert.ToDateTime(strPriceDate);
                record.ItemName = item.ItemName;
                record.ItemLevel = item.ItemLevel;
                record.ItemUnit = item.ItemUnit;
                record.Price01 = Convert.ToDecimal(IsNull(price01.Text));
                record.Price02 = Convert.ToDecimal(IsNull(price02.Text));
                record.Price03 = Convert.ToDecimal(IsNull(price03.Text));
                record.Price04 = Convert.ToDecimal(IsNull(price04.Text));
                record.ItemType = item.ItemType;

                if (newEntity)
                {
                    record = OR.Control.DAL.Add<model.PriceRecord>(record, true);
                    Log.info("录入" + EnumUtil.GetDescription(pt) + "信息 日期：" + strPriceDate + "  " + record.ToString(true));
                }
                else
                {
                    OR.Control.DAL.Update<model.PriceRecord>(record);
                    Log.info("更新" + EnumUtil.GetDescription(pt) + "信息 日期：" + strPriceDate + "  " + record.ToString(true));
                }
            }
        }

        // 返回上一级页面
        btnCancel_Click(null, null);
    }

    private String IsNull(String s)
    {
        return String.IsNullOrEmpty(s) ? "0" : s;
    }
}