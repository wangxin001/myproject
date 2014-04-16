using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysAdmin_Price : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPrice();
        }
    }

    protected void LoadPrice()
    {
        List<model.weichat.Dict_PriceCode> c = OR.Control.DAL.GetModelList<model.weichat.Dict_PriceCode>("");

            this.gvPrice.DataSource = c;
            this.gvPrice.DataBind();
    }

    protected void gvPrice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.weichat.Dict_PriceCode c = (model.weichat.Dict_PriceCode)e.Row.DataItem;

            LinkButton linkDel = e.Row.FindControl("linkDel") as LinkButton;

            linkDel.CommandName = "del";
            linkDel.CommandArgument = c.PriceID.ToString();

            LinkButton linkMod = e.Row.FindControl("linkMod") as LinkButton;
            linkMod.OnClientClick = String.Format("EditPrice({0}); return false;", c.PriceID.ToString());
        }
    }

    protected void gvPrice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            model.weichat.Dict_PriceCode c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_PriceCode>(Int32.Parse(e.CommandArgument.ToString()));
            OR.Control.DAL.Delete<model.weichat.Dict_PriceCode>(c);

            LoadPrice();
        }
    }
}