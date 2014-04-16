<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scroll.aspx.cs" Inherits="Scroll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <!--今日菜价 begin-->
    <div class="jrc_all">
        <div class="jrc_top">
            <ul id="layer">
                <li id="price0" class="white"><a href="javascript:void(0)"><strong>每日菜价</strong></a></li>
                <li id="price1" class="black"><a href="javascript:void(0)"><strong>每日粮油价格</strong></a></li>
                <li id="price2" class="black"><a href="javascript:void(0)"><strong>每日肉蛋水产价格</strong></a></li>
            </ul>
        </div>
        <div id="layer0" style="display: block;">
            <span class="jrt_mt">
                <table width="488" cellpadding="0" cellspacing="0" border="0" class="tabs">
                    <tr align="center">
                        <td width="69" align="center">
                            品类
                        </td>
                        <td width="76">
                            批发成交量<br />
                            万公斤
                        </td>
                        <td width="76">
                            批发价格<br />
                            元/500克
                        </td>
                        <td width="91">
                            农贸零售价格<br />
                            元/500克
                        </td>
                        <td width="91">
                            超市零售价格<br />
                            元/500克
                        </td>
                        <td width="75">
                            日期
                        </td>
                    </tr>
                </table>
            </span><span class="jrt_mb"><span class="jr_tt">
                <div id="demo0" style="overflow: hidden; float: left; width: 476px; height: 290px;
                    padding-left: 12px;">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td id="demo10">
                                    <div class="index_right_t3">
                                        <ul>
                                            <asp:Repeater ID="rpVegetablePrice" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <table width="476" cellpadding="0" cellspacing="0" border="0" class="tabw">
                                                            <tr class="utr">
                                                                <td width="69" class="yh">
                                                                    <%# Eval("ItemName").ToString()%>
                                                                </td>
                                                                <td width="76">
                                                                    <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price01").ToString())) %>
                                                                </td>
                                                                <td width="76">
                                                                    <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price02").ToString())) %>
                                                                </td>
                                                                <td width="91">
                                                                    <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price03").ToString())) %>
                                                                </td>
                                                                <td width="91">
                                                                    <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price04").ToString())) %>
                                                                </td>
                                                                <td width="75">
                                                                    <%# Convert.ToDateTime(Eval("PriceDate").ToString()).ToString("yyyy.MM.dd")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td id="demo20" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <script type="text/javascript">
                    var speed30 = 35;
                    var step0 = 1;
                    var direction0 = 'up';
                    var demo20 = document.getElementById('demo20');
                    var demo10 = document.getElementById('demo10');
                    var demo0 = document.getElementById('demo0');

                    demo20.innerHTML = demo10.innerHTML;

                    function Marquee0() {
                        if (direction0 == 'up') {
                            if (demo20.offsetHeight - demo0.scrollTop <= 0)
                                demo0.scrollTop -= demo10.offsetHeight;
                            else
                                demo0.scrollTop = demo0.scrollTop + step0;
                        }
                        if (direction0 == 'down') {
                            if (demo0.scrollTop <= 0)
                                demo0.scrollTop += demo20.offsetHeight
                            else
                                demo0.scrollTop = demo0.scrollTop - step0;
                        }

                        //alert("a"); 
                    }
                    var MyMar0 = setInterval(Marquee0, speed30)
                    demo0.onmouseover = function () { clearInterval(MyMar0) }
                    demo0.onmouseout = function () { MyMar0 = setInterval(Marquee0, speed30) }
                </script>
                <!--向上滚动结束-->
            </span>
                <div class="clear">
                </div>
                <span class="hr_gd"><a href="./vegetables/Price.aspx" target="_blank">
                    <img src="images/jr_gd.gif" /></a> </span></span>
        </div>
        <div id="layer1" style="display: none;">
            <span class="jrt_mt">
                <table width="488" cellpadding="0" cellspacing="0" border="0" class="tabs">
                    <tr align="center" height="36">
                        <td width="69">
                            品类
                        </td>
                        <td width="76">
                            规格等级
                        </td>
                        <td width="76">
                            批发价格
                        </td>
                        <td width="91">
                            农贸零售价格
                        </td>
                        <td width="91">
                            超市零售价格
                        </td>
                        <td width="75">
                            日期
                        </td>
                    </tr>
                </table>
            </span><span class="jrt_mb2"><span class="jr_tt">
                <div id="demo30" style="overflow: hidden; float: left; width: 476px; height: 290px;
                    padding-left: 12px;">
                    <table cellspacing="0" cellpadding="0" align="center" border="0">
                        <tr>
                            <td id="demo40">
                                <div class="index_right_t3">
                                    <ul>
                                        <asp:Repeater ID="rpLiangYouPrice" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <table width="476" cellpadding="0" cellspacing="0" border="0" class="tabw">
                                                        <tr class="utr">
                                                            <td width="69" class="yh">
                                                                <%# Eval("ItemName").ToString()%>
                                                            </td>
                                                            <td width="76">
                                                                <%# Eval("ItemLevel").ToString()%>
                                                            </td>
                                                            <td width="76">
                                                                <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price02").ToString())) %>
                                                            </td>
                                                            <td width="91">
                                                                <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price03").ToString())) %>
                                                            </td>
                                                            <td width="91">
                                                                <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price04").ToString())) %>
                                                            </td>
                                                            <td width="75">
                                                                    <%# Convert.ToDateTime(Eval("PriceDate").ToString()).ToString("yyyy.MM.dd")%>
                                                                </td>
                                                        </tr>
                                                    </table>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td id="demo50">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <script type="text/javascript">
                    var speed30 = 35;
                    var step0 = 1;
                    var direction0 = 'up';
                    var demo50 = document.getElementById('demo50');
                    var demo40 = document.getElementById('demo40');
                    var demo30 = document.getElementById('demo30');

                    demo50.innerHTML = demo40.innerHTML;

                    function Marquee0() {
                        if (direction0 == 'up') {
                            if (demo50.offsetHeight - demo30.scrollTop <= 0)
                                demo30.scrollTop -= demo40.offsetHeight;
                            else
                                demo30.scrollTop = demo30.scrollTop + step0;
                        }
                        if (direction0 == 'down') {
                            if (demo30.scrollTop <= 0)
                                demo30.scrollTop += demo50.offsetHeight
                            else
                                demo30.scrollTop = demo30.scrollTop - step0;
                        }
                    }
                    var MyMar0 = setInterval(Marquee0, speed30)
                    demo30.onmouseover = function () { clearInterval(MyMar0) }
                    demo30.onmouseout = function () { MyMar0 = setInterval(Marquee0, speed30) }
                </script>
            </span>
                <div class="clear">
                </div>
                <span class="hr_gd"><a href="./vegetables/Price.aspx" target="_blank">
                    <img src="images/jr_gd.gif" /></a> </span></span>
        </div>
        <div id="layer2" style="display: none;">
            <span class="jrt_mt">
                <table width="488" cellpadding="0" cellspacing="0" border="0" class="tabs">
                    <tr align="center">
                        <td width="69">
                            品类
                        </td>
                        <td width="76">
                            规格等级
                        </td>
                        <td width="76">
                            批发价格<br />
                            元/500克
                        </td>
                        <td width="91">
                            农贸零售价格<br />
                            元/500克
                        </td>
                        <td width="91">
                            超市零售价格<br />
                            元/500克
                        </td>
                        <td width="75">
                            日期
                        </td>
                    </tr>
                </table>
            </span><span class="jrt_mb3"><span class="jr_tt">
                <div id="demo60" style="overflow: hidden; float: left; width: 476px; height: 290px;
                    padding-left: 12px;">
                    <table cellspacing="0" cellpadding="0" align="center" border="0" cellspace="0">
                        <tr>
                            <td id="demo70">
                                <div class="index_right_t3">
                                    <ul>
                                        <asp:Repeater ID="rpRouDanPrice" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <table width="476" cellpadding="0" cellspacing="0" border="0" class="tabw">
                                                        <tr class="utr">
                                                            <td width="69" class="yh">
                                                                <%# Eval("ItemName").ToString()%>
                                                            </td>
                                                            <td width="76">
                                                                <%# Eval("ItemLevel").ToString()%>
                                                            </td>
                                                            <td width="76">
                                                                <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price02").ToString())) %>
                                                            </td>
                                                            <td width="91">
                                                                <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price03").ToString())) %>
                                                            </td>
                                                            <td width="91">
                                                                <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price04").ToString())) %>
                                                            </td>
                                                            <td width="75">
                                                                    <%# Convert.ToDateTime(Eval("PriceDate").ToString()).ToString("yyyy.MM.dd")%>
                                                                </td>
                                                        </tr>
                                                    </table>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td id="demo80">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <script type="text/javascript">
                    var speed30 = 35;
                    var step0 = 1;
                    var direction0 = 'up';
                    var demo80 = document.getElementById('demo80');
                    var demo70 = document.getElementById('demo70');
                    var demo60 = document.getElementById('demo60');

                    demo80.innerHTML = demo70.innerHTML;

                    function Marquee0() {
                        if (direction0 == 'up') {
                            if (demo80.offsetHeight - demo60.scrollTop <= 0)
                                demo60.scrollTop -= demo70.offsetHeight;
                            else
                                demo60.scrollTop = demo60.scrollTop + step0;
                        }
                        if (direction0 == 'down') {
                            if (demo60.scrollTop <= 0)
                                demo60.scrollTop += demo80.offsetHeight
                            else
                                demo60.scrollTop = demo60.scrollTop - step0;
                        }
                    }
                    var MyMar0 = setInterval(Marquee0, speed30)
                    demo60.onmouseover = function () { clearInterval(MyMar0) }
                    demo60.onmouseout = function () { MyMar0 = setInterval(Marquee0, speed30) }
                </script>
            </span>
                <div class="clear">
                </div>
                <span class="hr_gd"><a href="./vegetables/Price.aspx" target="_blank">
                    <img src="images/jr_gd.gif" alt="" /></a> </span></span>
        </div>
    </div>
    <!--今日菜价 end-->
    <script type="text/javascript" src='javascript/changes.js'></script>
    </form>
</body>
</html>
