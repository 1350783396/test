<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view.aspx.cs" Inherits="ETicket.Web.wap.view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        //是否是微信
        function is_weixn() {
            var ua = navigator.userAgent.toLowerCase();
            if (ua.match(/MicroMessenger/i) == "micromessenger") {
                return true;
            } else {
                return false;
            }
        }
        var q = "<%=NJiaSu.Libraries.PubFun.QueryString("q")%>";

        window.location.href = "/wap/view_2.aspx?q=" + q;
        /*
        if (is_weixn()) {
            window.location.href = "/wx_auth_wap.aspx?freq=1&page=view&q=" + q;
        }
        else {
            window.location.href = "/wap/view_2.aspx?q=" + q;
        }
        */
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
