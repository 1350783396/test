<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="partner.aspx.cs" Inherits="ETicket.Web.business.main_partner" %><head>
<!DOCTYPE html>
<html lang="en">
<style type="text/css">
<!--
.STYLE2 {font-size: 12px}
-->
</style>
<head>
	<meta charset="UTF-8">
	<title>会员中心</title>
	<meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=0" name="viewport" />
	<meta content="yes" name="apple-mobile-web-app-capable" />
	<meta content="black" name="apple-mobile-web-app-status-bar-style" />
	<meta content="telephone=no" name="format-detection" />
	<link href="css/style2022.css" rel="stylesheet" type="text/css" />
</head>
<body>
<section class="aui-flexView">
	<header class="aui-navBar aui-navBar-fixed">
		<a href="javascript:;" class="aui-navBar-item">
			<i class="icon "></i>		</a>
		<div class="aui-center">
			<span class="aui-center-title">我的</span>		</div>
		<a href="/business/partner_edit.aspx" class="aui-navBar-item">
			<i class="icon icon-sys"></i>		</a>	</header>
	<section class="aui-scrollView">
		<div class="aui-head-bg">
			<div class="aui-flex">
				<div class="aui-mine-user">
					<img src="images/user.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>分销名称：<%=ETicket.BLL.UserBLL.Instance.GetLoginModel().UserName %></p>
				</div>
				<div class="aui-arrow"></div>
			</div>
		</div>
		<div class="aui-mon-list">
			<a href="/business/partner/account_query_list.aspx" class="aui-flex b-line">
				<div class="aui-cou-img">
					<img src="images/nav-fl-001.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>我的钱包</p>
				</div>
				<div class="aui-arrow">				</div>
			</a>
			<a href="/dingdan/index.aspx" class="aui-flex b-line">
				<div class="aui-cou-img">
					<img src="images/nav-fl-002.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>订单管理</p>
				</div>
				<div class="aui-arrow">				</div>
			</a>
			<a href="/tuikuan/index.aspx" class="aui-flex b-line">
				<div class="aui-cou-img">
					<img src="images/nav-fl-003.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>退款订单</p>
				</div>
				<div class="aui-arrow">				</div>
			</a>
			<div class="divHeight"></div>
			<a href="/news-1.html" class="aui-flex b-line">
				<div class="aui-cou-img">
					<img src="images/nav-fl-007.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>分销通知</p>
				</div>
				<div class="aui-arrow">				</div>
			</a>
			<a href="/sms/index.aspx" class="aui-flex b-line">
				<div class="aui-cou-img">
					<img src="images/nav-fl-005.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>短信管理</p>
				</div>
				<div class="aui-arrow">				</div>
			</a>
			<a href="/business/partner_edit.aspx" class="aui-flex b-line">
				<div class="aui-cou-img">
					<img src="images/nav-fl-006.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>个人资料</p>
				</div>
				<div class="aui-arrow">				</div>
			</a>
			<a href="/business/user_chgpass.aspx" class="aui-flex b-line">
				<div class="aui-cou-img">
					<img src="images/nav-fl-004.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>密码修改</p>
				</div>
				<div class="aui-arrow">				</div>
			</a>
			<a href="/about/" class="aui-flex b-line">
				<div class="aui-cou-img">
					<img src="images/nav-fl-008.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>关于我们</p>
				</div>
				<div class="aui-arrow">				</div>
			</a>
			<div class="divHeight"></div>
			<a href="/logout.aspx" class="aui-flex b-line">
				<div class="aui-cou-img">
					<img src="2018images/me_set.png" alt="">				</div>
				<div class="aui-flex-box">
					<p>退出系统</p>
				</div>
				<div class="aui-arrow">				</div>
			</a>		</div>
		<div style="height:55px;"></div>
	</section>    
	<!--  weui-tabbar -->
	<footer class="aui-footer aui-footer-fixed">
		<a href="../index.aspx" class="aui-tabBar-item aui-tabBar-item-active">
            <div class="weui-tabbar__icon me">
                <img src="2018images/home.png" alt="" width="20" height="20" border="0">            </div>
            <p class="weui-tabbar__label STYLE2">首页</p>
        </a>
		<a href="/content/list-ticket.aspx" class="aui-tabBar-item aui-tabBar-item-active">
            <div class="weui-tabbar__icon me">
                <img src="2018images/store-1.png" alt="" width="20" height="20" border="0">            </div>
            <p class="weui-tabbar__label STYLE2">门票</p>
        </a>
		<a href="/content/list-line.aspx" class="aui-tabBar-item aui-tabBar-item-active">
            <div class="weui-tabbar__icon me">
                <img src="2018images/shopcar.png" alt="" width="20" height="20" border="0">            </div>
            <p class="weui-tabbar__label STYLE2">线路</p>
      </a>		
		<a href="/business/partner.aspx" class="aui-tabBar-item aui-tabBar-item-active">
            <div class="weui-tabbar__icon me">
                <img src="./2018images/my.png" alt="" width="20" height="20" border="0">            </div>
            <p class="weui-tabbar__label STYLE2">我的</p>
      </a>
		
		</footer>
</section>
</body>
<script type="text/javascript" src="js/jquery.min.js"></script>
</html>
