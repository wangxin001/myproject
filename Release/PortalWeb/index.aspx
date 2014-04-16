<%@ page language="C#" autoeventwireup="true" inherits="index, PageCode" enableeventvalidation="false" viewstateencryptionmode="Never" enableviewstate="false" %>

<%@ OutputCache CacheProfile="Cache1hour" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>北京市价格监测中心</title>
    <meta name="keywords" content="京价网、价格、价格监测、北京市价格监测中心、北京价格监测、北京价格">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/stylesheet.css" type="text/css">
    <script type="text/javascript" src="javascript/home.js"></script>
    <script type="text/javascript" src='javascript/pindex_compress.js'></script>
    <script type="text/javascript" src='javascript/tindex_compress.js'></script>
    <script type="text/javascript" src='javascript/windex_compress.js'></script>
    <script type="text/javascript" src='javascript/foucus.js'></script>
</head>
<body style="background-color: transparent;">
    <!--header begin-->
    <script language='javascript' src='javascript/header.js'></script>
    <!--header end-->
    <div class="clear">
    </div>
    <!--middle begin-->
    <div id="middle">
        <!--焦点图 begin-->
        <div class="jd_all">
            <div id="index-splash-block" class="index-splash-block">
                <div id="feature-slide-block" class="feature-slide-block">
                    <asp:Repeater ID="rpFocusImage" runat="server">
                        <ItemTemplate>
                            <div class="feature-slide-preview" style="display: none;">
                                <a href="<%# Eval("TopicLink").ToString()%>" class="screenshot">
                                    <img alt="<%# Eval("TopicTitle").ToString()%>" src="./upload/<%# Util.GetThumbImageURL( Eval("TopicContent").ToString())%>" /></a>
                                <span class="sop_j"><a href="<%# Eval("TopicLink").ToString()%>" target="_blank">
                                    <%# Eval("TopicTitle").ToString()%></a>&nbsp;<img src="images/san.gif" /></span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div id="feature-slide-list" class="feature-slide-list">
                        <a id="feature-slide-list-previous" class="feature-slide-list-previous"></a>
                        <div id="feature-slide-list-items" class="feature-slide-list-items">
                        </div>
                        <a id="feature-slide-list-next" class="feature-slide-list-next"></a>
                    </div>
                </div>
                <script type="text/javascript">
                    initFeatureSlide();
                </script>
            </div>
        </div>
        <!--焦点图 end-->
        <!--工作动态 begin-->
        <div class="gzd_all">
            <span class="gzd_t"><strong>价格资讯</strong></span> <span class="gzd_bot">
                <ul>
                    <asp:Repeater ID="rpJobActive" runat="server">
                        <ItemTemplate>
                            <li><span class="gz_cle"><a href="job/detail_<%# Eval("TopicID").ToString()%>.aspx"
                                target="_blank">
                                <div class="CuteTitle" title="<%# Eval("TopicTitle").ToString()%>">
                                    <%# Eval("TopicTitle").ToString()%></div>
                            </a></span><span class="gz_cri"><%# Boolean.Parse( Eval("ShowDate").ToString())? "["+Convert.ToDateTime(Eval("PublishDate").ToString()).ToString("MM-dd")+"]":"" %>
                            </span></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="clear">
                </div>
                <span class="jrz_g"><a href="job/index.aspx" target="_blank">
                    <img src="images/gz_gd.gif" /></a> </span></span>
        </div>
        <!--工作动态 end-->
        <!--价格资讯 begin-->
        <div class="gzd_all">
            <span class="gzd_t"><strong>价格预警</strong></span> <span class="gzd_bot"><span class="sd_title">
                <asp:Label ID="txtTitle" runat="server" Text=""></asp:Label>
            </span><span class="sd_cons">
                <asp:HyperLink ID="txtContent" runat="server">HyperLink</asp:HyperLink>
            </span>
                <div class="clear">
                </div>
                <span class="jrz_g"><a href="news/index.aspx" target="_blank">
                    <img src="images/gz_gd.gif" /></a> </span></span>
        </div>
        <!--价格资讯 end-->
        <div class="clear">
        </div>
        <!--今日菜价 begin-->
        <div class="jrc_all">
            <iframe name="CurrentStatus" src="Scroll.aspx" allowtransparency="true" width="512"
                height="408" marginwidth="0" marginheight="0" hspace="0" vspace="0" frameborder="0"
                scrolling="no"></iframe>
        </div>
        <!--今日菜价 end-->
        <!--百日菜价走势 CPI begin-->
        <div class="brc_all">
            <iframe name="LineChart" src="LineChart.aspx" allowtransparency="true" width="444"
                height="408" marginwidth="0" marginheight="0" hspace="0" vspace="0" frameborder="0"
                scrolling="no"></iframe>
        </div>
        <!--百日菜价走势 CPI end-->
        <div class="clear">
        </div>
        <!--小banner begin-->
        <div class="ban_all">
            <img src="images/gg1.gif" width="953" height="67" alt="" />
        </div>
        <!--小banner end-->
        <div class="clear">
        </div>
        <!--实用价格查询 价格政策 begin-->
        <div class="shi_all">
            <div class="ce_top">
                <div class="sy_top">
                    <strong>实用价格查询</strong>
                </div>
                <div class="jg_top">
                    <strong>价格政策</strong>
                </div>
            </div>
            <div class="ce_bot">
                <!--实用价格查询 begin-->
                <div class="shij_all">
                    <dl class="d_dl fl">
                        <asp:Repeater ID="Repeater33" runat="server">
                            <HeaderTemplate>
                                <dt>医疗<a href="price/index_33.aspx" class="d_more"><img src="images/d_more.jpg" alt="" /></a></dt>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <dd>
                                    <a href="<%# Eval("TopicType").ToString()=="1" ? ("./price/price_"+Eval("TopicID").ToString()+".aspx") : Eval("TopicLink").ToString() %>"
                                        title="<%# Eval("TopicTitle").ToString()%>" target="_blank">
                                        <%# Eval("TopicTitle").ToString() %></a>
                                </dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>
                    <dl class="d_dl fr">
                        <asp:Repeater ID="Repeater34" runat="server">
                            <HeaderTemplate>
                                <dt>食品<a href="price/index_34.aspx" class="d_more"><img src="images/d_more.jpg" alt="" /></a></dt>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <dd>
                                    <a href="<%# Eval("TopicType").ToString()=="1" ? ("./price/price_"+Eval("TopicID").ToString()+".aspx") : Eval("TopicLink").ToString() %>"
                                        title="<%# Eval("TopicTitle").ToString()%>" target="_blank">
                                        <%# Eval("TopicTitle").ToString() %></a>
                                </dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>
                    <dl class="d_dl fl">
                        <asp:Repeater ID="Repeater35" runat="server">
                            <HeaderTemplate>
                                <dt>居住<a href="price/index_35.aspx" class="d_more"><img src="images/d_more.jpg" alt="" /></a></dt>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <dd>
                                    <a href="<%# Eval("TopicType").ToString()=="1" ? ("./price/price_"+Eval("TopicID").ToString()+".aspx") : Eval("TopicLink").ToString() %>"
                                        title="<%# Eval("TopicTitle").ToString()%>" target="_blank">
                                        <%# Eval("TopicTitle").ToString() %></a>
                                </dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>
                    <dl class="d_dl fr">
                        <asp:Repeater ID="Repeater36" runat="server">
                            <HeaderTemplate>
                                <dt>出行<a href="price/index_36.aspx" class="d_more"><img src="images/d_more.jpg" alt="" /></a></dt>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <dd>
                                    <a href="<%# Eval("TopicType").ToString()=="1" ? ("./price/price_"+Eval("TopicID").ToString()+".aspx") : Eval("TopicLink").ToString() %>"
                                        title="<%# Eval("TopicTitle").ToString()%>" target="_blank">
                                        <%# Eval("TopicTitle").ToString() %></a>
                                </dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>
                    <dl class="d_dl fl">
                        <asp:Repeater ID="Repeater37" runat="server">
                            <HeaderTemplate>
                                <dt>教育<a href="price/index_37.aspx" class="d_more"><img src="images/d_more.jpg" alt="" /></a></dt>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <dd>
                                    <a href="<%# Eval("TopicType").ToString()=="1" ? ("./price/price_"+Eval("TopicID").ToString()+".aspx") : Eval("TopicLink").ToString() %>"
                                        title="<%# Eval("TopicTitle").ToString()%>" target="_blank">
                                        <%# Eval("TopicTitle").ToString() %></a>
                                </dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>
                    <dl class="d_dl fr">
                        <asp:Repeater ID="Repeater38" runat="server">
                            <HeaderTemplate>
                                <dt>其他<a href="price/index_38.aspx" class="d_more"><img src="images/d_more.jpg" alt="" /></a></dt>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <dd>
                                    <a href="<%# Eval("TopicType").ToString()=="1" ? ("./price/price_"+Eval("TopicID").ToString()+".aspx") : Eval("TopicLink").ToString() %>"
                                        title="<%# Eval("TopicTitle").ToString()%>" target="_blank">
                                        <%# Eval("TopicTitle").ToString() %></a>
                                </dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>
                </div>
                <!--实用价格查询 end-->
                <!--价格政策 begin-->
                <div class="sjg_all">
                    <ul>
                        <asp:Repeater ID="rpPricePolicy" runat="server">
                            <ItemTemplate>
                                <li><span class="sgz_cle"><a href="policy/detail_<%# Eval("TopicID").ToString()%>.aspx"
                                    target="_blank">
                                    <div class="CuteTitle" title="<%# Eval("TopicTitle").ToString()%>">
                                        <%# Eval("TopicTitle").ToString()%></div>
                                </a></span><span class="sgz_cri">[<%# Convert.ToDateTime(Eval("PublishDate").ToString()).ToString("MM-dd")%>]
                                </span></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="clear">
                    </div>
                    <span class="jrz_g"><a href="policy/index.aspx" target="_blank">
                        <img src="images/gz_gd.gif" alt="" /></a> </span>
                </div>
                <!--价格政策 end-->
            </div>
        </div>
        <!--实用价格查询 价格政策 end-->
        <!--价格监测定点单位名单 begin-->
        <div class="dcg_all">
            <span class="price"></span>
            <div class="priceL">
                <ul>
                    <li><a title="丰台新发地批发市场" href="/agency/Default.aspx">丰台新发地批发市场</a></li>
                    <li><a title="朝阳大洋路批发市场" href="/agency/Default.aspx">朝阳大洋路批发市场</a></li>
                    <li><a title="昌平水屯批发市场" href="/agency/Default.aspx">昌平水屯批发市场</a></li>
                </ul>
                <div class="clear">
                </div>
                <span class="jrz_g"><a href="./agency/Default.aspx">
                    <img src="images/gz_gd.gif" /></a> </span>
            </div>
        </div>
        <!--价格监测定点单位名单 end-->
        <!--互动交流 begin-->
        <div class="dcg_all" style="margin-top: 10px; *margin-top: 8px">
            <span class="dcg_t"></span>
            <div class="dcg_mm">
                <span class="dcg_cc"><a href="./survey/survey.aspx">
                    <%=strSurveyDesc %></a> </span><span class="dc_bsh"><span class="dc_g01"><a href="survey/survey.aspx">
                        <img src="images/tou.gif" /></a></span> <span class="dc_g02"><a href="survey/result.aspx">
                            <img src="images/cha.gif" /></a></span> </span>
            </div>
            <div class="dcg_b" style="border-bottom: 1px solid #E6E6E6; *height: 6px;">
            </div>
            <span class="dcb_ban"><a href="/medicine/index.html">
                <img src="images/dc_yy.gif" /></a> </span>
        </div>
        <!--互动交流 end-->
        <div class="clear">
        </div>
        <div class="di_logo">
            <table width="970" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <div id="demo" style="overflow: hidden; width: 950px; float: left; margin-left: 7px;
                            display: inline;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td id="marquePic1" valign="top">
                                        <asp:Repeater ID="rpBanner" runat="server">
                                            <HeaderTemplate>
                                                <table align="center" cellpadding="0" cellspace="0" border="0">
                                                    <tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <td valign="top" align="center">
                                                    <span class="lp01"><a href="<%# Eval("TopicLink").ToString() %>" target="_blank">
                                                        <img src="./upload/<%# Eval("TopicContent").ToString() %>" /></a></span>
                                                </td>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tr></table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td id="marquePic2" valign="top">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
                var istop = 1;
                var LorR = 0;
                var speed = 30;
                var demo = document.getElementById("demo");
                var marquePic1 = document.getElementById("marquePic1");
                var marquePic2 = document.getElementById("marquePic2");
                marquePic2.innerHTML = marquePic1.innerHTML;
                demo.scrollLeft = marquePic1.scrollWidth / 2;
                function left() {
                    LorR = 0;
                }
                function right() {
                    LorR = 1;
                }
                function spd(n) {
                    speed = n;
                    clearInterval(MyMar);
                    MyMar = setInterval(Marquee, speed);
                }
                function Marquee() {
                    if (LorR) MarqueeRight();
                    else MarqueeLeft();
                }
                function MarqueeLeft() {
                    with (demo) {
                        if (scrollLeft >= marquePic1.scrollWidth) {
                            scrollLeft = 0
                        } else {
                            scrollLeft++
                        }
                    }
                }
                function MarqueeRight() {
                    with (demo) {
                        if (scrollLeft <= 0) {
                            scrollLeft = marquePic1.scrollWidth
                        } else {
                            scrollLeft--
                        }
                    }
                }
                var MyMar = setInterval(Marquee, speed)
                demo.onmouseover = function () { clearInterval(MyMar) }
                demo.onmouseout = function () { MyMar = setInterval(Marquee, speed) } 
            </script>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="yq_mar">
        <div class="yql_t">
            <strong>友情链接</strong>
        </div>
        <div class="yql_bot">
            <span class="yqsel">
                <script language="JavaScript" type="text/javascript">
                    var select_typep = '1' //下拉列表框的开式：0为选择，1为跳转
                    var input_Namep = 'class_selectp' //隐藏域的ＩＤ
                    var select_Wp = 177;  //选择框的宽;
                    var select_Hp = 23;  //选择框的高; 
                    var font_sizep = 12;  //框里的字体大小;
                    var form_namep = 'form4'  //表单名称;
                    var font_colorp = '#959595'  //字体颜色; 
                    var select_bgImgp = 'images/dsel_b02.gif'
                    var add_itemp = new Array();
                    add_itemp[0] = "国家及各省市发展改革委|0";

                    add_itemp[1] = "国家发展改革委|http://www.sdpc.gov.cn/";
                    add_itemp[2] = "安徽省发展改革委|http://www.ahpc.gov.cn/";
                    add_itemp[3] = "重庆市发展改革委|http://www.cqdpc.gov.cn/";
                    add_itemp[4] = "天津市发展改革委|http://www.tjdpc.gov.cn/";
                    add_itemp[5] = "福建省发展改革委|http://www.fjdpc.gov.cn/";
                    add_itemp[6] = "四川省发展改革委|http://info.sc.cei.gov.cn/";
                    add_itemp[7] = "河北省发展改革委|http://www.hbdrc.gov.cn/";
                    add_itemp[8] = "江西省发展改革委|http://www.jxdpc.gov.cn/";
                    add_itemp[9] = "贵州省发展改革委|http://www.gzdpc.gov.cn/";
                    add_itemp[10] = "山西省发展改革委|http://www.sxdrc.gov.cn/";
                    add_itemp[11] = "山东省发展改革委|http://www.sdfgw.gov.cn/";
                    add_itemp[12] = "云南省发展改革委|http://www.yndpc.yn.gov.cn/";
                    add_itemp[13] = "内蒙古自治区发展改革委|http://www.nmgfgw.gov.cn/";
                    add_itemp[14] = "河南省发展改革委|http://www.hndrc.gov.cn/";
                    add_itemp[15] = "西藏自治区发展改革委|http://www.xdrc.gov.cn/";
                    add_itemp[16] = "辽宁省发展改革委|http://www.lndp.gov.cn/";
                    add_itemp[17] = "湖北省发展改革委|http://www.hbjw.gov.cn/";
                    add_itemp[18] = "陕西省发展改革委|http://www.sndrc.gov.cn/";
                    add_itemp[19] = "吉林省发展改革委|http://www.jlsdrc.gov.cn/";
                    add_itemp[20] = "湖南省发展改革委|http://www.hnfgw.gov.cn/";
                    add_itemp[21] = "甘肃省发展改革委|http://www.gspc.gov.cn";
                    add_itemp[22] = "黑龙江省发展改革委|http://www.hljdpc.gov.cn/";
                    add_itemp[23] = "青海省发展改革委|http://www.qhfgw.gov.cn";
                    add_itemp[24] = "广东省发展改革委|http://www.gddpc.gov.cn/";
                    add_itemp[25] = "上海市发展改革委|http://www.shdrc.gov.cn";
                    add_itemp[26] = "广西自治区发展改革委|http://www.gxdrc.gov.cn";
                    add_itemp[27] = "宁夏自治区发展改革委|http://www.nxdrc.gov.cn";
                    add_itemp[28] = "江苏省发展改革委|http://www.jsdpc.gov.cn";
                    add_itemp[29] = "海南省发展改革委|http://plan.hainan.gov.cn/";
                    add_itemp[30] = "新疆自治区发展改革委|http://www.xjdrc.gov.cn";
                    add_itemp[31] = "浙江省发展改革委|http://www.zjdpc.gov.cn/";

                    show_selectp(select_typep, input_Namep, select_Wp, select_Hp, select_bgImgp, add_itemp, form_namep, font_colorp, font_sizep);
                                  
                </script>
            </span><span class="yqsel">
                <script language="JavaScript" type="text/javascript">
                    var select_typet = '1' //下拉列表框的开式：0为选择，1为跳转
                    var input_Namet = 'class_selectt' //隐藏域的ＩＤ
                    var select_Wt = 177;  //选择框的宽;
                    var select_Ht = 23;  //选择框的高;
                    var font_sizet = 12;  //框里的字体大小;
                    var form_namet = 'form4'  //表单名称;
                    var font_colort = '#959595'  //字体颜色;
                    var select_bgImgt = 'images/dsel_b02.gif'
                    var add_itemt = new Array();
                    add_itemt[0] = "国家、各省市价格监测中心|0";

                    add_itemt[1] = "国家发改委价格监测中心|http://www.chinaprice.com.cn";
                    add_itemt[2] = "广东价格指数平台|http://www.gdprice.org/";
                    add_itemt[3] = "重庆市物价局|http://www.cqpn.gov.cn";
                    add_itemt[4] = "陕西省物价局|http://www.spic.gov.cn/";
                    add_itemt[5] = "广州市物价局|http://www.gzwjj.gov.cn";
                    add_itemt[6] = "南宁市价格监测中心|http://www.nnpn.gov.cn";
                    add_itemt[7] = "辽宁省物价局|http://www.lnprice.gov.cn";
                    add_itemt[8] = "武汉市物价局|http://www.wh-price.gov.cn";
                    add_itemt[9] = "天津市物价局|http://www.tpin.com.cn";
                    add_itemt[10] = "南京市价格监测中心|http://www.njprice.com";
                    add_itemt[11] = "扬州市物价局|http://www.yzp.gov.cn";
                    add_itemt[12] = "杭州市物价局|http://www.hzjg.gov.cn";
                    add_itemt[13] = "深圳市市场监督管理局|http://www.szaic.gov.cn";
                    add_itemt[14] = "福州市物价局|http://www.fz12358.com";
                    add_itemt[15] = "山东省物价局|http://www.sdwj.gov.cn";
                    add_itemt[16] = "济南市物价局|http://www.qpn.gov.cn";
                    add_itemt[17] = "河南省价格信息网|http://www.hapin.org.cn";
                    add_itemt[18] = "湖南省物价局|http://www.priceonline.gov.cn";
                    add_itemt[19] = "西安市物价局|http://www.xaprice.com";
                    add_itemt[20] = "新疆价格信息网|http://www.xjpi.gov.cn";
                    add_itemt[21] = "安徽省物价局|http://www.ahpi.gov.cn";
                    add_itemt[22] = "大连价格信息网|http://www.wjj.dl.gov.cn/";
                    add_itemt[23] = "青岛市物价局|http://qpinet.qingdao.gov.cn/";
                    add_itemt[24] = "山西省物价局|http://www.sxprice.gov.cn/";
                    add_itemt[25] = "贵州省物价局 |http://www.gz12358.gov.cn/";
                    add_itemt[26] = "宁夏回族自治区物价局 |http://www.nxcpic.gov.cn";
                    add_itemt[27] = "内蒙古价格信息网|http://www.nmpi.gov.cn/";
                    add_itemt[28] = "洛阳物价信息网|http://www.12358.net.cn/";


                    show_selectt(select_typet, input_Namet, select_Wt, select_Ht, select_bgImgt, add_itemt, form_namet, font_colort, font_sizet);
                                  
                </script>
            </span><span class="yqsel">
                <script language="JavaScript" type="text/javascript">
                    var select_typew = '1' //下拉列表框的开式：0为选择，1为跳转
                    var input_Namew = 'class_selectt' //隐藏域的ＩＤ
                    var select_Ww = 177;  //选择框的宽;
                    var select_Hw = 23;  //选择框的高;
                    var font_sizew = 12;  //框里的字体大小;
                    var form_namew = 'form4'  //表单名称;
                    var font_colorw = '#959595'  //字体颜色;
                    var select_bgImgw = 'images/dsel_b02.gif'
                    var add_itemw = new Array();
                    add_itemw[0] = "北京市各区县发展改革委|0";

                    add_itemw[1] = "东城区发展改革委|http://fzjhw.bjdch.gov.cn/n5687274/n5723019/index.html";
                    add_itemw[2] = "西城区发展改革委|http://www.bjxch.gov.cn/pub/xch_zhuzhan/A/A7/A7_1/A7_1_2/";
                    add_itemw[3] = "朝阳区发展改革委|http://fagaiwei.bjchy.gov.cn/";
                    add_itemw[4] = "海淀区发展改革委|http://www.hddrc.gov.cn/";
                    add_itemw[5] = "丰台区发展改革委|http://fgw.bjft.gov.cn/";
                    add_itemw[6] = "石景山区发展改革委|http://www.sjsfg.gov.cn/";
                    add_itemw[7] = "顺义区发展改革委|http://www.shyjw.gov.cn/";
                    add_itemw[8] = "昌平区发展改革委|http://www.zgsnzx.cn/html/jianjie/2008-3-28/200832814470_4294.shtml";
                    add_itemw[9] = "门头沟区发展改革委|http://www.zgsnzx.cn/html/jianjie/2008-4-4/20084414430_4921.shtml";
                    add_itemw[10] = "通州区发展改革委|http://www.bjtzh.gov.cn/portal/fzgn/tybs/jgzn/fzggw/webinfo/2008/10/1223510624052481.htm";
                    add_itemw[11] = "房山区发展改革委|http://fgw.bjfsh.gov.cn/";
                    add_itemw[12] = "大兴区发展改革委|http://www.dxfgw.gov.cn/web/fgw/";
                    add_itemw[13] = "怀柔区发展改革委|http://www.hrjw.gov.cn/";
                    add_itemw[14] = "平谷区发展改革委|http://www.pgjw.gov.cn/";
                    add_itemw[15] = "延庆县发展改革委|http://www.znxy.org.cn/xzdw/html/2009-1-15/20091151170_2222.shtml";
                    add_itemw[16] = "密云县发展改革委|http://www.myfgw.gov.cn/myfgwdoc/";

                    show_selectw(select_typew, input_Namew, select_Ww, select_Hw, select_bgImgw, add_itemw, form_namew, font_colorw, font_sizew); 
                </script>
            </span>
        </div>
    </div>
    <!--middle end-->
    <div class="clear">
    </div>
    <!--footer begin-->
    <script language='javascript' src='javascript/footer.js'></script>
    <!--footer end-->

    <div class="side-btns-2w" style="display: block;"><a title="北京价格公众号" href="./about/tdc.html" target="_blank" class="side-btns-2w-img"><em style=" font-size:13px; color: #0070c0; font-weight:bold;">北京价格</em><em>公众号</em> <img width="90" src="./images/2da.jpg"> <span>欢迎关注了解详情&gt;</span> </a></div>

</body>
</html>
