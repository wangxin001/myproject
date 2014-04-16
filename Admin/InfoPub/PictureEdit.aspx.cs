using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class InfoPub_PictureEdit : System.Web.UI.Page
{
    String strBoardID = string.Empty;
    String strTopicID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strBoardID = Request.QueryString["BoardID"];
        strTopicID = Request.QueryString["TopicID"];

        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "焦点图片管理";

        if (!IsPostBack)
        {
            if (String.IsNullOrEmpty(strTopicID))
            {
                LoadDefaultSetting();
            }
            else
            {
                LoadTopic();
            }

        }
    }

    protected void LoadDefaultSetting()
    {
        this.txtPublisher.Text = Util.GetLoginUserInfo().UserName;
        this.chkStatus.Checked = true;
        this.txtOrder.Text = "0";
    }

    protected void LoadTopic()
    {
        model.InfoTopicInfo topic = OR.Control.DAL.GetModelByKey<model.InfoTopicInfo>(Convert.ToInt32(strTopicID));

        this.txtTitle.Text = topic.TopicTitle;
        this.txtPublisher.Text = topic.Publisher;
        this.txtLink.Text = topic.TopicLink;
        this.txtOrder.Text = topic.TopicOrder.ToString();

        this.chkStatus.Checked = (topic.Status == (int)model.Status.Normal ? true : false);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        String strImageFile = string.Empty;

        if (FileUpload1.HasFile)
        {
            String[] strPath = Util.GetUploadFileSavePath();

            String strExtName = (new FileInfo(FileUpload1.FileName)).Extension;

            if (System.Configuration.ConfigurationManager.AppSettings["UploadImageFileExt"].ToLower().IndexOf(strExtName.ToLower()) < 0)
            {
                this.txtMessage.Text = "对不起，不能上传 " + strExtName + " 类型的文件";
                return;
            }

            // 欲保存的文件名
            String strGUID = Guid.NewGuid().ToString();
            String strFileName = strGUID + strExtName;

            // 图片保存的文件夹
            String strImageFolder = Util.FilePathInfo.GetPathInfo(strPath[0]).Append(strBoardID).Append(strPath[1]).ToString();

            // 创建文件夹
            DirectoryInfo directory = new DirectoryInfo(strImageFolder);
            if (!Directory.Exists(strImageFolder))
            {
                Directory.CreateDirectory(strImageFolder);
            }

            // 文件的全路径名称
            string strImageFileName = Util.FilePathInfo.GetPathInfo(strImageFolder).Append(strFileName).ToString();

            // 保存文件
            FileUpload1.SaveAs(strImageFileName);

            // 生成缩略图
            String strThumbImageFileName = Util.FilePathInfo.GetPathInfo(strImageFolder).Append("thumb_" + strGUID + ".jpg").ToString();
            CKFinder.Connector.ImageTools.ResizeImage(strImageFileName, strThumbImageFileName, 327, 227, false, 50);

            Log.info("上传文件：" + strFileName);

            // 得到除了根部分的其他部分文件夹名称，后面需记录
            strImageFile = Util.FilePathInfo.GetPathInfo(strBoardID).Append(strPath[1]).Append(strFileName).ToString().Replace("\\", "/");
        }

        if (String.IsNullOrEmpty(strTopicID))
        {
            /*
             * 新上传图片
             */

            if (String.IsNullOrEmpty(strImageFile))
            {
                this.txtMessage.Text = "对不起，新上传图片必须选择图片文件";
                return;
            }

            model.InfoTopicInfo topic = new model.InfoTopicInfo();

            topic.BoardID = Convert.ToInt32(strBoardID);
            topic.TopicTitle = this.txtTitle.Text;

            topic.TopicContent = strImageFile;
            topic.TopicLink = this.txtLink.Text;
            topic.TopicOrder = Convert.ToInt32(this.txtOrder.Text);

            topic.PublishDate = DateTime.Now.Date;
            topic.Publisher = this.txtPublisher.Text;
            topic.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);

            topic.Created = DateTime.Now;
            topic.Updated = DateTime.Now;
            topic.UserGUID = Util.GetLoginUserInfo().UserGUID;
            topic.UserName = Util.GetLoginUserInfo().UserName;

            topic.Versions = 1;

            topic = OR.Control.DAL.Add<model.InfoTopicInfo>(topic, true);

            Log.info("发布了新图片：" + topic.ToString());
        }
        else
        {
            /*
             * 更新图片
             */

            model.InfoTopicInfo topic = OR.Control.DAL.GetModelByKey<model.InfoTopicInfo>(Convert.ToInt32(strTopicID));

            topic.TopicTitle = this.txtTitle.Text;
            topic.TopicLink = this.txtLink.Text;

            if (!String.IsNullOrEmpty(strImageFile))
            {
                topic.TopicContent = strImageFile;
            }

            topic.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            topic.TopicOrder = Convert.ToInt32(this.txtOrder.Text);

            topic.Updated = DateTime.Now;
            topic.Versions = topic.Versions + 1;

            OR.Control.DAL.Update<model.InfoTopicInfo>(topic);

            Log.info("更新了新图片：" + topic.ToString());
        }

        Response.Redirect("./PictureList.aspx?BoardID=" + strBoardID);
    }
}