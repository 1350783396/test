<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="refund_account.aspx.cs" Inherits="ETicket.Web.refund_account" %>

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
    </head>
    <body class="um-vp " ontouchstart>

            <!--header开始-->
            <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="window.open('/');"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">订单退款</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-refresh umw2 umh4" onClick="window.open('#');"></div>
                </div>
            </div>
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>

<style type="text/css">
         #logo
        {
            float: none;
            margin: 0;
            padding: 10px 0;
            position: relative;
        }
        #logo b 
        {
            background: url("/bootstrap/logo/head-info.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
            height: 75px;
            left: 110px;
            position: absolute;
            top: 13px;
            width: 350px;
        }
        #foot
        {
            margin-top:20px;
        }

        td.title {
            vertical-align: middle;
            line-height: 25px;
            width: 70px;
        }
    </style>
	<div class="span6">
                    <h4 align="left" style="color: red">申请退款</h4>
                    <table width="357" border="3">
                        <tr>
                            <td width="130" class="title">单价：</td>
                            <td width="207">￥
                          <asp:Literal ID="litUnitPrice" runat="server"/></td>
                        </tr>
                        <tr>
                            <td class="title">购买数量：</td>
                            <td style="vertical-align: middle;"><asp:Literal ID="litNum" runat="server"/> </td>
                        </tr>
                        <tr>
                            <td class="title">退款金额：</td>
                            <td>
                                <h4 style="color: red">￥<asp:Literal ID="litTotalPrice" runat="server"/></h4>
                            </td>
                        </tr>
                        <tr>
                            <td height="27" colspan="2" class="title">
                                申请退款原因：                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtReason" runat="server" Height="50" Width="350" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span style="color: red">积分支付，退款成功后相应积分返回到积分账号</span>                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnSave" runat="server" Text="提交申请" Width="170" CssClass="btn btn-primary" OnClientClick="return check()" />
                            </td>
                        </tr>
                </table>
           </div>
            <!--header结束--><!--content开始-->
            <!--content结束-->
    </body>
</html>