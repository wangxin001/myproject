using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class Survey_save : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 防盗链处理

        String refUrl = Request.ServerVariables["HTTP_REFERER"];

        if (String.IsNullOrEmpty(refUrl))
        {
            this.txtMessage.Text = "对不起，您没有查看此页面的权限!";
            return;
        }

        String strApp = Request.ApplicationPath.ToString().Trim().ToLower();

        String fromUrl = "http://" + Request.ServerVariables["SERVER_NAME"].ToString().Trim() + strApp + (strApp.EndsWith("/") ? "" : "/") + "survey/survey.aspx";

        if (!refUrl.Equals(fromUrl.ToLower()))
        {
            this.txtMessage.Text = "对不起， 您没有查看此页面的权限!";
            return;
        }

        #endregion

        String strVoteID = Request.Form["surveyID"];

        #region 判断当前调查是否已停止

        // 取得调查信息，看看有没有结束停止。如果没有，则后面都空着就行
        String strSQL = "Select VoteID,VoteName,VoteDescription,Status,ResultPublic From VoteInfo WHERE VoteID=@VoteID";

        DataTable survey = OR.DB.SQLHelper.Query(strSQL,
            new SqlParameter("@VoteID", Convert.ToInt32(strVoteID))).Tables[0];

        // 该调查已被删除
        if (survey.Rows.Count == 0)
        {
            this.txtMessage.Text = "对不起，该调查已停止。";
            return;
        }

        // 该调查已停止
        if (survey.Rows[0]["Status"].ToString() != ((int)model.VoteStatus.Voting).ToString())
        {
            this.txtMessage.Text = "对不起，该调查已停止。";
            return;
        }
        #endregion

        #region 一天过期，一天之内不能重复投票

        HttpCookie voted = Request.Cookies.Get("VoteID");

        if (voted != null)
        {
            if (voted.Value.Equals(strVoteID))
            {
                this.txtMessage.Text = "对不起，你已经投过票了。谢谢您的参与。";
                return;
            }
        }
        // 记录cookie，一天过期，一天之内不能重复投票
        HttpCookie cookie = new HttpCookie("VoteID", strVoteID);
        cookie.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie);

        #endregion

        String remoteAddr = Request.ServerVariables["Remote_Addr"];
        String strSessionGUID = Guid.NewGuid().ToString();

        // 取得该调查的问题列表，逐个进行循环，获取答案
        strSQL = "Select QuestionID,QuestionContent,QuestionType,OptionsCount From VoteQuestion where Status=1 AND VoteID=@VoteID Order By DisplayOrder";
        DataTable dtQuestion = OR.DB.SQLHelper.Query(strSQL,
            new SqlParameter("@VoteID", Convert.ToInt32(strVoteID))).Tables[0];

        // 逐个问题进行循环

        // 先把保存用的sql准备好，下面直接循环使用
        strSQL = "Insert into VoteResult (VoteID, QuestionID, OptionID, SessionGUID, RemoteAddr, Created) Values ";
        strSQL += " (@VoteID, @QuestionID, @OptionID, @SessionGUID, @RemoteAddr, GetDate() ) ";

        for (int i = 0; i < dtQuestion.Rows.Count; i++)
        {
            int questionid = Convert.ToInt32(dtQuestion.Rows[i]["QuestionID"].ToString());

            String strAnswer = Request.Form["Q_" + questionid.ToString()];

            if (String.IsNullOrEmpty(strAnswer))
            {
                // 没有选问题
                continue;
            }

            // 取得每一个Option的Value
            String[] strOptionID = strAnswer.Split(',');

            for (int j = 0; j < strOptionID.Length; j++)
            {
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@VoteID", Convert.ToInt32(strVoteID)),
                    new SqlParameter("@QuestionID", questionid),
                    new SqlParameter("@OptionID", Convert.ToInt32(strOptionID[j])),
                    new SqlParameter("@SessionGUID", strSessionGUID),
                    new SqlParameter("@RemoteAddr", remoteAddr)};

                OR.DB.SQLHelper.ExecuteSql(strSQL, param);
            }
        }

        if (Convert.ToInt32(survey.Rows[0]["ResultPublic"].ToString()) == (int)model.Status.Normal)
        {
            Response.Redirect("./result.aspx");
        }
        else
        {
            this.txtMessage.Text = "您的投票已成功，感谢您的参与！";
        }
    }
}