using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.dict
{

    /// <summary>
    /// 品种
    /// </summary>
    public class Dict_RemoteCata : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public String CataCode { get; set; }
        public String CataName { get; set; }
        public String NodeCode { get; set; }
        public int Active { get; set; }
        public DateTime LastModified { get; set; }
    }
}
