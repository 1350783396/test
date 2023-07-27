<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ETicket.Web.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>心客网</title>
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="keywords" content="<%=key %>" />
    <meta name="description" content="<%=des %>"/>

    <!-- Le styles -->
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap.css">

    <!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap-ie6.css">
    <![endif]-->
    <!--[if lte IE 7]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/ie.css">
    <![endif]-->
    <link rel="stylesheet" type="text/css" href="/bootstrap/index.css">
    <style type="text/css">
    </style>
   

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
   
</head>

<body>

   <!--#include file="/templ/_head.html"-->

    <div id="main">
        <div class="container" style="background-color:#ffffff;">
            <div class="row">
                <div class="span15">
                    <div id="myCarousel" class="carousel slide">
                        <!-- Carousel items -->
                        <div class="carousel-inner">
                             <%=ETicket.Web.HtmlController.Instance.ListCarouse(5) %>
                        </div>
                        <!-- Carousel nav -->
                        <a class="carousel-control left" href="#myCarousel" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#myCarousel" data-slide="next">&rsaquo;</a>
                    </div>
                </div>
                <div class="span5 " style="height:350px;">
                    <div class="left_container" style="height:100%">
                        <div class="dl-title-bg">
                           <a href="<%=ETicket.BLL.RewriteMapBLL.Instance.ListUrl(10) %>" target="_blank">通知公告</a>
                        </div>
                        <ul style="line-height: 35px; font-size: 12px; width: 100%; float: left; ">
                           <%=ETicket.Web.HtmlController.Instance.IndexListArt(10,12,15,"/templ/index/lef_art.html") %>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-bottom:20px;">
                <div class="span15">
                    <div class="left_title_bg">
                        <div style="background-color: #017DDF; width: 100px; height: 35px; padding-left: 10px; float: left; line-height: 35px;">
                            <a href="#" style="font-size: 18px; font-weight: bold; color: #FFFFFF; ">景区门票</a>
                        </div>
                        <i style="float: right; padding-right: 10px; line-height: 35px;"><a href="/ticket.html" style="color: #000; ">更多</a></i>
                    </div>
                    <ul class="thumbnails" style="padding-top:20px;">
                        <%=ETicket.Web.HtmlController.Instance.ListProduct("ticket",6,"/templ/index/product.html") %>
                    </ul>
                </div>
                <div class="span5">
                    <div class="left_container" style="height:250px;">
                        <div class="dl-title-bg">
                            最新订单
                        </div>
                        <div id="scrollDiv"> 
                            <ul style="line-height: 35px; font-size: 12px; width: 100%; float: left; " class="unstyled">
                                 <%=ETicket.Web.HtmlController.Instance.ListNewOrder(100,"/templ/index/new_order.html") %>
                            </ul>
                        </div>
                        <span id="btn1" style="display:none">向前</span>  <span id="btn2"  style="display:none">向后</span> 
                    </div>
                    <div class="left_container" style="height:200px;margin-top:10px;">
                        <div class="dl-title-bg">
                            <a href="<%=ETicket.BLL.RewriteMapBLL.Instance.ListUrl(2) %>" target="_blank">旅游资讯</a>
                        </div>
                        <ul style="line-height: 35px; font-size: 12px; width: 100%; float: left; ">
                            <%=ETicket.Web.HtmlController.Instance.IndexListArt(2,6,15,"/templ/index/lef_art.html") %>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="span20">
                    <div class="left_title_bg">
                        <div style="background-color: #017DDF; width: 100px; height: 35px; padding-left: 10px; float: left; line-height: 35px;">
                            <a href="#" style="font-size: 18px; font-weight: bold; color: #FFFFFF; ">专线线路</a>
                        </div>
                        <i style="float: right; padding-right: 10px; line-height: 35px;"><a href="/line.html" style="color: #000; ">更多</a></i>
                    </div>
                    <ul class="thumbnails" style="padding-top:20px;">
                        <%=ETicket.Web.HtmlController.Instance.ListProduct("line",8,"/templ/index/product.html") %>
                    </ul>
                </div>
            </div>
            <div class="row">
                    <div class="span15">
                       
                            <ul class="nav nav-tabs" style="background-color: #0477EA; margin: 0 0;  repeat-x; height: 40px; line-height: 40px;">
                                <li id="tab1" class="cTab" >阳朔概况</li>
                                <li id="tab2" class="cTab" >阳朔购物</li>
                                <li id="tab3" class="cTab" >阳朔交通</li>
                                <li id="tab4" class="cTab ">特产美食</li>
                                <li id="tab5" class="cTab ">休闲娱乐</li>
                                <li id="tab6" class="cTab cTabSel">自驾车线路</li>
                            </ul>
                            <div id="tab1_con" class="cTabCon">
                                <table class="table ">
                                    <%=ETicket.Web.HtmlController.Instance.IndexListArt(3,7,200,"/templ/index/tab_art.html") %>
                                    <tr>
                                        <td style="width:80%; border:none"></td>
                                        <td style="width:20%; border:none"><a href="<%=ETicket.BLL.RewriteMapBLL.Instance.ListUrl(3) %>" target="_blank" style="font-size:16px;font-weight:bold;">更多........</a></td>
                                    </tr>
                                </table>
                            </div>
                            <div id="tab2_con" class="cTabCon">
                                <table class="table ">
                                    <%=ETicket.Web.HtmlController.Instance.IndexListArt(4,7,200,"/templ/index/tab_art.html") %>
                                    <tr>
                                        <td style="width:80%; border:none"></td>
                                        <td style="width:20%; border:none"><a href="<%=ETicket.BLL.RewriteMapBLL.Instance.ListUrl(4) %>" target="_blank" style="font-size:16px;font-weight:bold;">更多........</a></td>
                                    </tr>
                               </table>
                            </div>
                            <div id="tab3_con" class="cTabCon">
                                <table class="table ">
                                    <%=ETicket.Web.HtmlController.Instance.IndexListArt(5,7,200,"/templ/index/tab_art.html") %>
                                    <tr>
                                        <td style="width:80%; border:none"></td>
                                        <td style="width:20%; border:none"><a href="<%=ETicket.BLL.RewriteMapBLL.Instance.ListUrl(5) %>" target="_blank" style="font-size:16px;font-weight:bold;">更多........</a></td>
                                    </tr>
                               </table>
                            </div>
                            <div id="tab4_con" class="cTabCon">
                                <table class="table ">
                                     <%=ETicket.Web.HtmlController.Instance.IndexListArt(6,7,200,"/templ/index/tab_art.html") %>
                                    <tr>
                                        <td style="width:80%; border:none"></td>
                                        <td style="width:20%; border:none"><a href="<%=ETicket.BLL.RewriteMapBLL.Instance.ListUrl(6) %>" target="_blank" style="font-size:16px;font-weight:bold;">更多........</a></td>
                                    </tr>
                               </table>
                            </div>
                            <div id="tab5_con" class="cTabCon">
                                <table class="table ">
                                    <%=ETicket.Web.HtmlController.Instance.IndexListArt(7,7,200,"/templ/index/tab_art.html") %>
                                    <tr>
                                        <td style="width:80%; border:none"></td>
                                        <td style="width:20%; border:none"><a href="<%=ETicket.BLL.RewriteMapBLL.Instance.ListUrl(7) %>" target="_blank" style="font-size:16px;font-weight:bold;">更多........</a></td>
                                    </tr>
                               </table>
                            </div>
                            <div id="tab6_con" class="cTabCon cTabConSel">
                                <table class="table ">
                                    <%=ETicket.Web.HtmlController.Instance.IndexListArt(8,7,200,"/templ/index/tab_art.html") %>
                                    <tr>
                                        <td style="width:80%; border:none"></td>
                                        <td style="width:20%; border:none"><a href="<%=ETicket.BLL.RewriteMapBLL.Instance.ListUrl(8) %>" target="_blank" style="font-size:16px;font-weight:bold;">更多........</a></td>
                                    </tr>
                               </table>
                            </div>
                    </div>
                    <div class="span5 " style="height:350px;">
                        <div class="left_container" style="height:100%">
                            <div class="dl-title-bg">
                                <a href="<%=ETicket.BLL.RewriteMapBLL.Instance.ListUrl(9) %>" target="_blank">游记攻略</a>
                            </div>
                            <ul style="line-height: 35px; font-size: 12px; width: 100%; float: left;" >
                                <%=ETicket.Web.HtmlController.Instance.IndexListArt(9,13,15,"/templ/index/lef_art.html") %>
                            </ul>
                        </div>
                    </div>
            </div>
            
            <div class="row" style="min-height:50px;margin-top:5px;padding:5px 10px; border:2px #dedede solid;">
                <b>友情链接：</b> <%=ETicket.Web.HtmlController.Instance.ListLink() %>
            </div>
     
         </div> <!-- /container -->
    </div>
    
    <!--#include file="/templ/_foot.html"-->

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/bootstrap/js/bootstrap.js"></script>
    <!--<script type="text/javascript" src="js/kefu.js"></script>-->
    <!--[if lte IE 6]>
    <script type="text/javascript" src="/js/bootstrap-ie.js"></script>
    <![endif]-->
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                if ($.isFunction($.bootstrapIE6)) $.bootstrapIE6($(document));
            });
        })(jQuery);

        $(function () {
            /*dqTab1*/
            $('.cTab').hover(function () {

                $(".cTab").removeClass("cTabSel");
                $(".cTabCon").removeClass("cTabConSel");

                var id = this.id;
                $("#" + id).addClass("cTabSel");
                $("#" + id + "_con").addClass("cTabConSel");

            }, function () { /**/ }
            )//end hover

        })
    </script>

    <script type="text/javascript">
        (function ($) {
            $.fn.extend({
                Scroll: function (opt, callback) {
                    //参数初始化 
                    if (!opt) var opt = {};
                    var _btnUp = $("#" + opt.up);//Shawphy:向上按钮 
                    var _btnDown = $("#" + opt.down);//Shawphy:向下按钮 
                    var timerID;
                    var _this = this.eq(0).find("ul:first");
                    var lineH = _this.find("li:first").height(), //获取行高 
                    line = opt.line ? parseInt(opt.line, 10) : parseInt(this.height() / lineH, 10), //每次滚动的行数，默认为一屏，即父容器高度 
                    speed = opt.speed ? parseInt(opt.speed, 10) : 500; //卷动速度，数值越大，速度越慢（毫秒） 
                    timer = opt.timer //?parseInt(opt.timer,10):3000; //滚动的时间间隔（毫秒） 
                    if (line == 0) line = 1;
                    var upHeight = 0 - line * lineH;
                    //滚动函数 
                    var scrollUp = function () {
                        _btnUp.unbind("click", scrollUp); //Shawphy:取消向上按钮的函数绑定 
                        _this.animate({
                            marginTop: upHeight
                        }, speed, function () {
                            for (i = 1; i <= line; i++) {
                                _this.find("li:first").appendTo(_this);
                            }
                            _this.css({ marginTop: 0 });
                            _btnUp.bind("click", scrollUp); //Shawphy:绑定向上按钮的点击事件 
                        });
                    }
                    //Shawphy:向下翻页函数 
                    var scrollDown = function () {
                        _btnDown.unbind("click", scrollDown);
                        for (i = 1; i <= line; i++) {
                            _this.find("li:last").show().prependTo(_this);
                        }
                        _this.css({ marginTop: upHeight });
                        _this.animate({
                            marginTop: 0
                        }, speed, function () {
                            _btnDown.bind("click", scrollDown);
                        });
                    }
                    //Shawphy:自动播放 
                    var autoPlay = function () {
                        if (timer) timerID = window.setInterval(scrollUp, timer);
                    };
                    var autoStop = function () {
                        if (timer) window.clearInterval(timerID);
                    };
                    //鼠标事件绑定 
                    _this.hover(autoStop, autoPlay).mouseout();
                    _btnUp.css("cursor", "pointer").click(scrollUp).hover(autoStop, autoPlay);//Shawphy:向上向下鼠标事件绑定 
                    _btnDown.css("cursor", "pointer").click(scrollDown).hover(autoStop, autoPlay);
                }
            })
        })(jQuery);
        $(document).ready(function () {
            $("#scrollDiv").Scroll({ line: 4, speed: 2000, timer: 1000, up: "btn1", down: "btn2" });
        });
    </script> 

</body>
</html>
