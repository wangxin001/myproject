using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.dict
{
    /// <summary>
    /// 数据的状态
    /// </summary>
    public enum DictStatus : int
    {
        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = -1,
        /// <summary>
        /// 等待审批
        /// </summary>
        Pendding = 0,
        /// <summary>
        /// 已批准入库
        /// </summary>
        Approved = 1
    }
}
