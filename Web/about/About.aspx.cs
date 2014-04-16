using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class about_About : System.Web.UI.Page
{
    String strOrganize = System.Configuration.ConfigurationManager.AppSettings["Organize"].ToString();

    protected String strTopicID = string.Empty;
    protected String strTopicTitle = string.Empty;
    protected String strTopicContent = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strTopicID = "46";

        if (!IsPostBack)
        {
            LoadContent();
            LoadPhotoData();
        }
    }

    protected void LoadContent()
    {
        String strSQL = "Select TopicID,TopicTitle, case when TopicID=@TopicID then TopicContent else '' end  TopicContent From InfoTopicInfo where BoardID=@BoardID";

        DataSet ds = OR.DB.SQLHelper.Query(strSQL,
              new SqlParameter("@TopicID", Convert.ToInt32(strTopicID)),
              new SqlParameter("@BoardID", Convert.ToInt32(strOrganize)));

        DataRow[] dr = ds.Tables[0].Select("TopicID=" + strTopicID);
        if (dr.Length > 0)
        {
            strTopicTitle = dr[0]["TopicTitle"].ToString();
            strTopicContent = dr[0]["TopicContent"].ToString();
        }

        ds.Tables[0].Rows.RemoveAt(0);
        rpMenu.DataSource = ds.Tables[0];
        rpMenu.DataBind();
    }


    protected void LoadPhotoData()
    {

        String strPhoto = System.Configuration.ConfigurationManager.AppSettings["Photo"].ToString();

        String strSQL = "Select TopicID,TopicTitle,TopicContent From InfoTopicInfo where BoardID=@BoardID Order By TopicOrder";

        DataSet ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@BoardID", Convert.ToInt32(strPhoto)));

        rpPhoto.DataSource = ds.Tables[0];
        rpPhoto.DataBind();
    }
}