using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sync_ItemDetail : System.Web.UI.Page
{
    String itemCode = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        itemCode = Request.QueryString["ItemCode"];

        if (String.IsNullOrEmpty(itemCode))
        {
            return;
        }

        if (!IsPostBack)
        {
            LoadItemDetail();
        }
    }

    protected void LoadItemDetail()
    {
        String strSQL = @"SELECT item.ItemCode
	                        ,item.ItemName
	                        ,item.NodeCode
	                        ,tree.NodeName
	                        ,ISNULL(item.CatalogCode, '') CataCode
	                        ,ISNULL(cata.CataName, '') CataName
	                        ,ISNULL(item.BrandCode, '') BrandCode
	                        ,ISNULL(dbrand.[Name], '') BrandName
	                        ,ISNULL(item.SpecCode, '') SpecCode
	                        ,ISNULL(dspec.[NAME], '') SpecName
	                        ,ISNULL(item.AreaCode, '') AreaCode
	                        ,ISNULL(darea.[NAME], '') AreaName
                            ,item.Active
                        FROM [jjw].[dbo].[Dict_RemoteItem] item
                        LEFT JOIN dbo.Dict_RemoteTree tree ON item.NodeCode = tree.NodeCode
                        LEFT JOIN dbo.Dict_RemoteCata cata ON item.CatalogCode = cata.CataCode
                        LEFT JOIN dbo.Dict_RemoteDictValue dbrand ON item.BrandCode = dbrand.Code
                        LEFT JOIN dbo.Dict_RemoteDictValue dspec ON item.SpecCode = dspec.Code
                        LEFT JOIN dbo.Dict_RemoteDictValue darea ON item.AreaCode = darea.Code
                        WHERE item.ItemCode = @itemCode";

        System.Data.DataSet ds = OR.DB.SQLHelper.Query(strSQL, new System.Data.SqlClient.SqlParameter("@itemCode", itemCode));

        if (ds.Tables[0].Rows.Count > 0)
        {
            System.Data.DataRow row = ds.Tables[0].Rows[0];

            this.txtActive.Text = row["Active"].ToString().Equals("1") ? "有效" : "无效";
            this.txtAreaCode.Text = row["AreaName"].ToString();
            this.txtBrandCode.Text = row["BrandName"].ToString();
            this.txtCatalogCode.Text = row["CataName"].ToString();
            this.txtItemCode.Text = itemCode;
            this.txtItemName.Text = row["ItemName"].ToString();
            this.txtNodeCode.Text = row["NodeName"].ToString();
            this.txtSpecCode.Text = row["SpecName"].ToString();
        }
    }
}