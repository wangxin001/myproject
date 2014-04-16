using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Price_VPIPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "蔬菜价格指数录入";

        if (!IsPostBack)
        {
            this.txtPriceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            LoadVPIPrice();
        }
    }

    protected void LoadVPIPrice()
    {
        // 获取完整的数据记录，所有点数都显示出来
        String strSQL = " Select PriceDate,ISNULL(Price02,0.0) Price02,ISNULL(Price03,0.0) Price03,ISNULL(Price04,0.0) Price04 ";
        strSQL += " From PriceRecord WHERE ItemType = " + ((int)model.PriceType.Index_VPI).ToString();
        strSQL += " Order By PriceDate Desc ";

        DataTable dtPrice = OR.DB.SQLHelper.Query(strSQL).Tables[0];

        // 获取最大时间，最小时间
        DateTime maxDate = Convert.ToDateTime(dtPrice.Rows[0]["PriceDate"]);
        DateTime minDate = Convert.ToDateTime(dtPrice.Rows[dtPrice.Rows.Count - 1]["PriceDate"]);

        // 准备显示用数据
        DataTable dtResult = dtPrice.Clone();

        // 计算有多少数据，开始以及结束索引
        this.AspNetPager.RecordCount = maxDate.Subtract(minDate).Days + 1;
        int startIndex = this.AspNetPager.PageSize * (this.AspNetPager.CurrentPageIndex - 1);

        int endIndex = startIndex + this.AspNetPager.PageSize;

        for (int i = startIndex; i < endIndex && i < this.AspNetPager.RecordCount; i++)
        {
            DataRow dr = dtResult.NewRow();

            DateTime curDay = maxDate.AddDays(0 - i);

            dr["PriceDate"] = curDay;

            DataRow[] drPriceVPI = dtPrice.Select("PriceDate=#" + curDay + "#");

            if (drPriceVPI.Length > 0)
            {
                dr["Price02"] = Convert.ToDecimal(drPriceVPI[0]["Price02"].ToString());
                dr["Price03"] = Convert.ToDecimal(drPriceVPI[0]["Price03"].ToString());
                dr["Price04"] = Convert.ToDecimal(drPriceVPI[0]["Price04"].ToString());
            }
            else
            {
                dr["Price02"] = 0;
                dr["Price03"] = 0;
                dr["Price04"] = 0;
            }

            dtResult.Rows.Add(dr);
        }

        this.GridView1.DataSource = dtResult;
        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow data = ((DataRowView)e.Row.DataItem).Row;

            if (data["Price02"] == DBNull.Value)
            {
                e.Row.Cells[1].Text = "-";
            }
            if (data["Price03"] == DBNull.Value)
            {
                e.Row.Cells[2].Text = "-";
            }
            if (data["Price04"] == DBNull.Value)
            {
                e.Row.Cells[3].Text = "-";
            }
        }
    }

    protected void AspNetPager_PageChanged(object sender, EventArgs e)
    {
        LoadVPIPrice();
    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        LoadVPIPrice();
    }
}