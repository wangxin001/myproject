using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InfoPub_QAEdit : System.Web.UI.Page
{
    String strBoardID = string.Empty;
    String strTopicID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strBoardID = Request.QueryString["BoardID"];
        strTopicID = Request.QueryString["TopicID"];

        if (!IsPostBack)
        {
            MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
            master.PageTitle = "问答发布编辑";

            if (String.IsNullOrEmpty(strTopicID))
            {
                LoadDefaultSetting();
            }
            else
            {
                model.InfoTopicInfo topic = OR.Control.DAL.GetModelByKey<model.InfoTopicInfo>(Convert.ToInt32(strTopicID));
                this.txtTitile.Text = topic.TopicTitle;
                this.txtPublisher.Text = topic.Publisher;
                this.txtPublisDate.Text = topic.PublishDate.ToString("yyyy-MM-dd");
                this.txtContent.Text = topic.TopicContent;
                this.txtOrder.Text = topic.TopicOrder.ToString();

                this.chkStatus.Checked = ((model.Status)topic.Status == model.Status.Normal ? true : false);
            }
        }
    }

    protected void LoadDefaultSetting()
    {
        this.txtPublisDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        this.txtPublisher.Text = Util.GetLoginUserInfo().UserName;
        this.chkStatus.Checked = true;
        this.txtOrder.Text = "0";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(strTopicID))
        {
            model.InfoTopicInfo topic = new model.InfoTopicInfo();
            topic.BoardID = Convert.ToInt32(strBoardID);
            topic.Created = DateTime.Now;
            topic.PublishDate = Convert.ToDateTime(this.txtPublisDate.Text);
            topic.Publisher = this.txtPublisher.Text;
            topic.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            topic.TopicContent = this.txtContent.Text;
            topic.TopicLink = String.Empty;
            topic.TopicTitle = this.txtTitile.Text;
            topic.TopicOrder = Convert.ToInt32(this.txtOrder.Text);
            topic.Updated = DateTime.Now;
            topic.UserGUID = Util.GetLoginUserInfo().UserGUID;
            topic.UserName = Util.GetLoginUserInfo().UserName;
            topic.Versions = 1;

            topic = OR.Control.DAL.Add<model.InfoTopicInfo>(topic, true);

            Log.info("发布了新问答：" + topic.ToString());
        }
        else
        {
            model.InfoTopicInfo topic = OR.Control.DAL.GetModelByKey<model.InfoTopicInfo>(Convert.ToInt32(strTopicID));

            topic.PublishDate = Convert.ToDateTime(this.txtPublisDate.Text);
            topic.Publisher = this.txtPublisher.Text;
            topic.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            topic.TopicContent = this.txtContent.Text;
            topic.TopicTitle = this.txtTitile.Text;
            topic.TopicOrder = Convert.ToInt32(this.txtOrder.Text);
            topic.Updated = DateTime.Now;
            topic.Versions = topic.Versions + 1;
            OR.Control.DAL.Update<model.InfoTopicInfo>(topic);

            Log.info("更新问答：" + topic.ToString());
        }

        Response.Redirect("./QAList.aspx?BoardID=" + strBoardID);
    }
}