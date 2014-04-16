using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Price_YuanYouPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "原油价格指数录入";

        if (!IsPostBack)
        {
            this.txtPriceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            LoadPriceIndex();
        }
    }

    protected void LoadPriceIndex()
    {
        // 获取完整的数据记录，所有点数都显示出来
        String strSQL = " Select PriceID,PriceDate,ISNULL(Price02,0.0) Price02,ISNULL(Price03,0.0) Price03,ISNULL(Price04,0.0) Price04 ";
        strSQL += " From PriceRecord WHERE ItemType = " + ((int)model.PriceType.Index_YuanYou).ToString();
        strSQL += " Order By PriceDate Desc ";

        DataTable dtPrice = OR.DB.SQLHelper.Query(strSQL).Tables[0];

        // 获取最大时间，最小时间
        DateTime maxDate = DateTime.Now.Date;
        DateTime minDate = DateTime.Now.Date;
        if (dtPrice.Rows.Count>0){
            maxDate = Convert.ToDateTime(dtPrice.Rows[0]["PriceDate"]);
            minDate = Convert.ToDateTime(dtPrice.Rows[dtPrice.Rows.Count - 1]["PriceDate"]);
        }

        // 计算有多少个月
        int iCount = 0;
        for (DateTime curDateTmp = minDate; curDateTmp <= maxDate; )
        {
            iCount++;
            curDateTmp = curDateTmp.AddDays(1);
        }

        // 准备显示用数据
        DataTable dtResult = dtPrice.Clone();

        // 计算有多少数据，开始以及结束索引
        this.AspNetPager.RecordCount = iCount;

        int startIndex = this.AspNetPager.PageSize * (this.AspNetPager.CurrentPageIndex - 1);

        int endIndex = startIndex + this.AspNetPager.PageSize;

        for (int i = startIndex; i < endIndex && i < iCount; i++)
        {
            DataRow dr = dtResult.NewRow();

            DateTime curDay = maxDate.AddDays(0 - i);

            DataRow[] drPriceIndex = dtPrice.Select("PriceDate=#" + curDay + "#");

            dr["PriceDate"] = curDay;

            if (drPriceIndex.Length > 0)
            {
                dr["PriceID"] = Convert.ToInt32(drPriceIndex[0]["PriceID"]);
                dr["Price02"] = Convert.ToDecimal(drPriceIndex[0]["Price02"].ToString());
                dr["Price03"] = Convert.ToDecimal(drPriceIndex[0]["Price03"].ToString());
                dr["Price04"] = Convert.ToDecimal(drPriceIndex[0]["Price04"].ToString());
            }
            else
            {
                dr["PriceID"] = 0;
                dr["Price03"] = 0;
                dr["Price02"] = 0.0M;
                dr["Price04"] = 0.0M;
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

            if (data["Price02"] == DBNull.Value && data["Price03"] == DBNull.Value)
            {
                e.Row.Cells[1].Text = "-";
            }

            if (data["Price04"] != DBNull.Value && Convert.ToDecimal(data["Price04"]) == 1.0M)
            {
                e.Row.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            }

            LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
            btnDelete.CommandName = "Del";
            btnDelete.CommandArgument = data["PriceID"].ToString();

            String strDate = Convert.ToDateTime(data["PriceDate"].ToString()).ToString("yyyy-MM-dd");
            HyperLink lnkInput = e.Row.FindControl("lnkInput") as HyperLink;
            lnkInput.Attributes["onclick"] = "javascript:InputPriceIndex('" + strDate + "');return false;";
            lnkInput.Attributes["href"] = "###";
        }
    }

    protected void AspNetPager_PageChanged(object sender, EventArgs e)
    {
        LoadPriceIndex();
    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        LoadPriceIndex();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {

            model.PriceRecord price = OR.Control.DAL.GetModelByKey<model.PriceRecord>(Convert.ToInt32(e.CommandArgument));

            OR.Control.DAL.Delete<model.PriceRecord>(price);

            Log.info("删除原油价格指数：" + price.ToString(true));

            LoadPriceIndex();
        }
    }
}