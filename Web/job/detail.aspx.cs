﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class job_detail : System.Web.UI.Page
{
    String strTopicID = string.Empty;

    protected model.InfoTopicInfo info = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        strTopicID = Request.QueryString["TopicID"];

        if (String.IsNullOrEmpty(strTopicID))
        {
            Response.Redirect("./index.aspx");
            Response.End();
        }

        if (!IsPostBack)
        {
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
    }
}