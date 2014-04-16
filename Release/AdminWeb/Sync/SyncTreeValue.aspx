<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="Sync_SyncTreeValue, PageCode" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：同步品类数据
    </div>
    <div class="divToolBar">
        <asp:HyperLink ID="hyperParent" runat="server">&lt;&lt;返回上一级</asp:HyperLink>
        <asp:Button ID="btnSync" runat="server" Text="同步数据" OnClick="btnSync_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="删除现有数据" OnClick="btnDelete_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="编号">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="品类名称">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink2" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IsLeaf" HeaderText="是否叶子节点" />
            <asp:BoundField DataField="Active" HeaderText="是否有效" />
        </Columns>
    </asp:GridView>
    <div class="divToolBar">
        <asp:Label ID="txtTreeName" runat="server" Text="" Font-Bold="true" />
        &nbsp;&nbsp;&nbsp;&nbsp;
         共
        <asp:Label ID="txtTotalSize" runat="server" Text="" Font-Bold="true"></asp:Label>
        条 &nbsp;&nbsp;&nbsp;
        <asp:Label ID="txtLastSyncDate" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <p>
            说明：点击编号进入下一级 <b>品类</b> 目录列表，可进行品类的同步。<br />
            如当前品类下无下一级目录，则品类编号不可点。<br />
            点击品类名称可进入 <b>指标业务对象</b> 管理页面。
        </p>
    </div>
</asp:Content>
