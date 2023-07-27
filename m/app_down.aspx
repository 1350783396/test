<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_down.aspx.cs" Inherits="ETicket.Web.app_down" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>心客网</title>
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>

    <!-- Le styles -->
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap.css">

    <!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap-ie6.css">
    <![endif]-->
    <!--[if lte IE 7]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/ie.css">
    <![endif]-->
    <link rel="stylesheet" type="text/css" href="/bootstrap/index.css">
    <style type="text/css">
    </style>
   

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
   
</head>

<body>

   <!--#include file="/templ/_head.html"-->

    <div id="main" >
        <div class="container" style="background-color:#ffffff;">
            <div class="row">
                <div class="span20" style="text-align:center;">
                   <img src="/bootstrap/app_down.png"  border="0" usemap="#Map" />
                    <map name="Map" id="Map">
                        <area shape="rect" coords="372,346,621,411" href="shj_android.apk" target="_blank" alt="Android客户端下载" />
                        <area shape="rect" coords="372,439,620,507" href="https://itunes.apple.com/cn/app/xin-ke-wang/id955075050?mt=8" target="_blank" alt="iPhone客户端下载"/>
                    </map>
                </div>
            </div>
         </div> <!-- /container -->
    </div>
    
    <!--#include file="/templ/_foot.html"-->

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/bootstrap/js/bootstrap.js"></script>
    <!--<script type="text/javascript" src="js/kefu.js"></script>-->
    <!--[if lte IE 6]>
    <script type="text/javascript" src="/js/bootstrap-ie.js"></script>
    <![endif]-->
</body>
</html>
