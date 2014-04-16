using System;
using System.Xml;

namespace Action
{
    public enum ResultCode : int
    {
        /// <summary>
        /// 000：访问成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 001：没有数据
        /// </summary>
        NoData = 1,
        /// <summary>
        /// 002：不在服务时间之内
        /// </summary>
        NotInService = 2,
        /// <summary>
        /// 003：用户过于频繁访问，请稍后再来
        /// </summary>
        TooFrequent = 3,
        /// <summary>
        /// 004：用户不合法，IP地址错误
        /// </summary>
        InvalidIP = 4,
        /// <summary>
        /// 005：传入参数有误，请核对参数
        /// </summary>
        InvalidParams = 5,
        /// <summary>
        /// 006：数据库无法响应，请稍后访问
        /// </summary>
        DBNoResponse = 6
    }

    /// <summary>
    ///XmlUtils 的摘要说明
    /// </summary>
    public class XmlResult
    {
        public enum ValueType
        {
            Text,
            CDATA
        }

        XmlDocument _doc = new XmlDocument();

        XmlNode _root;
        XmlNode _head;

        String _initString = "<root><ret>000</ret></root>";

        public XmlResult(ResultCode resultCode)
        {
            _doc.LoadXml(_initString);
            _root = _doc.DocumentElement;

            _doc.SelectSingleNode("/root/ret").InnerText = String.Format("{0:D3}", (int)resultCode);
        }

        public void SetResultCode(ResultCode resultCode)
        {
            _doc.SelectSingleNode("/root/ret").InnerText = String.Format("{0:D3}", (int)resultCode);
        }

        public XmlDocument GetResultDom()
        {
            return _doc;
        }

        public String GetResultString()
        {
            return _doc.OuterXml;
        }

        public void AddHeader(String name, String value)
        {
            if (_head == null)
            {
                if ((_head = _root.SelectSingleNode("head")) == null)
                {
                    _head = _doc.CreateElement("head");
                    _root.AppendChild(_head);
                }
            }

            XmlNode _node = _doc.CreateElement(name);
            _node.InnerText = value;

            _head.AppendChild(_node);
        }

        public XmlElement AddRootNode(String nodeName)
        {
            XmlElement items = _doc.CreateElement(nodeName);
            _root.AppendChild(items);

            return items;
        }

        public XmlElement AddNode(String nodeName)
        {
            XmlElement node = _doc.CreateElement(nodeName);
            return node;
        }

        public XmlElement AddNode(XmlElement parentNode, String nodeName)
        {
            XmlElement node = _doc.CreateElement(nodeName);
            parentNode.AppendChild(node);
            return node;
        }

        public XmlElement AddNodeValue(String nodeName, String value)
        {
            XmlElement node = _doc.CreateElement(nodeName);
            node.InnerText = value;
            return node;
        }

        public XmlElement AddNodeValue(String nodeName, String value, ValueType valueType)
        {
            XmlElement node = _doc.CreateElement(nodeName);

            if (valueType == ValueType.CDATA)
            {
                node.AppendChild(_doc.CreateCDataSection(value ?? ""));
            }
            else
            {
                node.InnerText = value ?? "";
            }

            return node;
        }

        public XmlElement AddNodeValue(XmlElement parentNode, String nodeName, String value)
        {
            XmlElement node = _doc.CreateElement(nodeName);
            node.InnerText = value ?? "";
            parentNode.AppendChild(node);
            return node;
        }

        public XmlElement AddNodeValue(XmlElement parentNode, String nodeName, String value, ValueType valueType)
        {
            XmlElement node = _doc.CreateElement(nodeName);

            if (valueType == ValueType.CDATA)
            {
                node.AppendChild(_doc.CreateCDataSection(value ?? ""));
            }
            else
            {
                node.InnerText = value ?? "";
            }

            parentNode.AppendChild(node);
            return node;
        }
    }

    public class ParseParam
    {
        XmlDocument _doc = new XmlDocument();

        public Boolean IsParsed { get; set; }

        public ParseParam(String param)
        {
            try
            {
                if (!String.IsNullOrEmpty(param))
                {
                    _doc.LoadXml(param);

                    ConvertToHash();

                    IsParsed = true;
                }

            }
            catch (Exception)
            {
                IsParsed = false;
            }
        }

        System.Collections.Generic.Dictionary<String, String> _paramMap = new System.Collections.Generic.Dictionary<string, string>();

        private void ConvertToHash()
        {
            if (_doc.HasChildNodes)
            {
                XmlNode paraNode = _doc.SelectSingleNode("para");

                XmlNodeList childNodes = paraNode.ChildNodes;

                for (int i = 0; i < childNodes.Count; i++)
                {
                    if (!_paramMap.ContainsKey(childNodes[i].Name.ToLower().Trim() ))
                    {
                        _paramMap.Add(childNodes[i].Name.ToLower().Trim(), childNodes[i].InnerText.ToLower().Trim());
                    }
                }
            }
        }

        public String GetParamValue(String paramName)
        {
            if (_paramMap.ContainsKey(paramName.ToLower().Trim()))
            {
                return _paramMap[paramName.ToLower().Trim()];
            }

            return String.Empty;
        }
    }

    public class NoDataException : Exception
    {

    }

    public class InvalidActionParameterException : Exception
    {

    }

    public class Validator
    {
        public static Boolean IsInteger(String obj)
        {
            if (obj == null)
            {
                return false;
            }

            int v = 0;

            if (!Int32.TryParse(obj, out v))
            {
                return false;
            }

            return true;
        }

        public static Boolean IsInteger(String obj, int[] valueSet)
        {

            if (String.IsNullOrEmpty(obj))
            {
                return false;
            }

            int v = 0;

            if (!Int32.TryParse(obj, out v))
            {
                return false;
            }

            for (int i = 0; i < valueSet.Length; i++)
            {
                if (v == valueSet[i])
                {
                    return true;
                }
            }

            return false;
        }

        public static Boolean IsSubValue(String obj, String[] values ){

            if (String.IsNullOrEmpty(obj))
            {
                return false;
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (obj.ToLower().Equals(values[i].ToLower()))
                {
                    return true;
                }
            }

            return false;

        }

    }

}