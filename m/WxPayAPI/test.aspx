<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="ETicket.Web.WxPayAPI.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
   <script type="text/javascript">  
  
       //调用微信JS api 支付  
       function jsApiCall()  
       {  
           WeixinJSBridge.invoke(  
           'getBrandWCPayRequest',  
           <%=wxJsApiParam%>,//josn串  
                    function (res)  
                    {  
                        WeixinJSBridge.log(res.err_msg);  
                        if(res.err_msg == "get_brand_wcpay_request:ok" ) {  
                            document.getElementById("payFail").style.display="none";  
                            document.getElementById("paySuccess").style.display="block";  
                        }  
                        else{  
                            document.getElementById("payFail").style.display="block";  
                        }  
                    }  
                    );  
               }  
  
               function callpay()  
               {  
                   //官方代码，不贴了  
               }  
               callpay();  
     </script>  
  
<body>  
    <dl class="item_desc" id="dlPayInfo">  
        <dt class="item_title"><span class="txt">支付信息</span></dt>  
        <dd class="item_panel pl1">  
            <span class="title">33</span>  
        </dd>  
        <dd class="item_panel pl1">  
            <span class="title">订单号：</span><span class="selected com_c4">11</span>  
        </dd>  
        <dd class="item_select item_last">          
            <div class="title">  
                <strong>应付金额： </strong><span class="com_c4">11 元</span>  
            </div>          
        </dd>  
    </dl>  
    <br />  
    <dd class="item_select" id="paySuccess" style="display:none">  
        <div class="title" style="padding-left:3rem">  
            <img src="/images/onSuccess.gif" title="支付成功" />  
            <span class="selected"><b class="big">支付成功!</b><br />  
                系统正在处理收款，订单状态可能会有1-5分钟的滞后，感谢您的耐心等待。<br />  
                <a href="/member/order/info1/">查看订单</a><br /><br />  
                您还可以：<a href="/">继续购物</a><br /><br />  
                <br /><br />  
            </span>  
        </div>  
    </dd>  
    <dd class="item_select"id="payFail" style="display:none">  
        <div class="title item" style="padding-left:2.0rem">  
            <span class="selected"><b class="big">支付失败!</b></span>  
            <div class="btns" style="margin-top:10px"><a class="com_btn_7" style="padding:1px;cursor:pointer;" onclick="callpay()">重新支付</a></div>  
        </div>  
        <div class="title item" style="padding-left:2.0rem">  
            <span class="selected">  
                您可以查看微信交易记录。如果没有扣费，可以稍后尝试重新支付。如果已经扣费，请拨打客服电话联系我们  
                <br /><br />  
                如果多次重新支付还是遇到了问题，稍后您可以在登陆状态下，访问个人中心-><a href="/member/order/">我的订单</a>，为编号为11的订单付款，有多种支付方式供您选择    
                ...  
            </span>  
        </div>  
    </dd>  
</body>  
</html>
