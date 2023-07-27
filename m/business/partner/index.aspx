
<!DOCTYPE HTML>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0"/>
<meta http-equiv="Cache-Control" content="no-transform " />
<meta http-equiv="Content-Type" content="text/html;charset=utf-8">
<meta name="format-detection" content="telephone=no"/>
<title>智慧游</title>

<link type="text/css" rel="stylesheet" href="css/5054ead092b654a548a4eeec1cbf6ec8_20160802072348.css" />
<script src="http://libs.baidu.com/jquery/2.0.0/jquery.min.js" type="text/javascript"></script>
<script>

function displayit(n){

	for(i=0;i<4;i++){

		if(i==n){

			var id='menu_list'+n;

			if(document.getElementById(id).style.display=='none'){

				document.getElementById(id).style.display='';

				document.getElementById("plug-wrap").style.display='';

			}else{

				document.getElementById(id).style.display='none';

				document.getElementById("plug-wrap").style.display='none';

			}

		}else{

			if($('#menu_list'+i)){

				$('#menu_list'+i).css('display','none');

			}

		}

	}

}

function closeall(){

	var count = document.getElementById("top_menu").getElementsByTagName("ul").length;

	for(i=0;i<count;i++){

		document.getElementById("top_menu").getElementsByTagName("ul").item(i).style.display='none';

	}

	document.getElementById("plug-wrap").style.display='none';

}



document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {

	WeixinJSBridge.call('hideToolbar');

});

</script>
 <style type="text/css">

body { margin-bottom:60px !important; }

a, button, input { -webkit-tap-highlight-color:rgba(255, 0, 0, 0); }

ul, li { list-style:none; margin:0; padding:0 }

.top_bar { position: fixed; z-index: 900; bottom: 0; left: 0; right: 0; margin: auto; font-family: Helvetica, Tahoma, Arial, Microsoft YaHei, sans-serif; }

.top_menu { display:-webkit-box; border-top: 1px solid #3D3D46; display: block; width: 100%; background: rgba(255, 255, 255, 0.7); height: 48px; display: -webkit-box; display: box; margin:0; padding:0; -webkit-box-orient: horizontal; background: -webkit-gradient(linear, 0 0, 0 100%, from(#697077), to(#3F434E), color-stop(60%, #464A53)); box-shadow: 0 1px 0 0 rgba(255, 255, 255, 0.3) inset; }

.top_bar .top_menu>li { -webkit-box-flex:1; background: -webkit-gradient(linear, 0 0, 0 100%, from(rgba(0, 0, 0, 0.1)), color-stop(50%, rgba(0, 0, 0, 0.3)), to(rgba(0, 0, 0, 0.4))), -webkit-gradient(linear, 0 0, 0 100%, from(rgba(255, 255, 255, 0.1)), color-stop(50%, rgba(255, 255, 255, 0.1)), to(rgba(255, 255, 255, 0.15))); ; -webkit-background-size:1px 100%, 1px 100%; background-size:1px 100%, 1px 100%; background-position: 1px center, 2px center; background-repeat: no-repeat; position:relative; text-align:center; }

.top_menu li:first-child { background:none; }

.top_bar .top_menu>li>a { height:48px; display:block; text-align:center; color:#FFF; text-decoration:none; text-shadow: 0 1px rgba(0, 0, 0, 0.3); -webkit-box-flex:1; }

.top_bar .top_menu>li>a label { overflow:hidden; margin: 0 0 0 0; font-size: 12px; display: block !important; line-height: 18px; text-align: center; }

.top_bar .top_menu>li>a img { padding: 3px 0 0 0; height: 24px; width: 24px; color: #fff; line-height: 48px; vertical-align:middle; }

.top_bar li:first-child a { display: block; }

.menu_font { text-align:left; position:absolute; right:10px; z-index:500; background: -webkit-gradient(linear, 0 0, 0 100%, from(#697077), to(#3F434E), color-stop(60%, #464A53)); border-radius: 5px; width: 120px; margin-top: 10px; padding: 0; box-shadow: 0 1px 5px rgba(0, 0, 0, 0.3); }

.menu_font.hidden { display:none; }

.menu_font { top:inherit !important; bottom:60px; }

.menu_font li a { height:40px; margin-right: 1px; display:block; text-align:center; color:#FFF; text-decoration:none; text-shadow: 0 1px rgba(0, 0, 0, 0.3); -webkit-box-flex:1; }

.menu_font li a { text-align: left !important; }

.top_menu li:last-of-type a { background: none; }

.menu_font:after { top: inherit!important; bottom: -6px; border-color: #3F434E rgba(0, 0, 0, 0) rgba(0, 0, 0, 0); border-width: 6px 6px 0; position: absolute; content: ""; display: inline-block; width: 0; height: 0; border-style: solid; left: 80%; }

.menu_font li { border-top: 1px solid rgba(255, 255, 255, 0.1); border-bottom: 1px solid rgba(0, 0, 0, 0.2); }

.menu_font li:first-of-type { border-top: 0; }

.menu_font li:last-of-type { border-bottom: 0; }

.menu_font li a { height: 40px; line-height: 40px !important; position: relative; color: #fff; display: block; width: 100%; text-indent: 10px; white-space: nowrap; text-overflow: ellipsis; overflow: hidden; }

.menu_font li a img { width: 20px; height:20px; display: inline-block; margin-top:-2px; color: #fff; line-height: 40px; vertical-align:middle; }

.menu_font>li>a label { padding:3px 0 0 3px; font-size:14px; overflow:hidden; margin: 0; }

#menu_list0 { right:0; left:10px; }

#menu_list0:after { left: 20%; }

#sharemcover { position: fixed; top: 0; left: 0; width: 100%; height: 100%; background: rgba(0, 0, 0, 0.7); display: none; z-index: 20000; }

#sharemcover img { position: fixed; right: 18px; top: 5px; width: 260px; height: 180px; z-index: 20001; border:0; }

.top_bar .top_menu>li>a:hover, .top_bar .top_menu>li>a:active { background-color:#333; }

.menu_font li a:hover, .menu_font li a:active { background-color:#333; }

.menu_font li:first-of-type a { border-radius:5px 5px 0 0; }

.menu_font li:last-of-type a { border-radius:0 0 5px 5px; }

#plug-wrap { position: fixed; top: 0; left: 0; width: 100%; height: 100%; background: rgba(0, 0, 0, 0); z-index:800; }

#cate18 .device {bottom: 49px;}

#cate18 #indicator {bottom: 240px;}

#cate19 .device {bottom: 49px;}

#cate19 #indicator {bottom: 330px;}

#cate19 .pagination {bottom: 60px;}

</style>

 
<script type="text/javascript" src="js/de1a661a33c9526da1ff7a8206cddf9a_20160802072348.js" ></script></head>
<body id="wrapper-content">
<style>    
    .newhdinner{position: fixed;left:0;top: 0;z-index: 9;}
    body{padding-top: 48px;}
</style>
<header class="newhdinner" id="sheader" style="background: #fff !important;">
    <div class="newnobgpr">
       
        <span>阳朔票务网</span>
        <div class="mlistfrbox clearfix">
       
        </div>
    </div>
</header>
<link type="text/css" rel="stylesheet" href="css/d90843f3457c900ad1d86f99749d6b78_20160802072348.css" /><script type="text/javascript" src="js/5e758e90ef332ee31bd0e4b36db81521_20160802072348.js" ></script>  
<div class='swipe'>
    <ul id='sliders' class="clearfix">
        <li><a href="1.html"><img src="indeximages/245720023572.jpg" /></a></li>
        <li><a href="2.html"><img src="indeximages/245720023573.jpg" /></a></li>
        <li><a href="3.html"><img src="indeximages/245720023574.jpg" /></a></li>
    </ul>
    <div class="slidersNavi" id="slidersNavi">
        <a href="javascript:void(0)javascript:void(0)" ></a>
        <a href="javascript:void(0)javascript:void(0)" ></a>
        <a href="javascript:void(0)javascript:void(0)" ></a>
    </div>
</div>

<div class="mddshot">
  <div class="seeMdd">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td> <a href="/content/list-ticket.aspx" target="_self" class="vm">景区门票<span><img src="http://static.wed114.cn/static_mobile/miyue/ui/wap/img/r.png" /></span></a></td>
    <td><a href="/content/list-line.aspx" target="_self" class="vm">旅游专线<span><img src="http://static.wed114.cn/static_mobile/miyue/ui/wap/img/r.png" /></span></a></td>
  </tr>
</table>

  <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>
     
		
    </div>
    <ul class="clearfix">
        <li>
            <a href="/news-10.html">
                <img src="http://www.meyouone.com/images/up/334716493582.jpg" />
                <p>通知公告</p>
            </a>
        </li><li>
            <a href="javascript:void(0)/miyue_p2.html">
                <img src="http://www.meyouone.com/images/up/234272296974.jpg" />
                <p>通知公告1</p>
            </a>
        </li><li>
            <a href="javascript:void(0)/miyue_p3.html">
                <img src="http://www.meyouone.com/images/up/231004203853.jpg" />
                <p>通知公告2</p>
            </a>
        </li><li>
            <a href="javascript:void(0)/miyue_p4.html">
                <img src="http://www.meyouone.com/images/up/366225935122.jpg" />
                <p>通知公告3</p>
            </a>
        </li><li>
            <a href="javascript:void(0)/miyue_p5.html">
                <img src="http://www.meyouone.com/images/up/227304980600.jpg" />
                <p>通知公告4</p>
            </a>
        </li><li>
            <a href="javascript:void(0)/miyue_p7.html">
                <img src="http://www.meyouone.com/images/up/263264312005.jpg" />
                <p>通知公告5</p>
            </a>
        </li><li>
            <a href="javascript:void(0)/miyue_p12.html">
                <img src="http://www.meyouone.com/images/up/113815112769.jpg" />
                <p>通知公告6</p>
            </a>
        </li><li>
            <a href="javascript:void(0)/miyue_p13.html">
                <img src="http://www.meyouone.com/images/up/232295294302.jpg" />
                <p>通知公告7</p>
            </a>
        </li>  </ul>
  
</div>


<script type="text/javascript">
    $(function () {
        var active = 0, as = $('#slidersNavi a');
        for (var i = 0; i < as.length; i++) {
            (function () {
                var j = i;
                as.click(function () {
                    t.slide(j);
                    return false;
                })
            })();
        }
        var t = new TouchSlider('sliders', {
            duration: 1000,
            direction: 0,
            interval: 3000,
            fullsize: true
        });
        t.on('before', function (m, n) {
            as[m].className = '';
            as[n].className = 'active';
        });
    });
</script>

<script>
function hidedownloadTag(o) {
    $(o).parents('.downApp').hide();
    $.cookie('downloadTagFlag', true, { path: '/' });
}
</script>
<div class="top_bar" style="-webkit-transform:translate3d(0,0,0)">

    <nav>

    <ul id="top_menu" class="top_menu">

    <li> <a href="/"><img src="indeximages/plugmenu6.png"><label>首页</label></a>

                </li><li> <a href="/content/list-ticket.aspx"><img src="indeximages/plugmenu3.png"><label>景点门票</label></a>

                </li><li> <a href="/content/list-line.aspx"><img src="indeximages/plugmenu5.png"><label>旅游专线</label></a>

                </li><li> <a onclick="/business/partner.aspx"><img src="indeximages/plugmenu17.png"><label>个人中心</label></a>


                 </li> 

    </ul>

  </nav>

</div>



<div id="plug-wrap" onclick="closeall()" style="display: none;"></div>

</body>
<!--<nav class="navtab">
    <ul>
        <li ><a href="javascript:void(0)/"><i class="iconfont icon-home"></i>首页</a></li>
        <li ><a href="javascript:void(0)"><i class="iconfont icon-pic"></i>拍婚纱</a></li>
        <li ><a href="javascript:void(0)"><i class="iconfont icon-xiangji1"></i>办婚礼</a></li>
        <li ><a href="javascript:void(0)"><i class="iconfont icon-xiangji1"></i>吉日</a></li>
        <li ><a href="javascript:void(0)"><i class="iconfont icon-people"></i>我的</a></li>
    </ul>
</nav>-->

</body>
</html>