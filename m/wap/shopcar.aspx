<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopcar.aspx.cs" Inherits="ETicket.Web.wap.shopcar" %>

<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title>填写订单</title>
        <meta charset="utf-8">
       
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <meta name="apple-mobile-web-app-capable" content="yes" />
        <meta name="apple-mobile-web-app-status-bar-style" content="black" />
        <meta name="format-detection" content="telephone=no" />
        <style>
            .toast_css{
                z-index:1003;
                font-size: 1.37em;
                position: fixed;
                bottom:30%;
                width: 100%;
                opacity:0;
                height: 24px;
                display: none;
			    transition: opacity 1s ease-out;
			}
        </style>
        <link rel="stylesheet" href="/wap/cssapp/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-box.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-base.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-color.css">
        <link rel="stylesheet" href="/wap/cssapp/appcan.icon.css">
        <link rel="stylesheet" href="/wap/cssapp/appcan.control.css">
        <link rel="stylesheet" href="/wap/cssapp/shopcar_main.css">
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
        </script>
    </head>
    <body class="um-vp bc-bg">
        <div class="ub ub-ver" id="car">
            <%=html %>
        </div>
        <div id="toastId2" class="toast_css" style="display: none; opacity: 0;"></div>
        <script src="/wap/js/jquery-2.1.1.min.js"></script>
        
    </body>
    <script>
        var jq = jQuery.noConflict();

        
        jq(keyCal());
        //计算总价
        function keyCal() {
            var unit = jq("#unitPrice").html();
            var num = parseInt(jq("#buyNum").val());
            if (!isNaN(num) || num > 0) {
                jq("#totalPrice").html(parseFloat(unit) * num);

            }
        }
        //购物数量减
        jq("#btnNumJian").click(
            function(){
                var buyNum = jq("#buyNum").val();
                var buyNumInt = parseInt(buyNum);
                if (buyNumInt <= 1) {
                    buyNumInt = 1;
                } else {
                    buyNumInt = buyNumInt - 1;
                }
                jq("#buyNum").val(buyNumInt);
                keyCal();
            }
        );
        //购物数量加
        jq("#btnNumJia").click(
           function () {
               var buyNum = jq("#buyNum").val();
               var buyNumInt = parseInt(buyNum);
               if (buyNumInt <= 0) {
                   buyNumInt = 1;
               } else {
                   buyNumInt = buyNumInt + 1;
               }
               jq("#buyNum").val(buyNumInt);
               keyCal();
           }
       );
       jq("#btnSubmit").click(
          function () {
              toastshow(2);

              var qContent="<%=NJiaSu.Libraries.PubFun.QueryString("q")%>";
              //发车日期时间
              var startDate = jq("#startDate").val();
              var startTime = jq("#startTime").val();
              //上车地点
              var startAddress = jq("#startAddress").val();
              //游玩日期
              var palyDate = jq("#palyDate").val();
              var proID = jq("#hiddenProductID").val();
              var buyNum = jq("#buyNum").val();
              var selPayType = jq("#selPayType").val();
              var selValidType = jq("#selValidType").val();
              var realName = jq("#realName").val();
              var userPhone = jq("#phone").val();
              var txtMemo = jq("#txtMemo").val();
              if (parseInt(buyNum) <= 0) {
                  //uexWindow.toast(0, 5, "购买数量必须大于1", 2000);
                  toastshow("购买数量必须大于1");
                  return;
              }
              if (realName == "") {
                  //uexWindow.toast(0, 5, "请输入门票使用人姓名", 2000);
                  toastshow("请输入门票使用人姓名");
                  return;
              }
              
              if (userPhone == "") {
                  //uexWindow.toast(0, 5, "请输入二维码接收手机", 2000);
                  toastshow("请输入短信码接收手机");
                  return;
              }
             

              jq.ajax({
                  type: 'POST',
                  url: "/ajax/fx_order.ashx",
                  dataType: "json",
                  data: {
                      api_startdate: startDate, api_starttime: startTime, api_startaddr: startAddress,
                      api_playdate: palyDate, api_productid: proID, api_buyNum: buyNum, api_paytype: selPayType,
                      api_validtype: selValidType, api_realname: realName, api_phone: userPhone, api_Memo: txtMemo,
                      md5: "", token: "", appos: "", q: qContent
                  },
                  beforeSend: function () {
                      //uexWindow.toast(1, 5, "正在提交订单...", -1);
                      toastshow("正在提交订单...");
                  },
                  error: function () {
                      //uexWindow.closeToast();
                      //uexWindow.toast(0, 5, "提交订单失败，请重试", 2000);
                      toastshow("提交订单失败，请重试");
                  },
                  success: function (result) {
                      toastshow("提交成功！");
                      //uexWindow.closeToast();
                      if (result.Status == "0") {
                          //uexWindow.toast(0, 5, result.Msg, 4000);
                          toastshow(result.Msg);
                      }
                      else if (result.Status == "1") {
                          var itemData = result.List[0];
                          var orderID = itemData.OrderID;
                          if (is_weixn()) {
                              window.location.href = "/wap/pay_weixin.aspx?freq=shopcar&zq=" + qContent + "&zo=" + orderID;
                          }
                          else {
                              window.location.href = "/payapi/alipay_wap/go.aspx?freq=shopcar&zq=" + qContent + "&zo=" + orderID;
                          }
                          /*
                          var payType = itemData.PayType;
                          if (payType == "在线支付") {
                              localStorage.setItem("view_order_id", orderID);
                              appcan.window.evaluateScript("shopcar", "closeShopCar()");
                              appcan.window.open('pay_online_sel', 'pay_online_sel.html', 10);
                          }
                          else if (payType == "积分支付") {
                              localStorage.setItem("view_order_id", orderID);
                              appcan.window.evaluateScript("shopcar", "closeShopCar()");
                              appcan.window.open('pay_account', 'pay_account.html', 10);
                          }
                          */
                      }
                  }
              });//end ajax
          }//end function
       );

    </script>

    		<script type="text/javascript">
    		    function $S(s) { return document.getElementById(s); }
    		    function $html(s, html) { $S(s).innerHTML = html; }
    		    var toastTime2 = null;
    		    var displayTime2 = null;
    		    function setToast3(html) {
    		        if (toastTime2 != null) {
    		            clearTimeout(toastTime2);
    		            clearTimeout(displayTime2);
    		        }
    		        $S('toastId2').style.display = 'block';
    		        $S('toastId2').style.opacity = 1;
    		        $html('toastId2', html);
    		        toastTime2 = setTimeout(function () {
    		            $S('toastId2').style.opacity = 0;
    		            displayTime2 = setTimeout(function () { $S('toastId2').style.display = 'none'; }, 1000);
    		        }, 1000);
    		    }
    		    function toastshow(msg) {
    		        setToast3('<div style="color:#fff;background: rgba(0, 0, 0, 0.6);border-radius: 2px;padding: 2px;text-align: center;width:235px;margin: 0 auto;">' + msg + '</div>');
    		    }
		</script>
</html>