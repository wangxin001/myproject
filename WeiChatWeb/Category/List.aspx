<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Category_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript">

        var action = {};

        function CloseFrame() {
            $('#dialog').dialog('close');
        }

        function CreateCategory() {
            $("#dialogContent").attr("src", "./Edit.aspx?pid=<%=pid %>");

            $("#dialog").dialog({ title: '新建分类', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function EditCategory(sid) {
            $("#dialogContent").attr("src", "./Edit.aspx?id=" + sid);

            $("#dialog").dialog({ title: '修改分类', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function MoveCategory(sid) {

            action = {
                Type: "Category",
                Category: sid,
                ItemID: 0
            };

            $("#dialogContent").attr("src", "../common/SelectFolder.aspx");

            $("#dialog").dialog({ title: '修改分类', resizable: false, width: 570, height: 520, modal: true });
            $('#dialog').dialog('open');
        }

        function CreateItem() {
            $("#dialogContent").attr("src", "./Item.aspx?pid=<%=pid %>");

            $("#dialog").dialog({ title: '新建条目', resizable: false, width: 400, height: 250, modal: true });
            $('#dialog').dialog('open');
        }

        function EditItem(sid) {

            $("#dialogContent").attr("src", "./Item.aspx?id=" + sid);

            $("#dialog").dialog({ title: '修改条目', resizable: false, width: 400, height: 250, modal: true });
            $('#dialog').dialog('open');
        }

        function MoveItem(sid) {

            action = {
                Type: "Item",
                Category: 0,
                ItemID: sid
            };

            $("#dialogContent").attr("src", "../common/SelectFolder.aspx");

            $("#dialog").dialog({ title: '选择分类', resizable: false, width: 600, height: 550, modal: true });
            $('#dialog').dialog('open');
        }

        function GetResultValue(id, name) {

            if (id.length != 1) {
                alert("请选择一个分类");
                return false;
            }

            action.TargetID = parseInt(id[0]);

            $("#<%=txtAction.ClientID%>").val(JSON.stringify(action));
            $("#<%=btnAction.ClientID%>").click();
        }

        function ReloadPage() {
            document.location.href = document.location.href;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divCurrentPosition">
        当前位置：分类/条目管理
    </div>
    <div class="divToolBar">
        <asp:Button ID="btnCategory" runat="server" Text="添加分类" OnClientClick="javascript:CreateCategory();return false;" />
        <asp:Button ID="btnItem" runat="server" Text="添加条目" OnClientClick="javascript:CreateItem();return false;" />
        <asp:Button ID="btnAction" runat="server" Text="Action" CssClass="noneDisplay" OnClick="btnAction_Click" />
    </div>
    <asp:GridView ID="gvFolder" runat="server" OnRowDataBound="gvFolder_RowDataBound" OnRowCommand="gvFolder_RowCommand">
        <Columns>
            <asp:BoundField DataField="CategoryID" HeaderText="序号" HeaderStyle-Width="80px" />
            <asp:HyperLinkField HeaderText="名称" DataTextField="CategoryName" DataNavigateUrlFields="CategoryID" DataNavigateUrlFormatString="./List.aspx?id={0}" ItemStyle-CssClass="ItemLeft folder" />

            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150px">
                <ItemTemplate>
                    <asp:LinkButton ID="linkMod" runat="server">修改</asp:LinkButton>
                    <asp:LinkButton ID="linkMov" runat="server">移动</asp:LinkButton>
                    <asp:LinkButton ID="linkDel" runat="server" OnClientClick="return confirm('确认删除当前分类？')">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="txtSeparator" runat="server" Text="<br />"></asp:Label>

    <asp:GridView ID="gvItem" runat="server" OnRowDataBound="gvItem_RowDataBound" OnRowCommand="gvItem_RowCommand">
        <Columns>
            <asp:BoundField DataField="ItemID" HeaderText="序号" HeaderStyle-Width="80px" />
            <asp:HyperLinkField HeaderText="名称" DataTextField="ItemName" DataNavigateUrlFields="ItemID" DataNavigateUrlFormatString="./Price.aspx?id={0}" ItemStyle-CssClass="ItemLeft item" />
            <asp:TemplateField HeaderText="价格类型">
                <ItemTemplate>
                    <%# EnumUtil.GetDescription((model.weichat.TemplateType)Int32.Parse(Eval("TemplateType").ToString())) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="零售产品编号" DataField="RetailItemCode" />
            <asp:BoundField HeaderText="批发产品编号" DataField="WholesaleItemCode" />
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150px">
                <ItemTemplate>
                    <asp:LinkButton ID="linkMod" runat="server">修改</asp:LinkButton>
                    <asp:LinkButton ID="linkMov" runat="server">移动</asp:LinkButton>
                    <asp:LinkButton ID="linkDel" runat="server" OnClientClick="return confirm('确认删除当前条目？')">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:TextBox ID="txtAction" runat="server" CssClass="noneDisplay"></asp:TextBox>
    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;"></iframe>
    </div>

</asp:Content>

