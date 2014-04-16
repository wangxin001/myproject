<%@ page title="" language="C#" masterpagefile="~/MasterPage/InfoList.master" autoeventwireup="true" inherits="Survey_result, PageCode" enableviewstate="false" %>

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
				<span class="dc_tc"><span class="fs_t">
					<%=strVoteName %></span> <span class="fs_c">
						<%=strSurveyDesc%></span></span><br />
				<asp:Repeater ID="rpQuestion" runat="server">
					<ItemTemplate>
						<div class="clear" style="text-align: left;">
							<br />
							<div>
								<%# Eval("QuestionContent").ToString()%>：</div>
							<br />
							<%# GetQuestionOptions( Convert.ToInt32(Eval("QuestionID").ToString())) %>
						</div>
					</ItemTemplate>
				</asp:Repeater>
			</div>
		</div>
		<!--右侧 end-->
	</div>
</asp:Content>
