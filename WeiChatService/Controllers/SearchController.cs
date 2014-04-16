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
    public class SearchController : ApiController
    {
        private String[] pagerChar = new String[] { "f", "p", "n", "l" };

        private const String returnString = "返回上一级目录";

        public ResultBase Get(String key, String sessionId)
        {
            DateTime queryTime = DateTime.Now;

            // 此处可能为二次查询，按内容再来进行进一步判断

            object ret = WeiChatService.CacheClient.Client().Get(sessionId.ToLower());

            /*
             * 缓存里没有数据
             * 缓存里数据类型不对
             * 缓存里的sessionid与当前不一致: 该情况不存在，因为是已sessionID为Key取数据的
             */
            if (ret == null || !(ret is Query))
            {
                #region 第一次查询的内容

                // 此时为首次查询，当作关键词来弄
                // 可以加一个判断，不允许在此情况下输入数字
                Query query = new Query(sessionId);
                //开始查找数据
                query.performCategoryLike(key);

                // 缓存不能在这里加，只能在有分类的时候才能加。这里不需要。去掉之~~

                ProcessPager(query);

                model.weichat.QueryLog.save(sessionId, "Search", key, queryTime, DateTime.Now.Ticks - queryTime.Ticks);

                return query.returnResult;

                #endregion
            }
            else
            {
                // 开始进行查询操作，进一步进行判断
                Query query = ret as Query;

                // NO 二次查询序号
                int no = -1;

                // 如果当前是选择的0， 则返回上一级
                if (Int32.TryParse(key, out  no) && no == 0)
                {
                    #region 0 返回上一级

                    bool hasPreviousStep = false;

                    // 如果缓存里的数量大于2， 则可以进行返回，减少为1.否则输入0 为非法
                    if (query.resultTrace.Count > 0)
                    {
                        query.result = query.resultTrace[query.resultTrace.Count - 1];

                        if (query.resultTrace.Count > 1)
                        {
                            hasPreviousStep = true;
                        }
                    }

                    // 这时候敲的是0， 应该是走上一级的方式
                    if (hasPreviousStep)
                    {
                        // 如果有上一级，则进行返回
                        query.resultTrace.RemoveAt(query.resultTrace.Count - 1);
                        query.result = query.resultTrace[query.resultTrace.Count - 1];
                        query.result.ResetPage();
                    }
                    else
                    {
                        // 没有上一级，又敲了0， 则按关键词查询
                        query = new Query(sessionId);
                        query.performCategoryLike(key);
                    }

                    #endregion
                }
                else
                {
                    if (query.result is CategoryCollections)
                    {
                        #region 当前最后一次查询是分类列表

                        // 如果有上一级，则这里需要在总数量上减掉多出来的两个2，否则本身就是第一级，不需要减
                        if (Int32.TryParse(key, out  no) && (no >= 1 && no <= Constant.PageSize))
                        {
                            no = (query.result.PageIndex - 1) * Constant.PageSize + no;

                            // 二次查询输入的数字
                            // 范围正常，继续查询
                            query.performCategoryStepNO(no);
                        }
                        else if (pagerChar.Contains(key.ToLower()))
                        {
                            /**
                             * 分页的规则：
                             * 
                             * f: 第一页
                             * p: 上一页
                             * n: 下一页
                             * l: 最后一页
                             * gX: 跳到X页
                             * 
                             */

                            // 上下翻页
                            if (key.ToLower().Equals("f"))
                            {
                                query.result.PageIndex = 1;
                            }
                            else if (key.ToLower().Equals("l"))
                            {
                                query.result.PageIndex = query.result.PageCount;
                            }
                            else if (key.ToLower().Equals("p"))
                            {
                                query.result.PageIndex = query.result.PageIndex > 1 ? query.result.PageIndex - 1 : 1;
                            }
                            else if (key.ToLower().Equals("n"))
                            {
                                query.result.PageIndex = query.result.PageIndex < query.result.PageCount ? query.result.PageIndex + 1 : query.result.PageCount;
                            }
                        }
                        else if ((key.ToLower().Trim().StartsWith("g") && Int32.TryParse(key.Trim().Substring(1), out no)))
                        {
                            // 输入 gX 跳到第N页
                            if (no >= 1 && no <= query.result.PageCount)
                            {
                                query.result.PageIndex = no;
                            }
                        }
                        else
                        {
                            // 需要二次查询，但输入的不是数字或超出范围
                            query = new Query(sessionId);
                            query.performCategoryLike(key);
                        }
                        #endregion
                    }
                    else if (query.result is ItemCollections)
                    {
                        #region 最后一次查询是条目列表

                        if (Int32.TryParse(key, out  no) && (no >= 1 && no <= Constant.PageSize))
                        {
                            no = (query.result.PageIndex - 1) * Constant.PageSize + no;

                            query.performItemStepNO(no);
                        }
                        else if (pagerChar.Contains(key.ToLower()))
                        {
                            /**
                             * 分页的规则：
                             * 
                             * f: 第一页
                             * p: 上一页
                             * n: 下一页
                             * l: 最后一页
                             * gX: 跳到X页
                             * 
                             */

                            // 上下翻页
                            if (key.ToLower().Equals("f"))
                            {
                                query.result.PageIndex = 1;
                            }
                            else if (key.ToLower().Equals("l"))
                            {
                                query.result.PageIndex = query.result.PageCount;
                            }
                            else if (key.ToLower().Equals("p"))
                            {
                                query.result.PageIndex = query.result.PageIndex > 1 ? query.result.PageIndex - 1 : 1;
                            }
                            else if (key.ToLower().Equals("n"))
                            {
                                query.result.PageIndex = query.result.PageIndex < query.result.PageCount ? query.result.PageIndex + 1 : query.result.PageCount;
                            }
                        }
                        else if ((key.ToLower().Trim().StartsWith("g") && Int32.TryParse(key.Trim().Substring(1), out no)))
                        {
                            // 输入 gX 跳到第N页
                            if (no >= 1 && no <= query.result.PageCount)
                            {
                                query.result.PageIndex = no;
                            }
                        }
                        else
                        {
                            // 需要二次查询，但输入的不是数字
                            query = new Query(sessionId);
                            query.performCategoryLike(key);
                        }

                        #endregion
                    }
                    else if (query.result is PriceMedinceResult)
                    {
                        #region 最后一次查询是价格列表

                        if (pagerChar.Contains(key.ToLower()))
                        {
                            /**
                             * 分页的规则：
                             * 
                             * f: 第一页
                             * p: 上一页
                             * n: 下一页
                             * l: 最后一页
                             * gX: 跳到X页
                             * 
                             */

                            // 上下翻页
                            if (key.ToLower().Equals("f"))
                            {
                                query.result.PageIndex = 1;
                            }
                            else if (key.ToLower().Equals("l"))
                            {
                                query.result.PageIndex = query.result.PageCount;
                            }
                            else if (key.ToLower().Equals("p"))
                            {
                                query.result.PageIndex = query.result.PageIndex > 1 ? query.result.PageIndex - 1 : 1;
                            }
                            else if (key.ToLower().Equals("n"))
                            {
                                query.result.PageIndex = query.result.PageIndex < query.result.PageCount ? query.result.PageIndex + 1 : query.result.PageCount;
                            }
                        }
                        else if ((key.ToLower().Trim().StartsWith("g") && Int32.TryParse(key.Trim().Substring(1), out no)))
                        {
                            // 输入 gX 跳到第N页
                            if (no >= 1 && no <= query.result.PageCount)
                            {
                                query.result.PageIndex = no;
                            }
                        }
                        else
                        {
                            // 需要二次查询，但输入的不是数字或超出范围
                            query = new Query(sessionId);
                            query.performCategoryLike(key);
                        }

                        #endregion
                    }
                    else if (query.result is PriceDetailResult)
                    {
                        #region 最后一次查询是普通价格列表。在此列表下，没有可处理的输入，直接进行再一次查询
                        query = new Query(sessionId);
                        query.performCategoryLike(key);
                        #endregion
                    }
                    else if (query.result is PriceFoodResult)
                    {
                        #region 最后一次查询是食品列表。在此列表下，没有可处理的输入，直接进行再一次查询
                        query = new Query(sessionId);
                        query.performCategoryLike(key);
                        #endregion
                    }
                    else if (query.result is PriceURLResult)
                    {
                        #region 最后一次查询是URL的内容列表。在此列表下，没有可处理的输入，直接进行再一次查询

                        query = new Query(sessionId);
                        query.performCategoryLike(key);

                        #endregion
                    }
                    else
                    {
                        // 这个分支走不到，只有上面两种的时候才会放到缓存里。
                    }
                }

                ProcessPager(query);

                model.weichat.QueryLog.save(sessionId, "Search", key, queryTime, DateTime.Now.Ticks - queryTime.Ticks);

                return query.returnResult;
            }
        }

        #region 处理分页，返回指定页的内容

        /// <summary>
        /// 处理分页
        /// </summary>
        private void ProcessPager(Query query)
        {
            if (query.result is CategoryCollections)
            {
                ProcessCategoryPager(query);
            }
            else if (query.result is ItemCollections)
            {
                ProcessItemsPager(query);
            }
            else if (query.result is PriceMedinceResult)
            {
                ProcessMedincePager(query);
            }
            else
            {
                query.returnResult = query.result;
            }
        }

        private void ProcessCategoryPager(Query query)
        {
            // 复制原始数据的session信息，分页信息，根据页数复制条目信息

            query.returnResult = new CategoryCollections();
            query.returnResult.CloneSession(query.result);
            query.returnResult.PageCount = query.result.PageCount;
            query.returnResult.RowCount = query.result.RowCount;
            query.returnResult.PageIndex = query.result.PageIndex;
            query.returnResult.PageSize = query.result.PageSize;

            Int32 start = (query.result.PageIndex - 1) * Constant.PageSize;

            for (int idx = 0; idx < Constant.PageSize; idx++)
            {
                if (start + idx < query.result.RowCount)
                {
                    Category c = ((CategoryCollections)query.result).category[start + idx];
                    c.seqNO = idx + 1;

                    ((CategoryCollections)query.returnResult).category.Add(c);
                }
            }

            // 加上头尾
            if (query.resultTrace.Count > 1)
            {
                ((CategoryCollections)query.returnResult).category.Insert(0, new Category(0) { seqNO = 0, name = returnString });
                ((CategoryCollections)query.returnResult).category.Add(new Category(0) { seqNO = 0, name = returnString });
            }

        }

        private void ProcessItemsPager(Query query)
        {
            // 复制原始数据的session信息，分页信息，根据页数复制条目信息

            query.returnResult = new ItemCollections();
            query.returnResult.CloneSession(query.result);
            query.returnResult.PageCount = query.result.PageCount;
            query.returnResult.RowCount = query.result.RowCount;
            query.returnResult.PageIndex = query.result.PageIndex;
            query.returnResult.PageSize = query.result.PageSize;

            Int32 start = (query.result.PageIndex - 1) * Constant.PageSize;

            for (int idx = 0; idx < Constant.PageSize; idx++)
            {
                if (start + idx < query.result.RowCount)
                {
                    Item item = ((ItemCollections)query.result).items[start + idx];
                    item.seqNO = idx + 1;

                    ((ItemCollections)query.returnResult).items.Add(item);
                }
            }

            // 加上头尾
            if (query.resultTrace.Count > 1)
            {
                ((ItemCollections)query.returnResult).items.Insert(0, new Item(0, "") { seqNO = 0, name = returnString });
                ((ItemCollections)query.returnResult).items.Add(new Item(0, "") { seqNO = 0, name = returnString });
            }
        }

        private void ProcessMedincePager(Query query)
        {
            // 复制原始数据的session信息，分页信息，根据页数复制条目信息

            query.returnResult = new PriceMedinceResult();
            query.returnResult.CloneSession(query.result);
            query.returnResult.PageCount = query.result.PageCount;
            query.returnResult.RowCount = query.result.RowCount;
            query.returnResult.PageIndex = query.result.PageIndex;
            query.returnResult.PageSize = query.result.PageSize;

            Int32 start = (query.result.PageIndex - 1) * Constant.PageSize;

            for (int idx = 0; idx < Constant.PageSize; idx++)
            {
                if (start + idx < query.result.RowCount)
                {
                    ((PriceMedinceResult)query.returnResult).detail.Add(((PriceMedinceResult)query.result).detail[start + idx]);
                }
            }
        }

        #endregion
    }
}
