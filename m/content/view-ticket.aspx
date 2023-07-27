<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-ticket.aspx.cs" Inherits="ETicket.Web.content.view_ticket" %>
<title>智慧游</title>
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
        <link rel="stylesheet" href="/product_content/css/main.css">
    </head>
    <body class="um-vp " ontouchstart>
        <div id="page_0" class="up ub ub-ver" tabindex="0">
            <!--header开始-->
            <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="window.open('/content/list-ticket.aspx');"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0" id="titlePro">详情</h1>
                <div class="nav-btn nav-bt" id="nav-right"></div>
            </div>
            <!--header结束--><!--content开始-->
            <div id="content" class="ub-f1 tx-l">
 <%=PageHtml%>
            </div>
            <!--content结束-->
        </div>

    </body>

</html>
