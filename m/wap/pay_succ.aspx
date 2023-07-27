<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_succ.aspx.cs" Inherits="ETicket.Web.wap.pay_succ" %>


<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title>支付成功</title>
        <meta charset="utf-8">
        <meta name="viewport" content=" width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
        <link rel="stylesheet" href="/wap/cssapp/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-box.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-base.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-color.css">
        <link rel="stylesheet" href="/wap/cssapp/appcan.icon.css">
        <link rel="stylesheet" href="/wap/cssapp/appcan.control.css">
        <link rel="stylesheet" href="/wap/cssapp/pay_main.css">
    </head>
    <body class="um-vp bc-bg" >
        <div class="ub ub-ver">
            <div class="uinn-a6 t-red  ulev-app1" style="font-size:16px; text-align:center; padding-top:20%;">
               	<span style="color:green; font-weight:bold;">恭喜，已支付成功！</span>
                <br />
                <br />
                <span style="color:#000;">短信稍后将发送到你填写的手机号码上，请注意查看</span>          
                <br />
                <br />
                <br />
                <br />
                <img src="qrcode_for_shj.jpg" id="qrImg" style="width: 280px; height:280px;"/>  
                <br />
                <span style="color:#000;">长按住二维码，关注公众号</span>        
            </div>
           
            
        </div>

      
    </body>
    
</html>