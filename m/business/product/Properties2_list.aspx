﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Properties2_list.aspx.cs" Inherits="ETicket.Web.business.product.Properties2_list" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src sousouhao.cn ="/js/common.js"></script>
    <script  type="text/javascript" src="/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script  type="text/javascript" src="/js/chkSelectAll.js"></script>
    
</head>
<body style="background:none">
    <form id="form1" runat="server">
    <div id="page">
        <div id="content" class="clearfix">
            <div id="main">
                <br />
     
                   <asp:Button ID="btnDelete2" Text="删除所选" Width="100" runat="server" CssClass="btn" 
                                OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" OnClick="btnDelete2_Click" />
                   &nbsp; &nbsp;
                             <asp:Button ID="Button1" Text="新增" Width="100" runat="server" CausesValidation="false"  CssClass="btn" />&nbsp;&nbsp;
                <br /><br /><h3>
                    <asp:Literal ID="litMsg" runat="server"></asp:Literal></h3>
                <br />
                <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table cellpadding="0" class="table estList" cellspacing="0" border="1" id="process" style="width:600px;">
                            <thead>
                                <tr>
                                    <th class="sortable" style="width:100px;">
                                        <asp:CheckBox ID="chkDeleteALL" runat="server" Text="全选"/>
                                    </th>
                                    <th class="sortable" >
                                        属性分类
                                    </th>
                                    <th class="sortable" >
                                        名称
                                    </th>
                                   <th class="sortable" >
                                        排序
                                    </th>
                                    <th class="sortable" >
                                        操作
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="odd">
                            <td>
                                <asp:CheckBox ID="chkDelete" runat="server" />
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%#Eval("ID") %>' />
                            </td>
                            <td>
                                <%#GetProperties1(Eval("Pid").ToString()) %>
                            </td>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <%#Eval("Sort") %>
                            </td>
                            <td>
                                <a href="Properties2_edit.aspx?id=<%#Eval("ID") %>">修改</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
                <asp:Button ID="btnDelete" Width="100" Text="删除所选" runat="server" CssClass="btn" OnClick="btnDelete2_Click" 
                    OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" />
                &nbsp;&nbsp;
                <asp:Button ID="btnSave" Text="新增" Width="100" runat="server" CausesValidation="false" CssClass="btn" />
                <br />
            </div>
        </div>
    </div>
    </form>
    <br />
    <br />
</body>
</html>

