using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class vegetables_index : System.Web.UI.Page
{
    protected String strDate = string.Empty;
    DateTime? curDate = null;

    /// <summary>
    /// 该页面查询今日菜价。有参数时，以参数指定的日期为准进行查询，否则返回默认页面，看当前日期的内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        strDate = Request.QueryString["t"];

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
                Response.Redirect("./index.aspx");
            }

        }

        if (!IsPostBack)
        {
            StringBuilder strSQL = new StringBuilder();

            if (curDate.HasValue)
            {
                strSQL.Append(" select i.ItemID, i.ItemName, i.ItemType, @PriceDate PriceDate, ");
            }
            else
            {
                strSQL.Append(" select i.ItemID, i.ItemName, i.ItemType, PriceDate, ");
            }

            strSQL.Append(" ISNULL(p.Price01,0) Price01,ISNULL(p.Price02,0) Price02, ");
            strSQL.Append(" ISNULL(p.Price03,0) Price03,ISNULL(p.Price04,0) Price04 ");
            strSQL.Append(" From PriceItems i Left join ");
            strSQL.Append(" (select PriceDate,ItemID,Price01,Price02,Price03,Price04 From PriceRecord ");

            if (curDate.HasValue)
            {
                // 指定时间了
                strSQL.Append(" where PriceDate = @PriceDate AND ItemType=1) p ");
            }
            else
            {
                // 未指定时间，使用最大时间
                strSQL.Append(" where PriceDate = (Select Max(PriceDate) From PriceRecord WHERE ItemType=1)  AND ItemType=1) p ");
            }

            strSQL.Append(" on i.ItemID = p.ItemID where ItemType=" + ((int)model.PriceType.Price_VegetablePrice).ToString() + " ");
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
                this.Repeater1.DataSource = dt;
                this.Repeater1.DataBind();

                if (dt.Tables[0].Rows.Count > 0)
                {
                    strDate = Convert.ToDateTime(dt.Tables[0].Rows[0]["PriceDate"].ToString()).ToString("yyyy-MM-dd");
                }
            }
        }


        if (String.IsNullOrEmpty(strDate))
        {
            Response.Redirect("./index.aspx");
        }
    }

    protected String GetWeekList()
    {
        StringBuilder strWeekDays = new StringBuilder();

        // 从后一天开始计数，后面就直接每次减一
        DateTime cursorDate = DateTime.Now.Date.AddDays(1); ;

        for (int i = 0; i < 7; i++)
        {
            cursorDate = cursorDate.AddDays(-1);

            String strCurDate = cursorDate.ToString("MM/dd/yyyy");

            if (strCurDate.Equals(strDate))
            {
                // 当前是选中的天数，选中之
                strWeekDays.Append("&nbsp;<a href='./index_" + cursorDate.ToString("yyyyMMdd") + ".aspx' style='font-weight:bold'>" + cursorDate.ToString("yyyy-MM-dd") + "</a>&nbsp;|");
            }
            else
            {
                // 未选中的天数
                strWeekDays.Append("&nbsp;<a href='./index_" + cursorDate.ToString("yyyyMMdd") + ".aspx'>" + cursorDate.ToString("yyyy-MM-dd") + "</a>&nbsp;|");
            }
        }

        return strWeekDays.ToString().Substring(0, strWeekDays.Length - 1);
    }
}