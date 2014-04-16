using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_Edit : System.Web.UI.Page
{
    protected String pid = String.Empty;
    protected String id = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        pid = Request.QueryString["pid"];
        id = Request.QueryString["id"];

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(id))
            {
                LoadCategoryName();
            }
        }
    }

    protected void LoadCategoryName()
    {
        model.weichat.Dict_Category c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_Category>(Int32.Parse(id));

        if (c != null)
        {
            this.txtCategoryName.Text = c.CategoryName;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(id))
        {
            model.weichat.Dict_Category c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_Category>(Int32.Parse(id));
            c.CategoryName = this.txtCategoryName.Text;

            OR.Control.DAL.Update<model.weichat.Dict_Category>(c);
        }
        else
        {
            model.weichat.Dict_Category c = new model.weichat.Dict_Category();
            c.CategoryName = this.txtCategoryName.Text;
            c.PID = Int32.Parse(pid);
            OR.Control.DAL.Add<model.weichat.Dict_Category>(c, false);
        }

        ClientScript.RegisterStartupScript(typeof(Page), "reload", "parent.ReloadPage();", true);
    }
}