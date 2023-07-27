<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myqr_code_shop.aspx.cs" Inherits="ETicket.Web.business.partner.myqr_code_shop" %>



<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>专线二维码</title>
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
        body {
            padding-top: 30px;
           
        }
        .item{
            width:350px;
            margin: 10px 10px;
            float:left;
        }
    </style>

    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script  type="text/javascript" src="/js/chkSelectAll.js"></script>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->


</head>

<body >
    <form runat="server">
        <div class="container-fluid">
         <div class="row-fluid">
        <div class="span12">
             
               <h4><asp:Literal ID="lblCount" runat="server" /></h4>
               <h5 style="color:red;">注：二维码可做在海报或广告等地方，游客直接扫码即可购买电子票</h5>
               <br />
               <br />
               <asp:Image runat="server" ID="imgQR" />
               <br />
               <br />
                <asp:HyperLink ID="hyDownLoad" runat="server" />
                <br />
               <br />
                <asp:Literal ID="litResultList" runat="server" EnableViewState="false"></asp:Literal>
               <br />
               <br />
               <div style="text-align: center">
                    
               </div>
        </div>
        </div>
        </div>
        <br />
    <br />
    </form>
    <!-- Le javascript
    ================================================== -->
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