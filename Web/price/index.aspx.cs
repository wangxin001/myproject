using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class price_index : System.Web.UI.Page
{
    String strBoardID = String.Empty;

    protected model.InfoBoardInfo board = null;
    protected String strTypeName = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strBoardID = Request.QueryString["BoardID"];

        if (!IsPostBack)
        {
            GetInfoData();
        }
    }

    private void GetInfoData()
    {
        board = OR.Control.DAL.GetModelByKey<model.InfoBoardInfo>(Convert.ToInt32(strBoardID));

        String strSQL = "Select TopicID, TopicTitle,TopicLink, PublishDate,TopicType From InfoTopicInfo WHERE BoardID=@BoardID ";
        strSQL += " AND Status=" + ((int)model.Status.Normal).ToString() + " ORDER By  TopicOrder DESC,PublishDate desc,TopicID desc";

        DataTable infoData = OR.DB.SQLHelper.Query(strSQL, new SqlParameter("@BoardID", Convert.ToInt32(strBoardID))).Tables[0];

        this.rpInfo.DataSource = infoData;
        this.rpInfo.DataBind();
    }
}