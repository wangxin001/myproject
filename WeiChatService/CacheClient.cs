using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace WeiChatService
{
    public class CacheClient
    {
        public static System.Web.Caching.Cache Client()
        {
            return HttpRuntime.Cache;
        }
    }
}