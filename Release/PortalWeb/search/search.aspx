<%@ page title="" language="C#" masterpagefile="~/MasterPage/InfoList.master" autoeventwireup="true" inherits="search_search, PageCode" enableeventvalidation="false" viewstateencryptionmode="Never" enableviewstate="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
	<!--左侧标题 begin-->
	<div class="zns_top">
	</div>
	<!--左侧标题 end-->
	<div class="wei_ri">
		<a href="../index.aspx">首页</a> > <span class="we_te"><a href="search.aspx">站内搜索</a></span>
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
				<ul>
					<asp:Repeater ID="rpInfo" runat="server">
						<ItemTemplate>
							<li><span class="ne_le"><a href="../<%# GetFolder(Eval("BoardID").ToString()) %>/detail_<%# Eval("TopicID").ToString()%>.aspx"
								target="_blank">
								<div class="CuteTitle">
									<%# Eval("TopicTitle").ToString()%></div>
							</a></span><span class="ne_ld">[<%# Convert.ToDateTime(Eval("PublishDate").ToString()).ToString("yyyy-MM-dd")%>]
							</span></li>
							<%# (Container.ItemIndex>0 && (Container.ItemIndex+1) %5==0  )? "</ul><ul>":string.Empty  %>
						</ItemTemplate>
					</asp:Repeater>
				</ul>
			</div>
			<div class="clear">
			</div>
			<div class="page">
			</div>
		</div>
		<!--右侧 end-->
	</div>
</asp:Content>
