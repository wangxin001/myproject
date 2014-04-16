<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
    CodeFile="TopicEdit.aspx.cs" Inherits="InfoPub_TopicEdit" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="../ckfinder/ckfinder.js"></script>
    <script type="text/javascript">

        window.onload = function () {
            $("#<%=txtPublisDate.ClientID %>").datepicker();

            if (document.getElementById('<%=txtContent.ClientID %>') != undefined) {
                var editor = CKEDITOR.replace('<%=txtContent.ClientID %>');
                editor.config.toolbar = 'Topic';
                CKFinder.setupCKEditor(editor, '../ckfinder/');
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="2" cellspacing="1" class="TableInfo">
        <tr>
            <td class="Width-100">标题：
            </td>
            <td>
                <asp:TextBox ID="txtTitile" runat="server" CssClass="TextBorder Width-250"></asp:TextBox>
                <span class="MustInput">*</span>
            </td>
        </tr>
        <tr>
            <td>发布者：
            </td>
            <td>
                <asp:TextBox ID="txtPublisher" runat="server" CssClass="TextBorder Width-250"></asp:TextBox>
                <span class="MustInput">*</span>
            </td>
        </tr>
        <tr>
            <td>发布时间：
            </td>
            <td>
                <asp:TextBox ID="txtPublisDate" runat="server" CssClass="TextBorder Width-250"></asp:TextBox>
                <span class="MustInput">*</span>
                <asp:CheckBox ID="chkShowDate" runat="server" />显示发布时间
            </td>
        </tr>
        <tr>
            <td>顺序：
            </td>
            <td>
                <asp:TextBox ID="txtOrder" runat="server" CssClass="TextBorder Width-250">0</asp:TextBox>
                <span class="MustInput">*</span>
            </td>
        </tr>
        <tr>
            <td>状态：
            </td>
            <td>
                <asp:CheckBox ID="chkStatus" runat="server" />发布
            </td>
        </tr>
        <tr>
            <td>新闻类型：
            </td>
            <td>
                <asp:DropDownList ID="ddpContentType" runat="server" OnSelectedIndexChanged="ddpContentType_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="发布内容" Value="1" Selected="True" />
                    <asp:ListItem Text="外部链接" Value="0" />
                </asp:DropDownList>
            </td>
        </tr>
        <asp:Panel ID="Panel1" runat="server">
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
            <tr>
                <td>链接地址：
                </td>
                <td>
                    <asp:TextBox ID="txtTopicLink" runat="server" CssClass="TextBorder Width-250"></asp:TextBox>
                </td>
            </tr>
        </asp:Panel>
    </table>
    <div class="divToolBarCenter">
        <asp:Button ID="btnSubmit" runat="server" Text="发布" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="取消" />
    </div>
</asp:Content>
