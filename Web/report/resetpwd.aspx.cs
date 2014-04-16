using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report_resetpwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(this.txtUserName.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入帐号');", true);
            return;
        }

        if (String.IsNullOrEmpty(this.txtEmail.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入邮箱地址');", true);
            return;
        }

        model.UserInfo user = OR.Control.DAL.GetModel<model.UserInfo>("UserAccount=@UserAccount",
            new System.Data.SqlClient.SqlParameter("@UserAccount", this.txtUserName.Text.Trim()));

        if (user == null)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('该帐号无效');", true);
            return;
        }

        if (!this.txtEmail.Text.Trim().ToUpper().Equals( user.ContactEmail.ToUpper().Trim() ) )
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('帐号与邮箱地址不一致');", true);
            return;
        }

        String strASCII = System.Configuration.ConfigurationManager.AppSettings["AuthCodeAscii"];

        Random random = new Random();
        String strPwdCode = "";

        for (int i = 0; i < 6; i++)
        {
            strPwdCode += strASCII.Substring(random.Next(strASCII.Length - 1), 1);
        }

        user.UserPassword = Util.to_md5(strPwdCode, 32); ;
        OR.Control.DAL.Update<model.UserInfo>(user);
        try
        {

            String strTemplate = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/Email_Template.txt"), System.Text.Encoding.Default);

            strTemplate = strTemplate.Replace("[storename]", user.UserName).Replace("[password]", strPwdCode);

            EmailUtil.SendEmail(user.ContactEmail, strTemplate);

            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('密码已重置，请查收邮件获取新密码');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('" + ex.Message + "');", true);

        }
    }
}