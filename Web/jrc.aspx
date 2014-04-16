<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jrc.aspx.cs" Inherits="jrc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/stylesheet.css" type="text/css" />
    <script type="text/javascript" src='javascript/hua.js'></script>
    <script type="text/javascript" src="jQuery/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="js/highcharts.js"></script>
    <script type="text/javascript" src="js/themes/vpi.js"></script>
    <script type="text/javascript">
		
		var nYear = (new Date()).getFullYear();

        var chartVPI;
		
        jQuery(document).ready(function ()
        {
            SetTable(1);

            chartVPI = new Highcharts.Chart({
                chart: {
                    renderTo: 'containerVPI',
                    marginRight: 0,
                    marginLeft: 35,
                    marginBottom: 30 
                },
                title: {
                    text: ''
                },
                xAxis: {
					categories: [<%=strXAxisVPI %>],
					tickInterval: 10
                },
                yAxis: {
                    title: {
                        text: ''
                    }
                },
                plotOptions: {
                    series: {
                        marker: {
                            enabled: false,
                            states: {
                                hover: {
                                    enabled: true
                                }
                            }
                        }
                    }
                },
                tooltip: {
					formatter: function() {
						var n = this.series.name;
						var d = this.x.split('-');

					
						return '<b>'+n+'： </b><br/>'+ GetCurrDate(d[0], d[1]) +'：<b>￥'+this.y+'</b>';
					}
				},
				legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -228,
                    y: 5,
                    borderWidth: 1,
                    backgroundColor: 'white'
                },
                credits: {
                    enabled: false
                },
                series: [{
                    name: '批发价格(元/500克)',
                    data: [<%=strPrice02 %>]
                }, {
                    name: '农贸零售价格(元/500g)',
                    data: [<%=strPrice03 %>]
                }, {
                    name: '超市零售价格(元/500g)',
                    data: [<%=strPrice04 %>]
                }]
            });
        });

		function GetCurrDate(m,d){

			var t = new Date();
			t.setFullYear(nYear, parseInt(m)-1, parseInt(d));
			t.setHours(0,0,0,0);

			var now = new Date();
			now.setHours(0,0,0,0);

			if( (t.getTime() - now.getTime())>0 ){
				return (nYear-1)+'年'+m+'月'+d+'日';
			}
			else{
				return (nYear)+'年'+m+'月'+d+'日';
			}

		}
    
    </script>
</head>
<body style="margin: 0; padding: 0">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 500px;" border="0">
        <tr id="t" class="jrc_top">
            <td onmouseover="SetTable(1);" id="jrc_1" class="jrc_top_1_1" width="172px">&nbsp;
                <strong>每日菜价</strong>(<asp:Label ID="txtVegetablePriceDate" runat="server" Text=""></asp:Label>)
            </td>
            <td onmouseover="SetTable(2);" id="jrc_2" class="jrc_top_2_2" width="100px">
                <span>百日菜价走势</span>
            </td>
            <td class="jrc_top_3" width="228px">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div id="tab01">
                    <span class="jrt_mt">
                        <table width="470" cellspacing="0" cellpadding="0" border="0" style="padding-left: 20px"
                            class="tabs">
                            <tbody>
                                <tr valign="top" align="center">
                                    <td width="80" align="left">
                                        品类
                                    </td>
                                    <td width="95" style="padding-left: 15px;">
                                        批发成交量<br>
                                        万公斤
                                    </td>
                                    <td width="90" style="padding-left: 15px;">
                                        批发价格<br>
                                        元/500克
                                    </td>
                                    <td width="100" style="padding-left: 15px;">
                                        农贸零售价格<br>
                                        元/500克
                                    </td>
                                    <td width="100" style="padding-left: 15px;">
                                        超市零售价格<br>
                                        元/500克
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </span><span class="jrt_mb"><span class="jr_tt">
                        <div id="demo0" style="overflow: hidden; float: left; width: 468px; height: 290px;">
                            <table cellspacing="0" cellpadding="0" align="center" border="0" cellspace="0">
                                <tbody>
                                    <tr>
                                        <td id="demo10" valign="top">
                                            <div class="index_right_t3">
                                                <ul>
                                                    <asp:Repeater ID="rpVegetablePrice" runat="server">
                                                        <ItemTemplate>
                                                            <li>
                                                                <table width="473" cellpadding="0" cellspacing="0" border="0" class="tabw" style="padding-left: 20px;">
                                                                    <tr valign="top" align="center" class="utr">
                                                                        <td width="80" align="left" class="yh">
                                                                            <%# Eval("ItemName").ToString()%>
                                                                        </td>
                                                                        <td width="95" style="padding-left: 15px;">
                                                                            <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price01").ToString())) %>
                                                                        </td>
                                                                        <td width="90" style="padding-left: 15px;">
                                                                            <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price02").ToString())) %>
                                                                        </td>
                                                                        <td width="100" style="padding-left: 15px;">
                                                                            <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price03").ToString())) %>
                                                                        </td>
                                                                        <td width="100" style="padding-left: 15px;">
                                                                            <%# String.Format("{0:0.00}", Convert.ToDecimal( Eval("Price04").ToString())) %>
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
                            }
                            var MyMar0 = setInterval(Marquee0, speed30)
                            demo0.onmouseover = function () { clearInterval(MyMar0) }
                            demo0.onmouseout = function () { MyMar0 = setInterval(Marquee0, speed30) }
                        </script>
                        <!--向上滚动结束-->
                    </span>
                        <div class="clear">
                        </div>
                        <span class="hr_gd"><a href="vegetables/index.aspx" target="_blank">
                            <img src="images/jr_gd.gif" /></a> </span></span>
                </div>
                <div id="tab02" style="display: none;">
                    <div id="hotnews1">
                        <div id="hotnews_content1">
                            <div id="containerVPI" style="width: 498px; height: 384px; margin: 0 auto; border: 1 solid #CCCCCC;">
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
