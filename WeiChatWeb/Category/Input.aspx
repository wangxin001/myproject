<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="Category_Input" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="TableInfo" cellpadding="2" cellspacing="1">
                <tr>
                    <td>上报采集点：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpSite" runat="server" DataValueField="SiteID" DataTextField="SiteName"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>价格内容：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpPriceCode" runat="server" DataValueField="PriceID" DataTextField="PriceName"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="trPrice" runat="server">
                    <td>价格：
                    </td>
                    <td>
                        <asp:TextBox ID="txtPriceValue" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trUnit" runat="server">
                    <td>价格单位：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpUnit" runat="server" DataValueField="UnitID" DataTextField="UnitName"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="trNote" runat="server">
                    <td>价格说明：
                    </td>
                    <td>
                        <asp:TextBox ID="txtPriceNote" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trURL" runat="server">
                    <td>附加链接：
                    </td>
                    <td>
                        <asp:TextBox ID="txtURL" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="divToolBar">
                <asp:Button ID="btnSubmit" runat="server" Text="确定" OnClick="btnSubmit_Click" OnClientClick="return CheckInput();" />
                <input id="btnCancel" type="button" value="取消" onclick="return btnCancel_onclick()" />
            </div>
        </div>
    </form>
</body>
</html>
