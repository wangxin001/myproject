using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.weichat
{
    public class Data_ItemPriceValue : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int ID { get; set; }
        public DateTime PriceDate { get; set; }
        public int ItemID { get; set; }
        public String SiteID { get; set; }
        public int PriceID { get; set; }
        public int UnitID { get; set; }
        public Double PriceValue { get; set; }
        public String PriceNote { get; set; }
        public String URL { get; set; }
    }
}
