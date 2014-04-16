using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Price_CargoInput : System.Web.UI.Page
{
    String strPriceDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "大宗商品价格录入";

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
        strSQL.Append(" select i.ItemID,i.ItemName,i.ItemType,i.ItemUnit,@PriceDate PriceDate,p.PriceID, p.Price02 ");
        strSQL.Append(" From PriceItems i left join (select PriceID,ItemID,Price01,Price02,Price03,Price04 From PriceRecord ");
        strSQL.Append(" where PriceDate = @PriceDate) p ");
        strSQL.Append(" on i.ItemID = p.ItemID where ItemType=" + ((int)model.PriceType.Price_CargoPrice).ToString() + " ");
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

            TextBox price02 = e.Row.Cells[3].FindControl("txtPrice02") as TextBox;

            TextBox txtItemID = e.Row.Cells[4].FindControl("txtItemID") as TextBox;
            TextBox txtPriceID = e.Row.Cells[5].FindControl("txtPriceID") as TextBox;

            price02.Text = (data["Price02"] == DBNull.Value ? "" : data["Price02"].ToString());

            txtItemID.Text = data["ItemID"].ToString();
            txtPriceID.Text = (data["PriceID"] == DBNull.Value ? "" : data["PriceID"].ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        String url = "./CargoPrice.aspx?PriceDate=" + this.txtPriceDate.Text;
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
                TextBox price02 = row.Cells[3].FindControl("txtPrice02") as TextBox;

                TextBox txtItemID = row.Cells[4].FindControl("txtItemID") as TextBox;
                TextBox txtPriceID = row.Cells[5].FindControl("txtPriceID") as TextBox;

                String itemID = txtItemID.Text;

                model.PriceItems item = OR.Control.DAL.GetModelByKey<model.PriceItems>(Convert.ToInt32(itemID));

                model.PriceRecord record = new model.PriceRecord();
                record.ItemID = item.ItemID;
                record.ItemName = item.ItemName;
                record.ItemUnit = item.ItemUnit;
                record.Price01 = 0.0M;
                record.Price02 = Convert.ToDecimal(price02.Text);
                record.Price03 = 0.0M;
                record.Price04 = 0.0M;
                record.PriceDate = Convert.ToDateTime(strPriceDate);
                record.ItemType = item.ItemType;

                if (String.IsNullOrEmpty(txtPriceID.Text))
                {
                    record = OR.Control.DAL.Add<model.PriceRecord>(record, true);
                    Log.info("录入大宗价格信息 日期：" + strPriceDate + "  " + record.ToString(true));
                }
                else
                {
                    record.PriceID = Convert.ToInt32(txtPriceID.Text);
                    OR.Control.DAL.Update<model.PriceRecord>(record);
                    Log.info("更新大宗价格信息 日期：" + strPriceDate + "  " + record.ToString(true));
                }
            }
        }

        // 返回上一级页面
        btnCancel_Click(null, null);
    }
}