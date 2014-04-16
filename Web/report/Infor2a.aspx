<%@ Page Title="" Language="C#" MasterPageFile="~/report/MasterPage.master" AutoEventWireup="true" CodeFile="Infor2a.aspx.cs" Inherits="report_Infor2a" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<!--左侧标题 begin-->
    <div class="parity_top">
    </div>
    <!--左侧标题 end-->
    <div class="wei_ri">
        <a href="../index.aspx">首页</a> > <a href="###">平价菜店</a> > <span class="we_te"><a href="###">修改密码</a></span>
    </div>
    <div class="clear">
    </div>
    <div class="zbody_b">
        <!--左侧 begin-->
        <div class="zb_le">
            <span class="menu">
                <ul>
                   <li class="me_off"><a href="./price.aspx">菜价查询</a> </li>
                    <li class="me_on"><a href="./Infor1a.aspx">个人资料</a> </li>
                </ul>
            </span>
            <div class="clear"></div>
            <div class="left_img">
            <img src="../images/parity_09.jpg" width="212" height="73" /></div>
        </div>
        <!--左侧 end-->
        <!--右侧 begin-->
        <div class="zb_ri">
        		<div class="inf">
                	<div class="top"><span> <%=strStoreName %> </span><i><%= DateTime.Now.Date.ToString("yyyy-MM-dd") %>  </i> </div>
                    <div class="cont">                       
                        <h2>修改密码</h2>
                        <div class="inf_tab">
                              <table width="100%" border="0" cellspacing="0" cellpadding="1">
                              <tr>
                                <td width="26%"><span class="red">&nbsp;</span><strong>当前密码：</strong></td>
                                <td width="74%">
                                    <asp:TextBox class="int" TextMode=Password ID="txtCurrentpwd" runat="server"></asp:TextBox>
                               </td>
                              </tr>
                              <tr>
                                <td><span class="red">&nbsp;</span><strong>新密码：</strong></td>
                                <td><asp:TextBox class="int" TextMode=Password ID="txtpwd1" runat="server"></asp:TextBox></td>
                              </tr>
                               <tr>
                                <td><span class="red">&nbsp;</span><strong>确认新密码：</strong></td>
                                <td><asp:TextBox class="int" TextMode=Password ID="txtpwd2" runat="server"></asp:TextBox></td>
                              </tr>
       						  <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td>&nbsp;</td>
                                <td class="bot">
                                    <asp:Button class="int_c" ID="btnSubmit" runat="server" Text="" 
                                        onclick="btnSubmit_Click" />
                                </td>
                              </tr>
                              </table>
                            </div>
                            
                    </div>
                    <div class="bottom"> </div>
                </div>  
      	</div>
        <!--右侧 end-->
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

