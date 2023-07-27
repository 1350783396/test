<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="member_list.aspx.cs" Inherits="ETicket.Web.business.user.member_list" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
                <table class="formTable">
                    <tr>
                        <td class="label">
                            用户名
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserName" runat="server"  Width="200" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            联系电话
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPhone" runat="server" Width="200"/>
                        </td>
                    </tr>
                </table>
                <br />
               
                <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" />
                <h3>
                    <asp:Literal ID="lblCount" runat="server" /></h3>
                <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table class="table" cellspacing="0" border="1">
                            <thead>
                                <tr>
                                    <th style="display:none">
                                        选择
                                    </th>
                                     <th>
                                        用户名
                                    </th>
                                    <th>
                                        姓名
                                    </th>
                                    <th>
                                        手机号码
                                    </th>
                                    <th>
                                        注册日期
                                    </th>
                                     <th>
                                        操作
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="display:none">
                                <input type="checkbox" id="ChkOne" name="ChkOne" value='<%# Eval("UserID") %>' />
                                <asp:Label ID="lblUserID" runat="server" Visible="false" />
                            </td>
                            <td>
                                <%# Eval("UserName") %>
                            </td>
                            <td>
                                <%# Eval("RealName") %>
                            </td>
                            <td>
                                <%# Eval("Phone") %>
                            </td>
                           
                             <td>
                                <%# Eval("RegTime") %>
                            </td>
                             <td align="left" class="ht5" style="width: 30%">
                                <asp:HyperLink ID="hyEdit" runat="server">编辑</asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
                <asp:Button ID="Button1" Text="删除所选" Width="100" runat="server"  Visible="false" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" />
                &nbsp; &nbsp;
                <asp:Button ID="btnSortSave" Text="保存排序" Width="100" runat="server" CssClass="btn"  Visible="false"/>
                <br />
                <div style="text-align: center">
                    <%--<cc1:Pager ID="Pager1" runat="server">
                    </cc1:Pager>--%>
                </div>
            </div>
        </div>
    </div>
    </form>
    <br />
    <br />
</body>
</html>

