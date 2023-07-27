<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="ETicket.Web.openapi.product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>景区</h1>
    <table>
        <tr>
            <td style="width:100px;">产品ID</td>
            <td>产品名称</td>
        </tr>
        <asp:Repeater ID="repTicket" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("ProductID") %></td>
                    <td><%#Eval("ProductName") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
     </table>
    <h1>专线</h1>
    <table>
        <tr>
            <td style="width:100px;">产品ID</td>
            <td>产品名称</td>
            <td>发车时间</td>
            <td>上车地点</td>
        </tr>
        <asp:Repeater ID="repLine" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("ProductID") %></td>
                    <td><%#Eval("ProductName") %></td>
                    <td><asp:Literal ID="litTime" runat="server" /></td>
                    <td><asp:Literal ID="litAddress" runat="server" /></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
     </table>
    </div>
    </form>
</body>
</html>
