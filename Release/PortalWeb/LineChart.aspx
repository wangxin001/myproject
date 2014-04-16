<%@ page language="C#" autoeventwireup="true" inherits="LineChart, PageCode" enableviewstate="false" %>

<%@ OutputCache CacheProfile="Cache1hour" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>北京市价格监测中心</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="jQuery/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="js/highcharts.js"></script>
    <script type="text/javascript" src="js/themes/vpi.js"></script>
    <script type="text/javascript">
		
        var chartCaiJia;
		var chartJingMi;
        var chartFuQiangFen;
        var chartTiaoHeYou;
        var chartZhuRou;
        var chartJiDan;


        function getY(d){

            var y = (new Date()).getFullYear();

            if( new Date( y, parseInt(d[0])-1, parseInt(d[1]) ).getTime() > (new Date()).getTime() ){
                y = y-1;
            }

            return y;
        }


        jQuery(document).ready(function ()
        {
            // 菜价
			chartCaiJia = new Highcharts.Chart({
                chart: {
                    renderTo: 'containerCaiJia',
                    marginRight: 15,
                    marginLeft: 25,
                    marginBottom: 30
                },
                title: {
                    text: ''
                },
                xAxis: {
					categories : [<%=strXAxisCaiJia %>],
					tickInterval: 3,
					showLastLabel : true
                },
                yAxis: {
                    title: {
                        text: ''
                    },
					min : <%= strMinCaiJia %>,
					max : <%= strMaxCaiJia %>
                },
                tooltip: {
					formatter: function() {
						var n = this.series.name;
						var d = this.x.split('-');
						var y = getY(d);

						return '<b>'+n+'：</b><br/>'+y+'年'+d[0]+'月'+d[1]+'日：<b>'+this.y+'</b>';
					}
				},
				legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -207,
                    y: 5,
                    borderWidth: 1,
                    backgroundColor: 'white'
                },
                credits: {
                    enabled: false
                },
                series: [{
					name: '批发价格(元/500g)',
                    data: [<%=strPriceCaiJia2 %>]
                },	{
					name: '农贸零售价格(元/500g)',
                    data: [<%=strPriceCaiJia3 %>]
                },	{
					name: '超市零售价格(元/500g)',
                    data: [<%=strPriceCaiJia4 %>]
                }]
            });


            // 富强粉
			chartFuQiangFen = new Highcharts.Chart({
                chart: {
                    renderTo: 'containerFuQiangFen',
                    marginRight: 15,
                    marginLeft: 25,
                    marginBottom: 30
                },
                title: {
                    text: ''
                },
                xAxis: {
					categories : [<%=strXAxisFuQiangFen %>],
					tickInterval: 3,
					showLastLabel : true
                },
                yAxis: {
                    title: {
                        text: ''
                    },
					min : <%= strMinFuQiangFen %>,
					max : <%= strMaxFuQiangFen %>,
                    tickInterval: 0.5
                },
                tooltip: {
					formatter: function() {
                        
						var n = this.series.name;
						var d = this.x.split('-');

                        var y = getY(d);

						return '<b>'+n+'：</b><br/>'+y+'年'+d[0]+'月'+d[1]+'日：<b>'+this.y+'</b>';
					}
				},
				legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -207,
                    y: 5,
                    borderWidth: 1,
                    backgroundColor: 'white'
                },
                credits: {
                    enabled: false
                },
                series: [{
					name: '批发价格(元/500g)',
                    data: [<%=strPriceFuQiangFen2 %>]
                },	{
					name: '农贸零售价格(元/500g)',
                    data: [<%=strPriceFuQiangFen3 %>]
                },	{
					name: '超市零售价格(元/500g)',
                    data: [<%=strPriceFuQiangFen4 %>]
                }]
            });


            // 粳米
			chartJingMi = new Highcharts.Chart({
                chart: {
                    renderTo: 'containerJingMi',
                    marginRight: 15,
                    marginLeft: 25,
                    marginBottom: 30
                },
                title: {
                    text: ''
                },
                xAxis: {
					categories : [<%=strXAxisJingMi %>],
					tickInterval: 3,
					showLastLabel : true
                },
                yAxis: {
                    title: {
                        text: ''
                    },
					min : <%= strMinJingMi %>,
					max : <%= strMaxJingMi %>,
                    tickInterval: 0.5
                },
                tooltip: {
					formatter: function() {
						var n = this.series.name;
						var d = this.x.split('-');
						
                        var y = getY(d);

						return '<b>'+n+'：</b><br/>'+y+'年'+d[0]+'月'+d[1]+'日：<b>'+this.y+'</b>';
					}
				},
				legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -207,
                    y: 5,
                    borderWidth: 1,
                    backgroundColor: 'white'
                },
                credits: {
                    enabled: false
                },
                series: [{
					name: '批发价格(元/500g)',
                    data: [<%=strPriceJingMi2 %>]
                },	{
					name: '农贸零售价格(元/500g)',
                    data: [<%=strPriceJingMi3 %>]
                },	{
					name: '超市零售价格(元/500g)',
                    data: [<%=strPriceJingMi4 %>]
                }]
            });


            // 调和油
			chartTiaoHeYou = new Highcharts.Chart({
                chart: {
                    renderTo: 'containerTiaoHeYou',
                    marginRight: 15,
                    marginLeft: 25,
                    marginBottom: 30
                },
                title: {
                    text: ''
                },
                xAxis: {
					categories : [<%=strXAxisTiaoHeYou %>],
					tickInterval: 3,
					showLastLabel : true
                },
                yAxis: {
                    title: {
                        text: ''
                    },
					min : <%= strMinTiaoHeYou %>,
					max : <%= strMaxTiaoHeYou %>
                },
                tooltip: {
					formatter: function() {
						var n = this.series.name;
						var d = this.x.split('-');
						
                        var y = getY(d);

						return '<b>'+n+'：</b><br/>'+y+'年'+d[0]+'月'+d[1]+'日：<b>'+this.y+'</b>';
					}
				},
				legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -219,
                    y: 5,
                    borderWidth: 1,
                    backgroundColor: 'white'
                },
                credits: {
                    enabled: false
                },
                series: [{
					name: '批发价格(元/5L)',
                    data: [<%=strPriceTiaoHeYou2 %>]
                },	{
					name: '农贸零售价格(元/5L)',
                    data: [<%=strPriceTiaoHeYou3 %>]
                },	{
					name: '超市零售价格(元/5L)',
                    data: [<%=strPriceTiaoHeYou4 %>]
                }]
            });


            // 猪肉
			chartZhuRou = new Highcharts.Chart({
                chart: {
                    renderTo: 'containerZhuRou',
                    marginRight: 15,
                    marginLeft: 25,
                    marginBottom: 30
                },
                title: {
                    text: ''
                },
                xAxis: {
					categories : [<%=strXAxisZhuRou %>],
					tickInterval: 3,
					showLastLabel : true
                },
                yAxis: {
                    title: {
                        text: ''
                    },
					min : <%= strMinZhuRou %>,
					max : <%= strMaxZhuRou %>
                },
                tooltip: {
					formatter: function() {
						var n = this.series.name;
						var d = this.x.split('-');
						
                        var y = getY(d);

						return '<b>'+n+'：</b><br/>'+y+'年'+d[0]+'月'+d[1]+'日：<b>'+this.y+'</b>';
					}
				},
				legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -171,
                    y: 5,
                    borderWidth: 1,
                    backgroundColor: 'white'
                },
                credits: {
                    enabled: false
                },
                series: [{
					name: '白条猪批发价格(元/500g)',
                    data: [<%=strPriceZhuRou2 %>]
                },	{
					name: '精瘦肉农贸零售价格(元/500g)',
                    data: [<%=strPriceZhuRou3 %>]
                },	{
					name: '精瘦肉超市零售价格(元/500g)',
                    data: [<%=strPriceZhuRou4 %>]
                }]
            });

            
            // 鸡蛋
			chartJiDan = new Highcharts.Chart({
                chart: {
                    renderTo: 'containerJiDan',
                    marginRight: 15,
                    marginLeft: 25,
                    marginBottom: 30
                },
                title: {
                    text: ''
                },
                xAxis: {
					categories : [<%=strXAxisJiDan %>],
					tickInterval: 3,
					showLastLabel : true
                },
                yAxis: {
                    title: {
                        text: ''
                    },
					min : <%= strMinJiDan %>,
					max : <%= strMaxJiDan %>
                },
                tooltip: {
					formatter: function() {
						var n = this.series.name;
						var d = this.x.split('-');
						
                        var y = getY(d);

						return '<b>'+n+'：</b><br/>'+y+'年'+d[0]+'月'+d[1]+'日：<b>'+this.y+'</b>';
					}
				},
				legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -207,
                    y: 5,
                    borderWidth: 1,
                    backgroundColor: 'white'
                },
                credits: {
                    enabled: false
                },
                series: [{
					name: '批发价格(元/500g)',
                    data: [<%=strPriceJiDan2 %>]
                },	{
					name: '农贸零售价格(元/500g)',
                    data: [<%=strPriceJiDan3 %>]
                },	{
					name: '超市零售价格(元/500g)',
                    data: [<%=strPriceJiDan4 %>]
                }]
            }); 
                

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="brc_all">
        <ul id="level">
            <li id="tab0" class="white"><a href="javascript:void(0)"><strong>菜价走势</strong></a></li>
            <li id="tab1" class="black"><a href="javascript:void(0)"><strong>富强粉走势</strong></a></li>
            <li id="tab2" class="black"><a href="javascript:void(0)"><strong>粳米走势</strong></a></li>
            <li id="tab3" class="black"><a href="javascript:void(0)"><strong>调和油走势</strong></a></li>
            <li id="tab4" class="black"><a href="javascript:void(0)"><strong>猪肉走势</strong></a></li>
            <li id="tab5" class="black"><a href="javascript:void(0)"><strong>鸡蛋走势</strong></a></li>
        </ul>
        <div id="level0" style="display: block;">
            <div id="containerCaiJia" style="width: 442px; height: 384px; margin: 0 auto">
            </div>
        </div>
        <div id="level1" style="display: none;">
            <div id="containerFuQiangFen" style="width: 442px; height: 384px; margin: 0 auto">
            </div>
        </div>
        <div id="level2" style="display: none;">
            <div id="containerJingMi" style="width: 442px; height: 384px; margin: 0 auto">
            </div>
        </div>
        <div id="level3" style="display: none;">
            <div id="containerTiaoHeYou" style="width: 442px; height: 384px; margin: 0 auto">
            </div>
        </div>
        <div id="level4" style="display: none;">
            <div id="containerZhuRou" style="width: 442px; height: 384px; margin: 0 auto">
            </div>
        </div>
        <div id="level5" style="display: none;">
            <div id="containerJiDan" style="width: 442px; height: 384px; margin: 0 auto">
            </div>
        </div>
    </div>
    <script type="text/javascript" src='javascript/changes.js'></script>
    </form>
</body>
</html>
