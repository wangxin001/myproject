using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class VoteOption : OR.Model.Entity
    {
        /// <summary>
        /// 选项标识，自增主键
        /// </summary>
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int OptionID { get; set; }
        /// <summary>
        /// 选项所属调查编号
        /// </summary>
        public int VoteID { get; set; }
        /// <summary>
        /// 选项所属问题编号
        /// </summary>
        public int QuestionID { get; set; }
        /// <summary>
        /// 选项内容
        /// </summary>
        public String OptionContent { get; set; }
        /// <summary>
        /// 选项的显示顺序
        /// </summary>
        public int OptionOrder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; }
    }
}
