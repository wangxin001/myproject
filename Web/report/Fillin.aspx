<%@ Page Title="" Language="C#" MasterPageFile="~/report/MasterPage.master" AutoEventWireup="true" CodeFile="Fillin.aspx.cs" Inherits="report_Fillin"  EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

<!--左侧标题 begin-->
    <div class="parity_top">
    </div>
    <!--左侧标题 end-->
    <div class="wei_ri">
        <a href="../index.aspx">首页</a> > <a href="###">平价菜店</a> > <span class="we_te">
        <a href="###">
                 菜价填报</a>
                </span>
    </div>
    <div class="clear">
    </div>
    <div class="zbody_b">
        <!--左侧 begin-->
        <div class="zb_le">
            <span class="menu">
                <ul>
                    <li class="me_on"><a href="./Fillin.aspx">菜价填报</a> </li>
                    <li class="me_off"><a href="./query.aspx">菜价查询</a> </li>
                    <li class="me_off"><a href="./Infor1.aspx">本店资料</a> </li> 
                </ul>
            </span>
            <div class="clear"></div>
            <div class="left_img">
            <img src="../images/parity_09.jpg" width="212" height="73" /></div>
        </div>
        <!--左侧 end-->
        <!--右侧 begin-->
        <div class="zb_ri">
          <div class="greengrocer_tit">
            <span>
            <%= strStoreName %>
            </span><i>
            <%= strStorePriceDate %>
                
            </i>  
            <span>&nbsp;&nbsp;
            <asp:LinkButton ID="btnCopyData" runat="server" onclick="btnCopyData_Click">>>复制前一天价格</asp:LinkButton>
            </span>
            </div>
            
            <div class="g_tab_tit">
            	<table width="100%" border="0" cellspacing="0" cellpadding="1">
                  <tr>
                    <td align="center" valign="middle" class="ta_1" width="320">
                    <span>商品信息</span> <span><%= strStorePriceDate %></span> <BR />
                    </td>
                    <td width="84" rowspan="2" align="center" valign="middle" class="ta_2">
                    市平均价<Br />
                    <%= strAveragePriceDate%></td>
                   	 <td width="98" rowspan="2" align="center" valign="middle" class="ta_3">
                    本店零售价<Br />
                    <%= strStorePriceDate %>
                    </td>
                    <td width="109" rowspan="2" align="center" valign="middle" class="ta_3">
                    填报时间
                    </td>

                  </tr>
                  <tr>
                    <td align="left" valign="middle" class="ta_4">
                    	<span class="sx1">序号 </span>
                        <span class="sx2">品类</span>
                        <span class="sx3">规格等级</span>
                        <span class="sx4">价格单位</span>
                    </td>
                  </tr>
                </table>
            </div>

            <div class="g_tab_cont">
            	<ul class="g_list">
                    <asp:Repeater ID="Repeater1" runat="server" 
                        onitemdatabound="Repeater1_ItemDataBound" EnableViewState="true">
                        <ItemTemplate>
                    
                            <li>
                                <asp:Label ID="txtItemOrder" runat="server" Text="" class="cx1"></asp:Label>
                                <asp:Label ID="txtItemName" runat="server" Text="" class="cx2"></asp:Label>
                                <asp:Label ID="txtItemLevel" runat="server" Text="" class="cx3"></asp:Label>
                                <asp:Label ID="txtItemUnit" runat="server" Text="" class="cx4"></asp:Label>
                                <asp:Label ID="txtPrice03" runat="server" Text="" class="cx5"></asp:Label>

                                <span class="cx6" style=" height:20px">
                                    <asp:TextBox ID="txtItemPrice" runat="server" style="width:40px; height:16px"  CssClass="inputText" ></asp:TextBox>
                                    <asp:TextBox ID="txtItemPriceLast" runat="server" style="display:none" ></asp:TextBox>
                                    
                                </span>

                                <asp:Label ID="txtCreated" runat="server" Text="&nbsp;" class="cx7"></asp:Label>

                            </li>

                        </ItemTemplate>
                    </asp:Repeater>
                    
                </ul>
                
                <div class="date_l">

                    <asp:Button class="but1" ID="btnSubmit" runat="server" Text="" 
                        onclick="btnSubmit_Click" OnClientClick="return validate();" />
                   
                </div>
            </div>
      </div>
        <!--右侧 end-->
    </div>

        </ContentTemplate>

    </asp:UpdatePanel>    


    <script type="text/javascript" src="../common/InputValidCheck.js"></script>

    <script type="text/javascript">

        function validate() {

            var hasConfirm1 = false;

            var inputs = document.getElementsByTagName("input");

            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].type == 'text' && inputs[i].getAttribute("priceitem") == "1") {

                    if (inputs[i].value.trim() == "" || !inputs[i].value.IsNumeric()) {
                        alert("请输入正确的价格。")
                        return false;
                    }

                    if (inputs[i].getAttribute("itemid") == inputs[i + 1].getAttribute("itemid") && inputs[i + 1].getAttribute("priceitem") == "2") {

                        if (inputs[i + 1].value.trim() != "" && inputs[i].value.IsNumeric()) {
                            
                            var v = parseFloat( inputs[i].value ) - parseFloat( inputs[i + 1].value );

                            if (hasConfirm1==false) {

                                if (confirm("请确认数据录入无误后再次提交。\r\n\r\n确认提交本次数据？")) {
                                    hasConfirm1 = true;
                                    continue;
                                }
                                else {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }
    
    </script>

</asp:Content>

