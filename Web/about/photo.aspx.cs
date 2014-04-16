using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class about_photo : System.Web.UI.Page
{
    String strPhoto = System.Configuration.ConfigurationManager.AppSettings["Photo"].ToString();

    protected String strTitle = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPhotoData();
        }
    }

    protected void LoadPhotoData()
    {
        String strSQL = "Select TopicID,TopicTitle,TopicContent From InfoTopicInfo where BoardID=@BoardID Order By TopicOrder";

        DataSet ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@BoardID", Convert.ToInt32(strPhoto)));

        rpPhoto.DataSource = ds.Tables[0];
        rpPhoto.DataBind();

        strSQL = "Select BoardTitle,DisplayName From InfoBoardInfo where BoardID=@BoardID";
        ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@BoardID", Convert.ToInt32(strPhoto)));

        strTitle = ds.Tables[0].Rows[0][1].ToString();
    }
}