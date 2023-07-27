<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="one_update.aspx.cs" Inherits="ETicket.Web.business.product.one_update" %>

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

    <script type="text/javascript" src="/plugin/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <div id="content" class="clearfix">
            <div id="main">
                <br />
                <br />
                <font color="red">说明：一键维护提供快捷维护专线销售时间和库存、景区库存</font>
                 <br />
                <br />
                <table class="formTable" style="width:900px">
                    <tr>
                        <td colspan="2"><h3>专线维护</h3></td>
                    </tr>
                    <tr>
                        <td class="label">
                            更新销售日期<asp:CheckBox ID="chkLineUpdateTick" runat="server" />
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLineSaleDate" runat="server" onclick="WdatePicker()"/>&nbsp;&nbsp;注：请选择日期，格式如：2014-06-01
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            更新库存<asp:CheckBox ID="chkLineStock" runat="server" />
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLineStock" runat="server" />&nbsp;&nbsp;注：填写数字，格式如：100
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><h3>景区维护</h3></td>
                    </tr>
                      <tr>
                        <td class="label">
                            更新库存<asp:CheckBox ID="chkTicketStock" runat="server" />
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTicketStock" runat="server" />&nbsp;&nbsp;注：填写数字，格式如：100
                        </td>
                    </tr>
                </table>
                <br />
               
               
                 &nbsp;&nbsp;
                 <asp:Button ID="btnQuery" Text="执行" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="return sys_confirm('确认要批量批量更新数据？')"/>
              
             
              
              
            </div>
        </div>
    </div>
    </form>
    <br />
    <br />
</body>
</html>

