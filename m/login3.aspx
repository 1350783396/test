<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ETicket.Web.login" %>


<style type="text/css">
body { _behavior: url(/images/csshover.htc);
}
</style>
<link href="/images/base.css" rel="stylesheet" type="text/css">
<link href="/images/home_header.css" rel="stylesheet" type="text/css">
<link href="/images/member_login.css" rel="stylesheet" type="text/css">
<link href="/images/font-awesome.min.css" rel="stylesheet" /> 

<!--[if IE 7]>
  <link rel="stylesheet" href="/images/font-awesome-ie7.min.css">
<![endif]-->
<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->



    <script type="text/javascript">
        function loadimage() {
            document.getElementById("randImage").src = "/chk_code.aspx?" + Math.random();
        }
        function check() {
            var txtUserName = $("#txtUserName").val();
            if (txtUserName == "") {
                alert('�������û���');
                return false;
            }

            var txtPassword = $("#txtPassword").val();
            if (txtPassword == "") {
                alert('����������');
                return false;
            }

            var txtValidCode = $("#txtValidCode").val();
            if (txtValidCode == "") {
                alert('��������֤��');
                return false;
            }

         
            sAlert("���ں˶��û�������........���Ե�");
            return true;
            
        }
    </script>
</head>
<body>

  <div style="background-color: #017ddf; display:none;">
        <div class="w" style="width:890px;font-size:14px;">
			 <%=ETicket.Web.HtmlController.Instance.TopBarHtml("/templ/topbar/index.html","color:#4c4c4c") %>
        </div>
</div>

  <!-- PublicHeadLayout End -->
  <div class="nc-login-layout">
  <div class="nc-login">
    <div class="nc-login-mode">
      <ul class="tabs-nav">
        <li><a href="#default">�����̵�¼<i></i></a></li>
      </ul>
      <div id="tabs_container" class="tabs-container">
        <div id="default" class="tabs-content">
        
          <form id="loginform" method="post"  runat="server" class="nc-login-form">
            <input type='hidden' name='formhash' value='iquUYrplLbT6lOnQCIlng0OiKuRY7BP' />            <input type="hidden" name="form_submit" value="ok" />
            <input name="nchash" type="hidden" value="7aa29d66" />
            <dl>
              <dt>��&nbsp;&nbsp;&nbsp;�ţ�</dt>
              <dd>
              
              <input id="txtUserName" name="txtUserName" class="text" tabindex="1" type="text" runat="server"/>
              </dd>
            </dl>
            <dl>
              <dt>��&nbsp;&nbsp;&nbsp;�룺</dt>
              <dd>

                <input id="txtPassword" name="txtPassword" class="text" tabindex="2" type="password" runat="server"/>
               
               <input name="loginpwd" id="loginpwd" value="" class="hide highlight2" type="hidden" />
                
                
              </dd>
            </dl>
            <div class="code-div mt15">
              <dl>
                <dt>��֤�룺</dt>
                <dd>
         <input id="txtValidCode" name="txtValidCode"  class="text w100" tabindex="6" style="ime-mode:disabled" type="text" runat="server"/>         
                </dd>
              </dl>
              <span><img src="chk_code.aspx" id="randImage" onClick="loadimage();"> <a class="makecode" href="javascript:void(0)" onClick="loadimage();">�����壬��һ��</a></span></div>
                        <div class="handle-div">
            <span class="auto"><input type="checkbox" class="checkbox" name="auto_login" value="1">�����Զ���¼<em style="display: none;">�����ڹ��õ�����ʹ��</em></span>
            <a class="forget" href="/safe/chgLoginPwd_byPhone.aspx">�������룿</a></div>
            <div class="submit-div">
  

<input  class="submit" id="btnLogin" value="��&nbsp;&nbsp;&nbsp;¼" tabindex="8" type="submit" runat="server" onClick="return check();"/>
      
      
            </div>
          </form>
        </div>
      </div>
    </div>


  </div>
  <div class="clear"></div>
</div>


 
					<link href="/images/chat.css" rel="stylesheet" type="text/css">
					<div style="clear: both;"></div>
					<div id="web_chat_dialog" style="display: none;float:right;">
					</div>
					
                    
                    
                    <!-- ���벿��2 -->
<script type="text/javascript">
try {
var urlhash = window.location.hash;
if (!urlhash.match("fromapp"))
{
if ((navigator.userAgent.match(/(iPhone|iPod|Android|ios|iPad)/i)))
{
window.location="https://m.yugetour.cn/login.aspx"; //�������ַ���Ϊ���ֻ�վ����ַ
}
}
}
catch(err)
{
}
</script>
<!-- ���벿��2end -->

</body>
</html>