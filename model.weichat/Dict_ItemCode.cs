using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace model.weichat
{

    public enum TemplateType: int
    {
        /// <summary>
        /// 返回内容是价格明细，比如大宗商品，石油有多个地方，每个地方一个报价
        /// </summary>
        [Description("直接显示报价")]
        Detail = 3,
        /// <summary>
        /// 返回内容是一段内容加一段文字，比如政府定价
        /// </summary>
        [Description("URL显示更多")]
        URL = 4,
        /// <summary>
        /// 返回内容是一个条目，多个价格，比如蔬菜，有多个价格类型，每个类型里有多个价格，比如有批发价，零售价，农贸价，每个里面有最高，最低，平均
        /// </summary>
        [Description("蔬菜价格")]
        Food = 5,

        [Description("中西药品")]
        Medicine=6
    }

    public class Dict_ItemCode : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public String ItemName { get; set; }
        public int TemplateType { get; set; }
        public String RetailItemCode { get; set; }
        public String WholesaleItemCode { get; set; }
        public int OrderID { get; set; }
    }
}
