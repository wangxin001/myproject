using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Sync_SyncTreeCata : System.Web.UI.Page
{
    String pid = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        pid = Request.QueryString["pid"];

        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "同步品种";

        if (!IsPostBack)
        {

            this.ddpSyncType.Items.Add(new ListItem("指标业务对象", "2"));
            this.ddpSyncType.Items.Add(new ListItem("品种", "3"));

            this.ddpSyncType.SelectedIndex = 1;

            if (String.IsNullOrEmpty(pid))
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

            LoadTreeCatas();
        }
    }

    protected void LoadTreeCatas()
    {
        List<model.dict.Dict_RemoteCata> cata = OR.Control.DAL.GetModelList<model.dict.Dict_RemoteCata>("NodeCode=@pid", new System.Data.SqlClient.SqlParameter("@pid", pid));

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
        XmlDocument _doc = SyncService.GetRemoteContent(SyncService.RemoveService.Catas, pid);

        XmlNodeList items = _doc.SelectNodes("root/items/item");

        DateTime now = DateTime.Now;

        foreach (XmlNode item in items)
        {
            String code = item.SelectSingleNode("code").InnerText.Trim();
            String name = item.SelectSingleNode("name").InnerText.Trim();
            String ifactive = item.SelectSingleNode("ifactive").InnerText.Trim();

            model.dict.Dict_RemoteCata cataItem = OR.Control.DAL.GetModelByKey<model.dict.Dict_RemoteCata>(code);

            if (cataItem == null)
            {
                cataItem = new model.dict.Dict_RemoteCata();

                cataItem.CataCode = code;
                cataItem.CataName = name;
                cataItem.Active = Int32.Parse(ifactive);
                cataItem.LastModified = now;
                cataItem.NodeCode = pid;

                OR.Control.DAL.Add<model.dict.Dict_RemoteCata>(cataItem, false);
            }
            else
            {
                cataItem.CataName = name;
                cataItem.Active = Int32.Parse(ifactive);
                cataItem.LastModified = now;
                cataItem.NodeCode = pid;

                OR.Control.DAL.Update<model.dict.Dict_RemoteCata>(cataItem);
            }
        }

        LoadTreeCatas();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        OR.DB.SQLHelper.ExecuteSql("Delete From Dict_RemoteCata where NodeCode=@pid", new System.Data.SqlClient.SqlParameter("@pid", pid));

        LoadTreeCatas();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.dict.Dict_RemoteCata item = e.Row.DataItem as model.dict.Dict_RemoteCata;

            if (item.Active == 0)
            {
                e.Row.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            }
        }
    }

    protected void ddpSyncType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddpSyncType.SelectedItem.Value == "2")
        {
            Response.Redirect("./SyncTreeItem.aspx?pid=" + pid);
        }
        else
        {
            Response.Redirect("./SyncTreeCata.aspx?pid=" + pid);
        }
    }
}