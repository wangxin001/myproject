using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysAdmin_SiteEdit : System.Web.UI.Page
{
    protected String id = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(id))
            {
                LoadSiteName();
            }
        }
    }

    protected void LoadSiteName()
    {
        model.weichat.Dict_SiteInfo c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_SiteInfo>(Int32.Parse(id));

        if (c != null)
        {
            this.txtSiteName.Text = c.SiteName;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(id))
        {
            model.weichat.Dict_SiteInfo c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_SiteInfo>(Int32.Parse(id));
            c.SiteName = this.txtSiteName.Text;
            OR.Control.DAL.Update<model.weichat.Dict_SiteInfo>(c);
        }
        else
        {
            model.weichat.Dict_SiteInfo c = new model.weichat.Dict_SiteInfo();
            c.SiteName = this.txtSiteName.Text;
            OR.Control.DAL.Add<model.weichat.Dict_SiteInfo>(c, false);
        }

        ClientScript.RegisterStartupScript(typeof(Page), "reload", "parent.ReloadPage();", true);
    }
}