<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_print_item.aspx.cs" Inherits="ETicket.Web.business.partner.order_print_item" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        $(document).ready(function () {
            $.ajax({
                type: 'post',
                url: '/ajax/print_order_item.ashx?id=<%=NJiaSu.Libraries.PubFun.QueryInt("id")%>',
                data: {

                },
                beforeSend: function (XMLHttpRequest) {
                },
                success: function (data, textStatus) {

                },
                complete: function (XMLHttpRequest, textStatus) {
                },
                error: function () {
                }
            });
        });
        
    </script>
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
    <div class="page">
        <div class="pageContent">
            <div class="pageFormContent" id="oDiv">
                <div>
                    <%=ETicket.Web.HtmlController.Instance.OrderPrint(NJiaSu.Libraries.PubFun.QueryInt("id")) %>
                    <%Response.End(); %>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
