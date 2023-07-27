<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="refund_cash.aspx.cs" Inherits="ETicket.Web.refund_cash" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>申请退款</title>
     <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link rel="stylesheet" type="text/css" href="bootstrap/css/bootstrap.css">

    <!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="bootstrap/css/bootstrap-ie6.css">
    <![endif]-->
    <!--[if lte IE 7]>
    <link rel="stylesheet" type="text/css" href="bootstrap/css/ie.css">
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
        td.title {
            vertical-align: middle;
            line-height: 25px;
            width:70px;
        }
    </style>
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
                    <img src="/bootstrap/logo/head-logo.png" alt="心客网" height="75" width="100" />
                </a>
                <b></b>
            </div>
         </div>
        <form runat="server">
            <div class="row" style="background-color: #fff;">
                <div class="span12"><h2><asp:Literal ID="litProductName" runat="server"/></h2></div>
                <div class="span6">
                    <h4>预定须知：</h4>
                    <p style="min-height:120px;">
                       <asp:Literal ID="litRule" runat="server"/>                   
                    </p>
                    <h4>产品详情：</h4>
                    <p style="min-height:120px;">
                       <asp:Literal ID="litDetail" runat="server"/>
                    </p>
                </div>
                <div class="span6">
                    <h4 style="color: red">申请退款</h4>
                    <table>
                        <tr>
                            <td class="title">单价：</td>
                            <td>￥<asp:Literal ID="litUnitPrice" runat="server"/></td>
                        </tr>
                        <tr>
                            <td class="title">购买数量：</td>
                            <td style=" vertical-align:middle;"><asp:Literal ID="litNum" runat="server"/></td>
                        </tr>
                        <tr>
                            <td class="title">退款金额：</td>
                            <td><h4 style="color:red">￥<asp:Literal ID="litTotalPrice" runat="server"/></h4></td>
                        </tr>
                        <tr>
                            <td class="title" colspan="2">
                                申请退款原因：
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtReason" runat="server" Height="50" Width="350" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span style="color:red">订单使用现金支付购买，审核通过后请到财务处领取退款</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center;">
                                <asp:Button ID="btnSave" runat="server" Text="提交申请" Width="170" CssClass="btn btn-primary" OnClientClick="return check()" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </form>
        <div id="foot">
            <p style="text-align:center;">Copyright©2014 <a href="http://www.tianshuntour.com">阳朔天顺旅行社</a> 版权所有 桂公网安备 <a href="http://www.beian.gov.cn/portal/registerSystemInfo?recordcode=45032102450338" target="_blank" style="color:blue">45032102450338号</a></p>
        </div>
    </div><!-- /container -->

    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>

    <script type="text/javascript" src="bootstrap/js/bootstrap.js"></script>
    <!--[if lte IE 6]>
    <script type="text/javascript" src="bootstrap/js/bootstrap-ie.js"></script>
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