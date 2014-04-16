using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Action
{

    /// <summary>
    ///OnGetStorePrice 的摘要说明
    ///<para><date>2014-04-14</date></para>
    /// </summary>
    public class OnGetStorePrice : IAction
    {
        /// <summary>
        /// <para><datetime>xxxx</datetime></para>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="result"></param>
        public void perform(string param, Action.XmlResult result)
        {
            if (!String.IsNullOrEmpty(param))
            {
                ParseParam p = new ParseParam(param);

                if (p.IsParsed)
                {
                    String pdate = p.GetParamValue("date");
                    String pStoreGUID = p.GetParamValue("StoreGUID");

                    DateTime queryDate = DateTime.Now;

                    if (!DateTime.TryParse(pdate, out queryDate))
                    {
                        throw new InvalidActionParameterException();
                    }

                    String strSQL = @"  WITH bTime
                                        AS (
	                                        SELECT *
	                                        FROM (
		                                        SELECT StoreGUID
			                                        ,BatchID
			                                        ,Min(Created) batchTime
			                                        ,ROW_NUMBER() OVER (
				                                        PARTITION BY StoreGUID ORDER BY ABS(DATEDIFF(SECOND, MIN(Created), DateAdd(Hour, 10, @date)))
				                                        ) rankID
		                                        FROM StorePrice
		                                        WHERE PriceDate = @date
		                                        GROUP BY StoreGUID
			                                        ,BatchID
		                                        ) t
	                                        WHERE rankID = 1
	                                        )
                                        SELECT sp.*
                                        FROM StorePrice sp
                                        INNER JOIN bTime ON sp.StoreGUID = bTime.StoreGUID
	                                        AND sp.BatchID = bTime.BatchID
	                                        AND sp.PriceDate = @date ";

                    if (!String.IsNullOrEmpty(pStoreGUID))
                    {
                        strSQL += " WHERE sp.StoreGUID=@store ";
                    }

                    DataTable dt = OR.DB.SQLHelper.Query(strSQL,
                        new SqlParameter("@date", queryDate.Date),
                         new SqlParameter("@store", pStoreGUID)).Tables[0];

                    List<model.UserInfo> stores = OR.Control.DAL.GetModelList<model.UserInfo>("UserRole=@role and Status=1",
                        new SqlParameter("@role", (int)model.UserRole.数据上报用户));

                    if (dt.Rows.Count == 0)
                    {
                        throw new NoDataException();
                    }

                    DataTable df = OR.DB.SQLHelper.Query("Select * From Dict_RemotePriceField where PriceField=4").Tables[0];


                    XmlElement nodeStores = result.AddRootNode("Stores");

                    foreach (model.UserInfo store in stores)
                    {

                        if (!String.IsNullOrEmpty(pStoreGUID) && !pStoreGUID.ToUpper().Trim().Equals(store.UserGUID.ToUpper().Trim()))
                        {
                            continue;
                        }

                        DataRow[] row = dt.Select("StoreGUID='" + store.UserGUID + "'");

                        XmlElement nodeStore = result.AddNode(nodeStores, "Store");
                        result.AddNodeValue(nodeStore, "StoreGUID", store.UserGUID);
                        result.AddNodeValue(nodeStore, "StoreName", store.UserName, XmlResult.ValueType.CDATA);

                        result.AddNodeValue(nodeStore, "Count", row.Length.ToString());

                        if (row.Length > 0)
                        {
                            XmlElement items = result.AddNode(nodeStore, "Items");

                            List<model.StorePrice> prices = OR.Control.DAL.DataRowsToList<model.StorePrice>(row);

                            foreach (model.StorePrice price in prices)
                            {
                                DataRow[] r = df.Select("ItemID=" + price.ItemID.ToString());

                                String code = r.Length > 0 ? r[0]["ItemCode"].ToString() : price.ItemID.ToString();

                                XmlElement item = result.AddNode(items, "Item");

                                result.AddNodeValue(item, "ItemName", price.ItemName.ToString(), XmlResult.ValueType.CDATA);
                                result.AddNodeValue(item, "ItemCode", code);
                                result.AddNodeValue(item, "Price", price.Price01.ToString());
                                result.AddNodeValue(item, "PriceDate", price.PriceDate.ToString("yyyy-MM-dd"));
                                result.AddNodeValue(item, "Created", price.Created.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    throw new InvalidActionParameterException();
                }
            }
            else
            {
                throw new InvalidActionParameterException();
            }

        }
    }
}