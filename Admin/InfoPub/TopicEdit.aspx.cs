using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InfoPub_TopicEdit : System.Web.UI.Page
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
            master.PageTitle = "新闻发布编辑";

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
                this.chkShowDate.Checked = topic.ShowDate;
                this.txtContent.Text = topic.TopicContent;
                this.txtTopicLink.Text = topic.TopicLink;
                this.txtOrder.Text = topic.TopicOrder.ToString();
                this.ddpContentType.SelectedValue = topic.TopicType.ToString();

                if (topic.TopicType == 1)
                {
                    this.Panel1.Visible = true;
                    this.Panel2.Visible = false;
                }
                else
                {
                    this.Panel1.Visible = false;
                    this.Panel2.Visible = true;
                }

                this.chkStatus.Checked = ((model.Status)topic.Status == model.Status.Normal ? true : false);
            }
        }
    }

    protected void LoadDefaultSetting()
    {
        this.txtPublisDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        this.txtPublisher.Text = Util.GetLoginUserInfo().UserName;
        this.chkStatus.Checked = true;
        this.chkShowDate.Checked = true;
        this.Panel2.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(strTopicID))
        {
            model.InfoTopicInfo topic = new model.InfoTopicInfo();
            topic.BoardID = Convert.ToInt32(strBoardID);
            topic.Created = DateTime.Now;
            topic.PublishDate = Convert.ToDateTime(this.txtPublisDate.Text);
            topic.ShowDate = this.chkShowDate.Checked;
            topic.Publisher = this.txtPublisher.Text;
            topic.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            topic.TopicContent = this.txtContent.Text;
            topic.TopicLink = this.txtTopicLink.Text;
            topic.TopicTitle = this.txtTitile.Text;
            topic.Updated = DateTime.Now;
            topic.UserGUID = Util.GetLoginUserInfo().UserGUID;
            topic.UserName = Util.GetLoginUserInfo().UserName;
            topic.Versions = 1;
            topic.TopicOrder = Int32.Parse(this.txtOrder.Text);
            topic.TopicType = Int32.Parse(this.ddpContentType.SelectedItem.Value);

            topic = OR.Control.DAL.Add<model.InfoTopicInfo>(topic, true);

            Log.info("发布了新文章：" + topic.ToString());
        }
        else
        {
            model.InfoTopicInfo topic = OR.Control.DAL.GetModelByKey<model.InfoTopicInfo>(Convert.ToInt32(strTopicID));

            topic.PublishDate = Convert.ToDateTime(this.txtPublisDate.Text);
            topic.ShowDate = this.chkShowDate.Checked;

            topic.Publisher = this.txtPublisher.Text;
            topic.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            topic.TopicContent = this.txtContent.Text;
            topic.TopicLink = this.txtTopicLink.Text;
            topic.TopicTitle = this.txtTitile.Text;
            topic.Updated = DateTime.Now;
            topic.Versions = topic.Versions + 1;
            topic.TopicOrder = Int32.Parse(this.txtOrder.Text);
            topic.TopicType = Int32.Parse(this.ddpContentType.SelectedItem.Value);

            OR.Control.DAL.Update<model.InfoTopicInfo>(topic);

            Log.info("更新文章：" + topic.ToString());
        }

        Response.Redirect("./TopicList.aspx?BoardID=" + strBoardID);
    }

    protected void ddpContentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddpContentType.SelectedIndex == 0)
        {
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
        }
        else
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;
        }
    }
}