using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_AdminPage : System.Web.UI.MasterPage
{

    #region 页面基本功能

    protected void Page_Load(object sender, EventArgs e)
    {
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

        TreeNode root = new TreeNode("网站内容管理");
        root.SelectAction = TreeNodeSelectAction.Expand;
        root.ImageUrl = "~/images/comp2.gif";
        root.Expanded = true;
        this.TreeView1.Nodes.Add(root);

        TreeNode nodeBoard = new TreeNode("新闻栏目管理", "InfoPub");
        nodeBoard.SelectAction = TreeNodeSelectAction.SelectExpand;
        nodeBoard.Expanded = false;
        nodeBoard.ImageUrl = "~/images/FileOpen.png";
        nodeBoard.NavigateUrl = "~/InfoPub/Default.aspx";
        root.ChildNodes.Add(nodeBoard);

        LoadBoardInfo(nodeBoard);

        if (Request.Cookies["InfoPub"] != null)
        {
            nodeBoard.Expanded = (Request.Cookies["InfoPub"] == null ? true : Boolean.Parse(Request.Cookies["InfoPub"].Value.ToString()));
        }

        TreeNode nodePrice = new TreeNode("价格管理","Price");
        nodePrice.SelectAction = TreeNodeSelectAction.Expand;
        nodePrice.Expanded = false;
        nodePrice.ImageUrl = "~/images/FileOpen.png";
        root.ChildNodes.Add(nodePrice);

        LoadPriceInfo(nodePrice);

        if (Request.Cookies["Price"] != null)
        {
            nodePrice.Expanded = (Request.Cookies["Price"] == null ? true : Boolean.Parse(Request.Cookies["Price"].Value.ToString()));
        }

        TreeNode nodeSurvey = new TreeNode("调查问卷","Vote");
        nodeSurvey.SelectAction = TreeNodeSelectAction.Expand;
        nodeSurvey.Expanded = false;
        nodeSurvey.ImageUrl = "~/images/FileOpen.png";
        root.ChildNodes.Add(nodeSurvey);

        LoadSurveyInfo(nodeSurvey);

        if (Request.Cookies["Vote"] != null)
        {
            nodeSurvey.Expanded = (Request.Cookies["Vote"] == null ? true : Boolean.Parse(Request.Cookies["Vote"].Value.ToString()));
        }


        TreeNode nodeUser = new TreeNode("用户管理","UserMgmt");
        nodeUser.SelectAction = TreeNodeSelectAction.Expand;
        nodeUser.Expanded = false;
        nodeUser.ImageUrl = "~/images/FileOpen.png";
        root.ChildNodes.Add(nodeUser);

        LoadUserInfo(nodeUser);

        if (Request.Cookies["UserMgmt"] != null)
        {
            nodeUser.Expanded = (Request.Cookies["UserMgmt"] == null ? true : Boolean.Parse(Request.Cookies["UserMgmt"].Value.ToString()));
        }


        TreeNode nodeReport = new TreeNode("上报菜价管理", "ReportMgmt");
        nodeReport.SelectAction = TreeNodeSelectAction.Expand;
        nodeReport.Expanded = false;
        nodeReport.ImageUrl = "~/images/FileOpen.png";
        root.ChildNodes.Add(nodeReport);

        LoadReportInfo(nodeReport);

        if (Request.Cookies["ReportMgmt"] != null)
        {
            nodeReport.Expanded = (Request.Cookies["ReportMgmt"] == null ? true : Boolean.Parse(Request.Cookies["ReportMgmt"].Value.ToString()));
        }

        TreeNode nodeSync = new TreeNode("数据同步管理", "SyncMgmt");
        nodeSync.SelectAction = TreeNodeSelectAction.Expand;
        nodeSync.Expanded = false;
        nodeSync.ImageUrl = "~/images/FileOpen.png";
        root.ChildNodes.Add(nodeSync);

        LoadSyncInfo(nodeSync);

        if (Request.Cookies["SyncMgmt"] != null)
        {
            nodeSync.Expanded = (Request.Cookies["SyncMgmt"] == null ? true : Boolean.Parse(Request.Cookies["SyncMgmt"].Value.ToString()));
        }
    }

    protected void LoadBoardInfo(TreeNode boardNode)
    {
        /*
         * 新闻栏目当前分为4类：新闻类，图片类，链接类，下载类，各自用途如下：
         * 新闻类栏目：
         *          当前类型的栏目主要用于普通的新闻栏目发布，自己录入图文混排的新闻内容，然后在这里
         *          发布出来。在发布的时候，指定发布者、发布日期等。控制其是否显示。
         *          显示的内容顺序在管理界面上已创建时间为准，新闻发布页面已发布时间为准。
         *          发布时间可以不是创建时间。
         * 
         * 图片类新闻：
         *          用于两类地方：一个是首页的滚动图片，在那里可以挑选某几个图片显示在首页上，用作自动
         *          切换显示。每个图片有以下属性信息：标题，链接地址，部分新闻内容。此处链接可链接外部
         *          地址，也可链接自己的新闻内容。
         *          图片是作为附件上传到服务器上的。看需要决定是否需要加一个附件表。
         *          
         * 问答类：
         *          可以有提问，有回答。此部分的处理方式与新闻类一样，只是显示样式不同，只显示问题标题
         *          和回答内容。
         *          
         * 下载类：
         *          上传一个文件，作为附件，这里进行下载。有标题，描述内容，附件。
         * 
         * 现在暂时只处理新闻类和图片类的东西。
         */
        boardNode.ChildNodes.Clear();

        List<model.InfoBoardInfo> boards = OR.Control.DAL.GetModelList<model.InfoBoardInfo>("1=1 Order By BoardType, Created");

        foreach (model.InfoBoardInfo board in boards)
        {
            TreeNode nodeBoard = new TreeNode(board.BoardTitle, "Board_" + board.BoardID.ToString());
            nodeBoard.SelectAction = TreeNodeSelectAction.Select;
            nodeBoard.Expanded = false;

            switch ((model.BoardType)board.BoardType)
            {
                case model.BoardType.InfoPub:
                    nodeBoard.NavigateUrl = "~/InfoPub/TopicList.aspx?boardid=" + board.BoardID.ToString();
                    nodeBoard.ImageUrl = "~/images/TextBoxInsertWord.png";
                    break;
                case model.BoardType.Picture:
                    nodeBoard.NavigateUrl = "~/InfoPub/PictureList.aspx?boardid=" + board.BoardID.ToString();
                    nodeBoard.ImageUrl = "~/images/TextPictureFill.png";
                    break;
                case model.BoardType.QAndA:
                    nodeBoard.NavigateUrl = "~/InfoPub/QAList.aspx?boardid=" + board.BoardID.ToString();
                    nodeBoard.ImageUrl = "~/images/faq.gif";
                    break;
                case model.BoardType.File:
                    nodeBoard.NavigateUrl = "~/InfoPub/TopicList.aspx?boardid=" + board.BoardID.ToString();
                    nodeBoard.ImageUrl = "~/images/SaveAttachments.png";
                    break;
                default:
                    break;
            }


            boardNode.ChildNodes.Add(nodeBoard);
        }
    }

    protected void LoadPriceInfo(TreeNode priceNode)
    {

        TreeNode nodeCaijia = new TreeNode("菜价走势", "CJ");
        nodeCaijia.SelectAction = TreeNodeSelectAction.Select;
        nodeCaijia.Expanded = false;
        nodeCaijia.NavigateUrl = "~/Price/PriceIndex.aspx?PriceItem=" + ((int)model.PriceType.Chart_CaiJia).ToString();
        nodeCaijia.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeCaijia);

        TreeNode nodeFQF = new TreeNode("富强粉走势", "FQF");
        nodeFQF.SelectAction = TreeNodeSelectAction.Select;
        nodeFQF.Expanded = false;
        nodeFQF.NavigateUrl = "~/Price/PriceIndex.aspx?PriceItem=" + ((int)model.PriceType.Chart_FuQiangFen).ToString();
        nodeFQF.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeFQF);

        TreeNode nodeJM = new TreeNode("粳米走势", "JM");
        nodeJM.SelectAction = TreeNodeSelectAction.Select;
        nodeJM.Expanded = false;
        nodeJM.NavigateUrl = "~/Price/PriceIndex.aspx?PriceItem=" + ((int)model.PriceType.Chart_JingMi).ToString();
        nodeJM.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeJM);

        TreeNode nodeTHY = new TreeNode("调和油走势", "THY");
        nodeTHY.SelectAction = TreeNodeSelectAction.Select;
        nodeTHY.Expanded = false;
        nodeTHY.NavigateUrl = "~/Price/PriceIndex.aspx?PriceItem=" + ((int)model.PriceType.Chart_TiaoHeYou).ToString();
        nodeTHY.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeTHY);

        TreeNode nodeZR = new TreeNode("猪肉走势", "ZR");
        nodeZR.SelectAction = TreeNodeSelectAction.Select;
        nodeZR.Expanded = false;
        nodeZR.NavigateUrl = "~/Price/PriceIndex.aspx?PriceItem=" + ((int)model.PriceType.Chart_ZhuRou).ToString();
        nodeZR.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeZR);

        TreeNode nodeJD = new TreeNode("鸡蛋走势", "JD");
        nodeJD.SelectAction = TreeNodeSelectAction.Select;
        nodeJD.Expanded = false;
        nodeJD.NavigateUrl = "~/Price/PriceIndex.aspx?PriceItem=" + ((int)model.PriceType.Chart_JiDan).ToString();
        nodeJD.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeJD);


        TreeNode nodeRange = new TreeNode("价格区间管理", "JD");
        nodeRange.SelectAction = TreeNodeSelectAction.Select;
        nodeRange.Expanded = false;
        nodeRange.NavigateUrl = "~/Price/PriceRange.aspx";
        nodeRange.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeRange);

        TreeNode nodeSpa1 = new TreeNode("----------", "--------");
        nodeSpa1.SelectAction = TreeNodeSelectAction.None;
        priceNode.ChildNodes.Add(nodeSpa1);

        TreeNode nodeVegetable = new TreeNode("每日菜价价格录入", "Vegetable");
        nodeVegetable.SelectAction = TreeNodeSelectAction.Select;
        nodeVegetable.Expanded = false;
        nodeVegetable.NavigateUrl = "~/Price/VegetablePrice.aspx";
        nodeVegetable.ImageUrl = "~/images/ShapeStar.png";
        priceNode.ChildNodes.Add(nodeVegetable);

        TreeNode nodePriceItem = new TreeNode("每日菜价条目管理", "PriceItem");
        nodePriceItem.SelectAction = TreeNodeSelectAction.Select;
        nodePriceItem.Expanded = false;
        nodePriceItem.NavigateUrl = "~/Price/PriceItems.aspx";
        nodePriceItem.ImageUrl = "~/images/TextAlignLeft.png";
        priceNode.ChildNodes.Add(nodePriceItem);

        TreeNode nodeSpa = new TreeNode("----------", "--------");
        nodeSpa.SelectAction = TreeNodeSelectAction.None;
        priceNode.ChildNodes.Add(nodeSpa);

        TreeNode nodeCargo = new TreeNode("大宗商品价格录入", "Cargo");
        nodeCargo.SelectAction = TreeNodeSelectAction.Select;
        nodeCargo.Expanded = false;
        nodeCargo.NavigateUrl = "~/Price/CargoPrice.aspx";
        nodeCargo.ImageUrl = "~/images/cargo1.gif";
        priceNode.ChildNodes.Add(nodeCargo);

        TreeNode nodeVPI = new TreeNode("蔬菜价格指数录入", "XPI");
        nodeVPI.SelectAction = TreeNodeSelectAction.Select;
        nodeVPI.Expanded = false;
        nodeVPI.NavigateUrl = "~/Price/VPIPrice.aspx";
        nodeVPI.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeVPI);


        TreeNode nodeYuanYou = new TreeNode("原油价格指数录入", "YuanYou");
        nodeYuanYou.SelectAction = TreeNodeSelectAction.Select;
        nodeYuanYou.Expanded = false;
        nodeYuanYou.NavigateUrl = "~/Price/YuanYouPrice.aspx";
        nodeYuanYou.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeYuanYou);


        TreeNode nodeCPI = new TreeNode("消费价格指数录入", "CPI");
        nodeCPI.SelectAction = TreeNodeSelectAction.Select;
        nodeCPI.Expanded = false;
        nodeCPI.NavigateUrl = "~/Price/CPIPrice.aspx";
        nodeCPI.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeCPI);


        TreeNode nodeDaDou = new TreeNode("大豆价格指数录入", "DaDou");
        nodeDaDou.SelectAction = TreeNodeSelectAction.Select;
        nodeDaDou.Expanded = false;
        nodeDaDou.NavigateUrl = "~/Price/DaDouPrice.aspx";
        nodeDaDou.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeDaDou);

        TreeNode nodeYuMi = new TreeNode("玉米价格指数录入", "YuMi");
        nodeYuMi.SelectAction = TreeNodeSelectAction.Select;
        nodeYuMi.Expanded = false;
        nodeYuMi.NavigateUrl = "~/Price/YuMiPrice.aspx";
        nodeYuMi.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeYuMi);

        TreeNode nodeXiaoMai = new TreeNode("小麦价格指数录入", "XiaoMai");
        nodeXiaoMai.SelectAction = TreeNodeSelectAction.Select;
        nodeXiaoMai.Expanded = false;
        nodeXiaoMai.NavigateUrl = "~/Price/XiaoMaiPrice.aspx";
        nodeXiaoMai.ImageUrl = "~/images/line.gif";
        priceNode.ChildNodes.Add(nodeXiaoMai);

    }

    protected void LoadSurveyInfo(TreeNode surveyNode)
    {
        TreeNode nodeSurvry = new TreeNode("调查管理", "Survey");
        nodeSurvry.SelectAction = TreeNodeSelectAction.Select;
        nodeSurvry.Expanded = false;
        nodeSurvry.NavigateUrl = "~/Survey/Default.aspx";
        nodeSurvry.ImageUrl = "~/images/pollsmall.gif";
        surveyNode.ChildNodes.Add(nodeSurvry);
    }

    protected void LoadUserInfo(TreeNode userNode)
    {
        TreeNode nodeAccount = new TreeNode("帐号/密码管理", "User_Account");
        nodeAccount.SelectAction = TreeNodeSelectAction.Select;
        nodeAccount.Expanded = false;
        nodeAccount.NavigateUrl = "~/UserAdmin/UserList.aspx";
        nodeAccount.ImageUrl = "~/images/ShowMembersPage.png";
        userNode.ChildNodes.Add(nodeAccount);

        //TreeNode nodePwd = new TreeNode("修改密码", "User_ChangePwd");
        //nodePwd.SelectAction = TreeNodeSelectAction.Select;
        //nodePwd.Expanded = false;
        //nodePwd.NavigateUrl = "~/UserAdmin/ChangePwd.aspx";
        //nodePwd.ImageUrl = "~/images/ProtectMenu.png";
        //userNode.ChildNodes.Add(nodePwd);
    }

    protected void LoadReportInfo(TreeNode reportNode)
    {

        TreeNode nodeUser = new TreeNode("菜店信息管理", "ReportUser");
        nodeUser.SelectAction = TreeNodeSelectAction.Select;
        nodeUser.Expanded = false;
        nodeUser.NavigateUrl = "~/Report/UserList.aspx";
        nodeUser.ImageUrl = "~/images/ShowMembersPage.png";
        reportNode.ChildNodes.Add(nodeUser);
    }

    protected void LoadSyncInfo(TreeNode syncNode)
    {
        TreeNode nodeDict = new TreeNode("同步字典数据", "SyncDict");
        nodeDict.SelectAction = TreeNodeSelectAction.Select;
        nodeDict.Expanded = false;
        nodeDict.NavigateUrl = "~/Sync/SyncDictValue.aspx";
        nodeDict.ImageUrl = "~/images/sync.png";
        syncNode.ChildNodes.Add(nodeDict);


        TreeNode nodeTree = new TreeNode("同步品类数据", "SyncTree");
        nodeTree.SelectAction = TreeNodeSelectAction.Select;
        nodeTree.Expanded = false;
        nodeTree.NavigateUrl = "~/Sync/SyncTreeValue.aspx";
        nodeTree.ImageUrl = "~/images/sync.png";
        syncNode.ChildNodes.Add(nodeTree);

        //TreeNode nodeCombine = new TreeNode("关联数据条目", "SyncCombine");
        //nodeCombine.SelectAction = TreeNodeSelectAction.Select;
        //nodeCombine.Expanded = false;
        //nodeCombine.NavigateUrl = "~/Sync/CombineItem.aspx";
        //nodeCombine.ImageUrl = "~/images/icon_admin.png";
        //syncNode.ChildNodes.Add(nodeCombine);

        TreeNode nodePrice = new TreeNode("同步每日价格", "SyncPrice");
        nodePrice.SelectAction = TreeNodeSelectAction.Select;
        nodePrice.Expanded = false;
        nodePrice.NavigateUrl = "~/Sync/SyncItemPrice.aspx";
        nodePrice.ImageUrl = "~/images/icon_showtable.png";
        syncNode.ChildNodes.Add(nodePrice);

        TreeNode nodeTrend = new TreeNode("同步价格走势", "SyncTrend");
        nodeTrend.SelectAction = TreeNodeSelectAction.Select;
        nodeTrend.Expanded = false;
        nodeTrend.NavigateUrl = "~/Sync/SyncItemTrend.aspx";
        nodeTrend.ImageUrl = "~/images/icon_linechart.png";
        syncNode.ChildNodes.Add(nodeTrend);
    }

    #endregion

    protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        Response.Cookies.Set(new HttpCookie(e.Node.Value, "true"));
    }

    protected void TreeView1_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
    {
        Response.Cookies.Set(new HttpCookie(e.Node.Value, "false"));
    }
}
