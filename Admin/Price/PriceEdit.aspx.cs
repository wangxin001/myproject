using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Price_PriceEdit : System.Web.UI.Page
{
    String strItemID = string.Empty;
    String strItemType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strItemID = Request.QueryString["ItemID"];
        strItemType = Request.QueryString["ItemType"];

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(strItemID))
            {
                LoadVegetableInfo();
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        model.PriceItems priceItem = null;
        if (!String.IsNullOrEmpty(strItemID))
        {
            priceItem = OR.Control.DAL.GetModelByKey<model.PriceItems>(Convert.ToInt32(strItemID));
        }

        if (priceItem != null)
        {
            String strLog = priceItem.ToString(true) + " ======> ";

            // 更新数据
            priceItem.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            priceItem.ItemName = this.txtVegetableName.Text;
            priceItem.ItemOrder = Convert.ToInt32(this.txtVegetableOrder.Text);
            priceItem.ItemUnit = this.txtVegetableUnit.Text;
            priceItem.ItemLevel = this.txtItemLevel.Text;

            priceItem.ReportStatus = this.chkReport.Checked ? 1 : 0;

            OR.Control.DAL.Update<model.PriceItems>(priceItem);

            strLog += priceItem.ToString(true);

            Log.info("修改蔬菜条目信息：" + strLog);
            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.CloseFrame();parent.ReloadPage();</script>");
        }
        else
        {
            priceItem = new model.PriceItems();
            priceItem.Status = (int)(this.chkStatus.Checked ? model.Status.Normal : model.Status.Forbidden);
            priceItem.ItemName = this.txtVegetableName.Text;
            priceItem.ItemOrder = Convert.ToInt32(this.txtVegetableOrder.Text);
            priceItem.ItemUnit = this.txtVegetableUnit.Text;
            priceItem.ItemLevel = this.txtItemLevel.Text;

            priceItem.UserGUID = Util.GetLoginUserInfo().UserGUID;
            priceItem.UserName = Util.GetLoginUserInfo().UserName;
            priceItem.Created = DateTime.Now;
            priceItem.ItemType = Convert.ToInt32(strItemType);

            priceItem.ReportStatus = this.chkReport.Checked ? 1 : 0;

            priceItem = OR.Control.DAL.Add<model.PriceItems>(priceItem, true);

            Log.info("新建蔬菜条目信息：" + priceItem.ToString(true));
            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.CloseFrame();parent.ReloadPage();</script>");
        }
    }

    protected void LoadVegetableInfo()
    {
        model.PriceItems priceItem = OR.Control.DAL.GetModelByKey<model.PriceItems>(Convert.ToInt32(strItemID));

        if (priceItem != null)
        {
            this.txtVegetableName.Text = priceItem.ItemName;
            this.txtVegetableOrder.Text = priceItem.ItemOrder.ToString();
            this.txtVegetableUnit.Text = priceItem.ItemUnit;
            this.txtItemLevel.Text = priceItem.ItemLevel;

            this.chkReport.Checked = (priceItem.ReportStatus == 1);

            if (priceItem.Status == (int)model.Status.Normal)
            {
                this.chkStatus.Checked = true;
            }
            else
            {
                this.chkStatus.Checked = false;
            }
        }
    }
}