using System;
using System.Text.RegularExpressions;
using System.Web;

namespace HttpUrlRewrite.InSync
{
    /// <summary>
    ///ForwardRequrestAgent 的摘要说明
    /// </summary>
    public class ForwardRequrestAgent : IHttpHandler
    {
        public ForwardRequrestAgent()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public void ProcessRequest(HttpContext Context)
        {
            string Url = Context.Request.RawUrl; //.ToLower();

            Regex reg = new Regex(@"/reportmaster/(.*)", RegexOptions.IgnoreCase);
            
            Match m = reg.Match(Url);

            if (m.Success)
            {
                String remoteURL = System.Configuration.ConfigurationManager.AppSettings["ReportmasterHost"] + m.Groups[1].Value;

                String strRet = ActionUtils.GetWebRequest(remoteURL);

                Context.Response.ContentType = "text/xml";
                Context.Response.Write(strRet);
            }
            else
            {
                Context.Response.Write("URL Invalid");
            }

            Context.Response.End();
        }

        public bool IsReusable { get { return false; } }
    }
}