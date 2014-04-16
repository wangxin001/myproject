using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InfoPub_BoardEdit : System.Web.UI.Page
{
    String strBoardID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strBoardID = Request.QueryString["BoardID"];

        if (!IsPostBack)
        {

            this.ddlBoardType.Items.Add(new ListItem("新闻发布", ((int)model.BoardType.InfoPub).ToString()));
            this.ddlBoardType.Items.Add(new ListItem("图片新闻", ((int)model.BoardType.Picture).ToString()));
            this.ddlBoardType.Items.Add(new ListItem("问题回答", ((int)model.BoardType.QAndA).ToString()));
            this.ddlBoardType.Items.Add(new ListItem("文件下载", ((int)model.BoardType.File).ToString()));

            if (!String.IsNullOrEmpty(strBoardID))
            {
                LoadBoardInfo();
            }
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        model.InfoBoardInfo board = null;
        if (!String.IsNullOrEmpty(strBoardID))
        {
            board = OR.Control.DAL.GetModelByKey<model.InfoBoardInfo>(Convert.ToInt32(strBoardID));
        }

        if (board != null)
        {
            String strLog = board.ToString(true) + " ======> ";

            board.BoardTitle = this.txtBoardName.Text;
            board.DisplayName = this.txtDisplayName.Text;
            board.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            board.BoardType = Convert.ToInt32(this.ddlBoardType.SelectedValue);
            OR.Control.DAL.Update<model.InfoBoardInfo>(board);

            strLog += board.ToString(true);

            Log.info("修改栏目信息：" + strLog);
            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.ReloadPage();</script>");
        }
        else
        {
            board = new model.InfoBoardInfo();
            board.BoardTitle = this.txtBoardName.Text;
            board.DisplayName = this.txtDisplayName.Text;
            board.BoardType = Convert.ToInt32(this.ddlBoardType.SelectedValue);
            board.Created = DateTime.Now;
            board.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            board.UserGUID = Util.GetLoginUserInfo().UserGUID;
            board.UserName = Util.GetLoginUserInfo().UserName;
            board = OR.Control.DAL.Add<model.InfoBoardInfo>(board, true);

            Log.info("新建栏目：" + board.ToString(true));
            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.location.href=parent.location.href;</script>");
        }
    }

    protected void LoadBoardInfo()
    {
        model.InfoBoardInfo board = OR.Control.DAL.GetModelByKey<model.InfoBoardInfo>(Convert.ToInt32(strBoardID));

        if (board != null)
        {
            this.txtBoardName.Text = board.BoardTitle;
            this.txtDisplayName.Text = board.DisplayName;
            this.ddlBoardType.SelectedValue = board.BoardType.ToString();
            if (board.Status == (int)model.Status.Normal)
            {
                this.chkStatus.Checked = true;
            }
            else
            {
                this.chkStatus.Checked = false;
            }
        }
    }
}