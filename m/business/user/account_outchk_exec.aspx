<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account_outchk_exec.aspx.cs" Inherits="ETicket.Web.business.user.account_outchk_exec" %>


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
                                <h4>积分销除审核</h4>
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
                                销除积分数:
                            </td>
                            <td>
                                 <b><asp:Literal ID="litAccount" runat="server"></asp:Literal></b> &nbsp; &nbsp;
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
                               <asp:Literal ID="litRemarkRequst" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table class="formTable" style="width: 900px;">
                        <tr>
                            <td class="label">
                                审核结果
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlResutl" runat="server">
                                    <asp:ListItem Text="请选择">请选择</asp:ListItem>
                                    <asp:ListItem Text="审核通过">审核通过</asp:ListItem>
                                    <asp:ListItem Text="审核不通过">审核不通过</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                积分申请授权码
                            </td>
                            <td>
                               <asp:TextBox id="txtChkCode" runat="server" TextMode="Password" />
                               <b style="color:red">注：审核授权码由财务人员管理（和积分充值审核相同）</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                审核说明
                            </td>
                            <td>
                                <asp:TextBox runat="server" TextMode="MultiLine" Height="100" Width="500" ID="txtRemark"/> 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center;">
                                  <input type="submit" id="btnSave" class="btn" value="提交审核" style="width:100px;" onclick=" sAlert('正在保存数据。。。。。。。。。请稍等！');" runat="server" />
                              
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

