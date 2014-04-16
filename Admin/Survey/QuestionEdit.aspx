<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionEdit.aspx.cs" Inherits="Survey_QuestionEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .OptionList
        {
            line-height: 32px;
            padding: 1px;
            visibility: hidden;
            visibility: visible;
        }
    </style>
    <script type="text/javascript">

        function ChangeOptionsCount(obj)
        {
            var selectCount = parseInt(obj.options[obj.selectedIndex].value);

            for (var i = 0; i < obj.length; i++)
            {
                if (i < selectCount)
                {
                    document.getElementById("divOption" + i).style.visibility = "visible";
                }
                else
                {
                    document.getElementById("divOption" + i).style.visibility = "hidden";
                }
            }
        }

        function btnCancel_onclick()
        {
            parent.CloseFrame();
            return false;
        }

        window.onload = function ()
        {
            ChangeOptionsCount(document.getElementById("ddlOptionCount"));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="TableInfo" cellpadding="2" cellspacing="1">
            <tr>
                <td>
                    问题内容：
                </td>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" CssClass="TextBorder Width-180"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    问题类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlQuestionType" runat="server" CssClass="Width-180">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    选项个数：
                </td>
                <td>
                    <asp:DropDownList ID="ddlOptionCount" runat="server" CssClass="Width-180" AutoPostBack="false"
                        onclick="javascript:ChangeOptionsCount(this);">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    选项内容：
                </td>
                <td>
                    <asp:Panel ID="Panel1" runat="server">
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    问题顺序：
                </td>
                <td>
                    <asp:TextBox ID="txtOrder" runat="server" CssClass="TextBorder Width-180" Text="0"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divToolBarCenter">
            <asp:Button ID="btnAdd" runat="server" Text="确定" OnClick="btnAdd_Click" />
            <input id="btnCancel" type="button" value="取消" onclick="return btnCancel_onclick()" />
        </div>
    </div>
    </form>
</body>
</html>
