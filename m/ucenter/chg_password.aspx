<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chg_password.aspx.cs" Inherits="ETicket.Web.ucenter.chg_password" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>修改密码-会员中心</title>
     <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap.css">

    <!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap-ie6.css">
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
            background: url("/bootstrap/logo/head-info-2.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
            height: 75px;
            left: 110px;
            position: absolute;
            top: 13px;
            width: 350px;
        }
        .error{
            color:red;
        }
        td.title {
            vertical-align: middle;
           line-height:40px;
        }

        
.downList ul {
    background-color: #f5f5f5;
    color: #333;
    display: block;
    float: left;
    margin: 0;
    padding: 0;
    width: 170px;
}
.downList ul li {
    border-bottom: 1px solid #fff;
    display: block;
    height: 48px;
    margin: 0;
}
.downList ul li a {
    color: #333;
    display: block;
    font-size: 16px;
    height: 48px;
    line-height: 48px;
    margin: 0;
    text-align: center;
    text-decoration: none;
    vertical-align: middle;
}
.downList ul li a:hover {
    background-color: #6ca8fa;
    color: #fff;
    text-decoration: none;
}
.downList .downCk {
    color: #fff;
}
.downCk {
    background-color: #6ca8fa;
}
.callout {
    background: none repeat scroll 0 0 #fffef1;
    border: 1px solid #e5e2c8;
    border-radius: 4px;
    color: #8d8d6d;
    font-size: 13px;
    padding: 10px;
}
.tablehead
{
    background-color: #6ca8fa;
    color:#fff;
}
 .linkAction
        {
            padding:0px 3px;
            color:#6ca8fa;
        }
    </style>

    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>

    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>
   
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->


</head>

<body >
    <%=ETicket.Web.HtmlController.Instance.TopBarHtml("/templ/topbar/other.html") %>
    <div  style="background-color: #ededf0;">
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
        </div>
    </div>
    <div class="container">
         <!--内容-->
         <form id="regform" runat="server" method="post">
            <div class="row" style="background-color: #fff;padding-bottom:10px;padding-top:10px;">
                <div class="span4" style="height:400px;">
                    <div class="downList">
                        <ul>
                            <li><a id="orders" href="/ucenter/order.aspx" >我的订单</a></li>
                            <li><a id="backorder" href="/ucenter/order_back.aspx" >退款订单</a></li>
                            <li><a id="userinfo" href="/ucenter/userinfo.aspx">资料管理</a></li>
                            <li><a id="chgpass" href="/ucenter/chg_password.aspx" class="downCk">修改密码</a></li>
                        </ul>		
                    </div>
                </div>
                <div class="span16">
                    <div class="callout">
                        <span style="font-size:18px;font-weight:bold;">修改密码</span>
      
                    </div>
                    <br />
                    <br />
                    <div id="main">
                        <table class="formTable" >
                            <tr>
                                <td style="height:30px;vertical-align:middle;text-align:right;">
                                    旧密码
                                </td>
                                <td align="left" >
                                   <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:30px;vertical-align:middle;text-align:right;">
                                    新密码
                                </td>
                                <td align="left" >
                                   <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:30px;vertical-align:middle;text-align:right;">
                                    确认新密码
                                </td>
                                <td align="left" >
                                   <asp:TextBox ID="txtPass2" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label id="lblMsg" runat="server" ForeColor="Red" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:left;">
                                    <asp:Button ID="btnSave" runat="server" Text="保存" class="btn btn-primary" style="width:300px;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
         </form>
         
         
    </div><!-- /container -->
    <!--尾部-->
    <div style="border-top:2px solid #f9f9f9">
        <div class="container">
          
            <p style="text-align:center;">Copyright©2014 <a href="http://www.tianshuntour.com" style="color:blue">阳朔天顺旅行设</a> 版权所有 桂公网安备 <a href="http://www.beian.gov.cn/portal/registerSystemInfo?recordcode=45032102450338" target="_blank" style="color:blue">45032102450338号</a></p>
       
        </div>
    </div>
   
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

</body>
</html>

