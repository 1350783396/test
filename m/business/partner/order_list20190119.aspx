﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_list.aspx.cs" Inherits="ETicket.Web.business.partner.order_list" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

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
	font-size: 12px;
}
.table td{
	text-align: center;
	border-top-width: 1px;
	border-right-width: 1px;
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
    <body class="um-vp bc-bg" ontouchstart>
	    <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="javascript :history.back(-1);"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">我的订单</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-refresh umw2 umh4"  onclick="window.open('/business/partner/order_list.aspx','_self')"></div>
                </div>
            </div>
        <div class="umar-a sc-bg" id="goodsList">

  <form id="form1" runat="server">

			<div style="display:none">
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
                    <HeaderTemplate>	</div>
                        <table class="table" cellspacing="0" border="0">
                            <thead>
                                <tr >
                                  <div style="display:none"> 
                                         <asp:CheckBox id="chkAll" Text="全选" runat="server" /></div>
                                
								
                                     <div style="display:none">   销售商</div>
                                   
                                 
                                       <div style="display:none">   订单号</div>
                               
                                     <th>
                                        产品名称
                                    </th>
                          
                                       <div style="display:none"> 客人姓名</div>
                           
                              
                                       <div style="display:none"> 客人手机</div>
                             
							
                                       <div style="display:none"> 购买时间</div>
                                
                                    <th>
                                        数量
                                    </th>
							       <th>
									  总价
				                   </th>
                                    <th>
                                        名字
                                    </th>
                                     <th>
                                        电话
                                    </th>
                                     <th>
                                        状态
                                   </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr onClick="window.open('/business/partner/order_detail.aspx?orderid=<%# Eval("OrderID") %>','_self')">
                          
                              <div style="display:none"> <asp:CheckBox ID="chkItem" runat="server" BorderStyle="None" BorderWidth="0px" />
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Eval("OrderID") %>' /></div>
                         
				         
                     
                                <div style="display:none"> <%# Eval("SheetID") %></div>
                   
                            <td>
                                <%# Eval("ProductName") %>
                            </td>
                         
                               <div style="display:none">  <%# Eval("RealName") %></div>
                         
                       
                               <div style="display:none"> <%# Eval("Phone") %></div>
                      
					
                           
                       
                            <td>
                                <%# Eval("NUM") %>
                            </td>
                            <td>
							  <%# Eval("TotalPrice") %>
                            </td>
                            <td>
                                 <%# Eval("RealName") %>
                            </td>
							<td>
						         <%# Eval("Phone") %>
							  </td>
							 <td>
                               <span style=" color:#FF0000">  <%# Eval("OrderStatus") %></span>
                            </td>
                              <div style="display:none">  
                                 <asp:HyperLink ID="hyDetail" runat="server" Visible="false" CssClass="linkAction" >详情</asp:HyperLink>
                                <asp:HyperLink ID="hyPay" runat="server" Visible="false" CssClass="linkAction">付款</asp:HyperLink>
                                <asp:HyperLink ID="hyCancel" runat="server" Visible="false" CssClass="linkAction">取消订单</asp:HyperLink>
                                <asp:HyperLink ID="hyBack" runat="server" Visible="false" CssClass="linkAction">申请退款</asp:HyperLink></div>
                  
                        </tr>
                     </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
              	<div style="display:none">  <asp:Button ID="btnDel2" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" /></div>
                &nbsp; &nbsp;
                
                <br />
               <div style="text-align: center">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="100">
                   </webdiyer:AspNetPager>
                </div>
  
    </form>
        </div>

    </body>
  
</html>


