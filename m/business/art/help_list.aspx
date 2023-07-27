<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="help_list.aspx.cs" Inherits="ETicket.Web.business.art.help_list" %>

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
    <style type="text/css">
        .linkAction
        {
            padding:0px 5px;
        }
    </style>
    <script type="text/javascript">
        function add()
        {
            window.location.href = "help_add.aspx?mid="+<%=NJiaSu.Libraries.PubFun.QueryInt("mid")%>;
            return false
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <div id="content" class="clearfix">
            <div id="main">
                <br />
                <br />
                 <asp:Button ID="btnAdd" Text="新增记录" Width="200" Height="40" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="return add()" />
                <br />
                <br />
                <table class="formTable" style="width:800px;">
                    <tr>
                        <td class="label">
                            标题
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTitel" runat="server" Width="200"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            发布时间
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtStartTime" runat="server" onclick="WdatePicker()"/>-- <asp:TextBox ID="txtEndTime" runat="server" onclick="WdatePicker()"/>
                        </td>
                         
                    </tr>
                    <tr>
                        <td  align="center" colspan="2">
                              <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('系统正在查询。。。。请稍等！')"/>
                             &nbsp;&nbsp;
                              <asp:Button ID="btnRefrech" Text="刷新" Width="100" runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="sAlert('正在重新刷新数据。。。。请稍等！')"/>
                        </td>
                    </tr>
                </table>
                <br />
               
                <h3><asp:Literal ID="lblCount" runat="server" /></h3>
               
                <asp:Button ID="btnDel" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');"
                    CssClass="btn" />
                 &nbsp;&nbsp;
                <br />
                <br />
                <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table class="table estList" cellspacing="0" border="1">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:CheckBox id="chkAll" Text="全选" runat="server" />
                                    </th>
                                    <th>
                                        标题
                                    </th>
                                     <th>
                                        发布时间
                                    </th>
                                    <th>
                                        最后编辑时间
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="even">
                            <td>
                                <asp:CheckBox ID="chkItem" runat="server" BorderStyle="None" BorderWidth="0px" />
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Eval("HelpID") %>' />
                            </td>
                            <td>
                                 <%# Eval("HelpTitle") %>
                            </td>
                            <td>
                                <%# Eval("AddTime") %>
                            </td>
                            <td>
                                <%# Eval("UpdateTime") %>
                            </td>
                            <td align="left">
                                <asp:HyperLink ID="hyEdit" runat="server" CssClass="linkAction" >编辑</asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
                <asp:Button ID="btnDel2" Text="删除所选" Width="100" runat="server" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" />
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
