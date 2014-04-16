<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
    CodeFile="PriceEdit.aspx.cs" Inherits="Report_PriceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript">

        $(function () {
            $("#<%=txtDate.ClientID %>").datepicker();
        });

        function CloseFrame() {
            $('#dialog').dialog('close');
        }

        function InputPrice() {

            var v = document.getElementById("<%=txtDate.ClientID %>").value;

            document.getElementById("dialogContent").src = "./PriceInput.aspx?PriceDate=" + v + "&UserGUID=<%=strUserGUID %>";

            $("#dialog").dialog({ title: '录入价格 ' + v, resizable: false, width: 500, height: 400, modal: true });
            $('#dialog').dialog('open');
        }

        function ReloadPage() {
            document.getElementById("<%=btnQuery.ClientID %>").click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：菜价管理 -
        <asp:Label ID="txtStoreName" runat="server" Text=""></asp:Label>
    </div>
    <div class="divToolBar">
        <asp:TextBox ID="txtDate" runat="server" CssClass="TextBorder Width-100" />
        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
        <input type=button value="录入价格" onclick="javascript:InputPrice();" />
    </div>
    <div style="clear: both">
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="品类" DataField="ItemName" />
            <asp:BoundField HeaderText="规格等级" DataField="ItemLevel" />
            <asp:BoundField HeaderText="价格单位" DataField="ItemUnit" />
            <asp:TemplateField HeaderText="价格">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="TextBorder Width-50"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="noneDisplay" HeaderStyle-CssClass="noneDisplay">
                <ItemTemplate>
                    <asp:TextBox ID="txtPriceID" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="divToolBar">
        <asp:DropDownList ID="ddpBatch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddpBatch_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="btnDelete" runat="server" Text="删除" onclick="btnDelete_Click" OnClientClick="return confirm('确认删除所选批次的数据？\r\n\r\n数据删除后将无法恢复，请谨慎操作！');" />
        &nbsp;&nbsp;
        <asp:Button ID="txtSave" runat="server" Text="保存价格" OnClick="txtSave_Click" />
    </div>

    <div id="dialog" style="display: none;">
        
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;">
        </iframe>

    </div>

</asp:Content>
