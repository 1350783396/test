<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_detail.aspx.cs" Inherits="ETicket.Web.business.partner.order_detail" %>
<title>智慧游</title>
<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title></title>
		<style type="text/css">
<!--
.table {
	background-color: #FFFFFF;
	width: 100%;
	border: 1px solid #CCCCCC;
}
.table td{
	text-align: left;
	border-top-width: 1px;
	border-bottom-width: 1px;
	border-left-width: 1px;
	border-bottom-style: solid;
	border-top-color: #CCCCCC;
	border-right-color: #CCCCCC;
	border-bottom-color: #CCCCCC;
	border-left-color: #CCCCCC;
	padding-top: 10px;
	padding-right: 3px;
	padding-bottom: 10px;
	padding-left: 3px;
	border-right-style: none;
}
.table th{
	padding: 10px;
	border-top-width: 1px;
	border-right-width: 1px;
	border-bottom-width: 1px;
	border-left-width: 1px;
	border-bottom-style: solid;
	border-top-color: #CCCCCC;
	border-right-color: #CCCCCC;
	border-bottom-color: #CCCCCC;
	border-left-color: #CCCCCC;
}
.STYLE2 {color: #FF0000}
-->
        </style>
<head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta name="viewport" content="target-densitydpi=device-dpi, width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
        <link rel="stylesheet" href="/css/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="/css/ui-box.css">
        <link rel="stylesheet" href="/css/ui-base.css">
        <link rel="stylesheet" href="/css/ui-color.css">
        <link rel="stylesheet" href="/css/appcan.icon.css">
        <link rel="stylesheet" href="/css/appcan.control.css">
        <link rel="stylesheet" href="/ticket_content/css/main.css">
    </head>
<script type="text/javascript">setTimeout(function(){ window.location.reload();},2000);</script>

    <body class="um-vp bc-bg" ontouchstart>
	    <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="javascript :history.back(-1);"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">订单详情</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-refresh umw2 umh4"  onclick="window.open('#','_self')"></div>
                </div>
            </div>
        <div class="umar-a sc-bg" id="goodsList">
    <form id="form1" runat="server">
 
            <div style="display:none;"    <br />
                <br />
                 <asp:Button ID="btnRefresh" Text="刷新" CssClass="btn" Width="100" runat="server" OnClientClick="sAlert('正在重新加载数据。。。。请稍等')"/>
                 <asp:Button ID="btnCalcel" Text="确认取消订单" CssClass="btn" Width="100" runat="server"  OnClientClick="return sys_confirm('确认要取消订单吗？');" />
                <br />
                <br /></div>
                <table border="0" class="table" >
                    <tr >
                        <td colspan="4" align="center">
                            <h4 align="center" class="STYLE2"><asp:Literal ID="litProductName" runat="server"></asp:Literal></h4>                        </td>
                    </tr>
                    <tr >
                        <td width="31%" class="label" style="height:20px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            订单状态                        </td>
                        <td width="69%" align="left" >
                           <span class="STYLE2">
                           <asp:Literal ID="litOrderState" Text="待支付" runat="server"></asp:Literal>
                      </span></td>
                    
                    </tr>
					  <tr>
					     <td class="label" style="height:20px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            短信发送状态
                        </td>
                        <td align="left" >
                           <span class="STYLE2">
                           <asp:Literal ID="litSMSStatus" Text="待支付" runat="server"></asp:Literal>
                           </span></td>
			      </tr>
					
                    <tr>
                        <td class="label" style="height:20px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            单价
                        </td>
                        <td align="left">
                        <span class="STYLE2">￥                           
                        <asp:Literal ID="litUnitPrice" runat="server"></asp:Literal>
                        </span></td>
                    
                    </tr>
					  <tr>
					     <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            购买数量
                        </td>
                        <td align="left">
                            <span class="STYLE2">
                            <asp:Literal ID="litBuyNum" runat="server"></asp:Literal>
                            </span></td>
					 </tr>
                   <tr>
                       
                        <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            订单总额
                        </td>
                        <td align="left">
                            <span class="STYLE2">￥
                            <asp:Literal ID="litTotalPrice" runat="server"></asp:Literal>
                            </span></td>
                      
                   </tr>
				     <tr>
				     <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            订单号
                       </td>
                        <td align="left">
                            <span class="STYLE2">
                            <asp:Literal ID="litSheetID" runat="server"></asp:Literal>
                            </span></td>
				     </tr>
                    <tr>
                       <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
					   下单时间</td>
                        <td align="left"><span class="STYLE2">
                          <asp:Literal ID="litOrderTime" runat="server"></asp:Literal>
                        </span></td>
                     
                    </tr>
					  <tr>
					   <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
					   游客姓名 </td>
                        <td align="left"><span class="STYLE2">
                          <asp:Literal ID="litName" runat="server"></asp:Literal>
                        </span></td>
					 </tr>
           
                <div style="display:none;">
                           <asp:Literal ID="litIDCard" runat="server"></asp:Literal>
                
                  </div>
            
					
					  <tr>
					 <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            手机号码</td>
                        <td align="left">
                           <span class="STYLE2">
                           <asp:Literal ID="litPhone" runat="server"></asp:Literal>
                           </span></td>
				  </tr>
                    <tr>
                        <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            支付方式
                        </td>
                        <td align="left" >
                           <span class="STYLE2">
                           <asp:Literal ID="litPayType" runat="server"></asp:Literal>
                           </span></td>
                        
                    </tr>
					   <tr>
					 <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            取票方式
                        </td>
                        <td align="left" >
                           <span class="STYLE2">
                           <asp:Literal ID="litValidType" runat="server"></asp:Literal>
                           </span></td>
					  </tr>
					
                    <tr id="trLine" runat="server">
                        <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            发车时间
                        </td>
                        <td align="left" >
                           <span class="STYLE2">
                           <asp:Literal ID="litStartTime" runat="server"></asp:Literal>
                           </span></td>
                       
                   </tr>
				   
				     <tr>
				     <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            上车地点
                       </td>
                        <td align="left" >
                           <span class="STYLE2">
                           <asp:Literal ID="litStartAddress" runat="server"></asp:Literal>
                           </span></td>
			      </tr>
                   <tr id="trTicket" runat="server">
                         <td class="label" style="height:30px;border-right-width: 1px;border-right-style: solid;border-right-color: #CCCCCC;">
                            取票时间
                        </td>
                        <td colspan="3">
                            <span class="STYLE2">
                            <asp:Literal ID="litEnableValidTime" runat="server"></asp:Literal>
                            </span></td>
                   </tr>
                 
      </table>
                <div align="center">智慧游 www.zhihuiyou.cn <br />
                  
                  
                            </div>
    </form>
        </div>

    </body>
  
</html>