<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="ETicket.Web.register" %>
<title>智慧游</title>
<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title></title>
        <meta charset="utf-8">
        <meta name="viewport" content="target-densitydpi=device-dpi, width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
        <link rel="stylesheet" href="/css/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="/css/ui-box.css">
        <link rel="stylesheet" href="/css/ui-base.css">
        <link rel="stylesheet" href="/css/ui-color.css">
        <link rel="stylesheet" href="/css/appcan.icon.css">
        <link rel="stylesheet" href="/css/appcan.control.css">
    </head>
    <body class="um-vp " ontouchstart>

            <!--header开始-->
            <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="window.open('/');"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">会员注册</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-refresh umw2 umh4" onClick="window.open('#');"></div>
                </div>
            </div>
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>  
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->


</head>
<div class="container">
         <!--头部-->
         <!--内容-->
<form id="regform" runat="server" method="post">
            <div class="row" style="background-color: #fff;padding-bottom:10px;">
                <div class="span12" style="height:40px;padding:10px 20px 0px 20px;">
                  <table>
                        <tr>
                            <td colspan="2">
                          <h2 align="center">注册成为会员</h2>                          </td>
                        </tr>
                        <tr>
                            <td class="title"><div align="left">登姓名：</div></td>
                            <td>
                                <input type="text" id="username" name="username" />
                                <label class="error" for="username" style="margin: 0 0; display: inline; "></label>                            </td>
                        </tr>
                        <tr>
                            <td class="title"><div align="left">密码：</div></td>
                            <td>
                                <input type="password" id="txtPassword" name="txtPassword" />
                                <label class="error" for="txtPassword" style="margin: 0 0; display: inline; "></label>                            </td>
                        </tr>
                        <tr>
                            <td class="title"><div align="left">确认密码：</div></td>
                            <td>
                                <input type="password" id="txtPassword2" name="txtPassword2" />
                                <label class="error" for="txtPassword2" style="margin: 0 0; display: inline; "></label>                            </td>
                        </tr>
                        <tr>
                            <td class="title"><div align="left">手机号码： </div></td>
                            <td>
                                <input type="text" id="txtPhone" name="txtPhone" />
                                <label class="error" for="txtPhone" style="margin: 0 0; display: inline; "></label>                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color:green">
                           <!--  <div align="left">请填写正确手机号码，忘记密码时可通过手机号码找回密码
                                  <br />                              
                                  <br />                            
                              </div>-->
							  </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
                              <div align="left">
                                <button class="btn btn-primary" style="width:300px;" type="submit">提交注册</button>
                            </div></td>
                        </tr>
                    </table>
              </div>
            </div>
  </form>
         <!--尾部-->
</div>
<!-- /container -->
  
   
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
