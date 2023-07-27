<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_succ.aspx.cs" Inherits="ETicket.Web.pay_succ" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
   
    <title>支付成功-心客网</title>
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
        .SuccInfo {
            background: url("bootstrap/img/wcdd.gif") no-repeat scroll left 10px rgba(0, 0, 0, 0);
            font-size: 26px;
            font-weight: bold;
            line-height: 2em;
            padding: 10px 10px 10px 50px;
        }
        .ErrorInfo {
            background: url("bootstrap/img/no.gif") no-repeat scroll left 10px rgba(0, 0, 0, 0);
            font-size: 26px;
            font-weight: bold;
            line-height: 2em;
            padding: 10px 10px 10px 50px;
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
        <div class="row" style="background-color: #fff;height:400px;text-align:center;vertical-align:middle;">
            <div class="span12" style="padding-top:100px;">
                <b class="<%=style %>"><%=msg %></b>
            </div>
                
            
        </div>
        <div id="foot">
            <p style="text-align:center;">Copyright©2014 <a href="http://www.tianshuntour.com">心客网</a> 版权所有 工信部备案号：桂ICP备13001063号</p>
        </div>
    </div>
</body>
</html>

