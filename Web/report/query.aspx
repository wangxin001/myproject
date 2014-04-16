<%@ Page Title="" Language="C#" MasterPageFile="~/report/MasterPage.master" AutoEventWireup="true"
    CodeFile="query.aspx.cs" Inherits="report_query" EnableViewState="true" %>

<%@ Register Src="PriceQueryForm.ascx" TagName="PriceQueryForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <!--左侧标题 begin-->
    <div class="parity_top">
    </div>
    <!--左侧标题 end-->
    <div class="wei_ri">
        <a href="../index.aspx">首页</a> > <a href="###">平价菜店 </a>> <span class="we_te"><a
            href="###">菜价查询</a> </span>
    </div>
    <div class="clear">
    </div>
    <div class="zbody_b">
        <!--左侧 begin-->
        <div class="zb_le">
            <span class="menu">
                <ul>
                    <li class="me_off"><a href="./Fillin.aspx">菜价填报</a> </li>
                    <li class="me_on"><a href="./query.aspx">菜价查询</a> </li>
                    <li class="me_off"><a href="./Infor1.aspx">本店资料</a> </li>
                </ul>
            </span>
            <div class="clear">
            </div>
            <div class="left_img">
                <img src="../images/parity_09.jpg" width="212" height="73" /></div>
        </div>
        <!--左侧 end-->
        <!--右侧 begin-->
        <uc1:PriceQueryForm ID="PriceQueryForm1" runat="server" />
        <!--右侧 end-->
    </div>
</asp:Content>
