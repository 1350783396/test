<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="send.aspx.cs" Inherits="Com.KuiQian.Send" %>


<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title></title>
        <meta charset="utf-8">
        <meta name="viewport" content="target-densitydpi=device-dpi, width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
        <meta http-equiv="pragma" content="no-cache">
		<meta http-equiv="cache-control" content="no-cache">
		<meta http-equiv="expires" content="0">  
        <link rel="stylesheet" href="css/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="css/ui-box.css">
        <link rel="stylesheet" href="css/ui-base.css">
        <link rel="stylesheet" href="css/ui-color.css">
        <link rel="stylesheet" href="css/appcan.icon.css">
        <link rel="stylesheet" href="css/appcan.control.css">
        <link rel="stylesheet" href="css/main.css">
        <style>
            input {
                min-height: 1.8em;
                line-height: 1.8em;
                margin: 0px;
                padding: 0px;
            }
        </style>
    </head>
    <body class="um-vp bc-bg"  style="font-size: 32px;">
        <div class="ub ub-ver">
            <div class="uhide">
                  <form id="kqPay" name="kqPay" action="https://sandbox.99bill.com/mobilegateway/recvMerchantInfoAction.htm?r=12312" method="post" style="width:100%">
				    <input type="hidden" name="inputCharset" value="<%=inputCharset%>" />
				    <input type="hidden" name="pageUrl" value="<%=pageUrl%>" />
				    <input type="hidden" name="bgUrl" value="<%=bgUrl%>" />
				    <input type="hidden" name="version" value="<%=version%>" />
				    <input type="hidden" name="language" value="<%=language%>" />
				    <input type="hidden" name="signType" value="<%=signType%>" />
				    <input type="hidden" name="signMsg" value="<%=signMsg%>" />
				    <input type="hidden" name="merchantAcctId" value="<%=merchantAcctId%>" />
				    <input type="hidden" name="payerName" value="<%=payerName%>" />
				    <input type="hidden" name="payerContactType" value="<%=payerContactType%>" />
				    <input type="hidden" name="payerContact" value="<%=payerContact%>" />
				    <input type="hidden" name="orderId" value="<%=orderId%>" />
				    <input type="hidden" name="orderAmount" value="<%=orderAmount%>" />
				    <input type="hidden" name="orderTime" value="<%=orderTime%>" />
				    <input type="hidden" name="productName" value="<%=productName%>" />
				    <input type="hidden" name="productNum" value="<%=productNum%>" />
				    <input type="hidden" name="productId" value="<%=productId%>" />
				    <input type="hidden" name="productDesc" value="<%=productDesc%>" />
				    <input type="hidden" name="ext1" value="<%=ext1%>" />
				    <input type="hidden" name="ext2" value="<%=ext2%>" />
				    <input type="hidden" name="payType" value="<%=payType%>" />
				    <input type="hidden" name="bankId" value="<%=bankId%>" />
				    <input type="hidden" name="redoFlag" value="<%=redoFlag%>" />
				    <input type="hidden" name="pid" value="<%=pid%>" />
                    <%--<input type="submit" value="确认支付" style="min-height: 1.8em;width:100%;text-align:center;"  />--%>
			    </form>
            </div>
            <div class="uinn-a6 t-red  ulev-app1">
                 确认你要支付的订单：                 
            </div>
            <div class="ub ub-ver uinn-a1 ub-f1">
                <div class="uba bc-border c-wh">
                    <ul class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                             订单编号
                        </li>
                        <li class="ub-f1 ulev-app1" id="spanSheetID">
                            <%=orderId%>
                        </li>
                    </ul>
                    <ul  class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                             手机号码
                        </li>
                        <li class="ub-f1 ulev-app1" id="spanPhone">
                            1111333
                        </li>
                    </ul>
                    <ul  class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                             商品名称
                        </li>
                        <li class="ub-f1 ulev-app1" id="spanProductName">
                             <%= productName%>                                              
                        </li>
                    </ul>
                    <ul  class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                             购买数量
                        </li>
                        <li class="ub-f1 ulev-app1">
                            <%= productNum%>
                        </li>
                    </ul>
                    <ul  class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                            单价
                        </li>
                        <li class="ub-f1 ulev-app1">
                            
                        </li>
                    </ul>
                    <ul  class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                            合计金额
                        </li>
                        <li class="ub-f1 ulev-app1">
                            <%=orderAmount%>
                        </li>
                    </ul>
                    <ul  class="ubb ub bc-border bc-text ub-ac uinn-a9">
                        <li class="uw-infor ut-s sc-text  umar-r-infor ulev-app1">
                             支付方式
                        </li>
                        <li class="ub-f1 ulev-app1" id="spanPayType">
                             银行卡                                                  
                        </li>
                    </ul> 
                </div>
            </div>
            <div class="ubt bc-border ub sc-bg uinn">
              
              
                    <div class="btn ub ub-ac bc-text-head ub-pc bc-btn uc-a1 ub-f1" style="min-height: 1.8em;"  id="btnSubmit" onclick="pay();">
                                                                立刻支付
                    </div>
               
            </div>    
        </div>
        <script>
            function pay() {
                document.forms['kqPay'].submit();
            }
        </script>
</body>

</html>