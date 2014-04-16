using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Action
{
    /// <summary>
    /// OnUpdateTreeValue 的摘要说明
    /// 
    /// 实时更新品类和指标业务对象
    /// 
    /// id: 1 品类
    /// <para><id></id><code></code><name></name><pid></pid><ifactive></ifactive><ifleaf></ifleaf><action>AUD</action></para>
    /// 
    /// id: 2 指标业务对象
    /// 
    /// <para><id></id><code></code><name></name><pid></pid><catcode></catcode><brdcode></brdcode><speccode></speccode><areacode></areacode><ifactive></ifactive><action>AUD</action></para>
    /// 
    /// </summary>
    public class OnUpdateTreeValue : IAction
    {

        public void perform(string param, Action.XmlResult result)
        {
            ParseParam p = new ParseParam(param);

            if (!p.IsParsed)
            {
                throw new InvalidActionParameterException();
            }

            String id = p.GetParamValue("id");
            String code = p.GetParamValue("code");
            String name = p.GetParamValue("name");
            String pid = p.GetParamValue("pid");
            String ifactive = p.GetParamValue("ifactive");
            String ifleaf = p.GetParamValue("ifleaf");
            String action = p.GetParamValue("action");

            if (String.IsNullOrEmpty(code) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(pid)
                || !Validator.IsInteger(id, new int[] { 1, 2 })
                || !Validator.IsInteger(ifactive, new int[] { 0, 1 })
                || !Validator.IsInteger(ifleaf, new int[] { 0, 1 })
                || !Validator.IsSubValue(action, new string[] { "A", "U", "D" }))
            {
                throw new InvalidActionParameterException();
            }

            // 更新品类的内容
            if (id.Equals("1"))
            {
                model.dict.Dict_RemoteTree cata = OR.Control.DAL.GetModelByKey<model.dict.Dict_RemoteTree>(code);

                if (action.ToLower().Equals("d"))
                {
                    // delete

                    if (cata != null)
                    {
                        OR.Control.DAL.Delete<model.dict.Dict_RemoteTree>(cata);
                    }
                    else
                    {
                        throw new NoDataException();
                    }
                }
                else
                {
                    // add or update

                    if (cata == null)
                    {
                        cata = new model.dict.Dict_RemoteTree();

                        cata.Active = Int32.Parse(ifactive);
                        cata.NodeCode = code;
                        cata.NodeName = name;
                        cata.IsLeaf = Int32.Parse(ifleaf);
                        cata.LastModified = DateTime.Now;
                        cata.ParentCode = pid;

                        OR.Control.DAL.Add<model.dict.Dict_RemoteTree>(cata, false);

                    }
                    else
                    {
                        cata.Active = Int32.Parse(ifactive);
                        cata.NodeCode = code;
                        cata.NodeName = name;
                        cata.IsLeaf = Int32.Parse(ifleaf);
                        cata.LastModified = DateTime.Now;
                        cata.ParentCode = pid;

                        OR.Control.DAL.Update<model.dict.Dict_RemoteTree>(cata);
                    }
                }
            }
            else if (id.Equals("2"))
            {
                // <catcode></catcode><brdcode></brdcode><speccode></speccode><areacode></areacode>

                String catcode = p.GetParamValue("catcode");
                String brdcode = p.GetParamValue("brdcode");
                String speccode = p.GetParamValue("speccode");
                String areacode = p.GetParamValue("areacode");

                if (String.IsNullOrEmpty(catcode) || String.IsNullOrEmpty(brdcode) || String.IsNullOrEmpty(speccode) || String.IsNullOrEmpty(areacode))
                {
                    throw new InvalidActionParameterException();
                }


                model.dict.Dict_RemoteItem item = OR.Control.DAL.GetModelByKey<model.dict.Dict_RemoteItem>(code);

                if (action.ToLower().Equals("d"))
                {
                    if (item != null)
                    {
                        OR.Control.DAL.Delete<model.dict.Dict_RemoteItem>(item);
                    }
                    else
                    {
                        throw new NoDataException();
                    }
                }
                else
                {
                    if (item == null)
                    {
                        item = new model.dict.Dict_RemoteItem();

                        item.Active = Int32.Parse(ifactive);
                        item.AreaCode = areacode;
                        item.BrandCode = brdcode;
                        item.CatalogCode = pid;
                        item.ItemCode = code;
                        item.ItemName = name;
                        item.LastModified = DateTime.Now;
                        item.SpecCode = speccode;
                        item.Status = 0;

                        OR.Control.DAL.Add<model.dict.Dict_RemoteItem>(item, false);
                    }
                    else
                    {
                        item.Active = Int32.Parse(ifactive);
                        item.AreaCode = areacode;
                        item.BrandCode = brdcode;
                        item.CatalogCode = pid;
                        item.ItemCode = code;
                        item.ItemName = name;
                        item.LastModified = DateTime.Now;
                        item.SpecCode = speccode;
                        item.Status = 0;

                        OR.Control.DAL.Update<model.dict.Dict_RemoteItem>(item);
                    }
                }
            }
        }
    }
}