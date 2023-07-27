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
    <li><a href="art/view_list.aspx" target="rightFrame" class="selected"><img src="images/tz.gif" width="45" height="45" title="通知" />
      <h2>通知</h2></a></li>
    <li><a href="partner/order_print_list.aspx" target="rightFrame"><img src="images/i01.png" width="45" height="45" title="小票打印" />
      <h2>小票打印</h2></a></li>
    <li><a href="partner/order_sale_report.aspx"  target="rightFrame"><img src="images/icon04.png" title="报表打印" /><h2>报表打印</h2></a></li>
    <li><a href="partner/order_sms_list.aspx" target="rightFrame"><img src="images/icon05.png" title="短信管理" /><h2>短信管理</h2></a></li>
    <li><a href="partner/account_query_list.aspx"  target="rightFrame"><img src="images/yue.png" title="积分余额" /><h2>积分余额</h2></a></li>
    <li><a href="user_chgpass.aspx"  target="rightFrame"><img src="images/icon03.png" title="密码修改" /><h2>密码修改</h2></a></li>
    <li><a href="partner/partner_edit.aspx"  target="rightFrame"><img src="images/003.gif" title="资料设置" /><h2>资料修改</h2></a></li>
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
