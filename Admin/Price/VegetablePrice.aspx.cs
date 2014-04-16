using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Price_VegetablePrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        
        if (!IsPostBack)
        {
            LoadItemType();

            master.PageTitle = "今日" + EnumUtil.GetDescription((model.PriceType)Convert.ToInt32(this.ddlItemType.SelectedValue));
            this.txtTitle.Text = EnumUtil.GetDescription((model.PriceType)Convert.ToInt32(this.ddlItemType.SelectedValue));

            String strPriceDate = Request.QueryString["PriceDate"];

            if (String.IsNullOrEmpty(strPriceDate))
            {
                strPriceDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            this.txtPriceDate.Text = strPriceDate;

            LoadPriceByDate(Convert.ToDateTime(strPriceDate));
        }
    }


    protected void LoadItemType()
    {
        String t = Request.QueryString["PriceType"];

        this.ddlItemType.Items.Add(new ListItem("每日菜价", ((int)model.PriceType.Price_VegetablePrice).ToString()));
        this.ddlItemType.Items.Add(new ListItem("每日粮油", ((int)model.PriceType.Price_LiangYou).ToString()));
        this.ddlItemType.Items.Add(new ListItem("每日肉蛋水产", ((int)model.PriceType.Price_RouDan).ToString()));
        this.ddlItemType.Items.Add(new ListItem("大宗货品", ((int)model.PriceType.Price_CargoPrice).ToString()));

        if (t == ((int)model.PriceType.Price_VegetablePrice).ToString())
        {
            this.ddlItemType.SelectedIndex = 0;
        }
        else if (t == ((int)model.PriceType.Price_LiangYou).ToString())
        {
            this.ddlItemType.SelectedIndex = 1;
        }
        else if (t == ((int)model.PriceType.Price_RouDan).ToString())
        {
            this.ddlItemType.SelectedIndex = 2;
        }
        else if (t == ((int)model.PriceType.Price_CargoPrice).ToString())
        {
            this.ddlItemType.SelectedIndex = 3;
        }
    }

    protected void LoadPriceByDate(DateTime date)
    {
        StringBuilder strSQL = new StringBuilder();
        strSQL.Append(" select i.ItemID,i.ItemName,i.ItemType,i.ItemLevel,i.ItemUnit,@PriceDate PriceDate,p.Price01,p.Price02,p.Price03,p.Price04 ");
        strSQL.Append(" From PriceItems i left join (select ItemID,Price01,Price02,Price03,Price04 From PriceRecord ");
        strSQL.Append(" where PriceDate = @PriceDate) p ");
        strSQL.Append(" on i.ItemID = p.ItemID where ItemType=" + this.ddlItemType.SelectedValue + " ");
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

            if (data["Price01"] == DBNull.Value)
            {
                e.Row.Cells[4].Text = "-";
            }
            if (data["Price02"] == DBNull.Value)
            {
                e.Row.Cells[5].Text = "-";
            }
            if (data["Price03"] == DBNull.Value)
            {
                e.Row.Cells[6].Text = "-";
            }
            if (data["Price04"] == DBNull.Value)
            {
                e.Row.Cells[7].Text = "-";
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
        String url = "./VegetableInput.aspx?PriceDate=" + this.txtPriceDate.Text + "&PriceType=" + this.ddlItemType.SelectedValue;
        Response.Redirect(url);
        Response.End();
    }

    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "今日" + EnumUtil.GetDescription((model.PriceType)Convert.ToInt32(this.ddlItemType.SelectedValue));
        this.txtTitle.Text = EnumUtil.GetDescription((model.PriceType)Convert.ToInt32(this.ddlItemType.SelectedValue));

        LoadPriceByDate(Convert.ToDateTime(this.txtPriceDate.Text));
    }
}