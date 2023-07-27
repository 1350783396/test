<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="valid_edit.aspx.cs" Inherits="ETicket.Web.business.user.valid_edit" %>


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
                                <h4>编辑验票账号</h4>
                            </td>
                        </tr>
                        <tr>
                            <td class="label"><s>*</s>账号:</td>
                            <td>
                                <input type="text" id="txtUserName" name="txtUserName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">密码:</td>
                            <td>
                                <input type="password" id="txtPassword" name="txtPassword" runat="server" /><s> 如不需要更改密码，留空</s>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="label">
                                <s>*</s>选择可验专线:
                            </td>
                            <td>
                              <asp:CheckBoxList ID="chkListLine" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical" RepeatColumns="5">

                              </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <s>*</s>选择可验景区:
                            </td>
                            <td>
                              <asp:CheckBoxList ID="chkListTicket" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical" RepeatColumns="5">

                              </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                真实姓名：
                            </td>
                            <td>
                                <input type="text" id="txtRealName" name="txtRealName" runat="server"/>
                            </td>
                        </tr>

                        <tr>
                            <td class="label">手机号码 :
                            </td>
                            <td>
                               <input type="text" id="txtPhone" name="txtPhone" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">邮箱 :</td>
                            <td>
                                <input type="text" id="txtEmail" name="txtEmail" runat="server"/>
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

