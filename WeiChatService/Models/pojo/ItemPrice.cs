using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiChatService.Models.pojo
{

    public class PriceBase
    {
        /// <summary>
        /// 价格发布日期
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// 条目名称
        /// </summary>
        public String name { get; set; }
    }

    #region Detail

    public class PriceDetail : PriceBase
    {
        public String site { get; set; }
        public String priceName { get; set; }
        public String unit { get; set; }
        public double price { get; set; }
    }

    /// <summary>
    /// detail 类型的价格返回值
    /// </summary>
    public class PriceDetailResult : ResultBase
    {
        public List<PriceDetail> detail { get; set; }

        public override void ResetPage()
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region URL

    public class PriceURL : PriceBase
    {
        public String price { get; set; }
        public String url { get; set; }
    }

    /// <summary>
    /// URL 类型的价格返回值
    /// </summary>
    public class PriceURLResult : ResultBase
    {
        public List<PriceURL> detail { get; set; }

        public override void ResetPage()
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region Food Price

    public class p
    {
        public String priceName { get; set; }
        public String unit { get; set; }
        public double price { get; set; }
    }

    public class PriceFood : PriceBase
    {
        public String site { get; set; }
        public List<p> prices { get; set; }
    }

    /// <summary>
    /// Food 类型的价格返回值
    /// </summary>
    public class PriceFoodResult : ResultBase
    {
        public List<PriceFood> detail { get; set; }

        public override void ResetPage()
        {
            throw new NotImplementedException();
        }
    }
    
    #endregion

    #region Medince Price

    public class PriceMedince : PriceBase
    {
        /// <summary>
        /// 价格名称
        /// </summary>
        public String priceName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public String unit { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String memo { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public String spec { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public String type { get; set; }
    }

    public class PriceMedinceResult : ResultBase
    {
        public List<PriceMedince> detail { get; set; }

        public PriceMedinceResult()
        {
            detail = new List<PriceMedince>();
        }

        public override void ResetPage()
        {
            this.PageSize = Constant.PageSize;
            this.RowCount = detail.Count;
            this.PageIndex = 1;
            this.PageCount = (int)(detail.Count + Constant.PageSize - 1) / Constant.PageSize;
        }
    }

    #endregion
}