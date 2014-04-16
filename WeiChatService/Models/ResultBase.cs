using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace WeiChatService.Models
{
    public enum ResultType : int
    {
        /// <summary>
        /// 非法输入，数字或者字母为关键词查询
        /// </summary>
        Invalid = -1,
        /// <summary>
        /// 空结果集
        /// </summary>
        Empty = 0,
        /// <summary>
        /// 返回对象是多个分类列表
        /// </summary>
        Categories = 1,
        /// <summary>
        /// 返回对象是多个条目列表
        /// </summary>
        Items = 2,
        /// <summary>
        /// 返回内容是价格明细，比如大宗商品，石油有多个地方，每个地方一个报价
        /// </summary>
        [Description("直接显示报价")]
        Detail = 3,
        /// <summary>
        /// 返回内容是一段内容加一段文字，比如政府定价
        /// </summary>
        [Description("URL显示更多")]
        URL = 4,
        /// <summary>
        /// 返回内容是一个条目，多个价格，比如蔬菜，有多个价格类型，每个类型里有多个价格，比如有批发价，零售价，农贸价，每个里面有最高，最低，平均
        /// </summary>
        [Description("蔬菜价格")]
        Food = 5,
        /// <summary>
        /// 返回内容是一个多条目多个价格，每种药品的最高零售价
        /// </summary>
        [Description("中西药价格")]
        Medince=6
    }

    public abstract class ResultBase
    {
        public String resultStatus { get; set; }
        public System.Net.HttpStatusCode resultCode { get; set; }
        public String SessionId { get; set; }

        public ResultType resultType { get; set; }

        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }

        public void CloneSession(ResultBase p)
        {
            this.resultCode = p.resultCode;
            this.resultStatus = p.resultStatus;
            this.resultType = p.resultType;
            this.SessionId = p.SessionId;
        }

        public abstract void ResetPage();
    }

    public class ResultBaseEmpty : ResultBase
    {

        public override void ResetPage()
        {
            throw new NotImplementedException();
        }
    }
}