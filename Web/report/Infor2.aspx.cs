using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report_Infor2 : System.Web.UI.Page
{
    protected String strStoreName = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strStoreName = Util.GetLoginUserInfo().UserName;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        this.txtCurrentpwd.ForeColor = System.Drawing.Color.Black;
        this.txtpwd1.ForeColor = System.Drawing.Color.Black;
        this.txtpwd2.ForeColor = System.Drawing.Color.Black;

        model.UserInfo user = OR.Control.DAL.GetModelByKey<model.UserInfo>(Util.GetLoginUserInfo().UserGUID);

        if (String.IsNullOrEmpty(this.txtCurrentpwd.Text))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入当前密码');", true);
            return;
        }

        if (user.UserPassword != Util.to_md5(this.txtCurrentpwd.Text, 32))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('当前密码验证失败');", true);
            return;
        }

        if (String.IsNullOrEmpty(this.txtpwd1.Text))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入新密码');", true);
            return;
        }

        if (this.txtpwd1.Text != this.txtpwd2.Text)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('确认密码输入不一致');", true);
            return;
        }

        user.UserPassword = Util.to_md5(this.txtCurrentpwd.Text, 32);

        OR.Control.DAL.Update<model.UserInfo>(user);

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('密码更新成功');", true);
    }
}