using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class VoteInfo : OR.Model.Entity
    {
        /// <summary>
        /// 调查投票信息标识，自增主键
        /// </summary>
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int VoteID { get; set; }
        /// <summary>
        /// 调查投票名称
        /// </summary>
        public String VoteName { get; set; }
        /// <summary>
        /// 调查投票说明内容
        /// </summary>
        public String VoteDescription { get; set; }
        /// <summary>
        /// 投票开始时间
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// 投标结束时间
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// 数据创建时间，管理字段
        /// </summary>
        public DateTime Created { get; set; }
        public String UserGUID { get; set; }
        public String UserName { get; set; }
        /// <summary>
        /// 状态，当前投票的活动状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 调查结果是否公开  Normal 公开  Forbdden 不公开
        /// </summary>
        public int ResultPublic { get; set; }
    }
}
