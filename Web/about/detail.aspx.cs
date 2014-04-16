using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class about_detail : System.Web.UI.Page
{
    String strOrganize = System.Configuration.ConfigurationManager.AppSettings["Organize"].ToString();

    protected String strTopicID = string.Empty;
    protected String strTopicTitle = string.Empty;
    protected String strTopicContent = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strTopicID = Request.QueryString["TopicID"];

        if (!IsPostBack)
        {
            if (String.IsNullOrEmpty(strTopicID))
            {
                Response.End();
            }

            LoadContent();
        }
    }

    protected void LoadContent()
    {
        String strSQL = "Select TopicID,TopicTitle, case when TopicID=@TopicID then TopicContent else '' end  TopicContent From InfoTopicInfo where BoardID=@BoardID AND TopicID<>46";

        DataSet ds = OR.DB.SQLHelper.Query(strSQL,
              new SqlParameter("@TopicID", Convert.ToInt32(strTopicID)),
              new SqlParameter("@BoardID", Convert.ToInt32(strOrganize)));

        rpMenu.DataSource = ds.Tables[0];
        rpMenu.DataBind();

        DataRow[] dr = ds.Tables[0].Select("TopicID=" + strTopicID);
        if (dr.Length > 0)
        {
            strTopicTitle = dr[0]["TopicTitle"].ToString();
            strTopicContent = dr[0]["TopicContent"].ToString();
        }
    }

}