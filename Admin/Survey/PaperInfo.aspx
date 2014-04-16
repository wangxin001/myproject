<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
    CodeFile="PaperInfo.aspx.cs" Inherits="Survey_PaperInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery.ui.datepicker-zh-CN.js"></script>
    <script language="javascript" type="text/javascript">
<!--

        function btnAdd_onclick()
        {
            document.getElementById("dialogContent").src = "./QuestionEdit.aspx?VoteID=<%=voteID %>";

            $("#dialog").dialog({ title: '问题内容管理 ', resizable: false, width: 500, height: 450, modal: true });
            $('#dialog').dialog('open');
        }

        function CloseFrame()
        {
            $('#dialog').dialog('close');
        }

// -->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：投票调查-问卷内容管理
    </div>
    <div class="divToolBar">
        <input id="btnAdd" type="button" value="添加问题" onclick="javascript:btnAdd_onclick()" />
    </div>
    <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table class="TableInfo" cellpadding="2" cellspacing="1">
                <tr>
                    <th class="ItemCenter Width-50">
                        序号
                    </th>
                    <th>
                        问题内容
                    </th>
                    <th class="ItemCenter Width-60">
                        题型
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="ItemCenter Width-50">
                    <input name="chkQurstionItem" value="<%# Eval("QuestionID").ToString() %>" type="checkbox" />
                </td>
                <td>
                    <%# Container.ItemIndex+1 %>.
                    <%# Eval("QuestionContent").ToString()%>
                </td>
                <td class="ItemCenter Width-60">
                    <%# GetQuestionType( Convert.ToInt32( Eval("QuestionType").ToString()))%>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
                    <%# GetQuestionOption(Convert.ToInt32( Eval("QuestionID").ToString()),
                                                (model.QuestionType)Convert.ToInt32(Eval("QuestionType").ToString()))%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div class="divToolBarCenter">
        <asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_Click" />
    </div>
    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;">
        </iframe>
    </div>
</asp:Content>
