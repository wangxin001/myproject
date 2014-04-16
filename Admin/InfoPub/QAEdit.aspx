<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
	CodeFile="QAEdit.aspx.cs" Inherits="InfoPub_QAEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table cellpadding="2" cellspacing="1" class="TableInfo">
		<tr>
			<td class="Width-100">
				标题：
			</td>
			<td>
				<asp:TextBox ID="txtTitile" runat="server" CssClass="TextBorder Width-400"></asp:TextBox>
				<span class="MustInput">*</span>
			</td>
		</tr>
		<tr>
			<td>
				发布者：
			</td>
			<td>
				<asp:TextBox ID="txtPublisher" runat="server" CssClass="TextBorder Width-400"></asp:TextBox>
				<span class="MustInput">*</span>
			</td>
		</tr>
		<tr>
			<td>
				发布时间：
			</td>
			<td>
				<asp:TextBox ID="txtPublisDate" runat="server" CssClass="TextBorder Width-400"></asp:TextBox>
				<span class="MustInput">*</span>
			</td>
		</tr>
		<tr>
			<td>
				状态：
			</td>
			<td>
				<asp:CheckBox ID="chkStatus" runat="server" />发布
			</td>
		</tr>
		<tr>
			<td>
				顺序：
			</td>
			<td>
				<asp:TextBox ID="txtOrder" runat="server" CssClass="TextBorder Width-400">0</asp:TextBox>
				<span class="MustInput">*</span>
			</td>
		</tr>
		<tr>
			<td>
				内容：
			</td>
			<td>
				<asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" CssClass="TextBorder Width-400"></asp:TextBox>
			</td>
		</tr>
	</table>
	<div class="divToolBarCenter">
		<asp:Button ID="btnSubmit" runat="server" Text="发布" OnClick="btnSubmit_Click" />
		<asp:Button ID="btnCancel" runat="server" Text="取消" />
	</div>
</asp:Content>
