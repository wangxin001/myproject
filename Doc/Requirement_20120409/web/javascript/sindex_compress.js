
function changeInfop(id,className,co)
{for(i=1;i<=co;i++)
{item_namep=className+"_"+i;menu_id=className+i;if(id==i)
{document.getElementById(item_namep).style.display='block';document.getElementById(menu_id).className=className+'_yes';}
else
{document.getElementById(item_namep).style.display='none';document.getElementById(menu_id).className=className+'_no';}}}
function MM_openBrWindowp(theURL,winName,features){window.open(theURL,winName,features);}
function MM_preloadimagesp(){var d=document;if(d.images){if(!d.MM_p)d.MM_p=new Array();var i,j=d.MM_p.length,a=MM_preloadimagesp.arguments;for(i=0;i<a.length;i++)
if(a[i].indexOf("#")!=0){d.MM_p[j]=new Image;d.MM_p[j++].src=a[i];}}}
function MM_swapImgRestorep(){var i,x,a=document.MM_sr;for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++)x.src=x.oSrc;}
function MM_findObjp(n,d){var p,i,x;if(!d)d=document;if((p=n.indexOf("?"))>0&&parent.frames.length){d=parent.frames[n.substring(p+1)].document;n=n.substring(0,p);}
if(!(x=d[n])&&d.all)x=d.all[n];for(i=0;!x&&i<d.forms.length;i++)x=d.forms[i][n];for(i=0;!x&&d.layers&&i<d.layers.length;i++)x=MM_findObjp(n,d.layers[i].document);if(!x&&d.getElementById)x=d.getElementById(n);return x;}
function MM_swapImagep(){var i,j=0,x,a=MM_swapImagep.arguments;document.MM_sr=new Array;for(i=0;i<(a.length-2);i+=3)
if((x=MM_findObjp(a[i]))!=null){document.MM_sr[j++]=x;if(!x.oSrc)x.oSrc=x.src;x.src=a[i+2];}}
function show(item_namep)
{showhide=document.getElementById(item_namep).style.display;if(showhide=='block'){document.getElementById(item_namep).style.display='none';}else{document.getElementById(item_namep).style.display='block';}}
function hidden(item_namep)
{document.getElementById(item_namep).style.display='none';}
function show_div(item_namep)
{document.getElementById(item_namep).style.display='block';}
function show_selectp(select_typep,input_Namep,select_Wp,select_Hp,select_bgImgp,add_itemp,form_namep,font_colorp,font_sizep){if(select_typep==0)
{div_select_id="pro_classp";}
else
{div_select_id="site_gop";}
document.write("<style>")
document.write(".select_kuang{width:"+(select_Wp-4)+"px;padding-left:0;border:1px solid #cccccc;background:#ffffff;overflow:scroll;")
document.write("overflow-x:hidden;padding-top:8px; position:relative;top:"+select_Hp+"px;;z-index:100;height:150px;left:-1px; top:-160px;")
document.write("SCROLLBAR-FACE-COLOR:#CCCCCC ;SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; ")
document.write("SCROLLBAR-3DLIGHT-COLOR: #ffffff;SCROLLBAR-ARROW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #ffffff;")
document.write("SCROLLBAR-DARKSHADOW-COLOR: #ffffff;SCROLLBAR-BASE-COLOR: #ffffff}")
document.write(".select_kuang ul{margin:0px;padding:0px;}")
document.write(".select_kuang li{width:"+(select_Wp-1)+"px;font-size:"+font_sizep+"px;float:left;clear:both;padding:2px 5px 1px 5px;}")
document.write(".select_value{width:10px;display:none;}")
document.write("</style>")
document.write("<div style=\"width:"+select_Wp+"px;height:"+select_Hp+"px;background:url("+select_bgImgp+") repeat-y right;line-height:19px;padding-left:0;text-align:left;color:"+font_colorp+";cursor:default;\" onclick=\"show('"+div_select_id+"')\">");document.write("<div style=\"position:relative;left:2px;\"><div style=\"position:absolute;top:0px\">")
document.write("<div class=\"select_kuang\" id=\""+div_select_id+"\" style=\"display:none;cursor:default;\" onmousemove=\"show_div('"+div_select_id+"')\" onmouseout=\"hidden('"+div_select_id+"')\">")
document.write("<ul>");for(i=0;i<add_itemp.length;i++)
{select_option=add_itemp[i].split("|",2)[0];select_value=add_itemp[i].split("|",2)[1];if(select_typep==0)
{document.write("<li id=item_"+i+" onmousemove=\"javas"+"cript:this.style.background='#CCCCCC'\" onmouseout=\"javas"+"cript:this.style.background=''\" onclick=\"add_value('"+input_Namep+"','"+select_option+"','"+select_value+"','"+form_namep+"')\" >"+select_option+"</li><div id=\"value_"+i+"\" class=\"select_value\">"+select_value+"</div>")}
else if(select_typep==1)
{document.write("<li id=item_"+i+" onmousemove=\"javas"+"cript:this.style.background='#CCCCCC'\" onmouseout=\"javas"+"cript:this.style.background=''\"  onclick=\"go_url('"+select_option+"','"+select_value+"')\" >"+select_option+"</li><div id=\"value_"+i+"\" class=\"select_value\">"+select_value+"</div>")}}
document.write("</ul>")
document.write("</div></div>")
document.write("</div><div id=\"item_all\"  style=\"text-align:center;height:20px;line-height:20px; color:#959595\">"+add_itemp[0].split("|",2)[0]+"</div></div>")}
function add_value(input_Namep,select_option,select_value,form_namep)
{select_option=select_option
if(select_value==0){select_value=''}
eval("document."+form_namep+"."+input_Namep+".value='"+select_value+"'")
if(navigator.appName.indexOf("Explorer")>-1){document.getElementById("item_all").innerText=select_option;}else{document.getElementById("item_all").textContent=select_option;}}
function go_url(select_option,select_value){if(select_value!=0){MM_openBrWindowp(select_value,'','toolbar=yes,location=yes,status=yes,menubar=yes,scrollbars=yes,resizable=yes')}}
function ReadCookie(cookieName)
{var theCookie=""+document.cookie;var ind=theCookie.indexOf(cookieName);if(ind==-1||cookieName=="")return"";var ind1=theCookie.indexOf(';',ind);if(ind1==-1)ind1=theCookie.length;return unescape(theCookie.substring(ind+cookieName.length+1,ind1));}
