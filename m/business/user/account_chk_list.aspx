<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account_chk_list.aspx.cs" Inherits="ETicket.Web.business.user.account_chk_list" %>

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
                            账号
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserName" runat="server" Width="200"/>
                        </td>
                        <td class="label">
                            级别
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlUserLevel" runat="server" Width="200"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            联系电话
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPhone" runat="server" Width="200"/>
                        </td>
                         <td class="label">
                            分销商名称
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCPName" runat="server" Width="200"/>
                        </td>
                    </tr>
                </table>
                
                <br />
                <asp:Button ID="btnQuery" Text="查询" Width="100" runat="server" CausesValidation="false" CssClass="btn" OnClientClick="sAlert('系统正在查询中。。。。请稍等')"/>
                &nbsp;&nbsp;
                <asp:Button ID="btnRefresh" Text="刷新" Width="100" runat="server" CausesValidation="false" CssClass="btn" OnClientClick="sAlert('系统正在刷新数据中。。。。请稍等')"/>
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" Text="本页导出Excel" Width="100" runat="server"  CssClass="btn"  />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcelALL" Text="所有导出Excel" Width="100" runat="server"  CssClass="btn"  />
                <h3><asp:Literal ID="lblCount" runat="server" /></h3>
                <table class="table" cellspacing="0" border="1">
                    <asp:Repeater ID="repList" runat="server">
                        <HeaderTemplate>
                                <thead>
                                    <tr>
                                        <th style="display:none">
                                            选择
                                        </th>
                                        <th>
                                            账号 
                                        </th>
                                        <th>
                                            联系号码
                                        </th>
                                        <th>
                                            分销商名称
                                        </th>
                                        <th>
                                            级别
                                        </th>
                                        <th>
                                            充值金额
                                        </th>
                                        <th>
                                            申请日期
                                        </th>
                                        <th>
                                            申请人
                                        </th>
                                        <th>
                                            当前状态
                                        </th>
                                        <th>
                                            审核人
                                        </th>
                                        <th>
                                            审核时间
                                        </th>
                                        <th>
                                            操作
                                        </th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="display:none">
                                    <input type="checkbox" id="ChkOne" name="ChkOne" value='<%# Eval("UserID") %>' />
                                    <asp:Label ID="lblUserID" runat="server" Visible="false" />
                                </td>
                                <td>
                                    <%# Eval("UserName") %>
                                </td>
                                <td>
                                    <%# Eval("Phone") %>
                                </td>
                                <td>
                                    <%# Eval("CPName") %>
                                </td>
                                <td>
                                    <asp:Literal ID="litLevelName" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <%# Eval("Requst_Amount") %>
                                </td>
                                <td>
                                     <%# Eval("Requst_Time") %>
                                </td>
                                <td>
                                   <asp:Literal ID="litRequestUser" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <%# Eval("Stauts") %>
                                </td>
                                <td>
                                    <asp:Literal ID="litChkUser" runat="server"></asp:Literal>
                                </td>
                                <td>
                                     <%# Eval("CHK_Time") %>
                                </td>
                                <td align="left" class="ht5" >
                                    <asp:HyperLink ID="hyDetail" runat="server" CssClass="linkAction">详情</asp:HyperLink>
                                    <asp:HyperLink ID="hyEdit" runat="server" CssClass="linkAction">审核</asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></FooterTemplate>
                    </asp:Repeater>
                    <tr>
                        <td style="display:none"></td>
                        <td> </td>
                        <td></td>
                        <td> </td>
                        <td></td>
                        <td><asp:Literal ID="litCount" runat="server" /></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="display:none"></td>
                        <td> </td>
                        <td></td>
                        <td> </td>
                        <td></td>
                        <td><asp:Literal ID="litSum" runat="server" /></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="Button1" Text="删除所选" Width="100" runat="server"  Visible="false" OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" CssClass="btn" />
                &nbsp; &nbsp;
                <asp:Button ID="btnSortSave" Text="保存排序" Width="100" runat="server" CssClass="btn"  Visible="false"/>
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

