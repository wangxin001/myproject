using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InfoPub_InfoView : System.Web.UI.Page
{
    protected String strInfoID = string.Empty;
    protected model.InfoTopicInfo info = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        strInfoID = Request.QueryString["InfoID"];

        if (!IsPostBack)
        {
            info = OR.Control.DAL.GetModelByKey<model.InfoTopicInfo>(Convert.ToInt32(strInfoID));

            this.Page.Title = info.TopicTitle;
        }
    }
}