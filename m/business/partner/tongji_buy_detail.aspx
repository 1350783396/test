<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tongji_buy_detail.aspx.cs" Inherits="ETicket.Web.business.partner.tongji_buy_detail" %>

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
    <script  type="text/javascript" src="/js/LodopFuncs.js"></script>

   

    <script type="text/javascript">
        function PrintPage() {
            LODOP = getLodop();
            LODOP.PRINT_INIT("SalesReport");
            LODOP.ADD_PRINT_HTM(0, 0,2100, 2970, document.getElementById("printCosntent").innerHTML);
            LODOP.PREVIEW();
        };
        
    </script>
    <script type="text/javascript">
        ///***********************
        ///打印指定区域页面
        ///说明：obj–通过getElementById或其它方式获取标签标识，打印此obj内的文字
        function startPrint(obj) {
            var oWin = window.open("", "_blank");
            //var strPrint = "<h4 style=’font-size:18px; text-align:center;’>打印预览区</h4>\n";
            var strPrint = "";
            strPrint = strPrint + "<div style='text-align:center'><button onclick='printWin()' style='padding-left:4px;padding-right:4px;width:100px;'>打  印</button><button onclick='window.opener=null;window.close();'  style='padding-left:4px;padding-right:4px;width:100px;'>关  闭</button></div>\n";

            strPrint = strPrint + "<script type=\"text/javascript\">\n";
            strPrint = strPrint + "function printWin()\n";
            strPrint = strPrint + "{";
            strPrint = strPrint + "var oWin=window.open(\"\",\"_blank\");\n";
            strPrint = strPrint + "oWin.document.write(document.getElementById(\"content\").innerHTML);\n";
            strPrint = strPrint + "oWin.focus();\n";
            strPrint = strPrint + "oWin.document.close();\n";
            strPrint = strPrint + "oWin.print()\n";
            strPrint = strPrint + "oWin.close()\n";
            strPrint = strPrint + "}\n";
            strPrint = strPrint + "<\/script>\n";

            strPrint = strPrint + "<hr size='1' />\n";
            strPrint = strPrint + "<div id=\"content\">\n";
            strPrint = strPrint + obj.innerHTML + "\n";
            strPrint = strPrint + "</div>\n";
            strPrint = strPrint + "<hr size='1' />\n";
            strPrint = strPrint + "<h4 style=’font-size:18px; text-align:center;’>打印预览内容结束</h4>\n";
            oWin.document.write(strPrint);
            oWin.focus();
            oWin.document.close();
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
               
                <h4 style="color:red">注：只针对已支付、已验票、已过期的订单</h4>
                 <br />
                <br />
                <table class="formTable" style="width:900px;">
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
                        <td align="left">
                            <asp:TextBox ID="txtProductName" runat="server" Width="200"/>
                        </td>
                         <td class="label">
                            上车地点
                        </td>
                        <td align="left">
                             <asp:TextBox ID="txtStartAddress" runat="server" Width="200"/>
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
                            购买时间
                        </td>
                        <td>
                             <asp:TextBox ID="txtOrderTime1" runat="server" onclick="WdatePicker()" Width="90"/>--<asp:TextBox ID="txtOrderTime2" runat="server" onclick="WdatePicker()" Width="90"/>
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
               
                 <h3><asp:Literal ID="lblCount" runat="server" /></h3>

                 <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('系统正在查询。。。。请稍等！')"/>
                  &nbsp;&nbsp;
                 <asp:Button ID="btnRefrech" Text="刷新" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('正在重新刷新数据。。。。请稍等！')"/>
                  &nbsp;&nbsp;   
                 <asp:Button ID="btnExcel" Text="本页导出Excel" Width="100" runat="server"  CssClass="btn"  />
                 &nbsp;&nbsp;
                <asp:Button ID="btnExcelALL" Text="所有导出Excel" Width="100" runat="server"  CssClass="btn"  />
                <!--
                <input  type="button" id="btnPrint" value="打印报表(控件方式)" style="width:150px" class="btn" onclick="PrintPage()" />
                 &nbsp;&nbsp;
                <input  type="button" id="btnPrint2" value="打印报表(网页方式)" style="width:150px" class="btn" onclick="startPrint(document.getElementById('printCosntent'))" />
                -->
                <br />
                <br />
                <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table class="table estList" cellspacing="0" border="1">
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
                                    <th class="iswarp">购买时间</th>
                                    <th class="iswarp">发车时间</th>
                                    <th class="iswarp">上车地点</th>
                                    <th class="iswarp">订单号</th>
                                    <th class="iswarp">验票时间</th>
                                    <th class="iswarp">备注</th>
                                    <th class="iswarp" style="display:none">
                                        操作
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr >
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
                            <td><%# Eval("OrderTime") %></td>
                            
                            <td><%# Eval("StartTime") %></td>
                            <td><%# Eval("StartAddress") %></td>
                            <td><%# Eval("SheetID") %></td>
                            <td><%# Eval("ValidTime") %></td>
                            <td><%# Eval("Memo") %></td>
                            <td align="left" style="display:none">
                               
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
    <div id="printCosntent" style="display:none;" >
       
        <br />
        <br />
        <h2>发车时间：</h2>
        <h2>排班班号：</h2>
        <table style="width:200mm;">
            <tr>
                <th style="width:20%;text-align:center;">销售人</th>
                <th style="width:25%;text-align:center;">线路名称</th>
                <th style="width:10%;text-align:center;">人数</th>
                <th style="width:15%;text-align:center;">客人电话</th>
                <th style="width:15%;text-align:center;">上车地点</th>
                <th style="width:15%;text-align:center;">备注</th>
            </tr>
            <asp:Repeater ID="rptPrint" runat="server">
                <ItemTemplate>
                     <tr>
                        <td style="width:20%;text-align:center;"><%# Eval("CPName") %></td>
                        <td style="width:25%;text-align:center;"><%# Eval("ProductName") %></td>
                        <td style="width:10%;text-align:center;"><%# Eval("NUM") %></td>
                        <td style="width:15%;text-align:center;"><%# Eval("Phone") %></td>
                        <td style="width:15%;text-align:center;"><%# Eval("StartAddress") %></td>
                        <td style="width:15%;text-align:center;"><%# Eval("Memo") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div style="width:210mm; margin:0 auto;text-align:center;">
            <asp:Literal ID="litPrintCount" runat="server" />
        </div>
    </div>
    </form>
    <br />
    <br />
</body>
</html>
