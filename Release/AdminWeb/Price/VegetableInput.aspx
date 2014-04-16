<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="Price_VegetableInput, PageCode" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：录入今日蔬菜价格
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
            <asp:BoundField HeaderText="名称" DataField="ItemName" />
            <asp:BoundField HeaderText="规格等级" DataField="ItemLevel" />
            <asp:BoundField HeaderText="价格单位" DataField="ItemUnit" />
            <asp:BoundField HeaderText="报价日期" DataField="PriceDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField HeaderText="成交量">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrice01" runat="server" CssClass="TextBorder Width-50"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="批发价">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrice02" runat="server" CssClass="TextBorder Width-50"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="农贸零售价">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrice03" runat="server" CssClass="TextBorder Width-50"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="超市零售价">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrice04" runat="server" CssClass="TextBorder Width-50"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="蔬菜编号" HeaderStyle-CssClass="ItemHidden" ItemStyle-CssClass="ItemHidden">
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
