using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class MasterPage_AdminPage : System.Web.UI.MasterPage
{

    String cid = String.Empty;

    #region 页面基本功能

    protected void Page_Load(object sender, EventArgs e)
    {
        cid = Request.QueryString["id"];

        if (!IsPostBack)
        {
            model.UserInfo user = Util.GetLoginUserInfo();
            this.labUserName.Text = user.UserName;

            LoadMenuTree();
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Log.info("用户注销: " + Util.GetLoginUserInfo().UserName);

        System.Web.Security.FormsAuthentication.SignOut();
        System.Web.Security.FormsAuthentication.RedirectToLoginPage();
    }

    /// <summary>
    /// 设置页面标题
    /// </summary>
    public String PageTitle
    {
        set
        {
            Page.Title = "价格监测网管理网站-" + value;
        }
    }
    #endregion

    #region 对菜单内容进行初始化

    /// <summary>
    /// 初始化菜单树
    /// </summary>
    protected void LoadMenuTree()
    {
        TreeNode root = new TreeNode("微信内容管理");
        root.SelectAction = TreeNodeSelectAction.Expand;
        root.ImageUrl = "~/images/comp2.gif";
        root.Expanded = true;
        this.TreeView1.Nodes.Add(root);

        LoadCategory(root);
        LoadSysAdmin(root);
    }

    #region Category


    protected void LoadCategory(TreeNode root)
    {
        TreeNode nodeCategory = new TreeNode("分类管理", "0");
        nodeCategory.SelectAction = TreeNodeSelectAction.SelectExpand;
        nodeCategory.Expanded = true;
        nodeCategory.ImageUrl = "~/images/ShapeStar.png";
        nodeCategory.NavigateUrl = "~/Category/List.aspx?id=0";
        root.ChildNodes.Add(nodeCategory);

        if (Request.Cookies["Category_0"] != null)
        {
            nodeCategory.Expanded = (Request.Cookies["Category_0"] == null ? true : Boolean.Parse(Request.Cookies["Category_0"].Value.ToString()));
        }

        dt = OR.DB.SQLHelper.Query("Select * From Dict_Category WHERE 1=1 Order By CategoryID").Tables[0];

        FulfillNode(nodeCategory);
    }

    DataTable dt = null;

    protected void FulfillNode(TreeNode pNode)
    {

        DataRow[] rows = dt.Select("PID=" + pNode.Value);

        for (int i = 0; i < rows.Length; i++)
        {
            TreeNode node = new TreeNode(rows[i]["CategoryName"].ToString(), rows[i]["CategoryID"].ToString());
            node.SelectAction = TreeNodeSelectAction.SelectExpand;
            node.Expanded = false;
            node.ImageUrl = "~/images/FileOpen.png";
            node.NavigateUrl = "~/Category/List.aspx?id=" + node.Value;
            pNode.ChildNodes.Add(node);

            if (rows[i]["CategoryName"].ToString() != "中药" && rows[i]["CategoryName"].ToString() != "西药")
            {
                FulfillNode(node);
            }

            if (Request.Cookies["Category_" + rows[i]["CategoryID"].ToString()] != null)
            {
                node.Expanded = (Request.Cookies["Category_" + rows[i]["CategoryID"].ToString()] == null ? true : Boolean.Parse(Request.Cookies["Category_" + rows[i]["CategoryID"].ToString()].Value.ToString()));
            }

            Int32 id = 0;

            if (!String.IsNullOrEmpty(cid) && Int32.TryParse(cid, out id) && id.ToString().Equals(rows[i]["CategoryID"].ToString()))
            {
                node.Select();
            }
        }
    }
    #endregion

    protected void LoadSysAdmin(TreeNode root)
    {
        TreeNode nodeSysAdmin= new TreeNode("系统管理", "SysAdmin");
        nodeSysAdmin.SelectAction = TreeNodeSelectAction.None;
        nodeSysAdmin.Expanded = true;
        nodeSysAdmin.ImageUrl = "~/images/icon_admin.png";
        root.ChildNodes.Add(nodeSysAdmin);

        if (Request.Cookies["SysAdmin"] != null)
        {
            nodeSysAdmin.Expanded = (Request.Cookies["SysAdmin"] == null ? true : Boolean.Parse(Request.Cookies["SysAdmin"].Value.ToString()));
        }

        TreeNode nodePrice = new TreeNode("价格类型管理", "SysAdminPrice");
        nodePrice.SelectAction = TreeNodeSelectAction.Select;
        nodePrice.Expanded = true;
        nodePrice.NavigateUrl = "~/SysAdmin/Price.aspx";
        nodePrice.ImageUrl = "~/images/icon_showtable.png";
        nodeSysAdmin.ChildNodes.Add(nodePrice);

        TreeNode nodeSite = new TreeNode("价格上报站点", "SysAdminSite");
        nodeSite.SelectAction = TreeNodeSelectAction.Select;
        nodeSite.Expanded = true;
        nodeSite.NavigateUrl = "~/SysAdmin/Site.aspx";
        nodeSite.ImageUrl = "~/images/icon_showtable.png";
        nodeSysAdmin.ChildNodes.Add(nodeSite);

        TreeNode nodeUnit = new TreeNode("价格单位管理", "SysAdminUnit");
        nodeUnit.SelectAction = TreeNodeSelectAction.Select;
        nodeUnit.Expanded = true;
        nodeUnit.NavigateUrl = "~/SysAdmin/Unit.aspx";
        nodeUnit.ImageUrl = "~/images/icon_showtable.png";
        nodeSysAdmin.ChildNodes.Add(nodeUnit);
    }

    #endregion

    protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        Response.Cookies.Set(new HttpCookie("Category_" + e.Node.Value, "true"));
    }

    protected void TreeView1_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
    {
        Response.Cookies.Set(new HttpCookie("Category_" + e.Node.Value, "false"));
    }
}
