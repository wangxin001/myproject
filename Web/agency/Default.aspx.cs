using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class agency_Default : System.Web.UI.Page
{
    String strTopicID = string.Empty;

    protected model.InfoTopicInfo info = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        strTopicID = Request.QueryString["TopicID"];

        LoadTopicList();

        if (String.IsNullOrEmpty(strTopicID))
        {
            Response.Redirect("../index.aspx");
            Response.End();
        }

        if (!IsPostBack)
        {
            LoadTopicInfo();
        }
    }

    protected void LoadTopicList()
    {
        List<model.InfoTopicInfo> topics= OR.Control.DAL.GetModelList<model.InfoTopicInfo>("BoardID=@bid",
            new System.Data.SqlClient.SqlParameter("@bid", System.Configuration.ConfigurationManager.AppSettings["PriceMonitorUnitList"]));

        this.Repeater1.DataSource = topics;
        this.Repeater1.DataBind();

        if (String.IsNullOrEmpty(strTopicID) && topics.Count > 0)
        {
            strTopicID = topics[0].TopicID.ToString();
             
        }
    }

    protected void LoadTopicInfo()
    {
        info = OR.Control.DAL.GetModelByKey<model.InfoTopicInfo>(Convert.ToInt32(strTopicID));
        if (info == null)
        {
            Response.Redirect("../index.aspx");
            Response.End();
        }
    }
}