using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class policy_index : System.Web.UI.Page
{
    String strPricePolicy = System.Configuration.ConfigurationManager.AppSettings["PricePolicy"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetInfoData();
        }
    }

    private void GetInfoData()
    {
        String strSQL = "Select TopicID, TopicTitle,PublishDate,ShowDate From InfoTopicInfo WHERE BoardID=" + strPricePolicy;
        strSQL += " AND Status=" + ((int)model.Status.Normal).ToString() + " ORDER By PublishDate desc,TopicID desc";

        String strCurPage = Request.QueryString["page"];
        strCurPage = strCurPage ?? "1";

        int _RowCount = 0;
        int _PageCount = 0;

        DataTable infoData = OR.DB.SQLHelper.Query(strSQL, Convert.ToInt32(strCurPage) - 1, this.AspNetPager.PageSize, ref _RowCount, ref _PageCount);

        this.AspNetPager.RecordCount = _RowCount;
        this.AspNetPager.CurrentPageIndex = Convert.ToInt32(strCurPage);

        this.rpInfo.DataSource = infoData;
        this.rpInfo.DataBind();
    }
}