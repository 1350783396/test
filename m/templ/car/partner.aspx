<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="partner.aspx.cs" Inherits="ETicket.Web.business.main_partner" %><head>
<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title></title>
        <meta charset="utf-8">
        <meta name="viewport" content="target-densitydpi=device-dpi, width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
        <link rel="stylesheet" href="/css/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="/css/ui-box.css">
        <link rel="stylesheet" href="/css/ui-base.css">
        <link rel="stylesheet" href="/css/ui-color.css">
        <link rel="stylesheet" href="/css/appcan.icon.css">
        <link rel="stylesheet" href="/css/appcan.control.css">
        <link rel="stylesheet" href="/my_index_content/css/main.css">
    </head>
    <body class="um-vp bc-bg" ontouchstart>
	 <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x"  onClick="javascript :history.back(-1);"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">我的账号</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-home umw2 umh4" onclick="window.open('/business/partner.aspx','_self')"></div>
                </div>
            </div>
        <div class="ub ub-ver">
            <div class=" umar-at1">
                <div id="listview1"  class="ubt bc-border c-wh">
				
				<table width="100%" border="0" cellspacing="0" cellpadding="5">
  <tr>
    <td style="padding: 5px;"><img src="/my_index_content/css/myImg/myImg6.png"></td>
    <td style="padding: 5px;"><div class="sc-bg-alert uc-a1 uinn3 ulev-2 bc-text-head" style="width:45px;">分销商</div>帐号：<%=ETicket.BLL.UserBLL.Instance.GetLoginModel().UserName %></td>
  </tr>
</table>

				

                </div>
                <div id="listview2"  class="ubt bc-border ubb c-wh umar-at1 uinn-a7">

<div class="ub" onclick="window.open('/business/partner/order_list.aspx','_self')"><img src="/my_index_content/css/myImg/myImg3.png">我的订单</div>
 
<div class="ub" onclick="window.open('/business/partner/order_refund_list.aspx','_self')"><img src="/my_index_content/css/myImg/myImg1.png">退款订单</div>

<div class="ub" onclick="window.open('/business/partner/account_query_list.aspx','_self')"><img src="/my_index_content/css/myImg/myImg2.png">我的积分</div>

<div class="ub" onclick="window.open('/content/list-ticket.aspx','_self')"><img src="/my_index_content/css/myImg/ticket.png">购买景区门票</div>

<div class="ub" onclick="window.open('/content/list-line.aspx','_self')"><img src="/my_index_content/css/myImg/line.png">购买专线线路</div>
                </div>

            </div>
            <div id="listview3"  class="ubt ubb bc-border c-wh umar-at1 uinn-a7">
<div class="ub" onclick="window.open('/about/','_self')"><img src="/my_index_content/css/myImg/myImg4.png">关于我们</div>
            </div>
            <div  id="listview4"  class="ubt ubb bc-border c-wh umar-at1 uinn-a7">
<div class="ub" onclick="window.open('/logout.aspx','_self')"><img src="/my_index_content/css/myImg/myImg5.png">退出系统</div>
            </div>
        </div>
     
    </body>
  
</html>
