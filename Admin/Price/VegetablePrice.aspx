<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
    CodeFile="VegetablePrice.aspx.cs" Inherits="Price_VegetablePrice" %>

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
        当前位置：
        <asp:Label runat="server" ID="txtTitle" Text=""></asp:Label>
    </div>
    <div class="divToolBar">
        <asp:DropDownList ID="ddlItemType" runat="server" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged"
            AutoPostBack="true">
        </asp:DropDownList>
        选择日期：<asp:TextBox ID="txtPriceDate" runat="server" CssClass="TextBorder Width-90"></asp:TextBox>
        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
        <asp:Button ID="btnInput" runat="server" Text="录入" OnClick="btnInput_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="名称" DataField="ItemName" />
            <asp:BoundField HeaderText="规格等级" DataField="ItemLevel" />
            <asp:BoundField HeaderText="价格单位" DataField="ItemUnit" />
            <asp:BoundField HeaderText="报价日期" DataField="PriceDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText="成交量" DataField="Price01" HtmlEncode="false" />
            <asp:BoundField HeaderText="批发价" DataField="Price02" HtmlEncode="false" />
            <asp:BoundField HeaderText="农贸零售价" DataField="Price03" HtmlEncode="false" />
            <asp:BoundField HeaderText="超市零售价" DataField="Price04" HtmlEncode="false" />
        </Columns>
    </asp:GridView>
</asp:Content>
