using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class Survey_survey : System.Web.UI.Page
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

    DataTable dtOption = null;

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


            strSQL = "Select OptionID,QuestionID,OptionContent,OptionOrder From VoteOption where VoteID=@VoteID Order By OptionOrder ";
            dtOption = OR.DB.SQLHelper.Query(strSQL,
                new SqlParameter("@VoteID", Convert.ToInt32(strVoteID))).Tables[0];

            strSQL = "Select QuestionID,QuestionContent,QuestionType,OptionsCount From VoteQuestion where Status=1 AND VoteID=@VoteID Order By DisplayOrder";
            DataTable dtQuestion = OR.DB.SQLHelper.Query(strSQL,
                new SqlParameter("@VoteID", Convert.ToInt32(strVoteID))).Tables[0];

            rpQuestion.DataSource = dtQuestion;
            rpQuestion.DataBind();

        }
        else
        {
            strVoteID = "";
            strVoteName = "当前无调查";
            strSurveyDesc = "";
            this.btnSubmit.Visible = false;
            this.reset.Visible = false;
        }
    }

    protected String GetQuestionOptions(int QuestionID, int QuestionType)
    {
        if (dtOption == null)
        {
            return string.Empty;
        }

        DataRow[] drOptions = dtOption.Select("QuestionID=" + QuestionID.ToString(), "OptionOrder");

        StringBuilder strContent = new StringBuilder();

        for (int i = 0; i < drOptions.Length; i++)
        {
            strContent.Append(" <input ");

            switch ((model.QuestionType)QuestionType)
            {
                case model.QuestionType.SingleSelect:
                    strContent.Append("type='radio'");
                    break;
                case model.QuestionType.MultiSelect:
                    strContent.Append("type='checkbox'");
                    break;
                default:
                    continue;
            }

            strContent.Append(" id='O_" + drOptions[i]["OptionID"].ToString() + "' ");
            strContent.Append(" name='Q_" + QuestionID + "' ");
            strContent.Append(" value='" + drOptions[i]["OptionID"].ToString() + "' />");

            strContent.Append("&nbsp;" + drOptions[i]["OptionContent"].ToString() + "&nbsp;&nbsp;&nbsp;");
        }

        return strContent.ToString();
    }

}