<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view_2.aspx.cs" Inherits="ETicket.Web.wap.view_2" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <title>商品详情</title>

    <!--
    <script src="/wap/pace/pace.js"></script>
    <link href="/wap/pace/themes/pace-theme-center-circle.css" rel="external nofollow"  rel="stylesheet" />
     -->

    <link href="/wap/css/cp_style_v1.0.css" rel="stylesheet" type="text/css" />

    <link href="/wap/img/startup.png" rel="apple-touch-startup-image" />
    <link href="/wap/img/apple-touch-icon-57x57-precomposed.png" sizes="57x57" rel="apple-touch-icon-precomposed" />
    <link href="/wap/img/apple-touch-icon-72x72-precomposed.png" sizes="72x72" rel="apple-touch-icon-precomposed" />
    <link href="/wap/img/apple-touch-icon-114x114-precomposed.png" sizes="114x114" rel="apple-touch-icon-precomposed" />

    <!--
    <script type="text/javascript" src="http://d2.lashouimg.com/wap/js/jquery-1.3.2.min.js"></script>
    <script src="/wap/js/common20150123.js" type="text/javascript"></script>
        -->
      
</head>
<body>
    <header class="index">
        <div class="c-hd">
            <section class="back" style="display:none;">
                <a href="javascript:history.go(-1);"><i></i></a>
            </section>
            <section class="hd-title" style="font-size:18px;font-weight: bold;">
                <%=productName %>
            </section>
            <section class="hd-nav" style="display:none;">
                <a class="arrowside-fun" title="收藏" onclick="javascript:void(0)" href="javascript:void(0)"> <span class="ml-coll">收藏</span> </a>
                <a href="#" class="arrowside-fun" title="分享到"> <span class="ml-fx">分享到</span> </a>
            </section>
        </div>
    </header>
    <!--content-->
    <div id="fullbg"></div>
    <div class="content">
        <div class="d3">
            <!-- 图片和标题 ↓ -->
            <div class="section-detailbox">
                <div class="deal-pic">
                        <img src="<%=imgPath %>" original="" width="260" height="165" alt="<%=productName %>" />
                </div>
            </div>
            <!--  图片和标题 ↑ -->
            <!-- 门票专线信息 ↓ -->
            <div class="section-detailbox">
                <section class="title">
                    <h2><%=productType %>信息</h2>
                </section>
                <section class="title deal-site">
                    <p><%=productIntr %></p>
                </section>
            </div>
            <!-- 门票专线信息 ↑ -->

            <!-- 预定须知 ↓ -->
            <div class="section-detailbox">
                <section class="title">
                    <h2>预定须知</h2>
                </section>
                <section class="title">
                    <div class="detail_cen" style="padding:0;">
                        <p>
                            <%=rulesNote_WAP %>
                        </p>
                    </div>
                </section>
            </div>
            <!-- 预定须知 ↑ -->


            <!-- 详细介绍↓ -->
            <div class="section-detailbox">
                <section class="title">
                    <h2>详细介绍</h2>
                </section>
                <section class="title">
                    <div class="detail_cen" style="padding:0; font-size:12px;">
                        <p>
                            <%=detail_WAP %>
                        </p>
                    </div>
                </section>
            </div>
            <!-- 详细介绍↑ -->
        </div>

      

       
        <!-- 购买按钮 ↓ -->
        <div id="buybox">
            <div class="section-buybox">
                <div class="deal-buyatt">
                    <p class="price"><font>&yen;<b><%=myPrice %></b></font><del>&yen;<%=primeCost %></del></p>
                </div>
                <div class="deal-buybtn"><a href="/wap/shopcar.aspx?q=<%=NJiaSu.Libraries.PubFun.QueryString("q") %>" title="立即购买">立即购买</a></div>
            </div>
        </div>
        <div id="flow_buybox">
            <div class="section-buybox">
                <!-- <div id="show_pos">1</div> -->
                <div class="deal-buyatt">
                    <p class="price"><font>&yen;<b><%=myPrice %></b></font><del>&yen;<%=primeCost %></del></p>
                </div>

                <div class="deal-buybtn"><a href="/wap/shopcar.aspx?q=<%=NJiaSu.Libraries.PubFun.QueryString("q") %>" title="立即购买">立即购买</a></div>
            </div>
        </div>
        <!-- 购买按钮 ↑ -->
    </div>
    <!--
    <script type="text/javascript" src="/wap/js/detail-20151015.js"></script>
        -->
</body>
</html>
