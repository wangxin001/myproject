using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiChatService.Models.pojo
{
    public class Item
    {
        // 该字段内部使用，在序列化时，不返回客户端，所以将其改为private，然后通过接口将其返回
        private int _id;
        public int getID()
        {
            return _id;
        }

        private String _code;
        public String getCode()
        {
            return _code;
        }

        public Item(int id, String code)
        {
            this._id = id;
            this._code = code;
        }

        public int seqNO { get; set; }
        public String name { get; set; }
    }

    /// <summary>
    /// 多条目返回内容
    /// </summary>
    public class ItemCollections : ResultBase
    {
        public ItemCollections()
        {
            items = new List<Item>();
        }

        public List<Item> items { get; set; }

        public override void ResetPage()
        {
            this.PageSize = Constant.PageSize;
            this.RowCount = items.Count;
            this.PageIndex = 1;
            this.PageCount = (int)(items.Count + Constant.PageSize - 1) / Constant.PageSize;
        }
    }
}