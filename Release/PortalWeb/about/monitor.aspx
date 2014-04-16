<%@ page title="" language="C#" masterpagefile="~/MasterPage/InfoList.master" autoeventwireup="true" inherits="about_monitor, PageCode" enableviewstate="false" %>

<%@ OutputCache CacheProfile="Cache1hour" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
	<!--左侧标题 begin-->
	<div class="jgj_top">
	</div>
	<!--左侧标题 end-->
	<div class="wei_ri">
		<a href="../index.aspx">首页</a> > <a href="index.aspx">机构介绍</a> > <span class="we_te">
			<a href="monitor.aspx">春夏秋冬话监测</a></span>
	</div>
	<div class="clear">
	</div>
	<div class="zbody_b">
		<!--左侧 begin-->
		<div class="zb_le">
			<span class="menu">
				<ul>
					<asp:Repeater ID="rpMenu" runat="server">
						<ItemTemplate>
							<li class="me_off"><a href="detail_<%# Eval("TopicID").ToString() %>.aspx">
								<%# Eval("TopicTitle").ToString() %></a> </li>
						</ItemTemplate>
					</asp:Repeater>
					<li class="me_off"><a href="district.aspx">区县部门介绍</a> </li>
					<li class="me_on"><a href="monitor.aspx">春夏秋冬话监测</a> </li>
				</ul>
			</span>
		</div>
		<!--左侧 end-->
		<!--右侧 begin-->
		<div class="zb_ri">
			<div class="sgza">
				<iframe src="photo.aspx" allowtransparency="true" width="640" height="600" marginwidth="0"
					marginheight="0" hspace="0" vspace="0" frameborder="0" scrolling="no"></iframe>
			</div>
		</div>
		<!--右侧 end-->
	</div>
</asp:Content>
