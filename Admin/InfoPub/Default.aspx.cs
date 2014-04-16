using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InfoPub_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
            master.PageTitle = "新闻栏目管理";

            this.labelBoardInfo.Text = "新闻管理 -> 栏目管理";

            this.AspNetPager.CurrentPageIndex = 1;
            this.AspNetPager.PageSize = OR.Model.Util.GetPageSize();

            LoadBoardList();
        }
    }

    protected void LoadBoardList()
    {
        int _RowCount = 0;

        List<model.InfoBoardInfo> boards = OR.Control.DAL.GetModelList<model.InfoBoardInfo>("1=1 Order By Created", this.AspNetPager.CurrentPageIndex - 1, ref _RowCount);

        if (boards.Count == 0 && _RowCount > 0 && this.AspNetPager.CurrentPageIndex > 0)
        {
            /**
             * 这种情况是比如当前页面的最后一条删除后，重新加载页面，导致当前页面内容为空的情况
             */
            this.AspNetPager.CurrentPageIndex--;
            LoadBoardList();
            return;
        }

        this.AspNetPager.RecordCount = _RowCount;

        this.GridView1.DataSource = boards;
        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.InfoBoardInfo board = (model.InfoBoardInfo)e.Row.DataItem;
            switch (board.BoardType)
            {
                case (int)model.BoardType.InfoPub:
                    e.Row.Cells[4].Text = "新闻发布";
                    break;
                case (int)model.BoardType.Picture:
                    e.Row.Cells[4].Text = "图片新闻";
                    break;
                case (int)model.BoardType.QAndA:
                    e.Row.Cells[4].Text = "问题回答";
                    break;
                case (int)model.BoardType.File:
                    e.Row.Cells[4].Text = "文件下载";
                    break;
                default:
                    break;
            }

            if ((model.Status)board.Status == model.Status.Forbidden)
            {
                e.Row.ForeColor = System.Drawing.Color.FromArgb(168, 168, 168);
            }
        }
    }
    protected void AspNetPager_PageChanged(object sender, EventArgs e)
    {
        LoadBoardList();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        LoadBoardList();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        String[] sid = Request.Form["BoardItem"].Split(',');
        for (int i = 0; i < sid.Length; i++)
        {
            OR.Control.DAL.Delete<model.InfoBoardInfo>(Convert.ToInt32(sid[i]));
        }
        LoadBoardList();
    }
}