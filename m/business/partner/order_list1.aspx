<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_list.aspx.cs" Inherits="ETicket.Web.business.partner.order_list" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/common/main-min.js"></script>
    <script  type="text/javascript" src="/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script  type="text/javascript" src="/js/chkSelectAll.js"></script>
    <script  type="text/javascript" src="/plugin/My97DatePicker/WdatePicker.js"></script>

    <style type="text/css">
        .linkAction
        {
            padding:0px 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <div id="content" class="clearfix">
            <div id="main">
                <table class="formTable">
                    <tr>
                        <td class="label">
                            产品名称
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtProductName" runat="server" Width="200"/>
                        </td>
                        <td class="label">
                            订单状态
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlOrderStatus" runat="server" Width="200">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            客人姓名
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtRealname" runat="server" Width="200"/>
                        </td>
                         <td class="label">
                            客人手机
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtPhone" runat="server" Width="200"/>
                        </td>
                   
                    </tr>
                    <tr>
                        <td class="label">
                            下单时间
                        </td>
                        <td >
                            <asp:TextBox ID="txtOrderTime1" runat="server" onclick="WdatePicker()" Width="95"/>--<asp:TextBox ID="txtOrderTime2" runat="server" onclick="WdatePicker()" Width="95"/>
                        </td>
                         <td class="label">
                            订单号
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtSheetID" runat="server" Width="200"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            订单来源
                        </td>
                        <td align="left" colspan="3">
                            <asp:DropDownList ID="ddlClientType" runat="server"  Width="200">
                                <asp:ListItem Value="">所有来源</asp:ListItem>
                                <asp:ListItem Value="pc">电脑</asp:ListItem>
                                <asp:ListItem Value="app">手机APP</asp:ListItem>
                                <asp:ListItem Value="openapi">开放平台</asp:ListItem>
                                <asp:ListItem Value="alims">阿里码商</asp:ListItem>
                                <asp:ListItem Value="weixin">微信/扫码</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
               
               
               
                <asp:Button ID="btnDel" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');"
                    CssClass="btn" />
                 &nbsp;&nbsp;
                <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('系统正在查询。。。。请稍等！')"/>
                             &nbsp;&nbsp;
                <asp:Button ID="btnRefrech" Text="刷新" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('正在重新刷新数据。。。。请稍等！')"/>
                <br />
                <br />
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
                                        订单号
                                    </th>
                                     <th>
                                        产品名称
                                    </th>
                                    <th>
                                        客人姓名
                                    </th>
                                    <th>
                                        客人手机
                                    </th>
                                    <th>
                                        购买数量
                                    </th>
                                    <th>
                                        总价
                                    </th>
                                     <th>
                                        状态
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
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Eval("OrderID") %>' />
                            </td>
                            <td>
                                 <%# Eval("SheetID") %>
                            </td>
                            <td>
                                <%# Eval("ProductName") %>
                            </td>
                            <td>
                                <%# Eval("RealName") %>
                            </td>
                            <td>
                               <%# Eval("Phone") %>
                            </td>
                            <td>
                                <%# Eval("NUM") %>
                            </td>
                            <td>
                                 <%# Eval("TotalPrice") %>
                            </td>
                            <td>
                                 <%# Eval("OrderStatus") %>
                            </td>
                            <td align="left">
                               
                                <asp:HyperLink ID="hyDetail" runat="server" Visible="false" CssClass="linkAction" >详情</asp:HyperLink>
                                <asp:HyperLink ID="hyPay" runat="server" Visible="false" CssClass="linkAction">付款</asp:HyperLink>
                                <asp:HyperLink ID="hyCancel" runat="server" Visible="false" CssClass="linkAction">取消订单</asp:HyperLink>
                                <asp:HyperLink ID="hyBack" runat="server" Visible="false" CssClass="linkAction">申请退款</asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
                <asp:Button ID="btnDel2" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" />
                &nbsp; &nbsp;
                
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
