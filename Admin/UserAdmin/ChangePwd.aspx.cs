using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAdmin_ChangePwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
            master.PageTitle = "修改密码";
        }
    }
}