using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiChatService
{
    public class Constant
    {
        public static int PageSize
        {
            get
            {
                return Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            }
        }
    }
}