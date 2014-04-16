<%@ Page Language="C#" AutoEventWireup="true" CodeFile="photo.aspx.cs" Inherits="about_photo" %>

<%@ OutputCache CacheProfile="Cache1month" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>北京市价格监测中心</title>
	<meta name="keywords" content="京价网、价格、价格监测、北京市价格监测中心、北京价格监测、北京价格">
	<link href="../css/style.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" href="../css/stylesheet.css" type="text/css">
</head>
<body>
	<form id="form1" runat="server">
	<span class="gz_title">
		<%=strTitle%></span> <span class="sgz_tjg">
			<ul id="slideshow">
				<asp:Repeater ID="rpPhoto" runat="server">
					<ItemTemplate>
						<li>
							<h3>
							</h3>
							<span>../upload/<%# Eval("TopicContent").ToString() %></span>
							<p>
							</p>
							<a href="#">
								<img src="../upload/<%# Eval("TopicContent").ToString() %>" alt="" /></a>
						</li>
					</ItemTemplate>
				</asp:Repeater>
			</ul>
			<div id="wrapper">
				<div id="fullsize">
					<div id="imgprev" class="imgnav" title="上一张">
					</div>
					<div id="imglink">
					</div>
					<div id="imgnext" class="imgnav" title="下一张">
					</div>
					<div id="image">
					</div>
					<div id="information">
						<h3>
						</h3>
						<p>
						</p>
					</div>
				</div>
				<div id="thumbnails">
					<div id="slideleft" title="向右移">
					</div>
					<div id="slidearea">
						<div id="slider">
						</div>
					</div>
					<div id="slideright" title="向左移">
					</div>
				</div>
			</div>
			<script type="text/javascript" src="../javascript/compressed.js"></script>
			<script type="text/javascript">
				$('slideshow').style.display = 'none';
				$('wrapper').style.display = 'block';
				var slideshow = new TINY.slideshow("slideshow");
				window.onload = function ()
				{
					slideshow.auto = true;
					slideshow.speed = 5;
					slideshow.link = "linkhover";
					slideshow.thumbs = "slider";
					slideshow.left = "slideleft";
					slideshow.right = "slideright";
					slideshow.scrollSpeed = 4;
					slideshow.spacing = 2;
					slideshow.active = "#000";
					slideshow.init("slideshow", "image", "imgprev", "imgnext", "imglink");
				}
			</script>
		</span>
	</form>
</body>
</html>
