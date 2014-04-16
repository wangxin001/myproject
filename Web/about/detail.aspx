<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/InfoList.master" AutoEventWireup="true"
    CodeFile="detail.aspx.cs" Inherits="about_detail" %>

<%@ OutputCache CacheProfile="Cache1hourByTopicID" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <!--左侧标题 begin-->
    <div class="jgj_top">
    </div>
    <!--左侧标题 end-->
    <div class="wei_ri">
        <a href="../index.aspx">首页</a> > <a href="About.aspx">机构介绍</a> > <span class="we_te">
            <a href="./detail_<%=strTopicID %>.aspx">
                <%=strTopicTitle %></a></span>
    </div>
    <div class="clear">
    </div>
    <div class="zbody_b">
        <!--左侧 begin-->
        <div class="zb_le">
            <span class="menu">
                <ul>
                    <asp:Repeater ID="rpMenu" runat="server">
                        <HeaderTemplate>
                            <li class="me_off"><a href="About.aspx">职能介绍</a> </li>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="<%# strTopicID==Eval("TopicID").ToString()?"me_on":"me_off" %>"><a href="detail_<%# Eval("TopicID").ToString() %>.aspx">
                                <%# Eval("TopicTitle").ToString() %></a> </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li class="me_off"><a href="district.aspx">区县部门介绍</a> </li>
                    <%--<li class="me_off"><a href="monitor.aspx">春夏秋冬话监测</a> </li>--%>
                </ul>
            </span>
        </div>
        <!--左侧 end-->
        <!--右侧 begin-->
        <div class="zb_ri">
            <div class="gza">
                <span class="gz_title">
                    <%=strTopicTitle %></span><span class="gz_tc">
                        <%=strTopicContent %>
                    </span>
            </div>
        </div>
        <!--右侧 end-->
    </div>
</asp:Content>
