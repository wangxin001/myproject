
	var inputDateObj;//全局变量，需要输入的文本框



//创建日历DOM--------------------------------------------------------------------------------------------------------------
	function createCalendar(){
		//结构
		closeCalendar();
		var calendar=document.body.appendChild(document.createElement("div"));		
		var title=calendar.appendChild(document.createElement("div"));
		    var close=title.appendChild(document.createElement("div"));
		    var titleText=title.appendChild(document.createElement("div"));
		var head=calendar.appendChild(document.createElement("div"));
		    var lastYear=head.appendChild(document.createElement("div"));
		    var lastMonth=head.appendChild(document.createElement("div"));
		    var thisYearMonth=head.appendChild(document.createElement("div"));
		    var nextMonth=head.appendChild(document.createElement("div"));
		    var nextYear=head.appendChild(document.createElement("div"));
		var week=calendar.appendChild(document.createElement("div"));
		var body=calendar.appendChild(document.createElement("div"));
		//动作
		    lastMonth.onclick=function(){changeMonth(-1);};
		    nextMonth.onclick=function(){changeMonth(1);};
		    lastYear.onclick=function(){changeYear(-1);};
		    nextYear.onclick=function(){changeYear(1);};
		    close.onclick=function(){closeCalendar();};
		//属性
		calendar.setAttribute("id","calendar");
		//样式
		calendar.className="calendar";
		title.className="calendarTitle";
		close.className="calendarClose";
		head.className="calendarHead"; 
		lastMonth.className="calendarLastMonth";
		nextMonth.className="calendarNextMonth";
		lastYear.className="calendarLastYear"; 
		nextYear.className="calendarNextYear";
		thisYearMonth.className="calendarYearMonth";
		//内容
		titleText.innerHTML="<span onclick='closeCalendar()' style='cursou:hand;cursor:pointer;'>关闭</span>";
		
		//星期
		var aryWeek=new Array("日","一","二","三","四","五","六");
		for(i=0;i<7;i++){
				var weekday=week.appendChild(document.createElement("div"));
				    weekday.className="calendarWeekday";
				    if(i==0||i==6){weekday.className="calendarWeekend";}
				    weekday.innerHTML=aryWeek[i];
			}

		//计算日历
		var now=new Date();
		showCalendar(now.getFullYear(),now.getMonth());
	}







//根据年份、月份计算日历-------------------------------------------------------------------------------------------------------
	function showCalendar(y,m){
		var calendar=document.getElementById("calendar");
		var head=calendar.childNodes[1].childNodes[2];
		    head.innerHTML=y+"年"+(m+1)+"月";
		calendar.setAttribute("year",y);
		calendar.setAttribute("month",m);
		calendar=calendar.childNodes[3];
		calendar.innerHTML="";
		var firstDayOfMonth=new Date(y,m,1);
		var firstWeekDay=firstDayOfMonth.getDay();;
		var cMonth=firstDayOfMonth.getMonth();
		var WeekNo=0;
		var MonthDay=1;
		var Today=new Date();

		
		while(MonthDay>=0){
			var cDate=new Date(y,m,MonthDay);
			if(cMonth==cDate.getMonth()){
				var cWeekDiv=calendar.appendChild(document.createElement("div"));
				cWeekDiv.style.clear="both";
				for(i=0;i<7;i++){
					var cWeekdayDiv=cWeekDiv.appendChild(document.createElement("div"));
					    cWeekdayDiv.className="calendarDay";
					    if(i==0||i==6){cWeekdayDiv.className="calendarWeekendDay";}
					if(WeekNo==0){
						if(i>=firstWeekDay)MonthDay++;
					}
					else{					
						MonthDay++;
					}

					if(MonthDay>0){
						cDate=new Date(y,m,MonthDay-1);
						if(cMonth==cDate.getMonth()){
							cWeekdayDiv.innerHTML=cDate.getDate();
							cWeekdayDiv.setAttribute("Date",cDate);
							cWeekdayDiv.onclick=function(){showDate(this)};
		   					cWeekdayDiv.onmouseover=function(){overClendar(this);};
		    					cWeekdayDiv.onmouseout=function(){outClendar(this);};
							if(cDate.getFullYear()==Today.getFullYear()&&cDate.getMonth()==Today.getMonth()&&cDate.getDate()==Today.getDate())cWeekdayDiv.className="calendarToday";
						}
					}
				}
			WeekNo++;
			}
			else{break;}
		}
	}






//改变月份年份------------------------------------------------------------------------------------------------
	function changeMonth(d){
		var calendar=document.getElementById("calendar");
		var year=calendar.getAttribute("year");
		var month=calendar.getAttribute("month");
		month=Number(month)+Number(d);
		if(month<0){month=11;year=Number(year)-1;}
		if(month>11){month=0;year=Number(year)+1;}
		showCalendar(year,month);
	}
	function changeYear(d){
		var calendar=document.getElementById("calendar");
		var year=calendar.getAttribute("year");
		var month=calendar.getAttribute("month");
		year=Number(year)+d;
		showCalendar(year,Number(month));
	}







//鼠标动作------------------------------------------------------------------------------------------------
	function overClendar(obj){
		obj.setAttribute("oldClass",obj.className);
		obj.className="calendarOver";	
	}
	function outClendar(obj){
		obj.className=obj.getAttribute("oldClass");	
	}






//返回日期值------------------------------------------------------------------------------------------------
	function showDate(obj){	
		var d=new Date(obj.getAttribute("Date"));
		var txt=d.getFullYear()+"-"+(d.getMonth()+1)+"-"+d.getDate();
		var calendar=document.getElementById("calendar");
		closeCalendar();
		inputDateObj.value=txt;
		inputDateObj=null;
	}








//显示隐藏日历-----------------------------------------------------------------------------------------------	
	function displayCalendar(ev,objName){
		inputDateObj=document.getElementById(objName);
		createCalendar();
		ev = ev || window.event;   
    		var target = ev.target || ev.srcElement; 
		var calendar=document.getElementById("calendar");
		var x=ev.clientX||ev.pageX;
		var y=ev.clientY||ev.pageY;
		calendar.style.left=x+"px";
		calendar.style.top=y+"px";
	}
	function closeCalendar(){
		var calendar=document.getElementById("calendar");
		if(calendar)document.body.removeChild(calendar);
	}