<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YuanYouInput.aspx.cs" Inherits="Price_YuanYouInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                <td>
                    WTI原油价格指数：
                </td>
                <td>
                    <asp:TextBox ID="txtPriceIndex1" runat="server" CssClass="TextBorder"></asp:TextBox>
                </td>
            </tr>
			<tr>
                <td>
                    布伦特原油价格指数：
                </td>
                <td>
                    <asp:TextBox ID="txtPriceIndex2" runat="server" CssClass="TextBorder"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    是否隐藏：
                </td>
                <td>
                    <asp:CheckBox ID="chkHidden" runat="server" />隐藏
                </td>
            </tr>
        </table>
        <div class="divToolBarCenter">
            <asp:Button ID="btnAdd" runat="server" Text="确定" OnClick="btnAdd_Click" OnClientClick="return CheckInput();" />
            <input id="btnCancel" type="button" value="取消" onclick="return btnCancel_onclick()" />
        </div>
    </div>
    <asp:TextBox ID="txtPriceIndexID" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>