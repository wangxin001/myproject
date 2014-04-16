<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true" CodeFile="Unit.aspx.cs" Inherits="SysAdmin_Unit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript">

        function CloseFrame() {
            $('#dialog').dialog('close');
        }

        function CreateUnit() {
            document.getElementById("dialogContent").src = "./UnitEdit.aspx";

            $("#dialog").dialog({ title: '添加价格单位', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function EditUnit(id) {
            document.getElementById("dialogContent").src = "./UnitEdit.aspx?id=" + pid;

            $("#dialog").dialog({ title: '修改价格单位', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function ReloadPage() {
            document.location.href = document.location.href;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：价格单位管理
    </div>
    <div class="divToolBar">
        <asp:Button ID="btnAdd" runat="server" Text="添加采集点" OnClientClick="javascript:CreateUnit();return false;" />
    </div>
    <asp:GridView ID="gvUnit" runat="server" OnRowDataBound="gvUnit_RowDataBound" OnRowCommand="gvUnit_RowCommand">
        <Columns>
            <asp:BoundField DataField="UnitID" HeaderText="编号" HeaderStyle-Width="80px" />
            <asp:BoundField DataField="UnitName" HeaderText="名称" ItemStyle-CssClass="ItemLeft" />
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150px">
                <ItemTemplate>
                    <asp:LinkButton ID="linkMod" runat="server">修改</asp:LinkButton>
                    <asp:LinkButton ID="linkDel" runat="server" OnClientClick="return confirm('确认删除当前价格单位？')">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;"></iframe>
    </div>
</asp:Content>

