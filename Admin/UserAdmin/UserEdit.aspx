<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserEdit.aspx.cs" Inherits="UserAdmin_UserEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<table cellpadding="2" cellspacing="1" class="TableInfo">
		<tr>
			<td>
				用户帐号：
			</td>
			<td>
				<asp:TextBox ID="txtUserAccount" runat="server" CssClass="TextBorder Width-150"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				用户密码：
			</td>
			<td>
				<asp:TextBox ID="txtUserPassword" runat="server" CssClass="TextBorder Width-150"
					TextMode="Password"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				实际姓名：
			</td>
			<td>
				<asp:TextBox ID="txtUserName" runat="server" CssClass="TextBorder Width-150"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				用户角色：
			</td>
			<td>
				<asp:DropDownList ID="ddlUserRols" runat="server" CssClass="TextBorder Width-150">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				状态：
			</td>
			<td>
				<asp:CheckBox ID="chkStatus" runat="server" Text="启用" />
			</td>
		</tr>
	</table>
	<div class="divToolBarCenter">
		<asp:Button ID="btnSubmit" runat="server" Text="保存" onclick="btnSubmit_Click" />
		<input type="button" value="取消" />
	</div>
	</form>
</body>
</html>
