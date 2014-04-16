using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class vegetables_Price : System.Web.UI.Page
{
    protected String strDate = string.Empty;
    DateTime? curDate = null;

    private String priceType = String.Empty;
    private model.PriceType pt = model.PriceType.Price_VegetablePrice;

    protected void Page_Load(object sender, EventArgs e)
    {

        // 先处理价格类型
        priceType = Request.QueryString["PriceType"];

        if (!String.IsNullOrEmpty(priceType))
        {
            try
            {
                pt = (model.PriceType)Convert.ToInt32(priceType);
            }
            catch (Exception)
            {
                pt = model.PriceType.Price_VegetablePrice;
            }
        }

        // 现在开始处理时间
        strDate = Request.QueryString["t"] ?? "";
        String[] atm = strDate.Split('-');

        if (atm.Length == 3)
        {
            String t1 = (100 + Convert.ToInt32(atm[1])).ToString();
            strDate = atm[0] + t1.Substring(t1.Length - 2);

            t1 = (100 + Convert.ToInt32(atm[2])).ToString();
            strDate += t1.Substring(t1.Length - 2);
        }

        if (!String.IsNullOrEmpty(strDate))
        {
            // 如果长度不对，不是8字符长
            // 这里直接按默认正确的方式组织数据，如有异常，转到初初始界面

            try
            {
                String strYear = strDate.Substring(0, 4);
                String strMonth = strDate.Substring(4, 2);
                String strDay = strDate.Substring(6, 2);

                curDate = new DateTime(Convert.ToInt32(strYear), Convert.ToInt32(strMonth), Convert.ToInt32(strDay));

            }
            catch (Exception)
            {
                Response.Redirect("./Price.aspx");
            }
        }

        StringBuilder strSQL = new StringBuilder();

        if (curDate.HasValue)
        {
            strSQL.Append(" select i.ItemID, i.ItemName, i.ItemType, i.ItemUnit, @PriceDate PriceDate, ");
        }
        else
        {
            strSQL.Append(" select i.ItemID, i.ItemName, i.ItemType, i.ItemUnit, PriceDate, ");
        }

        strSQL.Append(" ISNULL(p.Price01,0.0) Price01,ISNULL(p.Price02,0.0) Price02, ");
        strSQL.Append(" ISNULL(p.Price03,0.0) Price03,ISNULL(p.Price04,0.0) Price04 ");
        strSQL.Append(" From PriceItems i Left join ");
        strSQL.Append(" (select PriceDate,ItemID,Price01,Price02,Price03,Price04 From PriceRecord ");

        if (curDate.HasValue)
        {
            // 指定时间了
            strSQL.Append(" where PriceDate = @PriceDate AND ItemType=" + ((int)pt).ToString() + ") p ");
        }
        else
        {
            // 未指定时间，使用最大时间
            strSQL.Append(" where PriceDate = (Select Max(PriceDate) From PriceRecord WHERE ItemType=" + ((int)pt).ToString() + ")  AND ItemType=" + ((int)pt).ToString() + ") p ");
        }

        strSQL.Append(" on i.ItemID = p.ItemID where ItemType=" + ((int)pt).ToString());
        strSQL.Append(" and i.Status=" + ((int)model.Status.Normal).ToString() + " order by ItemOrder ");

        DataSet dt = null;

        if (curDate.HasValue)
        {
            dt = OR.DB.SQLHelper.Query(strSQL.ToString(), new SqlParameter("@PriceDate", curDate.Value));
        }
        else
        {
            dt = OR.DB.SQLHelper.Query(strSQL.ToString());
        }


        if (dt != null)
        {
            if (pt == model.PriceType.Price_VegetablePrice)
            {
                this.rptVegetable.DataSource = dt;
                this.rptVegetable.DataBind();
                this.PanelVegetable.Visible = true;

                if (dt.Tables[0].Rows.Count > 0)
                {
                    this.txtDateVegatetable.Text = Convert.ToDateTime(dt.Tables[0].Rows[0]["PriceDate"].ToString()).ToString("yyyy-MM-dd");
                }

                this.txtCurrentProduct.Text = "一周蔬菜价格";
                this.txtMemo.Visible = false;
            }
            else if (pt == model.PriceType.Price_LiangYou)
            {
                this.rptLiangYou.DataSource = dt;
                this.rptLiangYou.DataBind();
                this.PanelLiangYou.Visible = true;
                this.txtLiangYou.Text = curDate.Value.ToString("yyyy-MM-dd");

                if (dt.Tables[0].Rows.Count > 0)
                {
                    this.txtLiangYou.Text = Convert.ToDateTime(dt.Tables[0].Rows[0]["PriceDate"].ToString()).ToString("yyyy-MM-dd");
                }

                this.txtCurrentProduct.Text = "一周粮油价格";
                this.txtMemo.Visible = true;
            }
            else if (pt == model.PriceType.Price_RouDan)
            {
                this.rptRouDan.DataSource = dt;
                this.rptRouDan.DataBind();
                this.PanelRouDan.Visible = true;
                this.txtRouDan.Text = curDate.Value.ToString("yyyy-MM-dd");

                if (dt.Tables[0].Rows.Count > 0)
                {
                    this.txtRouDan.Text = Convert.ToDateTime(dt.Tables[0].Rows[0]["PriceDate"].ToString()).ToString("yyyy-MM-dd");
                }

                this.txtCurrentProduct.Text = "一周肉蛋价格";
                this.txtMemo.Visible = false;
            }

            if (dt.Tables[0].Rows.Count > 0)
            {
                strDate = Convert.ToDateTime(dt.Tables[0].Rows[0]["PriceDate"].ToString()).ToString("yyyy-MM-dd");
            }
        }
    }


    protected String GetWeekList()
    {
        String strSQL = "select max(PriceDate) From PriceRecord where ItemType = @pt" ;  //+ ((int)pt).ToString();
        DateTime cursorDate = DateTime.Parse(OR.DB.SQLHelper.GetSingle(strSQL, new SqlParameter("@pt", (int)pt)).ToString()).AddDays(1);
        
        StringBuilder strWeekDays = new StringBuilder();
        
        // 从后一天开始计数，后面就直接每次减一
        //DateTime cursorDate = DateTime.Now.Date.AddDays(1);
        
        for (int i = 0; i < 7; i++)
        {
            cursorDate = cursorDate.AddDays(-1);

            String strCurDate = cursorDate.ToString("MM/dd/yyyy");

            if (strCurDate.Equals(strDate))
            {
                // 当前是选中的天数，选中之
                strWeekDays.Append("&nbsp;<a href='./Price.aspx?t=" + cursorDate.ToString("yyyyMMdd") + "&PriceType=" + ((int)pt).ToString() + "' style='font-weight:bold'>" + cursorDate.ToString("yyyy-MM-dd") + "</a>&nbsp;|");
            }
            else
            {
                // 未选中的天数
                strWeekDays.Append("&nbsp;<a href='./Price.aspx?t=" + cursorDate.ToString("yyyyMMdd") + "&PriceType=" + ((int)pt).ToString() + "'>" + cursorDate.ToString("yyyy-MM-dd") + "</a>&nbsp;|");
            }
        }

        return strWeekDays.ToString().Substring(0, strWeekDays.Length - 1);
    }
}