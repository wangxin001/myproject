using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class VoteResult : OR.Model.Entity
    {
        /// <summary>
        /// 调查结果编号，自增变量
        /// </summary>
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int ResultID { get; set; }
        /// <summary>
        /// 所属调查编号
        /// </summary>
        public int VoteID { get; set; }
        /// <summary>
        /// 当前答案所属问题编号
        /// </summary>
        public int QuestionID { get; set; }
        /// <summary>
        /// 所选选项ID
        /// </summary>
        public int OptionID { get; set; }
        /// <summary>
        /// 记录用户第一次访问的GUID，每次给一个GUID标识身份。通过这个能知道哪些答题选项是一起的
        /// </summary>
        public String SessionGUID { get; set; }
        /// <summary>
        /// 当前问卷人的访问地址
        /// </summary>
        public String RemoteAddr { get; set; }

        public String Created { get; set; }
    }
}
