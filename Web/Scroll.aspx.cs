using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Scroll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadVegetablePrice();
        }
    }

    protected void LoadVegetablePrice()
    {
        StringBuilder strSQL = new StringBuilder();

        strSQL.Append(" SELECT i.ItemID ,i.ItemName, i.ItemType, i.ItemOrder, i.ItemUnit, i.ItemLevel, r.PriceDate, r.Price01, r.Price02, r.Price03, r.Price04 ");
        strSQL.Append(" FROM PriceRecord r inner join PriceItems i on r.ItemID=i.ItemID where i.ItemType=" + ((int)model.PriceType.Price_VegetablePrice).ToString() + " and i.Status=1 ");
        strSQL.Append(" and PriceDate = (select MAX(PriceDate) From PriceRecord where ItemType =" + ((int)model.PriceType.Price_VegetablePrice).ToString() + ") ");

        strSQL.Append(" UNION ALL ");

        strSQL.Append(" SELECT i.ItemID ,i.ItemName, i.ItemType, i.ItemOrder, i.ItemUnit, i.ItemLevel, r.PriceDate, r.Price01, r.Price02, r.Price03, r.Price04 ");
        strSQL.Append(" FROM PriceRecord r inner join PriceItems i on r.ItemID=i.ItemID where i.ItemType=" + ((int)model.PriceType.Price_LiangYou).ToString() + " and i.Status=1 ");
        strSQL.Append(" and PriceDate = (select MAX(PriceDate) From PriceRecord where ItemType =" + ((int)model.PriceType.Price_LiangYou).ToString() + ") ");

        strSQL.Append(" UNION ALL ");

        strSQL.Append(" SELECT i.ItemID ,i.ItemName, i.ItemType, i.ItemOrder, i.ItemUnit, i.ItemLevel, r.PriceDate, r.Price01, r.Price02, r.Price03, r.Price04 ");
        strSQL.Append(" FROM PriceRecord r inner join PriceItems i on r.ItemID=i.ItemID where i.ItemType=" + ((int)model.PriceType.Price_RouDan).ToString() + " and i.Status=1 ");
        strSQL.Append(" and PriceDate = (select MAX(PriceDate) From PriceRecord where ItemType =" + ((int)model.PriceType.Price_RouDan).ToString() + ") ");

        DataTable priceData = OR.DB.SQLHelper.Query(strSQL.ToString()).Tables[0];

        DataTable vegetableData = priceData.Clone();
        DataRow[] dr1 = priceData.Select("ItemType=" + ((int)model.PriceType.Price_VegetablePrice).ToString(), "ItemOrder ");
        for (int i = 0; i < dr1.Length; i++)
        {
            vegetableData.ImportRow(dr1[i]);
        }
        this.rpVegetablePrice.DataSource = vegetableData;
        this.rpVegetablePrice.DataBind();


        DataTable liangyouData = priceData.Clone();
        DataRow[] dr2 = priceData.Select("ItemType=" + ((int)model.PriceType.Price_LiangYou).ToString(), "ItemOrder ");
        for (int i = 0; i < dr2.Length; i++)
        {
            liangyouData.ImportRow(dr2[i]);
        }
        this.rpLiangYouPrice.DataSource = liangyouData;
        this.rpLiangYouPrice.DataBind();


        DataTable rouleiData = priceData.Clone();
        DataRow[] dr3 = priceData.Select("ItemType=" + ((int)model.PriceType.Price_RouDan).ToString(), "ItemOrder ");
        for (int i = 0; i < dr3.Length; i++)
        {
            rouleiData.ImportRow(dr3[i]);
        }
        this.rpRouDanPrice.DataSource = rouleiData;
        this.rpRouDanPrice.DataBind();
    }
}