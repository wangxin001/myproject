using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Action
{
    /// <summary>
    /// OnUpdateKeyValue 的摘要说明
    /// 
    /// 更新KeyValue的字典表内容
    /// 
    /// <para><id></id><code></code><name></name><ifactive></ifactive></para>
    /// 
    /// </summary>
    public class OnUpdateKeyValue : IAction
    {

        public void perform(string param, XmlResult result)
        {
            ParseParam p = new ParseParam(param);

            if (p.IsParsed)
            {
                String id = p.GetParamValue("id");
                String code = p.GetParamValue("code");
                String name = p.GetParamValue("name");
                String active = p.GetParamValue("ifactive");

                #region 验证参数内容
               
                if (String.IsNullOrEmpty(code) || String.IsNullOrEmpty(name))
                {
                    throw new InvalidActionParameterException();
                }

                if (!Validator.IsInteger(id, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }))
                {
                    throw new InvalidActionParameterException();
                }

                int type = Int32.Parse(id);


                if (!Validator.IsInteger(active, new int[] { 0, 1 }))
                {
                    throw new InvalidActionParameterException();
                }

                int ifactive = Int32.Parse(active);

                #endregion

                model.dict.Dict_RemoteDictValue dictValue = OR.Control.DAL.GetModelByKey<model.dict.Dict_RemoteDictValue>(code);

                if (dictValue == null)
                {
                    dictValue = new model.dict.Dict_RemoteDictValue();

                    dictValue.Active = ifactive;
                    dictValue.Code = code;
                    dictValue.LastModified = DateTime.Now;
                    dictValue.Name = name;
                    dictValue.Type = type;

                    OR.Control.DAL.Add<model.dict.Dict_RemoteDictValue>(dictValue, false);
                }
                else
                {
                    dictValue.Active = ifactive;
                    dictValue.Code = code;
                    dictValue.LastModified = DateTime.Now;
                    dictValue.Name = name;
                    dictValue.Type = type;

                    OR.Control.DAL.Update<model.dict.Dict_RemoteDictValue>(dictValue);
                }
            }
            else
            {
                throw new InvalidActionParameterException();
            }
        }

    }
}