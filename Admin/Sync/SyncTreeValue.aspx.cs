using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Sync_SyncTreeValue : System.Web.UI.Page
{
    String pid = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        pid = Request.QueryString["pid"] ?? "E000000";

        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "同步品类数据";

        if (!IsPostBack)
        {

            if (pid.Equals("E000000", StringComparison.OrdinalIgnoreCase))
            {
                this.hyperParent.Visible = false;
            }
            else
            {
                model.dict.Dict_RemoteTree p = OR.Control.DAL.GetModelByKey<model.dict.Dict_RemoteTree>(pid);

                if (p != null)
                {
                    this.hyperParent.Visible = true;
                    this.hyperParent.NavigateUrl = String.Format("./SyncTreeValue.aspx?pid={0}", p.ParentCode);
                    this.txtTreeName.Text = "当前品类：" + p.NodeName;
                }
                else
                {
                    this.hyperParent.Visible = false;
                }
            }

            LoadTreeValue();
        }
    }

    protected void LoadTreeValue()
    {
        List<model.dict.Dict_RemoteTree> cata = OR.Control.DAL.GetModelList<model.dict.Dict_RemoteTree>("ParentCode=@pid", new System.Data.SqlClient.SqlParameter("@pid", pid));

        this.GridView1.DataSource = cata;
        this.GridView1.DataBind();

        this.txtTotalSize.Text = cata.Count.ToString();

        if (cata.Count == 0)
        {
            this.txtLastSyncDate.Text = "";
        }
        else
        {
            this.txtLastSyncDate.Text = cata[0].LastModified.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    protected void btnSync_Click(object sender, EventArgs e)
    {
        XmlDocument _doc = SyncService.GetRemoteContent(SyncService.RemoveService.Node, pid);

        XmlNodeList items = _doc.SelectNodes("root/items/item");

        DateTime now = DateTime.Now;

        foreach (XmlNode item in items)
        {
            String code = item.SelectSingleNode("code").InnerText.Trim();
            String name = item.SelectSingleNode("name").InnerText.Trim();
            String ifactive = item.SelectSingleNode("ifactive").InnerText.Trim();
            String ifleaf = item.SelectSingleNode("ifleaf").InnerText.Trim();

            model.dict.Dict_RemoteTree cata = OR.Control.DAL.GetModelByKey<model.dict.Dict_RemoteTree>(code);

            if (cata == null)
            {
                cata = new model.dict.Dict_RemoteTree();

                cata.Active = Int32.Parse(ifactive);
                cata.NodeCode = code.Trim();
                cata.NodeName = name.Trim();
                cata.IsLeaf = Int32.Parse(ifleaf);
                cata.LastModified = now;
                cata.ParentCode = pid.Trim();

                OR.Control.DAL.Add<model.dict.Dict_RemoteTree>(cata, false);
            }
            else
            {
                cata.Active = Int32.Parse(ifactive);
                cata.NodeName = name.Trim();
                cata.IsLeaf = Int32.Parse(ifleaf);
                cata.LastModified = now;

                OR.Control.DAL.Update<model.dict.Dict_RemoteTree>(cata);
            }
        }

        LoadTreeValue();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        OR.DB.SQLHelper.ExecuteSql("Delete From Dict_RemoteTree where ParentCode=@pid", new System.Data.SqlClient.SqlParameter("@pid", pid));

        LoadTreeValue();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.dict.Dict_RemoteTree cata = e.Row.DataItem as model.dict.Dict_RemoteTree;

            HyperLink hyperLink1 = e.Row.FindControl("HyperLink1") as HyperLink;
            HyperLink hyperLink2 = e.Row.FindControl("HyperLink2") as HyperLink;

            hyperLink1.Text = cata.NodeCode;
            hyperLink2.Text = cata.NodeName;

            if (cata.IsLeaf == 0)
            {
                hyperLink1.NavigateUrl = String.Format("./SyncTreeValue.aspx?pid={0}", cata.NodeCode);
                hyperLink2.NavigateUrl = String.Empty;
            }
            else
            {
                hyperLink1.NavigateUrl = String.Empty;
                hyperLink2.NavigateUrl = String.Format("./SyncTreeItem.aspx?pid={0}", cata.NodeCode);
            }

            if (cata.Active == 0)
            {
                e.Row.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            }
        }
    }
}