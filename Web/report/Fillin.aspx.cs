using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;

using NPOI.HSSF.Util;


public partial class report_Fillin : System.Web.UI.Page
{
    protected String strStoreName = String.Empty;
    protected String strAveragePriceDate = String.Empty;
    protected String strStorePriceDate = String.Empty;
    protected String strStorePriceTime = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strStoreName = Util.GetLoginUserInfo().UserName;
        strStorePriceDate = DateTime.Now.Date.ToString("yyyy-MM-dd");

        if (!IsPostBack)
        {
            LoadPriceItem();
        }
    }

    protected void LoadPriceItem()
    {

        // 获取当天平均价格
        String strSQL = @"
with 
maxdate as ((Select MAX(PriceDate) val From PriceRecord where ItemType in (1,15,16) AND PriceDate< Cast(GETDATE() as Date))),
StorePriceTmp as ( SELECT *	FROM StorePrice	WHERE PriceDate = CAST(GETDATE() as date) AND StoreGUID = @sguid )

SELECT i.ItemID, i.ItemName, i.ItemOrder, i.ItemLevel, 
       i.ItemUnit, a.PriceDate, ISNULL(Price03, 0) Price03, s.Price01 StorePrice, s.BatchID, s.Created
FROM PriceItems i
LEFT JOIN (
	SELECT * FROM PriceRecord WHERE PriceDate = (Select max(val) from maxdate)
	) a ON a.ItemID = i.ItemID
LEFT JOIN (
	Select * From StorePriceTmp where BatchID = (Select MAX(BatchID) From StorePriceTmp)
	) s ON i.ItemID = s.ItemID
WHERE i.ReportStatus = 1 AND i.STATUS = 1
ORDER BY i.ItemOrder ";

        DataTable itemAverageTable = OR.DB.SQLHelper.Query(strSQL,
            new System.Data.SqlClient.SqlParameter("@sguid", Util.GetLoginUserInfo().UserGUID)).Tables[0];

        if (itemAverageTable.Rows.Count > 0)
        {
            strAveragePriceDate = Convert.ToDateTime(itemAverageTable.Rows[0]["PriceDate"].ToString()).ToString("yyyy-MM-dd");
        }
        else
        {
            strAveragePriceDate = "--";
        }

        this.Repeater1.DataSource = itemAverageTable;
        this.Repeater1.DataBind();
    }

    protected void btnCopyData_Click(object sender, EventArgs e)
    {
        String strSQL = @"
with 
maxdate as ((Select MAX(PriceDate) val From PriceRecord where ItemType in (1,15,16) AND PriceDate< Cast(GETDATE() as Date))),
StoreDateBatch as (Select top 1 PriceDate,ISNULL(BatchID,0) BatchID From StorePrice where PriceDate < CAST(GETDATE() as date) and StoreGUID=@sguid Order By PriceDate Desc, ISNULL(BatchID,0) Desc)

SELECT i.ItemID, i.ItemName, i.ItemOrder, i.ItemLevel, 
       i.ItemUnit, a.PriceDate, ISNULL(Price03, 0) Price03, s.Price01 StorePrice, s.BatchID, NULL Created
FROM PriceItems i
LEFT JOIN (
	SELECT * FROM PriceRecord WHERE PriceDate = (Select max(val) from maxdate)
	) a ON a.ItemID = i.ItemID
LEFT JOIN (
	Select * From StorePrice where StoreGUID=@sguid AND PriceDate = (Select MAX(PriceDate) From StoreDateBatch) AND ISNULL(BatchID,0) = (Select MAX(BatchID) From StoreDateBatch)
	) s ON i.ItemID = s.ItemID
WHERE i.ReportStatus = 1 AND i.STATUS = 1
ORDER BY i.ItemOrder";

        DataTable itemAverageTable = OR.DB.SQLHelper.Query(strSQL,
            new System.Data.SqlClient.SqlParameter("@sguid", Util.GetLoginUserInfo().UserGUID)).Tables[0];

        if (itemAverageTable.Rows.Count > 0)
        {
            strAveragePriceDate = Convert.ToDateTime(itemAverageTable.Rows[0]["PriceDate"].ToString()).ToString("yyyy-MM-dd");
        }
        else
        {
            strAveragePriceDate = "--";
        }

        this.Repeater1.DataSource = itemAverageTable;
        this.Repeater1.DataBind();
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            System.Data.DataRowView item = e.Item.DataItem as System.Data.DataRowView;

            if (item != null)
            {
                TextBox priceTextBox = (TextBox)e.Item.FindControl("txtItemPrice");
                priceTextBox.Text = item.Row["StorePrice"].ToString();
                priceTextBox.Attributes.Add("ItemID", item.Row["ItemID"].ToString());
                priceTextBox.Attributes.Add("PriceItem", "1");


                TextBox priceLastTextBox = (TextBox)e.Item.FindControl("txtItemPriceLast");
                priceLastTextBox.Text = item.Row["StorePrice"].ToString();
                priceLastTextBox.Attributes.Add("ItemID", item.Row["ItemID"].ToString());
                priceLastTextBox.Attributes.Add("PriceItem", "2");


                Label l1 = e.Item.FindControl("txtItemOrder") as Label;
                Label l2 = e.Item.FindControl("txtItemName") as Label;
                Label l3 = e.Item.FindControl("txtItemLevel") as Label;
                Label l4 = e.Item.FindControl("txtItemUnit") as Label;
                Label l5 = e.Item.FindControl("txtPrice03") as Label;
                Label l7 = e.Item.FindControl("txtCreated") as Label;


                l1.Text = item.Row["ItemOrder"].ToString() + " ";
                l2.Text = item.Row["ItemName"].ToString() + " ";
                l3.Text = item.Row["ItemLevel"].ToString() + " ";
                l4.Text = item.Row["ItemUnit"].ToString() + " ";
                l5.Text = item.Row["Price03"].ToString() + " ";
                l7.Text = String.IsNullOrEmpty(item.Row["Created"].ToString()) ? " " : Convert.ToDateTime(item.Row["Created"].ToString()).ToString("HH时mm分ss秒");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        model.UserInfo u = OR.Control.DAL.GetModelByKey<model.UserInfo>(Util.GetLoginUserInfo().UserGUID);

        DataTable itemTable = OR.DB.SQLHelper.Query("Select * From PriceItems Where ReportStatus=1 AND Status=1").Tables[0];

        SqlParameter pguid = new SqlParameter("@pguid", Util.GetLoginUserInfo().UserGUID);
        SqlParameter pdate = new SqlParameter("@pdate", DateTime.Now.Date);

        int batchID = 1;

        DataTable batchTable = OR.DB.SQLHelper.Query("Select ISNULL(Max(BatchID),0) From StorePrice where StoreGUID=@pguid AND PriceDate=@pdate",
             pguid, pdate).Tables[0];

        if (batchTable.Rows.Count > 0)
        {
            batchID = Convert.ToInt32(batchTable.Rows[0][0].ToString()) + 1;
        }

        Boolean needSave = false;

        if (batchID == 1)
        {
            needSave = true;
        }

        for (int i = 0; i < this.Repeater1.Items.Count; i++)
        {
            RepeaterItem item = this.Repeater1.Items[i];

            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox priceTextBox = (TextBox)item.FindControl("txtItemPrice");
                TextBox priceLastTextBox = (TextBox)item.FindControl("txtItemPriceLast");

                priceTextBox.ForeColor = System.Drawing.Color.Black;

                // 为空
                if (String.IsNullOrEmpty(priceTextBox.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('数据项不能为空');", true);
                    return;
                }

                Double p = 0.0;

                // 非数字
                if (!Double.TryParse(priceTextBox.Text, out p))
                {
                    priceTextBox.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('数据项必须为数字');", true);
                    continue;
                }

                // 小于零
                if (p < 0.0)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('数据项不能小于零');", true);
                    priceTextBox.ForeColor = System.Drawing.Color.Red;
                    continue;
                }

                if (needSave == false)
                {

                    // 差距大于1
                    Double pLast = 0.0;

                    if (!Double.TryParse(priceLastTextBox.Text, out pLast))
                    {
                        continue;
                    }

                    if (p != pLast)
                    {
                        needSave = true;
                        continue;
                    }
                }

            }
        }

        if (needSave)
        {
            for (int i = 0; i < this.Repeater1.Items.Count; i++)
            {
                RepeaterItem item = this.Repeater1.Items[i];

                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox priceTextBox = (TextBox)item.FindControl("txtItemPrice");

                    priceTextBox.ForeColor = System.Drawing.Color.Black;

                    int itemID = 0;

                    if (!Int32.TryParse(priceTextBox.Attributes["ItemID"], out itemID))
                    {
                        continue;
                    }

                    DataRow[] priceItem = itemTable.Select("ItemID=" + itemID.ToString());

                    if (priceItem.Length == 0)
                    {
                        continue;
                    }

                    if (!String.IsNullOrEmpty(priceTextBox.Text.Trim()))
                    {
                        Double p = 0.0;

                        if (!Double.TryParse(priceTextBox.Text, out p))
                        {
                            priceTextBox.ForeColor = System.Drawing.Color.Red;
                            continue;
                        }

                        if (p < 0.0)
                        {
                            priceTextBox.ForeColor = System.Drawing.Color.Red;
                            continue;
                        }

                        model.StorePrice price = new model.StorePrice();

                        price.Created = DateTime.Now;
                        price.ItemID = itemID;
                        price.ItemLevel = priceItem[0]["ItemLevel"].ToString();
                        price.ItemName = priceItem[0]["ItemName"].ToString();
                        price.ItemType = Convert.ToInt32(priceItem[0]["ItemType"].ToString());
                        price.ItemUnit = priceItem[0]["ItemUnit"].ToString();
                        price.Price01 = p;
                        price.PriceDate = DateTime.Now.Date;
                        price.Status = (int)model.Status.Normal;
                        price.StoreGUID = u.UserGUID;
                        price.StoreName = u.UserName;
                        price.UserGUID = u.UserGUID;
                        price.BatchID = batchID;

                        price = OR.Control.DAL.Add<model.StorePrice>(price, true);
                    }
                }
            }

            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", "alert('数据保存成功');", true);
        }

        LoadPriceItem();
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {

    }
}