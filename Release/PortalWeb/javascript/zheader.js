﻿document.writeln("<!--header begin-->   ");
document.writeln("<div id=\"header\"> ");
document.writeln("       <div class=\"itop\">");
document.writeln("              <span class=\"sin\">");
document.writeln("                <a href=\"javascript:void(null);\" onclick=\"addFav()\"><img src=\"..\/images\/jr.gif\" \/><\/a>");
document.writeln("              <\/span>");
document.writeln("              <span class=\"sow\">");
document.writeln("                <a href=\"javascript:void(null);\" onclick=\"SetHome(this)\"><img src=\"..\/images\/sy.gif\" \/><\/a>");
document.writeln("              <\/span>");
document.writeln("       <\/div>");
document.writeln("       <div class=\"clear\"><\/div> ");
document.writeln("       <div class=\"iban\">");
document.writeln("            <object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\"  ");
document.writeln("                codebase=\"http:\/\/fpdownload.macromedia.com\/pub\/shockwave\/cabs\/flash\/swflash.cab#version=6,0,0,0\" width=\"951\" height=\"140\">   ");
document.writeln("                <param name=\"movie\" value=\"..\/flash\/logo.swf\" \/>");
document.writeln("                <param name=\"quality\" value=\"high\" \/>");
document.writeln("                <param name=\"wmode\" value=\"transparent\" \/>");
document.writeln("                <embed src=\"..\/flash\/logo.swf\" pluginspage=\"http:\/\/www.macromedia.com\/go\/getflashplayer\" ");
document.writeln("                    quality=\"high\" type=\"application\/x-shockwave-flash\" width=\"951\" height=\"140\"><\/embed> ");
document.writeln("            <\/object> ");
document.writeln("       <\/div>");
document.writeln("       <div class=\"clear\"><\/div>");
document.writeln("       <div class=\"imenu\">");
document.writeln("            <ul>");
document.writeln("                   <li class=\"m03\">");
document.writeln("                       <a href=\"..\/index.aspx\">首页<\/a>");
document.writeln("                   <\/li>");
document.writeln("                   <li class=\"mblank\">");
document.writeln("                   <\/li>  ");
document.writeln("                   <li class=\"m03\">");
document.writeln("                       <a href=\"..\/about\/About.aspx\" target=\"_blank\">机构介绍<\/a>");
document.writeln("                   <\/li>  ");
document.writeln("                   <li class=\"mblank\">");
document.writeln("                   <\/li>     ");
document.writeln("                   <li class=\"m03\">");
document.writeln("                       <a href=\"..\/job\/index.aspx\" target=\"_blank\">价格资讯<\/a>");
document.writeln("                   <\/li>");
document.writeln("                   <li class=\"mblank\">");
document.writeln("                   <\/li>  ");
document.writeln("                   <li class=\"m03\">");
document.writeln("                       <a href=\"..\/news\/index.aspx\" target=\"_blank\">价格预警<\/a>");
document.writeln("                   <\/li>   ");
document.writeln("                   <li class=\"mblank\">");
document.writeln("                   <\/li>  ");
document.writeln("                   <li class=\"m03\">");
document.writeln("                       <a href=\"..\/policy\/index.aspx\" target=\"_blank\">价格政策<\/a>");
document.writeln("                   <\/li>     ");
document.writeln("                   <li class=\"mblank\">");
document.writeln("                   <\/li>  ");
document.writeln("                   <li class=\"m02\">");
document.writeln("                       <a href=\"..\/vegetables\/Price.aspx\" target=\"_blank\">农副产品价格<\/a>");
document.writeln("                   <\/li>     ");
document.writeln("                   <li class=\"mblank\">");
document.writeln("                   <\/li>  ");
document.writeln("                   <li class=\"m03\">");
document.writeln("                       <a href=\"..\/medicine\/index.html\" target=\"_blank\">医药价格<\/a>");
document.writeln("                   <\/li>   ");
document.writeln("                   <li class=\"mblank\">");
document.writeln("                   <\/li>  ");
document.writeln("                   <li class=\"m02\">");
document.writeln("                       <a href=\"..\/price\/price.aspx\" target=\"_blank\">实用价格查询<\/a>");
document.writeln("                   <\/li>    ");
document.writeln("                   <li class=\"mblank\">");
document.writeln("                   <\/li>  ");
document.writeln("                   <li class=\"m03\">");
document.writeln("                       <a href=\"..\/report\/login.aspx\" target=\"_blank\">平价菜店<\/a>");
document.writeln("                   <\/li>    ");
document.writeln("            <\/ul>");
document.writeln("            <div class=\"clear\"><\/div> ");
//document.writeln("                <form action=\"..\/about\/search.html\" method=\"post\">");
document.writeln("                <div class=\"tm2008style\"  id=\"sun\">");
document.writeln("                    <select name=\"language_tm2008\" id=\"language_tm2008\">");
document.writeln("                        <option value=\"0\">全部新闻<\/option>");
document.writeln("                        <option value=\"1\">价格资讯<\/option>");
document.writeln("                        <option value=\"2\" >价格预警<\/option>");
document.writeln("                        <option value=\"3\" >价格政策<\/option>");
document.writeln("                    <\/select>");
document.writeln("                <\/div>");
document.writeln("                <div class=\"ser_all\"> ");
document.writeln("                  <input type=\"text\" name=\"searchKey\" id=\"searchKey\" class=\"sp_in\" onFocus=\"if(this.value=='输入关键字')this.value=''\"  onblur=\"if(this.value=='')this.value='输入关键字'\" value=\"输入关键字\" \/>");
document.writeln("                  <input type=\"button\" value=\"\" class=\"sp_bu\" onclick=\"javscript:return SubmitKeyword();\" \/> ");
document.writeln("                <\/div>");
document.writeln("                <script type=\"text\/javascript\" src=\"..\/javascript\/select2css.js\"><\/script> ");
//document.writeln("                <\/form>");
document.writeln("       <\/div>");
document.writeln("<\/div>");
document.writeln("<!--header end-->")

function SubmitKeyword()
{
	var key = document.getElementById("searchKey").value;
	var c = document.getElementById("language_tm2008").value;
	location.href = "../search/search.aspx?key=" + CtoH(key) + "&c=" + c;
	return false;
}

function CtoH(str)
{
	var result = "";
	for (var i = 0; i < str.length; i++)
	{
		if (str.charCodeAt(i) == 12288)
		{
			result += String.fromCharCode(str.charCodeAt(i) - 12256);
			continue;
		}
		if (str.charCodeAt(i) > 65280 && str.charCodeAt(i) < 65375)
			result += String.fromCharCode(str.charCodeAt(i) - 65248);
		else
			result += String.fromCharCode(str.charCodeAt(i));
	}
	return result;
}