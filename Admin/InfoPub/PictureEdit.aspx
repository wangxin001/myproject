<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
	CodeFile="PictureEdit.aspx.cs" Inherits="InfoPub_PictureEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table cellpadding="2" cellspacing="1" class="TableInfo">
		<tr>
			<td>
				图片标题：
			</td>
			<td>
				<asp:TextBox ID="txtTitle" runat="server" CssClass="TextBorder Width-400"></asp:TextBox>
				<span class="MustInput">*</span>
			</td>
		</tr>
		<tr>
			<td>
				链接地址：
			</td>
			<td>
				<asp:TextBox ID="txtLink" runat="server" CssClass="TextBorder Width-400"></asp:TextBox>
				<span class="MustInput">*</span>
			</td>
		</tr>
		<tr>
			<td>
				发布者：
			</td>
			<td>
				<asp:TextBox ID="txtPublisher" runat="server" CssClass="TextBorder Width-400"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				选择图片：
			</td>
			<td>
				<asp:FileUpload ID="FileUpload1" runat="server" CssClass="TextBorder Width-400" />
				<span class="MustInput">*</span>
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
				状态：
			</td>
			<td>
				<asp:CheckBox ID="chkStatus" runat="server" />发布
			</td>
		</tr>
	</table>
	<div class="divToolBarCenter">
		<asp:Button ID="btnSave" runat="server" Text="提交" OnClick="btnSubmit_Click" />
		<input type="button" value="取消" />
	</div>
	<div class=" divToolBarLeft">
		<asp:Label ID="txtMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
	</div>
</asp:Content>
