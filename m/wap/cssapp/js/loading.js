
function GetHtmlPageSize() {
    var scrW, scrH;
    if (window.innerHeight && window.scrollMaxY) {    // Mozilla    
        scrW = window.innerWidth + window.scrollMaxX;
        scrH = window.innerHeight + window.scrollMaxY;
    } else if (document.body.scrollHeight > document.body.offsetHeight) {    // all but IE Mac    
        scrW = document.body.scrollWidth;
        scrH = document.body.scrollHeight;
    } else if (document.body) { // IE Mac    
        scrW = document.body.offsetWidth;
        scrH = document.body.offsetHeight;
    }
    var winW, winH;
    if (window.innerHeight) { // all except IE    
        winW = window.innerWidth;
        winH = window.innerHeight;
    } else if (document.documentElement && document.documentElement.clientHeight) {    // IE 6 Strict Mode    
        winW = document.documentElement.clientWidth;
        winH = document.documentElement.clientHeight;
    } else if (document.body) { // other    
        winW = document.body.clientWidth;
        winH = document.body.clientHeight;
    }    // for small pages with total size less then the viewport  
    var pageW = (scrW < winW) ? winW : scrW; var pageH = (scrH < winH) ? winH : scrH;
    return { PageW: pageW, PageH: pageH, WinW: winW, WinH: winH };
}

function sAlert(str)
{   
   var msgw,msgh,bordercolor;   
   msgw=200;//提示窗口的宽度   
   msgh=50;//提示窗口的高度   
   titleheight=25 //提示窗口标题高度   
   bordercolor = "#00A1EA";//提示窗口的边框颜色   
   titlecolor="#99CCFF";//提示窗口的标题颜色   
  
   var sWidth,sHeight;   
   //sWidth=document.body.offsetWidth;//浏览器工作区域内页面宽度
   //sHeight = screen.height + 20; //屏幕高度（垂直分辨率）

   var sz = GetHtmlPageSize();
   sWidth=sz.PageW;
   sHeight = sz.PageH;

   
  
   //背景层（大小与窗口有效区域相同，即当弹出对话框时，背景显示为放射状透明灰色）   
   var bgObj=document.createElement("div");//创建一个div对象（背景层）   
   //定义div属性，即相当于
   bgObj.setAttribute('id','bgDiv');   
   bgObj.style.position="absolute";   
   bgObj.style.top="0";   
   bgObj.style.background="#777";   
   //bgObj.style.filter="progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75";   
   bgObj.style.opacity="0.6";   
   bgObj.style.left="0";   
   bgObj.style.width=sWidth + "px";   
   bgObj.style.height=sHeight + "px";
   bgObj.style.zIndex = "10000";   
   document.body.appendChild(bgObj);//在body内添加该div对象   
  
    //创建一个div对象（提示框层） 
   var msgObj = document.createElement("div");
   //定义div属性，即相当于   
   msgObj.setAttribute("id","msgDiv");   
   msgObj.setAttribute("align","center");   
   msgObj.style.background="white";   
   msgObj.style.border="2px solid " + bordercolor;   
   msgObj.style.position = "absolute";   
   msgObj.style.left = "50%";   
   msgObj.style.top = "50%";   
   msgObj.style.font="12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif";   
   msgObj.style.marginLeft = "-225px" ;   
   msgObj.style.marginTop = -75+document.documentElement.scrollTop+"px";   
   msgObj.style.width = msgw + "px";   
   msgObj.style.height =msgh + "px";   
   msgObj.style.textAlign = "center";   
   msgObj.style.lineHeight ="25px";   
   msgObj.style.zIndex = "10001";   
       

   //在body内添加提示框div对象msgObj   
   document.body.appendChild(msgObj);
   
   var txt = document.createElement("p");//创建一个p对象（提示框提示信息）  
   //定义p的属性，即相当于   
   
   txt.setAttribute("id", "msgTxt");
   txt.style.height = "16px";
   txt.style.lineHeight = "16px";
   txt.innerHTML = "<img src='loading.gif' border='0'/>" + str;//来源于函数调用时的参数值   

   //在提示框div中添加提示信息对象txt   
   document.getElementById("msgDiv").appendChild(txt);
}

function removeObj() {//点击标题栏触发的事件   
   
    //删除背景层Div 
    var bgObj=  document.getElementById("bgDiv")
    document.body.removeChild(bgObj);

    //删除提示框层  
    var msgObj = document.getElementById("msgDiv");  
    document.body.removeChild(msgObj); 
}

function sys_confirm(str) {
    var state = confirm(str);
    if (state)
        sAlert('系统正在处理中。。。。。。。。。请稍等！');

    return state;
      
}