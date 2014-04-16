﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Price_VPIInput : System.Web.UI.Page
{
    String strPriceDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strPriceDate = Request.QueryString["PriceDate"];

        if (!IsPostBack)
        {
            LoadPriceDate();
        }
    }

    protected void LoadPriceDate()
    {
        String strSQL = "  Select PriceID,ItemType,PriceDate,Price02,Price03,Price04 From PriceRecord WHERE PriceDate=@PriceDate ";
        strSQL += " AND ItemType = " + ((int)model.PriceType.Index_VPI).ToString();
        DataTable dtPrice = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@PriceDate", Convert.ToDateTime(strPriceDate))).Tables[0];

        DataRow[] drPriceVPI = dtPrice.Select("ItemType=" + ((int)model.PriceType.Index_VPI).ToString() + " AND PriceDate=#" + Convert.ToDateTime(strPriceDate).ToString("MM/dd/yyyy") + "#");

        if (drPriceVPI.Length > 0)
        {
            this.txtVPI01.Text = drPriceVPI[0]["Price02"].ToString();
            this.txtVPI02.Text = drPriceVPI[0]["Price03"].ToString();
            this.txtVPI03.Text = drPriceVPI[0]["Price04"].ToString();
            this.txtVPIID.Text = drPriceVPI[0]["PriceID"].ToString();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        // VPI
        if (!String.IsNullOrEmpty(this.txtVPIID.Text))
        {
            // 更新数据
            model.PriceRecord record = OR.Control.DAL.GetModelByKey<model.PriceRecord>(Convert.ToInt32(this.txtVPIID.Text));

            record.Price02 = Convert.ToDecimal(this.txtVPI01.Text);
            record.Price03 = Convert.ToDecimal(this.txtVPI02.Text);
            record.Price04 = Convert.ToDecimal(this.txtVPI03.Text);
            OR.Control.DAL.Update<model.PriceRecord>(record);

            Log.info("更新了 " + strPriceDate + " VPI价格指数：" + record.ToString(true));
        }
        else
        {
            model.PriceRecord record = new model.PriceRecord();

            record.ItemID = 0;
            record.ItemName = "";
            record.ItemType = (int)model.PriceType.Index_VPI;
            record.ItemUnit = "";
            record.Price01 = 0.0M;
            record.Price02 = Convert.ToDecimal(this.txtVPI01.Text);
            record.Price03 = Convert.ToDecimal(this.txtVPI02.Text);
            record.Price04 = Convert.ToDecimal(this.txtVPI03.Text);
            record.PriceDate = Convert.ToDateTime(strPriceDate);
            record = OR.Control.DAL.Add<model.PriceRecord>(record, true);

            Log.info("添加了 " + strPriceDate + " VPI价格指数：" + record.ToString(true));
        }

        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.CloseFrame();parent.ReloadPage();</script>");
    }
}