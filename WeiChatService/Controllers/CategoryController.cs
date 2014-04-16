using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeiChatService.Models;
using WeiChatService.Models.pojo;

namespace WeiChatService.Controllers
{
    public class CategoryController : ApiController
    {
        public ResultBase Get(String key, String sessionId)
        {
            DateTime queryTime = DateTime.Now;

            object ret = WeiChatService.CacheClient.Client().Get(sessionId.ToLower());

            if (ret != null)
            {
                WeiChatService.CacheClient.Client().Remove(sessionId.ToLower());
            }

            Query query = new Query(sessionId);

            int no = 0;

            if (!Int32.TryParse(key, out  no))
            {
                query.result.resultType = ResultType.Invalid;
            }
            else
            {
                query.performCategoryRoot(Int32.Parse(key));
            }

            model.weichat.QueryLog.save(sessionId, "Category", key, queryTime, DateTime.Now.Ticks - queryTime.Ticks);

            return query.result;
        }
    }
}
