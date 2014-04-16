using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.weichat
{
    public class Dict_Attribute : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int AttributeID { get; set; }
        public String AttributeName { get; set; }
        public String AttributeType { get; set; }
    }
}
