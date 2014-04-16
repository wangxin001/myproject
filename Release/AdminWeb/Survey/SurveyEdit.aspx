<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="Survey_SurveyEdit, PageCode" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：投票调查信息管理
    </div>
    <table cellpadding="2" cellspacing="1" class="TableInfo">
        <tr>
            <td>
                投票调查名称：
            </td>
            <td>
                <asp:TextBox ID="txtVoteName" runat="server" CssClass="TextBorder Width-400"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                投票调查说明：
            </td>
            <td>
                <asp:TextBox ID="txtVoteDescription" runat="server" TextMode="MultiLine" CssClass="TextBorder Width-400"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                拟开始时间：
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="TextBorder Width-400"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                拟结束时间：
            </td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="TextBorder Width-400"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                调查结果是否公开：
            </td>
            <td>
                <asp:CheckBox ID="chkResultPublic" runat="server" Checked="true" />
                公开
            </td>
        </tr>
    </table>
    <div class="divToolBarCenter">
        <asp:Button ID="btnSaveInfo" runat="server" Text="保存" OnClick="btnSaveInfo_Click" />
        <asp:Button ID="btnQuestion" runat="server" Text="调查问卷管理" OnClick="btnQuestion_Click" />
    </div>
</asp:Content>
