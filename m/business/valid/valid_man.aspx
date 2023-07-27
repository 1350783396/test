<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="valid_man.aspx.cs" Inherits="ETicket.Web.business.valid.valid_man" %>


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
                <h4 style="color:red">注：在无法使用验票终端时，可登陆网站使用订单号验票。</h4>
                <br />
                <br />
                
                 订单号：<asp:TextBox ID="txtSheetID" runat="server" CssClass="txt"/>&nbsp;&nbsp;<asp:Button ID="btnQuery" Text="查询" CssClass="btn" Width="100" runat="server"  OnClientClick="sAlert('正在加载查询数据。。。。请稍等')"/>
                &nbsp;&nbsp;
                 <asp:Button ID="btnValid" Text="确认验票" Visible="false" CssClass="btn" Width="100" runat="server"  OnClientClick="return sys_confirm('确认完成验票？确认后无法撤消');" />
                <br />
                <asp:Panel ID="resultPanel" runat="server" Visible="false">
                
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
                           <span style="color:red;font-size:18px;"><asp:Literal ID="litOrderState" Text="待支付" runat="server"></asp:Literal></span>
                           
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
                            购买数量
                        </td>
                        <td align="left">
                            <span style="color:red;font-size:18px;"><asp:Literal ID="litBuyNum" runat="server"></asp:Literal></span>
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
                    
                </table>
                </asp:Panel>
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
