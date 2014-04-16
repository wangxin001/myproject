using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_PaperInfo : System.Web.UI.Page
{
    protected int voteID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        voteID = Convert.ToInt32(Request.QueryString["VoteID"]);

        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "问卷管理";

        if (!IsPostBack)
        {
            LoadQuestion();
        }
    }

    System.Data.DataTable dtOptions = null;

    protected void LoadQuestion()
    {
        dtOptions = OR.Control.DAL.GetDataTable<model.VoteOption>("VoteID=" + voteID.ToString());

        List<model.VoteQuestion> questions = OR.Control.DAL.GetModelList<model.VoteQuestion>("VoteID=" + voteID.ToString() + " Order By DisplayOrder");

        this.Repeater1.DataSource = questions;
        this.Repeater1.DataBind();
    }

    protected String GetQuestionType(int QuestionType)
    {
        if ((model.QuestionType)QuestionType == model.QuestionType.SingleSelect)
        {
            return "单选题";
        }
        else
        {
            return "多选题";
        }
    }

    protected String GetQuestionOption(int QuestionID, model.QuestionType questionType)
    {
        System.Data.DataRow[] drOptions = dtOptions.Select("QuestionID=" + QuestionID.ToString(), "OptionOrder, OptionID");

        System.Text.StringBuilder strOptions = new System.Text.StringBuilder();

        for (int i = 0; i < drOptions.Length; i++)
        {
            strOptions.Append("<div>");
            strOptions.Append("<input type='" + (questionType == model.QuestionType.SingleSelect ? "radio" : "checkbox") + "' ");
            strOptions.Append("id='option" + drOptions[i]["OptionID"].ToString() + "' ");
            strOptions.Append("name='Question" + QuestionID.ToString() + "' ");
            strOptions.Append("value='" + drOptions[i]["OptionID"].ToString() + "' /> ");
            strOptions.Append(drOptions[i]["OptionContent"].ToString());
            strOptions.Append("</div>");
        }

        return strOptions.ToString();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("./SurveyEdit.aspx?VoteID=" + voteID.ToString());
        Response.End();
    }
}