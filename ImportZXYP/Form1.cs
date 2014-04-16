using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace ImportZXYP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataSet ds = new DataSet();
            ds.ReadXml("D:\\01.xml", XmlReadMode.ReadSchema);

            DataTable dt = ds.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 100 == 0)
                {
                    Console.WriteLine(String.Format("数据总数: {0}, 当前数量: {1}", dt.Rows.Count, i + 1));
                }
                ImportDataRow(dt.Rows[i]);
            }
        }

        private const int rootID = 88;
        private SqlParameter pRoot = new SqlParameter("@pid", rootID);

        private void ImportDataRow(DataRow row)
        {
            if (row[8].ToString().ToLower().Equals("null"))
            {
                return;
            }

            String siteName = row[0].ToString();
            String itemName = row[1].ToString();
            String itemJX = row[2].ToString();
            String itemGG = row[3].ToString();
            String wjh = row[4].ToString();
            String date = row[5].ToString();
            String memo = row[6].ToString();
            String unit = row[7].ToString();
            Double price = Double.Parse(row[8].ToString());
            String lx = row[9].ToString();

            //Console.WriteLine(String.Format("名称: {0}", itemName));

            // 最开始 按中西药建立分类

            model.weichat.Dict_Category categoryLX = OR.Control.DAL.GetModel<model.weichat.Dict_Category>("PID=@pid AND CategoryName=@lx", pRoot, new SqlParameter("@lx", lx));

            if (categoryLX == null)
            {
                categoryLX = new model.weichat.Dict_Category();
                categoryLX.CategoryName = lx;
                categoryLX.PID = rootID;

                categoryLX = OR.Control.DAL.Add<model.weichat.Dict_Category>(categoryLX, true);
            }
                
            // 第一步，按药品名称建立分类

            model.weichat.Dict_Category categoryItem = OR.Control.DAL.GetModel<model.weichat.Dict_Category>("PID=@pid AND CategoryName=@cn",
                new SqlParameter("@pid", categoryLX.CategoryID), new SqlParameter("@cn", itemName));

            if (categoryItem == null)
            {
                categoryItem = new model.weichat.Dict_Category();
                categoryItem.CategoryName = itemName;
                categoryItem.PID = categoryLX.CategoryID;

                categoryItem = OR.Control.DAL.Add<model.weichat.Dict_Category>(categoryItem, true);
            }

            // 第二步，按剂型建立二级分类
            model.weichat.Dict_Category categoryJX = OR.Control.DAL.GetModel<model.weichat.Dict_Category>("PID=@pid AND CategoryName=@cn", 
                new SqlParameter("@pid", categoryItem.CategoryID),  
                new SqlParameter("@cn", itemJX));

            if (categoryJX == null)
            {
                categoryJX = new model.weichat.Dict_Category();
                categoryJX.PID = categoryItem.CategoryID;
                categoryJX.CategoryName = itemJX;
                categoryJX.Exclude = 1;

                categoryJX = OR.Control.DAL.Add<model.weichat.Dict_Category>(categoryJX, true);
            }

            // 第三步，建立条目内容
            model.weichat.Dict_ItemCode item = new model.weichat.Dict_ItemCode();
            item.CategoryID = categoryJX.CategoryID;
            item.ItemName = itemName;   // 名称
            item.RetailItemCode = "";
            item.TemplateType = (int)model.weichat.TemplateType.Medicine;
            item.WholesaleItemCode = "";

            item = OR.Control.DAL.Add<model.weichat.Dict_ItemCode>(item, true);

            // 开始插入各类属性信息

            // 插入价格单位信息

            model.weichat.Dict_UnitCode dictUnit = OR.Control.DAL.GetModel<model.weichat.Dict_UnitCode>("UnitName=@unit", new SqlParameter("@unit", unit));
            if (dictUnit == null)
            {
                dictUnit = new model.weichat.Dict_UnitCode();
                dictUnit.Rate = 1.0;
                dictUnit.RemoteUnitCode = "";
                dictUnit.UnitName = unit;

                dictUnit = OR.Control.DAL.Add<model.weichat.Dict_UnitCode>(dictUnit, true);
            }

            // 插入单位信息
            model.weichat.Dict_SiteInfo dictSite = OR.Control.DAL.GetModel<model.weichat.Dict_SiteInfo>("SiteName=@site", new SqlParameter("@site", siteName));
            if (dictSite == null)
            {
                dictSite = new model.weichat.Dict_SiteInfo();
                dictSite.SiteName = siteName;

                dictSite = OR.Control.DAL.Add<model.weichat.Dict_SiteInfo>(dictSite, true);
            }

            // 插入价格信息

            model.weichat.Data_ItemPriceValue priceValue = new model.weichat.Data_ItemPriceValue();
            priceValue.ItemID = item.ItemID;
            priceValue.PriceDate = DateTime.Parse(date);    // 日期
            priceValue.PriceID = GetPriceCode().PriceID;            
            priceValue.PriceNote = memo;        // 备注
            priceValue.PriceValue = price;      // 价格
            priceValue.SiteID = dictSite.SiteID.ToString(); // 管理权限
            priceValue.UnitID = dictUnit.UnitID;
            priceValue.URL = String.Empty;

            OR.Control.DAL.Add<model.weichat.Data_ItemPriceValue>(priceValue, false);

            // 插入其他的属性信息

            InsertItemAttribute(item, "剂型", itemJX);
            InsertItemAttribute(item, "规格", itemGG);
            InsertItemAttribute(item, "文件号", wjh);
        }

        private model.weichat.Dict_PriceCode priceCode = null;

        private model.weichat.Dict_PriceCode GetPriceCode()
        {
            if (priceCode != null)
            {
                return priceCode;
            }

            priceCode = OR.Control.DAL.GetModel<model.weichat.Dict_PriceCode>("PriceName=@p",
                    new SqlParameter("@p", "最高零售价"));

            if (priceCode == null)
            {
                priceCode = new model.weichat.Dict_PriceCode();
                priceCode.PriceName = "最高零售价";

                priceCode = OR.Control.DAL.Add<model.weichat.Dict_PriceCode>(priceCode, true);
            }

            return priceCode;
        }

        private void InsertItemAttribute(model.weichat.Dict_ItemCode item, String name, String value)
        {
            model.weichat.Dict_Attribute a = GetAttribute(name);

            model.weichat.Data_ItemAttributeValue aValue = new model.weichat.Data_ItemAttributeValue();
            aValue.AttributeID = a.AttributeID;
            aValue.ItemID = item.ItemID;
            aValue.AttributeValue = value;

            OR.Control.DAL.Add<model.weichat.Data_ItemAttributeValue>(aValue, false);
        }

        private Dictionary<String, model.weichat.Dict_Attribute> att = new Dictionary<string, model.weichat.Dict_Attribute>();

        private model.weichat.Dict_Attribute GetAttribute(String attributeName)
        {
            if (att.ContainsKey(attributeName))
            {
                return att[attributeName];
            }
            else
            {
                model.weichat.Dict_Attribute a = OR.Control.DAL.GetModel<model.weichat.Dict_Attribute>("AttributeType=@type AND AttributeName=@name",
                    new SqlParameter("@type", "中西药品"),
                    new SqlParameter("@name", attributeName));

                if (a == null)
                {
                    a = new model.weichat.Dict_Attribute();
                    a.AttributeName = attributeName;
                    a.AttributeType = "中西药品";

                    a = OR.Control.DAL.Add<model.weichat.Dict_Attribute>(a, true);
                }

                att.Add(attributeName, a);
            }

            return att[attributeName];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet ds = OR.DB.SQLHelper.Query("Select * From View_Tmp");

            ds.WriteXml("D:\\01.xml", XmlWriteMode.WriteSchema);

        }

    }
}
