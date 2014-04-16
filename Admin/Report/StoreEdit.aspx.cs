using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_StoreEdit : System.Web.UI.Page
{

    String strUserGUID = string.Empty;
    String strScreenID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strUserGUID = Request.QueryString["UserGUID"];
        strScreenID = Request.QueryString["ScreenID"];

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(strScreenID))
            {
                LoadScreenInfo();
            }
        }
    }

    protected void LoadScreenInfo()
    {
        model.ScreenInfo screen = OR.Control.DAL.GetModelByKey<model.ScreenInfo>(Convert.ToInt32(strScreenID));

        this.txtScreenName.Text = screen.ScreenName;
        this.chkStatus.Checked = (screen.Status == 1 ? true : false);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        model.ScreenInfo u = OR.Control.DAL.GetModel<model.ScreenInfo>("ScreenName = @p", new System.Data.SqlClient.SqlParameter("@p", this.txtScreenName.Text ));

        if (String.IsNullOrEmpty(strScreenID))
        {

            if (u != null)
            {
                this.txtScreenName.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 添加新角色
            model.ScreenInfo screen = new model.ScreenInfo();

            screen.Created = DateTime.Now;
            screen.Status = (int)(chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            screen.ScreenName = this.txtScreenName.Text;
            screen.UserGUID = strUserGUID; 

            screen = OR.Control.DAL.Add<model.ScreenInfo>(screen, true);
            Log.info("添加屏幕：" + screen.ToString(true));

        }
        else
        {
            if (u != null && u.ScreenID.ToString() != strScreenID )
            {
                this.txtScreenName.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 更新内容
            model.ScreenInfo screen = OR.Control.DAL.GetModelByKey<model.ScreenInfo>(Convert.ToInt32(strScreenID));

            screen.Status = (int)(chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            screen.ScreenName = this.txtScreenName.Text;

            OR.Control.DAL.Update<model.ScreenInfo>(screen);
            Log.info("更新屏幕：" + screen.ToString(true));

        }

        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.location.href=parent.location.href; </script>");

    }
}