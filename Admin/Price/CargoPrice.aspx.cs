using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Price_CargoPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "大宗商品价格";

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

    protected void LoadPriceByDate(DateTime date)
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append(" select i.ItemID,i.ItemName,i.ItemType,i.ItemUnit,@PriceDate PriceDate,p.Price01,p.Price02,p.Price03,p.Price04 ");
        strSQL.Append(" From PriceItems i left join (select ItemID,Price01,Price02,Price03,Price04 From PriceRecord ");
        strSQL.Append(" where PriceDate = @PriceDate) p ");
        strSQL.Append(" on i.ItemID = p.ItemID where ItemType=" + ((int)model.PriceType.Price_CargoPrice).ToString() + " and i.Status=" + ((int)model.Status.Normal).ToString() + " order by ItemOrder ");

        DataSet ds = OR.DB.SQLHelper.Query(strSQL.ToString(), new SqlParameter("@PriceDate", date.Date));

        this.GridView1.DataSource = ds.Tables[0];
        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow data = ((DataRowView)e.Row.DataItem).Row;

            if (data["Price02"] == DBNull.Value)
            {
                e.Row.Cells[3].Text = "-";
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DateTime date = Convert.ToDateTime(this.txtPriceDate.Text);
        LoadPriceByDate(date);
    }

    protected void btnInput_Click(object sender, EventArgs e)
    {
        String url = "./CargoInput.aspx?PriceDate=" + this.txtPriceDate.Text;
        Response.Redirect(url);
        Response.End();
    }
}