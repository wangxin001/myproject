using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class jrc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        LoadVegetablePrice();
        LoadData();
    }


    protected void LoadVegetablePrice()
    {
        String strSQL = "SELECT i.ItemID ,i.ItemName, i.ItemType, r.PriceDate, r.Price01, r.Price02, r.Price03, r.Price04 ";
        strSQL += " FROM PriceRecord r inner join PriceItems i on r.ItemID=i.ItemID where i.ItemType=1 and i.Status=1 ";
        strSQL += " and PriceDate = (select MAX(PriceDate) From PriceRecord where ItemType=1) order by i.ItemOrder ";

        DataTable vegetableData = OR.DB.SQLHelper.Query(strSQL).Tables[0];

        if (vegetableData.Rows.Count > 0)
        {
            this.txtVegetablePriceDate.Text = Convert.ToDateTime(vegetableData.Rows[0]["PriceDate"]).ToString("yyyy-MM-dd");
        }
        this.rpVegetablePrice.DataSource = vegetableData;
        this.rpVegetablePrice.DataBind();
    }




    protected String strPrice02 = string.Empty;
    protected String strPrice03 = string.Empty;
    protected String strPrice04 = string.Empty;

    protected String strPriceVPI = string.Empty;

    protected String strXAxisVPI = string.Empty;

    protected String strStartDateVPI = string.Empty;


    protected void LoadData()
    {
        #region 获得配置项，每个曲线抓几个数据点

        String VPIPointCount = System.Configuration.ConfigurationManager.AppSettings["VPICount"];
        if (String.IsNullOrEmpty(VPIPointCount))
        {
            VPIPointCount = "100";
        }
        int iVpiCount = 0;
        if (!Int32.TryParse(VPIPointCount, out iVpiCount))
        {
            iVpiCount = 100;
        }
        #endregion


        String strSQL = "Select * From (Select top " + iVpiCount.ToString() + " ItemType, PriceDate,ISNULL(Price02,0.0) Price02,ISNULL(Price03,0.0) Price03,ISNULL(Price04,0.0) Price04 ";
        strSQL += "From PriceRecord where ItemType=" + ((int)model.PriceType.Index_VPI).ToString() + " Order By PriceDate Desc) t1";
        
        DataTable dt = OR.DB.SQLHelper.Query(strSQL).Tables[0];

        #region 计算VPI

        DataRow[] drVPI = dt.Select(" ItemType=" + ((int)model.PriceType.Index_VPI).ToString(), " PriceDate Desc");
        DateTime startDateV = Convert.ToDateTime(drVPI[0]["PriceDate"].ToString()).AddDays(1 - iVpiCount);
        strStartDateVPI = String.Format("{0}, {1}, {2}", startDateV.Year, startDateV.Month - 1, startDateV.Day);

        for (int i = 0; i < iVpiCount; i++)
        {
            DateTime curDay = Convert.ToDateTime(startDateV.AddDays(i));

            DataRow[] dr = dt.Select("ItemType=" + ((int)model.PriceType.Index_VPI).ToString() + " AND PriceDate=#" + curDay.ToString("MM/dd/yyyy") + "#");

            if (dr.Length > 0)
            {
                strPrice02 += dr[0]["Price02"].ToString() + ", ";
                strPrice03 += dr[0]["Price03"].ToString() + ", ";
                strPrice04 += dr[0]["Price04"].ToString() + ", ";
            }
            else
            {
                strPrice02 += "0.0, ";
                strPrice03 += "0.0, ";
                strPrice04 += "0.0, ";
            }

            strXAxisVPI += String.Format("'{0:MM-dd}', ", curDay);
        }

        strPrice02 = strPrice02.Substring(0, strPrice02.Length - 2);
        strPrice03 = strPrice03.Substring(0, strPrice03.Length - 2);
        strPrice04 = strPrice04.Substring(0, strPrice04.Length - 2);

        strXAxisVPI = strXAxisVPI.Substring(0, strXAxisVPI.Length - 2);
        #endregion
    }
}