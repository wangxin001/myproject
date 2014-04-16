using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Action;

namespace HttpUrlRewrite.InSync
{
    /// <summary>
    ///OnReceive 的摘要说明
    /// </summary>
    public class OnReceive : IHttpHandler
    {

        public void ProcessRequest(HttpContext Context)
        {

            string Url = Context.Request.RawUrl;

            Regex reg = new Regex(@".*/(.*)\.action(.*)", RegexOptions.IgnoreCase);

            Match m = reg.Match(Url);

            if (m.Success)
            {
                String p = Context.Request.QueryString["para"] ?? String.Empty;

                XmlResult ret = DispatchAction(m.Groups[1].Value, p);

                Context.Response.ContentType = "text/xml";
                Context.Response.Write(ret.GetResultString());
            }
            else
            {
                XmlResult result = new XmlResult(ResultCode.InvalidParams);

                Context.Response.ContentType = "text/xml";
                Context.Response.Write(result.GetResultString());
            }
        }

        public bool IsReusable { get { return false; } }

        private XmlResult DispatchAction(String actionName, String requestString)
        {
            ActionClass action = GetActionClass(actionName);

            if (action == null)
            {
                XmlResult result = new XmlResult(ResultCode.InvalidParams);

                return result;
            }

            String clazzName = action.Clazz;

            object c = Activator.CreateInstance(Type.GetType(clazzName));

            if (c is IAction)
            {
                // 是所需要的实例

                XmlResult ret = new XmlResult(ResultCode.Success);

                try
                {
                    ((IAction)c).perform(requestString, ret);
                }
                catch (InvalidActionParameterException)
                {
                    ret = new XmlResult(ResultCode.InvalidParams);
                }
                catch (NoDataException)
                {
                    ret = new XmlResult(ResultCode.NoData);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ret = new XmlResult(ResultCode.DBNoResponse);
                    ret.AddHeader("Message", ex.Message);
                }

                return ret;
            }
            else
            {
                XmlResult result = new XmlResult(ResultCode.InvalidParams);

                return result;
            }

        }

        private ActionClass GetActionClass(String actionName)
        {
            if (_map.Count == 0)
            {
                LoadDispatchMap();
            }

            if (_map.ContainsKey(actionName))
            {
                return _map[actionName];
            }
            else
            {
                return null;
            }
        }

        private static Dictionary<String, ActionClass> _map = new Dictionary<string, ActionClass>();

        private void LoadDispatchMap()
        {
            if (_map.Count == 0)
            {
                XmlDocument _doc = new XmlDocument();
                _doc.Load(HttpContext.Current.Server.MapPath("~/App_Data/ActionDispatchMap.xml"));

                XmlNodeList _actions = _doc.SelectNodes("/Actions/Action");

                foreach (XmlNode _action in _actions)
                {
                    if (!_map.ContainsKey(_action.Attributes["name"].Value))
                    {
                        ActionClass action = new ActionClass(_action.Attributes["name"].Value, _action.Attributes["class"].Value);

                        _map.Add(_action.Attributes["name"].Value, action);
                    }
                }
            }
        }
    }

    class ActionClass
    {
        private String nameSpace = "Action.";

        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private String _clazz;

        public String Clazz
        {
            get { return nameSpace + _clazz; }
            set { _clazz = value; }
        }

        public ActionClass(String name, String clazz)
        {
            this._name = name;
            this._clazz = clazz;
        }
    }
}