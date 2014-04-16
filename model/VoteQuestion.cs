using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class VoteQuestion : OR.Model.Entity
    {
        /// <summary>
        /// 问题编号，自增主键
        /// </summary>
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int QuestionID { get; set; }
        /// <summary>
        /// 所属投票编号
        /// </summary>
        public int VoteID { get; set; }
        /// <summary>
        /// 问题内容
        /// </summary>
        public String QuestionContent { get; set; }
        /// <summary>
        /// 问题类型，1：单选，2：多选，3：填空
        /// </summary>
        public int QuestionType { get; set; }
        /// <summary>
        /// 当前问题的问题答案选项数量
        /// </summary>
        public int OptionsCount { get; set; }
        /// <summary>
        /// 显示顺序，从小达到
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 状态，是否被禁用
        /// </summary>
        public int Status { get; set; }

        public DateTime Created { get; set; }
        public String UserGUID { get; set; }
        public String UserName { get; set; }
    }
}
