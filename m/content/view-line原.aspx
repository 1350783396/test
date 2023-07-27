<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-line.aspx.cs" Inherits="ETicket.Web.content.view_line" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8"/>
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
    <title><%=ProductName %></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>


    <!-- Le styles -->
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap.css"/>

    <!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/bootstrap-ie6.css">
    <![endif]-->
    <!--[if lte IE 7]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css2/ie.css">
    <![endif]-->

    <link rel="stylesheet" type="text/css" href="/bootstrap/index.css">
    <link rel="stylesheet" type="text/css" href="/bootstrap/ticket_detail.css">

</head>

<body>
    <!--#include file="/templ/_head.html"-->
    <div id="main" ><!--style="background-color:#ededf0;"-->
        <div class="container" style="border:1px solid #ededf0">
                <%=PageHtml%>
        </div>
    </div>
    <!--#include file="/templ/_foot.html"-->

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/bootstrap/js/bootstrap.js"></script>
    <!--<script type="text/javascript" src="js/kefu.js"></script>-->
    <!--[if lte IE 6]>
    <script type="text/javascript" src="/js/bootstrap-ie.js"></script>
    <![endif]-->
    <script type="text/javascript" src="/js/stickUp.js"></script>
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                if ($.isFunction($.bootstrapIE6)) $.bootstrapIE6($(document));
            });
        })(jQuery);
    </script>

    <%--
    <script type="text/javascript">
        jQuery(function ($) {
            $(document).ready(function () { //为 '.navbar-wrapper' class 启用stickUp插件
                $('.detail_product_div').stickUp(
                    {
                        parts: {
                            0: 'productDetail',
                            1: 'bookNote'
                        },
                        itemClass: 'tabBtn',
                        itemHover: 'cursor',
                        marginTop: '50px'
                    }
                 );
            });
        });
    </script>--%>
</body>
</html>