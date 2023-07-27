<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_account_back.aspx.cs" Inherits="ETicket.Web.pay_account" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>成功提交订单-顺和嘉票务网</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link rel="stylesheet" type="text/css" href="/bootstrap/css/bootstrap.css">

    <!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css/bootstrap-ie6.css">
    <![endif]-->
    <!--[if lte IE 7]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css/ie.css">
    <![endif]-->

    <style type="text/css">
         #logo
        {
            float: none;
            margin: 0;
            padding: 10px 0;
            position: relative;
        }
        #logo b 
        {
            background: url("/bootstrap/logo/head-info.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
            height: 75px;
            left: 110px;
            position: absolute;
            top: 13px;
            width: 350px;
        }
        #foot
        {
            margin-top:20px;
        }
        .tip_1 {
            background: url("bootstrap/img/wcdd.gif") no-repeat scroll left 10px rgba(0, 0, 0, 0);
            font-size: 16px;
            font-weight: bold;
            line-height: 2em;
            padding: 10px 10px 10px 50px;
        }
        .tip_5 {
            margin-bottom: 3px;
        }
        .tip_6 {
            font-size: 16px;
            font-weight: bold;
            margin: 20px 0 10px;
        }
        .banks dt {
            clear: both;
            margin-bottom: 10px;
        }
        .banks dd {
            float: left;
            height: 65px;
            margin-bottom: 0;
            overflow: hidden;
            text-align: center;
            width:200px;
        }
        td.title {
            vertical-align: middle;
           line-height:40px;
        }
    </style>

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/common.js"></script>
    <script type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script type="text/javascript">
          function check() {
              var s = $("#txtValidCode").val();
              if (s == "") {
                  alert('请输入短信验证码');
                  return false;
              }
              else {
                  sAlert("系统正在处理........请稍等");
                  return true;
              }
          }
    </script>
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->


</head>

<body style="background-color: #ededf0;">
    <%=ETicket.Web.HtmlController.Instance.TopBarHtml("/templ/topbar/other.html") %>
    <div class="container">
         <!--头部-->
         <div id="head">
              <div id="logo">
                <a href="/index.aspx">
                    <img src="/bootstrap/logo/head-logo.png" alt="顺和嘉" height="75" width="100" />
                </a>
                <b></b>
            </div>
         </div>
        <div class="row" style="background-color: #fff;height:400px;">
            <div class="span12">
                <div class="tip_1"> 订单已提交，请尽快付款！</div>
                <div class="tip_5">
                    <p>
                        您的订单号：
                        <strong style="color:red;"><%=base.sheet.SheetID %></strong>
                        应付金额：
                        <strong style="color:red;"><%=base.sheet.TotalPrice %></strong>
                        元
                        &nbsp; &nbsp; 支付方式：<strong style="color:red;">积分支付</strong>
                    </p>
                </div>
                <div class="tip_6">
                   还差一步,请立即支付(请您在40分钟内付清款项,否则订单会被自动取消)
                </div>
            </div>
           
            <div class="span12">
                <form id="form1" runat="server">
                   <h4>积分支付<small style="color:red;margin-left:5px;">当前账号可用积分：<%=account%>，1积分=1元</small></h4>
                   <table>
                          <tr>
                              <td class="title">输入短信验证码：</td>
                              <td class="title">
                                  <input type="text" style="width:100px;margin:0 0 ;" id="txtValidCode" name="txtValidCode" runat="server" />&nbsp;&nbsp;
                                   <input type="button" style="background: url(/bootstrap/img/smail.jpg) no-repeat scroll 0 0 transparent; border: 0 none; cursor: pointer; height: 30px; width: 150px" id="sendCodeBtn" onclick="sendPayCode('sendCodeBtn');" value="获取短信验证码"/>

                              </td>
                          </tr>
                          <tr>
                              <td style="text-align:center;" colspan="2">
                                  <asp:Button ID="btnSave" runat="server" Text="确定支付" Width="170" CssClass="btn btn-primary" OnClientClick="return check()" />
                              </td>
                          </tr>
                   </table>
                </form>
            </div>
        </div>
        <div id="foot">
            <p style="text-align:center;">Copyright©2014 <a href="http://www.shunhejia.com">阳朔嘉和顺国际旅社</a> 版权所有 工信部备案号：桂ICP备13001063号</p>
        </div>
    </div><!-- /container -->

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

</body>
</html>
