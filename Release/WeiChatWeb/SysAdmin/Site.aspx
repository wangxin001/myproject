<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="SysAdmin_Site, WeiChatWeb" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript">

        function CloseFrame() {
            $('#dialog').dialog('close');
        }

        function CreateSite() {
            document.getElementById("dialogContent").src = "./SiteEdit.aspx";

            $("#dialog").dialog({ title: '添加价格采集点', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function EditSite(id) {
            document.getElementById("dialogContent").src = "./SiteEdit.aspx?id=" + pid;

            $("#dialog").dialog({ title: '修改价格采集点', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function ReloadPage() {
            document.location.href = document.location.href;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：价格采集点管理
    </div>
    <div class="divToolBar">
        <asp:Button ID="btnAdd" runat="server" Text="添加采集点" OnClientClick="javascript:CreateSite();return false;" />
    </div>
    <asp:GridView ID="gvSite" runat="server" OnRowDataBound="gvSite_RowDataBound" OnRowCommand="gvSite_RowCommand">
        <Columns>
            <asp:BoundField DataField="SiteID" HeaderText="编号" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="SiteName" HeaderText="名称" ItemStyle-CssClass="ItemLeft" />
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150px">
                <ItemTemplate>
                    <asp:LinkButton ID="linkMod" runat="server">修改</asp:LinkButton>
                    <asp:LinkButton ID="linkDel" runat="server" OnClientClick="return confirm('确认删除当前采集点？')">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;"></iframe>
    </div>
</asp:Content>

