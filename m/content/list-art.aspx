<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list-art.aspx.cs" Inherits="ETicket.Web.content.list_art" %>

<title>÷«ª€”Œ</title>
<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title></title>
		<style type="text/css">
<!--
.table {
	background-color: #FFFFFF;
	width: 100%;
	border: 1px solid #CCCCCC;
}
.table a{
	color: #333333;
	text-decoration: none;

}
.table td{
	text-align: left;
	border-top-width: 1px;
	border-bottom-width: 1px;
	border-left-width: 1px;
	border-bottom-style: solid;
	border-top-color: #CCCCCC;
	border-right-color: #CCCCCC;
	border-bottom-color: #CCCCCC;
	border-left-color: #CCCCCC;
	padding-top: 10px;
	padding-right: 3px;
	padding-bottom: 10px;
	padding-left: 3px;
	border-right-style: none;
}
.table th{
	padding: 10px;
	border-top-width: 1px;
	border-right-width: 1px;
	border-bottom-width: 1px;
	border-left-width: 1px;
	border-bottom-style: solid;
	border-top-color: #CCCCCC;
	border-right-color: #CCCCCC;
	border-bottom-color: #CCCCCC;
	border-left-color: #CCCCCC;
}
-->
        </style>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"><head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta name="viewport" content="target-densitydpi=device-dpi, width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
        <link rel="stylesheet" href="/css/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="/css/ui-box.css">
        <link rel="stylesheet" href="/css/ui-base.css">
        <link rel="stylesheet" href="/css/ui-color.css">
        <link rel="stylesheet" href="/css/appcan.icon.css">
        <link rel="stylesheet" href="/css/appcan.control.css">
        <link rel="stylesheet" href="/ticket_content/css/main.css">
    </head>
	 <script type="text/javascript">window.onload(){
    if(location.href.indexOf('#reloaded')==-1){
    location.href=location.href+"#reloaded";
    location.reload();
    }
    }</script>
    <body class="um-vp bc-bg" ontouchstart>
	    <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="javascript :history.back(-1);"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0"><%=ModuleName %></h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-refresh umw2 umh4"  onclick="window.open('/content/list-art.aspx?mid=10','_self')"></div>
                </div>
            </div>
        <div class="umar-a sc-bg" id="goodsList">
      <table class="table">
                        <asp:Literal ID="litHtml" runat="server" EnableViewState="false" />
                    </table>
        </div>

    </body>
  
</html>


