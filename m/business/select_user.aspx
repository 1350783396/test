<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="select_user.aspx.cs" Inherits="ETicket.Web.business.select_user" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <br />
            名称：<asp:TextBox ID="txtCPName" runat="server" /><asp:Button ID="btnQuery" runat="server" Text="搜索" style="width:100px;"/>
            <br />
            <h4><asp:Literal ID="lblCount" runat="server" /></h4>
            <asp:RadioButtonList ID="rbtnList" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" AutoPostBack="true" />
            <br />
            <br />
            <asp:Button ID="btnClear" runat="server" Text="清空选择" style="width:100px;"/>
             <br />
                <div style="text-align: center">
                   <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="12">
                   </webdiyer:AspNetPager>
                </div>
          
        </ContentTemplate>
        <Triggers>
  <%--<asp:AsyncPostBackTrigger ControlID="btnSelectALL" EventName="Click" />--%>
  </Triggers>
    </asp:UpdatePanel>  
      <asp:Literal ID="litValue" runat="server" />
    </form>
  
</body>
</html>
