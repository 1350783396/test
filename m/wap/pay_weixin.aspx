<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_weixin.aspx.cs" Inherits="ETicket.Web.wap.pay_weixin" %>

<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title>微信支付</title>
        <meta charset="utf-8">
        <meta name="viewport" content=" width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
        <link rel="stylesheet" href="/wap/cssapp/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-box.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-base.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-color.css">
        <link rel="stylesheet" href="/wap/cssapp/appcan.icon.css">
        <link rel="stylesheet" href="/wap/cssapp/appcan.control.css">
        <link rel="stylesheet" href="/wap/cssapp/pay_main.css">

        <script type="text/javascript">
              //调用微信JS api 支付
              function jsApiCall()
              {
                   WeixinJSBridge.invoke(
                           'getBrandWCPayRequest',
                           <%=wxJsApiParam%>,//josn串
                    function (res)
                    {
                        //WeixinJSBridge.log(res.err_msg);
                        //alert(res.err_code + res.err_desc + res.err_msg);
                        if (res.err_msg == "get_brand_wcpay_request:ok") {
                            window.location.href="/wap/pay_succ.aspx";
                        }
                    }
                    );
               }

               function callpay()
               {
                   if (typeof WeixinJSBridge == "undefined")
                   {
                       if (document.addEventListener)
                       {
                           document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
                       }
                       else if (document.attachEvent)
                       {
                           document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                           document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
                       }
                   }
                   else
                   {
                       jsApiCall();
                   }
               }
     </script>
    </head>
    <body class="um-vp bc-bg" >
        <div class="ub ub-ver">
            <div class="uinn-a6 t-red  ulev-app1" style="font-size:18px; font-weight:bold;text-align:center;">
               	订单已提交，请完成支付。
               <input type="hidden" id="subject" value=""  />
               <input type="hidden" id="body" value=""/>
               <input type="hidden" id="fee" value="0"/>
               <input type="hidden" id="num" value=""/>
               <input type="hidden" id="orderID" value=""/>                        
            </div>
            <div class="ub ub-ver uinn-a1 ub-f1">
                <div class="uba bc-border c-wh">
                    <!--
                    <ul  class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                             订单编号
                        </li>
                        <li class="ub-f1 ulev-app1" id="spanSheetID">
                            
                        </li>
                    </ul>
                    -->
                    <ul class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                            姓名
                        </li>
                        <li class="ub-f1 ulev-app1" id="spanZhName">
                            <%=zhName %>
                        </li>
                    </ul>
                    <ul class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                            手机号码
                        </li>
                        <li class="ub-f1 ulev-app1" id="spanPhone">
                            <%=phone %>
                        </li>
                    </ul>
                    <ul class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                            商品名称
                        </li>
                        <li class="ub-f1 ulev-app1" id="spanProductName">
                            <%=proName %>                                             
                        </li>
                    </ul>
                    <ul class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                            购买数量
                        </li>
                        <li class="ub-f1 ulev-app1">
                            <span class="t-red1 ufm1" id="spanNum"> <%=num %>  </span>
                        </li>
                    </ul>
                    <ul class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                            单价
                        </li>
                        <li class="ub-f1 ulev-app1">
                            <span class="t-red1 ufm1" id="spanUnitPrice"> ￥<%=fxUnit %></span>
                        </li>
                    </ul>
                    <ul class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                           合计金额
                        </li>
                        <li class="ub-f1 ulev-app1">
                            <span class="t-red1 ufm1" id="spanTotalPrice">￥<%=fxTotal %></span>
                        </li>
                    </ul>
                    <ul class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                             支付方式
                        </li>
                        <li class="ub-f1 ulev-app1" id="spanPayType">
                             微信支付                                                   
                        </li>
                    </ul>
                     
                </div>
            </div>
            <div class="ubt bc-border ub sc-bg uinn">
                <div class="btn ub ub-ac bc-text-head ub-pc bc-btn uc-a1 ub-f1 " style="min-height: 1.8em;"  id="btnSubmit" onclick="callpay();">
                      立刻支付
                </div>
            </div>    
        </div>
		<!--
        <script src="/wap/js/jquery-2.1.1.min.js"></script>
            -->
      
    </body>
    
</html>