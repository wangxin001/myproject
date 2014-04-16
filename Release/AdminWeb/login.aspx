<%@ page language="C#" autoeventwireup="true" inherits="_Login1, PageCode" theme="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table>
			<tr>
				<td>
					用户名：
				</td>
				<td>
					<asp:TextBox ID="txtUserAccount" runat="server" CssClass="TextBorder Width-150"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					密码：
				</td>
				<td>
					<asp:TextBox ID="txtUserPwd" runat="server" TextMode="Password" CssClass="TextBorder Width-150"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<asp:Button ID="btnLogin" runat="server" Text="登陆" OnClick="btnLogin_Click" />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
