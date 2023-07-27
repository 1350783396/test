<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_chgpass.aspx.cs" Inherits="ETicket.Web.business.partner.user_chgpass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
    <script  type="text/javascript" src="/business/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/business/js/btnReSubmit.js"></script>
    <style type="text/css">
        .linkAction
        {
            padding:0px 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <div id="content" class="clearfix">
            <div id="main">
              
                <table class="formTable" style="width:800px;">
                    <tr >
                        <td colspan="4" >
                            <h2>修改密码</h2>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            旧密码
                        </td>
                        <td align="left" >
                           <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            新密码
                        </td>
                        <td align="left" >
                           <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            确认新密码
                        </td>
                        <td align="left" >
                           <asp:TextBox ID="txtPass2" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label id="lblMsg" runat="server" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;"><asp:Button ID="btnSave" runat="server" Text="保存" Width="100"  /></td>
                    </tr>
                </table>
                <br />
                <br />
               
            </div>
        </div>
    </div>
    </form>
    <br />
    <br />
</body>
</html>
