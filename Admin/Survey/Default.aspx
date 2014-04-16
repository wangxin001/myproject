<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
	CodeFile="Default.aspx.cs" Inherits="Survey_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<script language="javascript" type="text/javascript">
<!--

		function btnAdd_onclick()
		{
			location.href = "./SurveyEdit.aspx";
		}

// -->
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="divCurrentPosition">
		当前位置：调查管理
	</div>
	<div class="divToolBar">
		<input id="btnAdd" type="button" value="新建调查" onclick="return btnAdd_onclick()" />
	</div>
	<asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
		<Columns>
			<asp:BoundField HeaderText="调查名称" DataField="VoteName" HeaderStyle-CssClass="Width-180" />
			<asp:BoundField HeaderText="创建时间" DataField="Created" />
			<asp:BoundField HeaderText="结果是否公开" DataField="" />
			<asp:BoundField HeaderText="调查状态" DataField="Status" />
			<asp:TemplateField HeaderText="操作" HeaderStyle-CssClass="Width-120">
				<ItemTemplate>
					<asp:LinkButton ID="btnModify" runat="server">修改</asp:LinkButton>
					<asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('调查删除后，将无法恢复。确认删除？')">删除</asp:LinkButton>
					<asp:LinkButton ID="btnStart" runat="server" OnClientClick="return confirm('确认启动所选调查？')">启动</asp:LinkButton>
					<asp:LinkButton ID="btnStop" runat="server" OnClientClick="return confirm('确认结束所选调查？')">结束</asp:LinkButton>
					<asp:LinkButton ID="btnInfo" runat="server">查看</asp:LinkButton>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
</asp:Content>
