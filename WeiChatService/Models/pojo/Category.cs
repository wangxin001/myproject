using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiChatService.Models.pojo
{
    public class Category
    {
        // 该字段内部使用，在序列化时，不返回客户端，所以将其改为private，然后通过接口将其返回
        private int _id;

        public Category(int id)
        {
            this._id = id;
        }

        public int getID()
        {
            return _id;
        }

        public int seqNO { get; set; }
        public String name { get; set; }
        public int exclude { get; set; }
    }

    /// <summary>
    /// Category列表返回值
    /// </summary>
    public class CategoryCollections : ResultBase
    {
        public CategoryCollections()
        {
            category = new List<Category>();
        }

        public List<Category> category { get; set; }


        public override void ResetPage()
        {
            this.PageSize = Constant.PageSize;
            this.RowCount = category.Count;
            this.PageIndex = 1;
            this.PageCount = (int)(category.Count + Constant.PageSize - 1) / Constant.PageSize;
        }
    }
}