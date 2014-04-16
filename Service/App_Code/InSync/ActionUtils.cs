using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace HttpUrlRewrite.InSync
{
    /// <summary>
    ///HttpPostUtil 的摘要说明
    /// </summary>
    public class ActionUtils
    {
        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;

            byte[] byteArray = dataEncode.GetBytes(paramData);

            return PostWebRequest(postUrl, byteArray);
        }

        public static string PostWebRequest(string postUrl, byte[] byteArray)
        {
            string ret = string.Empty;

            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;

                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();

                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception)
            {
                ret = String.Empty;
            }

            return ret;
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
    }
}