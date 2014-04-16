<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PriceQueryForm.ascx.cs" Inherits="report_PriceQueryForm" EnableViewState="true" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <div class="zb_ri">
          <div class="greengrocer_tit">
            <span>
                <asp:Label ID="txtStoreName" runat="server" Text=""></asp:Label>
            </span><i>
            <%= strPriceDate %> </i>  
            </div>
            
            <div class="g_tab_tit">
            	<table width="100%" border="0" cellspacing="0" cellpadding="1">
                  <tr>
                    <td align="center" valign="middle" class="ta_1" width="360">
                    <span>商品信息</span> <span><%= strPriceTime%></span><BR />
                    </td>
                    <td rowspan="2" align="center" valign="middle" class="ta_2">
                    市平均价<Br />
                    <%=strAvgPriceDate%></td>
                   	 <td rowspan="2" align="center" valign="middle" class="ta_3">
                   本店零售价<Br />
                    <%=strStorePriceDate%>
                    </td>
                  </tr>
                  <tr>
                    <td align="left" valign="middle" class="ta_4">
                    	<span class="s1">序号 </span>
                        <span class="s2">品类</span>
                        <span class="s3">规格等级</span>
                        <span class="s4">价格单位</span>
                    </td>
                  </tr>
                </table>
            </div>

            <div class="g_tab_cont">
            	<ul class="g_list">
                    <asp:Repeater ID="Repeater1" runat="server" 
                        onitemdatabound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                    
                    <li>
                        <asp:Label ID="txtItemOrder" CssClass="c1" runat="server" Text=""></asp:Label>
                        <asp:Label ID="txtItemName" CssClass="c2" runat="server" Text=""></asp:Label>
                        <asp:Label ID="txtItemLevel" CssClass="c3" runat="server" Text=""></asp:Label>
                        <asp:Label ID="txtItemUnit" CssClass="c4" runat="server" Text=""></asp:Label>
                        <asp:Label ID="txtPrice03" CssClass="c5" runat="server" Text=""></asp:Label>
                        <asp:Label ID="txtStorePrice" CssClass="c6" runat="server" Text=""></asp:Label>
                    </li>
                    
                    </ItemTemplate>

                    </asp:Repeater>

                </ul>
                
                <!--分页-->
                <div class="g_page">

                    <asp:Repeater ID="Repeater2" runat="server" 
                        onitemcommand="Repeater2_ItemCommand" onitemdatabound="Repeater2_ItemDataBound">
                    <HeaderTemplate>

                    <asp:LinkButton ID="lnkFirst" runat="server" CssClass="page_bon b1"></asp:LinkButton>
                    <asp:LinkButton ID="lnkPrevious" runat="server" CssClass="page_bon b2"></asp:LinkButton>

                    </HeaderTemplate>

                    <ItemTemplate>
                    
                   <asp:LinkButton ID="lnkPage" runat="server"><%# Eval("BatchID") %></asp:LinkButton>
                    
                    </ItemTemplate>

                    <FooterTemplate>
                    
                  <asp:LinkButton ID="lnkNext" runat="server" CssClass="page_bon b3"></asp:LinkButton>
                    <asp:LinkButton ID="lnkLast" runat="server" CssClass="page_bon b4"></asp:LinkButton>

                    </FooterTemplate>
                    
                    </asp:Repeater>

                </div>
				<!--分页 end-->



                <div class="date_l">
                    <asp:Label ID="txtPrompt" runat="server" Text="选择菜店："></asp:Label>
                    <asp:DropDownList ID="ddpStore" runat="server" DataTextField="UserName" DataValueField="UserGUID">
                    </asp:DropDownList>
                    日期：
                    <asp:TextBox ID="element_id" runat="server"></asp:TextBox>
                    <asp:Button ID="btnQuery" class="bon" runat="server" Text="" 
                        onclick="btnQuery_Click" />

                    <asp:Button class="but2" ID="btnDownload" runat="server" Text="" 
                        onclick="btnDownload_Click" />

                </div>
            </div>
            
            <asp:HiddenField ID="currentBatchID" runat="server" />
            <asp:HiddenField ID="totalBatchCount" runat="server" />
      </div>

      
    </ContentTemplate>
    
        <Triggers>
        <asp:PostBackTrigger ControlID="btnDownload" />
        
        </Triggers>

    </asp:UpdatePanel>   
    
    <script type="text/javascript" src="../jQuery/calendar/jquery.js"></script>
	<script type="text/javascript" src="../jQuery/calendar/jquery.calendar.js"></script>
	<script type="text/javascript">

	    function setCanendar() {
	        $("#<%=element_id.ClientID %>").calendar();
	    }

	    $(function () {
	        setCanendar();
	    })
    </script>