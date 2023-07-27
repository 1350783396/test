<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account_query_list.aspx.cs" Inherits="ETicket.Web.business.partner.account_query_list" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

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
                    <div class="fa fa-angle-left fa-2x" onClick="window.open('/business/partner.aspx','_self')"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">我的订单</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-refresh umw2 umh4"  onclick="window.open('/business/partner/order_list.aspx','_self')"></div>
                </div>
            </div>
        <div class="umar-a sc-bg" id="goodsList">
 <form id="form1" runat="server">

                <table class="table">
                    <tr>
                        <td colspan="2">
                 
                          
                              <img src="/images/mn.gif" width="30" height="30" />
                              <asp:Literal ID="litUserAccount" runat="server" ></asp:Literal>
                             &nbsp;&nbsp;
                      <div style="display:none"><asp:Button ID="btnLoadAccount" Text="刷新" runat="server" CssClass="button" OnClientClick="sAlert('正在刷新积分余额。。。。请稍等')" Width="50" /></div></td>
                    </tr>
            
                    
                          <div style="display:none">类型             
                       
                          <asp:DropDownList ID="ddlACT" runat="server" Width="200">
                                <asp:ListItem Value="">所有</asp:ListItem>
                                <asp:ListItem Value="in">收入</asp:ListItem>
                                <asp:ListItem Value="out">支出</asp:ListItem>
                            </asp:DropDownList> 
             
               
                            发生日期范围                 
                     
                            <asp:TextBox ID="txtStartTime" runat="server" onclick="WdatePicker()" Width="95" />-- <asp:TextBox ID="txtEndTime" runat="server" onclick="WdatePicker()" Width="95"/>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                              <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CssClass="button" OnClientClick="sAlert('正在查询数据。。。。请稍等')" />                    
               </div> 
                </table>
                <br />
             
               <div style="display:none">  <h3><asp:Literal ID="lblCount" runat="server" /></h3>   </div> 
                <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table class="table" cellspacing="0" border="0">
                            <thead>
                                <tr>
                                    <div style="display:none"> 
                                        分销账号 </div> 
                                  
                                    <th width="15%">
                                        操作
                                    </th>
                                    <th width="25%">
                                        金额
                                    </th>
                                     <div style="display:none"> 
                                        发生日期 </div> 
                                    <th width="60%">
                                        说明
                                    </th>
                                   
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="height:30px;  ">
                        
                                    <div style="display:none">  <asp:Literal ID="litUserName" runat="server"></asp:Literal> </div> 
                          
                            <td>
                                <asp:Literal ID="litACT" runat="server"></asp:Literal>
                            </td>
                            <td>
                                 <asp:Literal ID="litACTRMB" runat="server"></asp:Literal>
                            </td>
                         
                                <div style="display:none"> <asp:Literal ID="litTime" runat="server"></asp:Literal> </div> 
                       
                             <td>
                                <asp:Literal ID="litMemo" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
               
                <div style="text-align: center">
                   <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="100">
                   </webdiyer:AspNetPager>
                </div>

    </form>
        </div>

    </body>
  
</html>


