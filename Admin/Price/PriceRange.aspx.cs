using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Price_PriceRange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "价格范围管理";

        if (!IsPostBack)
        {
            LoadRangePrice();
        }

    }

    protected void LoadRangePrice()
    {
        List<model.PriceRange> ranges = OR.Control.DAL.GetModelList<model.PriceRange>("1=1");

        foreach (model.PriceRange item in ranges)
        {
            switch ((model.PriceType)item.ItemID)
            {
                case model.PriceType.Chart_CaiJia:
                    this.txtMaxPriceCaiJia.Text = item.Max.ToString();
                    this.txtMinPriceCaiJia.Text = item.Min.ToString();
                    break;
                case model.PriceType.Chart_FuQiangFen:
                    this.txtMaxFuQiangFen.Text = item.Max.ToString();
                    this.txtMinFuQiangFen.Text = item.Min.ToString();
                    break;
                case model.PriceType.Chart_JingMi:
                    this.txtMaxJingMi.Text = item.Max.ToString();
                    this.txtMinJingMi.Text = item.Min.ToString();
                    break;
                case model.PriceType.Chart_TiaoHeYou:
                    this.txtMaxTiaoHeYou.Text = item.Max.ToString();
                    this.txtMinTiaoHeYou.Text = item.Min.ToString();
                    break;
                case model.PriceType.Chart_ZhuRou:
                    this.txtMaxZhuRou.Text = item.Max.ToString();
                    this.txtMinZhuRou.Text = item.Min.ToString();
                    break;
                case model.PriceType.Chart_JiDan:
                    this.txtMaxJiDan.Text = item.Max.ToString();
                    this.txtMinJiDan.Text = item.Min.ToString();
                    break;
                default:
                    break;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        for (int i = (int)model.PriceType.Chart_CaiJia; i <= (int)model.PriceType.Chart_JiDan; i++)
        {
            model.PriceRange item = OR.Control.DAL.GetModelByKey<model.PriceRange>(i);

            if (item == null)
            {
                item = new model.PriceRange();
            }

            switch ((model.PriceType)i)
            {
                case model.PriceType.Chart_CaiJia:
                    item.Max = decimal.Parse(this.txtMaxPriceCaiJia.Text);
                    item.Min = decimal.Parse(this.txtMinPriceCaiJia.Text);
                    break;
                case model.PriceType.Chart_FuQiangFen:
                    item.Max = decimal.Parse(this.txtMaxFuQiangFen.Text);
                    item.Min = decimal.Parse(this.txtMinFuQiangFen.Text);
                    break;
                case model.PriceType.Chart_JingMi:
                    item.Max = decimal.Parse(this.txtMaxJingMi.Text);
                    item.Min = decimal.Parse(this.txtMinJingMi.Text);
                    break;
                case model.PriceType.Chart_TiaoHeYou:
                    item.Max = decimal.Parse(this.txtMaxTiaoHeYou.Text);
                    item.Min = decimal.Parse(this.txtMinTiaoHeYou.Text);
                    break;
                case model.PriceType.Chart_ZhuRou:
                    item.Max = decimal.Parse(this.txtMaxZhuRou.Text);
                    item.Min = decimal.Parse(this.txtMinZhuRou.Text);
                    break;
                case model.PriceType.Chart_JiDan:
                    item.Max = decimal.Parse(this.txtMaxJiDan.Text);
                    item.Min = decimal.Parse(this.txtMinJiDan.Text);
                    break;
                default:
                    break;
            }

            if (item.ItemID == 0)
            {
                item.ItemID = i;
                OR.Control.DAL.Add<model.PriceRange>(item, false);
            }
            else
            {
                OR.Control.DAL.Update<model.PriceRange>(item);
            }
        }
    }
}