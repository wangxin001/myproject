<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true" CodeFile="Price.aspx.cs" Inherits="SysAdmin_Price" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript">

        function CloseFrame() {
            $('#dialog').dialog('close');
        }

        function CreatePrice() {
            document.getElementById("dialogContent").src = "./PriceEdit.aspx";

            $("#dialog").dialog({ title: '新建价格类型', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function EditPrice(pid) {
            document.getElementById("dialogContent").src = "./PriceEdit.aspx?id=" + pid;

            $("#dialog").dialog({ title: '修改价格类型', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function ReloadPage() {
            document.location.href = document.location.href;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divCurrentPosition">
        当前位置：分类管理
    </div>
    <div class="divToolBar">
        <asp:Button ID="btnAdd" runat="server" Text="添加分类" OnClientClick="javascript:CreatePrice();return false;" />
    </div>
    <asp:GridView ID="gvPrice" runat="server" OnRowDataBound="gvPrice_RowDataBound" OnRowCommand="gvPrice_RowCommand">
        <Columns>
            <asp:BoundField DataField="PriceID" HeaderText="编号" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="PriceName" HeaderText="名称" ItemStyle-CssClass="ItemLeft" />
            <asp:BoundField DataField="RemotePriceCode" HeaderText="价格中心编号" />
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150px">
                <ItemTemplate>
                    <asp:LinkButton ID="linkMod" runat="server">修改</asp:LinkButton>
                    <asp:LinkButton ID="linkDel" runat="server" OnClientClick="return confirm('确认删除当前价格类型？')">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;"></iframe>
    </div>
</asp:Content>

