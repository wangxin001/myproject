using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.weichat
{
    public class View_ItemPrice : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public int ID { get; set; }
        public DateTime PriceDate { get; set; }
        public int  ItemID { get; set; }
        public String ItemName { get; set; }
        public int TemplateType { get; set; }
        public String SiteID { get; set; }
        public String SiteName { get; set; }
        /// <summary>
        /// 价格类型编号
        /// </summary>
        public int PriceID { get; set; }
        /// <summary>
        /// 价格类型名称
        /// </summary>
        public String PriceName { get; set; }
        public int UnitID { get; set; }
        public String UnitName { get; set; }
        public double PriceValue { get; set; }
        public String PriceNote { get; set; }
        public String URL { get; set; }
    }
}
