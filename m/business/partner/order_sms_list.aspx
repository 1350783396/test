<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_sms_list.aspx.cs" Inherits="ETicket.Web.business.partner.order_sms_list" %>


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
              
                <table class="formTable" >
                    <tr>
                        <td colspan="4">
                             <h4 style="color:red">注：购买专线(或门票)取票方式为二维码(或身份证)时，支付成功后生成短信。短信发送失败(或发送成功)，可点击操作重新发送</h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            短信发送状态
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSendStatus" runat="server" Width="200">

                            </asp:DropDownList>
                        </td>
                        <td class="label">
                            取票方式
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlValid" runat="server" Width="200">
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
                            短信发送时间
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
                </table>
                <br />
                <asp:Button ID="btnDel" Visible="false" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');"
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
                                    <th style="display:none">
                                        <asp:CheckBox id="chkAll" Text="全选" runat="server" />
                                    </th>
                                    <th style="width:80px;">
                                        订单号
                                    </th>
                                    <th style="width:80px;">
                                        验证方式
                                    </th>
                                    <th style="width:80px;">
                                        客人姓名
                                    </th>
                                    <th style="width:80px;">
                                        客人手机
                                    </th>
                                    <th style="width:80px;">
                                        短信发送状态
                                    </th>
                                    <th style="width:80px;">发送次数</th>
                                    <th>短信内容</th>
                                    <th style="width:80px;">
                                        操作
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td rowspan="2" style="display:none">
                                <asp:CheckBox ID="chkItem" runat="server" BorderStyle="None" BorderWidth="0px" />
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Eval("OrderID") %>' />
                            </td>
                            <td>
                                 <%# Eval("SheetID") %>
                            </td>
                            <td>
                                <%# Eval("SMSCategory") %>
                            </td>
                            <td>
                                <%# Eval("OrderRealName") %>
                            </td>
                            <td>
                               <%# Eval("Phone") %>
                            </td>
                            <td>
                                <%# Eval("SendStatus") %>
                            </td>
                            <td> <%# Eval("SendNum") %></td>
                            <td style="line-height:18px;"><%#Eval("SMSContent") %></td>
                            <td align="left">
                                <asp:Literal ID="litReset" runat="server"></asp:Literal>
                                <asp:LinkButton ID="lbtnReset" runat="server" OnClientClick="return sys_confirm('确认要重新发送该短信？');" >重新发送</asp:LinkButton>
                            </td>
                        </tr>
                        
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
                <asp:Button ID="btnDel2" Visible="false" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" />
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
