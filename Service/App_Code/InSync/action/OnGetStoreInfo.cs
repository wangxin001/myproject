using System;
using System.Collections.Generic;
using System.Xml;

namespace Action
{

    /// <summary>
    /// OnGetStore 的摘要说明：
    /// 返回所有的菜店信息
    /// 或输入参数获取制定的菜店信息
    /// <para><code>xxxx</code></para>
    ///
    /// </summary>
    public class OnGetStoreInfo : IAction
    {
        public void perform(string param, XmlResult result)
        {

            List<model.UserInfo> users = null;

            if (!String.IsNullOrEmpty(param))
            {
                ParseParam p = new ParseParam(param);

                if (p.IsParsed)
                {
                    String code = p.GetParamValue("code");

                    if (!String.IsNullOrEmpty(code))
                    {
                        users = OR.Control.DAL.GetModelList<model.UserInfo>(" UserRole=@role AND UserGUID=@guid ",
                            new System.Data.SqlClient.SqlParameter("@role", (int)model.UserRole.数据上报用户),
                            new System.Data.SqlClient.SqlParameter("@guid", code));
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
            else
            {
                users = OR.Control.DAL.GetModelList<model.UserInfo>(" UserRole=@role ", new System.Data.SqlClient.SqlParameter("@role", (int)model.UserRole.数据上报用户));
            }

            if (users.Count == 0)
            {
                throw new NoDataException();
            }

            ConvertResultToXML(result, users);

        }

        protected void ConvertResultToXML(XmlResult result, List<model.UserInfo> users)
        {

            result.AddHeader("total", users.Count.ToString());

            XmlElement items = result.AddRootNode("items");

            for (int i = 0; i < users.Count; i++)
            {
                XmlElement item = result.AddNode(items, "item");

                result.AddNodeValue(item, "StoreCode", users[i].UserGUID);

                result.AddNodeValue(item, "StoreName", users[i].UserName, XmlResult.ValueType.CDATA);
                result.AddNodeValue(item, "ContactName", users[i].ContactName, XmlResult.ValueType.CDATA);
                result.AddNodeValue(item, "ContactTitle", users[i].ContactTitle, XmlResult.ValueType.CDATA);
                result.AddNodeValue(item, "ContactPhone", users[i].ContactPhone, XmlResult.ValueType.CDATA);
                result.AddNodeValue(item, "ContactMobile", users[i].ContactMobile, XmlResult.ValueType.CDATA);
                result.AddNodeValue(item, "ContactEmail", users[i].ContactEmail, XmlResult.ValueType.CDATA);

                result.AddNodeValue(item, "ifactive", users[i].Status.ToString());

            }
        }
    }
}