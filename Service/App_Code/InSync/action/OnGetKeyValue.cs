using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Action
{
    /// <summary>
    /// OnGetKeyValue 的摘要说明
    /// 获取所有的KeyValue
    /// 
    /// <para><id></id></para>
    /// 
    /// </summary>
    public class OnGetKeyValue : IAction
    {

        public void perform(string param, XmlResult result)
        {
            
            if (String.IsNullOrEmpty(param))
            {
                throw new InvalidActionParameterException();
            }

            ParseParam p = new ParseParam(param);

            if (!p.IsParsed)
            {
                throw new InvalidActionParameterException();
            }

            String id = p.GetParamValue("id");

            Int32 type = 0;

            if (String.IsNullOrEmpty(id) || !Int32.TryParse(id, out type))
            {
                throw new InvalidActionParameterException();
            }

            if (type < 1 || type > 11)
            {
                throw new InvalidActionParameterException();
            }

            List<model.dict.Dict_RemoteDictValue> dictValue = OR.Control.DAL.GetModelList<model.dict.Dict_RemoteDictValue>("type=@type",
                new System.Data.SqlClient.SqlParameter("@type", type));

            if (dictValue.Count == 0)
            {
                throw new NoDataException();
            }

            ConvertResultToXML(result, dictValue);

        }


        protected void ConvertResultToXML(XmlResult result, List<model.dict.Dict_RemoteDictValue> dictValues)
        {

            result.AddHeader("total", dictValues.Count.ToString());

            XmlElement items = result.AddRootNode("items");

            for (int i = 0; i < dictValues.Count; i++)
            {
                XmlElement item = result.AddNode(items, "item");

                result.AddNodeValue(item, "code", dictValues[i].Code);
                result.AddNodeValue(item, "name", dictValues[i].Name, XmlResult.ValueType.CDATA);

                result.AddNodeValue(item, "ifactive", dictValues[i].Active.ToString());

            }
        }
    }
}