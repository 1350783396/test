<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_chgpass.aspx.cs" Inherits="ETicket.Web.business.partner.user_chgpass" %>
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
	font-size: 12px;
}
.table td{
	text-align: center;
	border-top-width: 1px;
	border-right-width: 1px;
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
.STYLE1 {color: #FF0000}
-->
        </style>
<head id="Head1" runat="server">
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
    <body class="um-vp bc-bg" ontouchstart>
	    <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="javascript :history.back(-1);"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">密码修改</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-refresh umw2 umh4"  onclick="window.open('/business/partner/order_list.aspx','_self')"></div>
                </div>
    </div>
	
<div class="umar-a sc-bg" id="goodsList"></div>

<!DOCTYPE html>
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
     <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script  type="text/javascript" src="/js/chkSelectAll.js"></script>
    <form name="form" id="form" runat="server">
        <div id="page">
            <div id="content" class="clearfix">
                <div id="main">
                    <table width="450" height="290" class="formTable" style="width: 450px;">
                        <tr>
                          <td colspan="2"><h2 align="center" class="STYLE1"> 密码修改</h2>                            </td>
                        </tr>
                        <tr>
                            <td width="141" class="label" style="height:30px;vertical-align:middle;">旧密码:</td>
                            <td width="297">
                          <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password"></asp:TextBox>                          </td>
                        </tr>
                        <tr>
                            <td class="label" style="height:30px; vertical-align:middle;">
                                新密码:                            </td>
                            <td>
                               <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>                            </td>
                        </tr>
                        <tr>
                            <td class="label">确认密码：                            </td>
                            <td>
                                <asp:TextBox ID="txtPass2" runat="server" TextMode="Password"></asp:TextBox>                            </td>
                        </tr>
						</tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label id="lblMsg" runat="server" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <tr>
                            <td colspan="2" style="text-align:center;"><asp:Button ID="btnSave" runat="server" Text="保存" Width="100"  /></td>
                        </tr>
                  </table>
              </div>
            </div>
        </div>
    </form>
</body>
</html>


