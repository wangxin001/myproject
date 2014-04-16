using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class index : System.Web.UI.Page
{
    private DataTable infoData = null;

    String strJobActive = System.Configuration.ConfigurationManager.AppSettings["JobActive"].ToString();
    String strPriceInfo = System.Configuration.ConfigurationManager.AppSettings["PriceInfo"].ToString();
    String strPricePolicy = System.Configuration.ConfigurationManager.AppSettings["PricePolicy"].ToString();
    String strFocusPicture = System.Configuration.ConfigurationManager.AppSettings["FocusPicture"].ToString();
    String strBanner = System.Configuration.ConfigurationManager.AppSettings["Banner"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 加载新闻类内容
            LoadInfoData();

            LoadPriceList();

            // 加载下面的banner
            LoadBanner();

            // 加载调查信息
            LoadSurvey();
        }
    }

    /*
     * 当前对于首页栏目的处理，暂时先定死，按照现在的栏目编号，写在程序里，
     * 或者写在配置文件里，与数据库打交道的地方，就连接一次，避免数据库的多次连接
     * 一次将所需的几个栏目数据全部取出来，然后后面在根据栏目进行拆分
     * 
     * 问题：怎么保证每个栏目都只取出所需条数的内容？
     * union？
     * 相当于多次查询，不是很好，暂时先这样做吧，回头再优化。
     */

    protected void LoadInfoData()
    {
        String strSQL = " select * From ";
        strSQL += " (SELECT ROW_NUMBER() OVER(PARTITION BY BoardID ORDER BY PublishDate desc,TopicID desc) AS rnk, ";
        strSQL += " BoardID, TopicID, TopicTitle,PublishDate,ShowDate, ";
        strSQL += " case when BoardID in (" + strFocusPicture + ", " + strBanner + ", " + strPriceInfo + ") then TopicContent else '' end TopicContent, ";
        strSQL += " case when BoardID in (" + strFocusPicture + ", " + strBanner + ", " + strPriceInfo + ") then TopicLink else '' end TopicLink, TopicOrder ";
        strSQL += " FROM InfoTopicInfo where Status=" + ((int)model.Status.Normal).ToString() + " ) t ";
        strSQL += " where t.rnk < case when BoardID = @Banner then 100 when BoardID=@PriceInfo then 2 when BoardID = @PricePolicy then 14 when BoardID = @FocusPicture then 6 else 9 end ";
        strSQL += " ORDER By BoardID, rnk";

        infoData = OR.DB.SQLHelper.Query(strSQL,
            new SqlParameter("@PriceInfo", Convert.ToInt32(strPriceInfo)),
            new SqlParameter("@PricePolicy", Convert.ToInt32(strPricePolicy)),
            new SqlParameter("@FocusPicture", Convert.ToInt32(strFocusPicture)),
            new SqlParameter("@Banner", Convert.ToInt32(strBanner))).Tables[0];

        LoadFocusImage();
        LoadJobActive();
        LoadPriceInfo();
        LoadPricePolicy();
        LoadBanner();
    }
    /// <summary>
    /// 实用价格查询
    /// </summary>
    protected void LoadPriceList()
    {
        //按BoardID 分组 取出BoardID是33, 34, 35, 36, 37, 38的数据并为每一条分组记录返回一个数字
        String strSQL = @"
SELECT *
FROM (
	SELECT ROW_NUMBER() OVER (
			PARTITION BY BoardID ORDER BY TopicOrder DESC, PublishDate DESC, TopicID DESC
			) AS rnk, BoardID, TopicID, TopicTitle, PublishDate, TopicContent, TopicLink, TopicOrder, TopicType
	FROM InfoTopicInfo
	WHERE STATUS = 1
		AND BoardID IN (33, 34, 35, 36, 37, 38)
	) t
WHERE t.rnk < 4
ORDER BY BoardID, rnk";

        DataTable dt = OR.DB.SQLHelper.Query(strSQL).Tables[0];

        int[] boardIDs = new int[] { 33, 34, 35, 36, 37, 38 };

        for (int i = 0; i < boardIDs.Length; i++)
        {
            int b = boardIDs[i];

            DataRow[] drArray = dt.Select("BoardID=" + b.ToString());

            Repeater r = this.FindControl("Repeater" + b.ToString()) as Repeater;

            DataTable dt2 = dt.Clone();
            for (int j = 0; j < drArray.Length;j++)
            {
                dt2.ImportRow(drArray[j]);
            }

            r.DataSource = dt2;
            r.DataBind();
        }
    }

    /// <summary>
    /// 焦点图片
    /// </summary>
    protected void LoadFocusImage()
    {
        // 栏目号暂时固定为 1
        DataRow[] dr = infoData.Select(" BoardID=" + strFocusPicture);

        DataTable dt = infoData.Clone();
        for (int i = 0; i < dr.Length; i++)
        {
            dt.ImportRow(dr[i]);
        }

        this.rpFocusImage.DataSource = dt;
        this.rpFocusImage.DataBind();
    }

    /// <summary>
    /// 工作动态
    /// </summary>
    protected void LoadJobActive()
    {
        // 栏目号暂时固定为 1
        DataRow[] dr = infoData.Select(" BoardID=" + strJobActive);

        DataTable dt = infoData.Clone();
        for (int i = 0; i < dr.Length; i++)
        {
            dt.ImportRow(dr[i]);
        }

        this.rpJobActive.DataSource = dt;
        this.rpJobActive.DataBind();
    }

    /// <summary>
    /// 价格咨询
    /// </summary>
    protected void LoadPriceInfo()
    {
        // 栏目号暂时固定为 2
        DataRow[] dr = infoData.Select(" BoardID=" + strPriceInfo);

        if (dr.Length > 0)
        {
            this.txtTitle.Text = dr[0]["TopicTitle"].ToString();
            this.txtContent.Text = "&nbsp;&nbsp;&nbsp;" + Util.GetContent(dr[0]["TopicContent"].ToString(), 155);
            this.txtContent.NavigateUrl = "./news/detail_" + dr[0]["TopicID"].ToString() + ".aspx";
            this.txtContent.Target = "_blank";
        }
    }

    /// <summary>
    /// 价格政策
    /// </summary>
    protected void LoadPricePolicy()
    {
        // 栏目号暂时固定为 2
        DataRow[] dr = infoData.Select(" BoardID=" + strPricePolicy);

        DataTable dt = infoData.Clone();
        for (int i = 0; i < dr.Length; i++)
        {
            dt.ImportRow(dr[i]);
        }

        this.rpPricePolicy.DataSource = dt;
        this.rpPricePolicy.DataBind();
    }


    protected String strSurveyDesc = string.Empty;

    protected void LoadSurvey()
    {
        // 取得第一条刚发起的调查。如果没有，则后面都空着就行
        String strSQL = "Select top 1 VoteDescription From VoteInfo WHERE Status=" + ((int)model.VoteStatus.Voting).ToString();
        strSQL += " Order By StartDateTime Desc ";

        DataTable survey = OR.DB.SQLHelper.Query(strSQL).Tables[0];

        if (survey.Rows.Count > 0)
        {
            strSurveyDesc = survey.Rows[0][0].ToString();

            if (strSurveyDesc.Length > 33)
            {
                strSurveyDesc = strSurveyDesc.Substring(0, 31) + "...";
            }

        }
        else
        {
            strSurveyDesc = "当前无调查";
        }
    }

    protected void LoadBanner()
    {
        DataRow[] dr = infoData.Select("BoardID=" + strBanner, "TopicOrder");

        DataTable dt = infoData.Clone();
        for (int i = 0; i < dr.Length; i++)
        {
            dt.ImportRow(dr[i]);
        }

        this.rpBanner.DataSource = dt;
        this.rpBanner.DataBind();
    }
}