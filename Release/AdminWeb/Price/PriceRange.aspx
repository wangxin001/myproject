<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="Price_PriceRange, PageCode" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：价格范围管理
    </div>
    <div class="divToolBar">
        <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" />
    </div>
    <table cellpadding="2" cellspacing="1" style="width: 100%">
        <thead>
            <th>
                项目
            </th>
            <th align="left">
                下限
            </th>
            <th align="left">
                上限
            </th>
        </thead>
        <tr>
            <td>
                菜价走势：
            </td>
            <td>
                <asp:TextBox ID="txtMinPriceCaiJia" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtMaxPriceCaiJia" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                富强粉走势：
            </td>
            <td>
                <asp:TextBox ID="txtMinFuQiangFen" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtMaxFuQiangFen" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                粳米走势：
            </td>
            <td>
                <asp:TextBox ID="txtMinJingMi" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtMaxJingMi" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                调和油走势：
            </td>
            <td>
                <asp:TextBox ID="txtMinTiaoHeYou" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtMaxTiaoHeYou" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                猪肉走势：
            </td>
            <td>
                <asp:TextBox ID="txtMinZhuRou" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtMaxZhuRou" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                鸡蛋走势：
            </td>
            <td>
                <asp:TextBox ID="txtMinJiDan" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtMaxJiDan" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
