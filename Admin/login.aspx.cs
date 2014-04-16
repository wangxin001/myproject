using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Login1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "价格监测网管理网站-管理员登陆";
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        model.UserInfo user = OR.Control.DAL.GetModel<model.UserInfo>("UserAccount=@UserAccount",
            new System.Data.SqlClient.SqlParameter("@UserAccount", this.txtUserAccount.Text.Trim()));

        if (user != null && user.Status == (int)model.Status.Normal)
        {
            String strPassword = Util.to_md5(this.txtUserPwd.Text, 32);

            if ((model.UserRole)user.UserRole == model.UserRole.系统管理员 && strPassword.Equals(user.UserPassword))
            {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(Util.SerializeObj.Serialize<model.UserInfo>(user), false);
            }
        }
    }
}