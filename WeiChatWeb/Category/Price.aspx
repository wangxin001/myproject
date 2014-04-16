<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true" CodeFile="Price.aspx.cs" Inherits="Category_Price" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript">

        $(function () {
            $("#<%=txtPriceDate.ClientID %>").datepicker();
        });

        function CloseFrame() {
            $('#dialog').dialog('close');
        }

        function InputValue() {

            var d = $("#<%=txtPriceDate.ClientID %>").val();

            document.getElementById("dialogContent").src = "./Input.aspx?date=" + d + "&id=<%=id %>";

            $("#dialog").dialog({ title: '录入价格 ' + d, resizable: false, width: 550, height: 290, modal: true });
            $('#dialog').dialog('open');
        }

        function EditPrice(p) {

            var d = $("#<%=txtPriceDate.ClientID %>").val();

            document.getElementById("dialogContent").src = "./Input.aspx?date=" + d + "&id=<%=id %>" + "&p=" + p;

            $("#dialog").dialog({ title: '录入价格 ' + d, resizable: false, width: 550, height: 290, modal: true });
            $('#dialog').dialog('open');
        }

        function ReloadPage() {
            document.getElementById("<%=btnLoad.ClientID %>").click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divCurrentPosition">
        当前位置：录入价格
        <asp:Label ID="txtItemName" runat="server" Text=""></asp:Label>
    </div>
    <div class="divToolBar">
        <asp:TextBox ID="txtPriceDate" runat="server"></asp:TextBox>
        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
        <input id="btnItem" type="button" value="录入价格" onclick="javascript: InputValue();" />
        <asp:Button ID="btnLoad" runat="server" Text="Reload" OnClick="btnLoad_Click" CssClass="noneDisplay" />
    </div>

    <asp:GridView ID="gvTable" runat="server"  OnRowDataBound="gvTable_RowDataBound" OnRowCommand="gvTable_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="日期" DataField="PriceDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText="价格采集点" DataField="SiteName" />
            <asp:BoundField HeaderText="价格类型" DataField="PriceName" />
            <asp:BoundField HeaderText="价格" DataField="PriceValue" />
            <asp:BoundField HeaderText="单位" DataField="UnitName" />
            <asp:BoundField HeaderText="价格说明" DataField="PriceNote" />
            <asp:BoundField HeaderText="附加链接" DataField="URL" />
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150px">
                <ItemTemplate>
                    <asp:LinkButton ID="linkMod" runat="server">修改</asp:LinkButton>
                    <asp:LinkButton ID="linkDel" runat="server" OnClientClick="return confirm('确认删除当前条目？')">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;"></iframe>
    </div>

</asp:Content>

