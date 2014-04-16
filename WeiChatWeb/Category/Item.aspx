<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Item.aspx.cs" Inherits="Category_Item" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script language="javascript" type="text/javascript">
<!--

    function CheckInput() {
        return true;
    }

    function btnCancel_onclick() {
        parent.CloseFrame();
        return false;
    }

    // -->
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="TableInfo" cellpadding="2" cellspacing="1">
                <tr>
                    <td>条目名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtItemName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>零售价格编号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtRetailItemCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>批发价格编号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtWholesaleItemCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>价格展示模版：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpPriceType" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="divToolBarCenter">
                <asp:Button ID="btnSubmit" runat="server" Text="确定" OnClick="btnSubmit_Click" OnClientClick="return CheckInput();" />
                <input id="btnCancel" type="button" value="取消" onclick="return btnCancel_onclick()" />
            </div>
        </div>
    </form>
</body>
</html>