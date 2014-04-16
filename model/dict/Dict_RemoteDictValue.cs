using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.dict
{
    /// <summary>
    /// Code-name型指标类型列表
    /// </summary>
    public enum RemoteDictType
    {
        计量单位 = 1,
        环节 = 2,
        产地 = 3,
        品牌 = 4,
        规格等级 = 5,
        频率 = 6,
        指标 = 7,
        采集点报送类型 = 8,
        采集点类型 = 9,
        采集方式 = 10,
        数据来源 = 11
    }

    /// <summary>
    /// Code-name型指标接口
    /// </summary>
    public class Dict_RemoteDictValue : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public String Code { get; set; }
        public String Name { get; set; }
        public int Type { get; set; }
        public int Active { get; set; }
        public DateTime LastModified { get; set; }
    }
}
