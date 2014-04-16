using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.dict
{
    /// <summary>
    /// 指标业务对象
    /// </summary>
    public class Dict_RemoteItem : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
        public String NodeCode { get; set; }
        public String CatalogCode { get; set; }
        public String BrandCode { get; set; }
        public String SpecCode { get; set; }
        public String AreaCode { get; set; }
        public int Active { get; set; }
        public DateTime LastModified { get; set; }
        public int Status { get; set; }
    }

    public class ItemPojo : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Manually)]
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
        public String NodeCode { get; set; }
        public String NodeName { get; set; }
        public String CataCode { get; set; }
        public String CataName { get; set; }
        public String BrandCode { get; set; }
        public String BrandName { get; set; }
        public String SpecCode { get; set; }
        public String SpecName { get; set; }
        public String AreaCode { get; set; }
        public String AreaName { get; set; }
        public Int32 Active { get; set; }
    }
}
