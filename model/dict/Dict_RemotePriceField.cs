using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.dict
{
    public class Dict_RemotePriceField : OR.Model.Entity
    {
        /// <summary>
        /// 关联 指标业务对象 编号，Dict_RemoteItem
        /// </summary>
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public Int32 ItemID { get; set; }
        public Int32 PriceField { get; set; }
        public String ItemCode { get; set; }
        public String StageCode { get; set; }
        public String TargetCode { get; set; }
        public Decimal Rate { get; set; }
    }
}
