<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminPage.master" AutoEventWireup="true" EnableEventValidation="false" 
    CodeFile="SyncItemPrice.aspx.cs" Inherits="Sync_SyncItemPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../jQuery/css/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript" src="../jQuery/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="../jQuery/js/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript">

        $(function () {
            $("#<%=txtPriceDate.ClientID %>").datepicker();
        });

        function FieldManage(id, i) {

            document.getElementById("dialogContent").src = "./SelectRemoteItem.aspx?ItemId=" + id + "&index=" + i;

            var targetName = "";

            if (i == 1) {
                targetName = "成交量";
            }
            else if (i == 2) {
                targetName = "批发价";
            }
            else if (i == 3) {
                targetName = "农贸零售价";
            }
            else if (i == 4) {
                targetName = "超市零售价";
            }
            else {
                alert("参数错误");
                return;
            }

            $("#dialog").dialog({ title: '选择关联 指标业务对象 -- ' + targetName, resizable: false, width: 975, height: 520, modal: true });
            $('#dialog').dialog('open');

        }

        function Reload() {
            $('#dialog').dialog('close');
            document.getElementById("<%= btnReload.ClientID %>").click();
        }

        function Cancel() {
            $('#dialog').dialog('close');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divCurrentPosition">
        当前位置：同步
        <asp:Label runat="server" ID="txtTitle" Text=""></asp:Label>
        价格
    </div>
    <div class="divToolBar">
        <asp:TextBox ID="txtPriceDate" runat="server" CssClass="Width-80 TextBorder"></asp:TextBox>
        <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="txtQuery" runat="server" Text="查询" OnClick="txtQuery_Click" />
        <asp:Button ID="btnSync" runat="server" Text="同步数据" OnClick="btnSync_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="删除数据" OnClick="btnDelete_Click" />
        <asp:Button ID="btnReload" runat="server" Text="Reload" OnClick="btnReload_Click"
            Style="display: none;" />
    </div>
    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="名称" DataField="ItemName" />
            <asp:BoundField HeaderText="规格等级" DataField="ItemLevel" />
            <asp:BoundField HeaderText="价格单位" DataField="ItemUnit" />
            <asp:BoundField HeaderText="报价日期" DataField="PriceDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField HeaderText="成交量">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" /><br />
                    <asp:Label ID="txtYesterday1" runat="server" Text="0.0%"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="批发价">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink2" runat="server" /><br />
                    <asp:Label ID="txtYesterday2" runat="server" Text="0.0%"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="农贸零售价">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink3" runat="server" /><br />
                    <asp:Label ID="txtYesterday3" runat="server" Text="0.0%"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="超市零售价">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink4" runat="server" /><br />
                    <asp:Label ID="txtYesterday4" runat="server" Text="0.0%"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="divToolBar">
        <asp:Button ID="btnApprove" runat="server" Text="价格入库" OnClick="btnApprove_Click"
            OnClientClick="javascript:return confirm('确定要将当前价格正式入库？');" />
        <br />
        <br />
        <b>说明：</b><br />
        修改日期后，请先点击 查询 按钮切换到指定的日期。<br />
        每个指标前加 * 并且字体加粗，表示该条目已经完成关联操作。<br />
        黑色字体表示当前条目价格尚未审批入库。<br />
        绿色加粗字体标识当前条目已经完成审批入库。<br />
    </div>
    <div id="dialog" style="display: none;">
        <iframe frameborder="0" id="dialogContent" src="" style="width: 100%; height: 100%;"
            scrolling="no" />
    </div>
</asp:Content>
