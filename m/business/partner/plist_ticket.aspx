<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plist_ticket.aspx.cs" Inherits="ETicket.Web.business.partner.plist_ticket" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>景区下单</title>
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

        .item {
            width: 350px;
            margin: 10px 10px;
            float: left;
        }
.STYLE3 {color: #009933; font-weight: bold; }
    .STYLE4 {
	font-size: 14px;
	color: #FF0000;
}
.STYLE7 {color: #FF0000; font-weight: bold; font-size: 14px; }
    </style>

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/common.js"></script>
    <script type="text/javascript" src="/js/OpenTab.js"></script>
    <script type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script type="text/javascript" src="/js/chkSelectAll.js"></script>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->


</head>

<body>
    <form runat="server">
        <div class="container-fluid">
          <div class="row-fluid">
                <div class="span12">
                    <div class="bs-docs-example form-horizontal">
                        <div class="control-group">
                            <label class="control-label" for="txtUserName">景区名称</label>
                          <div class="controls">
                                <input type="text" id="txtProductName" name="txtProductName" runat="server">
                                <button type="button" class="btn" id="btnSerch" runat="server" onClick="sAlert('正在搜索。。。请稍等');">搜索</button>
                                &nbsp;&nbsp;
                                <button type="button" class="btn" id="btnClear" runat="server" onClick="sAlert('正在清空搜索结果，显示所有数据。。。请稍等');">清空搜索</button>
                          </div>
                        </div>
                    </div>
					
</div>
<div style="text-align: center">
<link rel="stylesheet" href="2020/css/bootstrap.css">
<style media="screen">
.table{text-align: center;}
</style>

</head>
<body><script src="/demos/googlegg.js"></script>

<table class="table table-bordered table-hover">
	<tr class="info">
		<td width="150"><div align="center">景区名称</div></td>
		<td width="110"><div align="center">景区电话</div></td>
		<td width="650"><div align="center">备注</div></td>
		<td width="65"><div align="center">门市价格</div></td>
		<td><div align="center">操作</div></td>
	</tr>
	<tr>	</tr>
</table>
  
<script src="2020/js/jquery.min.js"></script>
<script src="2020/js/table.js"></script>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="50">
                        </webdiyer:AspNetPager>
</div>
                    <h5>
                        <asp:Literal ID="lblCount" runat="server" /></h5>
                    <p>
                      <asp:Literal ID="litResultList" runat="server" EnableViewState="false"></asp:Literal>
                      <br />
                      <br />
    </p>
          </div>
            </div>
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
