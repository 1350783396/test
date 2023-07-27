﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tongji_buy_detail.aspx.cs" Inherits="ETicket.Web.business.order.tongji_buy_detail" %>

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
    
    <script type="text/javascript" src="/plugin/jbox-v2.3/jBox/jquery.jBox-2.3.min.js"></script>
    <script type="text/javascript" src="/plugin/jbox-v2.3/jBox/i18n/jquery.jBox-zh-CN.js"></script>
    <link type="text/css" rel="stylesheet" href="/plugin/jbox-v2.3/jBox/Skins/Blue/jbox.css" />
    
    <script  type="text/javascript">
        var SelectReValue = "";
        var SelectReText = "";
        function SelectUser() {
            $.jBox("iframe:../select_user.aspx?list=" + $("#txtJoinmanValue").val(), {
                title: "选择分销商",
                width: 650,
                height: 430,
                buttons: { '确定': true, '取消': false },
                submit: function (v) {
                    if (v) {
                        $("#txtSelText").attr("value", SelectReText);
                        $("#txtSelValue").attr("value", SelectReValue);
                    }
                }
            });
        }
        function ClearSel() {
            $("#txtSelText").attr("value", "");
            $("#txtSelValue").attr("value", "");
        }
    </script>

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
             
                <table class="formTable">
                    <tr>
                        <td colspan="4">
                             <h4 style="color:red">
                                说明：详细列出分销商、普通会员已下单购买的订单。包括：已支付、已验票、已过期的订单<br />
                            </h4>
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
                            产品名称
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtProductName" runat="server" Width="200"/>
                        </td>

                    </tr>
                    <tr>
                        <td class="label">
                            下单时间
                        </td>
                        <td>
                             <asp:TextBox ID="txtOrderTime1" runat="server" onclick="WdatePicker()" Width="95"/>--<asp:TextBox ID="txtOrderTime2" runat="server" onclick="WdatePicker()" Width="95"/>
                        </td>
                        <td class="label">
                            验票时间
                        </td>
                        <td>
                             <asp:TextBox ID="txtValidTime1" runat="server" onclick="WdatePicker()" Width="95"/>--<asp:TextBox ID="txtValidTime2" runat="server" onclick="WdatePicker()" Width="95"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            下单人级别
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlUserLevel" runat="server" Width="200"/>
                        </td>
                         <td class="label">
                            下单人账号
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtUserName" runat="server" Width="200"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            分销商
                        </td>
                        <td align="left">
                           <input name='txtSelText' id="txtSelText" runat="server" style="width:200px;" readonly="readonly"/>
                            <a href="javascript:SelectUser()">选择分销商</a>
                            &nbsp;&nbsp;
                            <a href="javascript:ClearSel()">清空选择</a>
                            <input name='txtSelValue' id='txtSelValue' type='hidden' runat="server"/>
                        </td>
                        <td class="label">
                            支付方式
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlPayType" runat="server" Width="200">
                               <asp:ListItem>所有方式</asp:ListItem>
                               <asp:ListItem>在线支付</asp:ListItem>
                               <asp:ListItem>在线支付-支付宝</asp:ListItem>
                               <asp:ListItem>在线支付-银联</asp:ListItem>
                               <asp:ListItem>在线支付-快钱</asp:ListItem>
                               <asp:ListItem>扫码-微信</asp:ListItem>
                               <asp:ListItem>扫码-支付宝</asp:ListItem>
                               <asp:ListItem>现金支付</asp:ListItem>
                               <asp:ListItem>积分支付</asp:ListItem>
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
                            上车地点
                        </td>
                        <td align="left">
                             <asp:TextBox ID="txtStartAddress" runat="server" Width="200"/>
                        </td>
                        <td class="label">
                            发车时间
                        </td>
                        <td>
                            日期<asp:TextBox ID="txtStartTime1" runat="server" onclick="WdatePicker()" Width="90"/>--<asp:TextBox ID="txtStartTime2" runat="server" onclick="WdatePicker()" Width="90"/>时间<asp:TextBox ID="txtStartHM" runat="server" onclick="WdatePicker({dateFmt:' HH:mm'})" Width="50"/>
                        </td>
                    </tr>

                    <tr>
                         <td class="label">
                            订单号
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtSheetID" runat="server" Width="200"/>
                        </td>
                        <td class="label">
                            订单来源
                        </td>
                        <td align="left">
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
               
               
                <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('系统正在查询。。。。请稍等！')"/>
                &nbsp;&nbsp;
                <asp:Button ID="btnRefrech" Text="刷新" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('正在重新刷新数据。。。。请稍等！')"/>
                &nbsp;&nbsp;
                  <asp:Button ID="btnExcel" Text="本页导出Excel" Width="100" runat="server"  CssClass="btn"  />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcelALL" Text="所有导出Excel" Width="100" runat="server"  CssClass="btn"  />
               

                <br />
                <br />
                <h3><asp:Literal ID="lblCount" runat="server" /></h3>
                   
                        <table class="table" cellspacing="0" border="1">
                            <thead>
                                <tr>
                                    <th style="display:none">
                                        <asp:CheckBox id="chkAll" Text="全选" runat="server" />
                                    </th>
                                    <th class="iswarp">销售人</th>
                                    <th class="iswarp">销售电话</th>
                                    <th class="iswarp" style="display:none">产品类型</th>
                                    <th class="iswarp">产品名称</th>
                                    <th class="iswarp">产品属性</th>
                                    <th class="iswarp">购买数量</th>
                                    <th class="iswarp">单价</th>
                                    <th class="iswarp">总价</th>
                                    <th class="iswarp">返现积分</th>
                                    <th class="iswarp">扫码返现</th>
                                    <th class="iswarp">购买时间</th>
                                    <th class="iswarp">支付类型</th>
                                    <th class="iswarp">发车时间</th>
                                    <th class="iswarp">上车地点</th>
                                    <th class="iswarp">订单号</th>
                                    <th class="iswarp">验票时间</th>
                                    <th class="iswarp" style="display:none">备注</th>
                                    <th class="iswarp" style="display:none">
                                        操作
                                    </th>
                                </tr>
                            </thead>
                             <asp:Repeater ID="repList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="display:none">
                                            <asp:CheckBox ID="chkItem" runat="server" BorderStyle="None" BorderWidth="0px" />
                                            <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Eval("OrderID") %>' />
                                        </td>
                                        <td><%# Eval("CPName") %></td>
                                        <td><%# Eval("CPTel") %></td>
                                        <td style="display:none"><asp:Literal ID="litProductCategory" runat="server"/></td>
                                        <td><%# Eval("ProductName") %></td>
                                        <td><%# GetProperties(Eval("Properties").ToString()) %></td>
                                        <td><%# Eval("NUM") %></td>
                                        <td><%# Eval("UnitPrice") %></td>
                                        <td><%# Eval("TotalPrice") %></td>
                                        <td><%# Eval("RebateTotal") %></td>
                                        <td><%# Eval("FxReturnPrice") %></td>
                                        <td><%# Eval("OrderTime") %></td>
                                        <td><asp:Literal ID="litPayType" runat="server" /></td>
                                        <td><%# Eval("StartTime") %></td>
                                        <td><%# Eval("StartAddress") %></td>
                                        <td><%# Eval("SheetID") %></td>
                                        <td><%# Eval("ValidTime") %></td>
                                        <td style="display:none"><%# Eval("Memo") %></td>
                                        <td align="left" style="display:none">
                                            <asp:HyperLink ID="hyDetail" runat="server" Visible="false" CssClass="linkAction" >详情</asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                             </asp:Repeater>
                             <tr>
                                <td style="display:none">
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td><asp:Literal ID="litCount" runat="server" /></td>
                                <td></td>
                                <td><asp:Literal ID="litSum" runat="server" /></td>
                                <td><asp:Literal ID="litSumRebate" runat="server" /></td>
                                <td><asp:Literal ID="litSumFxReturn" runat="server" /></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="display:none"></td>
                                <td align="left" style="display:none">
                                </td>
                            </tr>
                            <tr>
                                <td style="display:none">
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td><asp:Literal ID="litCountTotal" runat="server" /></td>
                                <td></td>
                                <td><asp:Literal ID="litSumTotal" runat="server" /></td>
                                <td><asp:Literal ID="litSumRebateTotal" runat="server" /></td>
                                <td><asp:Literal ID="litSumFxReturnTotal" runat="server" /></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="display:none"></td>
                                <td align="left" style="display:none">
                                </td>
                            </tr>
                        </table>
                   
               
                <br />
                <asp:Button ID="btnDel2" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" Visible="false" />
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
