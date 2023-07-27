<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qrcode.aspx.cs" Inherits="ETicket.Web.QRCode" %>

<!DOCTYPE html>
<html>
<head >
    <meta charset="utf-8">
    <title>二维码</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:300px;margin:auto;">
            <div style="text-align:center;margin:20px 0px; font-size:26px;word-break:break-all;" class="ulev-3">
                <%=productName %>
            </div>
            <div style="text-align:center;">
                <asp:Image Height="200" Width="200" runat="server" ID="imgQRCode" />
            </div>
        </div>
    </form>
</body>
</html>
