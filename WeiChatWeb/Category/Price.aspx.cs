using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_Price : System.Web.UI.Page
{
    protected String id = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];

        if (!IsPostBack)
        {
            LoadItemPrice();
        }
    }

    protected void LoadItemPrice()
    {
        model.weichat.Dict_ItemCode item = OR.Control.DAL.GetModelByKey<model.weichat.Dict_ItemCode>(Int32.Parse(id));
        this.txtItemName.Text = item.ItemName;

        if (((model.weichat.TemplateType)item.TemplateType) == model.weichat.TemplateType.URL)
        {
            this.gvTable.Columns[3].Visible = false;
            this.gvTable.Columns[4].Visible = false;
        }
        else
        {
            this.gvTable.Columns[5].Visible = false;
            this.gvTable.Columns[6].Visible = false;
        }

        // 先找到最后一天日期
        DateTime date = DateTime.Parse(OR.DB.SQLHelper.GetSingle("Select ISNULL(MAX(PriceDate), GetDate()) PriceDate From Data_ItemPriceValue Where ItemID=@id",
             new System.Data.SqlClient.SqlParameter("@id", Int32.Parse(id))).ToString());

        this.txtPriceDate.Text = date.ToString("yyyy-MM-dd");

        LoadItemPrice(date);
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        LoadItemPrice(DateTime.Parse(this.txtPriceDate.Text));
    }

    protected void LoadItemPrice(DateTime date)
    {
        List<model.weichat.View_ItemPrice> price = OR.Control.DAL.GetModelList<model.weichat.View_ItemPrice>("ItemID=@id AND PriceDate=@date Order BY SiteID",
            new System.Data.SqlClient.SqlParameter("@id", id),
            new System.Data.SqlClient.SqlParameter("@date", date));

        this.gvTable.DataSource = price;
        this.gvTable.DataBind();
    }

    protected void gvTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.weichat.View_ItemPrice c = (model.weichat.View_ItemPrice)e.Row.DataItem;

            LinkButton linkDel = e.Row.FindControl("linkDel") as LinkButton;

            linkDel.CommandName = "del";
            linkDel.CommandArgument = c.ID.ToString();

            LinkButton linkMod = e.Row.FindControl("linkMod") as LinkButton;
            linkMod.OnClientClick = String.Format("EditPrice({0}); return false;", c.ID.ToString());
        }
    }

    protected void gvTable_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            model.weichat.Data_ItemPriceValue c = OR.Control.DAL.GetModelByKey<model.weichat.Data_ItemPriceValue>(Int32.Parse(e.CommandArgument.ToString()));
            OR.Control.DAL.Delete<model.weichat.Data_ItemPriceValue>(c);

            LoadItemPrice(DateTime.Parse(this.txtPriceDate.Text));
        }
    }


    protected void btnLoad_Click(object sender, EventArgs e)
    {
        LoadItemPrice(DateTime.Parse(this.txtPriceDate.Text));
    }
}