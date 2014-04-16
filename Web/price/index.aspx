<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/InfoList.master" AutoEventWireup="true"
	CodeFile="index.aspx.cs" Inherits="price_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
	<!--左侧标题 begin-->
	<div class="syjg_top">
	</div>
	<!--左侧标题 end-->
	<div class="wei_ri">
		<a href="../index.aspx">首页</a> > 
        <a href="./index_<%=board.BoardID.ToString() %>.aspx">实用价格查询</a> > 
        <span class="we_te"><a href="./index_<%=board.BoardID.ToString() %>.aspx"><%=board.DisplayName %></a></span>
	</div>
	<div class="clear">
	</div>
	<div class="zbody_b">
		<!--左侧 begin-->
		<div class="zb_le">
			<div class="jia_ce">
				
                <!--当前栏目选中状态下的样式名为：class=“on”-->
                 <ul class="left_menu">
                     <li><a href="./index_33.aspx" title="医疗" class='<%= board.BoardID==33?"on":"" %>' >医疗</a></li>
                     <li><a href="./index_34.aspx" title="食品" class='<%= board.BoardID==34?"on":"" %>' >食品</a></li>
                     <li><a href="./index_35.aspx" title="居住" class='<%= board.BoardID==35?"on":"" %>' >居住</a></li>
                     <li><a href="./index_36.aspx" title="出行" class='<%= board.BoardID==36?"on":"" %>' >出行</a></li>
                     <li><a href="./index_37.aspx" title="教育" class='<%= board.BoardID==37?"on":"" %>' >教育</a></li>
                     <li><a href="./index_38.aspx" title="其他" class='<%= board.BoardID==38?"on":"" %>' >其他</a></li>
                 </ul>
                
			</div>
		</div>
		<!--左侧 end-->
		<!--右侧 begin-->
		<div class="zb_ri">
			<div class="gza">
				<ul class="list">
					<asp:Repeater ID="rpInfo" runat="server">
						<ItemTemplate>
                          <li><a href="<%# Eval("TopicType").ToString()=="1" ? ("./price_"+Eval("TopicID").ToString()+".aspx") : Eval("TopicLink").ToString() %>" title="<%# Eval("TopicTitle").ToString()%>" target="_blank"><%# Eval("TopicTitle").ToString() %></a></li>
						</ItemTemplate>
					</asp:Repeater>
				</ul>
			</div>
			
		</div>
		<!--右侧 end-->
	</div>
</asp:Content>
