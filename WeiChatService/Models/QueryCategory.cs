using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiChatService.Models.pojo;
using model.weichat;

namespace WeiChatService.Models
{
    public class QueryCategory
    {
        public void query(Query container, model.weichat.Dict_Category category)
        {

            List<model.weichat.Dict_Category> subCategories = OR.Control.DAL.GetModelList<model.weichat.Dict_Category>("PID=@pid Order By OrderID",
                new System.Data.SqlClient.SqlParameter("@pid", category.CategoryID));

            if (subCategories.Count > 1)
            {
                fullFillCategories(container, subCategories);

                WeiChatService.CacheClient.Client().Insert(container.result.SessionId.ToLower(), container, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else if (subCategories.Count == 1)
            {
                // 查找下一级的条目内容
                // TODO: 
                query(container, subCategories[0]);
            }
            else if (subCategories.Count == 0)
            {
                //like条目去
                container.performCategoryItem(category.CategoryID);
            }
        }

        /// <summary>
        /// 如果当前查找结果是多个目录，将其填充到结果中
        /// </summary>
        /// <param name="categories"></param>
        public static void fullFillCategories(Query container, List<model.weichat.Dict_Category> categories)
        {

            FilterSubCategory(ref categories);

            CategoryCollections c = new CategoryCollections();

            c.CloneSession(container.result);
            c.resultType = ResultType.Categories;

            int i = 1;

            foreach (model.weichat.Dict_Category item in categories)
            {
                c.category.Add(new pojo.Category(item.CategoryID) { seqNO = i++, name = item.CategoryName, exclude = item.Exclude });
            }

            container.result = c;
            container.resultTrace.Add(c);

            container.result.ResetPage();

        }

        public static void FilterSubCategory(ref List<model.weichat.Dict_Category> categories)
        {

            // 该变量是分类的全集，记录了每个变量的顺序和关系
            Dictionary<int, model.weichat.Dict_Category> categoriesCollections = new Dictionary<int,model.weichat.Dict_Category>();

            List<model.weichat.Dict_Category> fullCategories = OR.Control.DAL.GetModelList<model.weichat.Dict_Category>("");
            foreach (model.weichat.Dict_Category c in fullCategories)
            {
                categoriesCollections.Add(c.CategoryID, c);
            }

            // 传入的结果集，转为MAP
            Dictionary<int, model.weichat.Dict_Category> resultCollections = new Dictionary<int,model.weichat.Dict_Category>();
            foreach (model.weichat.Dict_Category c in categories)
            {
                resultCollections.Add(c.CategoryID, c);
            }

            /// 逐个内容做过滤，然后将保留下来的放在新的list里返回
            List<model.weichat.Dict_Category> newResult = new List<model.weichat.Dict_Category>();

            foreach (model.weichat.Dict_Category curr in categories)
            {
                if (matchParent(curr, resultCollections, categoriesCollections))
                {
                    newResult.Add(curr);
                }
            }

            categories = newResult;
        }

        /// <summary>
        /// 此方法来判断该分类的父节点是否在列表中
        /// 如果父节点在里面，返回false，不保留
        /// 如果父节点不在里面，返回true，要保留
        /// </summary>
        /// <param name="currCategory"></param>
        /// <param name="result"></param>
        /// <param name="fullCollection"></param>
        /// <returns></returns>
        private static bool matchParent(model.weichat.Dict_Category currCategory, Dictionary<int, model.weichat.Dict_Category> result, Dictionary<int, model.weichat.Dict_Category> fullCollection)
        {

            if (currCategory.PID == 0)
            {
                return true;
            }

            model.weichat.Dict_Category p = fullCollection[currCategory.PID];

            if (p == null)
            {
                return true;
            }

            if (result.ContainsKey(p.CategoryID))
            {
                // 结果集中找到了父节点
                return false;
            }

            return matchParent(p, result, fullCollection);
        }
    }
}