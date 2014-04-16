using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "调查管理";

        if (!IsPostBack)
        {
            LoadSurveyInfo();
        }
    }

    protected void LoadSurveyInfo()
    {
        List<model.VoteInfo> votes = OR.Control.DAL.GetModelList<model.VoteInfo>("1=1 Order By Created Desc");

        this.GridView1.DataSource = votes;
        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.VoteInfo vote = (model.VoteInfo)e.Row.DataItem;

            LinkButton btnModify = e.Row.FindControl("btnModify") as LinkButton;
            LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
            LinkButton btnStart = e.Row.FindControl("btnStart") as LinkButton;
            LinkButton btnStop = e.Row.FindControl("btnStop") as LinkButton;
            LinkButton btnInfo = e.Row.FindControl("btnInfo") as LinkButton;

            btnModify.CommandName = "Modify";
            btnDelete.CommandName = "Del";
            btnStart.CommandName = "Start";
            btnStop.CommandName = "Stop";
            btnInfo.CommandName = "Info";

            btnModify.CommandArgument = vote.VoteID.ToString();
            btnDelete.CommandArgument = vote.VoteID.ToString();
            btnStart.CommandArgument = vote.VoteID.ToString();
            btnStop.CommandArgument = vote.VoteID.ToString();
            btnInfo.CommandArgument = vote.VoteID.ToString();

            if (vote.Status == (int)model.VoteStatus.NotStart)
            {
                btnStop.Visible = false;
                btnInfo.Visible = false;
            }
            else if (vote.Status == (int)model.VoteStatus.Voting)
            {
                btnModify.Visible = false;
                btnStart.Visible = false;
            }
            else if (vote.Status == (int)model.VoteStatus.VoteEnd)
            {
                btnModify.Visible = false;
                btnStart.Visible = false;
                btnStop.Visible = false;
            }

            if (vote.ResultPublic == (int)model.Status.Normal)
            {
                e.Row.Cells[2].Text = "公开";
            }
            else
            {
                e.Row.Cells[2].Text = "不公开";
            }

            if (vote.Status == (int)model.VoteStatus.NotStart)
            {
                e.Row.Cells[3].Text = "未开始";
            }
            else if (vote.Status == (int)model.VoteStatus.Voting)
            {
                e.Row.Cells[3].Text = "调查中";
            }
            else if (vote.Status == (int)model.VoteStatus.VoteEnd)
            {
                e.Row.Cells[3].Text = "调查结束";
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            // 修改，导航到修改页面
            Response.Redirect("./SurveyEdit.aspx?VoteID=" + e.CommandArgument);
            Response.End();
        }
        else if (e.CommandName == "Del")
        {
            model.VoteInfo vote = OR.Control.DAL.GetModelByKey<model.VoteInfo>(Convert.ToInt32(e.CommandArgument));

            // 删除，将此调查的所有信息全部清理干净
            String strSQL = " Delete From VoteResult where VoteID=@VoteID ";
            strSQL += " Delete From VoteOption where VoteID=@VoteID ";
            strSQL += " Delete From VoteQuestion where VoteID=@VoteID ";
            strSQL += " Delete From VoteInfo where VoteID=@VoteID ";

            OR.DB.SQLHelper.ExecuteSql(strSQL,
                new System.Data.SqlClient.SqlParameter("@VoteID", vote.VoteID));

            Log.info("删除调查信息：" + vote.ToString(true));

        }
        else if (e.CommandName == "Start")
        {
            // 启动调查，修改状态即可
            model.VoteInfo vote = OR.Control.DAL.GetModelByKey<model.VoteInfo>(Convert.ToInt32(e.CommandArgument));
            vote.Status = (int)model.VoteStatus.Voting;
            vote.StartDateTime = DateTime.Now;

            OR.Control.DAL.Update<model.VoteInfo>(vote);

            Log.info("启动调查：" + vote.ToString(true));
        }
        else if (e.CommandName == "Stop")
        {
            // 停止调查，修改状态即可
            model.VoteInfo vote = OR.Control.DAL.GetModelByKey<model.VoteInfo>(Convert.ToInt32(e.CommandArgument));
            vote.Status = (int)model.VoteStatus.VoteEnd;
            vote.StartDateTime = DateTime.Now;

            OR.Control.DAL.Update<model.VoteInfo>(vote);

            Log.info("启动调查：" + vote.ToString(true));
        }
        else if (e.CommandName == "Info")
        {
            //
            Response.Redirect("./SurveyResult.aspx?VoteID=" + e.CommandArgument);
            Response.End();
        }

        LoadSurveyInfo();
    }
}