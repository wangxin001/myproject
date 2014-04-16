using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InfoPub_PictureList : System.Web.UI.Page
{
    protected String strBoardID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strBoardID = Request.QueryString["BoardID"];

        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "焦点图片管理";

        if (!IsPostBack)
        {
            model.InfoBoardInfo board = OR.Control.DAL.GetModelByKey<model.InfoBoardInfo>(Convert.ToInt32(strBoardID));
            this.labelBoardInfo.Text = "新闻管理 -> " + board.BoardTitle + "(" + ((model.BoardType)board.BoardType).ToString() + ")";

            this.AspNetPager.CurrentPageIndex = 1;
            this.AspNetPager.PageSize = OR.Model.Util.GetPageSize();

            LoadTopicList();
        }
    }

    protected void LoadTopicList()
    {
        int _RowCount = 0;

        List<model.InfoTopicInfo> topics = OR.Control.DAL.GetModelList<model.InfoTopicInfo>("BoardID=" + strBoardID + " Order By TopicOrder, Created Desc", this.AspNetPager.CurrentPageIndex - 1, ref _RowCount);

        if (topics.Count == 0 && _RowCount > 0 && this.AspNetPager.CurrentPageIndex > 0)
        {
            /**
             * 这种情况是比如当前页面的最后一条删除后，重新加载页面，导致当前页面内容为空的情况
             */
            this.AspNetPager.CurrentPageIndex--;
            LoadTopicList();
            return;
        }

        this.AspNetPager.RecordCount = _RowCount;

        this.GridView1.DataSource = topics;
        this.GridView1.DataBind();
    }

    protected void AspNetPager_PageChanged(object sender, EventArgs e)
    {
        LoadTopicList();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.InfoTopicInfo topic = (model.InfoTopicInfo)e.Row.DataItem;

            LinkButton btnMod = e.Row.FindControl("btnDel") as LinkButton;
            LinkButton btnHidden = e.Row.FindControl("btnHidden") as LinkButton;
            LinkButton btnPublic = e.Row.FindControl("btnPublic") as LinkButton;
            HyperLink hyMod = e.Row.FindControl("hyMod") as HyperLink;

            btnMod.CommandName = "Del";
            btnHidden.CommandName = "Hidden";
            btnPublic.CommandName = "Public";

            btnMod.CommandArgument = topic.TopicID.ToString();
            btnHidden.CommandArgument = topic.TopicID.ToString();
            btnPublic.CommandArgument = topic.TopicID.ToString();

            hyMod.NavigateUrl = "./PictureEdit.aspx?BoardID=" + strBoardID + "&TopicID=" + topic.TopicID.ToString();

            if ((model.Status)topic.Status == model.Status.Forbidden)
            {
                e.Row.ForeColor = System.Drawing.Color.FromArgb(168, 168, 168);
                btnHidden.Visible = false;
            }
            else
            {
                btnPublic.Visible = false;
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        model.InfoTopicInfo topic = OR.Control.DAL.GetModelByKey<model.InfoTopicInfo>(Convert.ToInt32(e.CommandArgument));

        if (e.CommandName == "Del")
        {
            OR.Control.DAL.Delete<model.InfoTopicInfo>(topic);
            Log.info("删除图片：" + topic.ToString(true));
        }
        else if (e.CommandName == "Hidden")
        {
            topic.Status = (int)model.Status.Forbidden;
            OR.Control.DAL.Update<model.InfoTopicInfo>(topic);
            Log.info("隐藏图片：" + topic.TopicID.ToString() + " -> " + topic.TopicTitle);
        }
        else if (e.CommandName == "Public")
        {
            topic.Status = (int)model.Status.Normal;
            OR.Control.DAL.Update<model.InfoTopicInfo>(topic);
            Log.info("公开图片：" + topic.TopicID.ToString() + " -> " + topic.TopicTitle);
        }

        LoadTopicList();
    }
}