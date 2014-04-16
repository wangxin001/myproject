using System;
using System.IO;
using System.Net;
using System.Text;

public class Utils
{
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

