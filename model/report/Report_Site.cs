using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.report
{

    public class Report_Site : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public int PriceType { get; set; }
        public String SiteCode { get; set; }
        public String SiteName { get; set; }
        public int Status { get; set; }
    }
}
