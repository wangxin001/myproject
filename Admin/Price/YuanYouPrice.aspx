<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true" CodeFile="YuanYouPrice.aspx.cs" Inherits="Price_YuanYouPrice" %>

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
            InputPriceIndex(curDate);
        }

        function InputPriceIndex(v) {
            document.getElementById("dialogContent").src = "./YuanYouInput.aspx?PriceDate=" + v;

            $("#dialog").dialog({ title: '录入原油价格指数 ' + v, resizable: false, width: 350, height: 190, modal: true });
            $('#dialog').dialog('open');
        }

        function ReloadPage() {
            document.getElementById("<%=btnLoad.ClientID %>").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：最近100天原油价格指数
    </div>
    <div class="divToolBar">
        选择日期：<asp:TextBox ID="txtPriceDate" runat="server" CssClass="TextBorder Width-90"></asp:TextBox>
        <input id="btnInput" type="button" value="录入" onclick="return btnInput_onclick()" />
        <asp:Button ID="btnLoad" runat="server" Text="Reload" OnClick="btnLoad_Click" CssClass="ItemHidden" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"
        OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 + (this.AspNetPager.CurrentPageIndex-1)*this.AspNetPager.PageSize %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="日期" DataField="PriceDate" DataFormatString="{0:yyyy年MM月dd日}" />
            <asp:BoundField HeaderText="WTL原油价格指数" DataField="Price02" HeaderStyle-CssClass="Width-150" />
            <asp:BoundField HeaderText="布伦特原油价格指数" DataField="Price03" HeaderStyle-CssClass="Width-150" />
            <asp:TemplateField HeaderText="状态" HeaderStyle-CssClass=" Width-75">
                <ItemTemplate>
                    <%# (Convert.ToDecimal(Eval("Price04"))==1.0M)?"隐藏":"显示"  %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" HeaderStyle-CssClass=" Width-75">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server">删除</asp:LinkButton>
                    <asp:HyperLink ID="lnkInput" runat="server">录入</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
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

