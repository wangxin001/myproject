using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAdmin_UserEdit : System.Web.UI.Page
{
    String strUserGUID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strUserGUID = Request.QueryString["UserGUID"];

        if (!IsPostBack)
        {

            this.ddlUserRols.Items.Add(new ListItem(model.UserRole.系统管理员.ToString(), ((int)model.UserRole.系统管理员).ToString()));
            this.ddlUserRols.Items.Add(new ListItem(model.UserRole.查看所有菜价用户.ToString(), ((int)model.UserRole.查看所有菜价用户).ToString()));
            this.ddlUserRols.Items.Add(new ListItem(model.UserRole.查询菜价接口用户.ToString(), ((int)model.UserRole.查询菜价接口用户).ToString()));

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

        //this.txtUserPassword.Text = user.UserPassword;
        this.txtUserName.Text = user.UserName;
        this.ddlUserRols.SelectedValue = user.UserRole.ToString();

        this.chkStatus.Checked = (user.Status == 1 ? true : false);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(strUserGUID))
        {
            // 添加新角色
            model.UserInfo user = new model.UserInfo();

            user.Created = DateTime.Now;
            user.Status = (int)(chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            user.UserAccount = this.txtUserAccount.Text;
            user.UserPassword = Util.to_md5(this.txtUserPassword.Text, 32);
            user.UserName = this.txtUserName.Text;

            user.UserGUID = Guid.NewGuid().ToString();
            user.UserRole = (int)((model.UserRole)Convert.ToInt32(this.ddlUserRols.SelectedValue));

            user = OR.Control.DAL.Add<model.UserInfo>(user, true);
            Log.info("添加用户：" + user.ToString(true));
        }
        else
        {
            // 更新内容
            model.UserInfo user = OR.Control.DAL.GetModelByKey<model.UserInfo>(strUserGUID);

            user.Status = (int)(chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);

            if (!String.IsNullOrEmpty(this.txtUserPassword.Text))
            {
                user.UserPassword = Util.to_md5(this.txtUserPassword.Text, 32);
            }
            user.UserName = this.txtUserName.Text;
            user.UserRole = (int)((model.UserRole)Convert.ToInt32(this.ddlUserRols.SelectedValue));

            OR.Control.DAL.Update<model.UserInfo>(user);
            Log.info("更新用户：" + user.ToString(true));
        }

        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.location.href=parent.location.href; </script>");

    }
}