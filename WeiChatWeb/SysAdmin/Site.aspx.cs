using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysAdmin_Site : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSite();
        }
    }

    protected void LoadSite()
    {
        List<model.weichat.Dict_SiteInfo> c = OR.Control.DAL.GetModelList<model.weichat.Dict_SiteInfo>("");

        this.gvSite.DataSource = c;
        this.gvSite.DataBind();
    }

    protected void gvSite_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.weichat.Dict_SiteInfo c = (model.weichat.Dict_SiteInfo)e.Row.DataItem;

            LinkButton linkDel = e.Row.FindControl("linkDel") as LinkButton;

            linkDel.CommandName = "del";
            linkDel.CommandArgument = c.SiteID.ToString();

            LinkButton linkMod = e.Row.FindControl("linkMod") as LinkButton;
            linkMod.OnClientClick = String.Format("EditSite({0}); return false;", c.SiteID.ToString());
        }
    }

    protected void gvSite_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            model.weichat.Dict_SiteInfo c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_SiteInfo>(Int32.Parse(e.CommandArgument.ToString()));
            OR.Control.DAL.Delete<model.weichat.Dict_SiteInfo>(c);

            LoadSite();
        }
    }
}