<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_list_refund_chkno.aspx.cs" Inherits="ETicket.Web.business.order.order_list_refund_chkno" %>

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
                            产品名称
                        </td>
                        <td align="left">
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
                            订单号
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtSheetID" runat="server" Width="200"/>
                        </td>
                    </tr>
                </table>
                <br />
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
                                    <th>
                                        订单号
                                    </th>
                                     <th>
                                        产品名称
                                    </th>
                                     <th>
                                        产品属性
                                    </th>
                                    <th>
                                        姓名
                                    </th>
                                    <th>
                                        手机
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
                                    <th>支付类型</th>
                                    <th>级别</th>
                                    <th>下单人</th>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="display:none">
                                <asp:CheckBox ID="chkItem" runat="server" BorderStyle="None" BorderWidth="0px" />
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Eval("ProductID") %>' />
                            </td>
                            <td>
                                 <%# Eval("SheetID") %>
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
                            <td>
                                 <%# Eval("TotalPrice") %>
                            </td>
                            <td>
                                 <%# Eval("OrderStatus") %>
                            </td>
                            <td><%#Eval("PayType") %></td>
                            <td><%#Eval("UserLevelName") %></td>
                            <td><%# Eval("UserName") %></td>
                            <td align="left">
                                <asp:HyperLink ID="hyDetail" runat="server"  CssClass="linkAction" >详情</asp:HyperLink>
                                <asp:HyperLink ID="hyExe" runat="server" CssClass="linkAction">处理</asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
                <asp:Button ID="Button1" Text="删除所选" Width="100" runat="server" Visible="false" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" />
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
