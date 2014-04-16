using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAdmin_UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "帐号管理";

        if (!IsPostBack)
        {           
            GetUserList();
        }
    }

    protected void GetUserList()
    {
        List<model.UserInfo> userList = OR.Control.DAL.GetModelList<model.UserInfo>("UserRole in (1,2,4)");
        this.GridView1.DataSource = userList;
        this.GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.UserInfo user = (model.UserInfo)e.Row.DataItem;

            e.Row.Cells[3].Text = ((model.UserRole)user.UserRole).ToString();
            e.Row.Cells[4].Text = ((model.Status)user.Status).ToString();

            HyperLink hyMod = e.Row.FindControl("hyMod") as HyperLink;
            hyMod.NavigateUrl = "javascript:UserMod('" + user.UserGUID + "')";

            LinkButton btnDel = e.Row.FindControl("btnDel") as LinkButton;
            btnDel.CommandName = "del";
            btnDel.CommandArgument = user.UserGUID;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            model.UserInfo user = OR.Control.DAL.GetModelByKey<model.UserInfo>(e.CommandArgument);

            OR.Control.DAL.Delete<model.UserInfo>(user);
            GetUserList();
        }
    }
}