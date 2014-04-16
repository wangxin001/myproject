using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class PriceRange : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public int ItemID { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
    }
}
