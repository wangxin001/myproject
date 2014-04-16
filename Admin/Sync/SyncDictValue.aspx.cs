using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Sync_SyncDictValue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_AdminPage master = (MasterPage_AdminPage)this.Master;
        master.PageTitle = "同步字典数据";

        if (!IsPostBack)
        {
            LoadKeyValueType();

            LoadDictValue();
        }
    }

    protected void LoadKeyValueType()
    {
        this.ddpKeyValueType.Items.Clear();

        foreach (model.dict.RemoteDictType item in Enum.GetValues(typeof(model.dict.RemoteDictType)))
        {
            this.ddpKeyValueType.Items.Add(new ListItem(item.ToString(), ((int)item).ToString()));
        }
    }

    protected void btnSync_Click(object sender, EventArgs e)
    {
        XmlDocument _doc = SyncService.GetRemoteContent(SyncService.RemoveService.DictValue, this.ddpKeyValueType.SelectedItem.Value);

        XmlNodeList items = _doc.SelectNodes("root/items/item");

        DateTime now = DateTime.Now;

        foreach (XmlNode item in items)
        {
            String type = this.ddpKeyValueType.SelectedItem.Value;
            String code = item.SelectSingleNode("code").InnerText.Trim();
            String name = item.SelectSingleNode("name").InnerText.Trim();
            String ifactive = item.SelectSingleNode("ifactive").InnerText.Trim();

            model.dict.Dict_RemoteDictValue dict = OR.Control.DAL.GetModelByKey<model.dict.Dict_RemoteDictValue>(code);

            if (dict == null)
            {
                dict = new model.dict.Dict_RemoteDictValue();
                dict.Active = Int32.Parse(ifactive);
                dict.Code = code;
                dict.LastModified = now;
                dict.Name = name;
                dict.Type = Int32.Parse(type);

                OR.Control.DAL.Add<model.dict.Dict_RemoteDictValue>(dict, false);
            }
            else
            {
                dict.Active = Int32.Parse(ifactive);
                dict.LastModified = now;
                dict.Name = name;
                dict.Type = Int32.Parse(type);

                OR.Control.DAL.Update<model.dict.Dict_RemoteDictValue>(dict);
            }
        }

        LoadDictValue();
    }

    protected void LoadDictValue()
    {
        List<model.dict.Dict_RemoteDictValue> dicts = OR.Control.DAL.GetModelList<model.dict.Dict_RemoteDictValue>("Type=@type",
            new System.Data.SqlClient.SqlParameter("@type", Int32.Parse(this.ddpKeyValueType.SelectedItem.Value)));

        this.GridView1.DataSource = dicts;
        this.GridView1.DataBind();

        this.txtTotalSize.Text = dicts.Count.ToString();

        if (dicts.Count == 0)
        {
            this.txtLastSyncDate.Text = "";
        }
        else
        {
            this.txtLastSyncDate.Text = dicts[0].LastModified.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ClearValue();

        LoadDictValue();
    }

    protected void ClearValue()
    {
        OR.DB.SQLHelper.ExecuteSql("Delete From Dict_RemoteDictValue Where Type=@type",
            new System.Data.SqlClient.SqlParameter("@type", Int32.Parse(this.ddpKeyValueType.SelectedItem.Value)));

    }
    protected void ddpKeyValueType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDictValue();
    }
}