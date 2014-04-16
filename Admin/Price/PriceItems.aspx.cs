using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Price_PriceItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "价格条目管理";

        if (!IsPostBack)
        {
            LoadItemType();
            LoadPriceItem();
        }
    }

    protected void LoadItemType()
    {
        this.ddlItemType.Items.Add(new ListItem("每日菜价", ((int)model.PriceType.Price_VegetablePrice).ToString()));
        this.ddlItemType.Items.Add(new ListItem("每日粮油", ((int)model.PriceType.Price_LiangYou).ToString()));
        this.ddlItemType.Items.Add(new ListItem("每日肉蛋水产", ((int)model.PriceType.Price_RouDan).ToString()));
        this.ddlItemType.Items.Add(new ListItem("大宗货品", ((int)model.PriceType.Price_CargoPrice).ToString()));
    }

    protected void LoadPriceItem()
    {
        List<model.PriceItems> Vs = OR.Control.DAL.GetModelList<model.PriceItems>("ItemType=" + this.ddlItemType.SelectedValue + " Order By ItemOrder");

        this.GridView1.DataSource = Vs;
        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.PriceItems priceItem = (model.PriceItems)e.Row.DataItem;

            LinkButton btnDel = e.Row.FindControl("btnDel") as LinkButton;
            LinkButton btnMod = e.Row.FindControl("btnMod") as LinkButton;

            btnDel.CommandName = "Del";
            btnDel.CommandArgument = priceItem.ItemID.ToString();

            btnMod.Attributes["onclick"] = "javascript:ModifyVegetable(" + priceItem.ItemID.ToString() + ", " + this.ddlItemType.SelectedValue + ");return false;";

            LinkButton btnHidden = e.Row.FindControl("btnHidden") as LinkButton;
            LinkButton btnPublic = e.Row.FindControl("btnPublic") as LinkButton;

            btnHidden.CommandName = "Hidden";
            btnPublic.CommandName = "Public";

            btnHidden.CommandArgument = priceItem.ItemID.ToString();
            btnPublic.CommandArgument = priceItem.ItemID.ToString();

            if ((model.Status)priceItem.Status == model.Status.Forbidden)
            {
                e.Row.ForeColor = System.Drawing.Color.FromArgb(168, 168, 168);
                btnHidden.Visible = false;
            }
            else
            {
                btnPublic.Visible = false;
            }

            e.Row.Cells[6].Text = ((model.Status)priceItem.Status == model.Status.Normal ? "正常" : "隐藏");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        model.PriceItems priceItem = OR.Control.DAL.GetModelByKey<model.PriceItems>(Convert.ToInt32(e.CommandArgument));

        if (e.CommandName == "Del")
        {
            OR.Control.DAL.Delete<model.PriceItems>(priceItem);
            Log.info("删除价格条目：" + priceItem.ToString(true));
        }
        else if (e.CommandName == "Hidden")
        {
            priceItem.Status = (int)model.Status.Forbidden;
            OR.Control.DAL.Update<model.PriceItems>(priceItem);
            Log.info("隐藏价格条目：" + priceItem.ItemID.ToString() + " -> " + priceItem.ItemName);
        }
        else if (e.CommandName == "Public")
        {
            priceItem.Status = (int)model.Status.Normal;
            OR.Control.DAL.Update<model.PriceItems>(priceItem);
            Log.info("公开价格条目：" + priceItem.ItemID.ToString() + " -> " + priceItem.ItemName);
        }

        LoadPriceItem();
    }
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPriceItem();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        LoadPriceItem();
    }
}