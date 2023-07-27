<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_detail_chk.aspx.cs" Inherits="ETicket.Web.business.order.order_detail_chk" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>

    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>

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
                <br />
                <br />
                 <table class="formTable" style="width:800px;">
                     <tr>
                         <td class="label" style="height:30px;vertical-align:middle;">申请退款日期</td>
                         <td><asp:Literal ID="litRTime" runat="server"/></td>
                     </tr>
                     <tr>
                         <td class="label" style="height:30px;vertical-align:middle;">申请退款说明</td>
                         <td><asp:Literal ID="litRMark" runat="server"/></td>
                     </tr>
                     <tr>
                         <td class="label" style="height:30px;vertical-align:middle;">审核结果</td>
                         <td>
                             <asp:DropDownList ID="ddlResult" runat="server">
                                 <asp:ListItem Value="请选择">请选择</asp:ListItem>
                                 <asp:ListItem Value="审核通过">审核通过</asp:ListItem>
                                 <asp:ListItem Value="审核不通过">审核不通过</asp:ListItem>
                             </asp:DropDownList>
                         </td>
                     </tr>
                      <tr>
                         <td class="label" style="height:30px;vertical-align:middle;">审核说明</td>
                         <td>
                             <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Height="50" Width="500"></asp:TextBox>
                         </td>
                     </tr>
                 </table>
                 <div style="text-align:center;width:800px;margin-top:5px;">
                    <asp:Button ID="btnRefresh" Text="刷新" CssClass="btn" Width="100" runat="server" OnClientClick="return sAlert('正在刷新数据。。。请稍等！');"/>
                    <asp:Button ID="btnChk" Text="提交审核" CssClass="btn" Width="100" runat="server"  OnClientClick="return sys_confirm('确认提交审核结果？提交后无法撤消');" />
                 </div>
                <br />
                <br />
                <table class="formTable" style="width:800px;">
                    <tr >
                        <td colspan="4" align="center">
                            <h4><asp:Literal ID="litProductName" runat="server"></asp:Literal></h4>
                        </td>
                    </tr>
                    <tr >
                        <td class="label" style="height:30px;vertical-align:middle;">
                            订单状态
                        </td>
                        <td align="left" >
                           <asp:Literal ID="litOrderState" Text="待支付" runat="server"></asp:Literal>
                           
                        </td>
                         <td class="label" style="height:30px;vertical-align:middle;">
                            短信发送状态
                        </td>
                        <td align="left" >
                           <asp:Literal ID="litSMSStatus" Text="待支付" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            单价
                        </td>
                        <td align="left">
                           ￥<asp:Literal ID="litUnitPrice" runat="server"></asp:Literal>
                        </td>
                         <td class="label" style="height:30px;vertical-align:middle;">
                            购买数量
                        </td>
                        <td align="left">
                            <asp:Literal ID="litBuyNum" runat="server"></asp:Literal>
                        </td>
                    </tr>
                   <tr>
                       
                        <td class="label" style="height:30px;vertical-align:middle;">
                            订单总额
                        </td>
                        <td align="left">
                            ￥<asp:Literal ID="litTotalPrice" runat="server"></asp:Literal>
                        </td>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            订单号
                        </td>
                        <td align="left">
                            <asp:Literal ID="litSheetID" runat="server"></asp:Literal>
                        </td>
                   </tr>
                    <tr>
                       <td class="label" style="height:30px;vertical-align:middle;">
                            姓名
                        </td>
                        <td align="left">
                            <asp:Literal ID="litName" runat="server"></asp:Literal>
                        </td>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            下单时间
                        </td>
                        <td align="left">
                            <asp:Literal ID="litOrderTime" runat="server"></asp:Literal>
                        </td>
                    </tr>
                     <tr>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            身份证
                        </td>
                        <td align="left">
                           <asp:Literal ID="litIDCard" runat="server"></asp:Literal>
                        </td>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            手机
                        </td>
                        <td align="left">
                           <asp:Literal ID="litPhone" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    
                   
                     <tr>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            支付方式
                        </td>
                        <td align="left" >
                           <asp:Literal ID="litPayType" runat="server"></asp:Literal>
                        </td>
                         <td class="label" style="height:30px;vertical-align:middle;">
                            取票方式
                        </td>
                        <td align="left" >
                           <asp:Literal ID="litValidType" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="height:30px;vertical-align:middle;">
                            下单人级别
                        </td>
                        <td align="left" >
                           <asp:Literal ID="litUserLevel" runat="server"></asp:Literal>
                        </td>
                         <td class="label" style="height:30px;vertical-align:middle;">
                            下单人账号
                        </td>
                        <td align="left" >
                           <asp:Literal ID="litUserName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr id="trLine" runat="server">
                        <td class="label" style="height:30px;vertical-align:middle;">
                            发车时间
                        </td>
                        <td align="left" >
                           <asp:Literal ID="litStartTime" runat="server"></asp:Literal>
                        </td>
                         <td class="label" style="height:30px;vertical-align:middle;">
                            上车地点
                        </td>
                        <td align="left" >
                           <asp:Literal ID="litStartAddress" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr id="trTicket" runat="server">
                         <td class="label" style="height:30px;vertical-align:middle;">
                            取票时间规定
                        </td>
                        <td >
                            <asp:Literal ID="litEnableValidTime" runat="server"></asp:Literal>
                        </td>
                        <td class="label">
                            游玩日期
                        </td>
                        <td>
                            <asp:Literal ID="litPlayDate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
              
            </div>
        </div>
    </div>
    </form>
   <br />
    <br />
</body>
</html>
