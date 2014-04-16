<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
    CodeFile="PriceIndex.aspx.cs" Inherits="Price_PriceIndex" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
        function btnInput_onclick() {
            var curDate = document.getElementById("<%=txtPriceDate.ClientID %>").value;

            document.getElementById("dialogContent").src = "./PriceInput.aspx?PriceDate=" + curDate + "&PriceItem=<%=strPriceType %>"

            $("#dialog").dialog({ title: '录入价格指数 ' + curDate, resizable: false, width: 400, height: 200, modal: true });
            $('#dialog').dialog('open');
        }

        function ReloadPage() {
            document.getElementById("<%=btnLoad.ClientID %>").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：最近100天<asp:Label ID="labelTitle" runat="server" Text=""></asp:Label>
    </div>
    <div class="divToolBar">
        选择日期：<asp:TextBox ID="txtPriceDate" runat="server" CssClass="TextBorder Width-90"></asp:TextBox>
        <input id="btnInput" type="button" value="录入" onclick="return btnInput_onclick()" />
        <asp:Button ID="btnLoad" runat="server" Text="Reload" OnClick="btnLoad_Click" CssClass="ItemHidden" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 + (this.AspNetPager.CurrentPageIndex-1)*this.AspNetPager.PageSize %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="日期" DataField="PriceDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText="批发价<br />(元/公斤)" DataField="Price02" HtmlEncode="false"
                HeaderStyle-CssClass="Width-150" />
            <asp:BoundField HeaderText="零售农贸价<br />(元/公斤)" DataField="Price03" HtmlEncode="false"
                HeaderStyle-CssClass="Width-150" />
            <asp:BoundField HeaderText="零售超市价<br />(元/公斤)" DataField="Price04" HtmlEncode="false"
                HeaderStyle-CssClass="Width-150" />
        </Columns>
    </asp:GridView>
    <div class="divToolBar">
        <webdiyer:AspNetPager ID="AspNetPager" runat="server" OnPageChanged="AspNetPager_PageChanged">
        </webdiyer:AspNetPager>
    </div>
    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;">
        </iframe>
    </div>
</asp:Content>
