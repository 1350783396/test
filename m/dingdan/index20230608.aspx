
<title>智慧游</title>
<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title></title>
        <meta charset="utf-8">
        <meta name="viewport" content="target-densitydpi=device-dpi, width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
        <link rel="stylesheet" href="/css/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="/css/ui-box.css">
        <link rel="stylesheet" href="/css/ui-base.css">
        <link rel="stylesheet" href="/css/ui-color.css">
        <link rel="stylesheet" href="/css/appcan.icon.css">
        <link rel="stylesheet" href="/css/appcan.control.css">
        <style type="text/css">
<!--
.STYLE1 {font-size: 36px}
-->
        </style>
</head>
    <body class="um-vp " ontouchstart>
	<div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="window.open('/business/partner.aspx','_self')"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">我的订单</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-refresh umw2 umh4"  onclick="window.open('#','_self')"></div>
                </div>
            </div>


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="\business\partner\order_list.aspx.cs" Inherits="ETicket.Web.business.partner.order_list" %>

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
                <div align="center">
                  <table width="608" class="formTable">
                    <tr>
                      <td width="103" class="label"><div align="center">产品名称 </div></td>
                      <td width="168" align="left"><asp:TextBox ID="txtProductName" runat="server" Width="100"/>                  
                      </td>
            <td class="label">
                            演出场次
                        </td>
                        <td  align="left">
                            <asp:DropDownList ID="ddlProperties" runat="server" Width="100">
                            </asp:DropDownList>
                             <asp:TextBox ID="txtProperties" runat="server" Width="100"/>
                        </td>
                    <tr>
                      <td class="label"> 订单号 </td>
                      <td align="left"><asp:TextBox ID="txtSheetID" runat="server" Width="100"/>                  
                      </td>
                      <td class="label"> 日期 </td>
                      <td align="left"><asp:TextBox ID="txtPalyDate" runat="server" onclick="WdatePicker()" Width="90"/>                  
                      </td>
                    </tr>
                    <tr>
                      <td class="label"> 姓名 </td>
                      <td align="left"><asp:TextBox ID="txtRealname" runat="server" Width="100"/>                  
                      </td>
                      <td class="label"> 手机号 </td>
                      <td align="left"><asp:TextBox ID="txtPhone" runat="server" Width="90"/>                  
                      </td>
                    </tr>
                    <tr>
                      <td class="label"> 订单状态 </td>
                      <td align="left"><asp:DropDownList ID="ddlOrderStatus" runat="server" Width="80"> </asp:DropDownList>
                      </td>
                      <td class="label"> 订单来源 </td>
                      <td align="left" colspan="3"><asp:DropDownList ID="ddlClientType" runat="server"  Width="80">
                          <asp:ListItem Value="">所有来源</asp:ListItem>
                          <asp:ListItem Value="pc">电脑</asp:ListItem>
                          <asp:ListItem Value="app">手机APP</asp:ListItem>
                          <asp:ListItem Value="openapi">开放平台</asp:ListItem>
                          <asp:ListItem Value="alims">阿里码商</asp:ListItem>
                          <asp:ListItem Value="weixin">微信/扫码</asp:ListItem>
                        </asp:DropDownList>
                      </td>
                    </tr>
                    <tr>
                      <td class="label"> 下单时间 </td>
                      <td ><asp:TextBox ID="txtOrderTime1" runat="server" onclick="WdatePicker()" Width="45"/>                  
                        --
                          <asp:TextBox ID="txtOrderTime2" runat="server" onclick="WdatePicker()" Width="45"/>                    
                      </td>
                    </tr>
                  </table>
                  <br />
                  
                  
                  
                </div>
                <asp:Button ID="btnDel" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');"
                    CssClass="btn" />
                 &nbsp;&nbsp;
                <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('系统正在查询。。。。请稍等！')"/>
                             &nbsp;&nbsp;
                <asp:Button ID="btnRefrech" Text="刷新" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('正在重新刷新数据。。。。请稍等！')"/>                
                <div align="center"><br />
                    <br />
                </div>
                <h3><asp:Literal ID="lblCount" runat="server" /></h3>
                <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table class="table" cellspacing="0" border="1">
                            <thead>
                                <tr>
                                   <!-- <th>
                                        <asp:CheckBox id="chkAll" Text="全选" runat="server" />
                                    </th>-->

								    <th>
                                        游览日期
                                    </th>
                                     <th>
                                        产品名称
                                    </th>
                                     <th>
                                        场次
                                    </th>
                                    <th>
                                       姓名
                                    </th>
                                    <th>
                                        电话
                                    </th>
                                    <th>
                                        数量
                                    </th>
                                    <!--<th>
                                        总价
                                    </th>-->
                                     <th>
                                        备注
                                    </th>		
								   <th>
                                        详情
                                    </th>
						            <th>
                                        操作
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                          <!--  <td>
                          <asp:CheckBox ID="chkItem" runat="server" BorderStyle="None" BorderWidth="0px" /></td>-->

							<td>
                                 <%# Eval("palydate") %>
                            </td>
							<td>
                                <%# Eval("ProductName") %>
                            </td>
                            <td>
                                <%# GetProperties(Eval("Properties").ToString()) %>
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
                           <!--总价开始 <td>
                                 <%# Eval("TotalPrice") %>
                            </td> 总价结束-->
                            <td>
                                 <%# Eval("Memo") %>
                            </td>  
							  <td>
                                 <a href="/dingdan/order_detail.aspx?orderid=<%# Eval("orderid") %>">详情</a>
                            </td>                         

                            <td align="left">
                               
                              <!--  <asp:HyperLink ID="hyDetail" runat="server" Visible="false" CssClass="linkAction" >详情</asp:HyperLink>-->
                                <asp:HyperLink ID="hyPay" runat="server" Visible="false" CssClass="linkAction">付款</asp:HyperLink>
                              <!--  <asp:HyperLink ID="hyCancel" runat="server" Visible="false" CssClass="linkAction">取消</asp:HyperLink>-->
                                 <asp:HyperLink ID="hyBack" runat="server" Visible="false" CssClass="linkAction">退款</asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <!-- <br />
                <asp:Button ID="btnDel2" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" />
                &nbsp; &nbsp;
                
                <br />-->
		
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

            <!--content结束-->
</body>
</html>