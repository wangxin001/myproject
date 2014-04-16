<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="Sync_SyncTreeItem, PageCode" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript">

        function OpenItemDetail(code) {

            document.getElementById("dialogContent").src = "./ItemDetail.aspx?ItemCode=" + code;

            $("#dialog").dialog({ title: '指标业务对象详细信息 ', resizable: false, width: 450, height: 300, modal: true });
            $('#dialog').dialog('open');

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：同步
        <asp:DropDownList ID="ddpSyncType" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddpSyncType_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div class="divToolBar">
        <asp:HyperLink ID="hyperParent" runat="server">&lt;&lt;返回品类目录</asp:HyperLink>
        <asp:Button ID="btnSync" runat="server" Text="同步数据" OnClick="btnSync_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="删除现有数据" OnClick="btnDelete_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="编号" />
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Active" HeaderText="是否有效" />
        </Columns>
    </asp:GridView>
    <div class="divToolBar">
        <asp:Label ID="txtTreeName" runat="server" Text="" Font-Bold="true" />
        &nbsp;&nbsp;&nbsp;&nbsp; 共
        <asp:Label ID="txtTotalSize" runat="server" Text="" Font-Bold="true"></asp:Label>
        条 &nbsp;&nbsp;&nbsp;
        <asp:Label ID="txtLastSyncDate" runat="server" Text=""></asp:Label>
    </div>
    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;">
        </iframe>
    </div>
</asp:Content>
