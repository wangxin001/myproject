using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Report_UserEdit : System.Web.UI.Page
{
    String strUserGUID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strUserGUID = Request.QueryString["UserGUID"];

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(strUserGUID))
            {
                LoadUserInfo();
            }
        }

    }

    protected void LoadUserInfo()
    {
        model.UserInfo user = OR.Control.DAL.GetModelByKey<model.UserInfo>(strUserGUID);

        this.txtUserAccount.Text = user.UserAccount;
        this.txtUserAccount.Enabled = false;

        this.txtUserPassword.Text = user.UserPassword;
        this.txtUserName.Text = user.UserName;
        this.txtContactEmail.Text = user.ContactEmail;
        this.txtContactMobile.Text = user.ContactMobile;
        this.txtContactName.Text = user.ContactName;
        this.txtContactPhone.Text = user.ContactPhone;
        this.txtContactTitle.Text = user.ContactTitle;

        this.chkStatus.Checked = (user.Status == 1 ? true : false);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        model.UserInfo u = OR.Control.DAL.GetModel<model.UserInfo>("UserAccount = @p", new System.Data.SqlClient.SqlParameter("@p", this.txtUserAccount.Text));

        if (String.IsNullOrEmpty(strUserGUID))
        {

            if (u != null)
            {
                this.txtUserAccount.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 添加新角色
            model.UserInfo user = new model.UserInfo();

            user.Created = DateTime.Now;
            user.Status = (int)(chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            user.UserAccount = this.txtUserAccount.Text;
            user.UserPassword = Util.to_md5(this.txtUserPassword.Text, 32);
            user.UserName = this.txtUserName.Text;

            user.UserGUID = Guid.NewGuid().ToString();
            user.UserRole = (int)model.UserRole.数据上报用户;

            user.ContactEmail = this.txtContactEmail.Text;
            user.ContactMobile = this.txtContactMobile.Text;
            user.ContactName = this.txtContactName.Text;
            user.ContactPhone = this.txtContactPhone.Text;
            user.ContactTitle = this.txtContactTitle.Text;

            user = OR.Control.DAL.Add<model.UserInfo>(user, true);
            Log.info("添加用户：" + user.ToString(true));
        }
        else
        {
            if (u != null && u.UserGUID != strUserGUID)
            {
                this.txtUserAccount.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 更新内容
            model.UserInfo user = OR.Control.DAL.GetModelByKey<model.UserInfo>(strUserGUID);

            user.Status = (int)(chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);

            if (!String.IsNullOrEmpty(this.txtUserPassword.Text))
            {
                user.UserPassword = Util.to_md5(this.txtUserPassword.Text, 32);
            }
            user.UserName = this.txtUserName.Text;
            user.UserRole = (int)model.UserRole.数据上报用户;

            user.ContactEmail = this.txtContactEmail.Text;
            user.ContactMobile = this.txtContactMobile.Text;
            user.ContactName = this.txtContactName.Text;
            user.ContactPhone = this.txtContactPhone.Text;
            user.ContactTitle = this.txtContactTitle.Text;

            OR.Control.DAL.Update<model.UserInfo>(user);
            Log.info("更新用户：" + user.ToString(true));
        }

        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.location.href=parent.location.href; </script>");

    }

}