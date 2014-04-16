<%@ page language="C#" autoeventwireup="true" inherits="Sync_ItemDetail, PageCode" theme="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="TableInfo" cellpadding="2" cellspacing="1">
            <tr>
                <td class="Width-100 ItemRight">
                    编号：
                </td>
                <td>
                    <asp:Label ID="txtItemCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Width-100 ItemRight">
                    名称：
                </td>
                <td>
                    <asp:Label ID="txtItemName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Width-100 ItemRight">
                    品类：
                </td>
                <td>
                    <asp:Label ID="txtNodeCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Width-100 ItemRight">
                    品种：
                </td>
                <td>
                    <asp:Label ID="txtCatalogCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
               <td class="Width-100 ItemRight">
                    品牌：
                </td>
                <td>
                    <asp:Label ID="txtBrandCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Width-100 ItemRight">
                    规格等级：
                </td>
                <td>
                    <asp:Label ID="txtSpecCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
               <td class="Width-100 ItemRight">
                    产地：
                </td>
                <td>
                    <asp:Label ID="txtAreaCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Width-100 ItemRight">
                    是否有效：
                </td>
                <td>
                    <asp:Label ID="txtActive" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
