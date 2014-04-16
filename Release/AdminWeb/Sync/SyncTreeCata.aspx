<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="Sync_SyncTreeCata, PageCode" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
            <asp:BoundField DataField="CataCode" HeaderText="编号" />
            <asp:BoundField DataField="CataName" HeaderText="名称" />
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
</asp:Content>

