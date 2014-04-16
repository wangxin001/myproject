using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;

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


    public static string GetWebRequest(String getUrl)
    {
        string ret = string.Empty;

        try
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(getUrl));
            webReq.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            ret = sr.ReadToEnd();

            sr.Close();
            response.Close();
        }
        catch (Exception)
        {
            ret = String.Empty;
        }

        return ret;
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
    /// 获取文件保存路径。此路径由两部分组成：保存基路径，该路径在配置文件里配置；时间戳子文件夹。
    /// 系统在应用时，建议再增加BoardID作为中间的部分，每个栏目分开进行存储管理。
    /// 每个文件保存地址为： 基路径+BoardID+时间戳文件夹+文件名GUID
    /// </summary>
    /// <returns></returns>
    public static String[] GetUploadFileSavePath()
    {
        String strPath = System.Web.HttpContext.Current.Server.MapPath("~/upload/");

        String strFolder = DateTime.Now.Year + "\\" + DateTime.Now.ToString("MMdd");

        return new String[] { strPath, strFolder };
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
    /// 连接文件夹名和文件名，主要处理两个名字之间有“\”的问题
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static String GetFileNameAppend(String folder, String filename)
    {
        return folder + (folder.EndsWith("\\") ? "" : "\\") + filename;
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

    /// <summary>
    /// 对文件夹路径进行处理的类，进行两个文件名部分的合并操作
    /// </summary>
    public class FilePathInfo
    {
        String _strPath = string.Empty;

        public FilePathInfo(String strFolderPath)
        {
            _strPath = strFolderPath;
        }

        /// <summary>
        /// 得到文件名处理对象
        /// </summary>
        /// <param name="strFolderPath"></param>
        /// <returns></returns>
        public static FilePathInfo GetPathInfo(String strFolderPath)
        {
            return new FilePathInfo(strFolderPath);
        }

        /// <summary>
        /// 连接文件夹名和文件名，主要处理两个名字之间有“\”的问题
        /// </summary>
        /// <param name="strFolderPath"></param>
        /// <returns></returns>
        public FilePathInfo Append(String strFolderPath)
        {

            _strPath = _strPath + (_strPath.EndsWith("\\") ? "" : "\\") + strFolderPath;

            return this;
        }

        override public String ToString()
        {
            return _strPath;
        }
    }

}

public class EnumUtil
{
    /// <summary>
    /// 获取枚举变量值的 Description 属性
    /// </summary>
    /// <param name="obj">枚举变量</param>
    /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>
    public static string GetDescription(object obj)
    {
        return GetDescription(obj, false);
    }

    /// <summary>
    /// 获取枚举变量值的 Description 属性
    /// </summary>
    /// <param name="obj">枚举变量</param>
    /// <param name="isTop">是否改变为返回该类、枚举类型的头 Description 属性，而不是当前的属性或枚举变量值的 Description 属性</param>
    /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>
    public static string GetDescription(object obj, bool isTop)
    {
        if (obj == null)
        {
            return string.Empty;
        }
        try
        {
            Type _enumType = obj.GetType();
            DescriptionAttribute dna = null;
            if (isTop)
            {
                dna = (DescriptionAttribute)Attribute.GetCustomAttribute(_enumType, typeof(DescriptionAttribute));
            }
            else
            {
                FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, obj));
                dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                   fi, typeof(DescriptionAttribute));
            }
            if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                return dna.Description;
        }
        catch
        {
        }
        return obj.ToString();
    }
}

