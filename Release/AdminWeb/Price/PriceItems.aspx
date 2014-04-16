<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="Price_PriceItems, PageCode" theme="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script language="javascript" type="text/javascript">
<!--
        function CloseFrame()
        {
            $('#dialog').dialog('close');
        }

        function btnAdd_onclick()
        {
            var t = document.getElementById("<%=ddlItemType.ClientID %>");

            document.getElementById("dialogContent").src = "./PriceEdit.aspx?ItemType=" + t.options[t.selectedIndex].value;

            $("#dialog").dialog({ title: '添加价格条目', resizable: false, width: 400, height: 280, modal: true });
            $('#dialog').dialog('open');
        }

        function ModifyVegetable(id, type)
        {
            document.getElementById("dialogContent").src = "./PriceEdit.aspx?ItemID=" + id + "&ItemType=" + type;

            $("#dialog").dialog({ title: '修改价格条目', resizable: false, width: 400, height: 280, modal: true });
            $('#dialog').dialog('open');
        }

        function ReloadPage()
        {
            document.getElementById("<%=btnReload.ClientID %>").click();
        }
// -->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：价格条目管理
    </div>
    <div class="divToolBar">
        <asp:DropDownList ID="ddlItemType" runat="server" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged"
            AutoPostBack="true">
        </asp:DropDownList>
        <input id="btnAdd" type="button" value="新增条目" onclick="return btnAdd_onclick()" />
        <asp:Button ID="btnReload" runat="server" Text="Reload" OnClick="btnReload_Click"
            Style="display: none;" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"
        OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="显示顺序" DataField="ItemOrder" />
            <asp:BoundField HeaderText="蔬菜名称" DataField="ItemName" />
            <asp:BoundField HeaderText="规格等级" DataField="ItemLevel" />
            <asp:BoundField HeaderText="价格单位" DataField="ItemUnit" />
            <asp:TemplateField HeaderText="是否需上报">
            <ItemTemplate>
            
            <%# Eval("ReportStatus").ToString()=="0"? "不上报": "上报" %>
            
            </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField HeaderText="添加时间" DataField="Created" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:BoundField HeaderText="状态" DataField="Status" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDel" runat="server">删除</asp:LinkButton>
                    <asp:LinkButton ID="btnMod" runat="server">修改</asp:LinkButton>
                    <asp:LinkButton ID="btnHidden" runat="server">隐藏</asp:LinkButton>
                    <asp:LinkButton ID="btnPublic" runat="server">公开</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div id="dialog" title="栏目名称" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;">
        </iframe>
    </div>
</asp:Content>
