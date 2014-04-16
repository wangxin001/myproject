using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_Item : System.Web.UI.Page
{
    protected String pid = String.Empty;
    protected String id = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        pid = Request.QueryString["pid"];
        id = Request.QueryString["id"];

        if (!IsPostBack)
        {
            foreach (model.weichat.TemplateType priceType in Enum.GetValues(typeof(model.weichat.TemplateType)))
            {
                this.ddpPriceType.Items.Add(new ListItem(EnumUtil.GetDescription(priceType), ((int)priceType).ToString()));
            }

            if (!String.IsNullOrEmpty(id))
            {
                LoadItemName();
            }
        }
    }

    protected void LoadItemName()
    {
        model.weichat.Dict_ItemCode c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_ItemCode>(Int32.Parse(id));

        if (c != null)
        {
            this.txtItemName.Text = c.ItemName;
            this.ddpPriceType.SelectedValue = c.TemplateType.ToString();
            this.txtRetailItemCode.Text = c.RetailItemCode;
            this.txtWholesaleItemCode.Text = c.WholesaleItemCode;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(id))
        {
            model.weichat.Dict_ItemCode c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_ItemCode>(Int32.Parse(id));
            c.ItemName = this.txtItemName.Text;
            c.TemplateType = Int32.Parse(this.ddpPriceType.SelectedValue);
            c.RetailItemCode = this.txtRetailItemCode.Text;
            c.WholesaleItemCode = this.txtWholesaleItemCode.Text;

            OR.Control.DAL.Update<model.weichat.Dict_ItemCode>(c);
        }
        else
        {
            model.weichat.Dict_ItemCode c = new model.weichat.Dict_ItemCode();
            c.ItemName = this.txtItemName.Text;
            c.CategoryID = Int32.Parse(pid);
            c.TemplateType = Int32.Parse(this.ddpPriceType.SelectedValue);
            c.RetailItemCode = this.txtRetailItemCode.Text;
            c.WholesaleItemCode = this.txtWholesaleItemCode.Text;

            OR.Control.DAL.Add<model.weichat.Dict_ItemCode>(c, false);
        }

        ClientScript.RegisterStartupScript(typeof(Page), "reload", "parent.ReloadPage();", true);
    }
}