using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.weichat
{
    public class Data_ItemAttributeValue : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public int ItemID { get; set; }
        public int AttributeID { get; set; }
        public String AttributeValue { get; set; }
    }
}
