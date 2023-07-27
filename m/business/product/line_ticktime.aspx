﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="line_ticktime.aspx.cs" Inherits="ETicket.Web.business.product.line_ticktime" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <link rel="stylesheet" type="text/css" href="/business/style/nav/nav.css" />
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
                    <table class="formTable" style="width: 900px;">
                        <tr>
                            <td >
                             <div>
                                <div id="divProduct" runat="server"><asp:HyperLink ID="hyProduct" runat="server" Text="专线基本信息" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div id="divPrice" runat="server"><asp:HyperLink ID="hyPrice" runat="server" Text="价格" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div  id="divTickTime" runat="server"><asp:HyperLink ID="hyTickTime" runat="server" Text="销售时间" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div  id="divStock" runat="server"><asp:HyperLink ID="hyStock" runat="server" Text="库存" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div  id="divAddress" runat="server"><asp:HyperLink ID="hyAddress" runat="server" Text="上车地点" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div id="divPublish" runat="server"><asp:HyperLink ID="hyPublish" runat="server" Text="发布" /></div>
                                <div style="clear: both"></div>
                             </div>
                            <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:25px;">
                                <asp:Label ID="lblPKID" runat="server" Visible="false"></asp:Label>
                            
                                发车时间：
                               <input type="text" id="txtStartTime" name="txtStartTime" runat="server" onclick="WdatePicker({ dateFmt: ' HH:mm' })" />
                                  &nbsp;&nbsp;
                                 最后下单时间：
                               <input type="text" id="txtLastOrderTime" name="txtLastOrderTime" runat="server" onclick="WdatePicker({ dateFmt: ' HH:mm' })" />
                                &nbsp;&nbsp;
                                  &nbsp;&nbsp;
                               <asp:Button runat="server" ID="btnSave" Text="新增" />
                                &nbsp;&nbsp;
                               <asp:Button runat="server" ID="btnCancel" Text="取消" Visible="false" />
                            </td>
                          
                        </tr>
                       
                    </table>
                    <br />
                    <br />
                 <h3><asp:Literal ID="litCount" runat="server" /></h3>
                 <asp:Repeater ID="repList" runat="server">
                    <HeaderTemplate>
                        <table class="table estList" id="bk" style="width: 900px;">
                            <thead>
                                <tr>
                                    <th class="sortable">
                                        <asp:CheckBox id="chkAll" Text="全选" runat="server" />
                                    </th>
                                 
                                    <th class="sortable">
                                        发车时间
                                    </th>
                                    <th class="sortable">
                                        最后下单时间
                                    </th>
                                 
                                    <th class="sortable">
                                        操作
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="odd">
                            <td>
                                <asp:CheckBox ID="chkItem" runat="server" BorderStyle="None" BorderWidth="0px" />
                                <asp:Label ID="lblPKID" runat="server" Text='<%#Eval("PKID") %>' Visible="false" />
                            </td>
                        
                            <td>
                                 <%#Eval("StartH")%>时<%#Eval("StartM")%>分
                            </td>
                            <td>
                                <%#Eval("LastOrderH")%>时<%#Eval("LastOrderM")%>分
                            </td>
                        
                            <td>
                                <asp:LinkButton ID="lbtnEdit" runat="server" Text="编辑"></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                 </asp:Repeater>
                 <br />
                <asp:Button ID="btnDelete" Width="100" Text="删除所选" runat="server" CssClass="btn" 
                    OnClientClick="return sys_confirm('确认要删除所选择的数据吗，删除无法还原？');" />
                </div>
            </div>
        </div>
    </form>
    <br />
    <br />
</body>
</html>