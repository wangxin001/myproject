using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class about_monitor : System.Web.UI.Page
{
    String strOrganize = System.Configuration.ConfigurationManager.AppSettings["Organize"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadContent();
        }
    }

    protected void LoadContent()
    {
        String strSQL = "Select TopicID,TopicTitle From InfoTopicInfo where BoardID=@BoardID";

        DataSet ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@BoardID", Convert.ToInt32(strOrganize)));

        rpMenu.DataSource = ds.Tables[0];
        rpMenu.DataBind();
    }
}