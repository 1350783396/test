﻿<title>智慧游</title>
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
        <style type="text/css">
<!--
.STYLE1 {font-size: 36px}
-->
        </style>
</head>
    <body class="um-vp " ontouchstart>
<div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="window.open('/dingdan/index.aspx');"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">订单退款</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-refresh umw2 umh4" onClick="window.open('#');"></div>
                </div>
            </div>

            <!--header开始-->
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>

  </tr>
</table>

            <!--header结束--><!--content开始-->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="refund_account.aspx.cs" Inherits="ETicket.Web.refund_account" %>


<!DOCTYPE html>
<html lang="en"><!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="bootstrap/css/bootstrap-ie6.css">
    <![endif]-->
    <!--[if lte IE 7]>
    <link rel="stylesheet" type="text/css" href="bootstrap/css/ie.css">
    <![endif]--><!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
<div class="container">
         <!--头部-->
         <form runat="server">
           <!-- <div class="row" style="background-color: #fff;">
              <!--<div class="span6">
                    <!--       <h4>预定须知：</h4>   -->
                    <p style="min-height: 120px;">
                    <!--   <asp:Literal ID="litRule" runat="server"/> -->
                    </p>
                    
                  <!--   <h2><asp:Literal ID="litProductName" runat="server"/></h2> -->
             
                    <p style="min-height: 50px;">
                    <!-- <asp:Literal ID="litDetail" runat="server"/>   -->
                    </p>
    
              <div class="span1">
                    <h4 align="center" style="color: red">申请退款</h4>
                    <div align="center">
                      <table width="357" border="3" align="center">
                        <tr>
                          <td width="130" class="title">单价：</td>
                            <td width="207">￥
                          <asp:Literal ID="litUnitPrice" runat="server"/></td>
                          </tr>
                        <tr>
                          <td class="title">购买数量：</td>
                            <td style="vertical-align: middle;"><asp:Literal ID="litNum" runat="server"/> </td>
                          </tr>
                        <tr>
                          <td class="title">退款金额：</td>
                            <td>
                                <h4 style="color: red">￥<asp:Literal ID="litTotalPrice" runat="server"/></h4>                            </td>
                          </tr>
                        <tr>
                          <td height="27" colspan="2" class="title">
                            申请退款原因：                            </td>
                          </tr>
                        <tr>
                          <td colspan="2">
                            <asp:TextBox ID="txtReason" runat="server" Height="50" Width="350" TextMode="MultiLine"></asp:TextBox>                            </td>
                          </tr>
                        <tr>
                          <td colspan="2">
                            <span style="color: red">积分支付，退款成功后相应积分返回到积分账号</span>                            </td>
                          </tr>
                        <tr>
                          <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="提交申请" Width="170" CssClass="btn btn-primary" OnClientClick="return check()" />                            </td>
                          </tr>
                          </table>
                    </div>
           </div>

    </form>
    </div>
        <!-- /container -->
   
    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>

    <script type="text/javascript" src="/bootstrap/js/bootstrap.js"></script>
    <!--[if lte IE 6]>
    <script type="text/javascript" src="/bootstrap/js/bootstrap-ie.js"></script>
    <![endif]-->
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                if ($.isFunction($.bootstrapIE6)) $.bootstrapIE6($(document));
            });
        })(jQuery);
    </script>
    <script type="text/javascript">
          function check() {
              var s = $("#<%=txtReason.ClientID%>").val();
             if (s == "") {
                 alert('请输入申请退款原因');
                 return false;
             }
             else {
                 sAlert("系统正在申请退款........请稍等");
                 return true;
             }
         }
    </script>
</body>
</html>
