<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account_outchk_detail.aspx.cs" Inherits="ETicket.Web.business.user.account_outchk_detail" %>


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
                    <table class="formTable" style="width: 900px;">
                        <tr>
                            <td colspan="4">
                                <h4>积分消除审核详细</h4>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="height:30px;vertical-align:middle;">
                                申请人账号
                            </td>
                            <td>
                              <asp:Literal ID="litOPUser" runat="server"></asp:Literal>
                            </td>
                            <td class="label" style="height:30px;vertical-align:middle;">
                                消除积分数:
                            </td>
                            <td>
                                 <asp:Literal ID="litAccount" runat="server"></asp:Literal> &nbsp; &nbsp;
                            </td>
                        </tr>
                       <tr>
                            <td class="label" style="height:30px;vertical-align:middle;">
                                分销商名称
                            </td>
                            <td>
                              <asp:Literal ID="litCPName" runat="server"></asp:Literal>
                            </td>
                            <td class="label" style="height:30px;vertical-align:middle;">
                                分销商登录账号
                            </td>
                            <td>
                                 <asp:Literal ID="litUserName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                         <tr>
                            <td class="label" style="height:30px;vertical-align:middle;">
                                分销商联系人
                            </td>
                            <td>
                              <asp:Literal ID="litRealName" runat="server"></asp:Literal>
                            </td>
                            <td class="label" style="height:30px;vertical-align:middle;">
                                分销商联系电话
                            </td>
                            <td>
                                 <asp:Literal ID="litPhone" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="height:30px;vertical-align:middle;">申请说明 :</td>
                            <td colspan="3">
                               <asp:Literal ID="litRemarkRequest" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="height:30px;vertical-align:middle;">
                                审核结果
                            </td>
                            <td colspan="3">
                                <asp:Literal ID="litStatus" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="height:30px;vertical-align:middle;">
                                审核说明
                            </td>
                            <td colspan="3">
                                <asp:Literal ID="litRemak" runat="server" />
                            </td>
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

