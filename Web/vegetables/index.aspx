<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="vegetables_index" %>

<%@ OutputCache CacheProfile="CacheVegetable1hourByDateTime" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>北京市价格监测中心</title>
    <meta name="keywords" content="京价网、价格、价格监测、北京市价格监测中心、北京价格监测、北京价格" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../css/stylesheet.css" type="text/css" />
    <script type="text/javascript" src="../javascript/home.js"></script>
    <script type="text/javascript" src="../javascript/calendar.js" charset="gb2312"></script>
</head>
<body style="background-color: transparent;">
    <!--header begin-->
    <form id="form1" runat="server">
    <script type="text/javascript" language='javascript' src='../javascript/zheader.js'></script>
    </form>
    <!--header end-->
    <div class="clear">
    </div>
    <!--middle begin-->
    <div id="middle">
        <!--左侧标题 begin-->
        <div class="wjrc_top">
        </div>
        <!--左侧标题 end-->
        <div class="wei_ri">
            <a href="../index.aspx">首页</a> > <span class="we_te"><a href="index.aspx">一周菜价</a></span>
        </div>
        <div class="clear">
        </div>
        <div class="zbody_b">
            <!--左侧 begin-->
            <div class="zb_le">
                <span class="men_le"></span>
                <div class="for_all">
                    <span class="for01">蔬菜 </span><span class="for02">
                        <input id="textbox1" size="7" onfocus="if(this.value=='MM/DD/YYYY')this.value=''"
                            onblur="if(this.value=='')this.value='MM/DD/YYYY'" class="sop01" onclick="displayCalendar(event,'textbox1')"
                            value="<%=strDate %>" />
                        <input type="button" class="button_calendar" onclick="displayCalendar(event,'textbox1')" />
                    </span>
                    <input type="button" value="" class="for03" onclick="javascript:QueryDatePrice();" />
                    <script type="text/javascript">
                        function QueryDatePrice() {
                            var vDate = document.getElementById("textbox1").value;

                            if (vDate == "") {
                                return; // 空字符串
                            }
                            var s = vDate.split('-');

                            var y = "0" + s[0];
                            var m = "0" + s[1];
                            var d = "0" + s[2];
                            y = y.substring(y.length - 4);
                            m = m.substring(m.length - 2);
                            d = d.substring(d.length - 2);

                            var t = y + m + d;

                            location.href = "./index_" + t + ".aspx";
                        }
					
                    </script>
                </div>
            </div>
            <!--左侧 end-->
            <!--右侧 begin-->
            <div class="zb_ri">
                <div class="wgza">
                    <div class="vge_ti">
                        <span class="vg01">品类 </span><span class="vg02">批发成交量<br />
                            万公斤 </span><span class="vg03">批发价格<br />
                                元/500克 </span><span class="vg04">农贸零售价格<br />
                                    元/500克 </span><span class="vg05">超市零售价格<br />
                                        元/500克 </span><span class="vg06">日期 </span>
                    </div>
                    <div class="vge_tc">
                        <ul>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <li><span class="vc01">
                                        <%# Eval("ItemName").ToString()%></span><span class="vc02">
                                            <%# Convert.ToDecimal(Eval("Price01").ToString()).ToString("0.00;0.00;-")%>
                                        </span><span class="vc03">
                                            <%# Convert.ToDecimal(Eval("Price02").ToString()).ToString("0.00;0.00;-")%>
                                        </span><span class="vc04">
                                            <%# Convert.ToDecimal(Eval("Price03").ToString()).ToString("0.00;0.00;-")%>
                                        </span><span class="vc05">
                                            <%# Convert.ToDecimal(Eval("Price04").ToString()).ToString("0.00;0.00;-")%>
                                        </span><span class="vc06">
                                            <%# Convert.ToDateTime(Eval("PriceDate").ToString()).ToString("yyyy.MM.dd")%>
                                        </span></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <div class="clear">
                        </div>
                        <div class="zpage">
                            <span class="pcc">
                                <%=GetWeekList() %>
                            </span>
                        </div>
                        <div class="ne_zhu">
                            注：粮油批发统计数据来自7家批发市场，肉禽蛋及蔬菜统计数据来自8家批发市场<br />
                            零售价格数据来自32家农贸市场和17家超市的平均零售价格
                        </div>
                    </div>
                </div>
            </div>
            <!--右侧 end-->
        </div>
    </div>
    <!--middle end-->
    <div class="clear">
    </div>
    <!--footer begin-->
    <script language='javascript' src='../javascript/zfooter.js'></script>
    <!--footer end-->
</body>
</html>
