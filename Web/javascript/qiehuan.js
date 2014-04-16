// JavaScript Document
 
function hai(thisObj,Num){
	//if(thisObj.className == "dsp_active")
		//return;
	//alert("o");
	var tabObj = thisObj.parentNode.id;
	var tabList = document.getElementById(tabObj).getElementsByTagName("li");
	for(i=0; i <tabList.length; i++)
	{
	  if (i == Num)
	  {
	   thisObj.className = "white";
	      document.getElementById(tabObj+i).style.display = "block";
		  
	  }else{
	   tabList[i].className = "black";
	   document.getElementById(tabObj+i).style.display = "none";
	  }
	}
}

