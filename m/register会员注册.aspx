<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="ETicket.Web.register" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>注册</title>
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link rel="stylesheet" type="text/css" href="/bootstrap/css/bootstrap.css">

    <!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css/bootstrap-ie6.css">
    <![endif]-->
    <!--[if lte IE 7]>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css/ie.css">
    <![endif]-->

    <style type="text/css">
       
        #logo
        {
            float: none;
            margin: 0;
            padding: 10px 0;
            position: relative;
        }
        #logo b 
        {
            background: url("/bootstrap/logo/head-info.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
            height: 75px;
            left: 110px;
            position: absolute;
            top: 13px;
            width: 350px;
        }
        .error{
            color:red;
        }
        td.title {
            vertical-align: middle;
           line-height:40px;
        }
    </style>

    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>

    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>
   
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->


</head>

<body style="background-color: #ededf0;">
    <%=ETicket.Web.HtmlController.Instance.TopBarHtml("/templ/topbar/other.html") %>
    <div class="container">
         <!--头部-->
         <div id="head">
              <div id="logo">
                <a href="/index.aspx">
                    <img src="/bootstrap/logo/head-logo.png" alt="阳朔票务网" height="75" width="100" />
                </a>
                <b></b>
            </div>
         </div>
         <!--内容-->
         <form id="regform" runat="server" method="post">
            <div class="row" style="background-color: #fff;padding-bottom:10px;">
                <div class="span12" style="height:400px;padding:100px 20px 0px 200px;">
                    <table>
                        <tr>
                            <td colspan="2">
                                <h2>注册成为阳朔票务网会员</h2>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">登姓名：</td>
                            <td>
                                <input type="text" id="username" name="username" />
                                <label class="error" for="username" style="margin: 0 0; display: inline; "></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">密码：</td>
                            <td>
                                <input type="password" id="txtPassword" name="txtPassword" />
                                <label class="error" for="txtPassword" style="margin: 0 0; display: inline; "></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">确认密码：</td>
                            <td>
                                <input type="password" id="txtPassword2" name="txtPassword2" />
                                <label class="error" for="txtPassword2" style="margin: 0 0; display: inline; "></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">手机号码： </td>
                            <td>
                                <input type="text" id="txtPhone" name="txtPhone" />
                                <label class="error" for="txtPhone" style="margin: 0 0; display: inline; "></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color:green">
                                请填写正确手机号码，忘记密码时可通过手机号码找回密码
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" ><button class="btn btn-primary" style="width:300px;" type="submit">提交注册</button></td>
                        </tr>
                    </table>
                </div>
            </div>
         </form>
         <!--尾部-->
         <div id="foot">
            <p style="text-align:center;">Copyright©2015-2020 <a href="http://www.yangshuo.cm">阳朔票务网</a> 版权所有 </p>
        </div>
    </div><!-- /container -->
  
   
    <script type="text/javascript" src="/bootstrap/js/bootstrap.js"></script>
    <!--[if lte IE 6]>
    <script type="text/javascript" src="/bootstrap/js/bootstrap-ie.js"></script>
    <![endif]-->
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                if ($.isFunction($.bootstrapIE6)) $.bootstrapIE6($(document));
            });
        })(jQuery);

        $(function () {

            jQuery.validator.addMethod("isPassWord", function (value, element) {
                value = $.trim(value);
                return this.optional(element) || /^\S{6,12}$/.test(value);
            }, "密码须为6-12位英文字母、数字和符号(不包括空格)");
            jQuery.validator.addMethod("isMobile", function (value, element) {
                var length = value.length;
                var mobile = /^(((13[0-9]{1})|(14[0-9]{1})|(15[0-9]{1})|(16[0-9]{1})|(17[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
                return this.optional(element) || (length == 11 && mobile.test(value));
            }, "请正确填写手机号码");

            //用户注册页字段验证
            $("#<%=regform.ClientID%>").validate({
                submitHandler: function (form) {
                    sAlert("正在提交注册数据..........请稍等");
                    form.submit();
                },
                ignore: "",
                rules: {
                    username: {
                        required: true,
                        remote: "/ajax/chk_username.ashx"
                    },
                    txtPassword: {
                        required: true,
                        isPassWord: true,
                        minlength: 6,
                        maxlength: 12
                    },
                    txtPassword2: {
                        required: true,
                        minlength: 6,
                        equalTo: "#txtPassword"
                    },
                    txtPhone: {
                        required: true,
                        isMobile: true
                    }
                },
                messages: {
                   
                    username: {
                        required: "请填写用户名",
                        remote:"该用户名已存在，无法注册。"
                    },
                    txtPassword: {
                        required: "请填写密码",
                        minlength: "密码长度在6-12个字符之间",
                        maxlength: "密码长度在6-12个字符之间"
                    },
                    txtPassword2: {
                        required: "请填写确认密码",
                        minlength: "密码长度在6-12个字符之间",
                        equalTo: "您输入的密码不一致"
                    },
                    txtPhone: {
                        required: "请输入手机号码"
                    }
                }
            });//验证

        });

      
       
       
      
       
    </script>

</body>
</html>
