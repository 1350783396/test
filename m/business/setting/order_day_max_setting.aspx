﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_day_max_setting.aspx.cs" Inherits="ETicket.Web.business.setting.order_day_max_setting" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
    
    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script  type="text/javascript" src="/js/chkSelectAll.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content" class="clearfix">
                <div id="main">
                    <br />
                    <br />
                    <table class="formTable" style="width: 900px;">
                        <tr>
                            <td  style="height:25px;">
                                最大可预定天数：<input type="text" id="txtDay" name="txtDay" runat="server" />天&nbsp;&nbsp;<asp:Button runat="server" ID="btnSave" Text="保存" />
                            </td>
                        </tr>
                         <tr>
                            <td  style="height:25px;">
                                <b style="color:red"><asp:Literal ID="litMemo" runat="server"></asp:Literal></b>
                            </td>
                         </tr>
                    </table>
                    <br />
                </div>
            </div>
        </div>
    </form>
    <br />
    <br />
</body>
</html>