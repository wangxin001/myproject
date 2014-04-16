using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.weichat
{
    public class Dict_PriceCode : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int PriceID  { get; set; }
        public String PriceName { get; set; }
        public String RemotePriceCode { get; set; }
    }
}
