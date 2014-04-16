using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Price_YuanYouInput : System.Web.UI.Page
{
    String strPriceDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strPriceDate = Convert.ToDateTime(Request.QueryString["PriceDate"]).ToString("yyyy-MM-dd");

        if (!IsPostBack)
        {
            LoadPriceDate();
        }
    }

    protected void LoadPriceDate()
    {
        String strSQL = "  Select PriceID,ItemType,PriceDate,Price02,Price03,ISNULL(Price04,0.0) Price04 From PriceRecord WHERE PriceDate=@PriceDate ";
        strSQL += " AND ItemType = " + ((int)model.PriceType.Index_YuanYou).ToString();
        DataTable dtPrice = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@PriceDate", Convert.ToDateTime(strPriceDate))).Tables[0];

        DataRow[] drPriceIndex = dtPrice.Select("PriceDate=#" + Convert.ToDateTime(strPriceDate).ToString("MM/dd/yyyy") + "#");

        if (drPriceIndex.Length > 0)
        {
            this.txtPriceIndex1.Text = drPriceIndex[0]["Price02"].ToString();
            this.txtPriceIndex2.Text = drPriceIndex[0]["Price03"].ToString();
            this.txtPriceIndexID.Text = drPriceIndex[0]["PriceID"].ToString();
            this.chkHidden.Checked = (Convert.ToDecimal(drPriceIndex[0]["Price04"].ToString()) == 1.0M ? true : false);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        // VPI
        if (!String.IsNullOrEmpty(this.txtPriceIndexID.Text))
        {
            // 更新数据
            model.PriceRecord record = OR.Control.DAL.GetModelByKey<model.PriceRecord>(Convert.ToInt32(this.txtPriceIndexID.Text));

            record.Price02 = Convert.ToDecimal(this.txtPriceIndex1.Text);
            record.Price03 = Convert.ToDecimal(this.txtPriceIndex2.Text);
            record.Price04 = this.chkHidden.Checked ? 1.0M : 0.0M;
            OR.Control.DAL.Update<model.PriceRecord>(record);

            Log.info("更新了 " + strPriceDate + " 原油价格指数：" + record.ToString(true));
        }
        else
        {
            model.PriceRecord record = new model.PriceRecord();

            record.ItemID = 0;
            record.ItemName = "";
            record.ItemType = (int)model.PriceType.Index_YuanYou;
            record.ItemUnit = "";
            record.Price01 = 0.0M;
            record.Price02 = Convert.ToDecimal(this.txtPriceIndex1.Text);
            record.Price03 = Convert.ToDecimal(this.txtPriceIndex2.Text);
            record.Price04 = this.chkHidden.Checked ? 1.0M : 0.0M;
            record.PriceDate = Convert.ToDateTime(strPriceDate);
            record = OR.Control.DAL.Add<model.PriceRecord>(record, true);

            Log.info("添加了 " + strPriceDate + " 原油价格指数：" + record.ToString(true));
        }

        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.CloseFrame();parent.ReloadPage();</script>");
    }
}