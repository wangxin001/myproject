using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.dict
{

    /// <summary>
    /// 品类 树形目录
    /// </summary>
    public class Dict_RemoteTree : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public String NodeCode { get; set; }
        public String NodeName { get; set; }
        public String ParentCode { get; set; }
        public int IsLeaf { get; set; }
        public int Active { get; set; }
        public DateTime LastModified { get; set; }
    }
}
