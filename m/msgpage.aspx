<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="msgpage.aspx.cs" Inherits="ETicket.Web.msgpage" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>支付成功</title>
    <meta content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=0" name="viewport">
    <meta content="yes" name="apple-mobile-web-app-capable">
    <meta content="black" name="apple-mobile-web-app-status-bar-style">
    <meta content="telephone=no" name="format-detection">
    <link rel="stylesheet" type="text/css" href="static/css/style.css">
    <style type="text/css">
<!--
.STYLE1 {
	font-size: 24;
	color: #333333;
}
.STYLE2 {
	font-size: 24px;
	font-weight: bold;
}
.STYLE3 {
	font-size: 24;
	color: #666666;
}
.STYLE4 {font-size: 18px}
-->
    </style>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head>
<body>

<section class="aui-flexView">
    <header class="aui-navBar aui-navBar-fixed">
     <!--   <div class="aui-center"> <span class="aui-center-title STYLE4">支付成功</span> </div>  -->
    </header>
    <section class="aui-scrollView">
        <div class="aui-pay-box">
            <div class="aui-pay-text">
            </div>
            <div class="aui-pay-fill">
                <div class="aui-pay-flex">
                  <div class="aui-flex b-line">
                        <div class="aui-flex-box">
                            <h2 class="aui-pay-titleS">支付成功</h2>
                        </div>
                    <span class="aui-arrow"><a href="/dingdan/index.aspx">查看详情</a></span>                    </div>
                    <div class="aui-pay-info">
                        <p class="aui-flex">支付方式 <em class="aui-flex-box">账户积分</em></p>
                        <p class="aui-flex">当前状态 <em class="aui-flex-box">已支付 </em></p>
                    </div>
                </div>

                <div class="aui-pay-flex">
                  <div class="aui-flex">
                       <!--  <h2 class="aui-pay-title">支付奖励</h2> -->
                  </div>
					              <span class="row STYLE1 STYLE2" style="background-color: #fff;height:400px;text-align:center;vertical-align:middle;"><span class="row STYLE3" style="height:400px;text-align:center;vertical-align:middle;"><%=msg %></span></span>
                    <div class="aui-palace">
                        <a href="javascript:;" class="aui-palace-grid">
                            <img src="static/picture/red.png" alt="">
                        </a>
                        <a href="javascript:;" class="aui-palace-grid">
                            <img src="static/picture/red.png" alt="">
                        </a>
                        <a href="javascript:;" class="aui-palace-grid">
                            <img src="static/picture/red.png" alt="">
                        </a>
                    </div>
                </div>


            </div>



            <div class="aui-pay-com">
                <button>
               <a href="/business/partner.aspx">完成</a>
                    </button>
            </div>
            </div>

     

    </section>

</section>
</body>
</html>
