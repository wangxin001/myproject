using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sync_SelectRemoteItem : System.Web.UI.Page, ICallbackEventHandler
{
    Int32 itemId = 0;
    Int32 priceIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        itemId = Int32.Parse(Request.QueryString["itemID"]);
        priceIndex = Int32.Parse(Request.QueryString["index"]);

        if (!IsPostBack)
        {
            TreeNode root = new TreeNode("所有分类", "E000000");
            this.TreeView1.Nodes.Add(root);

            LoadChildNode(root);

            root.ImageUrl = "~/images/comp2.gif";
            root.Expand();

            LoadInitItem();
        }
    }

    #region 获取品类内容


    protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        LoadChildNode(e.Node);
    }

    protected void LoadChildNode(TreeNode parent)
    {
        List<model.dict.Dict_RemoteTree> nodes = OR.Control.DAL.GetModelList<model.dict.Dict_RemoteTree>("ParentCode=@pid",
            new System.Data.SqlClient.SqlParameter("@pid", parent.Value));

        foreach (model.dict.Dict_RemoteTree item in nodes)
        {
            TreeNode node = new TreeNode(item.NodeName, item.NodeCode);

            node.Expanded = false;

            if (item.IsLeaf == 1)
            {
                node.ImageUrl = "~/images/TextAlignLeft.png";
                node.SelectAction = TreeNodeSelectAction.Select;
                node.NavigateUrl = String.Format("javascript:GetItemList('{0}')", item.NodeCode);
            }
            else
            {
                node.PopulateOnDemand = true;
                node.ImageUrl = "~/images/FileOpen.png";
                node.SelectAction = TreeNodeSelectAction.Expand;
            }

            if (item.Active == 0)
            {
                node.Text = String.Format("<font color='#888888'>{0}</font>", item.NodeName);
            }

            parent.ChildNodes.Add(node);
        }
    }

    private String strArgument;

    public void RaiseCallbackEvent(String eventArgument)
    {
        strArgument = eventArgument;
    }

    public string GetCallbackResult()
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
                        FROM Dict_RemoteItem item
                        LEFT JOIN dbo.Dict_RemoteTree tree ON item.NodeCode = tree.NodeCode
                        LEFT JOIN dbo.Dict_RemoteCata cata ON item.CatalogCode = cata.CataCode
                        LEFT JOIN dbo.Dict_RemoteDictValue dbrand ON item.BrandCode = dbrand.Code
                        LEFT JOIN dbo.Dict_RemoteDictValue dspec ON item.SpecCode = dspec.Code
                        LEFT JOIN dbo.Dict_RemoteDictValue darea ON item.AreaCode = darea.Code
                        WHERE item.NodeCode = @pid";

        System.Data.DataSet ds = OR.DB.SQLHelper.Query(strSQL, new System.Data.SqlClient.SqlParameter("@pid", strArgument));

        try
        {
            List<model.dict.ItemPojo> items = OR.Control.DAL.DataTableToModel<model.dict.ItemPojo>(ds.Tables[0]);

            fastJSON.JSONParameters p = new fastJSON.JSONParameters();
            p.EnableAnonymousTypes = true;

            String json = fastJSON.JSON.Instance.ToJSON(items, p);

            return json;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    #endregion

    protected void LoadInitItem()
    {

        /*
         * 获取指标项自己的详细信息
         */

        model.PriceItems priceItem = OR.Control.DAL.GetModelByKey<model.PriceItems>(itemId);

        this.txtItemName.Text = priceItem.ItemName;

        switch (priceIndex)
        {
            case 1:
                this.txtPriceField.Text = "成交量";
                break;
            case 2:
                this.txtPriceField.Text = "批发价";
                break;
            case 3:
                this.txtPriceField.Text = "农贸零售价";
                break;
            case 4:
                this.txtPriceField.Text = "超市零售价";
                break;
            default:
                break;
        }

        /*
         * 获取两个下拉框的内容
         */
        List<model.dict.Dict_RemoteDictValue> items = OR.Control.DAL.GetModelList<model.dict.Dict_RemoteDictValue>("Type=@t1 OR Type=@t2 Order By Code",
            new System.Data.SqlClient.SqlParameter("@t1", (int)model.dict.RemoteDictType.指标),
            new System.Data.SqlClient.SqlParameter("@t2", (int)model.dict.RemoteDictType.环节));

        foreach (model.dict.Dict_RemoteDictValue item in items)
        {
            switch ((model.dict.RemoteDictType)item.Type)
            {
                case model.dict.RemoteDictType.环节:
                    this.ddpStageCode.Items.Add(new ListItem(item.Name, item.Code));
                    break;
                case model.dict.RemoteDictType.指标:
                    this.ddpTargetCode.Items.Add(new ListItem(item.Name, item.Code));
                    break;
                default:
                    break;
            }
        }

        model.dict.Dict_RemotePriceField field = OR.Control.DAL.GetModel<model.dict.Dict_RemotePriceField>("ItemID=@id And PriceField=@p",
           new System.Data.SqlClient.SqlParameter("@id", priceItem.ItemID),
           new System.Data.SqlClient.SqlParameter("@p", priceIndex));

        if (field != null && !String.IsNullOrEmpty(field.StageCode))
        {
            this.ddpStageCode.SelectedValue = field.StageCode;
        }

        if (field != null && !String.IsNullOrEmpty(field.TargetCode))
        {
            this.ddpTargetCode.SelectedValue = field.TargetCode;
        }

        if (field != null)
        {
            this.txtRate.Text = field.Rate.ToString();
        }
        else
        {
            this.txtRate.Text = "1.0";
        }

        if (field != null)
        {
            model.dict.Dict_RemoteItem dict = OR.Control.DAL.GetModelByKey<model.dict.Dict_RemoteItem>(field.ItemCode);

            if (dict != null)
            {
                this.txtCode.Text = dict.ItemCode;
                this.txtCodeLabel.Text = dict.ItemCode;
                this.txtName.Text = dict.ItemName;
            }
        }

        this.txtPriceDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        model.dict.Dict_RemotePriceField field = OR.Control.DAL.GetModel<model.dict.Dict_RemotePriceField>("ItemID=@id And PriceField=@p",
          new System.Data.SqlClient.SqlParameter("@id", itemId),
          new System.Data.SqlClient.SqlParameter("@p", priceIndex));

        if (field == null)
        {

            field = new model.dict.Dict_RemotePriceField();

            field.ItemID = itemId;
            field.PriceField = priceIndex;

            field.ItemCode = this.txtCode.Text;
            field.Rate = Decimal.Parse(this.txtRate.Text);
            field.StageCode = this.ddpStageCode.SelectedValue;
            field.TargetCode = this.ddpTargetCode.SelectedValue;

            OR.Control.DAL.Add<model.dict.Dict_RemotePriceField>(field, false);
        }
        else
        {
            field.ItemCode = this.txtCode.Text;
            field.Rate = Decimal.Parse(this.txtRate.Text);
            field.StageCode = this.ddpStageCode.SelectedValue;
            field.TargetCode = this.ddpTargetCode.SelectedValue;

            OR.Control.DAL.Update<model.dict.Dict_RemotePriceField>(field);
        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "closeDialog", "parent.Reload();", true);
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        String itemCode = this.txtCode.Text.Trim();

        String ret = String.Empty;
        String unit = String.Empty;

        String resultValue = SyncService.GetPointValue(itemCode, this.txtPriceDate.Text,
            this.ddpTargetCode.SelectedValue, this.ddpStageCode.SelectedValue, out ret, true, out unit);

        if (ret == "000")
        {
            this.txtTestValue.Text = resultValue + " " + unit;
        }
        else
        {
            this.txtTestValue.Text = "错误：" + ret + " [" + GetErrorMsg(ret) + "]";
        }
    }

    protected String GetErrorMsg(String ret)
    {
        if (ret == "001")
        {
            return "没有数据";
        }
        else if (ret == "002")
        {
            return "不在服务时间之内";
        }
        else if (ret == "003")
        {
            return "用户过于频繁访问";
        }
        else if (ret == "004")
        {
            return "用户不合法，IP地址错误";
        }
        else if (ret == "005")
        {
            return "传入参数有误，请核对参数";
        }
        else if (ret == "006")
        {
            return "数据库无法响应";
        }
        else
        {
            return "未知错误";
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        model.dict.Dict_RemotePriceField field = OR.Control.DAL.GetModel<model.dict.Dict_RemotePriceField>("ItemID=@id And PriceField=@p",
          new System.Data.SqlClient.SqlParameter("@id", itemId),
          new System.Data.SqlClient.SqlParameter("@p", priceIndex));

        OR.Control.DAL.Delete<model.dict.Dict_RemotePriceField>(field);

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "closeDialog", "parent.Reload();", true);
    }
}

