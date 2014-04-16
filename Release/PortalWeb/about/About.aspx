<%@ page title="" language="C#" masterpagefile="~/MasterPage/InfoList.master" autoeventwireup="true" inherits="about_About, PageCode" enableviewstate="false" %>

<%@ OutputCache CacheProfile="Cache1hourByTopicID" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <!--左侧标题 begin-->
    <div class="jgj_top">
    </div>
    <!--左侧标题 end-->
    <div class="wei_ri">
        <a href="../index.aspx">首页</a> > <a href="About.aspx">机构介绍</a> > <span class="we_te">
            <a href="./About.aspx">职能介绍</a></span>
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
                            <li class="me_on"><a href="About.aspx">职能介绍</a> </li>
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
                <span class="gz_title">职能介绍</span><span class="gz_tc">
                    <%=strTopicContent %>
                </span>
                <br />
                <span class="sgz_tjg">
                    <ul id="slideshow">
                        <asp:Repeater ID="rpPhoto" runat="server">
                            <ItemTemplate>
                                <li>
                                    <h3>
                                    </h3>
                                    <span>../upload/<%# Eval("TopicContent").ToString() %></span>
                                    <p>
                                    </p>
                                    <a href="#">
                                        <img src="../upload/<%# Eval("TopicContent").ToString() %>" alt="" /></a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div id="wrapper">
                        <div id="fullsize">
                            <div id="imgprev" class="imgnav" title="上一张">
                            </div>
                            <div id="imglink">
                            </div>
                            <div id="imgnext" class="imgnav" title="下一张">
                            </div>
                            <div id="image">
                            </div>
                            <div id="information">
                                <h3>
                                </h3>
                                <p>
                                </p>
                            </div>
                        </div>
                        <div id="thumbnails">
                            <div id="slideleft" title="向右移">
                            </div>
                            <div id="slidearea">
                                <div id="slider">
                                </div>
                            </div>
                            <div id="slideright" title="向左移">
                            </div>
                        </div>
                    </div>
                    <script type="text/javascript" src="../javascript/compressed.js"></script>
                    <script type="text/javascript">
                        $('slideshow').style.display = 'none';
                        $('wrapper').style.display = 'block';
                        var slideshow = new TINY.slideshow("slideshow");
                        window.onload = function () {
                            slideshow.auto = true;
                            slideshow.speed = 5;
                            slideshow.link = "linkhover";
                            slideshow.thumbs = "slider";
                            slideshow.left = "slideleft";
                            slideshow.right = "slideright";
                            slideshow.scrollSpeed = 4;
                            slideshow.spacing = 2;
                            slideshow.active = "#000";
                            slideshow.init("slideshow", "image", "imgprev", "imgnext", "imglink");
                        }
                    </script>
                </span>
            </div>
        </div>
        <!--右侧 end-->
    </div>
</asp:Content>
