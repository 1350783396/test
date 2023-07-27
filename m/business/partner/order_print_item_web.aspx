<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_print_item_web.aspx.cs" Inherits="ETicket.Web.business.partner.order_print_item_web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/js/jquery-1.7.2.min.js" type="text/javascript"></script>
 
    <style type="text/css">
        body, table {
            font-size: 15px;
        }
        table {
            width: 100%;
        }
        .pageFormContent {
            line-height: 25px;
            font-size: 16px;
        }
        .pageFormContent span {
                font-size: 15px;
                font-weight: bold;
                line-height: 20px;
                font-size: 15px;
        }
        body, div {
            margin: 0;
            padding: 0;
            font-size: 16px;
            line-height: 20px;
        }
        hr {
            height: 1px;
            border: 0px;
            border-bottom: 1px #000000 dashed;
        }
        em {
            font-family: Arial, Helvetica, sans-serif;
            font-style: normal;
            text-decoration: none;
        }
        .salesid {
            position: absolute;
            top: 10px;
            left: 10px;
            font-size: 16px;
            line-height: 20px;
        }
    </style>
</head>
<body>
<br />
<br />
<div style='text-align:center'><button onclick='printWin()' style='padding-left:4px;padding-right:4px;width:100px;'>打  印</button><button onclick='window.opener=null;window.close();'  style='padding-left:4px;padding-right:4px;width:100px;'>关  闭</button></div>
<script type="text/javascript">
    function printWin() {
        var oWin = window.open("", "_blank");
        oWin.document.write(document.getElementById("content").innerHTML);
        oWin.focus();
        oWin.document.close();
        oWin.print()
        oWin.close()
    }
</script>
<hr size='1' style="border:2px solid #000;" />
<div id="content">
    <div class="page">
        <div class="pageContent">
            <div class="pageFormContent" id="oDiv">
                <div>
                    <%=ETicket.Web.HtmlController.Instance.OrderPrintForMulNum(NJiaSu.Libraries.PubFun.QueryInt("id")) %>
                    
                </div>
            </div>
        </div>
    </div>
</div>
<hr size='1' style="border:2px solid #000;" />
<h4 style='font-size:18px; text-align:center;'>打印预览内容结束</h4>
</body>
</html>
