<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/InfoList.master" AutoEventWireup="true"
	CodeFile="detail.aspx.cs" Inherits="policy_detail" %>

<%@ OutputCache CacheProfile="Cache1hourByTopicID" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
	<!--左侧标题 begin-->
	<div class="jgzc_top">
	</div>
	<!--左侧标题 end-->
	<div class="wei_ri">
		<a href="../index.aspx">首页</a> > <span class="we_te"><a href="index.aspx">价格政策</a></span>
	</div>
	<div class="clear">
	</div>
	<div class="zbody_b">
		<!--左侧 begin-->
		<div class="zb_le">
		</div>
		<!--左侧 end-->
		<!--右侧 begin-->
		<div class="zb_ri">
			<div class="gza">
				<span class="gz_title"><strong>
					<%=info.TopicTitle %></strong> </span><span class="gz_td">来源：<%=info.Publisher%>&nbsp;&nbsp;&nbsp;&nbsp;<%= info.ShowDate? "发布时间："+info.PublishDate.ToString("yyyy-MM-dd"):"" %>
					</span><span class="gz_tc">
						<%=info.TopicContent %></span>
			</div>
		</div>
		<!--右侧 end-->
	</div>
</asp:Content>
