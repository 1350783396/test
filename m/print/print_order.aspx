<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print_order.aspx.cs" Inherits="ETicket.Web.print.print_order" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    
    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
      <script type="javascript" src="/js/jquery.PrintArea.js"></script>  

    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script  type="text/javascript" src="/js/chkSelectAll.js"></script>

  
    
    <script type="text/javascript">
        $(function () {
            function print() {
                var options = { mode: 'popup', popWd: 900, popHt: 500, popX: 0, popY: 0, popTitle: '打印订单', popClose: true };
                $("#printContent").printArea();
                alert("打印完毕")
            };
            function doPrint() {
                //获取要打印的编辑区域
                var oPrintSection = $("div.printContent");
                //获取将要打印的区域
                var oPrintArea = $("div.PrintArea");
                oPrintArea.html(oPrintSection.html());
                //先获取textbox的id,并进行修改。
                oPrintArea.find("input[type='text']").each(function () {
                    $(this).attr("id", "new_" + $(this).attr("id"));
                });
                //过滤过按钮控件
                oPrintArea.find(".btn").remove();
                //$('div.PrintArea').find("input[type='text']").remove();
                //替换文本框
                oPrintArea.find("input[type='text']").each(function () {
                    var itemNewId = "";
                    itemNewId = $(this).attr("id");
                    $(this).replaceWith("<span>" + $("#" + itemNewId.replace(/new_/, "")).val() + "</span>");
                });
                var options = { mode: 'popup', popWd: 900, popHt: 500, popX: 0, popY: 0, popTitle: '打印订单', popClose: true };
                //var options = { mode: 'iframe', popClose: false };
                oPrintArea.printArea(options);
                return false;
            };
        })
       
     </script>  
        <script type="text/javascript">
            
    </script>
    <style>
         body {
            padding-top: 40px;
            padding-left: 40px;
        }
        .CPName{
            font-size:20px;
            font-weight:bold;
             text-align:center;
        }
         .ProductName{
            font-size:18px;
            font-weight:bold;
            text-align:center;
        }
        .ov{
            font-weight:bold;
        }
        .ovH{
            padding-top:10px;
        }
        .cCenter{
             text-align:center;
        }
        .splitTD{
            border-bottom:1px dashed #000000;
        }
        .title{
            text-align:right;
        }
    </style>
</head>

<body style="background-color: #f9f9f9;">
    <div  style="width:400px; background-color:#ffffff">
    <form runat="server">

    <div style="text-align:center;padding-top:5px;">
        <input type="button" onclick="print()" value="打    印" />
         &nbsp;
         &nbsp;
        <asp:Button ID="btnSave" runat="server" Text="确认完成打印" OnClientClick="return check()" Visible="false" />
    </div>
    <br />
         <div class="PrintArea" style="display: none; border: 1px solid #ccc;">
            </div>
    <div class="printContent">
    <%=PrintHTML %>
    <table style="width:300px;display:none">
         <tr>
            <td colspan="2">序号:11</td>
        </tr>
        <tr>
            <td colspan="2" class="CPName">分销商名字</td>
        </tr>
         <tr>
            <td colspan="2" class="ovH"></td>
        </tr>
         <tr>
            <td colspan="2" class="ProductName">产品名称</td>
        </tr>
         <tr>
            <td colspan="2" class="ovH"></td>
        </tr>
         <tr>
            <td class="title">发车时间：</td>
            <td class="ov">2013-11-30 20:11</td>
        </tr>
         <tr>
            <td colspan="2" class="splitTD"></td>
        </tr>
         <tr>
            <td class="title">人     数：</td>
            <td class="ov">3</td>
        </tr>
         <tr>
            <td class="title">客人姓名：</td>
            <td>张三</td>
        </tr>
         <tr>
            <td class="title">客人电话：</td>
            <td>13811111111</td>
        </tr>
         <tr>
            <td class="title">上车地点：</td>
            <td>武林大会</td>
        </tr>
        <tr>
            <td colspan="2" class="ovH"></td>
        </tr>
        <tr>
            <td class="title">销售人：</td>
            <td>上海</td>
        </tr>
        <tr>
            <td class="title">销售电话：</td>
            <td>6567893</td>
        </tr>
        <tr>
            <td class="title">订单号：</td>
            <td>1234567889955444544444</td>
        </tr>
        <tr>
            <td colspan="2" class="splitTD"></td>
        </tr>
        <tr>
            <td colspan="2" class="ovH"></td>
        </tr>
        <tr>
            <td colspan="2" class="cCenter">凭此单换取门票，限当日有效</td>
        </tr>
        <tr>
            <td colspan="2" class="ovH"></td>
        </tr>
        <tr>
            <td colspan="2" class="cCenter">请提前15分钟候车，过时不候</td>
        </tr>
    </table>
    </div>
    </form>
    </div>
   
  

</body>
</html>