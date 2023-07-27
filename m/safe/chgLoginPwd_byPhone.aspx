<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chgLoginPwd_byPhone.aspx.cs" Inherits="ETicket.Web.safe.chgLoginPwd_byPhone" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>重置密码</title>
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

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/js/common.js"></script>
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
            background: url("/bootstrap/logo/head-info-2.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
            height: 75px;
            left: 110px;
            position: absolute;
            top: 13px;
            width: 350px;
        }
        #foot
        {
            margin-top:20px;
        }
        .SuccInfo {
            background: url("bootstrap/img/wcdd.gif") no-repeat scroll left 10px rgba(0, 0, 0, 0);
            font-size: 26px;
            font-weight: bold;
            line-height: 2em;
            padding: 10px 10px 10px 50px;
        }
        .ErrorInfo {
            background: url("bootstrap/img/no.gif") no-repeat scroll left 10px rgba(0, 0, 0, 0);
            font-size: 26px;
            font-weight: bold;
            line-height: 2em;
            padding: 10px 10px 10px 50px;
        }
    </style>

   <script type="text/javascript">
    $(function () {
       

        jQuery.validator.addMethod("isPassWord", function (value, element) {
            value = $.trim(value);
            return this.optional(element) || /^\S{6,12}$/.test(value);
        }, "密码须为6-12位英文字母、数字和符号(不包括空格)");

        $("#findPwd").validate({
            submitHandler: function (form) {
                form.submit();
            },
            
            rules: {
                username: {
                    required: true,
                    remote: "/ajax/chk_username.ashx"
                },
                newpwd: {
                    required: true,
                    isPassWord: true,
                    minlength: 6,
                    maxlength: 12
                },
                newpwd2: {
                    required: true,
                    minlength: 6,
                    equalTo: "#newpwd"
                },
                code: { required: true }
            },
            messages: {
                username: {
                    required: "请输入账号",
                    remote: "你输入的账号不存在"
                },
                newpwd: {
                    required: "请输入密码",
                    minlength: "密码长度在6-12个字符之间",
                    maxlength: "密码长度在6-12个字符之间"
                },
                newpwd2: {
                    required: "请输入密码",
                    minlength: "密码长度在6-12个字符之间",
                    equalTo: "您输入的密码不一致"
                },
                code: "请输入手机验证码"
            }
        });
    });
   </script>
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
                    <img src="/bootstrap/logo/head-logo.png" alt="心客网" height="75" width="100" />
                </a>
                <b></b>
            </div>
         </div>
        <div class="row">
            <div class="span12" style="background-color: #fff;">
                
                <form method="post" id="findPwd" runat="server">
                    <table width="640" border="0" style="margin: 0 auto">
                        <tr>
                            <td colspan="2"><h2>重置密码-通过手机号码</h2></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding:5px 50px;">系统将会发送验证码到该账号下设置的手机号码</td>
                        </tr>
                        <tr>
                            <td width="150">
                                <div align="right">账号： </div>
                            </td>
                            <td>
                                <input id="username"  name="username"/>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="f_gray">
                                <label class="error" for="username" style="display: none"></label></td>
                        </tr>
                        <tr>
                            <td>
                                <div align="right">手机验证码： </div>
                            </td>
                            <td>
                                <input style="width: 115px; height: 22px" type="text" name="code" id="code">
                                <input type="button" style="background: url(/bootstrap/img/smail.jpg) no-repeat scroll 0 0 transparent; border: 0 none; cursor: pointer; height: 30px; width: 150px" id="sendCodeBtn" onclick="sendResetPassCode('username', 'sendCodeBtn');" value="获取短信验证码"/>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="f_gray">
                                <label class="error" for="code" style="display: none"></label></td>
                        </tr>
                        <tr>
                            <td align="right">
                                新密码：
                            </td>
                            <td>
                                <input style="width: 150px; height: 22px" type="password" name="newpwd" id="newpwd">
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="f_gray">
                                <label class="error" for="newpwd" style="display: none"></label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                确认新密码：
                            </td>
                            <td>
                                <input style="width: 150px; height: 22px" type="password" name="newpwd2" id="newpwd2">
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="f_gray">
                                <label class="error" for="newpwd2" style="display: none"></label>
                            </td>
                        </tr>
                        <tr>
                            <td height="5" colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td height="50">&nbsp;</td>
                            <td>
                                <input type="submit" id="submit" name="submit" value="重置密码" class="btn btn-primary" style="width:300px;" />
                            </td>
                        </tr>
                    </table>
                </form>
            </div>


        </div>

        <div id="foot">
            <p style="text-align:center;">Copyright©2015-2020 <a href="http://www.yangshuo.cm">阳朔电子票务</a> All Rights Reserved .Power by 阳朔电子票务 </p>
        </div>
    </div>





</body>
</html>
