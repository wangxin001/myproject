using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report_StoreInfor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadStoreInfo();
        }
    }

    protected void LoadStoreInfo()
    {
        model.UserInfo user = OR.Control.DAL.GetModelByKey<model.UserInfo>(Util.GetLoginUserInfo().UserGUID);

        if (user.FilledInfor == 1)
        {
            Response.Redirect("./Fillin.aspx");
            Response.End();
        }

        this.txtUserAccount.Text = user.UserAccount;
        this.txtUserAccount.Enabled = false;

        this.txtUserName.Text = user.UserName;
        this.txtContactEmail.Text = user.ContactEmail;
        this.txtContactMobile.Text = user.ContactMobile;
        this.txtContactName.Text = user.ContactName;
        this.txtContactPhone.Text = user.ContactPhone;
        this.txtContactTitle.Text = user.ContactTitle;

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        #region Validate
        if (String.IsNullOrEmpty(this.txtUserName.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入菜店名称');", true);
            return;
        }
        if (String.IsNullOrEmpty(this.txtContactName.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入联系人姓名');", true);
            return;
        }
        if (String.IsNullOrEmpty(this.txtContactTitle.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入联系人职位');", true);
            return;
        }
        if (String.IsNullOrEmpty(this.txtContactMobile.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入联系电话');", true);
            return;
        }
        if (String.IsNullOrEmpty(this.txtContactPhone.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入手机');", true);
            return;
        }
        if (String.IsNullOrEmpty(this.txtContactEmail.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('请输入邮箱');", true);
            return;
        }
        #endregion



        model.UserInfo user = OR.Control.DAL.GetModelByKey<model.UserInfo>(Util.GetLoginUserInfo().UserGUID);

        user.UserName = this.txtUserName.Text;

        if (!String.IsNullOrEmpty(this.txtPassword.Text))
        {
            user.UserPassword = Util.to_md5(this.txtPassword.Text, 32);
        }

        user.ContactEmail = this.txtContactEmail.Text;
        user.ContactMobile = this.txtContactMobile.Text;
        user.ContactName = this.txtContactName.Text;
        user.ContactPhone = this.txtContactPhone.Text;
        user.ContactTitle = this.txtContactTitle.Text;

        user.FilledInfor = 1;

        OR.Control.DAL.Update<model.UserInfo>(user);

        Response.Redirect("./Fillin.aspx");
    }   
}