<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
    CodeFile="UserList.aspx.cs" Inherits="Report_UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript">

        function CloseFrame() {
            $('#dialog').dialog('close');
        }

        function btnAdd_onclick() {
            document.getElementById("dialogContent").src = "./UserEdit.aspx";

            $("#dialog").dialog({ title: '新建菜店帐号', resizable: false, width: 450, height: 350, modal: true });
            $('#dialog').dialog('open');
        }

        function UserMod(ID) {
            document.getElementById("dialogContent").src = "./UserEdit.aspx?UserGUID=" + ID;

            $("#dialog").dialog({ title: '修改菜店帐号', resizable: false, width: 450, height: 350, modal: true });
            $('#dialog').dialog('open');
        }


        function openprice(id) {

            window.open("http://www.ccpn.gov.cn/report/queryadmin.aspx?StoreGUID=" + id, "_price", "width=700px, height=500px");

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：菜店信息管理
    </div>
    <div class="divToolBar">
        <input id="btnAdd" type="button" value="新建菜店帐号" onclick="return btnAdd_onclick()" />
    </div>
    
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"
        OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField DataField="UserAccount" HeaderText="登录帐号" />
            <asp:HyperLinkField HeaderText="菜店名称" DataTextField="UserName" DataNavigateUrlFields="UserGUID"
                DataNavigateUrlFormatString="./StoreInfo.aspx?UserGUID={0}" />
            <asp:BoundField DataField="ContactName" HeaderText="联系人姓名" />
            <asp:BoundField DataField="ContactPhone" HeaderText="联系电话" />
            <asp:BoundField DataField="Created" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:BoundField DataField="Status" HeaderText="状态" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:HyperLink ID="hyMod" runat="server">修改</asp:HyperLink>
                    <asp:LinkButton ID="btnDel" runat="server">删除</asp:LinkButton>
                    <asp:HyperLink ID="lnkPrice" runat="server">管理价格</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;">
        </iframe>
    </div>
</asp:Content>
