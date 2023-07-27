<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="partner.aspx.cs" Inherits="ETicket.Web.business.main_partner" %>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>阳朔电子票务 yangshuo.cm 售票管理系统</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" src="js/jquery.js"></script>
<script type="text/javascript">
$(function(){	
	//顶部导航切换
	$(".nav li a").click(function(){
		$(".nav li a.selected").removeClass("selected")
		$(this).addClass("selected");
	})	
})	
</script>


</head>

<body style="background:url(images/topbg.gif) repeat-x;">

    <div class="topleft">
    <a href="main.html" target="_parent"><img src="images/logo.png" title="系统首页" /></a>
    </div>
        
    <ul class="nav">
    <li><a href="default.html" target="rightFrame" class="selected"><img src="images/icon01.png" title="工作台" /><h2>工作台</h2></a></li>
    <li><a href="imgtable.html" target="rightFrame"><img src="images/icon02.png" title="模型管理" /><h2>模型管理</h2></a></li>
    <li><a href="imglist.html"  target="rightFrame"><img src="images/icon03.png" title="模块设计" /><h2>模块设计</h2></a></li>
    <li><a href="tools.html"  target="rightFrame"><img src="images/icon04.png" title="常用工具" /><h2>常用工具</h2></a></li>
    <li><a href="computer.html" target="rightFrame"><img src="images/icon05.png" title="文件管理" /><h2>文件管理</h2></a></li>
    <li><a href="tab.html"  target="rightFrame"><img src="images/icon06.png" title="系统设置" /><h2>系统设置</h2></a></li>
    </ul>
            
<div class="topright">    
    <ul>
    <li><span><img src="images/help.png" title="帮助"  class="helpimg"/></span><a href="#">帮助</a></li>
    <li><a href="/index.aspx">门户首页</a></li>
    <li><a href="/logout.aspx" target="_parent">退出</a></li>
    </ul>
     
    <div class="user">
    <span><a href="#">你好,<%=ETicket.BLL.UserBLL.Instance.GetLoginModel().UserName %></a></span>
    <i>消息</i>
    <b>5</b>
    </div>    
    
    </div>
 <head>
   <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
   <link href="assets/css/dpl-min.css" rel="stylesheet" type="text/css" />
   <link href="assets/css/bui-min.css" rel="stylesheet" type="text/css" />
   <link href="assets/css/main.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
<!--
.STYLE1 {color: #FF3300}
-->
   </style>
</head>
 <body>
 <div class="content">
    <div class="dl-main-nav">
      <div class="dl-inform"><div class="dl-inform-title">贴心小秘书<s class="dl-inform-icon dl-up"></s></div></div>
      <ul id="J_Nav"  class="nav-list ks-clear">
        <li class="nav-item dl-selected"><div class="nav-item-inner nav-home">分销系统</div></li>

      </ul>
    </div>
    <ul id="J_NavContent" class="dl-tab-conten">

    </ul>
   </div>
  <script type="text/javascript" src="assets/js/jquery-1.8.1.min.js"></script>
  <script type="text/javascript" src="./assets/js/bui.js"></script>
  <script type="text/javascript" src="./assets/js/config.js"></script>

  <script>
      BUI.use('common/main', function () {
          var config = [{
              id: 'menu',
              homePage: 'home',
              menu: [{
                  text: '系统设置',
                  items: [
                    { id: 'home', text: '通知公告', href: 'art/view_list.aspx' },
                    { id: 'account_query_list', text: '积分余额', href: 'partner/account_query_list.aspx' },
                    { id: 'account_query_list', text: '修改资料', href: 'partner/partner_edit.aspx' },
                    { id: 'user_chgpass', text: '修改密码', href: 'user_chgpass.aspx' }
                    
                  ]
              },
			  {
                  text: '产品预订',
                  items: [
				    { id: 'plist_ticket', text: '预订门票', href: 'partner/plist_ticket.aspx' },
                    { id: 'plist_line', text: '预订线路', href: 'partner/plist_line.aspx' },
				    { id: 'plist_hotel', text: '预订酒店', href: 'partner/plist_hotel.aspx' }
                    
                  ]
          },
			  
			   {
                  text: '交易管理',
                  items: [
                    { id: 'order_list', text: '订单列表', href: 'partner/order_list.aspx' },
                    { id: 'back_list', text: '退款订单', href: 'partner/order_refund_list.aspx' },
                    { id: 'order_sms_list', text: '短信管理', href: 'partner/order_sms_list.aspx' },
                    { id: 'order_print_list', text: '小票打印', href: 'partner/order_print_list.aspx' },
                    { id: 'order_sale_report', text: '报表打印', href: 'partner/order_sale_report.aspx' }
                    

                  ]
          },
			  {
                  text: '销售统计',
                  items: [
                      { id: 'tongji_buy_count', text: '购买统计', href: 'partner/tongji_buy_count.aspx' },
                      { id: 'tongji_buy_detail', text: '购买明细表', href: 'partner/tongji_buy_detail.aspx' },
                      { id: 'tongji_order_count', text: '订单统计', href: 'partner/tongji_order_count.aspx' }
                  ]
              },	
			  
              {
                  text: '技术支持',
                  items: [

                     { id: '#', text: 'QQ:541915', href: '#' },
		             { id: '#', text: '13878375544', href: '#' }
                  ]
              }
			  
              ]
          },
		  
		   {
		   
              id: 'product',
              menu: [

              {
                  text: '专线线路',
                  items: [
                    { id:'plist_line', text: '专线下单', href: 'partner/plist_line.aspx' }
                    

                  ]
              }
              ]
          },
          {
              id: 'count',
              menu: [{
                  text: '景点门票',
                  items: [
                     { id: 'plist_ticket', text: '景区下单', href: 'partner/plist_ticket.aspx' }
                  ]
				 
              }]
			  
          }];
          new PageUtil.MainPage({
              modulesConfig: config
          });
      });
	  
  </script>
   
     <!-- closeable: false-->
 </body>
</html>
