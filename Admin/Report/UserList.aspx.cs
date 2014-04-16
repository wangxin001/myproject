using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "菜店信息管理";

        if (!IsPostBack)
        {           
            GetUserList();
        }
    }

    protected void GetUserList()
    {
        List<model.UserInfo> userList = OR.Control.DAL.GetModelList<model.UserInfo>(" UserRole = " + ((int)model.UserRole.数据上报用户).ToString());
        this.GridView1.DataSource = userList;
        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.UserInfo user = (model.UserInfo)e.Row.DataItem;

            e.Row.Cells[5].Text = EnumUtil.GetDescription((model.Status)user.Status);

            HyperLink hyMod = e.Row.FindControl("hyMod") as HyperLink;
            hyMod.NavigateUrl = "javascript:UserMod('" + user.UserGUID + "')";

            LinkButton btnDel = e.Row.FindControl("btnDel") as LinkButton;
            btnDel.CommandName = "del";
            btnDel.CommandArgument = user.UserGUID;

            HyperLink linkPrice = e.Row.FindControl("lnkPrice") as HyperLink;
            linkPrice.NavigateUrl = "./PriceEdit.aspx?UserGUID=" + user.UserGUID;
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