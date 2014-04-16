using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category_List : System.Web.UI.Page
{
    protected String pid = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        pid = Request.QueryString["id"];

        if (!IsPostBack)
        {
            LoadFolder();

            LoadItem();

            if (this.gvFolder.Visible && this.gvItem.Visible)
            {
                this.txtSeparator.Visible = true;
            }
            else
            {
                this.txtSeparator.Visible = false;
            }
        }
    }

    protected void LoadFolder()
    {
        List<model.weichat.Dict_Category> c = OR.Control.DAL.GetModelList<model.weichat.Dict_Category>("PID=@pid",
            new System.Data.SqlClient.SqlParameter("@pid", Int32.Parse(pid)));

        if (c.Count == 0)
        {           
            this.gvFolder.Visible = false;
        }
        else
        {
            this.gvFolder.Visible = true;

            this.gvFolder.DataSource = c;
            this.gvFolder.DataBind();
        }
    }

    protected void LoadItem()
    {
        List<model.weichat.Dict_ItemCode> c = OR.Control.DAL.GetModelList<model.weichat.Dict_ItemCode>("CategoryID=@pid",
            new System.Data.SqlClient.SqlParameter("@pid", Int32.Parse(pid)));

        if (c.Count == 0)
        {
            this.gvItem.Visible = false;
        }
        else
        {
            this.gvItem.Visible = true;

            this.gvItem.DataSource = c;
            this.gvItem.DataBind();
        }
    }

    protected void gvFolder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.weichat.Dict_Category c = (model.weichat.Dict_Category)e.Row.DataItem;

            LinkButton linkDel = e.Row.FindControl("linkDel") as LinkButton;

            linkDel.CommandName = "del";
            linkDel.CommandArgument = c.CategoryID.ToString();

            LinkButton linkMod = e.Row.FindControl("linkMod") as LinkButton;
            linkMod.OnClientClick = String.Format("EditCategory({0}); return false;", c.CategoryID.ToString());

            LinkButton linkMov = e.Row.FindControl("linkMov") as LinkButton;
            linkMov.OnClientClick = String.Format("MoveCategory({0}); return false;", c.CategoryID.ToString());
        }
    }

    protected void gvFolder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            model.weichat.Dict_Category c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_Category>(Int32.Parse(e.CommandArgument.ToString()));
            OR.Control.DAL.Delete<model.weichat.Dict_Category>(c);

            LoadFolder();
        }
    }

    protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            model.weichat.Dict_ItemCode c = (model.weichat.Dict_ItemCode)e.Row.DataItem;

            LinkButton linkDel = e.Row.FindControl("linkDel") as LinkButton;

            linkDel.CommandName = "del";
            linkDel.CommandArgument = c.CategoryID.ToString();

            LinkButton linkMod = e.Row.FindControl("linkMod") as LinkButton;
            linkMod.OnClientClick = String.Format("EditItem({0}); return false;", c.ItemID.ToString());

            LinkButton linkMov = e.Row.FindControl("linkMov") as LinkButton;
            linkMov.OnClientClick = String.Format("MoveItem({0}); return false;", c.ItemID.ToString());

        }
    }

    protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            model.weichat.Dict_ItemCode c = OR.Control.DAL.GetModelByKey<model.weichat.Dict_ItemCode>(Int32.Parse(e.CommandArgument.ToString()));
            OR.Control.DAL.Delete<model.weichat.Dict_ItemCode>(c);

            LoadFolder();
        }
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        String str = this.txtAction.Text;

        //Action action = Newtonsoft.Json.JsonConvert.DeserializeObject(str, typeof(Action)) as Action;

        //if (action.Type.Equals("Category"))
        //{
        //    Response.Write("MoveCategory");
        //}
        //else if (action.Type.Equals("Item"))
        //{
        //    Response.Write("MoveItem");
        //}
        //else
        //{
        //    Response.Write("No Action");
        //}
    }
}