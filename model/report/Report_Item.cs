using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.report
{
    public class Report_Item : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public int PriceType { get; set; }
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
        public Double Rate { get; set; }
        public int Status { get; set; }
        public int OrderID { get; set; }
        public int LocalItemID { get; set; }
    }
}
