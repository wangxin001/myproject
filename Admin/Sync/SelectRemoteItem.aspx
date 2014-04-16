<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectRemoteItem.aspx.cs"
    Inherits="Sync_SelectRemoteItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .leftpanel
        {
            height: 400px;
            border: 1px solid black;
            width: 200px;
            float: left;
            overflow: auto;
            display: block;
        }
        
        .rightpanel
        {
            height: 400px;
            border: 1px solid black;
            width: 480px;
            float: left;
            overflow: auto;
            display: block;
            margin: 0 10px 0 10px;
        }
        
        .rightItem
        {
            height: 400px;
            width: 240px;
            float: left;
            overflow: auto;
            display: block;
        }
        
        .controlpanel
        {
            height: 50px;
            width: 990px;
            float: right;
            text-align: right;
            padding: 10px 0 0 0;
        }
        
        .odd
        {
            background: #EEEEEE;
        }
    </style>
    <script src="../jQuery/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function GetItemList(code) {
            var message = code;
            var context = code;

            WebForm_DoCallback('__Page', message, ShowPendingMessage, context, null, false);
        }

        var trFormat = "<tr style='color: {4}'><td align='center'><input type='radio' name='item' value='{0}' onclick='javascript:SelectItem(this, {5})' /></td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>";

        var items;

        function ShowPendingMessage(message, context) {

            items = eval('(' + message + ')');

            $("#itemTable tbody").html("");

            for (var i = 0; i < items.length; i++) {

                var item = items[i];

                var tr = String.format(trFormat, item.ItemCode, item.ItemName, item.CataName, item.SpecName, item.Active == 1 ? "#000000" : "#888888", i);

                $("#itemTable tbody").append(tr);
            }

            $("#itemTable tbody tr:odd").addClass("odd");

            $("#txtMessage").html(String.format("<b>共 {0} 条</b>", items.length));

            $("#txtCode").val("");
        }

        function SelectItem(obj, idx) {
            $("#txtName").html(items[idx].ItemName);
            $("#txtCodeLabel").html(items[idx].ItemCode);
            $("#txtCode").val(items[idx].ItemCode);
            $("#txtTestValue").html("");
        }

        function Submit() {

            if ($("#txtCode").val() == "") {
                alert("请选择指标业务对象");
                return false;
            }

            return true;
        }

        function cancel() {
            parent.Cancel();
        }

        String.format = function () {
            if (arguments.length == 0)
                return null;

            var str = arguments[0];
            for (var i = 1; i < arguments.length; i++) {
                var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
                str = str.replace(re, arguments[i]);
            }
            return str;
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="leftpanel">
        <asp:TreeView ID="TreeView1" runat="server" NodeIndent="15" SelectedNodeStyle-BackColor="#CCCCCC"
            OnTreeNodePopulate="TreeView1_TreeNodePopulate">
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <NodeStyle Font-Size="10pt" ForeColor="Black" HorizontalPadding="4px" NodeSpacing="0px"
                VerticalPadding="2px" />
        </asp:TreeView>
    </div>
    <div class="rightpanel">
        <table id="itemTable" cellspacing="0" cellpadding="3" border="1" style="background-color: White;
            border-color: #999999; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;
            clear: both" rules="cols">
            <thead>
                <tr align="center" style="color: White; background-color: #000084; font-weight: bold;">
                    <th scope="col">
                        选择
                    </th>
                    <th scope="col">
                        编号
                    </th>
                    <th scope="col">
                        名称
                    </th>
                    <th scope="col">
                        品种
                    </th>
                    <th scope="col">
                        规格等级
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div class="rightItem">
        <table class="TableInfo" cellpadding="2" cellspacing="1">
            <tr>
                <td class="ItemRight Width-50">
                    名称：
                </td>
                <td>
                    <asp:Label ID="txtItemName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="ItemRight">
                    指标：
                </td>
                <td>
                    <asp:Label ID="txtPriceField" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table class="TableInfo" cellpadding="2" cellspacing="1">
            <tr>
                <td class="ItemRight Width-50">
                    条目：
                </td>
                <td>
                    <asp:Label ID="txtName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="ItemRight Width-50">
                    编号：
                </td>
                <td>
                    <asp:Label ID="txtCodeLabel" runat="server" Text=""></asp:Label>
                    <asp:TextBox ID="txtCode" runat="server" Style="display: none"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="ItemRight">
                    环节：
                </td>
                <td>
                    <asp:DropDownList ID="ddpStageCode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="ItemRight">
                    指标：
                </td>
                <td>
                    <asp:DropDownList ID="ddpTargetCode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="ItemRight">
                    倍率：
                </td>
                <td>
                    <asp:TextBox ID="txtRate" runat="server" CssClass=" TextBorder Width-100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="ItemRight">
                    日期：
                </td>
                <td>
                    <asp:TextBox ID="txtPriceDate" runat="server" CssClass=" TextBorder Width-100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="ItemRight">
                    <asp:Button ID="btnTest" runat="server" Text="测试" OnClick="btnTest_Click" />
                </td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="txtTestValue" runat="server" Text=""></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnTest" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div style="clear: both;" />
    <div class="controlpanel">
        <span id="txtMessage"></span>
        <asp:Button ID="btnSubmit" runat="server" Text="确定" OnClientClick="javascript:return Submit();"
            OnClick="btnSubmit_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="删除" OnClientClick="javascript:confirm('确认删除当前关联信息？');"
            OnClick="btnDelete_Click" />
        <input type="button" value="取消" onclick="javascript:cancel()" />
    </div>
    </form>
</body>
</html>
