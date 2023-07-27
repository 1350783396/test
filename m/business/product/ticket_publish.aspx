<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ticket_publish.aspx.cs" Inherits="ETicket.Web.business.product.ticket_publish" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <link rel="stylesheet" type="text/css" href="/business/style/nav/nav.css" />
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
                            <td colspan="2">
                              <div>
                                <div id="divProduct" runat="server"><asp:HyperLink ID="hyProduct" runat="server" Text="景区基本信息" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>

                                <div id="divPrice" runat="server"><asp:HyperLink ID="hyPrice" runat="server" Text="价格" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>

                                <div  id="divPublish" runat="server"><asp:HyperLink ID="hyPublish" runat="server" Text="发布" /></div>
                                 
                                <div style="clear: both"></div>
                             </div>
                           
                            <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">条件检查:</td>
                            <td>
                                <asp:Literal ID="litChk" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">发布状态:
                            </td>
                            <td>
                              <asp:CheckBox ID="chkSale" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;注：勾选为发布状态
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnSave" runat="server" Text="保存" Width="100" OnClientClick="sAlert('正在保存数据。。。。。。。。。请稍等！');" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
    <br />
    <br />
</body>
</html>
