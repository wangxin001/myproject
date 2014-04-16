using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Xml;

/// <summary>
///SyncService 的摘要说明
/// </summary>
public class SyncService
{

    static String strRemoteServer = System.Configuration.ConfigurationManager.AppSettings["RemoteService"];

    public enum RemoveService
    {
        /// <summary>
        /// 需要传入的参数：ID
        /// </summary>
        [Description("/getKeyValue.action?para=<para><page>1</page><pagesize>1000</pagesize><id>{0}</id></para>")]
        DictValue,
        /// <summary>
        /// 当前的节点ID，参数：PID。获取第一级目录固定为 E000000
        /// </summary>
        [Description("/getTreeValue.action?para=<para><page>1</page><pagesize>1000</pagesize><id>1</id><pid>{0}</pid></para>")]
        Node,
        /// <summary>
        /// 获取指定品类下的业务指标对象列表，参数：PID
        /// </summary>
        [Description("/getTreeValue.action?para=<para><page>1</page><pagesize>1000</pagesize><id>2</id><pid>{0}</pid></para>")]
        Items,
        /// <summary>
        /// 获取指定品类下的业务品种列表，参数：PID
        /// </summary>
        [Description("/getTreeValue.action?para=<para><page>1</page><pagesize>1000</pagesize><id>3</id><pid>{0}</pid></para>")]
        Catas,
        /// <summary>
        /// 同步制定指标的价格. 参数：指标业务对象ID, 指标编码, 日期, 频率, 数据来源, 环节
        /// </summary>
        [Description("/getSummarySecData.action?para=<para><type>1</type><ids><id>{0}</id></ids><it>{1}</it><date>{2}</date><freqinfo>{3}</freqinfo><datasource>{4}</datasource><stagecode>{5}</stagecode></para>")]
        Price
    }

    public static XmlDocument GetRemoteContent(RemoveService remoteService, params String[] param)
    {

        String url = String.Format(strRemoteServer + EnumUtil.GetDescription(remoteService), param);

        String xml = Util.GetWebRequest(url);

        if (String.IsNullOrEmpty(xml))
        {
            return null;
        }

        XmlDocument _doc = new XmlDocument();

        _doc.LoadXml(xml);

        return _doc;
    }



    /// <summary>
    /// 按制定的指标读取数据。默认指定读 北京市价格监测中心 日报
    /// </summary>
    /// <param name="itemCode">条目编号</param>
    /// <param name="date">日期</param>
    /// <param name="target">指标编码</param>
    /// <param name="stage">环节</param>
    /// <param name="ret">结果编码，000:成功，006:无数据，005:参数错误，004:IP无授权</param>
    /// <param name="readUnit">是否要读取指标单位字段编码</param>
    /// <param name="strUnit">单位编码</param>
    /// <returns></returns>
    public static String GetPointValue(String itemCode, String date, String target, String stage, out String ret, Boolean readUnit, out String strUnit)
    {
        /*
        * 各参数所需内容：
        *      指标： Z000001 批发价
        *             Z000002 成交量
        *             Z000003 零售价
        *      频率： F000007 日报
        *      来源： S000001 北京市价格监测中心
        *      环节： H000004 批发  
        *             H000005 零售
        *             H000006 零售农贸
        *             H000007 零售超市
        * 
        * 批发成交量
        * 批发批发价
        * 
        * 
        * 
        * 每个条目需要拿四个数据：成交量，批发价，农贸零售价，超市零售价
        * 
         * 默认指定读 北京市价格监测中心 日报
        */

        XmlDocument _doc = GetRemoteContent(SyncService.RemoveService.Price,
           itemCode, target, date, "F000007", "S000001", stage);

        ret = _doc.SelectSingleNode("root/ret").InnerText;

        if (ret.Equals("000"))
        {
            XmlNode nodePrice = _doc.SelectSingleNode("root/content/items/item/pointvalue");

            strUnit = String.Empty;

            if (readUnit)
            {
                XmlNode nodeUnit = _doc.SelectSingleNode("root/content/items/item/comunitcode");

                if (nodeUnit != null)
                {
                    model.dict.Dict_RemoteDictValue unitDictValue = OR.Control.DAL.GetModelByKey<model.dict.Dict_RemoteDictValue>(nodeUnit.InnerText.Trim());

                    if (unitDictValue != null)
                    {
                        strUnit = unitDictValue.Name;
                    }
                }
            }

            return nodePrice.InnerText; ;
        }
        else
        {
            strUnit = String.Empty;
            return String.Empty;
        }
    }

}
