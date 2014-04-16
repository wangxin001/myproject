using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_PriceInput : System.Web.UI.Page
{

    String strDate = String.Empty;
    String strUserGUID = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strDate = Request.QueryString["PriceDate"];
        strUserGUID = Request.QueryString["UserGUID"];


        if (!IsPostBack)
        {
            LoadPriceItem();
        }

    }

    protected void LoadPriceItem()
    {
        List<model.PriceItems> items = OR.Control.DAL.GetModelList<model.PriceItems>(" ReportStatus=1 order by ItemOrder ");

        this.GridView1.DataSource = items;
        this.GridView1.DataBind();

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.PriceItems item = (model.PriceItems)e.Row.DataItem;

            TextBox txtItemID = e.Row.FindControl("txtItemID") as TextBox;

            txtItemID.Text = item.ItemID.ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        DateTime dn = DateTime.Now;

        model.UserInfo u = OR.Control.DAL.GetModelByKey<model.UserInfo>(strUserGUID);

        Int32 batchID = Int32.Parse(OR.DB.SQLHelper.GetSingle("Select ISNULL(MAX(BatchID),0)+1 as B From StorePrice Where StoreGUID = @pGUID and PriceDate = @pDate ",       
            new System.Data.SqlClient.SqlParameter("@pGUID", strUserGUID),
            new System.Data.SqlClient.SqlParameter("@pDate", strDate)).ToString());

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if (this.GridView1.Rows[i].RowType == DataControlRowType.DataRow)
            {
                
                TextBox txtPrice = this.GridView1.Rows[i].FindControl("txtPrice") as TextBox;
                TextBox txtItemID = this.GridView1.Rows[i].FindControl("txtItemID") as TextBox;

                model.PriceItems priceItem = OR.Control.DAL.GetModelByKey<model.PriceItems>(Int32.Parse(txtItemID.Text));
                
                Double pn = 0;

                if (Double.TryParse(txtPrice.Text, out pn))
                {
                    model.StorePrice p = new model.StorePrice();

                    p.BatchID = batchID;
                    p.Created = dn;
                    p.ItemID = priceItem.ItemID;
                    p.Price01 = pn;
                    p.ItemLevel = priceItem.ItemLevel;
                    p.ItemName = priceItem.ItemName;
                    p.ItemType = priceItem.ItemType;
                    p.ItemUnit = priceItem.ItemUnit;
                    p.PriceDate = DateTime.Parse(strDate).Date;
                    p.Status = (int)model.Status.Normal;
                    p.StoreGUID = u.UserGUID;
                    p.StoreName = u.UserName;
                    p.UserGUID = Util.GetLoginUserInfo().UserGUID;

                    OR.Control.DAL.Add<model.StorePrice>(p, false);
                }
            }
        }

        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.CloseFrame();parent.ReloadPage();</script>");
    }

}