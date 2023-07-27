<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" src="js/jquery.js"></script>
<script type="text/javascript">
$(function(){	
	//顶部导航切换
	$(".nav li a").click(function(){
		$(".nav li a.selected").removeClass("selected")
		$(this).addClass("selected");
	})	
})	
</script>


</head>

<body style="background:url(images/topbg.gif) repeat-x;">

    <div class="topleft">
    <a href="partner.aspx" target="_parent"><img src="images/logo.png" title="系统首页" /></a>
    </div>
        
    <ul class="nav">
    <li></li>
    </ul>
            
    <div class="topright">    
    <ul>
    <li><a href="/logout.aspx" target="_parent"><img src="images/005.gif" width="32" height="32" title="退出" />退出</a></li>
    </ul>
     
    <div class="user">
    <span>欢迎您 ,<%=ETicket.BLL.UserBLL.Instance.GetLoginModel().UserName %></span>
    <i>会员等级</i>
    <b>5</b>
    </div>    
    
    </div>

</body>
</html>
