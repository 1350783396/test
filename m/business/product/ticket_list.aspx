<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ticket_list.aspx.cs" Inherits="ETicket.Web.business.product.ticket_list" %>

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
                            名称
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtProductName" runat="server" Width="200"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            地区
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlRegion" runat="server" Width="200">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnDel" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');"
                    CssClass="btn" />
                &nbsp; &nbsp;
                <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CausesValidation="false" CssClass="btn" OnClientClick="sAlert('系统正在查询。。。。请稍等！')" />
                <h3><asp:Literal ID="lblCount" runat="server" /></h3>
                <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table class="table" cellspacing="0" border="1">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:CheckBox id="chkAll" Text="全选" runat="server" />
                                    </th>
                                     <th>
                                        景区名称
                                    </th>
                                    <th>
                                        地区
                                    </th>
                                    <th>
                                        供应商
                                    </th>
                                    <th>
                                        原价
                                    </th>
                                    <th>
                                        状态
                                    </th>
                                    <th>
                                        管理属性
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
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Eval("ProductID") %>' />
                            </td>
                            <td>
                                <%# Eval("ProductName") %>
                            </td>
                            <td>
                                <asp:Literal ID="lblSupplyName" runat="server"/>
                            </td>
                            <td>
                                <%# Eval("SupplyName") %>
                            </td>
                            <td>
                                <%# Eval("PrimeCost") %>
                            </td>
                            <td>
                                 <asp:Literal ID="litSaleFlag" runat="server"/>
                            </td>
                            <td>  <a href="Properties2_list.aspx?id=<%# Eval("ProductID") %>">管理属性</a></td>
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
