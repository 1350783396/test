<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sms_ad_setting.aspx.cs" Inherits="ETicket.Web.business.setting.sms_ad_setting" %>


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
                    <b style="color:red;">
                      注：<br />
                    1、如有多条重复或时间交错的配置，最新添加的配置起作用；<br />
                    <br />
                    2、为了不影响短信发送的性能，请最多配置50条，超过50条，请删除后再添加；</b>
                     <br />
                    <br />
                    <table class="formTable" style="width: 900px;">
                        <tr>
                            <td  style="height:25px;">
                                <asp:Label ID="lblPKID" runat="server" Visible="false"></asp:Label>
                                开始日期：
                                <input type="text" id="txtStart" name="txtStart" runat="server" onclick="WdatePicker()" />
                                &nbsp;&nbsp;
                                接收日期：
                                <input type="text" id="txtEnd" name="txtEnd" runat="server" onclick="WdatePicker()"/>
                          
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 广告内容：
                               <textarea id="txtAD" name="txtAD" runat="server" style="height:50px;width:500px;"></textarea>
                              
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center">
                                 <asp:Button runat="server" ID="btnSave" Text="新增" Width="100" />
                                    &nbsp;&nbsp;
                                   <asp:Button runat="server" ID="btnCancel" Text="取消" Visible="false" Width="100"/>
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
                                        开始日期
                                    </th>
                                    <th class="sortable">
                                        结束日期
                                    </th>
                                    <th class="sortable">
                                        广告内容
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
                               <%#Convert.ToDateTime(Eval("StartTime")).ToString("yyyy-MM-dd")%>
                            </td>
                            <td>
                                 <%#Convert.ToDateTime(Eval("EndTime")).ToString("yyyy-MM-dd")%>
                            </td>
                            
                            <td>
                                  <%#Eval("ADContent")%>
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
                 <br />
                  
                </div>
            </div>
        </div>
    </form>
    <br />
    <br />
</body>
</html>