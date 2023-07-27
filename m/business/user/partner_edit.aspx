<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="partner_edit.aspx.cs" Inherits="ETicket.Web.business.user.partner_edit" %>


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
                            <td colspan="2">
                                <h4>编辑分销商</h4>
                            </td>
                        </tr>
                        <tr runat="server" id="tabUser" visible="true">
                            <td class="label"><s>*</s>账号:</td>
                            <td>
                                <input type="text" id="txtUserName" name="txtUserName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">密码:
                            </td>
                            <td>
                                <input type="password" id="txtPassword" name="txtPassword" runat="server" /><s> 如不需要更改密码，留空</s>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="label">
                                <s>*</s>所属级别:
                            </td>
                            <td>
                               <asp:DropDownList ID="ddlUserLevel" runat="server">

                               </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <s>*</s>联系人：
                            </td>
                            <td>
                                <input type="text" id="txtLinkMan" name="txtLinkMan" runat="server"/>
                            </td>
                        </tr>

                        <tr>
                            <td class="label"><s>*</s>联系号码 :
                            </td>
                            <td>
                               <input type="text" id="txtLinkPhone" name="txtLinkPhone" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">邮箱 :</td>
                            <td>
                                <input type="text" id="txtEmail" name="txtEmail" runat="server"/>
                            </td>
                        </tr>
                         <tr>
                            <td class="label"><s>*</s>分销商名称 :</td>
                            <td>
                               <input type="text" id="txtCPName" name="txtCPName" style="width:300px;" runat="server"/>
                            </td>
                        </tr>
                         <tr>
                            <td class="label">电话 :</td>
                            <td>
                               <input type="text" id="txtTel" name="txtTel"  runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">公司地址 :</td>
                            <td>
                               <input type="text" id="txtCPAddress" name="txtCPAddress" style="width:300px;" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="label"><s>*</s>支付方式 :</td>
                            <td>
                               <asp:CheckBox ID="chkOnline" runat="server" Text="在线支付" />
                               <asp:CheckBox ID="chkAccount" runat="server" Text="积分支付" />
                               <asp:CheckBox ID="chkCash" runat="server" Text="现金支付" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                扫码限制：
                            </td>
                            <td>
                                <asp:CheckBox ID="chkQRIsLock" Text="是否限制扫码" runat="server" /><br /><br />
                                限制IP仅为：<input type="text" id="txtQRLockIP" name="txtQRLockIP" runat="server" style="width:300px;"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                              
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

