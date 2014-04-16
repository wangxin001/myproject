using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report_Infor1a : System.Web.UI.Page
{
    protected String strStoreName = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strStoreName = Util.GetLoginUserInfo().UserName;

        if (!IsPostBack)
        {
            model.UserInfo user = OR.Control.DAL.GetModelByKey<model.UserInfo>(Util.GetLoginUserInfo().UserGUID);   

            this.txtStoreName.Text = user.UserName;
            this.txtContactEmail.Text = user.ContactEmail;
            this.txtContactMobile.Text = user.ContactMobile;
            this.txtContactName.Text = user.ContactName;
            this.txtContactPhone.Text = user.ContactPhone;
            this.txtContactTitle.Text = user.ContactTitle;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Validate
        if (String.IsNullOrEmpty(this.txtStoreName.Text.Trim()))
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

        user.UserName = this.txtStoreName.Text.Trim();
        user.ContactEmail = this.txtContactEmail.Text.Trim();
        user.ContactMobile = this.txtContactMobile.Text.Trim();
        user.ContactName = this.txtContactName.Text.Trim();
        user.ContactPhone = this.txtContactPhone.Text.Trim();
        user.ContactTitle = this.txtContactTitle.Text.Trim();

        OR.Control.DAL.Update<model.UserInfo>(user);

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('本店资料保存成功');", true);
 
    }
}