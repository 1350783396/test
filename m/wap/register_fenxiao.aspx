<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register_fenxiao.aspx.cs" Inherits="ETicket.Web.wap.register_fenxiao" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <title>注册-分销商</title>

    <link href="/wap/css/cp_style_v1.0.css" rel="stylesheet" type="text/css" />
    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
</head>
<body>

    <!--header-->
    <header class="index">
        <div class="c-hd">
            <section class="back">
              
            </section>
            <section class="hd-title">
                注册分销商账号
            </section>
            <section class="hd-nav"></section>
        </div>
    </header>
    <!--content-->
    <div class="content">
  
        <div class="entry" style="display:none;"></div>
        <!--article-->
        <div class="login-box">
          
                <div class="codebox">
                    <p>
                        <input id="username" type="text" name="username" class="zhaoinput" placeholder="请输入您的登录名" value="" onblur="chkuser()">
                    </p>
                    <p>
                        <input id="phone" type="text" name="phone" class="zhaoinput" placeholder="请输入您的手机号" value="">
                    </p>
                    <p><input id="pwd" type="password" name="pwd" value="" class="zhaoinput" autocomplete="off" placeholder="请设置六位及以上密码"></p>
                    <p><input id="pwd2" type="password" name="pwd2" value="" class="zhaoinput" autocomplete="off" placeholder="请再输入密码确认"></p>
                </div>
                <div class="deal-btn">
                    <input type="button" class="j_submit" name="doregister" value="注 册" onclick="register()">
                </div>
        
        </div>
    </div>
  
    <footer class="footer">
        <section class="footer-t">
            
        </section>
        <section class="footer-sort">
            <ul class="com-footitem">
                <li><a href="http://w.zhihuiyou.cn/">网站登陆</a></li>
                <li><a href="http://w.zhihuiyou.cn/app_qr_down.html">客户端APP下载</a></li>
            </ul>
        </section>
        <section class="footer-service">客服（<b><a href="tel:18278384444">18278384444</a></b>　8:00-18:00）</section>
        <section class="footer-copyright"><span>&copy; 2023 阳朔智慧游</span></section>
    </footer>
    <script type="text/javascript">
        function chkuser()
        {
            var username = $("#username").val();
            if (username == "")
            {
                alert("请输入登录名");
                return;
            }

            var url = "/ajax/register_fenxiao.ashx?_action=ChkParter&username=" + username;

            $.ajax({
                url: url,
                type: 'get',
                cache: false,
                dataType: "json",
                success: function (re) {
                   if (re.isOk=="False") {
                       alert(re.msg);
                   }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    
                }
            });

        }

        function validatemobile(mobile) {

            if (mobile.length == 0) {
                return false;
            }

            if (mobile.length != 11) {
                return false;
            }

            var myreg = /^(((13[0-9]{1})|(14[0-9]{1})|(15[0-9]{1})|(16[0-9]{1})|(17[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
            if (!myreg.test(mobile)) {
                return false;
            }

            return true;
        }
        function register()
        {


            var user = $("#username").val();
            if (user == "") {
                alert("请输入登录名");
                return;
            }
            var p = $("#phone").val();
            if (p == "") {
                alert("请输入手机号码");
                return;
            }
            var ismoblie = validatemobile(p);
            if (!ismoblie)
            {
                alert('请输入有效的手机号码！');
                return;
            }
            var pwd1 = $("#pwd").val();
            var pwd2 = $("#pwd2").val();
            if (pwd1 == "") {
                alert("请输入密码");
                return;
            }
            if (pwd1.length < 6)
            {
                alert("请输入6位以上密码");
                return;
            }
            if(pwd1!=pwd2){
                alert("两次输入密码不一致");
                return;
            }
            var arg = { username: user, password: pwd1, phone: p,userlevel:"阳朔分销商" };

            var url = "/ajax/register_fenxiao.ashx?_action=RegParter";

            if (window.onSaveing)
                return;
            window.onSaveing = true;

            try{

                $.ajax({
                    url: url,
                    type: 'post',
                    data: arg,
                    dataType: "json",
                    cache: false,
                    success: function (re) {

                        if (re.isOk=="False") {
                            alert(re.msg);
                        }
                        else {
                            //alert("注册成功！")
                            window.location.href = "register_fenxiao_succ.aspx";
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert("注册失败！")
                    }
                });

            }
            finally{
                window.onSaveing = false;
            }


        }
    </script>
</body>
</html>

