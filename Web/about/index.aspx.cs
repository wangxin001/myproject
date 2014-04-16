using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class about_index : System.Web.UI.Page
{
    String strOrganize = System.Configuration.ConfigurationManager.AppSettings["Organize"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String strSQL = "Select TopicID From InfoTopicInfo WHERE Status=1 AND BoardID=@BoardID Order By TopicOrder ";

            DataSet ds = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@BoardID", Convert.ToInt32(strOrganize)));

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("./detail_" + ds.Tables[0].Rows[0][0].ToString() + ".aspx");
                Response.End();
            }

        }
    }
}