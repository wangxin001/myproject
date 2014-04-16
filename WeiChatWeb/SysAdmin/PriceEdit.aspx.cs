using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysAdmin_PriceEdit : System.Web.UI.Page
{
    protected String id = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(id))
            {
                LoadPriceName();
            }
        }
    }

    protected void LoadPriceName()
    {
        model.weichat.Dict_PriceCode c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_PriceCode>(Int32.Parse(id));

        if (c != null)
        {
            this.txtPriceName.Text = c.PriceName;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(id))
        {
            model.weichat.Dict_PriceCode c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_PriceCode>(Int32.Parse(id));
            c.PriceName = this.txtPriceName.Text;
            OR.Control.DAL.Update<model.weichat.Dict_PriceCode>(c);
        }
        else
        {
            model.weichat.Dict_PriceCode c = new model.weichat.Dict_PriceCode();
            c.PriceName = this.txtPriceName.Text;
            OR.Control.DAL.Add<model.weichat.Dict_PriceCode>(c, false);
        }

        ClientScript.RegisterStartupScript(typeof(Page), "reload", "parent.ReloadPage();", true);
    }
}