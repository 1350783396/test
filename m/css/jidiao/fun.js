String.prototype.trim= function(){return this.replace(/(^\s*)|(\s*$)/g,"");};
//------------------------------------------------------------------------------------------
function ismob(){
    var w=document.body.clientWidth||document.documentElement.clientWidth;
	if(w>760){
		return false;
	}else{
		return true;
	}
}
//------------------------------------------------------------------------------------------
function ispc(){
    var w=document.body.clientWidth||document.documentElement.clientWidth;
	if(w>760){
		return true;
	}else{
		return false;
	}
}
//------------------------------------------------------------------------------------------
function isWeiXin(){ 
   var ua = window.navigator.userAgent.toLowerCase(); 
   if(ua.match(/MicroMessenger/i) == 'micromessenger'){ 
      return true; 
   }else{ 
      return false; 
   } 
} 
//------------------------------------------------------------------------------------------
function val(ms){
	  if(typeof(ms)=="undefined")return 0;
      var v=parseFloat(ms);
      if(isNaN(v))v=0;
      return v;	
}
//------------------------------------------------------------------------------------------
function doajax(mfile,callback){
//通用AJAX模块
var mstr;var majax;try{majax=new XMLHttpRequest();}catch (e){try{majax=new ActiveXObject("Msxml2.XMLHTTP");}catch (e){try{majax=new ActiveXObject("Microsoft.XMLHTTP");}catch (e){majax=null;}}}
if(majax!=null){
  majax.onreadystatechange=function(){    
      if(majax.readyState==4){
	    mstr=majax.responseText.replace(/\r\n/g,"");
        try{callback(mstr)}catch(e){}
 	  }};
   majax.open("POST",mfile,true);majax.send(null);
  }
}
//------------------------------------------------------------------------------------------
function ajaxpost(formname,mfile,callback){
//通用AJAX提交表单模块
var mstr;var majax;try{majax=new XMLHttpRequest();}catch (e){try{majax=new ActiveXObject("Msxml2.XMLHTTP");}catch (e){try{majax=new ActiveXObject("Microsoft.XMLHTTP");}catch (e){majax=null;}}}
if(majax!=null){
  majax.onreadystatechange=function(){    
      if(majax.readyState==4){
	    mstr=majax.responseText.replace(/\r\n/g,"");
        callback(mstr);
 	  }};
   majax.open("POST",""+mfile,true);  //http://zjfyds.com
   majax.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
   var ms=getFormSendData(formname); 
   majax.send(ms);
  }
}
//------------------------------------------------------------------------------------------
function getFormSendData(formname){
   var f=document.getElementById(formname);
   var mname="";
   var mtype;
   var o;
   var aa=new Array();
   var bb=new Array();
   var ms="";
   for(var t=0;t<f.elements.length;t++){
	      o=f.elements.item(t);
		  mname="";
		  mname=o.name;
		  mtype=o.type;
		  if(mtype=="checkbox"){ //checkbox处理要稍复杂一点
            if(o.checked==true){
			   	if(aa[mname]==1){
				   //如果此名称已经处理过，就加到里面并用 ， 号分隔
				   bb[mname]+=","+o.value;
				}else{
				   //如果此名称是第一次处理	
				   aa[mname]=1;
				   bb[mname]=o.value;
				}
			}
		  }else if(mtype=="radio"){
            if(o.checked==true){
				bb[mname]=o.value;
			}
		  }else if(mtype=="password"||mtype=="hidden"||mtype=="text"||mtype=="select-one"||mtype=="select-multiple"){
			   	if(aa[mname]==1){
			       bb[mname]+=","+o.value;
				}else{
				   aa[mname]=1;
			       bb[mname]=o.value;
				}
		  }else if(mtype=="textarea"){
		     bb[mname]=o.value;
		  }else{

		  }
   }
   for(index in bb){
	   if(ms==""){ms+=index+"="+encodeURIComponent(bb[index]);}
	   else{ms+="&"+index+"="+encodeURIComponent(bb[index]);}
   }
   return ms;
}
//------------------------------------------------------------------------------------------
function getpar(pname){
	    var ms=window.location.search.substr(1);
		var aa=ms.split("&");
		var tot=aa.length;
		for(var t=0;t<tot;t++){
			var bb=aa[t].split("=");
			if(bb[0]==pname){
				return bb[1];
				break;
			}
		}
		return "";
}

function closeloading(){
	   try{
		 document.body.removeChild(document.getElementById("dialog_loading"));
	   }catch(e){}
	   try{
		 document.body.removeChild(document.getElementById("dialog_loading_bg"));
	   }catch(e){}
}
//------------------------------------------------------------------------------------------
function showloading(){
	function closeme(){
	   try{
		 document.body.removeChild(document.getElementById("dialog_loading"));
	   }catch(e){}
	   try{
		 document.body.removeChild(document.getElementById("dialog_loading_bg"));
	   }catch(e){}
	}
	closeme();
    var w=document.documentElement.clientWidth||document.body.clientWidth;
	var h=document.documentElement.clientHeight||document.body.clientHeight;
	var ww=100;
	var hh=100;

	var bg=document.createElement("div");
	bg.id="dialog_loading_bg";
	bg.style.zIndex=999998;
	bg.style.overflow="hidden";
	bg.style.position="fixed";
	bg.style.width=w+"px";
	bg.style.height=h+"px";
	bg.style.left=0+"px";
	bg.style.top=0+"px";
	bg.style.backgroundColor="#333";
	bg.style.display="none";
    try{bg.style.filter="alpha(opacity=70)";}catch(e){}
	try{bg.style.opacity=0.7;}catch(e){}
	
	var div=document.createElement("div");
	div.id="dialog_loading";
	div.style.zIndex=999999;
	div.style.overflow="hidden";
	div.style.position="fixed";
	div.style.width=ww+"px";
	div.style.height=hh+"px";
	div.style.left=(w-ww)/2+"px";
	div.style.top=(h-hh)/2+"px";
	div.style.display="none";

	var ss=document.createElement("div");
	var ms="<img src='/9766/pub/m/img/load0.gif' width='100' height='100'/>";
	ss.innerHTML=ms;

	div.appendChild(ss);
	document.body.appendChild(bg);
	document.body.appendChild(div);

	$(div).show();
	$(bg).show();
	
}	

//----------------------------------------------------------------
// DIV 拖动
//----------------------------------------------------------------


function dragmehead(obj){
	//--------------------------------------------------------------
	var f_down=false;
	var f_move=false;
	var startX = 0;
	var startY = 0;
	var deltaX = 0;
	var deltaY = 0;
	//--------------------------------------------------------------
	function getobj(o){
	   if(typeof o=="string"){return document.getElementById(o)}
	   else{return o}
	}
	//--------------------------------------------------------------
	function listen(o,ms,fun){
        if(document.attachEvent){o.attachEvent("on"+ms,fun)}
		else{o.addEventListener(ms,fun,false)}
	}
	//--------------------------------------------------------------
	function dellisten(o,ms,fun){
			if(document.attachEvent){o.detachEvent("on"+ms,fun)}
			else{o.removeEventListener(ms,fun,false)}
	}
	//--------------------------------------------------------------
    function bodyDown(ee){
		f_down=true;
	    var e = ee||event||window.event;
		var mouseX = e.clientX;
		var mouseY = e.clientY;
		var xy=$(drag).offset();
		//startX = drag.offsetLeft;
		//startY = drag.offsetTop;
		startX=parseFloat(xy.left);
		startY=parseFloat(xy.top);
		
		deltaX = mouseX - startX;
		deltaY = mouseY - startY;
if(deltaY<40){
        listen(drag,"mousemove",dragMove);
}
	}
	//--------------------------------------------------------------
	function bodyMove(ee){
			if(f_down&&f_move){
			   var e = ee||event||window.event;
			   drag.style.left = (e.clientX - deltaX)+"px";
			   drag.style.top = (e.clientY - deltaY)+"px";
			   
			   var xy=$(drag).offset();
			   if(xy.left<0){
				   drag.style.left=0;
			   }
			   if(xy.top<0){
				   drag.style.top=0;
			   }
			   var ww=document.body.clientWidth||document.documentElement.clientWidth;
			   if(xy.left+$(drag).outerWidth()>ww){
				   drag.style.left=(ww-$(drag).outerWidth())+"px";
			   }
			}else{
			   dellisten(drag,"mousemove",dragMove);
			}
	}
	//--------------------------------------------------------------
	function bodyUp(){
			f_down=false;
			f_move=false;
			dellisten(drag,"mousemove",dragMove);
	}
	//--------------------------------------------------------------
	function dragMove(){
            f_move=true;
	}
	//--------------------------------------------------------------
	var drag=getobj(obj);
	if(drag){
	   listen(document,"mousedown",bodyDown);
	   listen(document,"mouseup",bodyUp);
	   listen(document,"mousemove",bodyMove);
	}

}
//------------------------------------------------------------------------------------------
function emform(){
	//信息窗
	var opts=arguments[0];
	var isyes=false;
	if(typeof arguments[1] == "function"){
		var callbackready=arguments[1];
	}
	if(typeof arguments[2] == "function"){
		var callbackyes=arguments[2];
		isyes=true;
	}


	function closeme(){
		try{parent.document.body.removeChild(bg)}catch(e){}
		try{parent.document.body.removeChild(box)}catch(e){}
	}
	
    var w=parent.document.body.clientWidth||parent.document.documentElement.clientWidth;
    var h=parent.document.body.clientHeight||parent.document.documentElement.clientHeight;
    var st=parent.document.body.scrollTop||parent.document.documentElement.scrollTop;
	var bg=parent.document.createElement("div");
	
	//bg.id="emconfirm_bgid";
	bg.style.position="fixed";
	bg.style.width="100%";
	bg.style.height="100%";
	bg.style.left=0+"px";
	bg.style.top=0+"px";
	bg.style.backgroundColor="#333333";
	bg.style.zIndex=999998;
	parent.document.body.appendChild(bg);
    try{bg.style.filter="alpha(opacity=80)";}catch(e){}
	try{bg.style.opacity=0.8;}catch(e){}

	var box=parent.document.createElement("div");
	//box.id="emconfirm_boxid";
	box.style.position="absolute";
	box.style.overflow="hidden";
	if(w>760){
	   box.style.width="50%";
	}else{
	   box.style.width="98%";
	}
	box.style.height="auto";
	box.style.backgroundColor="#FFF";
	box.style.zIndex=999999;
    box.style.display="none";

	var head=parent.document.createElement("div");
	head.style.overflow="hidden";
	head.style.height=40+"px";
	head.style.position="relative";
	head.style.backgroundColor="#FFF";
	head.style.borderBottom="1px solid #ddd";
	head.style.margin=10+"px";
	box.appendChild(head);

		var cap=parent.document.createElement("span");
		cap.style.overflow="hidden";
		cap.style.display="block";
		cap.style.color="#333";
		cap.style.height=40+"px";
		cap.style.lineHeight=40+"px";
		cap.style.fontSize=16;
		cap.style.fontWeight="700";
		if(opts.title){
		   cap.innerHTML=opts.title;
		}else{
		   cap.innerHTML="系统消息";
		}
    head.appendChild(cap);
	
		var cc=parent.document.createElement("div");
		cc.style.overflow="hidden";
		cc.style.width=34+"px";
		cc.style.height=34+"px";
		cc.style.backgroundColor="#FFF";
		cc.style.fontSize=28+"px";
		cc.style.lineHeight=32+"px";
		cc.style.color="#777";
		cc.style.position="absolute";
		cc.style.right=2+"px";
		cc.style.top=2+"px";
		cc.style.cursor="pointer";
		cc.style.textAlign="center";
		//cc.style.border="1px solid #DDD";
		cc.style.fontFamily="Verdana, Geneva, sans-serif";
		try{cc.style.borderRadius=3+"px";}catch(e){}
		cc.innerHTML="&times;";
		cc.onmousemove=function(){cc.style.backgroundColor="#DDD";};
		cc.onmouseout=function(){cc.style.backgroundColor="#FFF";};
		cc.onclick=function(){closeme()};
    head.appendChild(cc);
	
	var cont=parent.document.createElement("div");
	cont.style.position="relative";
	cont.style.overflow="hidden";
	cont.style.height="auto";
	cont.style.margin=10+"px";
    try{cont.innerHTML=opts.msg;}catch(e){}
	box.appendChild(cont);


	var foot=parent.document.createElement("div");
	foot.style.position="relative";
	foot.style.overflow="hidden";
	foot.style.height="auto";
	foot.style.margin=10+"px";
	foot.style.textAlign="right";
	foot.style.paddingTop=10+"px";
	foot.style.borderTop="1px solid #e7e7e7";
	
	var fc1=parent.document.createElement("span");
	fc1.style.display="inline-block";
	fc1.style.overflow="hidden";
	fc1.style.width=100+"px";
	fc1.style.height=35+"px";
	fc1.style.border="1px solid #CCC";
	fc1.style.textAlign="center";
	fc1.style.lineHeight=35+"px";
	fc1.style.fontSize=12+"px";
	fc1.style.cursor="pointer";
	fc1.style.color="#F63";
	try{fc1.style.borderRadius=5+"px"}catch(e){}
	if(opts.yestitle){
	   fc1.innerHTML=opts.yestitle;
	}else{
	   fc1.innerHTML="确&nbsp;&nbsp;定";
	}
	fc1.onmousemove=function(){fc1.style.backgroundColor="#f1f1f1";};
	fc1.onmouseout=function(){fc1.style.backgroundColor="";};
	fc1.onclick=function(){
		try{callbackyes();closeme()}catch(e){}
	};
	if(isyes){
       foot.appendChild(fc1);	
	}

	var fc2=parent.document.createElement("span");
	fc2.style.display="inline-block";
	fc2.style.overflow="hidden";
	fc2.style.width=100+"px";
	fc2.style.height=35+"px";
	fc2.style.border="1px solid #CCC";
	fc2.style.textAlign="center";
	fc2.style.lineHeight=35+"px";
	fc2.style.fontSize=12+"px";
	fc2.style.cursor="pointer";
	fc2.style.color="#333";
	fc2.style.marginLeft=10+"px";
	try{fc2.style.borderRadius=5+"px"}catch(e){}
	fc2.innerHTML="关&nbsp;&nbsp;闭";
	fc2.onmousemove=function(){fc2.style.backgroundColor="#f1f1f1";};
	fc2.onmouseout=function(){fc2.style.backgroundColor="";};
	fc2.onclick=function(){closeme()};
    foot.appendChild(fc2);	
    
	if(opts.nofoot){
	}else{
	   box.appendChild(foot);
	}

	parent.document.body.appendChild(box);
	$(box).css({"top":(10+st)+"px","left":((w-$(box).outerWidth())/2)+"px"});
	box.off=function(){
		closeme();
	}
	try{callbackready(box)}catch(e){}
	$(box).fadeIn(function(){
       dragmehead(box);
	});	
}
//------------------------------------------------------------------------------------------
function emalert(){
	//信息窗
	var msg=arguments[0];
	if(typeof arguments[1] == "function"){
		var callback=arguments[1];
	}

	function closeme(){
		try{parent.document.body.removeChild(bg)}catch(e){}
		try{parent.document.body.removeChild(box)}catch(e){}
	    try{callback()}catch(e){}
	}
	
    var w=parent.document.body.clientWidth||parent.document.documentElement.clientWidth;
    var h=parent.document.body.clientHeight||parent.document.documentElement.clientHeight;
    var st=parent.document.body.scrollTop||parent.document.documentElement.scrollTop;
	var bg=parent.document.createElement("div");
	
	//bg.id="emconfirm_bgid";
	bg.style.position="fixed";
	bg.style.width="100%";
	bg.style.height="100%";
	bg.style.left=0+"px";
	bg.style.top=0+"px";
	bg.style.backgroundColor="#333333";
	bg.style.zIndex=9999998;
	parent.document.body.appendChild(bg);
    try{bg.style.filter="alpha(opacity=80)";}catch(e){}
	try{bg.style.opacity=0.8;}catch(e){}

	var box=parent.document.createElement("div");
	//box.id="emconfirm_boxid";
	box.style.position="fixed";
	box.style.overflow="hidden";
	if(w>760){
	   box.style.width="40%";
	}else{
	   box.style.width="80%";
	}
	box.style.height="auto";
	box.style.backgroundColor="#FFF";
	box.style.zIndex=9999999;
    box.style.display="none";
	box.style.fontSize=14+"px";
	box.className="yinyin";

	var head=parent.document.createElement("div");
	head.style.overflow="hidden";
	head.style.height=40+"px";
	head.style.position="relative";
	head.style.backgroundColor="#FFF";
	head.style.borderBottom="1px solid #ddd";
	head.style.margin=10+"px";
	box.appendChild(head);

		var cap=parent.document.createElement("span");
		cap.style.overflow="hidden";
		cap.style.display="block";
		cap.style.color="#333";
		cap.style.height=40+"px";
		cap.style.lineHeight=40+"px";
		cap.style.fontSize=16;
		cap.style.fontWeight="700";
	   cap.innerHTML="系统消息";
    head.appendChild(cap);
	
		var cc=parent.document.createElement("div");
		cc.style.overflow="hidden";
		cc.style.width=34+"px";
		cc.style.height=34+"px";
		cc.style.backgroundColor="#FFF";
		cc.style.fontSize=28+"px";
		cc.style.lineHeight=32+"px";
		cc.style.color="#777";
		cc.style.position="absolute";
		cc.style.right=2+"px";
		cc.style.top=2+"px";
		cc.style.cursor="pointer";
		cc.style.textAlign="center";
		//cc.style.border="1px solid #DDD";
		cc.style.fontFamily="Verdana, Geneva, sans-serif";
		try{cc.style.borderRadius=3+"px";}catch(e){}
		cc.innerHTML="&times;";
		cc.onmousemove=function(){cc.style.backgroundColor="#DDD";};
		cc.onmouseout=function(){cc.style.backgroundColor="#FFF";};
		cc.onclick=function(){closeme()};
    head.appendChild(cc);
	
	var cont=parent.document.createElement("div");
	cont.style.position="relative";
	cont.style.overflow="hidden";
	cont.style.height="auto";
	cont.style.margin=10+"px";
    try{cont.innerHTML=msg;}catch(e){}
	box.appendChild(cont);


	var foot=parent.document.createElement("div");
	foot.style.position="relative";
	foot.style.overflow="hidden";
	foot.style.height="auto";
	foot.style.margin=10+"px";
	foot.style.textAlign="right";
	foot.style.paddingTop=10+"px";
	foot.style.borderTop="1px solid #e7e7e7";
	
	var fc1=parent.document.createElement("span");
	fc1.style.display="inline-block";
	fc1.style.overflow="hidden";
	fc1.style.width=100+"px";
	fc1.style.height=35+"px";
	fc1.style.border="1px solid #CCC";
	fc1.style.textAlign="center";
	fc1.style.lineHeight=35+"px";
	fc1.style.fontSize=12+"px";
	fc1.style.cursor="pointer";
	fc1.style.color="#333";
	try{fc1.style.borderRadius=5+"px"}catch(e){}
	fc1.innerHTML="确&nbsp;&nbsp;定";
	fc1.onmousemove=function(){fc1.style.backgroundColor="#f1f1f1";};
	fc1.onmouseout=function(){fc1.style.backgroundColor="";};
	fc1.onclick=function(){
		closeme();
	};
    
    foot.appendChild(fc1);
    box.appendChild(foot);

	parent.document.body.appendChild(box);
	st=0;
	$(box).css({"top":(20+st)+"px","left":((w-$(box).outerWidth())/2)+"px"});
	$(box).fadeIn(function(){
       dragmehead(box);
	});	
}
window.alertem=emalert;
//------------------------------------------------------------------------------------------
function loginform(){
	var w1="40",w2="50",fl="left";
	if(ismob()){
		w1="90";
		w2="90";
		fl="right";
	}
	var ms='';
	ms+='<div class="editform">';
	ms+='<div style="width:'+w2+'%;float:right">';
	ms+='<input type="text" id="txtuserid" placeholder="输入用户ID/手机号码" maxlength="20"/><br />';
	ms+='<input type="text" id="txtuserpwd" placeholder="输入登录密码" maxlength="20"/><br />';
	ms+='<a href="javascript:;" id="findpwd" style="margin-top:12px;margin-left:10px;display:inline-block"><i class="fa fa-binoculars"></i> 忘记密码?点这里找回</a>';
	//ms+='<a href="/register.html" style="margin-top:12px;margin-left:110px;display:inline-block"><i class="fa fa-user"></i> 免费注册</a>';
	ms+='<br /><br />';
	ms+='<span id="cmdlogin" class="cmdlogin" style="width:85%">登录</span>';
	ms+='</div>';
	ms+='<div style="clear:both;overflow:hidden;height:2px"></div>';
	ms+='<div style="margin-top:10px;border-top:1px solid #ddd;padding-top:20px">';
	ms+='<div style="text-align:center" >';
	
	/*
	ms+='<a href="javascript:qqlogin()"><img src="/images/qqlogin.png" style="border:0px none;height:40px;vertical-align:middle"/></a>';
	if(isWeiXin()){
	   ms+='<p style="overflow:hidden;height:10px">&nbsp;</p><a href="javascript:wxlogin()" id="wxlogin"><img src="/images/weixin.jpg" style="border:0px none;height:40px;vertical-align:middle"/> <font style="font-size:16px">微信登录</font></a>';
	}else{
	   ms+='<p style="overflow:hidden;height:10px">&nbsp;</p><img src="/images/code2.png" style="width:150px;height:150px"/>';
	}
	*/
	
	//ms+='<span class="logincmd" data="qq"><em class="qq"></em><dd>ＱＱ登录</dd></span>';
	ms+='<span class="logincmd" data="wx" style="margin-left:20px"><em class="wx"></em><dd>微信登录</dd></span>';
	
	
	ms+='</div>';
	ms+='</div>';
	ms+='</div>';
	return ms;
}
//------------------------------------------------------------------------------------------
function login(){
	emform({
		title:"用户登录",
		msg:loginform(),
		nofoot:true
	},function(){
		$(window).bind("keyup",function(e){
			if(e.which==13){
				$("#cmdlogin").click();
			}
		})
		$("#findpwd").unbind().click(function(){
		  if($("#txtuserid").val()==""){
			  alertem("请输入手机号");
			  return;
		  }
		  $.get("/dataapi.asp?dowhat=chktel&tel="+$("#txtuserid").val(),function(data,stat){
			  if(data.replace(/\r\n/gi,"")=="ok"){
		          if(window.isfinding)return;
		          window.isfinding=true;
		          $.get("/emsendpwdasp.asp?dowhat=findpwd&tel="+$("#tel").val(),function(data,stat){
			         window.isfinding=false;
			         alertem("密码已发您的手机,请注意接收");
		          });
			  }else{
				  alertem("此手机不存在!");
			  }
		  });
		});
		
		$(".logincmd").each(function(index,obj){
			obj.onclick=function(){
				var s=$(obj).attr("data");
				try{eval("loginby"+s+"()")}catch(e){}
			};
		});
		$("#cmdlogin").unbind("click").click(function(){
			if($("#txtuserid").val().trim()==""){
			   alertem("请输入用户ID");
			   return;
			}
			if($("#txtuserpwd").val().trim()==""){
			   alertem("请输入登录密码");
			   return;
			}
			$.get("/php/ajax.php?dowhat=dologin&userid="+encodeURIComponent($("#txtuserid").val())+"&userpwd="+encodeURIComponent($("#txtuserpwd").val())+"&chkcode="+encodeURIComponent($("#txtchkcode").val()),function(data,stat){
				$("#txtchkcode").val("");
				$("#imgchkcode").click();
				if(data.replace(/\r\n/gi,"")==""){
					alertem("登录失败,请检查用户名/密码/验证码是否正确");
					return;
				}
				 eval("var aa="+data);
				 if(aa.err==""){
					 //alertem(aa.username+"你好,你已登录成功",function(){
				 		document.location.reload();
					 //});
				 }else{
					 alertem("登录失败,"+aa.err);
				 }
			});
		});
	});
}
//------------------------------------------------------------------------------------------
function exit(){
	$.get("/php/ajax.php?dowhat=exitlogin",function(data,stat){
		document.location.reload();
	});
}
//------------------------------------------------------------------------------------------
function qqlogin(){
   //window.open('https://graph.qq.com/oauth2.0/authorize?client_id=101299014&amp;response_type=token&amp;scope=all&amp;redirect_uri=http%3A%2F%2Fwww.zjfyds.com%2Fqqlogincallback.php', 'oauth2Login_10911' ,'height=525,width=585, toolbar=no, menubar=no, scrollbars=no, status=no, location=yes, resizable=yes');
      var ww=document.documentElement.clientWidth||document.body.clientWidth;
	  if(ww<700){
		  document.location.href="/qq/login.php";
	  }else{
          var childWindow;
          childWindow = window.open("/qq/login.php","TencentLogin","width=705,height=425,menubar=0,scrollbars=1, resizable=1,status=1,titlebar=0,toolbar=0,location=1");
	  }

}
//------------------------------------------------------------------------------------------
function wxlogin(){
    document.location.href="/wxpay/wxreg.php";
}
//------------------------------------------------------------------------------------------
function loginbyqq(){
	qqlogin();
}
//------------------------------------------------------------------------------------------
function loginbywx(){
	if(isWeiXin()){
		wxlogin();
	}else{
		    var dd=new Date();
		    var rnd=dd.getFullYear().toString()+(dd.getMonth()+1).toString()+dd.getDate().toString()+dd.getHours().toString()+dd.getMinutes().toString()+dd.getSeconds().toString()+parseInt(Math.random()*9999).toString();
		    alertem('<div style="text-align:center"><div style="margin:10px 0px">微信扫码登录</div><img src="/wxpay/example/qrcode.php?data='+encodeURIComponent("http://www.zjfyds.com/wxpay/wxreg.php?rnd="+rnd)+'" style="width:180px;height:180px"/></div>');
			chkloginstat();
			function chkloginstat(){
				$.get("/php/ajax.php?dowhat=getloginstat&rnd="+rnd,function(data,stat){
					if(data=="ok"){
						document.location.reload();
					}else{
						setTimeout(function(){
							chkloginstat();
						},1000);
					}
				});
			}
//document.location.href="https://open.weixin.qq.com/connect/qrconnect?appid=wx43f187a68a6e0642&redirect_uri="+encodeURIComponent("http://www.zjfyds.com/wxlogin.php")+"&response_type=code&scope=snsapi_login&state=123#wechat_redirect";
	}
}
//------------------------------------------------------------------------------------------
function pubsetup(){
   var f=document.getElementById("formup");
   if(!f){
	   div=document.createElement("div");
	   div.style.display="none";
	   $(div).html('<form id="formup" name="formup" enctype="multipart/form-data" style="display:none" ><input type="file" id="file1" name="file1" class="up" /></form>');
	   document.body.appendChild(div);
   }
   $("#file1").unbind("change").change(function(){
	   window.isuping=true;
	    $("#file").hide();
		$.ajax({
    		url: '/emadmin/doupload.php',
    		type: 'POST',
    		cache: false,
    		data: new FormData($('#formup')[0]),
    		processData: false,
    		contentType: false
		}).done(function(res){
			window.isuping=false;
			eval("var aa="+res);
			$("#file1").show();
			var txt=$("#file1").attr("datatxt");
			var img=$("#file1").attr("dataimg");
		    $("#"+img).attr("src",aa.url);
		    $("#"+txt).val(aa.url);
			try{
				eval($("#file1").attr("datado")+"('"+aa.url+"')");
			}catch(e){}
		}).fail(function(res){
			window.isuping=false;
			$("#file1").show();
		}); 	   
    });
	$(".upcmd").each(function(index,obj){
		 obj.onclick=function(){
			 if(window.isuping){
				 alertem("还有文件正在上传中");
				 return;
			 }
			 $("#file1").attr("datatxt",$(obj).attr("datatxt")).attr("dataimg",$(obj).attr("dataimg")).attr("datado",$(obj).attr("datado"));
			 $("#file1").click();
		 };
	});

}
//------------------------------------------------------------------------------------------
function showtip(obj,msg){
	var div;
	var divid="em_tip_layer_id";
	var ww=document.documentElement.clientWidth||document.body.clientWidth;
	function closeme(){
		try{document.body.removeChild(document.getElementById(divid))}catch(e){}
	}
	function show(){
	  closeme();
	  div=document.createElement("span");
	  div.id=divid;
	  div.className="tiplayer";
      document.body.appendChild(div);
	  var xy=$(obj).offset();
	  var x=xy.left;
      if(ww<580){
	     div.style.right=5+"px";
	  }else{
	     div.style.left=x+"px";
	  }
	  div.style.top=(xy.top+$(obj).outerHeight())+"px";
	  div.innerHTML=msg;
	}
	obj.onmouseover=function(){
		show();
	};
	obj.onclick=function(){
		if(document.getElementById(divid)){
			closeme();
		}else{
		    show();
		}
	};
	obj.onmouseout=function(){
		closeme();
	}
}
//------------------------------------------------------------------------------------------
function seteditor(){
        try{KindEditor.remove(".editor");}catch(e){}	//很重要，不然编辑器会叠加创建多个控件	
        var editor;
        try{
		   editor=KindEditor.create('textarea[class="editor"]', {
                allowFileManager : false,
				resizeType:0,
				width:"95%",
				items : ['source','|', 'undo', 'redo','|','fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold','italic', 'underline', 'lineheight', '|','justifyleft', 'justifycenter', 'justifyright','justifyfull', '|', 'clearhtml','removeformat','link', 'unlink','|','image']
				
           });
		   return editor;
		}catch(e){}
	
}
//------------------------------------------------------------------------------------------
function getCookie(c_name){
       if (document.cookie.length>0){
          c_start=document.cookie.indexOf(c_name + "=")
          if (c_start!=-1){ 
            c_start=c_start + c_name.length+1 
            c_end=document.cookie.indexOf(";",c_start)
            if (c_end==-1) c_end=document.cookie.length
            return unescape(document.cookie.substring(c_start,c_end))
         } 
       }
       return "";
}
//------------------------------------------------------------------------------------------
function delCookie(name){
   var exp = new Date();
   exp.setTime(exp.getTime() - 1);
   var cval=getCookie(name);
   if(cval!=null)document.cookie= name + "="+cval+";expires="+exp.toGMTString();
}
//------------------------------------------------------------------------------------------
function setCookie(name,value,time){
   var strsec = getsec(time);
   var exp = new Date();
   exp.setTime(exp.getTime() + strsec*1);
   document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();
}
//------------------------------------------------------------------------------------------
function getsec(str){
   //s20是代表20秒
   //h是指小时，如12小时则是：h12
   //d是天数，30天则：d30	
   var str1=str.substring(1,str.length)*1;
   var str2=str.substring(0,1);
   if (str2=="s"){
      return str1*1000;
   }else if (str2=="h"){
      return str1*60*60*1000;
   }else if (str2=="d"){
      return str1*24*60*60*1000;
   }
}
//------------------------------------------------------------------------------------------
function settopleftcmd(){
		 $("#i_back").unbind().click(function(e){
		    e.stopPropagation();
			document.location.href=$("#i_back").attr("data");
		 });

}
//------------------------------------------------------------------------------------------
function initBottomMenu(aa){
	var pubbgc="#00A2CA";
	var pubfc="#EEEEEE";
	var sublinec="#BBB";
   function init(aa){
	
	$(window).click(function(e){
        $(".bottomDH_subDH").hide();		
	});
	
	//初始化底部导航
	var menuHeight=50;
	//背景盒
	var fixBox=document.createElement("div");
	fixBox.style.position="fixed";
	fixBox.style.zIndex=9999;
	fixBox.style.width="100%";
	fixBox.style.height=menuHeight+"px";
	fixBox.style.backgroundColor=pubbgc;
	fixBox.style.borderTop="1px solid #BBBBBB";
	fixBox.style.left=0+"px";
	fixBox.style.bottom=0+"px";
	fixBox.style.cursor="default";
	fixBox.className="BM_fixBox";
	document.body.appendChild(fixBox);
	function buildDH(fag,callback){
		//单个导航
		if(fag=="center"){
			var ccc=document.createElement("div");
			ccc.style.position="absolute";
			ccc.style.overflow="hidden";
			ccc.style.backgroundColor="transparent";
			ccc.style.width=90+"px";
			ccc.style.height=90+"px";
			//try{ccc.style.borderRadius=90+"px";}catch(e){}
			ccc.style.top=-60+"px";
			ccc.style.left="50%";
			ccc.style.marginLeft=-45+"px";
			ccc.style.zIndex=1;
			ccc.style.backgroundRepeat="no-repeat";
			try{
			ccc.style.backgroundImage="url(/9766/pub/pubmob/img/cen_s.png)";
			ccc.style.backgroundPosition="center center";
			ccc.style.backgroundSize="100% auto";
			}catch(e){}
			
			//fixBox.appendChild(ccc);
		}
		var div=document.createElement("div");
		div.style.position="absolute";
		div.style.width="33%";
		div.style.top=0+"px";
		div.style.left=0+"px";
		div.style.margin=0+"px";
		div.style.padding=0+"px";
		div.className="BM_DH";
		if(ccc)div.appendChild(ccc);
		
		fixBox.appendChild(div);
		var lab=document.createElement("div");
		lab.style.height=menuHeight+"px";
		//lab.style.lineHeight=menuHeight+"px";
		lab.style.lineHeight="1";
		lab.style.paddingTop="8px";
		lab.style.textAlign="center";
		lab.style.fontSize=12+"px";
		lab.style.color=pubfc;
		lab.style.fontWeight="100";
		lab.style.width="100%";
		//lab.className="whitei";
		lab.style.position="absolute";
		lab.className="BM_lab";
		lab.style.zIndex=99;
		
		//子导航
		var box=document.createElement("div");
		box.style.width="100%";
		box.style.height="auto";
		box.style.border="1px solid #CCC";
		box.style.borderBottom="0px none";
		box.style.position="absolute";
		box.style.overflow="hidden";
		box.className="bottomDH_subDH ";
		box.style.left=0+"px";
		box.style.bottom=(0)+"px";
		box.style.backgroundColor=pubbgc;
		box.style.display="none";
		box.style.paddingLeft="8px";
		//try{box.style.borderTopLeftRadius=8+"px";}catch(e){}
		//try{box.style.borderTopRightRadius=8+"px";}catch(e){}
		div.appendChild(lab);
		div.appendChild(box);
		
		$(div).click(function(e){
			e.stopPropagation();
			var s=$(box).css("display");
			$(".bottomDH_subDH").hide();
			if(s=="none"){
				$(box).show();
			}else{
				$(box).hide();
			}
		});
		$(box).click(function(e){
			e.stopPropagation();
		});
		
		try{
			callback(div,lab,box)
			$(lab).find("i").each(function(index,obj){
				obj.style.fontSize="18px";
				obj.style.color=pubfc;
			});
			$(lab).find("hr").css({"border":"0px none","overflow":"hidden","height":"4px"});
		}catch(e){}
		return div;
	}
	//创建行
	function buildLine(box,ms,callback){
		var mline=document.createElement("div");
		//mline.style.width="100%";
		mline.height=33+"px";
		mline.textAlign="left";
		mline.style.margin="5px";
		mline.style.lineHeight=33+"px";
		mline.style.borderBottom="1px solid "+sublinec;
		mline.style.color=pubfc;
		mline.innerHTML=ms;
		box.appendChild(mline);
		$(mline).click(function(e){
			e.stopPropagation();
			$(box).hide();
			try{callback();}catch(e){}
		});
		return mline;
	}

	//左边
	var ml=buildDH("",function(div,lab,box){
		var bb=[];
		var t;
	    div.style.left="0%";
        lab.innerHTML=("<a ><i class='fa fa-credit-card'></i><hr><font style='color:"+pubfc+"'>景区报单</font></a>");
		
	    if(("A"+aa.mtype).indexOf("老板")>0||("A"+aa.mtype).indexOf("员工")>0||("A"+aa.mtype).indexOf("系统管理员")>0){
		   bb.push(t=buildLine(box,"<i class='fa fa-credit-card'></i> 景区报单",function(){
	           document.location.href="sightlist.php";
	       }));
		   bb.push(t=buildLine(box,"<i class='fa fa-paste'></i> 补单操作",function(){
	           document.location.href="rebd.php";
	       }));
	    }

	    if(("A"+aa.mtype).indexOf("系统管理员")>0||("A"+aa.mtype).indexOf("系统操作员")>0||("A"+aa.mtype).indexOf("计调员")>0){
          bb.push(buildLine(box,"<i class='fa fa-twitch' style='color:#DDD'></i> <font style='color:#DDD'>游客统计</font>",function(){
             document.location.href="bdyk.php";
          }));
	    }
	   
	    try{bb[bb.length-1].style.borderBottom="0px none";}catch(e){}
	});
	//中间
	var mc=buildDH("",function(div,lab,box){
	   var bb=[];
	   div.style.left="33%";
       lab.innerHTML=("<a ><i class='fa fa-gears'></i><hr><font style='color:"+pubfc+"'>系统功能</font></a>");

	   if(("A"+aa.mtype).indexOf("系统管理员")>0||("A"+aa.mtype).indexOf("财务员")>0){
			bb.push(buildLine(box,"<i class='fa fa-lock' style='color:#DDD'></i> <font style='color:#DDD'>报单锁定</font>",function(){
			   document.location.href="ordlock.php";
			}));
			bb.push(buildLine(box,"<i class='fa fa-bar-chart-o' style='color:#DDD'></i> <font style='color:#DDD'>财务统计</font>",function(){
			   document.location.href="moneystat.php";
			}));
	   }

       /*
	   bb.push(buildLine(box,"<i class='fa fa-sitemap' style='color:#DDD'></i> <font style='color:#DDD'>报单查询</font>",function(){
             document.location.href="bdsearch.php";
       }));
	   */
	   if(("A"+aa.mtype).indexOf("系统管理员")>0||("A"+aa.mtype).indexOf("计调员")>0){
          bb.push(buildLine(box,"<i class='fa fa-twitch' style='color:#DDD'></i> <font style='color:#DDD'>信息模板</font>",function(){
             document.location.href="yktip.php";
          }));
		   
	   }
	   
       //bb.push(buildLine(box,"<i class='fa fa-calendar' style='color:#DDD'></i> <font style='color:#DDD'>报单详情</font>",function(){
       //     document.location.href="ordinfo.php";
       //}));
	   /*
	   if(("A"+aa.mtype).indexOf("系统管理员")>0||("A"+aa.mtype).indexOf("计调员")>0){
          bb.push(buildLine(box,"<i class='fa fa-sitemap' style='color:#DDD'></i> <font style='color:#DDD'>报单组团</font>",function(){
             document.location.href="bdteam.php";
          }));
	   }
	   */

	   if(("A"+aa.mtype).indexOf("系统管理员")>0||("A"+aa.mtype).indexOf("系统操作员")>0||("A"+aa.mtype).indexOf("计调员")>0||("A"+aa.mtype).indexOf("财务员")>0){
          bb.push(buildLine(box,"<i class='fa fa-line-chart' style='color:#FF0'></i> <font style='color:#FF0'>报单统计</font>",function(){
             document.location.href="stat.php";
          }));
          bb.push(buildLine(box,"<i class='fa fa-clipboard' style='color:#DDD'></i> <font style='color:#DDD'>退改详情</font>",function(){
             document.location.href="retedit.php";
          }));

	   }
	   if(("A"+aa.mtype).indexOf("系统管理员")>0||("A"+aa.mtype).indexOf("财务员")>0){
          //bb.push(buildLine(box,"<i class='fa fa-list-ul' style='color:#DDD'></i> <font style='color:#DDD'>财务统计</font>",function(){
          //   document.location.href="flstat.php";
          //}));

          if(("A"+aa.mtype).indexOf("系统管理员")>0||("A"+aa.mtype).indexOf("财务员")>0){
          bb.push(buildLine(box,"<i class='fa fa-dedent' style='color:#DDD'></i> <font style='color:#DDD'>报单管理</font>",function(){
             document.location.href="ord.php";
          }));
		  }
          bb.push(buildLine(box,"<i class='fa fa-user' style='color:#DDD'></i> <font style='color:#DDD'>用户管理</font>",function(){
             document.location.href="user.php";
          }));
          if(("A"+aa.mtype).indexOf("系统管理员")>0){
          bb.push(buildLine(box,"<i class='fa fa-user-md ' style='color:#DDD'></i> <font style='color:#DDD'>用户审核</font>",function(){
             document.location.href="reglist.php";
          }));
		  }
          bb.push(buildLine(box,"<i class='fa fa-bar-chart' style='color:#DDD'></i> <font style='color:#DDD'>充值统计</font>",function(){
             document.location.href="jb.php";
          }));

          if(("A"+aa.mtype).indexOf("系统管理员")>0){
          bb.push(buildLine(box,"<i class='fa fa-phone ' style='color:#DDD'></i> <font style='color:#DDD'>手机短信</font>",function(){
             document.location.href="telmsg.php";
          }));
		  }

          bb.push(buildLine(box,"<i class='fa fa-calendar ' style='color:#DDD'></i> <font style='color:#DDD'>操作日志</font>",function(){
             document.location.href="log.php";
          }));
	   }

       /*
	   bb.push(buildLine(box,"<i class='fa fa-home ' style='color:#DDD'></i> <font style='color:#DDD'>返回首页</font>",function(){
            document.location.href="index.php";
       }));
	   */
	   try{bb[bb.length-1].style.borderBottom="0px none";}catch(e){}
	});

	//右边
	var mr=buildDH("",function(div,lab,box){
       var bb=[];
	   div.style.left="auto";
	   div.style.right="0%";

	   lab.innerHTML=("<a ><i id='i_mine' class='fa fa-th-large'></i><hr><font style='color:"+pubfc+"'>用户中心</font></a>");

	   if(("A"+aa.mtype).indexOf("老板")>0){
           bb.push(buildLine(box,"<i class='fa fa-sitemap' style='color:#DDD'></i> <font style='color:#DDD'>员工注册</font>",function(){
              document.location.href="/9766/pub/wxpay/bdlogin.php";
           }));
	   }

	   if(("A"+aa.mtype).indexOf("老板")>0||("A"+aa.mtype).indexOf("员工")>0){
           bb.push(buildLine(box,"<i class='fa fa-dedent' style='color:#DDD'></i> <font style='color:#DDD'>我的报单</font>",function(){
              document.location.href="myord.php";
           }));
           bb.push(buildLine(box,"<i class='fa fa-line-chart' style='color:#DDD'></i> <font style='color:#DDD'>我的统计</font>",function(){
              document.location.href="mystat.php";
           }));
	   }
       bb.push(buildLine(box,"<i class='fa fa-newspaper-o' style='color:#DDD'></i> <font style='color:#DDD'>我的资料</font>",function(){
            document.location.href="myinfo.php";
       }));
       bb.push(buildLine(box,"<i class='fa fa-th-large' style='color:#DDD'></i> <font style='color:#DDD'>选择系统</font>",function(){
            document.location.href="selbdsys.php";
       }));

	   try{bb[bb.length-1].style.borderBottom="0px none";}catch(e){}

	});
  }
  //var aa=[];
  init(aa);
}
//------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
function doform(title,msg,callback){
	var _this=this;
	var boxid="doformbox";
	_this.closeme=function(){
		try{document.body.removeChild(document.getElementById(boxid))}catch(e){}
	}
	var ms='';
	ms+='<div class="fade"><div class="modal-dialog"><div class="modal-content">';
    ms+='<div class="modal-header"><button type="button" class="close jsclose" data-dismiss="modal" aria-hidden="true">×</button><span class="glyphicon glyphicon-envelope" style="font-size: 20px"> '+title+'</span></div>';
    ms+='<div class="modal-body" style="font-size: 14px;"><div style="text-align: center;margin-top:0px;" id="doformcont">'+msg+'</div></div>';
    ms+='<div class="modal-footer"><button type="button" class="btn btn-default jsclose" data-dismiss="modal">关闭</button></div></div></div></div>';
	_this.closeme();
	var div=document.createElement("div");
	div.id=boxid;
	div.className="modal";
	div.innerHTML=ms;
	document.body.appendChild(div);
	_this.box=div;
	$(div).show();
	$(".jsclose").unbind().click(function(){
		_this.closeme();
	});
	try{callback(_this);}catch(e){}
}


function selform(msg,callback){
	var _this=this;
	var boxid="selformbox";
	_this.closeme=function(){
		try{document.body.removeChild(document.getElementById(boxid))}catch(e){}
	}
	var ms='';
	ms+='<div class="fade" style="max-height:90%"><div class="modal-dialog" style="height:95%"><div class="modal-content" style="height:98%;overflow-y:auto">';
    ms+='<div class="modal-body" style="font-size: 14px;"><div style="text-align: center;margin-top:0px;" id="doformcont">'+msg+'</div></div>';
    ms+='</div></div></div>';
	_this.closeme();
	var div=document.createElement("div");
	div.id=boxid;
	div.className="modal";
	div.innerHTML=ms;
	document.body.appendChild(div);
	_this.box=div;
	$(div).show();
	try{callback(_this);}catch(e){}
}

//------------------------------------------------------------------------------------------
function showconfirm(title,msg,callback,yesfun){
	var _thisaa=this;
	var boxid="showconfirmbox";
	_thisaa.closemeaa=function(){
		try{document.body.removeChild(document.getElementById(boxid))}catch(e){}
	}
	var ms='';
	ms+='<div class="fade"><div class="modal-dialog"><div class="modal-content">';
    ms+='<div class="modal-header"><button type="button" class="close jscloseaa" data-dismiss="modal" aria-hidden="true">×</button><span class="glyphicon glyphicon-envelope" style="font-size: 20px"> '+title+'</span></div>';
    ms+='<div class="modal-body" style="font-size: 14px;"><div style="text-align: center;margin-top:0px;" id="showconfirmcont">'+msg+'</div></div>';
    ms+='<div class="modal-footer">';
	ms+='<span class="btn btn-warning" id="showconfirm_cmdyes">确定</span>&nbsp;';
	ms+='<span class="btn btn-black jscloseaa">关闭</span>';
	ms+='</div>';
	ms+='</div></div></div>';
	_thisaa.closemeaa();
	var div=document.createElement("div");
	div.id=boxid;
	div.className="modal";
	div.innerHTML=ms;
	document.body.appendChild(div);
	_thisaa.box=div;
	$(div).show();
	$(".jscloseaa").unbind().click(function(){
		_thisaa.closemeaa();
	});
	try{callback(_thisaa);}catch(e){}
	$("#showconfirm_cmdyes").unbind().click(function(){
	    try{yesfun(_thisaa);}catch(e){}
	});
}	

function alllogin(callback){
        var  ms='<div style="text-align:left">';
       	ms+='<input type="text" class="txt" id="txt_userid" placeholder="用户ID">';
        ms+='<input type="password" class="txt" id="txt_pwd" placeholder="登录密码">';
		ms+='</div>';
        ms+='<div style="text-align:center;margin-top:10px">';
        ms+='<span class="cmd" style="background-color:#fd4c17" id="cmd_tologin">登录</span>';
		ms+='</div>';
        ms+='<div style="text-align:center;margin-top:25px">';
        ms+='<a href="findpwd.html" ><i class="fa fa-question-circle"></i> 忘记密码</a>';
		ms+='</div>';
	    doform("请登录",ms,function(_s){	
			 $("#txt_userid").val(getCookie("auto_uid"));
			 $("#txt_pwd").val(getCookie("auto_pwd"));
			 $("#cmd_tologin").unbind().click(function(){
			             var uid=encodeURIComponent($("#txt_userid").val());
			             var pwd=encodeURIComponent($("#txt_pwd").val());
			             $.get("../emadmin/ajaxmob.php?dowhat=tologin&u="+uid+"&p="+pwd,function(data,stat){
				           var aa=data.split(",");
				           if(aa[0]=="ok"){
							   setCookie("auto_uid",uid,30);
							   setCookie("auto_pwd",pwd,30);
                               document.location.reload();
				           }else{
					           alertem(aa[1],"登录失败");
				           }
			             });
						 
			 });
	 });
}



function setfindprdbykey(){
	function showprdtip(obj){
		var divid="prdtipid";
		try{document.body.removeChild(document.getElementById(divid));}catch(e){}
	    $(window).click(function(e){
		   try{document.body.removeChild(document.getElementById(divid));}catch(e){}
	    });

		if($(obj).val()=="")return;
		$.get("/168m/asp/ajaxfun.asp?dowhat=getprdfindlist&key="+encodeURIComponent(encodeURIComponent($(obj).val())),function(data,stat){
			data=data.replace(/\r\n/gi,"");
			if(data==""){
				return;
			}
			try{clearTimeout(window.showprdtipret);}catch(e){}
			window.showprdtipret=setTimeout(function(){
			    showtip(data);
			},100);
		});
		
		function showtip(data){
           try{$(".showprdtipbox").remove()}catch(e){}		   
		   var div=document.createElement("div");
		   div.id=divid;
		   var xy=$(obj).offset();
		   div.className="showprdtipbox";
		   div.style.left=xy.left+"px";
		   div.style.top=(xy.top+$(obj).outerHeight())+"px";
		   div.style.height="auto";
		   div.style.width=$(obj).outerWidth()+"px";
		   div.style.border="1px solid #CCC";
		   div.style.backgroundColor="#FFF";
		   div.style.overflow="hidden";
		   div.style.position="absolute";
		   div.style.overflowY="auto";
		   div.style.maxHeight=300+"px";
		   div.style.zIndex=7777;
		   div.innerHTML="<div style='margin:10px'>"+data+"</div>";
		   document.body.appendChild(div);
		   $(div).click(function(e){
			  e.stopPropagation();
		   });
		   $(".prdline").each(function(ttt,ooo){
			    ooo.onclick=function(){
					//var aa=$(ooo).html().split("]");
					$(obj).val($(ooo).attr("datano1"));
					$("#cmdfind").click();
				};
		   });
		}
		
	}
	$("#txtkey").bind("input propertychange click",function(e){
	    e.stopPropagation();
		showprdtip(document.getElementById("txtkey"));
	});
		
}




function setCookie(c_name,value,expiredays){
    var exdate=new Date();
    exdate.setDate(exdate.getDate()+expiredays);
    document.cookie=c_name+ "=" +escape(value)+((expiredays==null) ? "" : ";expires="+exdate.toGMTString());
}   
function getCookie(c_name){
   if (document.cookie.length>0) {
      c_start=document.cookie.indexOf(c_name + "=")
      if (c_start!=-1){ 
         c_start=c_start + c_name.length+1 
         c_end=document.cookie.indexOf(";",c_start);
         if (c_end==-1) c_end=document.cookie.length;
         return unescape(document.cookie.substring(c_start,c_end));
      } 
   }
   return "";
}
function delCookie(name){
   var exp = new Date();
   exp.setTime(exp.getTime() - 1);
   var cval=getCookie(name);
   if(cval!=null)document.cookie= name + "="+cval+";expires="+exp.toGMTString();
}	
//------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------

function loading(){
         var divid="loading_id";
		 try{document.body.removeChild(document.getElementById(divid))}catch(e){}
		 var div=document.createElement("div");
		 div.id=divid;
		 div.style.position="fixed";
		 div.style.left="0px";
		 div.style.top="0px";
		 div.style.width="100%";
		 div.style.height="100%";
		 div.style.zIndex=999999;
		 div.style.backgroundColor="#000";
         try{div.style.filter="alpha(opacity=80)";}catch(e){}
	     try{div.style.opacity=0.8;}catch(e){}
		 div.style.backgroundRepeat="no-repeat";
		 div.style.backgroundPosition="center center";
		 div.style.backgroundSize="50px 50px";
		 div.style.backgroundImage="url(/9766/pub/images/load1.gif)";
		 document.body.appendChild(div);
}
function unloading(){
         var divid="loading_id";
		 try{document.body.removeChild(document.getElementById(divid))}catch(e){}
}
//------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------
