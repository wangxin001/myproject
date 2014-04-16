using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.report
{
    public class Report_PriceAverage : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int PriceID { get; set; }
        public DateTime PriceDate { get; set; }
        public int PriceType { get; set; }
        public String ItemCode { get; set; }
        public double PriceAvg { get; set; }
        public String UnitCode { get; set; }
    }
}
