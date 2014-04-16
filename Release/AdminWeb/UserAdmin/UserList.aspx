<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="UserAdmin_UserList, PageCode" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery.ui.datepicker-zh-CN.js"></script>
	<script type="text/javascript">

		function CloseFrame()
		{
			$('#dialog').dialog('close');
		}

		function btnAdd_onclick()
		{
			document.getElementById("dialogContent").src = "./UserEdit.aspx";

			$("#dialog").dialog({ title: '新建帐号', resizable: false, width: 450, height: 300, modal: true });
			$('#dialog').dialog('open');
		}

		function UserMod(ID)
		{
			document.getElementById("dialogContent").src = "./UserEdit.aspx?UserGUID=" + ID;

			$("#dialog").dialog({ title: '修改帐号', resizable: false, width: 450, height: 300, modal: true });
			$('#dialog').dialog('open');
		}

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="divCurrentPosition">
		当前位置：帐号管理
	</div>
	<div class="divToolBar">
		<input id="btnAdd" type="button" value="新建帐号" onclick="return btnAdd_onclick()" />
	</div>
	<asp:GridView ID="GridView1" runat="server" 
		OnRowDataBound="GridView1_RowDataBound" onrowcommand="GridView1_RowCommand">
		<Columns>
			<asp:BoundField DataField="UserAccount" HeaderText="登录帐号" />
			<asp:BoundField DataField="UserName" HeaderText="实际姓名" />
			<asp:BoundField DataField="Created" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
			<asp:BoundField DataField="UserRole" HeaderText="用户角色" />
			<asp:BoundField DataField="Status" HeaderText="状态" />
			<asp:TemplateField HeaderText="操作">
				<ItemTemplate>
					<asp:HyperLink ID="hyMod" runat="server">修改</asp:HyperLink>
					<asp:LinkButton ID="btnDel" runat="server">删除</asp:LinkButton>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<div id="dialog" style="display: none;">
		<iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;">
		</iframe>
	</div>
</asp:Content>
