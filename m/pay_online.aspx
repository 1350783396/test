<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_online.aspx.cs" Inherits="ETicket.Web.pay_online" %>

<style type="text/css">
<!--
.yudingdiv {

	justify-content: center;
	align-items: center;
	width: 100%;
	height: 40px; /*px*/
        // left: 0;
	margin: auto;
	color: #FFFFFF;
	background-color: #FF3300;
	border-top-style: none;
	border-right-style: none;
	border-bottom-style: none;
	border-left-style: none;
    }
.dingdandiv {
	background-color: #FFFFFF;
	width: 95%;
	margin-top: 5px;
	margin-right: auto;
	margin-bottom: 5px;
	margin-left: auto;
	border: 1px solid #CCCCCC;
}
.dingdandiv tr{
	border-bottom-width: 1px;
	border-bottom-style: solid;
	border-bottom-color: #CCCCCC;
	height: 25px;
}
.dingdandiv td{
	padding: 5px;
}
.yudingdiv a{
	color: #FFFFFF;
	text-decoration: none;
}
body {
	background-color: #E4E4E4;
}
.STYLE5 {color: #FF0000}
--> 
</style>

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
		 <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/common.js"></script>
    <script type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script type="text/javascript">




          function check() {
              var s = $("#txtValidCode").val();
			  var h = $("#ExPws").val();
			  var h = h.substr(-4);
              if (s == "") {
                  alert('请输入验证码');
                  return false;
              }
              else {

window.open ("/business/partner/order_detail.aspx?orderid="+h);

             // sAlert("系统正在处理........请稍等");
			  
				 
                  //return true;

              }
          }
          function loadimage() {
              document.getElementById("randImage").src = "/chk_code.aspx?" + Math.random();
          }
    </script>
    </head>
    <body class="um-vp " ontouchstart>
        <div id="page_0" class="up ub ub-ver bc-bg" tabindex="0">
            <!--header开始-->
            <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x"  onClick="javascript :history.back(-1);"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">在线付款</h1>
                <div class="nav-btn nav-bt" id="nav-right"></div>
            </div>
            <!--header结束--><!--content开始-->
            <div id="content" class="ub-f1 tx-l">
<table width="100%" border="0" cellpadding="10" class="dingdandiv">
 <tr>
    <td colspan="2" style="background-color:#999999; font-size:16px; color:#000000; line-height:30px;">订单已提交，请尽快付款！</td>
 </tr>
  <tr><input type="hidden" name="ExPws" id="ExPws" value="<%=base.sheet.SheetID %>">
    <td>您的订单号:</td>
	 <td><span class="STYLE5"><%=base.sheet.SheetID %></span></td>
  </tr>
  <tr>
    <td>应付金额:</td>
	 <td><span class="STYLE5"><%=base.sheet.TotalPrice %></span></td>
  </tr>
  <tr>
    <td width="30%">支付方式：</td>
	 <td><span class="STYLE5">在线付款</span></td>
  </tr>
  <form id="form1" runat="server">

  <tr>
    <td width="30%">支付平台：</td>
	 <td><a href="/payapi/index.aspx?type=alipay&id=<%=base.sheet.OrderID %>" target="_blank" onClick="payModal()"><img align="middle" title="支付宝" alt="支付宝" src="bootstrap/img/payment/zfb.gif"></a></td>
  </tr>
</table>
 <br />
<br /> 

   </form>


    <script type="text/javascript" src="/bootstrap/js/bootstrap.js"></script>
    <!--[if lte IE 6]>
    <script type="text/javascript" src="/js/bootstrap-ie.js"></script>
    <![endif]-->
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                if ($.isFunction($.bootstrapIE6)) $.bootstrapIE6($(document));
            });
        })(jQuery);


    </script>
            </div>
            <!--content结束-->

        </div>

    </body>

</html>
