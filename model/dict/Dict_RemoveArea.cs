using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.dict
{
    /// <summary>
    /// 
    /// </summary>
    public class Dict_RemoveArea : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public String AreaCode { get; set; }
        public String AreaName { get; set; }
        public String ParentCode { get; set; }
        public int Active { get; set; }
        public DateTime LastModified { get; set; }
        public int Status { get; set; }
    }
}
