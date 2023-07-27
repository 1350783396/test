<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="ETicket.Web.business.main" %>

<!DOCTYPE HTML>
<html>
 <head>
   <title>售票系统</title>
   <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
   <link href="assets/css/dpl-min.css" rel="stylesheet" type="text/css" />
   <link href="assets/css/bui-min.css" rel="stylesheet" type="text/css" />
   <link href="assets/css/main.css" rel="stylesheet" type="text/css" />
 </head>
 <body>

  <div class="header">    
       <div id="top">
          <div id="logo">
              <div id="functions">

                  <a href="#">你好,<%=ETicket.BLL.UserBLL.Instance.GetLoginModel().UserName %></a>
                  | <a href="/index.aspx">门户首页</a>
                  | <a href="/logout.aspx">退出系统</a>
              </div>
          </div>
      </div>
   </div>
  
   <div class="content">
    <div class="dl-main-nav">
      <div class="dl-inform"><div class="dl-inform-title">贴心小秘书<s class="dl-inform-icon dl-up"></s></div></div>
      <ul id="J_Nav"  class="nav-list ks-clear">
          <%if(roleid==1){ %>
        <li class="nav-item dl-selected"><div class="nav-item-inner nav-home">首页</div></li>
        <li class="nav-item"><div class="nav-item-inner nav-inventory">产品</div></li>
        <li class="nav-item"><div class="nav-item-inner nav-order">订单</div></li>
        <li class="nav-item"><div class="nav-item-inner nav-supplier">用户</div></li>
        <li class="nav-item"><div class="nav-item-inner nav-marketing">统计</div></li>
        <li class="nav-item"><div class="nav-item-inner nav-marketing">内容</div></li>
          <%}else if(roleid==2){ %>
           <li class="nav-item dl-selected"><div class="nav-item-inner nav-home">首页</div></li>
           <li class="nav-item"><div class="nav-item-inner nav-order">订单</div></li>
           <li class="nav-item"><div class="nav-item-inner nav-supplier">用户</div></li>
           <li class="nav-item"><div class="nav-item-inner nav-marketing">统计</div></li>
          <%}else if (roleid == 3){ %>
           <li class="nav-item dl-selected"><div class="nav-item-inner nav-home">首页</div></li>
           <li class="nav-item"><div class="nav-item-inner nav-order">订单</div></li>
           <li class="nav-item"><div class="nav-item-inner nav-marketing">统计</div></li>    
          <%}%>
      </ul>
    </div>
    <ul id="J_NavContent" class="dl-tab-conten">

    </ul>
     
   </div>
  <div style="height:20px; width:100%; background-color:aliceblue;z-index:9999;">
     售票管理系统
   </div>
 
  <script type="text/javascript" src="assets/js/jquery-1.8.1.min.js"></script>
  <script type="text/javascript" src="assets/js/bui.js"></script>
  <script type="text/javascript" src="assets/js/config.js"></script>
      <%if(roleid==1){ %>
  <script>
      BUI.use('common/main', function () {
          var config = [{
              id: 'menu',
              homePage: 'home',
              menu: [{
                  text: '首页内容',
                  items: [
                    { id: 'home', text: '通知公告', href: 'art/view_list.aspx'},
                    { id: 'user_chgpass', text: '修改密码', href: 'user_chgpass.aspx' }
                    
                  ]
              }, {
                  text: '系统设置',
                  items: [
                    { id: 'ticket_failday_setting', text: '景区门票失效', href: 'setting/ticket_failday_setting.aspx' },
                    { id: 'line_fail_minute_setting', text: '专线票失效', href: 'setting/line_fail_minute_setting.aspx' },
                    { id: 'order_day_max_setting', text: '预订设置', href: 'setting/order_day_max_setting.aspx' },
                    { id: 'sms_ad_setting', text: '网站域名', href: 'setting/sitedomain_setting.aspx' },
                    { id: 'seo_key_index_setting', text: '门户SEO关键字', href: 'setting/seo_key_index_setting.aspx' },
                    { id: 'seo_des_index_setting', text: '门户SEO描述', href: 'setting/seo_des_index_setting.aspx' },
                    { id: 'sms_ad_setting', text: '短信广告管理', href: 'setting/sms_ad_setting.aspx' },
                    { id: 'sms_qrcode_templ_setting', text: '景区-二维码短信', href: 'setting/sms_qrcode_templ_setting.aspx' },
                    { id: 'sms_idcard_templ_setting', text: '景区-身份证短信', href: 'setting/sms_idcard_templ_setting.aspx' },
                    { id: 'sms_qrcode_templ_setting_forline', text: '专线-二维码短信', href: 'setting/sms_qrcode_templ_setting_forline.aspx' },
                    { id: 'sms_idcard_templ_setting_forline', text: '专线-身份证短信', href: 'setting/sms_idcard_templ_setting_forline.aspx' },
                    { id: 'sms_valid_templ', text: '短信验证码模板', href: 'setting/sms_valid_templ.aspx' }
                     ]
              }]
          }, {
              id: 'product',
              menu: [{
                  text: '产品管理',
                  items: [
                     { id: 'line_add', text: '添加专线', href: 'product/line_add.aspx' },
                     { id: 'line_list', text: '管理专线', href: 'product/line_list.aspx' },
                     { id: 'ticket_add', text: '添加景区', href: 'product/ticket_add.aspx' },
                     { id: 'ticke_list', text: '管理景区', href: 'product/ticket_list.aspx' },
                     { id: 'region_list', text: '地区分类', href: 'product/region_list.aspx' },
                     { id: 'Properties1_list', text: '属性管理', href: 'product/Properties1_list.aspx' }
                  ]
              }
              ]
          }, {
              id: 'order',
              menu: [{
                  text: '订单管理',
                  items: [
                    { id: 'admin_order_list', text: '订单列表', href: 'order/order_list.aspx' },
                    { id: 'order_list_cashwaitpay', text: '现金待收', href: 'order/order_list_cashwaitpay.aspx' },
                    { id: 'order_list_refund_waitchk', text: '退款待审', href: 'order/order_list_refund_waitchk.aspx' },
                    { id: 'order_list_refund_chkno', text: '退款审核不通过', href: 'order/order_list_refund_chkno.aspx' },
                    { id: 'order_list_refund_chkyes', text: '退款审核通过', href: 'order/order_list_refund_chkyes.aspx' },
                    { id: 'order_list_refund_chksucc', text: '已退款', href: 'order/order_list_refund_chksucc.aspx' },
                    { id: 'order_refund_cz', text: '异常订单冲正', href: 'order/order_refund_cz.aspx' },
                    
                    { id: 'order_sms_list', text: '短信管理', href: 'order/order_sms_list.aspx' },
                    { id: 'order_print_list', text: '打印管理', href: 'order/order_print_list.aspx' },
                    { id: 'order_sale_report', text: '打印报表', href: 'order/order_sale_report.aspx' },
                    { id: 'valid_man', text: '手动验票', href: 'order/valid_man.aspx' }
                  ]}
           
              ]
          }, {
              id: 'user',
              menu: [{
                  text: '分销商',
                  items: [
                    { id: 'partner_add', text: '添加分销商', href: 'user/partner_add.aspx' },
                    { id: 'partner_list', text: '分销商管理', href: 'user/partner_list.aspx' },
                    { id: 'partnertype_list', text: '级别管理', href: 'user/partnertype_list.aspx' },
                    { id: 'account_in_list', text: '积分充值', href: 'user/account_in_list.aspx' },
                    { id: 'account_chk_list', text: '积分充值审核', href: 'user/account_chk_list.aspx' },
                    { id: 'account_chk_list', text: '积分销除', href: 'user/account_out_list.aspx' },
                    { id: 'account_outchk_list', text: '积分销除审核', href: 'user/account_outchk_list.aspx' },
                    { id: 'account_query_list', text: '积分查询', href: 'user/account_query_list.aspx' }
                  ]
              },
              {
                text: '注册会员',
                items: [
                    { id: 'member_list', text: '会员管理', href: 'user/member_list.aspx' }
                ]
              } ,
              {
                  text: '后台用户',
                  items: [
                      { id: 'admin_add', text: '添加用户', href: 'user/admin_add.aspx' },
                      { id: 'admin_list', text: '管理用户', href: 'user/admin_list.aspx' }
                      
                  ]
              },
              {
                  text: '验票账号',
                  items: [
                      { id: 'valid_add', text: '添加验票账号', href: 'user/valid_add.aspx' },
                      { id: 'valid_list', text: '管理验票账号', href: 'user/valid_list.aspx' }
                  ]
              },
              {
                  text: '订单查看账号',
                  items: [
                      { id: 'orderview_add', text: '添加查看账号', href: 'user/orderview_add.aspx' },
                      { id: 'orderview_list', text: '管理查看账号', href: 'user/orderview_list.aspx' }
                  ]
              }

          ]
          },
          {
              id: 'chart',
              menu:
                  [{
                  text: '统计',
                  items:
                  [
                      { id: 'tongji_buy_count', text: '销售量统计-分销商', href: 'tongji/tongji_buy_count.aspx' },
                      { id: 'tongji_buy_count_forUser', text: '销售量统计-普通会员', href: 'tongji/tongji_buy_count_forUser.aspx' },
                      { id: 'tongji_buy_detail', text: '销售明细表', href: 'tongji/tongji_buy_detail.aspx' },
                      { id: 'tongji_order_count', text: '订单量统计', href: 'tongji/tongji_order_count.aspx' },
                      { id: 'tongji_buy_sum_forParner', text: '购买合计-分销商', href: 'tongji/tongji_buy_sum_forParner.aspx' },
                      { id: 'tongji_buy_percent', text: '购买比例', href: 'tongji/tongji_buy_percent.aspx' },
                      { id: 'tongji_paytype_count', text: '支付方式统计', href: 'tongji/tongji_paytype_count.aspx' }
                  ]
              }]
          },
          {
              id: 'cms',
              menu:
          [{
              text: '内容',
              items:
              [
                  { id: 'art_list_1', text: '通知公告-分销', href: 'art/art_list.aspx?mid=1' },
                  { id: 'art_list_1', text: '通知公告-门户', href: 'art/art_list.aspx?mid=10' },
                  { id: 'art_list_2', text: '旅游资讯', href: 'art/art_list.aspx?mid=2' },
                  { id: 'art_list_3', text: '阳朔概况', href: 'art/art_list.aspx?mid=3' },
                  { id: 'art_list_4', text: '阳朔购物', href: 'art/art_list.aspx?mid=4' },
                  { id: 'art_list_5', text: '阳朔交通', href: 'art/art_list.aspx?mid=5' },
                  { id: 'art_list_6', text: '特产美食', href: 'art/art_list.aspx?mid=6' },
                  { id: 'art_list_7', text: '休闲娱乐', href: 'art/art_list.aspx?mid=7' },
                  { id: 'art_list_8', text: '自驾车线路', href: 'art/art_list.aspx?mid=8' },
                  { id: 'art_list_9', text: '游记攻略', href: 'art/art_list.aspx?mid=9' },
                  { id: 'widget_list_carousel', text: '门户轮播图片', href: 'art/widget_list.aspx?mid=carousel' },
                  { id: 'widget_list_link', text: '友情链接', href: 'art/widget_list.aspx?mid=link' }
              ]
          },
          {
              text: '帮助中心',
              items:
              [
               
                  { id: 'help_list_1', text: '订购方式', href: 'art/help_list.aspx?mid=1' },
                  { id: 'help_list_2', text: '配送方式', href: 'art/help_list.aspx?mid=2' },
                  { id: 'help_list_3', text: '支付方式', href: 'art/help_list.aspx?mid=3' },
                  { id: 'help_list_4', text: '账户安全', href: 'art/help_list.aspx?mid=4' },
                  { id: 'help_list_5', text: '售后服务', href: 'art/help_list.aspx?mid=5' },
                  { id: 'help_list_6', text: '常见问题', href: 'art/help_list.aspx?mid=6' }
              ]
          }
          ]
          }

          ];
          new PageUtil.MainPage({
              modulesConfig: config
          });
      });
  </script>
      <%}else if(roleid==2){ %>
    <script>
        BUI.use('common/main', function () {
            var config = [{
                id: 'menu',
                homePage: 'home',
                menu: [{
                    text: '首页内容',
                    items: [
                      { id: 'home', text: '通知公告', href: 'art/view_list.aspx' },
                      { id: 'user_chgpass', text: '修改密码', href: 'user_chgpass.aspx' }]}
                  ]
            },
            {
                id: 'order',
                menu: [{
                    text: '订单管理',
                    items: [
                      { id: 'admin_order_list', text: '订单列表', href: 'order/order_list.aspx' },
                      { id: 'order_list_cashwaitpay', text: '现金待收', href: 'order/order_list_cashwaitpay.aspx' },
                      { id: 'order_list_refund_waitchk', text: '退款待审', href: 'order/order_list_refund_waitchk.aspx' },
                      { id: 'order_list_refund_chkno', text: '退款审核不通过', href: 'order/order_list_refund_chkno.aspx' },
                      { id: 'order_list_refund_chkyes', text: '退款审核通过', href: 'order/order_list_refund_chkyes.aspx' },
                      { id: 'order_list_refund_chksucc', text: '已退款', href: 'order/order_list_refund_chksucc.aspx' },
                      { id: 'order_refund_cz', text: '异常订单冲正', href: 'order/order_refund_cz.aspx' },
                      { id: 'order_sms_list', text: '短信管理', href: 'order/order_sms_list.aspx' },
                      { id: 'order_print_list', text: '打印管理', href: 'order/order_print_list.aspx' },
                      { id: 'order_sale_report', text: '打印报表', href: 'order/order_sale_report.aspx' },
                      { id: 'valid_man', text: '手动验票', href: 'order/valid_man.aspx' }
                    ]
                }

                ]
            }, {
                id: 'user',
                menu: [{
                    text: '分销商',
                    items: [
                      { id: 'account_chkin_code', text: '积分审核授权码设置', href: 'setting/accout_checkin_code.aspx' },
                      { id: 'account_in_list', text: '积分充值', href: 'user/account_in_list.aspx' },
                      { id: 'account_chk_list', text: '积分充值审核', href: 'user/account_chk_list.aspx' },
                       { id: 'account_chk_list', text: '积分销除', href: 'user/account_out_list.aspx' },
                      { id: 'account_outchk_list', text: '积分销除审核', href: 'user/account_outchk_list.aspx' },
                      { id: 'account_query_list', text: '积分查询', href: 'user/account_query_list.aspx' }
                    ]
                }
                ]
            },
            {
                id: 'chart',
                menu:
                    [{
                        text: '统计',
                        items:
                        [
                            { id: 'tongji_buy_count', text: '销售量统计-分销商', href: 'tongji/tongji_buy_count.aspx' },
                            { id: 'tongji_buy_count_forUser', text: '销售量统计-普通会员', href: 'tongji/tongji_buy_count_forUser.aspx' },
                            { id: 'tongji_buy_detail', text: '销售明细表', href: 'tongji/tongji_buy_detail.aspx' },
                            { id: 'tongji_order_count', text: '订单量统计', href: 'tongji/tongji_order_count.aspx' },
                            { id: 'tongji_buy_sum_forParner', text: '购买合计-分销商', href: 'tongji/tongji_buy_sum_forParner.aspx' },
                            { id: 'tongji_buy_percent', text: '购买比例', href: 'tongji/tongji_buy_percent.aspx' },
                            { id: 'tongji_paytype_count', text: '支付方式统计', href: 'tongji/tongji_paytype_count.aspx' }
                        ]
                    }]
            }
         

            ];
            new PageUtil.MainPage({
                modulesConfig: config
            });
        });
  </script>
      <%}else if(roleid==3){ %>
         <script>
             BUI.use('common/main', function () {
                 var config = [{
                     id: 'menu',
                     homePage: 'home',
                     menu: [{
                         text: '首页内容',
                         items: [
                           { id: 'home', text: '通知公告', href: 'art/view_list.aspx' },
                           { id: 'user_chgpass', text: '修改密码', href: 'user_chgpass.aspx' }]
                     }
                     ]
                 },
                 {
                     id: 'order',
                     menu: [{
                         text: '订单管理',
                         items: [
                           { id: 'admin_order_list', text: '订单列表', href: 'order/order_list.aspx' },
                           { id: 'order_list_refund_chksucc', text: '已退款', href: 'order/order_list_refund_chksucc.aspx' },
                           { id: 'order_sms_list', text: '短信管理', href: 'order/order_sms_list.aspx' },
                           { id: 'order_print_list', text: '打印管理', href: 'order/order_print_list.aspx' },
                           { id: 'order_sale_report', text: '打印报表', href: 'order/order_sale_report.aspx' },
                           { id: 'valid_man', text: '手动验票', href: 'order/valid_man.aspx' }
                         ]
                     }

                     ]
                 },
                 {
                     id: 'chart',
                     menu:
                         [{
                             text: '统计',
                             items:
                             [
                                 { id: 'tongji_buy_count', text: '销售量统计-分销商', href: 'tongji/tongji_buy_count.aspx' },
                                 { id: 'tongji_buy_count_forUser', text: '销售量统计-普通会员', href: 'tongji/tongji_buy_count_forUser.aspx' },
                                 { id: 'tongji_buy_detail', text: '销售明细表', href: 'tongji/tongji_buy_detail.aspx' },
                                 { id: 'tongji_order_count', text: '订单量统计', href: 'tongji/tongji_order_count.aspx' },
                                 { id: 'tongji_buy_sum_forParner', text: '购买合计-分销商', href: 'tongji/tongji_buy_sum_forParner.aspx' },
                                 { id: 'tongji_buy_percent', text: '购买比例', href: 'tongji/tongji_buy_percent.aspx' },
                                 { id: 'tongji_paytype_count', text: '支付方式统计', href: 'tongji/tongji_paytype_count.aspx' }
                             ]
                         }]
                 }


                 ];
                 new PageUtil.MainPage({
                     modulesConfig: config
                 });
             });
  </script>
      <%} %>
 </body>
</html>
