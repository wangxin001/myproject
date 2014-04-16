<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="InfoPub_TopicList, PageCode" theme="_Default" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
<!--

        function btnAdd_onclick()
        {
            location.href = "TopicEdit.aspx?BoardID=<%=strBoardID %>";
        }

// -->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：<asp:Label ID="labelBoardInfo" runat="server" Text=""></asp:Label>
    </div>
    <div class="divToolBar">
        <input id="btnAdd" type="button" value="发布文章" onclick="return btnAdd_onclick()" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"
        OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="Width-30">
                <HeaderTemplate>
                    <input type="checkbox" id="SelectItem" name="Items" onclick="return SelectAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" id="<%# Eval("TopicID").ToString() %>" name="TopicItem" value="<%# Eval("TopicID").ToString() %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="编号" DataField="TopicID" HeaderStyle-CssClass="Width-30" />
            <asp:HyperLinkField HeaderText="标题" DataTextField="TopicTitle" DataNavigateUrlFields="TopicID"
                DataNavigateUrlFormatString="./InfoView.aspx?InfoID={0}" Target="_blank" ItemStyle-CssClass="ItemLeft" />
            <asp:BoundField HeaderText="发布者" DataField="Publisher"  HeaderStyle-CssClass="Width-60"/>
            <asp:BoundField HeaderText="发布日期" DataField="PublishDate" DataFormatString="{0:yyyy-MM-dd}"  HeaderStyle-CssClass="Width-80"/>
            <asp:BoundField HeaderText="创建者" DataField="UserName"  HeaderStyle-CssClass="Width-60" />
            <asp:BoundField HeaderText="创建时间" DataField="Created" DataFormatString="{0:yyyy-MM-dd HH:mm}"  HeaderStyle-CssClass="Width-80"/>
            <asp:TemplateField HeaderText="操作"  HeaderStyle-CssClass="Width-100">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDel" runat="server">删除</asp:LinkButton>
                    <asp:HyperLink ID="hyMod" runat="server">修改</asp:HyperLink>
                    <asp:LinkButton ID="btnHidden" runat="server">隐藏</asp:LinkButton>
                    <asp:LinkButton ID="btnPublic" runat="server">公开</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="divToolBar">
        <webdiyer:AspNetPager ID="AspNetPager" runat="server" OnPageChanged="AspNetPager_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
