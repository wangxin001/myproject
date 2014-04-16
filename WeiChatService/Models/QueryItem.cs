using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiChatService.Models.pojo;
using System.Data;

namespace WeiChatService.Models
{
    public class QueryItem
    {

        public static void fullFillItems(Query query, List<model.weichat.Dict_ItemCode> items)
        {
            ItemCollections c = new ItemCollections();
            c.CloneSession(query.result);
            c.resultType = ResultType.Items;

            int i = 1;
            foreach (model.weichat.Dict_ItemCode item in items)
            {
                c.items.Add(new pojo.Item(item.ItemID, item.RetailItemCode) { name = item.ItemName, seqNO = i++ });
            }

            query.result = c;
            query.resultTrace.Add(c);

            query.result.PageSize = Constant.PageSize;
            query.result.RowCount = c.items.Count;
            query.result.PageIndex = 1;
            query.result.PageCount = (int)(c.items.Count + Constant.PageSize - 1) / Constant.PageSize;
        }

        /// <summary>
        /// 按条目进行价格检索
        /// </summary>
        /// <param name="itemId"></param>
        public static void showItemPrice(Query query, int itemId)
        {
            /**
             * 首先判断该价格条目需要使用什么类型的模版，然后根据模版显示不同的内容
             */

            model.weichat.Dict_ItemCode item = OR.Control.DAL.GetModelByKey<model.weichat.Dict_ItemCode>(itemId);

            switch ((model.weichat.TemplateType)item.TemplateType)
            {
                case model.weichat.TemplateType.Detail:
                    performType_Detail(query, itemId);
                    break;
                case model.weichat.TemplateType.URL:
                    performType_URL(query, itemId);
                    break;
                case model.weichat.TemplateType.Food:
                    performType_Food(query, itemId);
                    break;
                case model.weichat.TemplateType.Medicine:
                    performType_Medince(query, itemId);
                    break;
                default:
                    query.result = new pojo.PriceDetailResult();
                    break;
            }
        }

        /// <summary>
        /// ResultType.Detail
        /// </summary>
        private static void performType_Detail(Query query, int itemId)
        {

            List<model.weichat.View_ItemPrice> prices = OR.Control.DAL.GetModelList<model.weichat.View_ItemPrice>(" ItemID = @id AND PriceDate = (Select Max(PriceDate) From Data_ItemPriceValue where ItemID=@id)",
                new System.Data.SqlClient.SqlParameter("@id", itemId));

            pojo.PriceDetailResult p = new pojo.PriceDetailResult();
            p.CloneSession(query.result);
            p.resultType = ResultType.Detail;

            p.detail = new List<pojo.PriceDetail>();

            foreach (model.weichat.View_ItemPrice price in prices)
            {
                p.detail.Add(
                    new pojo.PriceDetail()
                    {
                        date = price.PriceDate,
                        name = price.ItemName,
                        price = price.PriceValue,
                        priceName = price.PriceName,
                        site = price.SiteName,
                        unit = price.UnitName
                    }
                    );
            }

            query.result = p;

        }

        /// <summary>
        /// ResultType.URL
        /// </summary>
        private static void performType_URL(Query query, int itemId)
        {
            List<model.weichat.View_ItemPrice> prices = OR.Control.DAL.GetModelList<model.weichat.View_ItemPrice>(" ItemID = @id AND PriceDate = (Select Max(PriceDate) From Data_ItemPriceValue where ItemID=@id) ",
                new System.Data.SqlClient.SqlParameter("@id", itemId));

            pojo.PriceURLResult p = new pojo.PriceURLResult();
            p.CloneSession(query.result);
            p.resultType = ResultType.URL;

            p.detail = new List<pojo.PriceURL>();

            foreach (model.weichat.View_ItemPrice price in prices)
            {
                p.detail.Add(
                    new pojo.PriceURL()
                    {
                        date = price.PriceDate,
                        name = price.ItemName,
                        price = price.PriceNote,
                        url = price.URL
                    }
                    );
            }

            query.result = p;
        }

        /// <summary>
        /// ResultType.Food
        /// </summary>
        private static void performType_Food(Query query, int itemId)
        {
            List<model.weichat.View_ItemPrice> prices = OR.Control.DAL.GetModelList<model.weichat.View_ItemPrice>(" ItemID = @id AND PriceDate = (Select Max(PriceDate) From Data_ItemPriceValue where ItemID=@id) Order By SiteID,PriceID",
                new System.Data.SqlClient.SqlParameter("@id", itemId));

            pojo.PriceFoodResult p = new pojo.PriceFoodResult();
            p.CloneSession(query.result);
            p.resultType = ResultType.Food;

            p.detail = new List<pojo.PriceFood>();

            String currentSiteCode = String.Empty;

            foreach (model.weichat.View_ItemPrice price in prices)
            {
                if (!price.SiteID.Equals(currentSiteCode))
                {
                    PriceFood t = new PriceFood();
                    t.prices = new List<pojo.p>();
                    p.detail.Add(t);

                    currentSiteCode = price.SiteID;

                    p.detail[p.detail.Count - 1].date = price.PriceDate;
                    p.detail[p.detail.Count - 1].name = price.ItemName;
                    p.detail[p.detail.Count - 1].site = price.SiteName;
                }

                p.detail[p.detail.Count - 1].prices.Add(new pojo.p() { priceName = price.PriceName, price = price.PriceValue, unit = price.UnitName });
            }

            query.result = p;
        }

        private static void performType_Medince(Query query, int itemId)
        {
            model.weichat.View_ItemPrice price = OR.Control.DAL.GetModel<model.weichat.View_ItemPrice>("ItemID = @id",
                new System.Data.SqlClient.SqlParameter("@id", itemId));

            PriceMedince pM = new PriceMedince();

            pM.date = price.PriceDate;
            pM.memo = price.PriceNote;
            pM.name = price.ItemName;
            pM.price = price.PriceValue;
            pM.priceName = price.PriceName;
            pM.unit = price.UnitName;

            pM.spec = GetItemAttributeValue(itemId, "规格");
            pM.type = GetItemAttributeValue(itemId, "剂型");

            ((pojo.PriceMedinceResult)query.result).detail.Add(pM);
        }

        private static String GetItemAttributeValue(int itemID, String attributeName)
        {
            DataTable dt = GetCachedAttributeValues(attributeName);

            DataRow[] row = dt.Select("ItemID = " + itemID.ToString());

            if (row.Length > 0)
            {
                return row[0]["AttributeValue"].ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        private static Dictionary<String, DataTable> cachedAttributeValue = new Dictionary<string, DataTable>();

        private static DataTable GetCachedAttributeValues(String attributeName)
        {
            if (!cachedAttributeValue.ContainsKey(attributeName))
            {
                DataTable dt = OR.DB.SQLHelper.Query("Select * From Data_ItemAttributeValue Where AttributeID = (Select AttributeID From Dict_Attribute where AttributeName = @an)", 
                    new System.Data.SqlClient.SqlParameter("@an", attributeName)).Tables[0];

                cachedAttributeValue.Add(attributeName, dt);

                return dt;
            }
            else
            {
                return cachedAttributeValue[attributeName];
            }
        }
    }
}