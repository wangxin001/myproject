using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

/// <summary>
///SyncService 的摘要说明
/// </summary>
public class SyncService
{
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #region 基础工具类定义

    /// <summary>
    /// it, stagecode
    /// Z000003: 零售价,  H000005: 零售
    /// Z000001: 批发价,  H000004: 批发
    /// </summary>
    public static String[][] detailItemParam = null;
    public static String[][] averageItemParam = null;

    /// <summary>
    /// 完整URL, 0:it,  1:stagecode,  2:date,  3:id,   4:sitecodes, 5: freqinfo F0007日报 F0006寻报
    /// </summary>
    private const String detailURL = "http://www.ccpn.gov.cn/syncservice/reportmaster/getDetailMoreSecData.action?para=<para><type>1</type><freqinfo>{5}</freqinfo><datasource>S000001</datasource><cjtypecode></cjtypecode><it>{0}</it><stagecode>{1}</stagecode><date>{2}</date><id>{3}</id><sitecodes>{4}</sitecodes></para>";

    /// <summary>
    /// 完整URL, 0:ids array,  1:it Z000003,  2:date,  3:stagecode H000007  4: freqinfo F0007日报 F0006寻报
    /// </summary>
    private const String summaryURL = "http://www.ccpn.gov.cn/syncservice/reportmaster/getSummarySecData.action?para=<para><type>1</type><ids>{0}</ids><it>{1}</it><date>{2}</date><freqinfo>{4}</freqinfo><datasource>S000001</datasource><stagecode>{3}</stagecode></para>";

    /// <summary>
    /// 0:it,  1:stagecode,  2:date,  3:id,   4:sitecodes
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public static String GetRemoteContent(String url, params String[] param)
    {
        String URL = String.Format(url, param);

        String xml = Utils.GetWebRequest(URL);

        if (String.IsNullOrEmpty(xml))
        {
            return null;
        }

        return xml;
    }

    #endregion

    public void StartSync(DateTime date)
    {
        if (detailItemParam == null)
        {
            detailItemParam = new String[][]{ 
                new String[] { "Z000003", "H000005","F000007" }, 
                new String[] { "Z000003", "H000005","F000007" }, 
                new String[] { "Z000003", "H000005","F000007" }, 
                new String[] { "Z000003", "H000005","F000007" }, 
                new String[] { "Z000001", "H000004","F000007" }, 
                new String[] { "Z000001", "H000004","F000007" }, 
                new String[] { "Z000001", "H000004","F000007" },

                new String[] { "Z000003", "H000005","F000006" }, 
                new String[] { "Z000003", "H000005","F000006" },
                new String[] { "Z000001", "H000004","F000007" },
            };
        }


        /**
         * 1. 蔬菜
         * <option value="H000007" selected="selected">零售超市</option>
         * <option value="Z000003" selected="selected">零售价</option>
         * 2. 蔬菜
         * <option value="H000006" selected="selected">零售农贸</option>
         * <option value="Z000003" selected="selected">零售价</option>
         * 3. 肉蛋水产
         * <option value="H000007" selected="selected">零售超市</option>
         * <option value="Z000003" selected="selected">零售价</option>
         * 4. 肉蛋水产
         * <option value="H000006" selected="selected">零售农贸</option>
         * <option value="Z000003" selected="selected">零售价</option>
         * 5. 蔬菜
         * <option value="H000004" selected="selected">批发</option>
         * <option value="Z000001" selected="selected">批发价</option>
         * 6. 肉禽蛋水产批发
         * <option value="H000004" selected="selected">批发</option>
         * <option value="Z000001" selected="selected">批发价</option>
         * 7. 粮油批发
         * <option value="H000004" selected="selected">批发</option>
         * <option value="Z000001" selected="selected">批发价</option>
         * 
         * 8. 超市 副食品零售
         * <option value="H000007" selected="selected">零售超市</option>
         * <option value="Z000003" selected="selected">零售价</option>
         * 9. 农贸 副食品零售
         * <option value="H000006" selected="selected">零售农贸</option>
         * <option value="Z000003" selected="selected">零售价</option>
         * 10. 水果批发
         * <option value="H000004" selected="selected">批发</option>
         * <option value="Z000001" selected="selected">批发价</option>             * 
         */

        if (averageItemParam == null)
        {
            averageItemParam = new String[][]{ 
                new String[] { "Z000003", "H000007", "F000007"}, 
                new String[] { "Z000003", "H000006", "F000007"},
                new String[] { "Z000003", "H000007", "F000007"}, 
                new String[] { "Z000003", "H000006", "F000007"},
                new String[] { "Z000001", "H000004", "F000007"}, 
                new String[] { "Z000001", "H000004", "F000007"}, 
                new String[] { "Z000001", "H000004", "F000007"},

                new String[] { "Z000003", "H000007", "F000006"},
                new String[] { "Z000003", "H000006", "F000006"},
                new String[] { "Z000001", "H000004", "F000007"}
            };
        }

        // 获得所有需同步的项目内容，从1~7

        for (int i = 1; i <= detailItemParam.Length ; i++)
        {
            logger.InfoFormat("开始处理{0:yyyy-MM-dd}数据，第{1}类...", date, i);

            StartDetailSync(i, date);
            StartSummarySync(i, date);
        }

        logger.InfoFormat("{0:yyyy-MM-dd}数据处理完成。", date);

        try
        {
            logger.InfoFormat("开始同步到微信平台..");

            OR.DB.SQLHelper.ExecuteSql("EXEC WeiXin.dbo.SyncPriceToWeChat @date", new System.Data.SqlClient.SqlParameter("@date", date.Date));

            logger.InfoFormat("同步到微信平台完成。\r\n================================================");
        }
        catch(Exception e){
            logger.ErrorFormat("同步到微信平台出错：{0}", e);
        }
    }

    protected void StartDetailSync(Int32 index, DateTime date)
    {
        List<model.report.Report_Item> items = OR.Control.DAL.GetModelList<model.report.Report_Item>("PriceType=@type", new System.Data.SqlClient.SqlParameter("@type", index));

        List<model.report.Report_Site> sites = OR.Control.DAL.GetModelList<model.report.Report_Site>("PriceType=@type", new System.Data.SqlClient.SqlParameter("@type", index));

        StringBuilder strSites = new StringBuilder();

        foreach (model.report.Report_Site site in sites)
        {
            if (site.Status == (int)model.Status.Normal)
            {
                strSites.AppendFormat("<sitecode>{0}</sitecode>", site.SiteCode);
            }
        }

        foreach (model.report.Report_Item item in items)
        {
            if (item.Status != (int)model.Status.Normal)
            {
                continue;
            }

            String content = GetRemoteContent(detailURL, detailItemParam[index - 1][0], detailItemParam[index - 1][1], date.ToString("yyyy-MM-dd"), item.ItemCode, strSites.ToString(), detailItemParam[index - 1][2]);

            if (String.IsNullOrEmpty(content))
            {
                continue;
            }

            XmlDocument _xml = new XmlDocument();

            _xml.LoadXml(content);

            XmlNode ret = _xml.SelectSingleNode("/root/ret");

            if (ret.InnerText == "001")
            {
                // 当前没有数据，继续下一个
                continue;
            }

            XmlNodeList _nodes = _xml.SelectNodes("/root/content/items/item");

            if (_nodes == null)
            {
                continue;
            }

            foreach (XmlNode _node in _nodes)
            {
                // 逐个入库

                XmlNode siteNode = _node.SelectSingleNode("sitecode");
                XmlNode pointValueNode = _node.SelectSingleNode("pointvalue");
                XmlNode unitNode = _node.SelectSingleNode("comunitcode");

                if (siteNode == null || pointValueNode == null)
                {
                    // 没有节点，下一个
                    continue;
                }

                if (String.IsNullOrEmpty(pointValueNode.InnerText))
                {
                    // 没有价格
                    continue;
                }

                String siteCode = siteNode.InnerText.Trim();
                Double pointValue = Double.Parse(String.IsNullOrEmpty(pointValueNode.InnerText) ? "0.0" : pointValueNode.InnerText);
                String unitCode = (unitNode == null ? "" : unitNode.InnerText);

                model.report.Report_PriceRecord price = OR.Control.DAL.GetModel<model.report.Report_PriceRecord>("PriceDate=@date AND PriceType=@type AND ItemCode=@item AND SiteCode=@site",
                new System.Data.SqlClient.SqlParameter("@date", date.Date),
                new System.Data.SqlClient.SqlParameter("@type", index),
                new System.Data.SqlClient.SqlParameter("@item", item.ItemCode),
                new System.Data.SqlClient.SqlParameter("@site", siteCode));

                if (price == null)
                {
                    price = new model.report.Report_PriceRecord();

                    price.PriceDate = date.Date;
                    price.PriceType = index;
                    price.ItemCode = item.ItemCode;
                    price.SiteCode = siteCode;
                    price.UnitCode = unitCode;

                    price.Price01 = pointValue;

                    OR.Control.DAL.Add<model.report.Report_PriceRecord>(price, false);
                }
                else
                {
                    price.UnitCode = unitCode;
                    price.Price01 = pointValue;

                    OR.Control.DAL.Update<model.report.Report_PriceRecord>(price);
                }
            }
        }
    }

    protected void StartSummarySync(Int32 index, DateTime date)
    {
        List<model.report.Report_Item> items = OR.Control.DAL.GetModelList<model.report.Report_Item>("PriceType=@type", new System.Data.SqlClient.SqlParameter("@type", index));

        StringBuilder strIds = new StringBuilder();

        foreach (model.report.Report_Item item in items)
        {
            if (item.Status != (int)model.Status.Normal)
            {
                continue;
            }

            strIds.AppendFormat("<id>{0}</id>", item.ItemCode);
        }

        String content = GetRemoteContent(summaryURL, strIds.ToString(), averageItemParam[index - 1][0], date.ToString("yyyy-MM-dd"), averageItemParam[index - 1][1], averageItemParam[index - 1][2]);

        if (String.IsNullOrEmpty(content))
        {
            return;
        }

        XmlDocument _xml = new XmlDocument();

        _xml.LoadXml(content);

        XmlNodeList _nodes = _xml.SelectNodes("/root/content/items/item");

        if (_nodes == null)
        {
            return;
        }

        foreach (XmlNode _node in _nodes)
        {
            // 逐个入库
            XmlNode itemNode = _node.SelectSingleNode("pointcode");
            XmlNode pointValueNode = _node.SelectSingleNode("pointvalue");
            XmlNode unitNode = _node.SelectSingleNode("comunitcode");

            if (String.IsNullOrEmpty(pointValueNode.InnerText))
            {
                // 没有价格
                continue;
            }

            String itemCode = itemNode.InnerText;
            Double pointValue = Double.Parse(String.IsNullOrEmpty(pointValueNode.InnerText) ? "0.0" : pointValueNode.InnerText);
            String unitCode = (unitNode == null ? "" : unitNode.InnerText);

            model.report.Report_PriceAverage price = OR.Control.DAL.GetModel<model.report.Report_PriceAverage>("PriceDate=@date AND PriceType=@type AND ItemCode=@item",
            new System.Data.SqlClient.SqlParameter("@date", date.Date),
            new System.Data.SqlClient.SqlParameter("@type", index),
            new System.Data.SqlClient.SqlParameter("@item", itemCode));

            if (price == null)
            {
                price = new model.report.Report_PriceAverage();

                price.PriceDate = date.Date;
                price.PriceType = index;
                price.ItemCode = itemCode;
                price.UnitCode = unitCode;

                price.PriceAvg = pointValue;

                OR.Control.DAL.Add<model.report.Report_PriceAverage>(price, false);
            }
            else
            {
                price.UnitCode = unitCode;
                price.PriceAvg = pointValue;

                OR.Control.DAL.Update<model.report.Report_PriceAverage>(price);
            }
        }
    }
}
