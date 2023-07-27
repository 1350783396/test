<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="partner_openedit.aspx.cs" Inherits="ETicket.Web.business.user.partner_openedit" %>


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
                        <tr style="height:30px;">
                            <td colspan="2">
                                <h4>开发平台配置</h4>
                            </td>
                        </tr>
                        <tr style="height:30px;">
                            <td class="label">账号:</td>
                            <td style="width:30px;">
                                <asp:Literal ID="litUserName" runat="server"/>
                            </td>
                        </tr>
                        <tr style="height:30px;">
                            <td class="label">分销商名称 :</td>
                            <td >
                               <asp:Literal ID="litCPName" runat="server" />
                            </td>
                        </tr>
                        <tr style="height:30px;">
                            <td class="label">联系人:
                            </td>
                            <td >
                                <asp:Literal ID="litLinkMan" runat="server" />
                            </td>
                        </tr>
                        <tr style="height:30px;">
                            <td class="label">
                                联系电话:
                            </td>
                            <td >
                               <asp:Literal ID="litLinkPhone" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <s>*</s>是否开通：
                            </td>
                            <td >
                               <asp:DropDownList ID="ddlOpenStatus" runat="server" Width="100">
                                   <asp:ListItem Text="是" Value="是"></asp:ListItem>
                                   <asp:ListItem Text="否" Value="否"></asp:ListItem>
                               </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <s>*</s>ParterID：
                            </td>
                            <td >
                                <input type="text" id="txtParterID" name="txtParterID"  runat="server" style="width:300px;"/>
                                <input type="submit" id="btnGengerID" class="btn" value="自动生成Parter和Key"  onclick="sAlert('正在自动生成。。。。。。。。。请稍等！');" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label"><s>*</s>Key :
                            </td>
                            <td >
                               <input type="text" id="txtKey" name="txtKey"  runat="server" style="width:300px;"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <div style="color:red">开通状态下，请勿轻易更改Parter和Key，否则将无法接收数据</div>
                                <br />
                                <input type="submit" id="btnSave" class="btn" value="保存" style="width:100px;" onclick="sAlert('正在保存数据。。。。。。。。。请稍等！');" runat="server" />
                                &nbsp;&nbsp;
                            
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
