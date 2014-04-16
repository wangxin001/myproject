<%@ page language="C#" autoeventwireup="true" inherits="Vegetables, PageCode" enableviewstate="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>北京市价格监测中心</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src='javascript/hua.js'></script>
    <script type="text/javascript" src="jQuery/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="js/highcharts.js"></script>
    <script type="text/javascript" src="js/themes/vpi.js"></script>
    <script type="text/javascript">
		
		var nYear = (new Date()).getFullYear();

        var chartVPI;
		var chartCPI;
        jQuery(document).ready(function ()
        {
            chartVPI = new Highcharts.Chart({
                chart: {
                    renderTo: 'containerVPI',
                    marginRight: 0,
                    marginLeft: 25,
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
                    name: '批发价格(元/公斤)',
                    data: [<%=strPrice02 %>]
                }, {
                    name: '农贸零售价格(元/500g)',
                    data: [<%=strPrice03 %>]
                }, {
                    name: '超市零售价格(元/500g)',
                    data: [<%=strPrice04 %>]
                }]
            });

			chartCPI = new Highcharts.Chart({
                chart: {
                    renderTo: 'containerCPI',
                    marginRight: 0,
                    marginLeft: 25,
                    marginBottom: 30
                },
                title: {
                    text: ''
                },
                xAxis: {
					categories : [<%=strXAxisCPI %>],
					tickInterval: 3,
					showLastLabel : true
                },
                yAxis: {
                    title: {
                        text: ''
                    },
					min : 95,
					max : 115
                },
                tooltip: {
					formatter: function() {
						var n = this.series.name;
						var d = this.x.split('-');
						return '<b>'+n+'：</b><br/>20'+d[0]+'年'+d[1]+'月：<b>'+this.y+'</b>';
					}
				},
				legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -218,
                    y: 5,
                    borderWidth: 1,
                    backgroundColor: 'white'
                },
                credits: {
                    enabled: false
                },
                series: [{
					name: '北京市居民消费价格指数',
                    data: [<%=strPriceCPI2 %>]
                },	{
					name: '全国居民消费价格指数',
                    data: [<%=strPriceCPI3 %>]
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
<body>
    <form id="form1" runat="server">
    <div id="hotnews">
        <div id="hotnews_caption">
            <ul>
                <li class="cur2_on" id="bm01" onmouseover="SetVisiable(1);">
                    <strong>原油</strong></li>
                <li class="cur2_off" id="bm02" onmouseover="SetVisiable(2);">
                    <strong>CPI</strong></li>
                <li class="cur2_off" id="bm03" onmouseover="SetVisiable(3);">
                    <strong>大豆</strong></li>
                <li class="cur2_off" id="bm04" onmouseover="SetVisiable(4);">
                    <strong>玉米</strong></li>
                <li class="cur2_off" id="bm05" onmouseover="SetVisiable(5);">
                    <strong>小麦</strong></li>
            </ul>
        </div>
        <div id="hotnews_content">
            <div id="list1">
                <div id="containerVPI" style="width: 442px; height: 384px; margin: 0 auto">
                </div>
            </div>
            <div id="list2" style="display: none;">
                <div id="containerCPI" style="width: 442px; height: 384px; margin: 0 auto">
                </div>
            </div>
            <div id="list3" style="display: none;">
                <div id="containerDaDou" style="width: 442px; height: 384px; margin: 0 auto">
                </div>
            </div>
            <div id="list4" style="display: none;">
                <div id="containerYuMi" style="width: 442px; height: 384px; margin: 0 auto">
                </div>
            </div>
            <div id="list5" style="display: none;">
                <div id="containerXiaoMai" style="width: 442px; height: 384px; margin: 0 auto">
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
