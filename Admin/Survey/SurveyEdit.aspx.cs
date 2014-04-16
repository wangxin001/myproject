using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_SurveyEdit : System.Web.UI.Page
{
    String strVoteID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strVoteID = Request.QueryString["VoteID"];

        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "投票调查信息管理";

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(strVoteID))
            {
                // 载入老的数据
                LoadVoteInfo(Convert.ToInt32(strVoteID));
                this.btnQuestion.Visible = true;
            }
            else
            {
                this.btnQuestion.Visible = false;
            }
        }
    }
    protected void btnSaveInfo_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(strVoteID))
        {
            model.VoteInfo vote = new model.VoteInfo();

            vote.VoteName = this.txtVoteName.Text;
            vote.VoteDescription = this.txtVoteDescription.Text;

            vote.StartDateTime = Convert.ToDateTime(this.txtStartDate.Text);
            vote.EndDateTime = Convert.ToDateTime(this.txtEndDate.Text);

            vote.Status = (int)model.VoteStatus.NotStart;
            vote.ResultPublic = (int)(this.chkResultPublic.Checked ? model.Status.Normal : model.Status.Forbidden);

            vote.Created = DateTime.Now;
            vote.UserGUID = Util.GetLoginUserInfo().UserGUID;
            vote.UserName = Util.GetLoginUserInfo().UserName;

            vote = OR.Control.DAL.Add<model.VoteInfo>(vote, true);

            Log.info("添加新的投票：" + vote.ToString(true));

            Response.Redirect("./SurveyEdit.aspx?VoteID=" + vote.VoteID.ToString());
            Response.End();
        }
        else
        {
            model.VoteInfo vote = OR.Control.DAL.GetModelByKey<model.VoteInfo>(Convert.ToInt32(strVoteID));

            vote.VoteName = this.txtVoteName.Text;
            vote.VoteDescription = this.txtVoteDescription.Text;

            vote.StartDateTime = Convert.ToDateTime(this.txtStartDate.Text);
            vote.EndDateTime = Convert.ToDateTime(this.txtEndDate.Text);
            vote.ResultPublic = (int)(this.chkResultPublic.Checked ? model.Status.Normal : model.Status.Forbidden);

            OR.Control.DAL.Update<model.VoteInfo>(vote);

            Log.info("更新投票信息：" + vote.ToString(true));
        }
    }

    protected void LoadVoteInfo(int voteID)
    {
        model.VoteInfo vote = OR.Control.DAL.GetModelByKey<model.VoteInfo>(voteID);

        this.txtVoteName.Text = vote.VoteName;
        this.txtVoteDescription.Text = vote.VoteDescription;
        this.txtStartDate.Text = vote.StartDateTime.ToString("yyyy-MM-dd");
        this.txtEndDate.Text = vote.EndDateTime.ToString("yyyy-MM-dd");
        this.chkResultPublic.Checked = (vote.ResultPublic == (int)model.Status.Normal ? true : false);
    }


    protected void btnQuestion_Click(object sender, EventArgs e)
    {
        Response.Redirect("./PaperInfo.aspx?VoteID=" + strVoteID);
        Response.End();
    }
}