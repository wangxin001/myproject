using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class LineChart : System.Web.UI.Page
{
    // 菜价
    protected String strPriceCaiJia2 = string.Empty;
    protected String strPriceCaiJia3 = string.Empty;
    protected String strPriceCaiJia4 = string.Empty;
    protected String strXAxisCaiJia = string.Empty;
    protected String strStartCaiJia = string.Empty;
    protected Double strMaxCaiJia = 0;
    protected Double strMinCaiJia = 0;

    // 富强粉
    protected String strPriceFuQiangFen2 = string.Empty;
    protected String strPriceFuQiangFen3 = string.Empty;
    protected String strPriceFuQiangFen4 = string.Empty;
    protected String strXAxisFuQiangFen = string.Empty;
    protected String strStartFuQiangFen = string.Empty;
    protected Double strMaxFuQiangFen = 0;
    protected Double strMinFuQiangFen = 0;

    // 粳米
    protected String strPriceJingMi2 = string.Empty;
    protected String strPriceJingMi3 = string.Empty;
    protected String strPriceJingMi4 = string.Empty;
    protected String strXAxisJingMi = string.Empty;
    protected String strStartJingMi = string.Empty;
    protected Double strMaxJingMi = 0;
    protected Double strMinJingMi = 0;

    // 调和油
    protected String strPriceTiaoHeYou2 = string.Empty;
    protected String strPriceTiaoHeYou3 = string.Empty;
    protected String strPriceTiaoHeYou4 = string.Empty;
    protected String strXAxisTiaoHeYou = string.Empty;
    protected String strStartTiaoHeYou = string.Empty;
    protected Double strMaxTiaoHeYou = 0;
    protected Double strMinTiaoHeYou = 0;

    // 猪肉
    protected String strPriceZhuRou2 = string.Empty;
    protected String strPriceZhuRou3 = string.Empty;
    protected String strPriceZhuRou4 = string.Empty;
    protected String strXAxisZhuRou = string.Empty;
    protected String strStartZhuRou = string.Empty;
    protected Double strMaxZhuRou = 0;
    protected Double strMinZhuRou = 0;

    // 鸡蛋
    protected String strPriceJiDan2 = string.Empty;
    protected String strPriceJiDan3 = string.Empty;
    protected String strPriceJiDan4 = string.Empty;
    protected String strXAxisJiDan = string.Empty;
    protected String strStartJiDan = string.Empty;
    protected Double strMaxJiDan = 0;
    protected Double strMinJiDan = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPriceRange();
            LoadData();
        }
    }

    protected void LoadData()
    {
        #region 获得配置项，每个曲线抓几个数据点

        // 菜价
        String PointCountCaiJia = System.Configuration.ConfigurationManager.AppSettings["CountCaiJia"];
        if (String.IsNullOrEmpty(PointCountCaiJia))
        {
            PointCountCaiJia = "40";
        }

        int iCountCaiJia = 0;
        if (!Int32.TryParse(PointCountCaiJia, out iCountCaiJia))
        {
            iCountCaiJia = 40;
        }

        // 富强粉
        String PointCountFuQiangFen = System.Configuration.ConfigurationManager.AppSettings["CountFuQiangFen"];
        if (String.IsNullOrEmpty(PointCountFuQiangFen))
        {
            PointCountFuQiangFen = "40";
        }

        int iCountFuQiangFen = 0;
        if (!Int32.TryParse(PointCountFuQiangFen, out iCountFuQiangFen))
        {
            iCountFuQiangFen = 40;
        }

        // 粳米
        String PointCountJingMi = System.Configuration.ConfigurationManager.AppSettings["CountJingMi"];
        if (String.IsNullOrEmpty(PointCountJingMi))
        {
            PointCountJingMi = "40";
        }

        int iCountJingMi = 0;
        if (!Int32.TryParse(PointCountJingMi, out iCountJingMi))
        {
            iCountJingMi = 40;
        }

        // 调和油
        String PointCountTiaoHeYou = System.Configuration.ConfigurationManager.AppSettings["CountTiaoHeYou"];
        if (String.IsNullOrEmpty(PointCountTiaoHeYou))
        {
            PointCountTiaoHeYou = "40";
        }

        int iCountTiaoHeYou = 0;
        if (!Int32.TryParse(PointCountTiaoHeYou, out iCountTiaoHeYou))
        {
            iCountTiaoHeYou = 40;
        }

        // 猪肉
        String PointCountZhuRou = System.Configuration.ConfigurationManager.AppSettings["CountZhuRou"];
        if (String.IsNullOrEmpty(PointCountZhuRou))
        {
            PointCountZhuRou = "40";
        }

        int iCountZhuRou = 0;
        if (!Int32.TryParse(PointCountZhuRou, out iCountZhuRou))
        {
            iCountZhuRou = 40;
        }

        // 鸡蛋
        String PointCountJiDan = System.Configuration.ConfigurationManager.AppSettings["CountJiDan"];
        if (String.IsNullOrEmpty(PointCountJiDan))
        {
            PointCountJiDan = "40";
        }

        int iCountJiDan = 0;
        if (!Int32.TryParse(PointCountJiDan, out iCountJiDan))
        {
            iCountJiDan = 40;
        }

        #endregion

        #region 从数据库查询内容

        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("Select * From (Select top " + iCountCaiJia.ToString() + " ItemType, PriceDate, ISNULL(Price02,0.0) Price02, ISNULL(Price03, 0.0) Price03, ISNULL(Price04, 0.0) Price04 ");
        strSQL.Append("From PriceRecord where ItemType=" + ((int)model.PriceType.Chart_CaiJia ).ToString() + " Order By PriceDate Desc) t2");

        strSQL.Append(" UNION ALL ");
        strSQL.Append("Select * From (Select top " + iCountFuQiangFen.ToString() + " ItemType, PriceDate, ISNULL(Price02,0.0) Price02, ISNULL(Price03, 0.0) Price03, ISNULL(Price04, 0.0) Price04 ");
        strSQL.Append("From PriceRecord where ItemType=" + ((int)model.PriceType.Chart_FuQiangFen).ToString() + " Order By PriceDate Desc) t3");

        strSQL.Append(" UNION ALL ");
        strSQL.Append("Select * From (Select top " + iCountJingMi.ToString() + " ItemType, PriceDate, ISNULL(Price02,0.0) Price02, ISNULL(Price03, 0.0) Price03, ISNULL(Price04, 0.0) Price04 ");
        strSQL.Append("From PriceRecord where ItemType=" + ((int)model.PriceType.Chart_TiaoHeYou).ToString() + " Order By PriceDate Desc) t3");

        strSQL.Append(" UNION ALL ");
        strSQL.Append("Select * From (Select top " + iCountTiaoHeYou.ToString() + " ItemType, PriceDate, ISNULL(Price02,0.0) Price02, ISNULL(Price03, 0.0) Price03, ISNULL(Price04, 0.0) Price04 ");
        strSQL.Append("From PriceRecord where ItemType=" + ((int)model.PriceType.Chart_JingMi).ToString() + " Order By PriceDate Desc) t3");

        strSQL.Append(" UNION ALL ");
        strSQL.Append("Select * From (Select top " + iCountZhuRou.ToString() + " ItemType, PriceDate, ISNULL(Price02,0.0) Price02, ISNULL(Price03, 0.0) Price03, ISNULL(Price04, 0.0) Price04 ");
        strSQL.Append("From PriceRecord where ItemType=" + ((int)model.PriceType.Chart_ZhuRou).ToString() + " Order By PriceDate Desc) t3");

        strSQL.Append(" UNION ALL ");
        strSQL.Append("Select * From (Select top " + iCountJiDan.ToString() + " ItemType, PriceDate, ISNULL(Price02,0.0) Price02, ISNULL(Price03, 0.0) Price03, ISNULL(Price04, 0.0) Price04 ");
        strSQL.Append("From PriceRecord where ItemType=" + ((int)model.PriceType.Chart_JiDan).ToString() + " Order By PriceDate Desc) t3");

        DataTable dt = OR.DB.SQLHelper.Query(strSQL.ToString()).Tables[0];

        #endregion

        #region 计算菜价

        DataRow[] drCaiJia = dt.Select(" ItemType=" + ((int)model.PriceType.Chart_CaiJia).ToString(), " PriceDate Desc");
        DateTime startDateCaiJia = Convert.ToDateTime(drCaiJia[0]["PriceDate"].ToString()).AddDays(1 - iCountCaiJia);

        strStartCaiJia = String.Format("{0}, {1}, {2}", startDateCaiJia.Year, startDateCaiJia.Month - 1, 1);

        for (int i = 0; i < iCountCaiJia; i++)
        {
            DateTime curMonth = Convert.ToDateTime(startDateCaiJia.AddDays(i));

            DataRow[] dr = dt.Select("ItemType=" + ((int)model.PriceType.Chart_CaiJia).ToString() + " AND PriceDate=#" + curMonth.ToString("MM/dd/yyyy") + "#");

            if (dr.Length > 0)
            {
                strPriceCaiJia2 += dr[0]["Price02"].ToString() + ", ";
                strPriceCaiJia3 += dr[0]["Price03"].ToString() + ", ";
                strPriceCaiJia4 += dr[0]["Price04"].ToString() + ", ";
            }
            else
            {
                strPriceCaiJia2 += "null, ";
                strPriceCaiJia3 += "null, ";
                strPriceCaiJia4 += "null, ";
            }

            strXAxisCaiJia += String.Format("'{0:MM-dd}', ", curMonth);
        }

        strPriceCaiJia2 = strPriceCaiJia2.Trim(new char[] { ' ', ',' });
        strPriceCaiJia3 = strPriceCaiJia3.Trim(new char[] { ' ', ',' });
        strPriceCaiJia4 = strPriceCaiJia4.Trim(new char[] { ' ', ',' });
        strXAxisCaiJia = strXAxisCaiJia.Trim(new char[] { ' ', ',' });

        #endregion

        #region 计算粳米

        DataRow[] drJingMi = dt.Select(" ItemType=" + ((int)model.PriceType.Chart_JingMi).ToString(), " PriceDate Desc");
        DateTime startDateJingMi = Convert.ToDateTime(drJingMi[0]["PriceDate"].ToString()).AddDays(1 - iCountJingMi);

        strStartJingMi = String.Format("{0}, {1}, {2}", startDateJingMi.Year, startDateJingMi.Month - 1, 1);

        for (int i = 0; i < iCountJingMi; i++)
        {
            DateTime curMonth = Convert.ToDateTime(startDateJingMi.AddDays(i));

            DataRow[] dr = dt.Select("ItemType=" + ((int)model.PriceType.Chart_JingMi).ToString() + " AND PriceDate=#" + curMonth.ToString("MM/dd/yyyy") + "#");

            if (dr.Length > 0)
            {
                strPriceJingMi2 += dr[0]["Price02"].ToString() + ", ";
                strPriceJingMi3 += dr[0]["Price03"].ToString() + ", ";
                strPriceJingMi4 += dr[0]["Price04"].ToString() + ", ";
            }
            else
            {
                strPriceJingMi2 += "null, ";
                strPriceJingMi3 += "null, ";
                strPriceJingMi4 += "null, ";
            }

            strXAxisJingMi += String.Format("'{0:MM-dd}', ", curMonth);
        }

        strPriceJingMi2 = strPriceJingMi2.Trim(new char[] { ' ', ',' });
        strPriceJingMi3 = strPriceJingMi3.Trim(new char[] { ' ', ',' });
        strPriceJingMi4 = strPriceJingMi4.Trim(new char[] { ' ', ',' });
        strXAxisJingMi = strXAxisJingMi.Trim(new char[] { ' ', ',' });

        #endregion

        #region 计算富强粉

        DataRow[] drFuQiangFen = dt.Select(" ItemType=" + ((int)model.PriceType.Chart_FuQiangFen).ToString(), " PriceDate Desc");
        DateTime startDateFuQiangFen = Convert.ToDateTime(drFuQiangFen[0]["PriceDate"].ToString()).AddDays(1 - iCountFuQiangFen);

        strStartFuQiangFen = String.Format("{0}, {1}, {2}", startDateFuQiangFen.Year, startDateFuQiangFen.Month - 1, 1);

        for (int i = 0; i < iCountFuQiangFen; i++)
        {
            DateTime curMonth = Convert.ToDateTime(startDateFuQiangFen.AddDays(i));

            DataRow[] dr = dt.Select("ItemType=" + ((int)model.PriceType.Chart_FuQiangFen).ToString() + " AND PriceDate=#" + curMonth.ToString("MM/dd/yyyy") + "#");

            if (dr.Length > 0)
            {
                strPriceFuQiangFen2 += dr[0]["Price02"].ToString() + ", ";
                strPriceFuQiangFen3 += dr[0]["Price03"].ToString() + ", ";
                strPriceFuQiangFen4 += dr[0]["Price04"].ToString() + ", ";
            }
            else
            {
                strPriceFuQiangFen2 += "null, ";
                strPriceFuQiangFen3 += "null, ";
                strPriceFuQiangFen4 += "null, ";
            }

            strXAxisFuQiangFen += String.Format("'{0:MM-dd}', ", curMonth);
        }

        strPriceFuQiangFen2 = strPriceFuQiangFen2.Trim(new char[] { ' ', ',' });
        strPriceFuQiangFen3 = strPriceFuQiangFen3.Trim(new char[] { ' ', ',' });
        strPriceFuQiangFen4 = strPriceFuQiangFen4.Trim(new char[] { ' ', ',' });
        strXAxisFuQiangFen = strXAxisFuQiangFen.Trim(new char[] { ' ', ',' });

        #endregion

        #region 计算调和油

        DataRow[] drTiaoHeYou = dt.Select(" ItemType=" + ((int)model.PriceType.Chart_TiaoHeYou).ToString(), " PriceDate Desc");
        DateTime startDateTiaoHeYou = Convert.ToDateTime(drTiaoHeYou[0]["PriceDate"].ToString()).AddDays(1 - iCountTiaoHeYou);

        strStartTiaoHeYou = String.Format("{0}, {1}, {2}", startDateTiaoHeYou.Year, startDateTiaoHeYou.Month - 1, 1);

        for (int i = 0; i < iCountTiaoHeYou; i++)
        {
            DateTime curMonth = Convert.ToDateTime(startDateTiaoHeYou.AddDays(i));

            DataRow[] dr = dt.Select("ItemType=" + ((int)model.PriceType.Chart_TiaoHeYou).ToString() + " AND PriceDate=#" + curMonth.ToString("MM/dd/yyyy") + "#");

            if (dr.Length > 0)
            {
                strPriceTiaoHeYou2 += dr[0]["Price02"].ToString() + ", ";
                strPriceTiaoHeYou3 += dr[0]["Price03"].ToString() + ", ";
                strPriceTiaoHeYou4 += dr[0]["Price04"].ToString() + ", ";
            }
            else
            {
                strPriceTiaoHeYou2 += "null, ";
                strPriceTiaoHeYou3 += "null, ";
                strPriceTiaoHeYou4 += "null, ";
            }

            strXAxisTiaoHeYou += String.Format("'{0:MM-dd}', ", curMonth);
        }

        strPriceTiaoHeYou2 = strPriceTiaoHeYou2.Trim(new char[] { ' ', ',' });
        strPriceTiaoHeYou3 = strPriceTiaoHeYou3.Trim(new char[] { ' ', ',' });
        strPriceTiaoHeYou4 = strPriceTiaoHeYou4.Trim(new char[] { ' ', ',' });
        strXAxisTiaoHeYou = strXAxisTiaoHeYou.Trim(new char[] { ' ', ',' });

        #endregion

        #region 计算猪肉

        DataRow[] drZhuRou = dt.Select(" ItemType=" + ((int)model.PriceType.Chart_ZhuRou).ToString(), " PriceDate Desc");
        DateTime startDateZhuRou = Convert.ToDateTime(drZhuRou[0]["PriceDate"].ToString()).AddDays(1 - iCountZhuRou);

        strStartZhuRou = String.Format("{0}, {1}, {2}", startDateZhuRou.Year, startDateZhuRou.Month - 1, 1);

        for (int i = 0; i < iCountZhuRou; i++)
        {
            DateTime curMonth = Convert.ToDateTime(startDateZhuRou.AddDays(i));

            DataRow[] dr = dt.Select("ItemType=" + ((int)model.PriceType.Chart_ZhuRou).ToString() + " AND PriceDate=#" + curMonth.ToString("MM/dd/yyyy") + "#");

            if (dr.Length > 0)
            {
                strPriceZhuRou2 += dr[0]["Price02"].ToString() + ", ";
                strPriceZhuRou3 += dr[0]["Price03"].ToString() + ", ";
                strPriceZhuRou4 += dr[0]["Price04"].ToString() + ", ";
            }
            else
            {
                strPriceZhuRou2 += "null, ";
                strPriceZhuRou3 += "null, ";
                strPriceZhuRou4 += "null, ";
            }

            strXAxisZhuRou += String.Format("'{0:MM-dd}', ", curMonth);
        }

        strPriceZhuRou2 = strPriceZhuRou2.Trim(new char[] { ' ', ',' });
        strPriceZhuRou3 = strPriceZhuRou3.Trim(new char[] { ' ', ',' });
        strPriceZhuRou4 = strPriceZhuRou4.Trim(new char[] { ' ', ',' });
        strXAxisZhuRou = strXAxisZhuRou.Trim(new char[] { ' ', ',' });

        #endregion

        #region 计算鸡蛋

        DataRow[] drJiDan = dt.Select(" ItemType=" + ((int)model.PriceType.Chart_JiDan).ToString(), " PriceDate Desc");
        DateTime startDateJiDan = Convert.ToDateTime(drJiDan[0]["PriceDate"].ToString()).AddDays(1 - iCountJiDan);

        strStartJiDan = String.Format("{0}, {1}, {2}", startDateJiDan.Year, startDateJiDan.Month - 1, 1);

        for (int i = 0; i < iCountJiDan; i++)
        {
            DateTime curMonth = Convert.ToDateTime(startDateJiDan.AddDays(i));

            DataRow[] dr = dt.Select("ItemType=" + ((int)model.PriceType.Chart_JiDan).ToString() + " AND PriceDate=#" + curMonth.ToString("MM/dd/yyyy") + "#");

            if (dr.Length > 0)
            {
                strPriceJiDan2 += dr[0]["Price02"].ToString() + ", ";
                strPriceJiDan3 += dr[0]["Price03"].ToString() + ", ";
                strPriceJiDan4 += dr[0]["Price04"].ToString() + ", ";
            }
            else
            {
                strPriceJiDan2 += "null, ";
                strPriceJiDan3 += "null, ";
                strPriceJiDan4 += "null, ";
            }

            strXAxisJiDan += String.Format("'{0:MM-dd}', ", curMonth);
        }

        strPriceJiDan2 = strPriceJiDan2.Trim(new char[] { ' ', ',' });
        strPriceJiDan3 = strPriceJiDan3.Trim(new char[] { ' ', ',' });
        strPriceJiDan4 = strPriceJiDan4.Trim(new char[] { ' ', ',' });
        strXAxisJiDan = strXAxisJiDan.Trim(new char[] { ' ', ',' });

        #endregion
    }

    protected void LoadPriceRange()
    {
        List<model.PriceRange> ranges = OR.Control.DAL.GetModelList<model.PriceRange>("1=1");

        foreach (model.PriceRange item in ranges)
        {
            switch ((model.PriceType)item.ItemID)
            {
                case model.PriceType.Chart_CaiJia:
                    this.strMaxCaiJia = Convert.ToDouble(item.Max);
                    this.strMinCaiJia = Convert.ToDouble(item.Min);
                    break;
                case model.PriceType.Chart_FuQiangFen:
                    this.strMaxFuQiangFen = Convert.ToDouble(item.Max);
                    this.strMinFuQiangFen = Convert.ToDouble(item.Min);
                    break;
                case model.PriceType.Chart_JingMi:
                    this.strMaxJingMi = Convert.ToDouble(item.Max);
                    this.strMinJingMi = Convert.ToDouble(item.Min);
                    break;
                case model.PriceType.Chart_TiaoHeYou:
                    this.strMaxTiaoHeYou = Convert.ToDouble(item.Max);
                    this.strMinTiaoHeYou = Convert.ToDouble(item.Min);
                    break;
                case model.PriceType.Chart_ZhuRou:
                    this.strMaxZhuRou = Convert.ToDouble(item.Max);
                    this.strMinZhuRou = Convert.ToDouble(item.Min);
                    break;
                case model.PriceType.Chart_JiDan:
                    this.strMaxJiDan = Convert.ToDouble(item.Max);
                    this.strMinJiDan = Convert.ToDouble(item.Min);
                    break;
                default:
                    break;
            }
        }
    }
}