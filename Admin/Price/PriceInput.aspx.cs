using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Price_PriceInput : System.Web.UI.Page
{
    String strPriceDate = string.Empty;
    String strPriceType = string.Empty;
    protected model.PriceType priceType = model.PriceType.Chart_CaiJia;

    protected void Page_Load(object sender, EventArgs e)
    {
        strPriceType = Request.QueryString["PriceItem"];
        priceType = (model.PriceType)(Convert.ToInt32(strPriceType));

        strPriceDate = Request.QueryString["PriceDate"];

        if (!IsPostBack)
        {
            LoadPriceDate();
        }
    }

    protected void LoadPriceDate()
    {
        String strSQL = "  Select PriceID,ItemType,PriceDate,Price02,Price03,Price04 From PriceRecord WHERE PriceDate=@PriceDate ";
        strSQL += " AND ItemType = " + ((int)priceType).ToString();
        DataTable dtPrice = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@PriceDate", Convert.ToDateTime(strPriceDate))).Tables[0];

        DataRow[] drPricePI = dtPrice.Select("ItemType=" + ((int)priceType).ToString() + " AND PriceDate=#" + Convert.ToDateTime(strPriceDate).ToString("MM/dd/yyyy") + "#");

        if (drPricePI.Length > 0)
        {
            this.txtPI01.Text = drPricePI[0]["Price02"].ToString();
            this.txtPI02.Text = drPricePI[0]["Price03"].ToString();
            this.txtPI03.Text = drPricePI[0]["Price04"].ToString();
            this.txtPIID.Text = drPricePI[0]["PriceID"].ToString();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        // VPI
        if (!String.IsNullOrEmpty(this.txtPIID.Text))
        {
            // 更新数据
            model.PriceRecord record = OR.Control.DAL.GetModelByKey<model.PriceRecord>(Convert.ToInt32(this.txtPIID.Text));

            record.Price02 = Convert.ToDecimal(this.txtPI01.Text);
            record.Price03 = Convert.ToDecimal(this.txtPI02.Text);
            record.Price04 = Convert.ToDecimal(this.txtPI03.Text);
            OR.Control.DAL.Update<model.PriceRecord>(record);

            Log.info("更新了 " + strPriceDate + " " + EnumUtil.GetDescription(priceType) + "：" + record.ToString(true));
        }
        else
        {
            model.PriceItems item = OR.Control.DAL.GetModel<model.PriceItems>("ItemType=@type", new SqlParameter("@type", (int)priceType));

            model.PriceRecord record = new model.PriceRecord();

            record.ItemID = (item == null ? 0 : item.ItemID);
            record.ItemName = (item == null ? "" : item.ItemName);
            record.ItemType = (int)priceType;
            record.ItemUnit = "";
            record.Price01 = 0.0M;
            record.Price02 = Convert.ToDecimal(this.txtPI01.Text);
            record.Price03 = Convert.ToDecimal(this.txtPI02.Text);
            record.Price04 = Convert.ToDecimal(this.txtPI03.Text);
            record.PriceDate = Convert.ToDateTime(strPriceDate);
            record = OR.Control.DAL.Add<model.PriceRecord>(record, true);

            Log.info("添加了 " + strPriceDate + " " + EnumUtil.GetDescription(priceType) + "：" + record.ToString(true));
        }

        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.CloseFrame();parent.ReloadPage();</script>");
    }
}