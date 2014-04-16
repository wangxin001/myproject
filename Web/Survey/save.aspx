<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/InfoList.master" AutoEventWireup="true"
	CodeFile="save.aspx.cs" Inherits="Survey_save" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
	<!--左侧标题 begin-->
	<div class="dcw_top">
	</div>
	<!--左侧标题 end-->
	<div class="wei_ri">
		<a href="../index.aspx">首页</a> > <span class="we_te"><a href="survey.aspx">调查问卷</a></span>
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
			<div class="dgza">
				<span class="dc_tc">
					<asp:Label ID="txtMessage" runat="server" Text=""></asp:Label>
				</span>
				<br />
				<div class="clear">
				</div>
			</div>
		</div>
		<!--右侧 end-->
	</div>
</asp:Content>
