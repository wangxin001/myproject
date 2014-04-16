<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BoardEdit.aspx.cs" Inherits="InfoPub_BoardEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
<!--

        function CheckInput()
        {
            return true;
        }

        function btnCancel_onclick()
        {
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
                    栏目名称：
                </td>
                <td>
                    <asp:TextBox ID="txtBoardName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    显示名称：
                </td>
                <td>
                    <asp:TextBox ID="txtDisplayName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    栏目类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlBoardType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    栏目状态：
                </td>
                <td>
                    <asp:CheckBox ID="chkStatus" runat="server" Checked="true" />启用
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
