using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.weichat
{
    public class Dict_UnitCode : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int UnitID { get; set; }
        public String UnitName { get; set; }
        public String RemoteUnitCode { get; set; }
        public Double Rate { get; set; }
    }
}
