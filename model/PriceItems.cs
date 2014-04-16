using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class PriceItems : OR.Model.Entity
    {
        /// <summary>
        /// 蔬菜编号
        /// </summary>
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int ItemID { get; set; }
        /// <summary>
        /// 蔬菜名称
        /// </summary>
        public String ItemName { get; set; }
        /// <summary>
        /// 蔬菜规格
        /// </summary>
        public String ItemUnit { get; set; }
        /// <summary>
        /// 价格单位
        /// </summary>
        public String ItemLevel { get; set; }
        /// <summary>
        /// 蔬菜类型，此处不应该说是蔬菜，应该是物品类型，蔬菜是其中一种。
        /// 分为：蔬菜，大宗货物，CPI指数，今日菜价指数等几类
        /// </summary>
        public int ItemType { get; set; }
        /// <summary>
        /// 该农副产品价格是否需上报
        /// </summary>
        public int ReportStatus { get; set; }
        /// <summary>
        /// 创建时间，基础管理字段
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 显示顺序，控制上下或左右位置
        /// </summary>
        public int ItemOrder { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public String UserGUID { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        /// 状态，当前是否有效
        /// </summary>
        public int Status { get; set; }
        
    }
}
