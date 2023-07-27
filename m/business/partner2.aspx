﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="partner.aspx.cs" Inherits="ETicket.Web.business.main_partner" %><head>
<title>智慧游</title>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="description" content="">
    <link rel="stylesheet" href="2018lib/weui.min.css">
    <link rel="stylesheet" href="2018css/jquery-weui.css">
    <link rel="stylesheet" href="2018css/reset.css">
    <link rel="stylesheet" href="2018css/box-flex.css">
    <link rel="stylesheet" href="2018css/style.css">
    <script src="2018lib/jquery-2.1.4.js"></script>
    <script src="2018js/adaptive.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
    adaPtive(); //铺页面调用             
    //页面加载时调用
    $(function() { direction(); });
    //用户变化屏幕方向时调用
    $(window).on('orientationchange', function(e) { direction(); });
    //调用adaptive
    function adaPtive(max) {
        window['adaptive'].desinWidth = 720;
        window['adaptive'].baseFont = 24;
        window['adaptive'].scaleType = 1;
        window['adaptive'].maxWidth = max;
        window['adaptive'].init();
    }
    //判断手机屏幕方向
    function direction() { if (window.orientation == 90 || window.orientation == -90) { adaPtive(320); return false; } else if (window.orientation == 0 || window.orientation == 180) { adaPtive(); return false; } }
    </script>
</head>

<body ontouchstart>
    <div class="wx-header clearfix flex">
        <h1 class="flex-1">会员中心</h1>
        <div class="wx-header-right" id="showMoreLink">
            <i class="iconfont icon-jia"></i>
        </div>
    </div>
    <!--me-main -->
    <div class="weui-msg me-main clearfix">
        <div class="weui-cells" style="margin-top: 0">
            <a class="weui-cell head-cells weui-cell_access" href="/business/partner_edit.aspx">
                <div class="weui-cell__hd"><img src="img/ren.png" alt=""></div>
                <div class="weui-cell__bd flex flex-v">
                    <p>账号：<%=ETicket.BLL.UserBLL.Instance.GetLoginModel().UserName %></p>
                </div>
                <div class="weui-cell__ft"></div>
            </a>        </div>
        <div class="weui-cells__title"></div>
        <div class="weui-cells">
            <a class="weui-cell weui-cell_access" href="/news-1.html">
                <div class="weui-cell__hd"><img src="2018images/wallet_card.png" alt=""></div>
                <div class="weui-cell__bd">
                    <p>分销商通知</p>
                </div>
                <div class="weui-cell__ft"></div>
            </a>        </div>
			<div class="weui-cells">
            <a class="weui-cell weui-cell_access" href="/business/partner/account_query_list.aspx">
                <div class="weui-cell__hd"><img src="2018images/me_plate.png" alt=""></div>
                <div class="weui-cell__bd">
                    <p>我的钱包</p>
                </div>
                <div class="weui-cell__ft"></div>
            </a>        </div>
        <div class="weui-cells__title"></div>
        <div class="weui-cells">
            <a class="weui-cell weui-cell_access" href="/dingdan/index.aspx">
                <div class="weui-cell__hd"><img src="2018images/me_collect.png" alt=""></div>
                <div class="weui-cell__bd">
                    <p>我的订单</p>
                </div>
                <div class="weui-cell__ft"></div>
            </a>
            <a class="weui-cell weui-cell_access" href="/tuikuan/index.aspx">
                <div class="weui-cell__hd"><img src="2018images/me_photo.png" alt=""></div>
                <div class="weui-cell__bd">
                    <p>退款订单</p>
                </div>
                <div class="weui-cell__ft"></div>
            </a>
            <a class="weui-cell weui-cell_access" href="/sms/index.aspx">
                <div class="weui-cell__hd"><img src="2018images/me_card.png" alt=""></div>
                <div class="weui-cell__bd">
                    <p>短信管理</p>
                </div>
                <div class="weui-cell__ft"></div>
            </a>
			            <a class="weui-cell weui-cell_access" href="/business/user_chgpass.aspx">
                <div class="weui-cell__hd"><img src="2018images/wallet_sy.png" alt=""></div>
                <div class="weui-cell__bd">
                    <p>密码修改</p>
                </div>
                <div class="weui-cell__ft"></div>
            </a>
            <a class="weui-cell weui-cell_access" href="/about/">
                <div class="weui-cell__hd"><img src="2018images/me_smile.png" alt=""></div>
                <div class="weui-cell__bd">
                    <p>关于我们</p>
                </div>
                <div class="weui-cell__ft"></div>
            </a>        </div>
        <div class="weui-cells__title"></div>
        <div class="weui-cells">
            <a class="weui-cell weui-cell_access" href="/logout.aspx">
                <div class="weui-cell__hd"><img src="2018images/me_set.png" alt=""></div>
                <div class="weui-cell__bd">
                    <p>退出系统</p>
                </div>
                <div class="weui-cell__ft"></div>
            </a>        </div>
        <!-- moreLink -->
        <div class="moreLink">
            <ul class="morelink-ul">
                <li><a href="/news-10.html"><img src="2018images/moreLink_msg.png" alt=""><p>公告</p></a></li>
                <li><a href="/business/partner/order_list.aspx"><img src="2018images/moreLink_add.png" alt=""><p>订单查看</p></a></li>
    <!-- <li><a href=""><img src="2018images/moreLink_sys.png" alt=""><p>扫一扫</p></a></li>   -->
    <!-- <li><a href=""><img src="2018images/moreLink_pay.png" alt=""><p>收付款</p></a></li>   -->
            </ul>
        </div>
        <!-- moreLink -->
    </div>
    <!--me-main -->
    <!--  weui-tabbar -->
    <div class="weui-tabbar">
        <a href="../index.aspx" class="weui-tabbar__item">
            <!--   <span class="weui-badge" style="position: absolute;top: -.4em;right: 1em;">8</span> -->
            <div class="weui-tabbar__icon wj">
                <img src="2018images/home.png" alt="" width="40" height="40">            </div>
            <p class="weui-tabbar__label">首页</p>
        </a>
        <a href="/content/list-ticket.aspx" class="weui-tabbar__item">
            <div class="weui-tabbar__icon rw">
                <img src="2018images/store-1.png" alt="" width="40" height="40">            </div>
            <p class="weui-tabbar__label">门票</p>
        </a>
        <a href="/content/list-line.aspx" class="weui-tabbar__item">
            <div class="weui-tabbar__icon zx">
                <img src="2018images/shopcar.png" alt="" width="40" height="40">            </div>
            <p class="weui-tabbar__label">线路</p>
        </a>
        <a href="/business/partner.aspx" class="weui-tabbar__item  weui-bar__item--on">
            <div class="weui-tabbar__icon me">
                <img src="./2018images/tarbar_me.png" alt="" width="32" height="49">            </div>
            <p class="weui-tabbar__label">我的</p>
        </a>
    </div>
    <!--  weui-tabbar -->
    
    <script src="2018lib/fastclick.js"></script>
    <script>
    $(function() {
        FastClick.attach(document.body);
        $("#showMoreLink").on('click',function(){
            $(".moreLink").toggle("fast");
        })
    });
    </script>
    <script src="2018js/jquery-weui.js"></script>
</body>

</html>