using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_PriceEdit : System.Web.UI.Page
{

    protected String strUserGUID = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strUserGUID = Request.QueryString["UserGUID"];

        Page.Title = "菜价管理";

        if (!IsPostBack)
        {

            model.UserInfo user = OR.Control.DAL.GetModelByKey<model.UserInfo>(strUserGUID);

            this.txtStoreName.Text = user.UserName;

            LoadPriceInfo();

        }
    }

    protected void LoadPriceInfo()
    {
        LoadLastPriceDate();
    }

    protected void LoadLastPriceDate()
    {
        String strSQL = "select MAX(PriceDate) From dbo.StorePrice where StoreGUID = @pGUID";

        DateTime date = Convert.ToDateTime(OR.DB.SQLHelper.GetSingle(strSQL, new System.Data.SqlClient.SqlParameter("@pGUID", strUserGUID)));

        this.txtDate.Text = date.ToString("yyyy-MM-dd");

        LoadBatchInfo(this.txtDate.Text);
    }

    protected void LoadBatchInfo(String strDate)
    {
        String strSQL = "Select BatchID, MIN(Created) Created From StorePrice where StoreGUID = @pGUID and PriceDate = @pDate group by BatchID";

        System.Data.DataTable dt = OR.DB.SQLHelper.Query(strSQL, new System.Data.SqlClient.SqlParameter("@pGUID", strUserGUID), new System.Data.SqlClient.SqlParameter("@pDate", strDate)).Tables[0];

        this.ddpBatch.Items.Clear();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.ddpBatch.Items.Add(new ListItem(dt.Rows[i]["BatchID"].ToString() + " : " + Convert.ToDateTime(dt.Rows[i]["Created"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"), dt.Rows[i]["BatchID"].ToString()));
        }

        if (this.ddpBatch.Items.Count > 0)
        {
            LoadBatchPrice(this.txtDate.Text, Int32.Parse(this.ddpBatch.SelectedItem.Value));
        }
        else
        {
            LoadBatchPrice(this.txtDate.Text, 0);
        }
    }

    protected void LoadBatchPrice(String strDate, Int32 batchID)
    {
        List<model.StorePrice> prices = OR.Control.DAL.GetModelList<model.StorePrice>("StoreGUID = @pGUID and PriceDate = @pDate and BatchID= @pBatchID order by PriceID",
            new System.Data.SqlClient.SqlParameter("@pGUID", strUserGUID),
            new System.Data.SqlClient.SqlParameter("@pDate", strDate),
            new System.Data.SqlClient.SqlParameter("@pBatchID", batchID));

        this.GridView1.DataSource = prices;
        this.GridView1.DataBind();
    }

    protected void ddpBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatchPrice(this.txtDate.Text, Int32.Parse(this.ddpBatch.SelectedItem.Value));
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.StorePrice data = (model.StorePrice)e.Row.DataItem;

            TextBox txtPrice = e.Row.FindControl("txtPrice") as TextBox;
            TextBox txtPriceID = e.Row.FindControl("txtPriceID") as TextBox;

            txtPrice.Text = data.Price01.ToString();
            txtPriceID.Text = data.PriceID.ToString();
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        LoadBatchInfo(this.txtDate.Text);
    }

    protected void txtSave_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if (this.GridView1.Rows[i].RowType == DataControlRowType.DataRow)
            {
                TextBox txtPrice = this.GridView1.Rows[i].FindControl("txtPrice") as TextBox;
                TextBox txtPriceID = this.GridView1.Rows[i].FindControl("txtPriceID") as TextBox;

                Int32 pn = 0;

                if (Int32.TryParse(txtPriceID.Text, out pn))
                {
                    model.StorePrice p = OR.Control.DAL.GetModelByKey<model.StorePrice>(Int32.Parse(txtPriceID.Text));

                    p.Price01 = Double.Parse(txtPrice.Text);

                    OR.Control.DAL.Update<model.StorePrice>(p);
                }
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        String strSQL = "Delete From StorePrice where StoreGUID = @pGUID and PriceDate = @pDate and BatchID= @pBatchID";

        OR.DB.SQLHelper.ExecuteSql(strSQL,
            new System.Data.SqlClient.SqlParameter("@pGUID", strUserGUID),
            new System.Data.SqlClient.SqlParameter("@pDate", this.txtDate.Text),
            new System.Data.SqlClient.SqlParameter("@pBatchID", Int32.Parse(this.ddpBatch.SelectedItem.Value)));

        LoadBatchInfo(this.txtDate.Text);
    }
}