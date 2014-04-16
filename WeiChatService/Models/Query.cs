using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeiChatService.Models.pojo;

namespace WeiChatService.Models
{
    public class Query
    {
        /// <summary>
        /// 查询的结果，用户保存内容，等待翻页
        /// </summary>
        public ResultBase result { get; set; }

        /// <summary>
        /// 翻页返回的结果
        /// </summary>
        public ResultBase returnResult { get; set; }

        public List<ResultBase> resultTrace { get; set; }

        private void reset(String sessionId)
        {
            result = new ResultBaseEmpty()
            {
                SessionId = sessionId,
                resultStatus = HttpStatusCode.OK.ToString(),
                resultCode = HttpStatusCode.OK,
                resultType = ResultType.Empty
            };

            resultTrace = new List<ResultBase>();
        }

        public Query(String sessionId)
        {
            reset(sessionId);
        }

        /// <summary>
        /// 按关键词查找Like的内容，相当于重置查询步骤
        /// </summary>
        public void performCategoryLike(String key)
        {
            /**
             * 给定某个关键字时，首先查找分类。如果能查到分类，并且分类为多个，则显示多个分类，由用户根据序号选择下一次显示的内容
             * 
             * 如果找到了多个分类，则将这多个分类全部列出。此处需要进行过滤，不显示子分类，只显示每个分类的最高级
             * 
             * 如果找到了一个分类，则继续找其下一级内容，如果有多个，则列目录，如果有一个，继续循环。
             * 
             * 如果下面没有自分类了，则找条目，按like方式找，找到后，将其价格返回。返回价格时，需要判断该条目的价格显示类型。
             * 
             * 如果有多个条目符合，如何显示？
             * 
             * 如果有一个条目符合，则按价格类型的模版形式进行返回不同的内容格式
             * 
             */

            // 当前已经是作为一个新的关键词了，将之前的trace信息删除即可了。
            reset(result.SessionId);

            WeiChatService.CacheClient.Client().Remove(this.result.SessionId.ToLower());

            // 首先检查关键字的合法性：如果关键词长度小于等于2位，则不能全为数字，不能为单个字母

            if (key.Length < 3)
            {

                Double nKey = 0.0;
                if (Double.TryParse(key, out nKey))
                {
                    result.resultType = ResultType.Invalid;
                    // 返回之前清除缓存里的东西
                    WeiChatService.CacheClient.Client().Remove (this.result.SessionId.ToLower());
                    return;
                }

                String m = "abcdefghijklmnopqrstuvwxyz0123456789,./<>?;':\"[]{}~!@#$%^&*()_+-=";

                int mCount = 0;

                for (int i = 0; i < key.Length; i++)
                {
                    if (m.IndexOf(key.Substring(i, 1).ToLower()) >= 0)
                    {
                        mCount++;
                    }
                }

                if (mCount == key.Length)
                {
                    result.resultType = ResultType.Invalid;
                    return;
                }
            }

            List<model.weichat.Dict_Category> categories = OR.Control.DAL.GetModelList<model.weichat.Dict_Category>("CategoryName Like @key AND Exclude=0 Order By OrderID",
                new System.Data.SqlClient.SqlParameter("@key", "%" + key.Trim() + "%"));

            if (categories.Count > 1)
            {
                QueryCategory.fullFillCategories(this, categories);

                WeiChatService.CacheClient.Client().Insert(this.result.SessionId.ToLower(), this, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else if (categories.Count == 1)
            {
                // 查找下一级的条目内容

                QueryCategory cQuery = new QueryCategory();
                cQuery.query(this, categories[0]);
            }
            else if (categories.Count == 0)
            {
                //like条目去
                performItemLike(key);
            }
        }

        /// <summary>
        /// 查找分类二次查询下一步内容
        /// </summary>
        /// <param name="stepNO"></param>
        public void performCategoryStepNO(Int32 stepNO)
        {
            int offset = 1;

            List<model.weichat.Dict_Category> subCategory = OR.Control.DAL.GetModelList<model.weichat.Dict_Category>("PID=@pid  Order By OrderID",
                        new System.Data.SqlClient.SqlParameter("@pid", ((CategoryCollections)result).category[stepNO - offset].getID()));

            if (subCategory.Count > 1)
            {
                // 如果有多个，返回列表继续找
                QueryCategory.fullFillCategories(this, subCategory);

                // 加入缓存，等待下次处理
                WeiChatService.CacheClient.Client().Insert(this.result.SessionId.ToLower(), this, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else if (subCategory.Count == 1)
            {

                if (subCategory[0].Exclude == 0)
                {
                    // 如果只有一个下级，并且是正常分类，继续走。剂型分类走下一个分支
                    // 如果找到一个，继续向下找下一级，知道找到最后一级，如果是最后一级，则开始查询条目了
                    QueryCategory cQuery = new QueryCategory();
                    cQuery.query(this, subCategory[0]);
                }
                else if(subCategory[0].Exclude == 1)
                {
                    // 该分类是剂型，直接找下一级条目价格
                    performMedinceCategoryItem(subCategory[0].CategoryID);
                }
            }
            else
            {
                int exclude = ((CategoryCollections)result).category[stepNO - offset].exclude;

                if (exclude == 0)
                {
                    // 正常的价格分类条目
                    // 查找下面的菜价内容，
                    performCategoryItem(((CategoryCollections)result).category[stepNO - offset].getID());
                }
                else if (exclude == 1)
                {
                    // 当前分类是医药价格下的剂型节点，直接查找下面所有的价格条目
                    performMedinceCategoryItem(((CategoryCollections)result).category[stepNO - offset].getID());
                }
            }
        }

        public void performCategoryItem(int categoryID)
        {
            List<model.weichat.Dict_ItemCode> items = OR.Control.DAL.GetModelList<model.weichat.Dict_ItemCode>("CategoryID=@pid Order By OrderID",
                    new System.Data.SqlClient.SqlParameter("@pid", categoryID));

            if (items.Count > 1)
            {
                QueryItem.fullFillItems(this, items);

                // 加入缓存，等待下次处理
                WeiChatService.CacheClient.Client().Insert(this.result.SessionId.ToLower(), this, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else if (items.Count == 1)
            {
                QueryItem.showItemPrice(this, items[0].ItemID);
            }
            else
            {
                // 没有找到内容
                this.result = new ResultBaseEmpty();
            }
        }

        /// <summary>
        /// 查找某剂型下的药品价格明细
        /// </summary>
        /// <param name="categoryID"></param>
        public void performMedinceCategoryItem(int categoryID)
        {
            // 首先制定下面结果的类型，然后逐项向上附加价格条目
            pojo.PriceMedinceResult r = new PriceMedinceResult();
            r.CloneSession(this.result);
            r.resultType = ResultType.Medince;

            this.result = r;

            List<model.weichat.Dict_ItemCode> items = OR.Control.DAL.GetModelList<model.weichat.Dict_ItemCode>("CategoryID=@pid Order By OrderID",
                    new System.Data.SqlClient.SqlParameter("@pid", categoryID));

            foreach (model.weichat.Dict_ItemCode item in items)
            {
                QueryItem.showItemPrice(this, item.ItemID);
            }

            // 因为该分类下可能存在分页的问题，将其保存下来
            this.result.ResetPage();

            WeiChatService.CacheClient.Client().Insert(this.result.SessionId.ToLower(), this, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public void performCategoryRoot(int parentID)
        {
            List<model.weichat.Dict_Category> subCategory = OR.Control.DAL.GetModelList<model.weichat.Dict_Category>("PID=@pid Order By OrderID",
                        new System.Data.SqlClient.SqlParameter("@pid", parentID));

            if (subCategory.Count > 1)
            {
                // 如果有多个，返回列表继续找
                QueryCategory.fullFillCategories(this, subCategory);

                // 加入缓存，等待下次处理
                WeiChatService.CacheClient.Client().Insert(this.result.SessionId.ToLower(), this, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else if (subCategory.Count == 1)
            {
                // 如果找到一个，继续向下找下一级，知道找到最后一级，如果是最后一级，则开始查询条目了
                QueryCategory cQuery = new QueryCategory();
                cQuery.query(this, subCategory[0]);
            }
            else
            {
                // 查找下面的菜价内容，
                performCategoryItem(parentID);
            }
        }

        public void performItemLike(String key)
        {
            List<model.weichat.Dict_ItemCode> items = OR.Control.DAL.GetModelList<model.weichat.Dict_ItemCode>("ItemName Like @key  Order By OrderID",
                    new System.Data.SqlClient.SqlParameter("@key", "%" + key.Trim() + "%"));

            if (items.Count > 1)
            {
                // 该处是否显示列表，依赖于我们是否会提供一次展示多个产品。当前没有展示
                QueryItem.fullFillItems(this, items);

                // 加入缓存，等待下次处理
                WeiChatService.CacheClient.Client().Insert(this.result.SessionId.ToLower(), this, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else if (items.Count == 1)
            {
                QueryItem.showItemPrice(this, items[0].ItemID);
            }
            else
            {
                // 没有找到内容
                this.result = new ResultBaseEmpty();
            }
        }

        // 查找条目二次查询的下一步
        public void performItemStepNO(Int32 stepNO)
        {
            int offset = 1;

            // 输出指定标号的菜
            QueryItem.showItemPrice(this, ((ItemCollections)result).items[stepNO - offset].getID());
        }
    }
}