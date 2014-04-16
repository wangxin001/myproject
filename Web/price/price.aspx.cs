using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

public partial class price_price : System.Web.UI.Page
{
    String strTopicID = string.Empty;
    protected String strTypeID = String.Empty;

    protected model.InfoTopicInfo info = null;

    protected model.InfoBoardInfo board = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        strTopicID = Request.QueryString["TopicID"];
        strTypeID = Request.QueryString["Type"];

        if (String.IsNullOrEmpty(strTypeID))
        {
            strTypeID = "0";
        }

        if (!IsPostBack)
        {
            if (String.IsNullOrEmpty(strTopicID))
            {
                strTopicID = System.Configuration.ConfigurationManager.AppSettings["PriceDefault"].ToString();
            }

            LoadTopicInfo();
        }
    }

    protected void LoadTopicInfo()
    {
        info = OR.Control.DAL.GetModelByKey<model.InfoTopicInfo>(Convert.ToInt32(strTopicID));
        if (info == null)
        {
            Response.Redirect("./index.aspx");
            Response.End();
        }

        board = OR.Control.DAL.GetModelByKey<model.InfoBoardInfo>(info.BoardID);
    }
}