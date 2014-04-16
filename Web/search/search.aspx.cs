using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class search_search : System.Web.UI.Page
{

    String strBoardID = string.Empty;
    int boardID = 0;
    String strKeyWord = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strBoardID = Request.QueryString["c"];
        strKeyWord = Request.QueryString["key"];

        strKeyWord = Server.UrlDecode(strKeyWord);

        if (!Int32.TryParse(strBoardID, out boardID))
        {
            return;
        }

        if (!IsPostBack)
        {
            GetInfoData();
        }
    }

    private void GetInfoData()
    {
        String strSQL = "Select BoardID,TopicID, TopicTitle,PublishDate From InfoTopicInfo WHERE ";
        if (boardID == 0)
        {
            strSQL += " BoardID in (1,2,3) AND ";
        }
        else
        {
            strSQL += " BoardID=@BoardID AND ";
        }
        strSQL += " Status=" + ((int)model.Status.Normal).ToString() + " AND TopicTitle like @key ";
        strSQL += " ORDER By PublishDate desc,TopicID desc ";

        DataTable infoData = OR.DB.SQLHelper.Query(strSQL,
            new SqlParameter("@BoardID", boardID),
            new SqlParameter("@key", "%" + strKeyWord + "%")).Tables[0];

        this.rpInfo.DataSource = infoData;
        this.rpInfo.DataBind();
    }

    protected String GetFolder( String strBoard )
    {
        if (strBoard == "1")
        {
            return "job";
        }
        else if (strBoard == "2")
        {
            return "news";
        }
        else if (strBoard == "3")
        {
            return "policy";
        }

        return "";
    }
}