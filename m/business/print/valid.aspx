<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="valid.aspx.cs" Inherits="ETicket.Web.business.validmain" %>

<!DOCTYPE HTML>
<html>
 <head>
   <title>售票管理系统</title>
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
        <li class="nav-item dl-selected"><div class="nav-item-inner nav-home">首页</div></li>
       
   
     
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
                  text: '首页内容',
                  items: [
                    { id: 'home', text: '通知公告', href: 'art/view_list.aspx' },
                    { id: 'valid_man', text: '输入订单号验票', href: 'valid/valid_man.aspx' },
                    { id: 'valid_list', text: '已验票明细', href: 'valid/valid_list.aspx' },
                    { id: 'tongji_valid_count', text: '已验票统计', href: 'valid/tongji_valid_count.aspx' },
                    { id: 'user_chgpass', text: '修改密码', href: 'user_chgpass.aspx' }
                    
                  ]
              }]
          }
          ];
          new PageUtil.MainPage({
              modulesConfig: config
          });
      });
  </script>
     <!-- closeable: false-->
 </body>
</html>
