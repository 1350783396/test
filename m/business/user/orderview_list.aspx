<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderview_list.aspx.cs" Inherits="ETicket.Web.business.user.orderview_list" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
                            <asp:TextBox ID="txtUserName" runat="server" Width="200" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="label">
                            手机号码
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPhone" runat="server" Width="200" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CausesValidation="false" CssClass="btn" OnClientClick="sAlert('系统正在查询中。。。。请稍等')" />
                &nbsp;&nbsp;
                <asp:Button ID="btnDel" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');"
                    CssClass="btn" />
             
              
                <h3><asp:Literal ID="lblCount" runat="server" /></h3>
                <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table class="table estList" cellspacing="0" border="1">
                            <thead>
                                <tr>
                                    <th>
                                       <input id="chkALL" type="checkbox" name="chkALL" onclick="javascript:FormSelectAll('<%=this.form1.ClientID %>    ','chkItem',this)">全选
                                    </th>
                                    <th>
                                        用户名
                                    </th>
                                    <th>
                                        真实姓名
                                    </th>
                                    <th>
                                        手机号码
                                    </th>
                                    <th>
                                        邮箱
                                    </th>
                                   
                                    <th>
                                        操作
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkItem" runat="server" BorderStyle="None" BorderWidth="0px" />
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Eval("UserID") %>' />
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
                                <%# Eval("EMail") %>
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
                <asp:Button ID="btnDel2" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" />
                <br />
                <div style="text-align: center">
                   <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="100">
                   </webdiyer:AspNetPager>
                </div>
            </div>
        </div>
    </div>
    </form>
    <br />
    <br />
</body>
</html>
