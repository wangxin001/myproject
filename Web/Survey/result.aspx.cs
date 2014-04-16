using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class Survey_result : System.Web.UI.Page
{

    protected String strSurveyDesc = string.Empty;
    protected String strVoteID = string.Empty;
    protected String strVoteName = string.Empty;

    /*
     * 鉴于现在的页面处理方式，暂时系统只将最新的调查列出来，供用户进行投票
     * 以后根据需要，进行投票内容的扩展，考虑从左面菜单中加入历史投票的选择
     */

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSurveyInfo();
        }
    }

    DataSet ds = null;

    /// <summary>
    /// 列出最新的投票内容
    /// </summary>
    protected void LoadSurveyInfo()
    {
        // 取得第一条刚发起的调查。如果没有，则后面都空着就行
        String strSQL = "Select top 1 VoteID,VoteName,VoteDescription From VoteInfo WHERE Status=" + ((int)model.VoteStatus.Voting).ToString();
        strSQL += " Order By StartDateTime Desc ";

        DataTable survey = OR.DB.SQLHelper.Query(strSQL).Tables[0];

        if (survey.Rows.Count > 0)
        {
            strVoteID = survey.Rows[0]["VoteID"].ToString();
            strVoteName = survey.Rows[0]["VoteName"].ToString();
            strSurveyDesc = survey.Rows[0]["VoteDescription"].ToString();

            // 取得本次投票的问题列表，用于显示结果
            strSQL = " Select QuestionID,QuestionContent,QuestionType,OptionsCount From VoteQuestion where Status=1 AND VoteID=@VoteID Order By DisplayOrder ";

            // 取得所有的可选项内容
            strSQL += " Select OptionID,QuestionID,OptionContent,OptionOrder From VoteOption where VoteID=@VoteID Order By OptionOrder ";

            // 取得本次投票所有的投票结果数量
            strSQL += " Select QuestionID,OptionID,COUNT(*) CNT From VoteResult where VoteID=@VoteID Group By QuestionID,OptionID ";

            // 取得投票的人数总数
            strSQL += " Select COUNT(*) CNT From (Select distinct SessionGUID From VoteResult where VoteID=@VoteID) t ";

            ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@VoteID", Convert.ToInt32(strVoteID)));

            rpQuestion.DataSource = ds.Tables[0]; ;
            rpQuestion.DataBind();

        }
        else
        {
            strVoteID = "";
            strVoteName = "当前无调查";
            strSurveyDesc = "";
        }
    }

    protected String GetQuestionOptions(int QuestionID)
    {
        if (ds == null || ds.Tables.Count != 4 || ds.Tables[1] == null)
        {
            return string.Empty;
        }

        StringBuilder strContent = new StringBuilder();

        decimal TotalCount = Convert.ToInt32(ds.Tables[3].Rows[0][0].ToString());

        DataRow[] drOptions = ds.Tables[1].Select("QuestionID=" + QuestionID.ToString(), "OptionOrder");

        for (int i = 0; i < drOptions.Length; i++)
        {
            decimal CurrentOptionCount = 0.0M;

            DataRow[] drResult = ds.Tables[2].Select("QuestionID=" + QuestionID.ToString() + " AND OptionID=" + drOptions[i]["OptionID"].ToString());
            if (drResult.Length > 0)
            {
                CurrentOptionCount = Convert.ToInt32(drResult[0]["CNT"].ToString());
            }

            String pre = ((char)(i + 65)).ToString();
            String preColor = ((char)(i % 5 + 65)).ToString();

            strContent.Append("<div class='ax_all'>");
            strContent.Append("<span class='a_ale'>" + pre.ToUpper() + " " + drOptions[i]["OptionContent"].ToString() + "：</span>");
            strContent.Append("<span class='he_all'>");
            strContent.Append("<span class='he_01'><img src='../images/" + preColor + "_le.gif' width='2' height='10' /></span>");
            strContent.Append("<span class='he_02'><img src='../images/" + preColor + "_mm.gif' width='" + String.Format("{0:0}", (TotalCount == 0 ? 0 : CurrentOptionCount / TotalCount) * 156) + "' height='10' /></span>");
            strContent.Append("<span class='he_03'><img src='../images/" + preColor + "_ri.gif' width='2' height='10' /></span>");
            strContent.Append("</span>");
            strContent.Append("<span class='a_ari'>" + String.Format("{1:0.0%}", CurrentOptionCount, (TotalCount == 0 ? 0 : CurrentOptionCount / TotalCount)) + "</span>");
            strContent.Append("</div><div class='clear'></div>");
        }

        return strContent.ToString();
    }
}