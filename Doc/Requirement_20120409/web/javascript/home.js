		function  addFav() {   //加入收藏夹    
             if  (document.all) {   
                window.external.addFavorite('http://www.hongru.com', '京价网');   
            }   
             else   if  (window.sidebar) {   
            window.sidebar.addPanel('京价网', 'http://www.hongru.com',  "" );   
            }   
        }   
function SetHome(obj){   
    try{   
        obj.style.behavior='url(#default#homepage)';   
        obj.setHomePage('http://www.hongru.com');   
    }catch(e){   
        if(window.netscape){   
            try{    
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");   
            }catch(e){   
                alert("抱歉，您的浏览器需要修改设置才能设为首页！\n\n请在浏览器地址栏输入about:config并回车然后将 [signed.applets.codebase_principal_support]设置为'true'");    
            };   
        }else{   
            alert("抱歉，您的浏览器需要修改设置才能设为首页。\n\n您需要手动将'http://www.hongru.com/'设置为首页。");   
        };   
    };   
};  