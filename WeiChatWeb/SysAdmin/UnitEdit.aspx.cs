using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysAdmin_UnitEdit : System.Web.UI.Page
{
    protected String id = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(id))
            {
                LoadUnitName();
            }
        }
    }

    protected void LoadUnitName()
    {
        model.weichat.Dict_SiteInfo c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_SiteInfo>(Int32.Parse(id));

        if (c != null)
        {
            this.txtUnitName.Text = c.SiteName;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(id))
        {
            model.weichat.Dict_UnitCode c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_UnitCode>(Int32.Parse(id));
            c.UnitName = this.txtUnitName.Text;
            OR.Control.DAL.Update<model.weichat.Dict_UnitCode>(c);
        }
        else
        {
            model.weichat.Dict_UnitCode c = new model.weichat.Dict_UnitCode();
            c.UnitName = this.txtUnitName.Text;
            OR.Control.DAL.Add<model.weichat.Dict_UnitCode>(c, false);
        }

        ClientScript.RegisterStartupScript(typeof(Page), "reload", "parent.ReloadPage();", true);
    }
}