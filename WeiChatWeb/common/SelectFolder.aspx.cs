using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class common_SelectFolder : System.Web.UI.Page
{
    protected String type = String.Empty;

    String strFormat = "<input type=\"checkbox\" id=\"{0}\" value=\"{1}\" onclick=\"javascript:SelectObj(this)\" />{1}";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TreeNode root = new TreeNode("分类", "0");
            root.SelectAction = TreeNodeSelectAction.Expand;
            //root.ImageUrl = "~/images/comp2.gif";
            root.Expanded = true;
            this.treeMenu.Nodes.Add(root);

            LoadChildNode(root);
        }
    }

    protected void LoadChildNode(TreeNode pNode)
    {
        List<model.weichat.Dict_Category> categories = OR.Control.DAL.GetModelList<model.weichat.Dict_Category>("PID=@pid", new System.Data.SqlClient.SqlParameter("@pid", Int32.Parse(pNode.Value)));

        foreach (model.weichat.Dict_Category category in categories)
        {
            TreeNode nodeRegion = new TreeNode(category.CategoryName, "" + category.CategoryID);
            nodeRegion.SelectAction = TreeNodeSelectAction.Expand;
            nodeRegion.ImageUrl = "~/images/tree_folder.gif";
            nodeRegion.PopulateOnDemand = true;
            nodeRegion.Expanded = false;
            nodeRegion.Text = String.Format(strFormat, category.CategoryID, category.CategoryName);

            pNode.ChildNodes.Add(nodeRegion);
        }
    }

    protected void treeMenu_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        LoadChildNode(e.Node);
    }
}