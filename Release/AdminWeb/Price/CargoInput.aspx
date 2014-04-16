<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="Price_CargoInput, PageCode" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：录入今日大宗商品价格
    </div>
    <div class="divToolBar">
        当前录入日期：
        <asp:Label ID="txtPriceDate" runat="server" Text="" Font-Bold="true" ForeColor="#AA0000"
            Font-Size="16px"></asp:Label>
        <asp:Button ID="btnInput" runat="server" Text="保存" OnClick="btnInput_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="大宗商品名称" DataField="ItemName" />
            <asp:BoundField HeaderText="规格等级" DataField="ItemUnit" />
            <asp:BoundField HeaderText="报价日期" DataField="PriceDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField HeaderText="价格" HeaderStyle-CssClass="Width-80">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrice02" runat="server" CssClass="TextBorder Width-50"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="大宗商品编号" HeaderStyle-CssClass="ItemHidden" ItemStyle-CssClass="ItemHidden">
                <ItemTemplate>
                    <asp:TextBox ID="txtItemID" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="价格编号" HeaderStyle-CssClass="ItemHidden" ItemStyle-CssClass="ItemHidden">
                <ItemTemplate>
                    <asp:TextBox ID="txtPriceID" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
