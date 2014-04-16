using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_Input : System.Web.UI.Page
{
    protected DateTime date = DateTime.Now;
    protected Int32 id = 0;
    protected Int32 p = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        date = DateTime.Parse(Request.QueryString["date"]);
        id = Int32.Parse(Request.QueryString["id"] ?? "0");
        p = Int32.Parse(Request.QueryString["p"] ?? "0");

        if (!IsPostBack)
        {
            LoadInfo();
            LoadPrice();
        }
    }

    protected void LoadInfo()
    {
        model.weichat.Dict_ItemCode item = OR.Control.DAL.GetModelByKey<model.weichat.Dict_ItemCode>(id);

        if (((model.weichat.TemplateType)item.TemplateType) == model.weichat.TemplateType.URL)
        {
            this.trPrice.Visible = false;
            this.trUnit.Visible = false;
        }
        else
        {
            this.trNote.Visible = false;
            this.trURL.Visible = false;
        }

        List<model.weichat.Dict_SiteInfo> sites = OR.Control.DAL.GetModelList<model.weichat.Dict_SiteInfo>("");
        this.ddpSite.DataSource = sites;
        this.ddpSite.DataBind();

        List<model.weichat.Dict_PriceCode> prices = OR.Control.DAL.GetModelList<model.weichat.Dict_PriceCode>("");
        this.ddpPriceCode.DataSource = prices;
        this.ddpPriceCode.DataBind();

        List<model.weichat.Dict_UnitCode> units = OR.Control.DAL.GetModelList<model.weichat.Dict_UnitCode>("");
        this.ddpUnit.DataSource = units;
        this.ddpUnit.DataBind();
    }

    protected void LoadPrice()
    {
        if (p != 0)
        {
            model.weichat.Data_ItemPriceValue price = OR.Control.DAL.GetModelByKey<model.weichat.Data_ItemPriceValue>(p);

            if (price != null)
            {
                this.txtPriceNote.Text = price.PriceNote;
                this.txtPriceValue.Text = price.PriceValue.ToString();
                this.txtURL.Text = price.URL;
                this.ddpPriceCode.SelectedValue = price.PriceID.ToString();
                this.ddpSite.SelectedValue = price.SiteID.ToString();
                this.ddpUnit.SelectedValue = price.UnitID.ToString();
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        model.weichat.Dict_ItemCode item = OR.Control.DAL.GetModelByKey<model.weichat.Dict_ItemCode>(id);

        model.weichat.Data_ItemPriceValue p = OR.Control.DAL.GetModel<model.weichat.Data_ItemPriceValue>("PriceDate=@date AND ItemID=@id AND SiteID=@site AND PriceID=@pid",
            new System.Data.SqlClient.SqlParameter("@date", date),
            new System.Data.SqlClient.SqlParameter("@id", id),
            new System.Data.SqlClient.SqlParameter("@pid", Int32.Parse(this.ddpPriceCode.SelectedValue)),
            new System.Data.SqlClient.SqlParameter("@site", this.ddpSite.SelectedValue));

        if (p == null)
        {
            p = new model.weichat.Data_ItemPriceValue();
            p.ItemID = id;
            p.PriceDate = date;
            p.PriceID = Int32.Parse(this.ddpPriceCode.SelectedValue);

            if (((model.weichat.TemplateType)item.TemplateType) == model.weichat.TemplateType.URL)
            {
                p.PriceNote = this.txtPriceNote.Text;
                p.URL = this.txtURL.Text;
            }
            else
            {
                p.PriceValue = Double.Parse(this.txtPriceValue.Text);
                p.UnitID = Int32.Parse(this.ddpUnit.SelectedValue);
            }

            p.SiteID = this.ddpSite.SelectedValue;

            OR.Control.DAL.Add<model.weichat.Data_ItemPriceValue>(p, false);
        }
        else
        {
            if (((model.weichat.TemplateType)item.TemplateType) == model.weichat.TemplateType.URL)
            {
                p.PriceNote = this.txtPriceNote.Text;
                p.URL = this.txtURL.Text;
            }
            else
            {
                p.PriceValue = Double.Parse(this.txtPriceValue.Text);
                p.UnitID = Int32.Parse(this.ddpUnit.SelectedValue);
            }

            OR.Control.DAL.Update<model.weichat.Data_ItemPriceValue>(p);
        }

        ClientScript.RegisterStartupScript(typeof(Page), "reload", "parent.ReloadPage();", true);
    }
}