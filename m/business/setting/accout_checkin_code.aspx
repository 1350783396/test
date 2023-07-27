<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accout_checkin_code.aspx.cs" Inherits="ETicket.Web.business.setting.accout_checkin_code" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/common.js"></script>
    <script type="text/javascript" src="/js/OpenTab.js"></script>
    <script type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script type="text/javascript" src="/js/chkSelectAll.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content" class="clearfix">
                <div id="main">
                    <br />
                    <font style="color:red">提醒：更改积分审核授权码以后，请记住新的授权码</font>
                    <br />
                    <br />
                    <br />
                    <table class="formTable" style="width: 900px;">
                        <tr>
                            <td class="label" style="color:red">旧的积分审核授权码： </td>
                            <td style="height: 25px;">
                                <input type="password" id="txtOld" name="txtOld" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">新的积分审核授权码： </td>
                            <td style="height: 25px;">
                                <input type="password" id="txtNew" name="txtNew" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">确认积分审核授权码： </td>
                            <td style="height: 25px;">
                                 <input type="password" id="txtNew2" name="txtNew2" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 25px;" colspan="2">
                                <asp:Button runat="server" ID="btnSave" Text="保存更改" Width="100" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 25px;" colspan="2">
                                <b style="color: red">
                                    <asp:Literal ID="litMemo" runat="server"></asp:Literal></b>
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
