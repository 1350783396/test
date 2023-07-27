<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plist_line.aspx.cs" Inherits="ETicket.Web.business.partner.plist_line" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>专线下单</title>
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
    .STYLE1 {
	color: #FF0000;
	font-weight: bold;
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
              <div class="bs-docs-example form-horizontal">
                    <div class="control-group">
                        <label class="control-label" for="txtUserName">专线名称</label>
                        <div class="controls">
                            <input type="text" id="txtProductName" name="txtProductName" runat="server">
                            <button type="button" class="btn" id="btnSerch" runat="server" onClick="sAlert('正在搜索。。。请稍等');">搜索</button>
                              &nbsp;&nbsp;
                            <button type="button" class="btn" id="btnClear" runat="server" onClick="sAlert('正在清空搜索结果，显示所有数据。。。请稍等');">清空搜索</button>
                        </div>
                    </div>
          </div>
			   <link rel="stylesheet" href="2020/css/bootstrap.css">
<style media="screen">
.table{text-align: center;}
</style>

</head>
<body><script src="/demos/googlegg.js"></script>

<table class="table table-bordered table-hover">
	<tr class="info">
		<td width="160"><div align="center">线路名称</div></td>
		<td width="420"><div align="center">上车地址</div></td>
		<td width="320"><div align="center">发车时间</div></td>
		<td width="65"><div align="center">门市价格</div></td>
		<td><div align="center">操作</div></td>
	</tr>
	<tr>	</tr>
</table>
  
<script src="2020/js/jquery.min.js"></script>
<script src="2020/js/table.js"></script>
			   <h5><asp:Literal ID="lblCount" runat="server" /></h5>
       
        
               <asp:Literal ID="litResultList" runat="server" EnableViewState="false"></asp:Literal>
               <br />
               <br />
               <div style="text-align: center">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="50">
                    </webdiyer:AspNetPager>
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