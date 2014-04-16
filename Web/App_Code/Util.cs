using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Xml;

/// <summary>
///Util 的摘要说明
/// </summary>
public class Util
{
    public Util()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 根据路径获取文件的缩略图路径
    ///   比如：14\2010\1225\5ba5015e-4d1b-4e5c-8334-0484955ad591.JPG
    /// 转换后：14\2010\1225\thumb_5ba5015e-4d1b-4e5c-8334-0484955ad591.JPG
    /// </summary>
    /// <param name="strURL"></param>
    /// <returns></returns>
    public static String GetThumbImageURL(String strURL)
    {
        strURL = strURL.Replace("\\", "/");

        String strFileName = strURL.Substring(strURL.LastIndexOf("/") + 1);
        String strFileNameNoExt = strFileName.Substring(0, strFileName.LastIndexOf("."));

        return strURL.Substring(0, strURL.LastIndexOf("/") + 1) + "thumb_" + strFileNameNoExt + ".jpg";
    }

    /// <summary>
    /// 根据指定的编号获取新闻内容
    /// </summary>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public static DataRow GetTopicInfo(int topicID)
    {
        String strSQL = "Select Top 1 TopicID,TopicTitle,Publisher,PublishDate,TopicContent From InfoTopicInfo";
        strSQL += "  where TopicID=@TopicID";

        DataTable dt = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@TopicID", topicID)).Tables[0];

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        else
        {
            return null;
        }
    }


    #region 价格查询左侧菜单的处理

    private static XmlDocument _xml = null;

    /// <summary>
    /// 获得xml的单例实例
    /// </summary>
    /// <returns></returns>
    private static XmlDocument GetXMLDocument()
    {
        if (_xml == null)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(HttpContext.Current.Server.MapPath("~/App_Data/price.xml"));

            _xml = xml;
        }
        return _xml;
    }

    private static String _strMenuContent = null;
    /// <summary>
    /// 根据XML生成价格策略栏目左侧菜单树
    /// </summary>
    /// <returns></returns>
    public static String GetPriceMenu()
    {
        #region 判断状态，从内存里面读缓存，或者第一次读xml

        if (String.IsNullOrEmpty(_strMenuContent))
        {
            StringBuilder strMenu = new StringBuilder();

            XmlDocument xml = GetXMLDocument();

            XmlNodeList submenus = xml.GetElementsByTagName("submenu");

            // menu

            for (int i = 0; i < submenus.Count; i++)
            {
                XmlNode submenu = submenus[i];

                // submenu
                if (i == 0)
                {
                    strMenu.Append("<span class='cbx_ti'><strong>");
                }
                else
                {
                    strMenu.Append("<span class='sbx_ti'><strong>");
                }

                strMenu.Append(submenu.Attributes["name"].Value + "</strong> </span><span class='cbx_tc'>");

                XmlNodeList lines = submenu.ChildNodes;

                foreach (XmlNode line in lines)
                {
                    // line
                    strMenu.Append("<ul>");

                    for (int j = 0; j < line.ChildNodes.Count; j++)
                    {
                        // menu
                        XmlNode li = line.ChildNodes[j];

                        String type = li.Attributes["type"].Value;

                        // topic，文章，直接链接到文章内容
                        if (type.Equals("topic"))
                        {
                            strMenu.Append("<li><a href='./price_" + li.Attributes["id"].Value + "_" + i.ToString() + ".aspx'>" + li.InnerText + "</a> </li>");
                        }
                        else
                        {
                            // 读取column属性，获得每个页面有几列信息显示
                            String strColumns = (li.Attributes["column"] == null ? "1" : li.Attributes["column"].Value);

                            if (strColumns.Equals("2"))
                            {
                                strMenu.Append("<li><a href='./index2_" + li.Attributes["id"].Value + "_" + i.ToString() + ".aspx'>" + li.InnerText + "</a> </li>");
                            }
                            else
                            {
                                strMenu.Append("<li><a href='./index_" + li.Attributes["id"].Value + "_" + i.ToString() + ".aspx'>" + li.InnerText + "</a> </li>");
                            }
                        }

                        if (j != line.ChildNodes.Count - 1)
                        {
                            strMenu.Append("<li><img src='../images/bjj.gif' /></li>");
                        }
                    }

                    strMenu.Append("</ul>");
                }
                strMenu.Append("</span><div class='clear'></div>");
            }

            _strMenuContent = strMenu.ToString();
        }
        #endregion

        return _strMenuContent;
    }

    
    /// <summary>
    /// 内存缓存，不读取XML，能快点
    /// </summary>
    private static Dictionary<int, String> _menuName = new Dictionary<int, string>();

    /// <summary>
    /// 根据ID号，获得当前分类的名字
    /// </summary>
    /// <param name="menuID"></param>
    /// <returns></returns>
    public static String GetPriceMenuName(int menuID)
    {
        String strMenuName = string.Empty;

        if (!_menuName.ContainsKey(menuID))
        {
            XmlDocument xml = GetXMLDocument();
            XmlNodeList submenus = xml.GetElementsByTagName("submenu");
            try
            {
                _menuName.Add(menuID, submenus[menuID].Attributes["name"].Value);
            }
            catch (Exception)
            {
                //
            }
        }

        return _menuName[menuID];
    }

    #endregion


    public static String GetContent(String strContent)
    {
        return GetContent(strContent, -1);
    }

    public static String GetContent(String strContent, int len)
    {
        int pos1 = 0;
        int pos2 = 0;

        pos1 = strContent.IndexOf("<");

        while (pos1 != -1)
        {
            pos2 = strContent.IndexOf(">", pos1);
            strContent = strContent.Remove(pos1, pos2 - pos1 + 1);
            pos1 = strContent.IndexOf("<", pos1);
        }

        if (len <= 0)
        {
            return strContent;
        }
        else
        {
            return strContent.Length > len ? strContent.Substring(0, len) + "..." : strContent;
        }
    }



    /// <summary>
    /// 获取当前登陆用户信息
    /// </summary>
    /// <returns></returns>
    public static model.UserInfo GetLoginUserInfo()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            return Util.SerializeObj.Desrialize<model.UserInfo>(HttpContext.Current.User.Identity.Name);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 转换到MD5
    /// </summary>
    /// <param name="str">加密前字符串</param>
    /// <param name="code">16位加密还是32位加密</param>
    /// <returns>加密后字符串</returns>
    public static string to_md5(string str, int code)
    {
        if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
        }
        if (code == 32) //32位加密 
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }
        return "00000000000000000000000000000000";
    }

    /// <summary>
    /// 进行实体类的序列化操作，主要用户是记录当前登陆用户信息
    /// </summary>
    public class SerializeObj
    {
        public SerializeObj()
        { }

        /// <summary>
        /// 序列化 对象到字符串
        /// </summary>
        /// <param name="obj">泛型对象</param>
        /// <returns>序列化后的字符串</returns>
        public static string Serialize<T>(T obj)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
                return Convert.ToBase64String(buffer);
            }
            catch (Exception ex)
            {
                throw new Exception("序列化失败,原因:" + ex.Message);
            }
        }

        /// <summary>
        /// 反序列化 字符串到对象
        /// </summary>
        /// <param name="obj">泛型对象</param>
        /// <param name="str">要转换为对象的字符串</param>
        /// <returns>反序列化出来的对象</returns>
        public static T Desrialize<T>(string str) where T : OR.Model.Entity, new()
        {
            T obj = new T();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                byte[] buffer = Convert.FromBase64String(str);
                MemoryStream stream = new MemoryStream(buffer);
                obj = (T)formatter.Deserialize(stream);
                stream.Flush();
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("反序列化失败,原因:" + ex.Message);
            }
            return obj;
        }
    }
}