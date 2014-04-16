<%@ page language="C#" autoeventwireup="true" inherits="Report_PriceInput, PageCode" theme="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="品类" DataField="ItemName" />
            <asp:BoundField HeaderText="规格等级" DataField="ItemLevel" />
            <asp:BoundField HeaderText="价格单位" DataField="ItemUnit" />
            <asp:TemplateField HeaderText="价格">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="TextBorder Width-50"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="noneDisplay" HeaderStyle-CssClass="noneDisplay">
                <ItemTemplate>
                    <asp:TextBox ID="txtItemID" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />

    </div>
    </form>
</body>
</html>
