<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view_list.aspx.cs" Inherits="ETicket.Web.business.art.view_list" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title></title>
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
            padding-top: 10px;
            padding-bottom: 40px;
            margin-left: 40px;
        }
    </style>

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/common.js"></script>
    <script type="text/javascript" src="/js/btnReSubmit.js"></script>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

</head>

<body>
    <!-- <div style="background-color: #ccc;height:50px;width:940px;margin:auto;"></div> -->




    <form id="form1" runat="server">
        

        <div class="row" style="background-color: #fff;">
            <div class="span12">
                
                               
                            
                                 <p style="font-size:14px; font-weight:700; line-height:22px;">
                                        投诉电话：182-7838-4444
                                     <span style="color:#F00; ">
                                     
                                     <img src="/images/kefu.jpg">
                                    <span style="font-size:24px;">0773-2300400</span>
                                    <%-- 投诉电话：<span style="font-size:24px;">138-7733-4444</span>--%>
                                     </span>
                                 </p>						
                                	
                           
            </div>
            <div class="span12" style="padding: 3px 5px; height: 24px; background-color: #CAE8EA;">
                <div>
                    <span style="font-size: 22px; font-weight: bold; width: 80%;color: #006699">通知公告</span>
                    <span style="float: right; width: 20%">
                        <asp:Button ID="btnRefresh" runat="server" Text="刷新" Width="100" OnClientClick="sAlert('正在重新加载数据。。。。请稍等')" /></span>
                </div>
                <asp:Literal ID="litResultList" runat="server" EnableViewState="false"></asp:Literal>
                <div style="text-align:center;">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="100">
                    </webdiyer:AspNetPager>
                </div>
            </div>
        </div>   
     <br />
    <br />
    </form>
    <div class="container">
    </div>
    <!-- /container -->

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