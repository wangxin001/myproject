using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_StoreInfo : System.Web.UI.Page
{
    protected String strUserGUID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strUserGUID = Request.QueryString["UserGUID"];

        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "菜店显示屏管理";

        if (!IsPostBack)
        {
            GetScreenList();
        }
    }

    protected void GetScreenList()
    {
        List<model.ScreenInfo> userList = OR.Control.DAL.GetModelList<model.ScreenInfo>(" UserGUID = @p", new System.Data.SqlClient.SqlParameter("@p", strUserGUID));
        this.GridView1.DataSource = userList;
        this.GridView1.DataBind();

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.ScreenInfo screen = (model.ScreenInfo)e.Row.DataItem;

            e.Row.Cells[3].Text = EnumUtil.GetDescription((model.Status)screen.Status);

            HyperLink hyMod = e.Row.FindControl("hyMod") as HyperLink;
            hyMod.NavigateUrl = "javascript:ScreenMod('" + screen.ScreenID.ToString() + "')";

            LinkButton btnDel = e.Row.FindControl("btnDel") as LinkButton;
            btnDel.CommandName = "del";
            btnDel.CommandArgument = screen.ScreenID.ToString();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            model.ScreenInfo screen = OR.Control.DAL.GetModelByKey<model.ScreenInfo>(e.CommandArgument);

            OR.Control.DAL.Delete<model.ScreenInfo>(screen);
            GetScreenList();
        }
    }
}