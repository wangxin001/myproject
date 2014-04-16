using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class about_district : System.Web.UI.Page
{
    String strOrganize = System.Configuration.ConfigurationManager.AppSettings["Organize"].ToString();
    String strDepartment = System.Configuration.ConfigurationManager.AppSettings["Department"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadContent();
            LoadDepartment();
        }
    }

    protected void LoadContent()
    {
        String strSQL = "Select TopicID,TopicTitle From InfoTopicInfo where BoardID=@BoardID AND TopicID<>46";

        DataSet ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@BoardID", Convert.ToInt32(strOrganize)));

        rpMenu.DataSource = ds.Tables[0];
        rpMenu.DataBind();
    }

    protected void LoadDepartment()
    {
        String strSQL = "Select TopicID,TopicTitle,TopicContent From InfoTopicInfo where BoardID=@BoardID Order By TopicOrder";

        DataSet ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@BoardID", Convert.ToInt32(strDepartment)));

        rpDepartment.DataSource = ds.Tables[0];
        rpDepartment.DataBind();
    }
}