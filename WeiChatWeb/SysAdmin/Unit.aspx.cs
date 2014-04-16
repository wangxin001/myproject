using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysAdmin_Unit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUnit();
        }
    }

    protected void LoadUnit()
    {
        List<model.weichat.Dict_UnitCode> c = OR.Control.DAL.GetModelList<model.weichat.Dict_UnitCode>("");

        this.gvUnit.DataSource = c;
        this.gvUnit.DataBind();
    }

    protected void gvUnit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.weichat.Dict_UnitCode c = (model.weichat.Dict_UnitCode)e.Row.DataItem;

            LinkButton linkDel = e.Row.FindControl("linkDel") as LinkButton;

            linkDel.CommandName = "del";
            linkDel.CommandArgument = c.UnitID.ToString();

            LinkButton linkMod = e.Row.FindControl("linkMod") as LinkButton;
            linkMod.OnClientClick = String.Format("EditSite({0}); return false;", c.UnitID.ToString());
        }
    }

    protected void gvUnit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            model.weichat.Dict_UnitCode c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_UnitCode>(Int32.Parse(e.CommandArgument.ToString()));
            OR.Control.DAL.Delete<model.weichat.Dict_UnitCode>(c);

            LoadUnit();
        }
    }
}