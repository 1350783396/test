function getLodop(oOBJECT,oEMBED){
/**************************
  本函数根据浏览器类型决定采用哪个对象作为控件实例：

  IE系列、IE内核系列的浏览器采用oOBJECT，

  其它浏览器(Firefox系列、Chrome系列、Opera系列、Safari系列等)采用oEMBED。

**************************/
    var strHtml1 = "<br><font color='#FF00FF'>打印控件未安装!</font>";
    var strHtml2 = "<br><font color='#FF00FF'>打印控件需要升级!</font>";
        var strHtml3="<br><br><font color='#FF00FF'>注意：<br>1：如曾安装过旧版附件,请在卸它;</font>";
        var LODOP=oEMBED;		
	try{		     
	     if (navigator.appVersion.indexOf("MSIE")>=0) LODOP=oOBJECT;

	     if ((LODOP==null)||(typeof(LODOP.VERSION)=="undefined")) {
		 if (navigator.userAgent.indexOf('Firefox')>=0)
  	         document.documentElement.innerHTML=strHtml3+document.documentElement.innerHTML;
		 if (navigator.appVersion.indexOf("MSIE")>=0) document.write(strHtml1); else
		 document.documentElement.innerHTML=strHtml1+document.documentElement.innerHTML;
		 return LODOP; 
	     } else if (LODOP.VERSION<"6.0.3.4") {
		 if (navigator.appVersion.indexOf("MSIE")>=0) document.write(strHtml2); else
		 document.documentElement.innerHTML=strHtml2+document.documentElement.innerHTML; 
		 return LODOP;
	     }
	     //*****如下空白位置适合调用统一功能:*********	     


        LODOP.SET_LICENSES("北京中电亿商网络技术有限责任公司", "653726081798577778794959892839", "", "");
	     //*******************************************
	     return LODOP; 
	}catch(err){
	     document.documentElement.innerHTML="Error:"+strHtml1+document.documentElement.innerHTML;
	     return LODOP; 
	}
}
