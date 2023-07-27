<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list-line.aspx.cs" Inherits="ETicket.Web.content.list_line" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>旅游线路-</title>
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

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
  
</head>

<body>
    <!--#include file="/templ/_head.html"-->

    <div id="main" >
        <div class="container" style="background-color:#ffffff">
            <div class="row">
                <div class="span20">
                    <h2>旅游线路</h2>
                    <ul class="thumbnails" style="padding-top:20px;">
                        <%=ETicket.Web.HtmlController.Instance.ListProduct("line",50,"/templ/index/product.html") %>
                    </ul>
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
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                if ($.isFunction($.bootstrapIE6)) $.bootstrapIE6($(document));
            });
        })(jQuery);

    </script>
</body>
</html>

