<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/InfoList.master" AutoEventWireup="true"
	CodeFile="survey.aspx.cs" Inherits="Survey_survey" %>

<%@ OutputCache CacheProfile="Cache1hour" %>
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
				<span class="mar40"><span class="dc_tc"><span class="fs_t">
					<%=strVoteName %></span> <span class="fs_c">
						<%=strSurveyDesc%></span></span><br />
					<asp:Repeater ID="rpQuestion" runat="server">
						<ItemTemplate>
							<br />
							<%# Eval("QuestionContent").ToString()%>：
							<br />
							<span class="dc_tx">
								<%# GetQuestionOptions(Convert.ToInt32(Eval("QuestionID").ToString()), Convert.ToInt32(Eval("QuestionType").ToString()))%>
							</span>
						</ItemTemplate>
					</asp:Repeater>
					<asp:Button ID="btnSubmit" runat="server" CssClass="tj01" Text="提 交" PostBackUrl="~/Survey/save.aspx" />
					<input type="reset" id="reset" runat="server" class="tj02" value="重 置" />
					<input type="hidden" id="surveyID" name="surveyID" value="<%=strVoteID %>" />
					<div class="clear">
					</div>
				</span>
			</div>
		</div>
		<!--右侧 end-->
	</div>
</asp:Content>
