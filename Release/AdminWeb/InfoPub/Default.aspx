<%@ page title="" language="C#" masterpagefile="~/MasterPage/AdminPage.master" autoeventwireup="true" inherits="InfoPub_Default, PageCode" theme="_Default" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript">

        function CloseFrame() {
            $('#dialog').dialog('close');
        }

        function OpenNewBoard() {
            document.getElementById("dialogContent").src = "./BoardEdit.aspx";

            $("#dialog").dialog({ title: '新建栏目', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function btnMod_onclick() {
            var sid = "";

            var bs = document.getElementsByName("BoardItem");
            for (var i = 0; i < bs.length; i++) {
                var b = bs[i];

                if (b.checked) {
                    if (sid == "") {
                        sid = b.id;
                    }
                    else {
                        alert("对不起，您只能选择一个栏目进行修改");
                        return false;
                    }
                }
            }

            if (sid == "") {
                alert("请选择需修改的栏目");
                return false;
            }

            document.getElementById("dialogContent").src = "./BoardEdit.aspx?BoardID=" + sid;

            $("#dialog").dialog({ title: '修改栏目', resizable: false, width: 400, height: 210, modal: true });
            $('#dialog').dialog('open');
        }

        function SelectAll(obj) {
            var bs = document.getElementsByName("BoardItem");
            for (var i = 0; i < bs.length; i++) {
                bs[i].checked = obj.checked;
            }
        }

        function ReloadPage() {
            CloseFrame();
            document.getElementById("<%=btnReload.ClientID %>").click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：<asp:Label ID="labelBoardInfo" runat="server" Text=""></asp:Label>
    </div>
    <div class="divToolBar">
        <asp:Button ID="btnAdd" runat="server" Text="添加" OnClientClick="javascript:OpenNewBoard();return false;" />
        <input id="btnMod" type="button" value="修改" onclick="return btnMod_onclick()" />
        <asp:Button ID="btnDel" runat="server" Text="删除" OnClick="btnDel_Click" OnClientClick="return confirm('确定删除所选栏目？');" />
        <asp:Button ID="btnReload" runat="server" Text="重新加载" CssClass="ItemHidden" OnClick="btnReload_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" id="SelectItem" name="Items" onclick="return SelectAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" id="<%# Eval("BoardID").ToString() %>" name="BoardItem" value="<%# Eval("BoardID").ToString() %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="BoardID" HeaderText="编号" />
            <asp:BoundField DataField="BoardTitle" HeaderText="栏目名称" ItemStyle-CssClass="ItemLeft" />
            <asp:BoundField DataField="DisplayName" HeaderText="显示名称" ItemStyle-CssClass="ItemLeft" />
            <asp:BoundField DataField="BoardType" HeaderText="栏目类型" />
            <asp:BoundField DataField="Created" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:BoundField DataField="UserName" HeaderText="创建者" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%# (Eval("Status").ToString()=="1")?"启用":"禁用" %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="divToolBar">
        <webdiyer:AspNetPager ID="AspNetPager" runat="server" OnPageChanged="AspNetPager_PageChanged">
        </webdiyer:AspNetPager>
    </div>
    <div id="dialog" title="栏目名称" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;">
        </iframe>
    </div>
</asp:Content>
