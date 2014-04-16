<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true"
    CodeFile="SyncDictValue.aspx.cs" Inherits="Sync_SyncDictValue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：同步字典数据
    </div>
    <div class="divToolBar">
        <asp:DropDownList ID="ddpKeyValueType" runat="server" OnSelectedIndexChanged="ddpKeyValueType_SelectedIndexChanged"
            AutoPostBack="true">
        </asp:DropDownList>
        <asp:Button ID="btnSync" runat="server" Text="同步数据" OnClick="btnSync_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="删除现有数据" OnClick="btnDelete_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="编号" />
            <asp:BoundField DataField="Name" HeaderText="名称" />
            <asp:BoundField DataField="Active" HeaderText="是否有效" />
        </Columns>
    </asp:GridView>
    <div class="divToolBar">
        共
        <asp:Label ID="txtTotalSize" runat="server" Text="" Font-Bold="true"></asp:Label>
        条 &nbsp;&nbsp;&nbsp;
        <asp:Label ID="txtLastSyncDate" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
