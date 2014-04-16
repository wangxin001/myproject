using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Vegetables : System.Web.UI.Page
{

    protected String strPrice02 = string.Empty;
    protected String strPrice03 = string.Empty;
    protected String strPrice04 = string.Empty;

    protected String strPriceCPI2 = string.Empty;
    protected String strPriceCPI3 = string.Empty;

    protected String strXAxisCPI = string.Empty;
    protected String strXAxisVPI = string.Empty;

    protected String strStartDateVPI = string.Empty;
    protected String strStartDateCPI = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void LoadData()
    {
        #region 获得配置项，每个曲线抓几个数据点

        String CPIPointCount = System.Configuration.ConfigurationManager.AppSettings["CPICount"];
        if (String.IsNullOrEmpty(CPIPointCount))
        {
            CPIPointCount = "40";
        }

        int iCpiCount = 0;
        if (!Int32.TryParse(CPIPointCount, out iCpiCount))
        {
            iCpiCount = 40;
        }


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
        strSQL += " UNION ALL ";
        strSQL += "Select * From (Select top " + iCpiCount.ToString() + " ItemType, PriceDate, ISNULL(Price02,0.0) Price02, ISNULL(Price03, 0.0) Price03, 0.0 Price04 ";
        strSQL += "From PriceRecord where ItemType=" + ((int)model.PriceType.Index_CPI).ToString() + " Order By PriceDate Desc) t2";

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

        #region 计算CPI

        DataRow[] drCPI = dt.Select(" ItemType=" + ((int)model.PriceType.Index_CPI).ToString(), " PriceDate Desc");
        DateTime startDateC = Convert.ToDateTime(drCPI[0]["PriceDate"].ToString()).AddMonths(1 - iCpiCount);

        strStartDateCPI = String.Format("{0}, {1}, {2}", startDateC.Year, startDateC.Month - 1, 1);

        for (int i = 0; i < iCpiCount; i++)
        {
            DateTime curMonth = Convert.ToDateTime(startDateC.AddMonths(i));

            DataRow[] dr = dt.Select("ItemType=" + ((int)model.PriceType.Index_CPI).ToString() + " AND PriceDate=#" + curMonth.ToString("MM/dd/yyyy") + "#");

            if (dr.Length > 0)
            {
                strPriceCPI2 += dr[0]["Price02"].ToString() + ", ";
                strPriceCPI3 += dr[0]["Price03"].ToString() + ", ";
            }
            else
            {
                strPriceCPI2 += "null, ";
                strPriceCPI3 += "null, ";
            }

            strXAxisCPI += String.Format("'{0:yy-MM}', ", curMonth);
        }

        strPriceCPI2 = strPriceCPI2.Substring(0, strPriceCPI2.Length - 2);
        strPriceCPI3 = strPriceCPI3.Substring(0, strPriceCPI3.Length - 2);

        strXAxisCPI = strXAxisCPI.Substring(0, strXAxisCPI.Length - 2);
        #endregion

    }
}