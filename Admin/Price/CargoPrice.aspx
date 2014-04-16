<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
    CodeFile="CargoPrice.aspx.cs" Inherits="Price_CargoPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript">

        $(function () {
            $("#<%=txtPriceDate.ClientID %>").datepicker();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：今日大宗商品价格
    </div>
    <div class="divToolBar">
        选择日期：<asp:TextBox ID="txtPriceDate" runat="server" CssClass="TextBorder Width-90"></asp:TextBox>
        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
        <asp:Button ID="btnInput" runat="server" Text="录入" OnClick="btnInput_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="大宗商品名称" DataField="ItemName" />
            <asp:BoundField HeaderText="规格等级" DataField="ItemUnit" />
            <asp:BoundField HeaderText="报价日期" DataField="PriceDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText="价格" DataField="Price02" HtmlEncode="false" HeaderStyle-CssClass="Width-80" />
        </Columns>
    </asp:GridView>
</asp:Content>
