<%@ page language="C#" autoeventwireup="true" inherits="Report_UserEdit, PageCode" theme="_Default" %>

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
				登陆帐号：
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
				<asp:TextBox ID="txtUserPassword" runat="server" CssClass="TextBorder Width-150"
					TextMode="Password"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				平价菜店名称：
			</td>
			<td>
				<asp:TextBox ID="txtUserName" runat="server" CssClass="TextBorder Width-150"></asp:TextBox>
			</td>
		</tr>


        <tr>
			<td>
				联系人姓名：
			</td>
			<td>
				<asp:TextBox ID="txtContactName" runat="server" CssClass="TextBorder Width-150"></asp:TextBox>
			</td>
		</tr>

        <tr>
			<td>
				联系人职位：
			</td>
			<td>
				<asp:TextBox ID="txtContactTitle" runat="server" CssClass="TextBorder Width-150"></asp:TextBox>
			</td>
		</tr>

        <tr>
			<td>
				联系电话：
			</td>
			<td>
				<asp:TextBox ID="txtContactPhone" runat="server" CssClass="TextBorder Width-150"></asp:TextBox>
			</td>
		</tr>

        <tr>
			<td>
				手机：
			</td>
			<td>
				<asp:TextBox ID="txtContactMobile" runat="server" CssClass="TextBorder Width-150"></asp:TextBox>
			</td>
		</tr>

         <tr>
			<td>
				邮箱：
			</td>
			<td>
				<asp:TextBox ID="txtContactEmail" runat="server" CssClass="TextBorder Width-150"></asp:TextBox>
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
