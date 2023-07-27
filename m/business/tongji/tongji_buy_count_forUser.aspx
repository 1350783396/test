<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tongji_buy_count_forUser.aspx.cs" Inherits="ETicket.Web.business.tongji.tongji_buy_count_forUser" %>

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
    <script type="text/javascript" src="/plugin/My97DatePicker/WdatePicker.js"></script>


    <style type="text/css">
        .linkAction
        {
            padding:0px 5px;
        }
        .iswarp{
            white-space:nowrap;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <div id="content" class="clearfix">
            <div id="main">
                <table class="formTable" style="width:900px;">
                    <tr>
                        <td class="label">
                            销售时间
                        </td>
                        <td>
                             <asp:TextBox ID="txtOrderTime1" runat="server" onclick="WdatePicker()" Width="95"/>--<asp:TextBox ID="txtOrderTime2" runat="server" onclick="WdatePicker()" Width="95"/>
                        </td>
                    </tr>
                    <tr>
                         <td class="label">
                            产品类型
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlProductCategory" runat="server" Width="200">
                            </asp:DropDownList>
                        </td>
                    </tr>
                   
                    <tr>
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
                            支付方式
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlPayType" runat="server" Width="200">
                               <asp:ListItem>所有方式</asp:ListItem>
                               <asp:ListItem>在线支付</asp:ListItem>
                               <asp:ListItem>在线支付-支付宝</asp:ListItem>
                               <asp:ListItem>在线支付-银联</asp:ListItem>
                               <asp:ListItem>现金支付</asp:ListItem>
                               <asp:ListItem>积分支付</asp:ListItem>
                           </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnQuery" Text="统计" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('系统正在执行统计。。。。请稍等！')"/>
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" Text="导出Excel" Width="100" runat="server" 
                    CssClass="btn" />
                <br />
                <br />
                <h3><asp:Literal ID="lblCount" runat="server" /></h3>
                <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table class="table estList" cellspacing="0" border="1" style="width:900px;">
                            <thead>
                                <tr>
                                    <th class="iswarp">用户名</th>
                                    <th class="iswarp">产品分类</th>
                                    <th class="iswarp">产品名称</th>
                                    <th class="iswarp">单价</th>
                                    <th class="iswarp">销售数量</th>
                                    <th class="iswarp">销售金额</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("UserName") %></td>
                            <td><%#ETicket.Utility.CovertHelper.ConvertProductCategory(Eval("CategoryID")) %></td>
                            <td><%# Eval("ProductName") %></td>
                            <td><%# Eval("UnitPrice") %></td>
                            <td><%# Eval("BuySum") %></td>
                            <td><%# Eval("PriceSum") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
               
                
                <br />
                <div style="text-align: center">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="10000">
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

