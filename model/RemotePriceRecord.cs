using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    /// <summary>
    /// 暂存数据表。临时放一下。审批完后，才正式放进去
    /// </summary>
    public class RemotePriceRecord : OR.Model.Entity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int PriceID { get; set; }
        /// <summary>
        /// 蔬菜编号，JJW编号
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// 报价日期
        /// </summary>
        public DateTime PriceDate { get; set; }

        /// <summary>
        /// 蔬菜名称
        /// </summary>
        public String ItemName { get; set; }
        /// <summary>
        /// 蔬菜规格等级
        /// </summary>
        public String ItemLevel { get; set; }
        /// <summary>
        /// 蔬菜价格单位
        /// </summary>
        public String ItemUnit { get; set; }
        /// <summary>
        /// 价格类型 1:蔬菜 2:大宗 3:蔬菜指数 4:CPI
        /// </summary>
        public int ItemType { get; set; }
        /// <summary>
        /// 成交量 万公斤
        /// 该字段是预留字段，在蔬菜价格中使用改字段记录成交量，其他类型的产品待定，大宗商品不用
        /// </summary>
        public decimal Price01 { get; set; }
        /// <summary>
        /// 批发价 元/公斤。蔬菜价格标识批发价，大宗商品价格，CPI指数等标识当前数值等。
        /// </summary>
        public decimal Price02 { get; set; }
        /// <summary>
        /// 农贸零售价 元/500g。同上，蔬菜价格使用，XPI使用，其他的不用。
        /// </summary>
        public decimal Price03 { get; set; }
        /// <summary>
        /// 超市零售价 元/500g。同上，蔬菜价格使用，XPI使用，其他的不用
        /// </summary>
        public decimal Price04 { get; set; }
        /// <summary>
        /// 状态，是否已批准。批准后才会写入正式数据表里。这里只是暂存
        /// </summary>
        public int Status { get; set; }

    }
}
