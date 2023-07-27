<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_print_list.aspx.cs" Inherits="ETicket.Web.business.partner.order_print_list" %>

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
    <script  type="text/javascript" src="/js/LodopFuncs.js"></script>
    
    <script type="text/javascript" src="/plugin/jbox-v2.3/jBox/jquery.jBox-2.3.min.js"></script>
    <script type="text/javascript" src="/plugin/jbox-v2.3/jBox/i18n/jquery.jBox-zh-CN.js"></script>
    <link type="text/css" rel="stylesheet" href="/plugin/jbox-v2.3/jBox/Skins/Blue/jbox.css" />

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
                        <td colspan="4">
                            <span style="color:red">
                                说明：<br /><br />
                                1、购买专线(或门票)取票方式为纸质时，支付成功后方可打印小票。(纸质票订单支付成功后即为已验票状态)<br /><br />
                                2、在线打印需要安装插件，如未安装点击这里<a href='/install_lodop32.exe' target='_self'>下载安装</a>,安装后请重启浏览器<br /><br />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            取票方式
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlValid" runat="server" Width="200">
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
                        <td align="left">
                            <asp:TextBox ID="txtProductName" runat="server" Width="200"/>
                        </td>
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
                            订单号
                        </td>
                        <td align="left" colspan="4">
                           <asp:TextBox ID="txtSheetID" runat="server" Width="200"/>
                        </td>
                       
                    </tr>
                </table>
                <br />
               
               
               
                <asp:Button ID="btnDel" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');"
                    CssClass="btn" Visible="false" />
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
                        <table class="table estList" cellspacing="0" border="1">
                            <thead>
                                <tr>
                                    <th>
                                        打印
                                    </th>
                                    <th style="display:none">
                                        <asp:CheckBox id="chkAll" Text="全选" runat="server" />
                                    </th>
                                    <th>
                                        订单号
                                    </th>
                                     <th>
                                        取票方式
                                    </th>
                                    <th>
                                        产品类型
                                    </th>
                                     <th>
                                        产品名称
                                    </th>
                                     <th>
                                        产品属性
                                    </th>
                                    <th>
                                        客人姓名
                                    </th>
                                    <th>
                                        客人手机
                                    </th>
                                    <th>
                                        购买数量
                                    </th>
                                    <th>
                                        总价
                                    </th>
                                    <th>
                                        订单状态
                                    </th>
                                    
                                    <th>
                                        操作
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td id="tr_<%#Eval("OrderID")%>">
                                 <asp:Literal ID="litIsPrint" runat="server" />
                            </td>
                            <td style="display:none">
                                <asp:CheckBox ID="chkItem" runat="server" BorderStyle="None" BorderWidth="0px" />
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Eval("OrderID") %>' />
                               
                            </td>
                            <td>
                                 <%# Eval("SheetID") %>
                            </td>
                             <td>
                                 <%# Eval("ValidType") %>
                            </td>
                            <td>
                                <asp:Literal ID="litProductCategory" runat="server"/>
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
                            
                            <td align="left">
                               
                                <asp:HyperLink ID="hyDetail" runat="server" Visible="false" CssClass="linkAction" >详情</asp:HyperLink>
                             
                               
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
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
   <script type="text/javascript"> 
        var LODOP; //声明为全局变量      
        function PrintTicket(strID) {
            $.jBox.warning("小票只能打印一次，请检查打印机是否正常，纸张是否充足。如不确定请先点击【打印测试】按钮进行打印测试。<input type=\"hidden\" id=\"printID\" name=\"printID\" value=\"" + strID + "\" />",
                "打印提示",
                submit,
                { buttons: { '确定打印': 'ok', '打印测试': 'test', '关闭': 'cancel' } }
               );
        };
        
        var submit = function (v, h, f) {
            if (v == 'ok')
            {
                var id = f.printID;
                PrintOrderItem(id);
                $("#tr_" + id).html("已打印");
                return true;
            }
            else if (v == 'test')
            {
                PrintTest();
                return false;
            }
            else if (v == 'cancel') {
                return true;
            }
            return false;
        };
        
        function PrintTest()
        {
            LODOP = getLodop();
            LODOP.PRINT_INIT("");
            LODOP.ADD_PRINT_URL(30, 20, 270, "95%", "order_print_item_test.html");
            LODOP.SET_PRINT_STYLEA(0, "HOrient", 3);
            LODOP.SET_PRINT_STYLEA(0, "VOrient", 3);
            LODOP.PREVIEW();
        }
        function PrintOrderItem(id) {
            LODOP = getLodop();
            LODOP.PRINT_INIT("");
            LODOP.ADD_PRINT_URL(30, 20, 270, "95%", "order_print_item.aspx?id="+id);
            LODOP.SET_PRINT_STYLEA(0, "HOrient", 3);
            LODOP.SET_PRINT_STYLEA(0, "VOrient", 3);
            LODOP.PRINT();
        }
   </script>
    <br />
    <br />
</body>
</html>
