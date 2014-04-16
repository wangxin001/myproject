using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 123456 :  e10adc3949ba59abbe56e057f20f883e
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {

        if (Request.Cookies["_AuthCode"] == null)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('验证码错误');", true);
            return;
        }

        if (this.txtAuthorCode .Text.ToUpper() != Request.Cookies["_AuthCode"].Value.ToString().ToUpper())
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('验证码错误');", true);
            return;
        }

        model.UserInfo user = OR.Control.DAL.GetModel<model.UserInfo>("UserAccount=@UserAccount",
            new System.Data.SqlClient.SqlParameter("@UserAccount", this.txtUserName.Text.Trim()));

        if (user != null && user.Status == (int)model.Status.Normal)
        {
            String strPassword = Util.to_md5(this.txtPassword.Text, 32);

            if ((model.UserRole)user.UserRole == model.UserRole.数据上报用户 && strPassword.Equals(user.UserPassword))
            {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(Util.SerializeObj.Serialize<model.UserInfo>(user), false);
            }
            else if ((model.UserRole)user.UserRole == model.UserRole.查看所有菜价用户 && strPassword.Equals(user.UserPassword))
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(Util.SerializeObj.Serialize<model.UserInfo>(user), false);

                Response.Redirect("./price.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "ChangeAuthCode();alert('帐号/密码输入错误');", true);
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "ChangeAuthCode();alert('帐号/密码输入错误');", true);
            return;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("./resetpwd.aspx");
    }
}