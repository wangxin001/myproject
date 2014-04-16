<%@ page title="" language="C#" masterpagefile="~/report/MasterPage.master" autoeventwireup="true" inherits="report_Infor1, PageCode" enableviewstate="true" %>

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
                 资料编辑</a>
                </span>
    </div>
    <div class="clear">
    </div>
    <div class="zbody_b">
        <!--左侧 begin-->
        <div class="zb_le">
            <span class="menu">
                <ul>
                    <li class="me_off"><a href="./Fillin.aspx">菜价填报</a> </li>
                    <li class="me_off"><a href="./query.aspx">菜价查询</a> </li>
                    <li class="me_on"><a href="./Infor1.aspx">本店资料</a> </li> 
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
                        <h2>资料编辑</h2>
                        <div class="inf_tab">
                              <table width="100%" border="0" cellspacing="0" cellpadding="1">
                              <tr>
                                <td colspan="2"><div class="tit red">带（*）为必填选项</div></td>
                                </tr>
                              <tr>
                                <td width="26%"><span class="red">*</span><strong>平价菜店名称：</strong></td>
                                <td width="74%">
                                    <asp:TextBox ID="txtStoreName" class="int" runat="server"></asp:TextBox>
                                </td>
                              </tr>
                              <tr>
                                <td><span class="red">*</span><strong>联系人姓名：</strong></td>
                                <td><asp:TextBox ID="txtContactName" class="int" runat="server"></asp:TextBox></td>
                              </tr>
                              <tr>
                                <td><span class="red">*</span><strong>联系人职位：</strong></td>
                                <td><asp:TextBox ID="txtContactTitle" class="int" runat="server"></asp:TextBox></td>
                              </tr>
                              <tr>
                                <td><span class="red">*</span><strong>联系电话：</strong></td>
                                <td><asp:TextBox ID="txtContactPhone" class="int" runat="server"></asp:TextBox></td>
                              </tr>
                              <tr>
                                <td><span class="red">*</span><strong>手 机：</strong></td>
                                <td><asp:TextBox ID="txtContactMobile" class="int" runat="server"></asp:TextBox></td>
                              </tr>
                              <tr>
                                <td><span class="red">*</span><strong>邮 箱：</strong></td>
                                <td><asp:TextBox ID="txtContactEmail" class="int" runat="server"></asp:TextBox></td>
                              </tr>
                              <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td>&nbsp;</td>
                                <td class="bot">
                                    <asp:Button class="int_a" ID="btnSubmit" runat="server" Text="" 
                                        onclick="btnSubmit_Click" />
                               <input class="int_b" name="" type="reset"value="" /> <a href="./Infor2.aspx" >修改密码</a></td>
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

