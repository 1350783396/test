<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-ticket.aspx.cs" Inherits="ETicket.Web.content.view_ticket" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>龙腾古道-顺和嘉票务网</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="专线线路-顺和嘉票务网">
    <meta name="author" content="专线线路-顺和嘉票务网">

    <!-- Le styles -->
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap.css">

    <!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap-ie6.css">
    <![endif]-->
    <!--[if lte IE 7]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/ie.css">
    <![endif]-->

    <link rel="stylesheet" type="text/css" href="/bootstrap/index.css">
    <link rel="stylesheet" type="text/css" href="/bootstrap/ticket_detail.css">
   
</head>

<body>
    <!--#include file="/templ/_head.html"-->

    <div id="main" style="background-color:#ededf0;">
        <div class="container" style="background-color:#ffffff">
            <div class="row">
                <div class="span20 ">
                    <div class="detail_media_bg basefix" >
                        <div id="js_photoviewer" class="detail_media_left">
                            <div data-id="0" class="attraction_photo_big" id="js-preview-photo">
                            	<img width="400px" height="280px" alt="" src="/noimg.jpg">
                               <%-- <a class="prev" title="" href="javascript:;"></a>
                                <a class="next" title="" href="javascript:;"></a>
                                <div class="photo_name">
                                    <p>5/6 </p>	
                                    <a class="play" title="自动播放" href="javascript:;" style="display:none;">自动播放</a>
                                    <a class="stop" title="暂停播放" href="javascript:;">暂停播放</a>
                                </div>--%>

                            </div>
                          
                       

                        </div>
                        <div class="detail_media_right">
                            <h2 class="detail_media_title">
                                天籁·蝴蝶泉<%--<span><strong>AAAA</strong>级景区</span>--%>
                            </h2>
                            <ul class="detail_media_content">
                               <%-- <li class="sum">
                                    <em><span>4.0</span>分</em>
                                    <a data-targetid="ticket_info" class="copies goToAnchor" href="###">源自12人点评</a>
                                </li>
                                <li>
                                    <em>服务承诺</em>
                                    <a data-role="jmp" data-params="{options: {css:{maxWidth:'300'},type:'jmp_text',content:{txt0:'预订此景区门票入园无票且携程10分钟内未解决，可买市价票入园携程双倍赔差价'},classNames:{boxType:'jmp_text'},template:'#jmp_text1',alignTo:'cursor'}}" href="javascript:void(0);" class="detail_media_label01">入园保证</a>
                                </li>--%>
                                <li class="w">
                                    <em>景点地址</em>广西省桂林市阳朔县十里画廊。
                                    <%--<a data-targetid="jtzn" href="###" class="map_icon goToAnchor"><i></i>查看地图</a>--%>
                                </li>
                                <li class="w">
                                    <em>营业时间</em>8：00&mdash;17：30。
                                </li>
                            </ul>
                        </div>
                        <div class="price_bg">
                            <%--<div class="time">30分钟前有人预订该景点</div>--%>
                            <div class="price_box">
                                <dfn>¥</dfn>
                                <span>50</span> 元
                            </div>
                            <a data-targetid="ticket_package" class="btn_c goToAnchor" href="###">立刻预订</a>
                        </div>
                    </div>
                </div>
            </div>
            <div  class="row">
                <div class="span20">
                     <div class="detail_product_div">
                           <div class="detail_product_tab ">
                               <a class="tabBtn cursor" href="#productDetail">预订须知</a>
                               <a class="tabBtn" href="#bookNote">景点简介</a>
                               <%--<a class="tabBtn" href="###">交通指南</a>
                               <a class="tabBtn" href="###" id="fjjd" style="display:none">附近酒店</a>
                               <a class="tabBtn" href="###">用户点评</a>--%>
                               <a data-targetid="ticket_package" class="btn_red_big goToAnchor" href="###">立刻预订</a>
                           </div>
                     </div>
                </div>
            </div>
            <div class="row">
                <div class="span20">
                    
                         <div class="ticket_info no_border" id="productDetail">
                               <div class="ticket_info_style">
                                   <span>预订须知</span>
                                   <i class="detail_icon1"></i>
                                   <b></b>
                               </div>

                               <h1>productDetail</h1>
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               <br />
                               <br />
                               1
                               <br />
                               1
                               <br />
                               <br />
                               1
                               <br />
                               1
                               <br />
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                               <br />
                               1
                           </div>
                     
                     
                         <div class="ticket_info" id="bookNote">
                           <div class="ticket_info_style">
                               <span>景点介绍</span>
                               <i class="detail_icon3"></i>
                               <b></b>
                           </div>

                           <h1>bookNote</h1>
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           <br />
                           <br />
                           1
                           <br />
                           1
                           <br />
                           <br />
                           1
                           <br />
                           1
                           <br />
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                           <br />
                           1
                       </div>
                    
                </div>
            </div>
         </div>
    </div>
    
    <!--#include file="/templ/_foot.html"-->

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/bootstrap/js/bootstrap.js"></script>
    <!--<script type="text/javascript" src="js/kefu.js"></script>-->
    <!--[if lte IE 6]>
    <script type="text/javascript" src="/js/bootstrap-ie.js"></script>
    <![endif]-->
    <script type="text/javascript" src="/js/stickUp.js"></script>
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                if ($.isFunction($.bootstrapIE6)) $.bootstrapIE6($(document));
            });
        })(jQuery);

    </script>

    <%-- <script type="text/javascript">
         jQuery(function($) 
         { 
             $(document).ready( function() 
             { //为 '.navbar-wrapper' class 启用stickUp插件
                 $('.detail_product_div').stickUp(
                     {
                         parts:{
                             0: 'productDetail',
                             1: 'bookNote'
                         },
                         itemClass: 'tabBtn',
                         itemHover: 'cursor',
                         marginTop: '50px'
                     }
                  );
             });
         });
     </script>--%>
</body>
</html>

