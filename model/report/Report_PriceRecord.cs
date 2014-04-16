using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.report
{

    public enum PriceType : int
    {
        蔬菜零售_超市 = 1,
        蔬菜零售_农贸 = 2,
        副食品零售_超市 = 3,
        副食品零售_农贸 = 4,
        蔬菜批发 = 5,
        肉禽蛋水产批发 = 6,
        粮油批发 = 7
    }

    public class Report_PriceRecord : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int PriceID { get; set; }
        public DateTime PriceDate { get; set; }
        public int PriceType { get; set; }
        public String ItemCode { get; set; }
        public String SiteCode { get; set; }
        public Double Price01 { get; set; }
        public String UnitCode { get; set; }
    }
}
