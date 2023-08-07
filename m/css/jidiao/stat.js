
function setseldiary(ms,fag){
	var mdate=new Date();
			var opt={};
			opt.date = {preset : 'date'};
			opt.datetime = {preset : 'datetime'};
			opt.time = {preset : 'time'};
			opt.default = {
				preset:fag,
				theme: 'android-ics light', //皮肤样式
		        display: 'modal', //显示方式 
		        mode: 'scroller', //日期选择模式
				dateFormat: 'yy-mm-dd',
				lang: 'zh',
				showNow: false,
				nowText: "今天",
		        startYear: 2019, //开始年份
		        endYear: mdate.getFullYear()+1 //结束年份
			};
		  	$(ms).mobiscroll($.extend(opt['date'], opt['default']));
}

function shopinit(){
	var orderby="";
    function showform(mtitle,ms,callback){
		        var nowo=null;
				var divid="movetop_form_id";
				try{document.body.removeChild(document.getElementById(divid))}catch(e){}
				var div=document.createElement("div");
				div.id=divid;
				div.style.position="fixed";
				div.style.width="100%";
				div.style.height="100%";
				div.style.backgroundColor="#FFF";
				div.style.zIndex=888888;
				div.style.left=0+"px";
				div.style.top="100%";
				var c=document.createElement("div");
				c.style.position="absolute";
				c.style.width=30+"px";
				c.style.height=30+"px";
				c.style.lineHeight=28+"px";
				try{c.style.borderRadius=30+"px";}catch(e){}
				c.style.textAlign="center";
				c.style.fontSize=24+"px";
				c.style.color="#999";
				c.innerHTML="&times;";
				c.style.top=0+"px";
				c.style.right=0+"px";
				c.style.backgroundColor="#FFF";
				//c.style.border="1px solid #999";
				c.style.padding=0+"px";
				c.style.zIndex=999;
				c.onclick=function(){
				   try{document.body.removeChild(document.getElementById(divid))}catch(e){}
				};
				div.appendChild(c);

				var cap=document.createElement("div");
				cap.style.position="absolute";
				cap.style.left=0+"px";
				cap.style.top=0+"px";
				cap.style.margin=10+"px";
				cap.style.textAlign="left";
				cap.style.color="#333";
				cap.style.fontSize=14+"px";
				cap.innerHTML=mtitle;
				div.appendChild(cap);

				var cont=document.createElement("div");
				cont.style.marginTop=30+"px";
				cont.style.backgroundColor="#feffe9";
				cont.style.width="100%";
				cont.style.overflowY="scroll";
				cont.style.position="relative";
				//cont.style.left=0+"px";
				//cont.style.bottom=0+"px";
				cont.style.borderTop="1px solid #999";
				
				div.appendChild(cont);
				document.body.appendChild(div);
				cont.style.height=($(div).innerHeight()-35)+"px";
				$(div).animate({"top":"0"});
				cont.innerHTML=ms;

				var nowo=null;
				$(div).find("input").focus(function(){
					nowo=this;
				    //nowo.scrollIntoView(true);
				});
				$(window).resize(function(){
				    cont.style.height=($(div).innerHeight()-35)+"px";
					if(nowo){
					   var xy=$(nowo).position();
					   var top2=xy.top+$(nowo).outerHeight();
					   if(top2>$(cont).innerHeight()){
						   //$(cont).animate({"scrollTop":top2+"px"});
						   try{clearTimeout(window.srollret)}catch(e){}
						   window.scrollret=setTimeout(function(){
						      $(cont).scrollTop(top2);
						   },100);
					   }
					}
				});

				
				try{callback(div,c)}catch(e){}
	}		
	//---------------------------------------------------------------------------------------------------
	$(".tckey").each(function(ttt,ooo){
         ooo.onclick=function(){
			 $("#sx_tckey").val($(ooo).html());
		 };
	});
	$("#tckeycls").unbind().click(function(){
		$("#sx_tckey").val("");
		$("#txtStartTime1").val("");
		$("#txtStartTime2").val("");
		$("#txtStartHM").val("");
		$("#sx_date1").val("");
		$("#sx_date2").val("");
		$("#RadioButton1").prop("checked", 'checked');
		$("#sx_ykyes option:first").prop("selected", 'selected');
		$("#ddlUserLevel option:first").prop("selected", 'selected');
		$("#txtSelValue option:first").prop("selected", 'selected');
		$("#txtProductName option:first").prop("selected", 'selected');
		$("#ddlProperties option:first").prop("selected", 'selected');
		$("#txtProperties").empty();
	});
	$("#telcls").unbind().click(function(){
		$("#sx_tel").val("");
	});
	$("#notecls").unbind().click(function(){
		$("#sx_note").val("");
	});
	$(".orderby").each(function(t1,o1){
		o1.onclick=function(){
			$(".orderby").removeClass("opton");
			$(o1).addClass("opton");
			orderby=$(o1).attr("data");
			$("#cmdtj").click();
		};
	});
	//---------------------------------------------------------------------------------------------------
	$("#cmdtoday").unbind().click(function(){
		var dd=new Date();
		var ms1="",ms2="";
		var v=dd.getMonth()+1;
		if(v<10){
			ms1="0"+v.toString();
		}else{
			ms1=v.toString();
		}
		v=dd.getDate();
		if(v<10){
			ms2="0"+v.toString();
		}else{
			ms2=v.toString();
		}

		$("#sx_date1").val(dd.getFullYear()+"-"+ms1+"-"+ms2).trigger("change");
		$("#sx_date2").val($("#sx_date1").val()).trigger("change");
	});

	$("#cmdyestoday").unbind().click(function(){
		var dd=new Date();
		dd.setDate(dd.getDate()-1);
		var ms1="",ms2="";
		var v=dd.getMonth()+1;
		if(v<10){
			ms1="0"+v.toString();
		}else{
			ms1=v.toString();
		}
		v=dd.getDate();
		if(v<10){
			ms2="0"+v.toString();
		}else{
			ms2=v.toString();
		}

		$("#sx_date1").val(dd.getFullYear()+"-"+ms1+"-"+ms2).trigger("change");
		$("#sx_date2").val($("#sx_date1").val()).trigger("change");
	});

	$("#cmdtomorrow").unbind().click(function(){
		var dd=new Date();
		dd.setDate(dd.getDate()+1);
		var ms1="",ms2="";
		var v=dd.getMonth()+1;
		if(v<10){
			ms1="0"+v.toString();
		}else{
			ms1=v.toString();
		}
		v=dd.getDate();
		if(v<10){
			ms2="0"+v.toString();
		}else{
			ms2=v.toString();
		}

		$("#sx_date1").val(dd.getFullYear()+"-"+ms1+"-"+ms2).trigger("change");
		$("#sx_date2").val($("#sx_date1").val()).trigger("change");
	});

	//---------------------------------------------------------------------------------------------------
    function getplaytime(objstr,tcid,callback){
        var sightid=val($(".selectsighton").attr("dataid"));
		clearTimeout(window.getplaytimeret);
		window.getplaytimeret=setTimeout(function(){
		  $.get("/9766/pub/php/ajaxbd.php?dowhat=getydtime&sightid="+sightid+"&tcid="+tcid+"&date1="+$("#sx_date1").val()+"&date2="+$("#sx_date2").val(),function(data,stat){
			data=data.replace(/\r\n/,"");
			$(objstr).html(data);
			$(objstr).find(".ydtime").each(function(ttt,ooo){
				ooo.onclick=function(){
					$(objstr).find(".ydtimesel").removeClass("ydtimesel");
					$(ooo).addClass("ydtimesel");
					try{callback(ooo)}catch(e){}
				};
				if(ttt==0)ooo.click();
			});
		  });
		},200);
	}
    function getplaytime1(objstr,tcid,callback){
		clearTimeout(window.getplaytimeret1);
		window.getplaytimeret1=setTimeout(function(){
		  $.get("/9766/pub/php/ajaxbd.php?dowhat=getydtime1&tcid="+tcid+"&date1="+$("#sx_date1").val()+"&date2="+$("#sx_date2").val(),function(data,stat){
			data=data.replace(/\r\n/,"");
			$(objstr).html(data);
			$(objstr).find(".ydtime").each(function(ttt,ooo){
				ooo.onclick=function(){
					$(objstr).find(".ydtimesel").removeClass("ydtimesel");
					$(ooo).addClass("ydtimesel");
					try{callback(ooo)}catch(e){}
				};
				if(ttt==0)ooo.click();
			});
		  });
		},200);
	}

	//---------------------------------------------------------------------------------------------------
	setseldiary("#sx_date1","date");
	setseldiary("#sx_date2", "date");
	setseldiary("#txtStartTime1", "date");
	setseldiary("#txtStartTime2", "date");
	setseldiary("#txtStartHM", "time");
	$("#sx_date1").change(function(){
		$("#sx_date2").val($("#sx_date1").val()).trigger("change");
		getplaytime("#sel_playtime",$("#sx_tcid").val(),function(){
        });
		getplaytime1("#sel_playtime1",$("#sx_tcid1").val(),function(){
        });
		
	});

	$("#sx_date2").change(function(){
		getplaytime("#sel_playtime",$("#sx_tcid").val(),function(){
        });
		getplaytime1("#sel_playtime1",$("#sx_tcid1").val(),function(){
        });
		
	});
	$("#txtStartTime1").change(function () {
		$("#txtStartTime2").val($("#txtStartTime1").val()).trigger("change");
	});
	//---------------------------------------------------------------------------------------------------
	function getsxtclist1(){
		return;
		
		$.get("/9766/pub/php/ajaxbd.php?dowhat=getsxtclist1"+"&tcid="+$("#sx_tcid").val(),function(data,stat){
			$("#sel_tcid1").html(data);
	        $("#sx_tcid1").change(function(){
			   getplaytime1("#sel_playtime1",$("#sx_tcid1").val(),function(){
	           });
			});
			getplaytime1("#sel_playtime1",$("#sx_tcid1").val(),function(){
	        });
		});
	}
	//---------------------------------------------------------------------------------------------------
	function getsxtclist(callback){
		$.get("/9766/pub/php/ajaxbd.php?dowhat=getsxtclist",function(data,stat){
			$("#sel_tcid").html(data);
	        $("#sx_tcid").change(function(){
			   getplaytime("#sel_playtime",$("#sx_tcid").val(),function(){
	           });
			   getsxtclist1();
			});
			getplaytime("#sel_playtime",$("#sx_tcid").val(),function(){
	        });
			try{callback()}catch(e){}
		});
	}

	function getsxsightlist(callback){
		$.get("/9766/pub/php/ajaxbd.php?dowhat=getsxsightlist",function(data,stat){
			$("#sel_sight").html(data);
	        $("#sx_sight").change(function(){

			});
			try{callback()}catch(e){}
		});
	}


	/*
	getsxtclist(function(){
	    getsxtclist1();
	});
	*/
	function getbdlist(){
		$.get("/9766/pub/php/ajaxbd.php?dowhat=getsxbdlist&date1="+$("#sx_date1").val()+"&date2="+$("#sx_date2").val(),function(data,stat){
			if(data.replace(/\r\n/gi,"")==""){
				$("#sxline_bdname").hide();
				$("#sxline_sale").hide();
				$("#sxline_pay").hide();
			}else{
			    $("#sel_bdname").html(data);
			}
		});
	}
	//getbdlist();
	
	function setsxcmd(){
		$.get("/9766/pub/php/ajaxbd.php?dowhat=setsxcmd",function(data,stat){
			if(data.replace(/\r\n/gi,"")=="ok"){
			}else{
				$("#sxline_bdname").hide();
				$("#sxline_sale").hide();
				$("#sxline_pay").hide();
			}
		});
		
		$("#sel_bdname").unbind().click(function(){
			var ms='<div id="sxbdlines" style="margin-top:10px"></div>';
			showform("选择报单者",ms,function(cont,c){
				var sel=$("#sel_bdname").attr("data");
				$.get("/9766/pub/php/ajaxbd.php?dowhat=sxbdlines&sel="+encodeURIComponent(sel)+"&d1="+$("#sx_date1").val()+"&d2="+$("#sx_date2").val(),function(data,stat){
					$("#sxbdlines").html(data);
					$("#sxbdlines").find(".chkonebd").each(function(t1,o1){
						 o1.onclick=function(){
							 if($(o1).hasClass("chked")){
								 $(o1).removeClass("chked");
							 }else{
								 $(o1).addClass("chked");
							 }
							 $("#sxbdlines").find(".chkallbd").removeClass("chked");
							 var aa=$("#sxbdlines").find(".chked");
							 var tot=aa.length;
							 if(tot>0){
								 var s1="";
								 var s2="";
								 for(var t=0;t<tot;t++){
									 if(s1==""){
										 s1=$(aa[t]).html();
										 s2=$(aa[t]).attr("datawxid");
									 }else{
										 s1+=","+$(aa[t]).html();
										 s2+=","+$(aa[t]).attr("datawxid");
									 }
								 }
							     $("#sel_bdname").html(s1).attr("data",s2);
							 }
						 };
					});
					$("#sxbdlines").find(".chkallbd").unbind().click(function(){
						if($("#sxbdlines").find(".chkallbd").hasClass("chked")){
							$("#sxbdlines").find(".chkallbd").removeClass("chked");
							$("#sxbdlines").find(".chkonebd").removeClass("chked");
							//全不选
						}else{
							$("#sxbdlines").find(".chkallbd").addClass("chked");
							$("#sxbdlines").find(".chkonebd").addClass("chked");
							//全选
							$("#sel_bdname").html("全部").attr("data","");
						}
					});
				});
			});
		});
		

		$("#sel_users").unbind().click(function(){
			var ms='<div id="sxbdlines" style="margin-top:10px"></div>';
			showform("选择用户组",ms,function(cont,c){
				var sel=$("#sel_users").attr("data");
				$.get("/9766/pub/php/ajaxbd.php?dowhat=sxuserslines&sel="+encodeURIComponent(sel),function(data,stat){
					$("#sxbdlines").html(data);
					$("#sxbdlines").find(".chkonebd").each(function(t1,o1){
						 o1.onclick=function(){
							 if($(o1).hasClass("chked")){
								 $(o1).removeClass("chked");
							 }else{
								 $(o1).addClass("chked");
							 }
							 $("#sxbdlines").find(".chkallbd").removeClass("chked");
							 var aa=$("#sxbdlines").find(".chked");
							 var tot=aa.length;
							 if(tot>0){
								 var s1="";
								 var s2="";
								 for(var t=0;t<tot;t++){
									 if(s1==""){
										 s1=$(aa[t]).html();
										 s2=$(aa[t]).attr("dataid");
									 }else{
										 s1+=","+$(aa[t]).html();
										 s2+=","+$(aa[t]).attr("dataid");
									 }
								 }
							     $("#sel_users").html(s1).attr("data",s2);
							 }
						 };
					});
					$("#sxbdlines").find(".chkallbd").unbind().click(function(){
						if($("#sxbdlines").find(".chkallbd").hasClass("chked")){
							$("#sxbdlines").find(".chkallbd").removeClass("chked");
							$("#sxbdlines").find(".chkonebd").removeClass("chked");
							//全不选
						}else{
							$("#sxbdlines").find(".chkallbd").addClass("chked");
							$("#sxbdlines").find(".chkonebd").addClass("chked");
							//全选
							$("#sel_users").html("全部").attr("data","");
						}
					});
				});
			});
		});


		$("#sel_tcid").unbind().click(function(){
			var ms='<div id="sxtclines" style="margin-top:10px"></div>';
			showform("选择套餐",ms,function(cont,c){
				var sel=$("#sel_tcid").attr("data");
				var sight=0;
				//sight=$("#sel_sight").attr("data");
				sight=val($(".selectsighton").attr("dataid"));
				
				$.get("/9766/pub/php/ajaxbd.php?dowhat=moresxtc&sel="+encodeURIComponent(sel)+"&sight="+sight,function(data,stat){
					$("#sxtclines").html(data);
					$("#sxtclines").find(".chkonebd").each(function(t1,o1){
						 o1.onclick=function(){
							 if($(o1).hasClass("chked")){
								 $(o1).removeClass("chked");
							 }else{
								 $(o1).addClass("chked");
							 }
							 $("#sxtclines").find(".chkallbd").removeClass("chked");
							 var aa=$("#sxtclines").find(".chked");
							 var tot=aa.length;
							 if(tot>0){
								 var s1="";
								 var s2="";
								 for(var t=0;t<tot;t++){
									 if(s1==""){
										 s1=$(aa[t]).html();
										 s2=$(aa[t]).attr("datatcid");
									 }else{
										 s1+=","+$(aa[t]).html();
										 s2+=","+$(aa[t]).attr("datatcid");
									 }
								 }
							     $("#sel_tcid").html(s1).attr("data",s2);
							 }
						 };
					});
					$("#sxtclines").find(".chkallbd").unbind().click(function(){
						if($("#sxtclines").find(".chkallbd").hasClass("chked")){
							$("#sxtclines").find(".chkallbd").removeClass("chked");
							$("#sxtclines").find(".chkonebd").removeClass("chked");
							//全不选
						}else{
							$("#sxtclines").find(".chkallbd").addClass("chked");
							$("#sxtclines").find(".chkonebd").addClass("chked");
							//全选
							$("#sel_tcid").html("全部套餐").attr("data","");
						}
					});
				});
			});
		});	
		
		
		$("#sel_sight").unbind().click(function(){
			var ms='<div id="sxsightlines" style="margin-top:10px"></div>';
			showform("选择景区",ms,function(cont,c){
				var sel=$("#sel_sight").attr("data");
                setsxsight(sel,false);
			});
		});			
			
		
	}setsxcmd();

	function sx_selectsight(){
		$.get("/9766/pub/php/ajaxbd.php?dowhat=sxselectsight",function(data,stat){
			$("#selectsightbox").html(data);
			$(".selectsight").each(function(t1,o1){
				o1.onclick=function(){
					$(".selectsighton").removeClass("selectsighton");
					$(o1).addClass("selectsighton");
		            getplaytime("#sel_playtime",$("#sx_tcid").val(),function(){});
					if(("A"+$(o1).html()).indexOf("刘三姐")>0){
						$("#tc1").html("普通");
						$("#tc2").html("新贵");
					}else if(("A"+$(o1).html()).indexOf("融创")>0){
						$("#tc1").html("海世");
						$("#tc2").html("水世");
					}else{
						$("#tc1").html("观众");
						$("#tc2").html("贵宾");
					}

				};
				if(t1==1)o1.click();
			});
		});
	}sx_selectsight();


	
	function setsxsight(sel,fag){
		     if(fag){
				 var div=document.createElement("div");
				 div.id="sxsightlines";
				 document.body.appendChild(div);
			 }
				$.get("/9766/pub/php/ajaxbd.php?dowhat=moresxsight&sel="+encodeURIComponent(sel),function(data,stat){
					$("#sxsightlines").html(data);
					$("#sxsightlines").find(".chkonebd").each(function(t1,o1){
						 o1.onclick=function(){
							 /*
							 if($(o1).hasClass("chked")){
								 $(o1).removeClass("chked");
							 }else{
								 $(o1).addClass("chked");
							 }
							 */
							 $("#sxsightlines").find(".chked").removeClass("chked");
							 $(o1).addClass("chked");
							 
							 $("#sxsightlines").find(".chkallbd").removeClass("chked");
							 var aa=$("#sxsightlines").find(".chked");
							 var tot=aa.length;
							 if(tot>0){
								 var s1="";
								 var s2="";
								 for(var t=0;t<tot;t++){
									 if(s1==""){
										 s1=$(aa[t]).html();
										 s2=$(aa[t]).attr("dataid");
									 }else{
										 s1+=","+$(aa[t]).html();
										 s2+=","+$(aa[t]).attr("dataid");
									 }
								 }
							     $("#sel_sight").html(s1).attr("data",s2);
							 }
						 };
						 if(fag){
							 if(t1==0)o1.click();
							 $("#sxsightlines").remove();
						 }
					});
					$("#sxsightlines").find(".chkallbd").unbind().click(function(){
						if($("#sxsightlines").find(".chkallbd").hasClass("chked")){
							$("#sxsightlines").find(".chkallbd").removeClass("chked");
							$("#sxsightlines").find(".chkonebd").removeClass("chked");
							//全不选
						}else{
							$("#sxsightlines").find(".chkallbd").addClass("chked");
							$("#sxsightlines").find(".chkonebd").addClass("chked");
							//全选
							$("#sel_sight").html("全部景区").attr("data","");
						}
					});
				});		
	}
    
	//setsxsight("",true);
	
	//---------------------------------------------------------------------------------------------------
	function allstat(){
		var nowtop=$(window).scrollTop();
		$("#divlist").html("ready...");
		//var bdwxid=$("#sx_bdname").val();
		var bdwxid="";
		var playtime=$("#sel_playtime").find(".ydtimesel").attr("datatime");
		var playtimestr=$("#sel_playtime").find(".ydtimesel").attr("datatimestr");
		var cc=$("#sel_playtime").find(".ydtimesel").attr("datacc");
		var my=$("#hid_my").val();
		var cckey=$("#sx_cckey").val();
		var tckey=$("#sx_tckey").val();

		var playtime1=$("#sel_playtime1").find(".ydtimesel").attr("datatime");
		var playtimestr1=$("#sel_playtime1").find(".ydtimesel").attr("datatimestr");
		var cc1=$("#sel_playtime1").find(".ydtimesel").attr("datacc");
		
		var bds=encodeURIComponent($("#sel_bdname").attr("data"));
		var tcs=encodeURIComponent($("#sel_tcid").attr("data"));
		//var sights=encodeURIComponent($("#sel_sight").attr("data"));
		var sights=val($(".selectsighton").attr("dataid"));
		var users=encodeURIComponent($("#sel_users").attr("data"));
		
		loading();
		
		$.get("/9766/pub/php/ajaxbd.php?dowhat=allstat&my="+my+"&date1="+$("#sx_date1").val()+"&date2="+$("#sx_date2").val()+"&tcid="+$("#sx_tcid").val()+"&tcid1="+$("#sx_tcid1").val()+"&playtime="+playtime+"&playtimestr="+encodeURIComponent(playtimestr)+"&cc="+cc+"&bds="+bds+"&bdwxid="+encodeURIComponent(bdwxid)+"&cckey="+encodeURIComponent(cckey)+"&tckey="+encodeURIComponent(tckey)+"&playtime1="+playtime1+"&playtimestr1="+encodeURIComponent(playtimestr1)+"&cc1="+cc1+"&sale="+$("#sx_sale").val()+"&sxth="+$("#sx_th").val()+"&pay="+$("#sx_pay").val()+"&tcs="+tcs+"&tel="+$("#sx_tel").val()+"&ykyes="+$("#sx_ykyes").val()+"&dyout="+$("#sx_dyout").val()+"&orderby="+orderby+"&note="+encodeURIComponent($("#sx_note").val())+"&sights="+sights+"&users="+users,function(data,stat){
			unloading();
			$("#divlist").html(data);
			$(window).scrollTop(nowtop);
			$("#trhead").html($("#trfoot").html());
			
			$("#makexls").unbind().click(function(){
				showconfirm("请注意","你确定要将当前统计生成电子表格吗?",function(_s){
				},function(_s){
					_s.closemeaa();
					loading();
					$.get("/9766/newadmin/ajaxexcel.php?dowhat=bdordstat&my="+my+"&date1="+$("#sx_date1").val()+"&date2="+$("#sx_date2").val()+"&tcid="+$("#sx_tcid").val()+"&tcid1="+$("#sx_tcid1").val()+"&playtime="+playtime+"&playtimestr="+encodeURIComponent(playtimestr)+"&cc="+cc+"&bds="+bds+"&bdwxid="+encodeURIComponent(bdwxid)+"&cckey="+encodeURIComponent(cckey)+"&tckey="+encodeURIComponent(tckey)+"&playtime1="+playtime1+"&playtimestr1="+encodeURIComponent(playtimestr1)+"&cc1="+cc1+"&sale="+$("#sx_sale").val()+"&sxth="+$("#sx_th").val()+"&pay="+$("#sx_pay").val()+"&tcs="+tcs+"&tel="+$("#sx_tel").val()+"&ykyes="+$("#sx_ykyes").val()+"&dyout="+$("#sx_dyout").val()+"&orderby="+orderby+"&note="+encodeURIComponent($("#sx_note").val())+"&sights="+sights+"&users="+users,function(data,stat){
						unloading();
						selformclose("<div style='padding-top:10px;font-size:16px'>电子表格已成功生成！<br><br><a href='/9766/newadmin/rpt/"+data+"' style='color:#39C'>点击打开电子表格</a></div>",null);
					});
				});
						
			});
	
	
//------------按钮开始----------------------	
            $(".sfzcopy").each(function(t1,o1){
                o1.onclick=function(){
				   const range = document.createRange();
				   range.selectNode($(o1).find(".sfzinfo1")[0]);
				   const selection = window.getSelection();
				   if (selection.rangeCount > 0) selection.removeAllRanges();
				   selection.addRange(range);
				   document.execCommand('copy');
				   sysmsg("复制成功")
					
				};
			});

            $("#batedit").unbind().click(function(){
				   var ms='';
	  
				   ms+='<div style="margin:10px;display:">';
				   ms+='全票单价调整 <input type="text" id="txt_p1" class="txt1" style="width:100px;margin-top:5px" value=""/>&#12288;';
				   ms+='<span id="cmdchg" class="btn btn-success">保存更改</span>';
				   ms+='</div>';
	  
				   doform("批量调价",ms,function(_s){
							 $("#cmdchg").unbind().click(function(){
								 //更改订单
								 var aa=$(".opt_selon");
								 var ss="";
								 var tot=aa.length;
								 for(var t=0;t<tot;t++){
									 if(ss==""){
										 ss=$(aa[t]).attr("dataid");
									 }else{
										 ss+=","+$(aa[t]).attr("dataid");
									 }
								 }
								 $.get("/9766/pub/php/ajaxbd.php?dowhat=batchgmanp&sel="+ss+"&manp="+$("#txt_p1").val(),function(data,stat){
									  data=data.replace(/\r\n/gi,"").split(",");
									  if(data=="ok"){
									      _s.closeme();
										  sysmsg("更改成功!");
										  $.get("/9766/pub/wxpay/bdmsg.php?dowhat=batordchgmsg&sel="+ss,function(data,stat){});
										  allstat();
									  }else{
										  sysmsg("未做更改!");
									  }
								 });
							 });
					   
				   });				
			});

			$(".opt_sel").each(function(ttt,ooo){
				ooo.onclick=function(){
					if($(ooo).hasClass("opt_selon")){
						$(ooo).removeClass("opt_selon");
					}else{
						$(ooo).addClass("opt_selon");
					}
				};
			});

			$(".qsms").each(function(ttt,ooo){
				ooo.onclick=function(){
                  showconfirm("请注意","你确定要向游客发送云短信吗？",null,function(_s){					
					  loading();
					  $.get("/9766/pub/qsms/ajax.php?dowhat=bdmsg_zh&id="+$(ooo).attr("dataid"),function(data,stat){
						  unloading();
						  eval("var aa="+data);
						  if(aa.msg=="ok"){
							  $(ooo).css({"color":"#093","border":"1px solid #093"});
						  }
						  _s.closemeaa();
					  })
				  });
				};
			});
			$(".qsms_zf").each(function(ttt,ooo){
				ooo.onclick=function(){
					var ms='';
					ms+='手机号：<input type="number" id="zf_tel" class="txt" maxlength=11 style="width:150px" placeholder="输入手机号"/> <span id="cmd_zf" class="cmd" style="width:auto;padding-left:10px;padding-right:10px">发送信息</span>';
					doform("云短信转发",ms,function(_s){
						$("#cmd_zf").unbind().click(function(){
							if($("#zf_tel").val().trim()==""){
								sysmsg("请输入手机号");
								return;
							}
			                $.get("/9766/pub/qsms/ajax.php?dowhat=telbdmsg_zh&id="+$(ooo).attr("dataid")+"&tel="+$("#zf_tel").val(),function(data,stat){});
						    _s.closeme();
						});
					});
				};
			});

			
			

			$(".mobmsg").each(function(ttt,ooo){
				ooo.onclick=function(){
					var ms='';
					ms+='<div id="div_mobmsg"></div>';
					ms+='<div id="sendlist" style="margin-top:10px;border-top:1px solid #999;padding-top:10px;text-align:left"></div>';
					doform("短信复制",ms,function(_s){
						function getsendlist(){
							$.get("/9766/pub/php/ajaxbd.php?dowhat=getsendlist&id="+$(ooo).attr("dataid"),function(data,stat){
								$("#sendlist").html(data);
							});
						}getsendlist();
						$.get("/9766/pub/php/ajaxbd.php?dowhat=getmobmsglist&id="+$(ooo).attr("dataid"),function(data,stat){
							$("#div_mobmsg").html(data);

							var clipboard = new Clipboard('.copyBtn').on('success', function(e) {
							   var e = e || window.event;
							   //console.log(e);
							   sysmsg("复制成功");
							   //setit();
							}).on('error', function(e) {
								sysmsg("复制失败");
								//setit()
							});
							
							
							$(".copyBtn").click(function(){
								setit();
							});
							
							$("#nonesend").unbind().click(function(){
									$.get("/9766/pub/php/ajaxbd.php?dowhat=nonesend&id="+$(ooo).attr("dataid"),function(data,stat){
										$(ooo).html("短信").css({"color":"#999","border":"1px solid #999"});
										sysmsg("已设为未发状态");
										getsendlist();
									});
							});
							
							function setit(){
								//clearTimeout(window.setit_ret);
								//window.setit_ret=setTimeout(function(){
									if(window.setit_ret){
										return;
									}
									window.setit_ret=99;
									$.get("/9766/pub/php/ajaxbd.php?dowhat=setbdmobmsg&id="+$(ooo).attr("dataid"),function(data,stat){
										window.setit_ret=0;
										$(ooo).html("已发").css({"color":"#093","border":"1px solid #093"});
										getsendlist();
									});
								//},500);
							}
							
						});
					});
				};
			});



			$(".setdyout").each(function(t1,o1){
				o1.onclick=function(){
					var ms='';
					ms+='<div style="font-size:14px;font-weight:bold;padding:10px">备注说明</div>';
					ms+='<div id="dyoutinfo" style="text-align:left;font-size:14px;padding:10px"></div>';
					ms+='<textarea id="outnote" class="txt" style="height:50px;width:90%"></textarea>';
					ms+='<span class="btn btn-red" id="unout">未给票</span>';
					ms+='<span class="btn btn-blue" id="doout" style="margin-left:30px">已给票</span>';
					ms+='<div style="margin:10px 0px" id="tellog"></div>';
					ms+='<div style="margin:10px 0px">';
					ms+='<span class="btn btn-success" id="teled">已电话</span>';
					ms+='<span class="btn btn-black" id="untel" style="margin-left:30px">未电话</span>';
					ms+='</div>';
					ms+='<div style="margin:10px 0px;font-size:14px;padding-top:10px">';
					ms+=$(o1).attr("dataykname")+"：<a href='tel://"+$(o1).attr("datayktel")+"' style='font-size:16px;color:#39C'><i class='fa fa-phone'></i> "+$(o1).attr("datayktel")+'</a>';
					ms+='</div>';
					doform("设置出票",ms,function(_s){
						function dyoutinfo(){
							$.get("/9766/pub/wxpay/ajaxdy.php?dowhat=dyoutinfo&id="+$(o1).attr("dataid"),function(data,stat){
								$("#dyoutinfo").html(data);
							});
						}dyoutinfo();
						function getoutnote(){
							$.get("/9766/pub/wxpay/ajaxdy.php?dowhat=getoutnote&id="+$(o1).attr("dataid"),function(data,stat){
								$("#outnote").val(data);
							});
						}getoutnote();
						function gettellog(){
							$.get("/9766/pub/wxpay/ajaxdy.php?dowhat=gettellog&id="+$(o1).attr("dataid"),function(data,stat){
								$("#tellog").html(data);
							});
						}gettellog();
						
						$("#unout").unbind().click(function(){
							funout("0");
						});
						$("#doout").unbind().click(function(){
							funout("99");
						});

						$("#teled").unbind().click(function(){
							loading();
							$.get("/9766/pub/wxpay/ajaxdy.php?dowhat=teled&id="+$(o1).attr("dataid")+"&mnote="+encodeURIComponent($("#outnote").val()),function(data,stat){
								//$("#a_tel").attr("href","tel://"+$(o1).attr("datayktel")).click();
								unloading();
								gettellog();
								$(o1).parent().find(".telinfo").html('<span style="display:inline-block;overflow:hidden;width:15px;height:15px;border-radius:15px;text-align:center;line-height:15px;background-color:#093;color:#fff;font-size:12px">电</span>');
							});
						});
						$("#untel").unbind().click(function(){
							loading();
							$.get("/9766/pub/wxpay/ajaxdy.php?dowhat=untel&id="+$(o1).attr("dataid")+"&mnote="+encodeURIComponent($("#outnote").val()),function(data,stat){
								unloading();
								gettellog();
								$(o1).parent().find(".telinfo").html("");
							});
						});

						function funout(sss){
							$.get("/9766/pub/wxpay/ajaxdy.php?dowhat=setdyout&fag="+sss+"&id="+$(o1).attr("dataid")+"&mnote="+encodeURIComponent($("#outnote").val()),function(data,stat){
								if(data.replace(/\r\n/gi,"")=="ok"){
									if(sss=="99"){
										$(o1).removeClass("bdcmdblue").addClass("bdcmdblue").html("已给票");
									}else{
										$(o1).removeClass("bdcmdblue").html("未给票");
									}
								}
								_s.closeme();
							});
						}
					});
				};
			});



			$(".setdyp").each(function(t1,o1){
				o1.onclick=function(){
					var ms='';
			        ms+='<div style="margin:10px">';
			        ms+='导游费 <input type="text" id="dyp" class="txt1" style="width:100px;margin-top:5px" value="'+$(o1).attr("datadyp")+'"/> 元<br>';
			        ms+='导&#12288;游 <textarea id="dys" class="txt1" style="margin-top:5px;vertical-align:top;width:200px;height:100px"></textarea><br>';
					ms+='<div id="havedys" style="margin:5px;margin-left:40px"></div>';
			        ms+='</div>';
	                ms+='<div style="clear:both;overflow:hidden;height:150px"></div>';
                    emform({"msg":ms,"title":"设置导游费"},function(){
						$.get("/9766/pub/php/ajaxbd.php?dowhat=getordinfo&id="+$(o1).attr("dataid"),function(data,stat){
							eval("var aa="+data);
							$("#dys").val(decodeURIComponent(aa.dys));
							$("#dyp").val(aa.dyp);
							var bb=decodeURIComponent(aa.havedys).split(",");
							var s='';
							for(i=0;i<bb.length;i++){
								s+='<span class="getdys" style="padding:5px;border:1px solid #39C;color:#39C;display:inline-block;margin:5px;border-radius:5px">'+bb[i]+'</span>';
							}
							$("#havedys").html(s);
							$(".getdys").each(function(t2,o2){
								o2.onclick=function(){
									var cc=$("#dys").val().split(",");
									if(inArray($(o2).html(),cc)){
									}else{
										var s=$("#dys").val();
										if(s==""){
											s=$(o2).html();
										}else{
											s+=","+$(o2).html();
										}
										$("#dys").val(s);
									}
								};
							});
						});
					},function(){
							$.get("/9766/pub/php/ajaxbd.php?dowhat=savedyp&id="+$(o1).attr("dataid")+"&dyp="+$("#dyp").val()+"&dys="+encodeURIComponent($("#dys").val()),function(data,stat){
								//ordlist(np);
								reflash(o1);
							});
				    });
				};
			});

			$(".saleord").each(function(t1,o1){
				o1.onclick=function(){
					var ms='';
			        ms+='<div style="margin:10px">';
			        ms+='实售价 <input type="text" id="factp" class="txt1" style="width:100px;margin-top:5px;border:0px none" readonly value="'+$(o1).attr("datafactp")+'"/> 元<br>';
			        ms+='转售价 <input type="text" id="salep" class="txt1" style="width:100px;margin-top:5px" value="'+$(o1).attr("datasalep")+'"/> 元<br><br>';
					ms+='计算器 <input type="number" id="zs_c" class="txt1" style="width:60px;margin-top:5px" value="'+$(o1).attr("datac")+'"/> 人 &times; ';
					ms+=' <input type="number" id="zs_p" class="txt1" style="width:60px;margin-top:5px" value=""/> 元<br><br>';
					ms+='备&#12288;注 ';
					ms+='<textarea id="salenote" class="txt1" style="width:200px;height:80px;vertical-align:top;">'+decodeURIComponent($(o1).attr("datasalenote"))+'</textarea>';
					if(val($(o1).attr("dataissale"))==99){
					   ms+='<br>&#12288;&#12288;&#12288;<span class="cmd1" id="notsale">取消转售</span>'
					}
			        ms+='</div>';
	                ms+='<div style="clear:both;overflow:hidden;height:150px"></div>';
                    emform({"msg":ms,"title":"报单转售","yestitle":"设置转售"},function(box){
						
					    $("#zs_c").unbind().bind("input propertychange",function(){
							var c=val($("#zs_c").val());
							var p=val($("#zs_p").val());
							$("#salep").val(p*c);
						});
					    $("#zs_p").unbind().bind("input propertychange",function(){
							var c=val($("#zs_c").val());
							var p=val($("#zs_p").val());
							$("#salep").val(p*c);
						});

						
						$.get("/9766/pub/php/ajaxbd.php?dowhat=getbdinfo&id="+$(o1).attr("dataid"),function(data,stat){
							 eval("var aa="+data);
							 $("#factp").val(aa.factp);
							 $("#salep").val(aa.salep);
							 $("#salenote").val(aa.salenote);
						});
						$("#notsale").unbind().click(function(){
							$.get("/9766/pub/php/ajaxbd.php?dowhat=unsaleord&id="+$(o1).attr("dataid"),function(data,stat){
							   box.off();
							   //ordlist(np);
							   $(o1).css({"color":"#AAA","border":"1px solid #AAA"});
							});
						});
					},function(){
							$.get("/9766/pub/php/ajaxbd.php?dowhat=saleord&id="+$(o1).attr("dataid")+"&salep="+$("#salep").val()+"&salenote="+encodeURIComponent($("#salenote").val()),function(data,stat){
								//ordlist(np);
							   $(o1).css({"color":"#39C","border":"1px solid #39C"});
							});
				    });
				};
			});

			$(".payord").each(function(t1,o1){
				o1.onclick=function(){
					var ms='';
			        ms+='<div style="margin:10px">';
			        ms+='实售价 <input type="text" class="txt1" style="width:100px;margin-top:5px;border:0px none" readonly value="'+$(o1).attr("datafactp")+'"/> 元<br>';
					if(val($(o1).attr("dataissale"))==99){
			           ms+='已转售 <input type="text" class="txt1" style="width:100px;margin-top:5px;border:0px none" readonly value="'+$(o1).attr("datasalep")+'"/> 元<br>';
					}
			        ms+='日&#12288;期 <input type="text" id="paydate" class="txt1" style="width:200px;margin-top:5px;" value="'+$(o1).attr("datapaydate")+'"/> <br>&#12288;&#12288;&#12288; 留空则取当前日期<br>';
					ms+='备&#12288;注 ';
					ms+='<textarea id="paynote" class="txt1" style="width:200px;height:80px;vertical-align:top;margin-top:5px">'+decodeURIComponent($(o1).attr("datapaynote"))+'</textarea>';
					if(val($(o1).attr("dataispay"))==99){
					   ms+='<br>&#12288;&#12288;&#12288;<span class="cmd1" id="notpay">取消结算</span>'
					}
			        ms+='</div>';
	                ms+='<div style="clear:both;overflow:hidden;height:150px"></div>';
                    emform({"msg":ms,"title":"报单结算","yestitle":"设置结算"},function(box){
						setseldiary("#paydate","date")
						$("#notpay").unbind().click(function(){
							$.get("/9766/pub/php/ajaxbd.php?dowhat=unpayord&id="+$(o1).attr("dataid"),function(data,stat){
							   box.off();
							   //ordlist(np);
							   $(o1).css({"color":"#AAA","border":"1px solid #AAA"});
							});
						});
					},function(){
							$.get("/9766/pub/php/ajaxbd.php?dowhat=payord&id="+$(o1).attr("dataid")+"&paynote="+encodeURIComponent($("#paynote").val())+"&paydate="+$("#paydate").val(),function(data,stat){
								//ordlist(np);
							   $(o1).css({"color":"#39C","border":"1px solid #39C"});
							});
				    });
				};
			});
			
			$(".retord").each(function(t1,o1){
				o1.onclick=function(){
					showconfirm("请注意","你确定要退掉此订单吗？",null,function(_s){
						_s.closemeaa();
						loading();
						$.get("/9766/pub/php/ajaxbd.php?dowhat=retord&id="+$(o1).attr("dataid"),function(data,stat){
							unloading();
							data=data.replace(/\r\n/gi,"");
							if(data==""){
								$.get("/9766/pub/wxpay/bdmsg.php?dowhat=retordmsg&id="+$(o1).attr("dataid"),function(data,stat){});

/*
$.ajax({
     async: true,
     type: 'GET',
     url: "/9766/pub/wxpay/bdmsg.php?dowhat=retordmsg&id="+$(o1).attr("dataid"),
     success: function(data) {
          //callback
     }
});
*/								
							    //ordlist(np);
								reflash(o1);
							}else{
							   sysmsg(data);
							}
						});
					});
				};
			});

			$(".delord").each(function(t1,o1){
				o1.onclick=function(){
					showconfirm("请注意","你确定要删除此订单吗？",null,function(_s){
						$.get("/9766/pub/php/ajaxbd.php?dowhat=delord&id="+$(o1).attr("dataid"),function(data,stat){
							_s.closemeaa();
							reflash(o1);
						});
					});
				};
			});

			$(".movesys").each(function(t1,o1){
				o1.onclick=function(){
                    emform({"msg":'<div id="selbdsys"></div>',"title":"选择报单系统"},function(box){

					   $.get("/9766/newadmin/ajaxbd.php?dowhat=selbdsys&bdsys="+$(o1).attr("databdsys"),function(data,stat){
						   $("#selbdsys").html(data);
						   $(".selbdsys").each(function(t2,o2){
							   o2.onclick=function(){
								   $.get("/9766/newadmin/ajaxbd.php?dowhat=setbdsys&id="+$(o1).attr("dataid")+"&bdsys="+$(o2).attr("dataid"),function(data,stat){
									   box.off();
									   reflash(o1);
								   });
							   };
						   });
					   });

                         
					});
				};
			});

			$(".locked").each(function(t1,o1){
			    $(o1).parent().find(".ordcmd").hide();
			});

			$(".lockord").each(function(t1,o1){
				o1.onclick=function(){
					$.get("/9766/pub/php/ajaxbd.php?dowhat=lockord&id="+$(o1).attr("dataid"),function(data,stat){
						if(data.replace(/\r\n/gi,"")=="99"){
						   $(o1).css({"color":"#C00","border":"1px solid #C00"});
						   $(o1).parent().find(".ordcmd").hide();
						}else{
						   $(o1).css({"color":"#999","border":"1px solid #999"});
						   $(o1).parent().find(".ordcmd").show();
						}
						reflash(o1);
					});
				};
			});


			$(".passord").each(function(t1,o1){
				o1.onclick=function(){
					showconfirm("请注意","你要把此订单设为已安排吗？",null,function(_s){
						$.get("/9766/pub/php/ajaxbd.php?dowhat=passord&id="+$(o1).attr("dataid"),function(data,stat){
							_s.closemeaa();
							//ordlist(np);
							$(o1).css({"color":"#093","border":"1px solid #093"});
							reflash(o1);
						});
					});
				};
			});
			$(".unpassord").each(function(t1,o1){
				o1.onclick=function(){
					showconfirm("请注意","你确定把此订单恢复成待安排吗？",null,function(_s){
						$.get("/9766/pub/php/ajaxbd.php?dowhat=unpassord&id="+$(o1).attr("dataid"),function(data,stat){
							_s.closemeaa();
							//ordlist(np);
							$(o1).css({"color":"#999","border":"1px solid #999"});
							reflash(o1);
						});
					});
				};
			});


			$(".chgord").each(function(t1,o1){
				o1.onclick=function(){
				   var ms='';
				   ms+='<div style="margin:10px;font-size:14px;color:#000">';
				   ms+='<font style="font-weight:bold">'+$(o1).find(".ordname").html()+'</font>';
				   ms+='<br>'+$(o1).attr("dataplaydate")+" "+$(o1).find(".playtimestr").html();
				   if($(o1).attr("dataadmin")=="yes"){
					  ms+='<div style="margin:8px 0px">如要更改场次请从下面选择</div>'
					  ms+='<div id="selcc"></div>';
				   }
				   ms+='</div>';
	  
				   ms+='<div style="margin:10px">';
				   ms+='入园日期 <input type="text" id="txt_playdate" class="txt1" style="width:100px;margin-top:5px" value="'+$(o1).attr("dataplaydate")+'"/><br>';
				   ms+='</div>';

				   var dp="";
				   if($("#hid_my").val()=="my"||("A"+window.mytype).indexOf("计调")>0){
					   dp="none";
				   }
	  
				   ms+='<div style="margin:10px;display:'+dp+'">';
				   ms+='全票单价 <input type="text" id="txt_p1" class="txt1" style="width:100px;margin-top:5px" value="'+$(o1).attr("datamanp")+'"/><br>';
				   ms+='优票单价 <input type="text" id="txt_p2" class="txt1" style="width:100px;margin-top:5px" value="'+$(o1).attr("databoyp")+'"/><br>';
				   ms+='</div>';
				   
				   ms+='<div >';

				   ms+='<span style="display:block;width:auto;margin-left:20px;padding:0px">';
				   ms+='<span  class="dj_mor"  style="display:inline-block;vertical-align:middle;position:relative;margin:0px;padding:0px;font-size:12px">全票</span><div class="dj_mor" style="display:inline-block;position:relative;margin:0px;margin-left:10px;padding:0px"><p><span class="cmdaz low az" data="-1">-</span><input class="txtc" data="man" type="text" value="0" style="width:35px;height:33px;vertical-align:middle;margin-top:-3px" ><span class="cmdaz add az" data="1">+</span></p></div> <span id="man_p" style="color:#f63">'+$(o1).attr("datamanp")+'</span>元/位';
				   ms+='</span>';

				   ms+='<div style="clear:both;overflow:hidden;height:15px"></div>';
				   
				   ms+='<span id="ybox" style="display:none;width:auto;padding:0px;margin-left:20px">';
				   ms+='<span  class="dj_mor"  style="display:inline-block;vertical-align:middle;position:relative;margin:0px;padding:0px;font-size:12px">优票</span><div class="dj_mor" style="display:inline-block;position:relative;margin:0px;margin-left:10px;padding:0px;"><p><span class="cmdaz low az" data="-1">-</span><input class="txtc" data="boy" type="text" value="0" style="width:35px;height:33px;vertical-align:middle;margin-top:-2px" ><span class="cmdaz add az" data="1">+</span></p></div> <span id="boy_p" style="color:#f63">'+$(o1).attr("databoyp")+'</span>元/位';
				   ms+='</span>';
				   
				   ms+='<div style="clear:both;overflow:hidden;height:15px"></div>';
				   ms+='<div id="tcboybox" style="display:none;margin-left:20px"></div>';

				   ms+='</div>';
	  
				   ms+='<div style="clear:both;overflow:hidden;height:15px"></div>';
			       ms+='<div style="margin:10px">用户组名 <span id="selusers" datausersid="0"></span></div>';
				   ms+='<div style="clear:both;overflow:hidden;height:15px"></div>';

		  
				   ms+='<div style="margin:10px"><textarea id="mnote" style="height:100px;width:90%" class="txt1" placeholder="备注信息"/>'+$(o1).find(".mnote").html()+'</textarea></div>';
				   ms+='<div style="margin:10px"><span id="cmdget" style="display:block;width:90%;padding:8px 5px;text-align:center;background-color:#39C;color:#fff;border-radius:5px">识别用户信息</span></div>';
				   
				   ms+='<div style="margin:10px">';
				   ms+='游客姓名 <input type="text" id="ykname" class="txt1" style="width:220px;margin-top:5px" value="'+$(o1).find(".ykname").html()+'"/><br>';
				   ms+='手机号码 <input type="text" id="yktel" class="txt1" style="width:220px;margin-top:5px" value="'+$(o1).find(".yktel").html()+'"/><br>';
				   ms+='身份证号 <input type="text" id="yksfz" class="txt1" style="width:220px;margin-top:5px" value="'+$(o1).find(".yksfz").html()+'"/><br>';
				   ms+='</div>';
				   ms+='<div style="clear:both;overflow:hidden;height:150px"></div>';
	  
				   ms+='<div style="text-align:center;margin:0px;width:100%;height:45px;position:fixed;left:0px;bottom:0px;z-index:999;background-color:#fff;border-top:1px solid #CCC;">';
				   ms+='<span style="display:block;overflow:hidden;margin-right:100px;height:45px;line-height:43px;text-align:left;color:#777;font-size:14px">&#12288;总价格 <span style="color:#f63;font-size:18px" id="totp">'+$(o1).attr("datafactp")+'</span> 元 </span>';
				   ms+='<span id="cmdchg" style="position:absolute;right:0px;top:0px;width:100px;height:45px;line-height:43px;display:block;text-align:center;padding:0px;background-color:#093;color:#fff;">保存更改</span>';
				   ms+='</div>';
	  
				   showform("订单更改",ms,function(cont,c){

                          function setpinfo(){
							  var p1=val($(".txtc[data='man']").val())*val($("#txt_p1").val());
							  var p2=val($(".txtc[data='boy']").val())*val($("#txt_p2").val());
							  $("#man_p").html(val($("#txt_p1").val()));
							  $("#boy_p").html(val($("#txt_p2").val()));
							  $("#pinfo_man").html(val($("#txt_p1").val())+" &times; "+val($(".txtc[data='man']").val()));
							  $("#pinfo_boy").html(val($("#txt_p2").val())+" &times; "+val($(".txtc[data='boy']").val()));

							  var a=$(".txtc[data='tcboy']");
							  var tot=a.length;
							  var p3=0;
							  for(var t=0;t<tot;t++){
								 // p3+=val($(a[t]).val())*val($(a[t]).attr("datap"));
								  p3+=val($(a[t]).val())*val($(a[t]).parent().find(".em_tcboyp").val());
								  $(a[t]).parent().find(".pinfo_tcboy").html(val($(a[t]).parent().find(".em_tcboyp").val())+" &times; "+val($(a[t]).val()));
							  }
							  var totp=p1+p2+p3;
							  $("#totp").html(totp);
						  }
						  $(".txtc[data='man']").val($(o1).attr("datamanc"));
						  $(".txtc[data='boy']").val($(o1).attr("databoyc"));
					   $.get("/9766/pub/php/ajaxbd.php?dowhat=getbdordinfo&id="+$(o1).attr("dataid"),function(data,stat){
						   eval("var aa="+data.replace(/\r\n/gi,""));
						   $("#txt_playdate").val(decodeURIComponent(aa.playdate));
						   $("#txt_p1").val(aa.manp);
						   $("#txt_p2").val(aa.boyp);
						   $("#txt_p1").val(aa.manp);
						   $(".txtc[data='man']").val(aa.manc);
						   $(".txtc[data='boy']").val(aa.boyc);
						   $("#ykname").val(decodeURIComponent(aa.ykname));
						   $("#yktel").val(decodeURIComponent(aa.yktel));
						   $("#yksfz").val(decodeURIComponent(aa.yksfz));
						   $("#mnote").val(decodeURIComponent(aa.mnote));
						   $("#selusers").html(decodeURIComponent(aa.users)).attr("datausersid",decodeURIComponent(aa.usersid));



					$("#selusers").unbind().click(function(){
						var ms='';
						var aa="A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z".split(",");
						var ss='';
						for(var t=0;t<aa.length;t++){
							ss+='<span class="selsm" >'+aa[t]+'</span>';
						}
						ms+='<div style="position:relative;height:300px">';
						ms+='<div id="boxleft" style="width:180px;overflow-y:scroll;height:100%;background-color:#feffe9"></div>';
						ms+='<div id="boxright" style="width:90px;overflow-y:auto;height:100%;position:absolute;top:0px;right:0px">'+ss+'</div>';
						ms+='</div>';
						topfrm(ms,function(_s){
						    $.get("/9766/pub/php/ajaxbd.php?dowhat=getuserssel",function(data,stat){
							   $("#boxleft").html(data);
							   $(".selusers").each(function(t2,o2){
								   o2.onclick=function(){
									   $("#selusers").html($(o2).html()).attr("datausersid",$(o2).attr("dataid"))
									   _s.closeme();
								   };
							   });
							   
							   $(".selsm").each(function(t2,o2){
								   o2.onclick=function(){
									  $("#boxleft").scrollTop(0)
									  var xy=$("#boxleft").offset();
									  
									  var xy1=$(".selusers[datasm^='"+$(o2).html().toLowerCase()+"']").offset();
									  $("#boxleft").animate({"scrollTop":(xy1.top-xy.top)+"px"},10);
								   };
							   });
							   
						    });
							
						});
					});
						   
						   setpinfo();
					   });
						  
						  function setbdtcboy(){
							  $.get("/9766/pub/php/ajaxbd.php?dowhat=getbdtcboypydlist&my="+$("#hid_my").val()+"&tcid="+$(o1).attr("datatcid")+"&ordid="+$(o1).attr("dataid"),function(data,stat){
								  data=data.replace(/\r\n/gi,"");
								  if(data==""){
									  $("#txt_p2").show();
									  $("#ybox").show();
									  $("#tcboybox").html("").hide();
								  }else{
									  $("#txt_p2").hide();
									  $("#ybox").hide();
									 $("#tcboybox").html(data).show();
								  }

								  if(("A"+window.mytype).indexOf("计调")>0){
									  $(".em_tcboyp").hide();
								  }
								  
								  setpinfo();
	  
								   $(".az").each(function(t1,o1){
									   o1.onclick=function(){
										   var m=val($(o1).attr("data"));
										   var v=val($(o1).parent().find(".txtc").val());
										   var a=$(".txtc");
										   var totv=0;
										   for(var t=0;t<a.length;t++){
											   totv+=val($(a[t]).val());
										   }
										   totv+=m;
										   v+=m;
										   if(v<0||(m<0&&totv<1))return;
										   $(o1).parent().find(".txtc").val(v);
										   setpinfo();
									   };
								   });

								   $(".em_tcboyp").each(function(t1,o1){
									   $(o1).unbind().bind("blur input propertychange",function(){
										   setpinfo();
									   });
								   });
	  
								   $(".txtc").each(function(t1,o1){
									   $(o1).unbind().bind("blur input propertychange",function(){
										   var a=$(".txtc");
										   var totv=0;
										   var v=val($(o1).val());
										   for(var t=0;t<a.length;t++){
											   totv+=val($(a[t]).val());
										   }
										   if(v<0||totv<1){
											   sysmsg("数量不正确");
											   $(o1).val(0);
											   return;
										   }
										   setpinfo();
									   });
								   });
								   
								   
	  
							  });
						  }setbdtcboy();	
						  
						   setdiary("txt_playdate","date");
						   $("#txt_playdate").change(function(){
							   getdatecc($(o1).attr("datatcid"),$(o1).find(".playtimestr").html(),$("#txt_playdate").val());
						   });
							 //manp=val($("#txt_p1").va());
							 //boyp=val($("#txt_p2").val());

							 getnowcc(o1);
	  
							 $("#cmdget").unbind().click(function(){
								 getcard($("#mnote").val(),function(aa){
									 $("#ykname").val(aa.name);
									 $("#yktel").val(aa.tel);
									 $("#yksfz").val(aa.sfz);
								 });
							 });
							 
							 $("#cmdchg").unbind().click(function(){
								 //更改订单
								 var id=$(o1).attr("dataid");
								 $("#cmdchg").unbind();
								 var oo=$("#selcc").find(".ydtimesel")[0];
								 var cc="";
								 var playtimestr=""
								 var playtime="";
								 if(oo){
									 cc=$(oo).attr("datacc");
									 playtime=$(oo).attr("data");
									 playtimestr=$(oo).html();
								 }

								 var a=$(".txtc[data='tcboy']");
								 var tcboyp='',tcboyc='',tcboyname='';
								 for(var t=0;t<a.length;t++){
									 if(tcboyname==""){
										 //tcboyp=val($(a[t]).attr("datap"));
										 tcboyp=val($(a[t]).parent().find(".em_tcboyp").val());
										 tcboyc=val($(a[t]).val());
										 tcboyname=decodeURIComponent($(a[t]).attr("dataname"));
									 }else{
										 //tcboyp+=","+val($(a[t]).attr("datap"));
										 tcboyp+=","+val($(a[t]).parent().find(".em_tcboyp").val());
										 tcboyc+=","+val($(a[t]).val());
										 tcboyname+=","+decodeURIComponent($(a[t]).attr("dataname"));
									 }
								 }
								 tcboyname=encodeURIComponent(tcboyname);
								 
								 $.get("/9766/pub/php/ajaxbd.php?dowhat=ordchg&id="+id+"&mnote="+encodeURIComponent($("#mnote").val())+"&ykname="+encodeURIComponent($("#ykname").val())+"&yktel="+encodeURIComponent($("#yktel").val())+"&yksfz="+encodeURIComponent($("#yksfz").val())+"&manc="+$(".txtc[data='man']").val()+"&boyc="+$(".txtc[data='boy']").val()+"&cc="+cc+"&playtime="+playtime+"&playtimestr="+encodeURIComponent(playtimestr)+"&manp="+val($("#txt_p1").val())+"&boyp="+val($("#txt_p2").val())+"&playdate="+$("#txt_playdate").val()+"&tcboyc="+tcboyc+"&tcboyp="+tcboyp+"&tcboyname="+tcboyname+"&usersid="+$("#selusers").attr("datausersid"),function(data,stat){
									  $(c).click();
									  var aa=data.replace(/\r\n/gi,"").split("{emean}");
									  if(aa[0]=="ok"){
										  sysmsg("更改成功!");
										  reflash(o1);
										  $.get("/9766/pub/wxpay/bdmsg.php?dowhat=ordchgmsg&ordno="+aa[1],function(data,stat){});
										  //ordlist(np);
									  }else{
										  sysmsg("未做更改!"+aa[1]);
									  }
								 });
							 });
					   
				   });
				   
				};
			});
			

	function getnowcc(o1){
		   var aa=$(o1).attr("dataplaydate").split("-");
		   $.get("/9766/pub/php/ajaxbd.php?dowhat=getnowccedit&tcid="+$(o1).attr("datatcid")+"&nowcc="+$(o1).attr("datacc")+"&nowplaytimestr="+encodeURIComponent($(o1).find(".playtimestr").html())+"&y="+aa[0]+"&m="+aa[1]+"&d="+aa[2],function(data,stat){
			   $("#selcc").html(data);
			   $("#selcc").find(".ydtime").each(function(ttt,ooo){
				   /*
				   if(val($(o1).attr("datacc"))==val($(ooo).attr("datacc"))){
					   $(ooo).addClass("ydtimesel");
				   }
				   */
				   ooo.onclick=function(){
					   if($(ooo).hasClass("ydtimesel")){
					      $(ooo).removeClass("ydtimesel");
					   }else{
					      $("#selcc").find(".ydtimesel").removeClass("ydtimesel");
					      $(ooo).addClass("ydtimesel");
					   }
				   }
			   });
		   });
	}
	//---------------------------------------------------------------------------------------------------
	function getdatecc(tcid,playtimestr,playdate){
		   var aa=playdate.split("-");
		   //sysmsg("入园日期发生变化");
		   $.get("/9766/pub/php/ajaxbd.php?dowhat=getnowcc&tcid="+tcid+"&playtimestr="+encodeURIComponent(playtimestr)+"&y="+aa[0]+"&m="+aa[1]+"&d="+aa[2],function(data,stat){
			   if(("A"+data).indexOf(playtimestr)>0){
			   }else{
   				   sysmsg("请注意,本日场次有变化");
			   }
			     $("#selcc").html(data);
			     $("#selcc").find(".ydtime").each(function(ttt,ooo){
				   ooo.onclick=function(){
					   if($(ooo).hasClass("ydtimesel")){
					      $(ooo).removeClass("ydtimesel");
					   }else{
					      $("#selcc").find(".ydtimesel").removeClass("ydtimesel");
					      $(ooo).addClass("ydtimesel");
					   }
				   }
			     });
		   });
	}
	function reflash(o){
		$.get("/9766/pub/php/ajaxbd.php?dowhat=getbdordinfosmall&id="+$(o).attr("dataid"),function(data,stat){
			 //eval("var aa="+data);
			 var ms='';
			 ms+='<tr>';
			 ms+='<td colspan="20" style="color:#c00">更改后'+data+'</td>';
			 ms+='</tr>';
			 $(o).parent().parent().after(ms);
		});
		//allstat();
	}
//------------按钮结束----------------------			
		});
	}
	//---------------------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------------------
	$("#cmdtj").unbind().click(function(){
		allstat();
	});
	//---------------------------------------------------------------------------------------------------
	$("#cmdcls").unbind().click(function(){
		$("#sx_date1").val("");
		$("#sx_date2").val("");
		$("#txtStartTime1").val("");
		$("#txtStartTime2").val("");
		$("#sx_bdname").val("");
		$("#sx_cckey").val("");
		$("#sx_tckey").val("");
		$(".ydtimesel").removeClass("ydtimesel");
		
		//allstat();
	});
	//---------------------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------------------
}shopinit();