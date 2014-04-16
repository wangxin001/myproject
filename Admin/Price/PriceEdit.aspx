<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PriceEdit.aspx.cs" Inherits="Price_PriceEdit" %>

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
                <td>
                    价格条目名称：
                </td>
                <td>
                    <asp:TextBox ID="txtVegetableName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    规格等级：
                </td>
                <td>
                    <asp:TextBox ID="txtItemLevel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    价格单位：
                </td>
                <td>
                    <asp:TextBox ID="txtVegetableUnit" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    顺序：
                </td>
                <td>
                    <asp:TextBox ID="txtVegetableOrder" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    是否上报价格：
                </td>
                <td>
                    <asp:CheckBox ID="chkReport" runat="server" /> 需上报
                </td>
            </tr>
            <tr>
                <td>
                    状态：
                </td>
                <td>
                    <asp:CheckBox ID="chkStatus" runat="server" /> 正常
                </td>
            </tr>
        </table>
        <div class="divToolBarCenter">
            <asp:Button ID="btnAdd" runat="server" Text="确定" OnClick="btnAdd_Click" OnClientClick="return CheckInput();" />
            <input id="btnCancel" type="button" value="取消" onclick="return btnCancel_onclick()" />
        </div>
    </div>
    </form>
</body>
</html>
