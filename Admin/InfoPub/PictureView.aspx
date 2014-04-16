<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PictureView.aspx.cs" Inherits="InfoPub_PictureView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div class="InfoContainer">
		<div class="InfoTitle">
			<%= info.TopicTitle %>
		</div>
		<br />
		<div>
			<div class="InfoPublisher">
				<%=info.Publisher %>
			</div>
			<div class="InfoDate">
				<%=info.PublishDate.ToString("yyyy年MM月dd日") %>
			</div>
		</div>
		<hr style="clear: both; width: 90%; border: red; background: red; height: 5px;" />
		<div class="InfoContent">
			链接地址：<asp:HyperLink ID="HyperLink2" runat="server" Target="_blank"></asp:HyperLink>
		</div>
		<div class="InfoContent">
			<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank">
				<asp:Image ID="Image1" runat="server" />
			</asp:HyperLink>
		</div>
	</div>
	</form>
</body>
</html>
