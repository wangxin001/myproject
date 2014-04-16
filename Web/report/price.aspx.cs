using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report_price : System.Web.UI.Page
{
    protected String strStoreName = String.Empty;
    protected String strPriceDate = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strStoreName = Util.GetLoginUserInfo().UserName;
        strPriceDate = DateTime.Now.Date.ToString("yyyy-MM-dd");

        if (!IsPostBack)
        {
            this.PriceQueryForm1.StoreVisible = true;
            this.PriceQueryForm1.LoadStoreInfo();
            this.PriceQueryForm1.LoadInitPrice();
        }
    }
}