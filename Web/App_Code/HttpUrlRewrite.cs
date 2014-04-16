using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

namespace HttpUrlRewrite
{

    /// <summary>
    /// 新闻内容URL的处理
    /// </summary>
    public class DetailUrlRewrite : IHttpHandler
    {
        log4net.ILog log = log4net.LogManager.GetLogger("DetailUrlRewrite"); 

        public DetailUrlRewrite()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public void ProcessRequest(HttpContext Context)
        {

            string Url = Context.Request.RawUrl.ToLower();

            Regex Reg = new Regex(@".*/(.*)/detail_(\d+)\.aspx", RegexOptions.IgnoreCase);

            Match m = Reg.Match(Url);

            if (m.Success)
            {
                Context.Server.Execute(@"~/" + m.Groups[1] + "/detail.aspx?TopicID=" + m.Groups[2]);
            }
            else
            {
                Context.Response.Write("500 ERROR！");
            }
        }

        public bool IsReusable { get { return false; } }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DefaultUrlRewrite : IHttpHandler
    {
        public DefaultUrlRewrite()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public void ProcessRequest(HttpContext Context)
        {
            string Url = Context.Request.RawUrl.ToLower();

            Regex Reg = new Regex(@".*/(.*)/default_(\d+)\.aspx", RegexOptions.IgnoreCase);

            Match m = Reg.Match(Url);

            if (m.Success)
            {
                Context.Server.Execute(@"~/" + m.Groups[1] + "/default.aspx?TopicID=" + m.Groups[2]);
            }
            else
            {
                Context.Response.Write("404 ERROR！");
            }
        }

        public bool IsReusable { get { return false; } }
    }

    /// <summary>
    /// 新闻内容URL的处理
    /// </summary>
    public class VegetablesUrlRewrite : IHttpHandler
    {
        public VegetablesUrlRewrite()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public void ProcessRequest(HttpContext Context)
        {
            string Url = Context.Request.RawUrl.ToLower();

            Regex Reg = new Regex(@".*/vegetables/index_(\d+)\.aspx", RegexOptions.IgnoreCase);

            Match m = Reg.Match(Url);

            if (m.Success)
            {
                Context.Server.Execute(@"~/vegetables/index.aspx?t=" + m.Groups[1]);
            }
            else
            {
                Context.Response.Write("404 ERROR！");
            }
        }

        public bool IsReusable { get { return false; } }
    }

    /// <summary>
    /// 新闻内容URL的处理
    /// </summary>
    public class PriceUrlRewrite : IHttpHandler
    {
        public PriceUrlRewrite()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //

            /*
             * 该部分处理比较复杂。
             * 传入的参数形式有以下几种：  
             * 
             *     /price/price.aspx
             *     /price/price_18.aspx
             *     /price/index_15.aspx
             *     /price/index2_16.aspx
             *     
             * 对其处理规则如下：
             *     
             *     1、先判断，是否就等于 price.aspx，如果是，直接重定向过去
             *     2、是否匹配 price_(\d) 格式，如果是，转格式重定向
             *     3、index_1.aspx：转格式重定向
             *     5、其他格式的：404
             */
        }

        public void ProcessRequest(HttpContext Context)
        {
            string Url = Context.Request.RawUrl.ToLower();

            #region 处理第一类情况，加编号的最终页
            {
                Regex Reg = new Regex(@".*/price/price_(\d+)\.aspx", RegexOptions.IgnoreCase);
                Match m = Reg.Match(Url);

                if (m.Success)
                {
                    Context.Server.Execute(@"~/price/price.aspx?TopicID=" + m.Groups[1]);
                    return;
                }
            }
            #endregion

            #region 处理第二类情况，每页一列内容
            {
                Regex Reg = new Regex(@".*/price/index_(\d+)\.aspx", RegexOptions.IgnoreCase);

                Match m = Reg.Match(Url);

                if (m.Success)
                {
                    Context.Server.Execute(@"~/price/index.aspx?BoardID=" + m.Groups[1]);
                    return;
                }
            }
            #endregion

            Context.Response.StatusCode = 404;
            Context.Response.End();
        }

        public bool IsReusable { get { return false; } }
    }
}