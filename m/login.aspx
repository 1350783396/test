<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ETicket.Web.login" %>

<!DOCTYPE html>
<title>智慧游</title>

<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <style type="text/css">
<!--
.STYLE3 {color: #333333}
-->
    </style>
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
        <link rel="stylesheet" href="/login_content/css/main.css">

    <script type="text/javascript">
        function loadimage() {
            document.getElementById("randImage").src = "/chk_code.aspx?" + Math.random();
        }
        function check() {
            var txtUserName = $("#txtUserName").val();
            if (txtUserName == "") {
                alert('请输入用户名');
                return false;
            }

            var txtPassword = $("#txtPassword").val();
            if (txtPassword == "") {
                alert('请输入密码');
                return false;
            }
    
            sAlert("正在核对用户名密码........请稍等");
            return true;
            
        }
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312"></head>
	

    <body class="um-vp bc-bg" ontouchstart>
	            <div id="header" class="uh bc-text-head ub bc-head">
                <div class="nav-btn" id="nav-left">
                    <div class="fa fa-angle-left fa-2x" onClick="window.open('/');"></div>
                </div>
                <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">登录</h1>
                <div class="nav-btn nav-bt" id="nav-right">
                    <div class="ub-img icon-home umw2 umh4" onClick="window.open('/');"></div>
                </div>
            </div>
		<div id="append_parent"></div>
<div id="ajaxwaitid"></div>
	<p>&nbsp;</p>
	<p>&nbsp;</p>
	<p>&nbsp;</p>
	<p>&nbsp;</p>
	<div class="ub ub-ver uinn-a1">
            <div class="ub ub-ver">
              <form id="loginform" method="post"  runat="server" class="nc-login-form">
            <input type='hidden' name='formhash' value='iquUYrplLbT6lOnQCIlng0OiKuRY7BP' />            <input type="hidden" name="form_submit" value="ok" />
            <input name="nchash" type="hidden" value="7aa29d66" />
                    <div class="umar-a uba bc-border c-wh">
                        <div class="ub ub-ac ubb umh5 bc-border ">
                            <div class=" uinput ub ub-f1">
                                <div class="uinn fa fa-user sc-text">
								</div>
								  <input placeholder="用户名"  id="txtUserName" name="txtUserName" tabindex="1" type="text" runat="server" class="ub-f1"/>
                            </div>
                        </div>
                        <div class="ub ub-ac ubb umh5 bc-border ">
                            <div class=" uinput ub ub-f1">
                                <div class="uinn fa fa-lock sc-text"></div>
			  
								   <input placeholder="密码"  id="txtPassword" name="txtPassword" class="umw4 ub-f1" tabindex="2" type="password" runat="server"/>
               
               <input name="loginpwd" id="loginpwd" value="" class="hide highlight2" type="hidden" />
                            </div>
                        </div>
						
					        <div class="ub ub-ac ubb umh5 bc-border ">
                            <div class=" uinput ub ub-f1">
                                <div class="uinn fa fa-user sc-text"></div>
								
								 <input  placeholder="验证码"  id="txtValidCode" name="txtValidCode"  class="ub-f1" tabindex="6" style="ime-mode:disabled" type="text" runat="server"/> <img src="chk_code.aspx" name="randImage" width="80" height="30" id="randImage" onClick="loadimage();">                            </div>
                        </div>	
                    </div>

             


 <div class="ub " id="listview">
                    <ul class="ub">
                        <li class=" ub  bc-text ub-ac lis" data-index="0">
                            <div class="checkbox umar-r">
                          <input type="checkbox" class="checkbox" name="auto_login" value="1">
				
                            </div>
                             <div class="lv_title ub-f1 marg-l ub ub-ver ut-m line1">
                          七天自动登录
                            </div>  
                        </li>

                        <!--
                        <li class=" ub  bc-text ub-ac lis" data-index="1">
                            <div class="checkbox umar-r">
                                <input type="checkbox" class="uabs ub-con" id="chkAuto" checked="true"/>
                            </div>
                            <div class="lv_title ub-f1 marg-l ub ub-ver ut-m line1">
                                                                                                ??????
                            </div>
                        </li>-->
                      <li class=" ub  bc-text ub-ac lis">
                            <div class="sc-text">
                            <span class="STYLE3">没有账号? </span> <a>联系14796019601</a>  <!-- <a href="/register.aspx">立即注册</a>    -->   
                     </div>
                      </li>
                    </ul> 
                </div>
                <div class="ub ">
<input  class="btn ub ub-ac bc-text-head ub-pc bc-btn uc-a1 ub-f1" id="btnLogin" value=" 登录" tabindex="8" type="submit" runat="server" onClick="return check();"/>

              </form>        
      </div>
       
	</div>

					<link href="/images/chat.css" rel="stylesheet" type="text/css">
					<div style="clear: both;"></div>
					<div id="web_chat_dialog" style="display: none;float:right;">
			</div>
					
    </body>
 <!--<body background="/images/login_bg.jpg"></body>   -->
</html>

