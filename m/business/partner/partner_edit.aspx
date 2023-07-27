<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="partner_edit.aspx.cs" Inherits="ETicket.Web.business.partner.partner_edit" %>


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
                                <h2>编辑资料</h2>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="height:30px;vertical-align:middle;">账号:</td>
                            <td>
                                <asp:Literal ID="litUserName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="height:30px; vertical-align:middle;">
                                所属级别:
                            </td>
                            <td>
                               <asp:Literal ID="litLevel" runat="server"></asp:Literal>
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
                               <input type="text" id="txtLinkPhone" name="txtLinkPhone" runat="server"/> 注：积分支付、找回密码功能需要用手机获取验证码，请正确填写
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
                            <td class="label">座机 :</td>
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
                            <td class="label">支付方式 :</td>
                            <td>
                               <asp:CheckBox ID="chkOnline" runat="server" Text="在线支付" Enabled="false" />
                               <asp:CheckBox ID="chkAccount" runat="server" Text="积分支付" Enabled="false"/>
                               <asp:CheckBox ID="chkCash" runat="server" Text="现金支付" Enabled="false"/>
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

