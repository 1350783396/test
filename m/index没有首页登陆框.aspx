<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��˷����Ʊ��,��˷������Ʊ����,��˷����,��˷һ����-��˷�ǻ�����, ��˷ www.yangshuo.cm</title>
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="keywords" content="��˷������Ʊ,����Ȫ,ͼ�ڹŵ�,������,��ˮ��,�콭Ư��,�콭��Ư��,������,������ר��,������Ư��,��˷,��˷����,��˷һ���Σ���˷������Ʊ��������˷����ר�߳���ӡ��������,������,��˷����Ʊ��,����̶-��ʯ��,��˷��,��˷�ǻ��� www.yangshuo.cm ��˷������Ʊ����0773-2300400" />
    <meta name="description" content="��˷����Ʊ��,��˷������Ʊ����˷������ѯ����"/>
<link href="style/css/login.css" rel="stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="/bootstrap/login/login.css" />
<script type="text/javascript" src="style/js/jQuery-1.7.1.min.js"></script> 
<script type="text/javascript" src="style/js/lhgdialog.min.js"></script>
<script type="text/javascript" src="style/js/banner.js"></script>
<!--[if lte IE 6]>
<script src="style/js/ie6.js" type="text/javascript"></script>
<script type="text/javascript">
	DD_belatedPNG.fix('*');
</script>
<![endif]-->
<script type="text/JavaScript">
<!--
function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function YY_checkform() { //v4.65
//copyright (c)1998,2002 Yaromat.com
//�˲���ɱ�������www.youqian.com
  var args = YY_checkform.arguments; var myDot=true; var myV=''; var myErr='';var addErr=false;var myReq;
  for (var i=1; i<args.length;i=i+4){
    if (args[i+1].charAt(0)=='#'){myReq=true; args[i+1]=args[i+1].substring(1);}else{myReq=false}
    var myObj = MM_findObj(args[i].replace(/\[\d+\]/ig,""));
    myV=myObj.value;
    if (myObj.type=='text'||myObj.type=='password'||myObj.type=='hidden'){
      if (myReq&&myObj.value.length==0){addErr=true}
      if ((myV.length>0)&&(args[i+2]==1)){ //fromto
        var myMa=args[i+1].split('_');if(isNaN(parseInt(myV))||myV<myMa[0]/1||myV > myMa[1]/1){addErr=true}
      } else if ((myV.length>0)&&(args[i+2]==2)){
          var rx=new RegExp("^[\\w\.=-]+@[\\w\\.-]+\\.[a-z]{2,4}$");if(!rx.test(myV))addErr=true;
      } else if ((myV.length>0)&&(args[i+2]==3)){ // date
        var myMa=args[i+1].split("#"); var myAt=myV.match(myMa[0]);
        if(myAt){
          var myD=(myAt[myMa[1]])?myAt[myMa[1]]:1; var myM=myAt[myMa[2]]-1; var myY=myAt[myMa[3]];
          var myDate=new Date(myY,myM,myD);
          if(myDate.getFullYear()!=myY||myDate.getDate()!=myD||myDate.getMonth()!=myM){addErr=true};
        }else{addErr=true}
      } else if ((myV.length>0)&&(args[i+2]==4)){ // time
        var myMa=args[i+1].split("#"); var myAt=myV.match(myMa[0]);if(!myAt){addErr=true}
      } else if (myV.length>0&&args[i+2]==5){ // check this 2
            var myObj1 = MM_findObj(args[i+1].replace(/\[\d+\]/ig,""));
            if(myObj1.length)myObj1=myObj1[args[i+1].replace(/(.*\[)|(\].*)/ig,"")];
            if(!myObj1.checked){addErr=true}
      } else if (myV.length>0&&args[i+2]==6){ // the same
            var myObj1 = MM_findObj(args[i+1]);
            if(myV!=myObj1.value){addErr=true}
      }
    } else
    if (!myObj.type&&myObj.length>0&&myObj[0].type=='radio'){
          var myTest = args[i].match(/(.*)\[(\d+)\].*/i);
          var myObj1=(myObj.length>1)?myObj[myTest[2]]:myObj;
      if (args[i+2]==1&&myObj1&&myObj1.checked&&MM_findObj(args[i+1]).value.length/1==0){addErr=true}
      if (args[i+2]==2){
        var myDot=false;
        for(var j=0;j<myObj.length;j++){myDot=myDot||myObj[j].checked}
        if(!myDot){myErr+='* ' +args[i+3]+'\n'}
      }
    } else if (myObj.type=='checkbox'){
      if(args[i+2]==1&&myObj.checked==false){addErr=true}
      if(args[i+2]==2&&myObj.checked&&MM_findObj(args[i+1]).value.length/1==0){addErr=true}
    } else if (myObj.type=='select-one'||myObj.type=='select-multiple'){
      if(args[i+2]==1&&myObj.selectedIndex/1==0){addErr=true}
    }else if (myObj.type=='textarea'){
      if(myV.length<args[i+1]){addErr=true}
    }
    if (addErr){myErr+='* '+args[i+3]+'\n'; addErr=false}
  }
  if (myErr!=''){alert('����֤����,��������:\t\t\t\t\t\n\n'+myErr)}
  document.MM_returnValue = (myErr=='');
}
//-->
</script>

<script type="text/javascript">
        function loadimage() {
            document.getElementById("randImage").src = "chk_code.aspx?" + Math.random();
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
<title>��˷����Ʊ��</title>
</head>
<body>
<div class="header">
  <div class="w980">
    <div class="fl logo">
      <table width="550" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td height="61"><a href="/index.aspx" target="_self"><img src="style/images/logo.png" alt="��˷����Ʊ��" height="61" border="0" /></a></td>
          <td align="center"><table width="98%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td height="30" align="left" valign="bottom">&nbsp;<span style="color:#999"> �������ߣ�</span></td>
            </tr>
            <tr>
              <td height="25" align="left" valign="bottom">&nbsp; <span style="color:#999">0773-2300400</span></td>
            </tr>
          </table></td>
        </tr>
      </table>
    <a href="#"></a></div>
    <div class="fr menu fw">
      <ul>
        <li class="hover"><a href="#">��ҳ</a></li>
        <li><a href="app.aspx">APP</a></li>
        <li><a href="login.aspx">��½</a></li>
     </ul>
    </div>
  </div>
</div>
<div class="login w980">

</div>
<div class="banner">
  <div id="banner_js" class="banner_js">
    <ul>
<li style="display:block; background: url(style/images/img/login_banner_c.jpg) 50% 0% no-repeat;"><a href="#" title="��˷����Ʊ��"  target="_blank"></a></li>
<li style="display:block; background: url(style/images/img/20150208201836W020140811351158849714_E589AFE69CAC.jpg) 50% 0% no-repeat;"><a href="#" title="��˷����Ʊ��"  target="_blank"></a></li>
<li style="display:block; background: url(style/images/img/20150208203236W020140820615106403316_E589AFE69CAC.jpg) 50% 0% no-repeat;"><a href="#" title="��˷����Ʊ��"  target="_blank"></a></li>
    </ul>
    <div id="jsNav" class="jsNav">
          <a class='trigger imgSelected'  href="javascript:void(0)"></a>      <a class='trigger'  href="javascript:void(0)"></a>      <a class='trigger'  href="javascript:void(0)"></a>    </div>
  </div>
</div>
 
<div class="cl"></div>
<div class="main">
  <div class="w980">
    <div class="mian_dl">
      <dl>
        <dt><img src="style/images/in_19.png" /></dt>
        <dd><span>&nbsp; �����̡����¶�����</span>
      
          <div id="demoF" style="overflow:hidden;height:158px;width:320px; margin-top:15px; clear:both;">
            <div id="demoF1">
                            <ul style="line-height: 35px; font-size: 12px; width: 100%; float: left; " class="unstyled">
                               <%=ETicket.Web.HtmlController.Instance.ListNewOrder(100,"/templ/index/new_order.html") %>
                            </ul>
            </div>
            <div id="demoF2"></div>
          </div>
        <script>
   var speed=100
   demoF2.innerHTML=demoF1.innerHTML
   function MarqueeF(){
   if(demoF2.offsetTop-demo.scrollTop<=0)
   demoF.scrollTop-=demoF1.offsetHeight
   else{
   demoF.scrollTop++
   }
   }
   var MyMarF=setInterval(MarqueeF,speed)
   demoF.onmouseover=function() {clearInterval(MyMarF)}
   demoF.onmouseout=function() {MyMarF=setInterval(MarqueeF,speed)}
            </script>
                <!--��������-->
        </dd>
      </dl>
      <dl>
        <dt><img src="style/images/in_22.png" /></dt>
        <dd><span>&nbsp;�����̡��������ʡ�</span>
   
          <div id="demoG" style="overflow:hidden;height:158px;width:320px;margin-top:15px; clear:both;">
            <div id="demoG1">
                            <ul style="line-height: 35px; font-size: 12px; width: 100%; float: left; " class="unstyled">
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷�����������</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷��������������</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷ӡ�����β�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷�׾�������</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷��������</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷��֮��</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷��Ӫ���β�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷������</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷�����������β�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷��ͥ�Ƶ����β�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷��ʢ������</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷�ļ����β�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷�𸣾Ƶ����β�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷��������</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷����������</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷�������β�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷����������</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷��˹�����վƵ�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷�����Ƶ����β�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷�������β�</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷��ң��</li>
                                <li style="margin-left:6px; font-size:13px; line-height:22px; color:#666"> ��˷�������β�</li>

                            </ul>
            </div>
            <div id="demoG2"></div>
          </div>
                 <script>
   var speed=100
   demoG2.innerHTML=demoG1.innerHTML
   function MarqueeG(){
   if(demoG2.offsetTop-demo.scrollTop<=0)
   demoG.scrollTop-=demoG1.offsetHeight
   else{
   demoG.scrollTop++
   }
   }
   var MyMarG=setInterval(MarqueeG,speed)
   demoG.onmouseover=function() {clearInterval(MyMarG)}
   demoG.onmouseout=function() {MyMarG=setInterval(MarqueeG,speed)}
            </script>
                <!--��������-->
        </dd>
      </dl>
      <dl>
        <dt><img src="style/images/in_24.png" /></dt>
        <dd><span>&nbsp;���桾֪ͨ���桿</span>     
          <div id="demo" style="overflow:hidden;height:158px;width:320px; margin-top:15px; clear:both;">
            <div id="demo">
                            <ul style="line-height: 35px; font-size: 12px; width: 100%; float: left; " class="unstyled">
							<marquee onmouseover=this.stop() style="FONT-SIZE: 9pt; WIDTH: 158px; COLOR: #000000; HEIGHT: 124px" onmouseout=this.start() scrollAmount=1 scrollDelay=60 direction=up width=223 height=124>
<div><strong><%=ETicket.Web.HtmlController.Instance.IndexListArt(10,12,15,"/templ/index/lef_art.html") %></strong></div>
<div>&nbsp;</div>
</marquee>
                               
                            </ul>
            </div>
            <div id="demo2"></div>
          </div>
               <script>
   var speed=100
   demo2.innerHTML=demo1.innerHTML
   function Marquee(){
   if(demo2.offsetTop-demo.scrollTop<=0)
   demo.scrollTop-=demo1.offsetHeight
   else{
   demo.scrollTop++
   }
   }
   var MyMar=setInterval(Marquee,speed)
   demo.onmouseover=function() {clearInterval(MyMar)}
   demo.onmouseout=function() {MyMar=setInterval(Marquee,speed)}
            </script>
                <!--��������-->
        </dd>
      </dl>
    </div>
    
  </div>
</div>
<div class="cl"></div>
<div class="footer">
  <div class="w980">
    <div class="s1">
      <div class="fl"><img src="style/images/in_32.png" /></div>
      <div class="fr">
        <ul>
                  <li ><a href="#" target="_blank" title="������"><span><img src="style/images/20141229230604200848111452.jpg" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="���Ǿ���"><span><img src="style/images/20141229230718footer_logo.png" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="�ž���"><span><img src="style/images/20141229230834logo__1_.jpg" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="������"><span><img src="style/images/20141229230907logo__2_.jpg" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="����"><span><img src="style/images/20141229230939logo__2_.png" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="����"><span><img src="style/images/20141229231002logo.jpg" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="ɽˮ��"><span><img src="style/images/20141229231047main09.gif" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="����Ȫ"><span><img src="style/images/hdq.png" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="������"><span><img src="style/images/20141229231132nav_03.jpg" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="�����ĺ�"><span><img src="style/images/20141229231156QQE59BBEE7898720141229225645.png" width="80" height="40" /></span></a></li>
                    <li ><a href="#" target="_blank" title="������Դ"><img src="style/images/shiwaitaoyuan.jpg" width="80" height="40" /></a></li>
				    <li ><a href="#" target="_blank" title="������"><img src="style/images/longjinghe.jpg" width="80" height="40" /></a></li>
					<li ><a href="#" target="_blank" title="�콭"><img src="style/images/lvjiang.jpg" width="80" height="40" /></a></li>
					<li ><a href="#" target="_blank" title="������"><img src="style/images/yulonghe.jpg" width="80" height="40" /></a></li>
					<li ><a href="#" target="_blank" title="����"><img src="style/images/longji.jpg" width="80" height="40" /></a></li>
					<li ><a href="#" target="_blank" title="Ңɽ"><img src="style/images/yaoshan.jpg" width="80" height="40" /></a></li>
					<li ><a href="#" target="_blank" title="��ɽ"><img src="style/images/xiangshan.jpg" width="80" height="40" /></a></li>
					<li ><a href="#" target="_blank" title="����"><img src="style/images/guanyan.jpg" width="80" height="40" /></a></li>
					<li ><a href="#" target="_blank" title="«����"><img src="style/images/ludiyan.jpg" width="80" height="40" /></a></li>
					<li ><a href="#" target="_blank" title="����"><img src="style/images/wc.png" width="80" height="40" /></a></li>
        </ul>
      </div>
    </div>
 <div class="row" style="min-height:50px;margin-top:5px;padding:5px 10px; border:2px #dedede solid;">
                <b>�������ӣ�</b> <%=ETicket.Web.HtmlController.Instance.ListLink() %>
            </div>
    <div class="cl"></div>
    <div class="text">
    ��ܰ��ʾ��Ϊ�˻�ø��õ����飬����ʹ��IE8�����������(IE6��֧�ֲ����¼���)�������ֱ�����Ϊ1280*768�����&nbsp; &nbsp; <br />
          ��ַ������ʡ��������˷����˷����ɽ·һ��44�� &nbsp;| �������ߣ�0773-2300400&nbsp;|&nbsp; ����QQ:530678888<br />
          Copyright@2012-2018   <a href="http://www.yangshuo.cm">��˷����Ʊ��</a>yangshuo.cm ��Ȩ����  <a href="/app_yanpiao.html">��Ʊ</a> ��ICP��15007182��-2<br />
    </div>
  </div>
</div>
<link type="text/css" rel="stylesheet" href="css/style.css">
<body style="height:980px;">
<!-- ���벿��begin -->
<div class="toolbar">
   <a href="javascript:;" class="toolbar-item toolbar-item-weixin"><span class="toolbar-layer"></span></a>
   <a href="javascript:;" class="toolbar-item toolbar-item-feedback"></a>
   <a href="javascript:;" class="toolbar-item toolbar-item-app"><span class="toolbar-layer"></span></a>
   <a href="javascript:scroll(0,0)" id="top" class="toolbar-item toolbar-item-top"></a>
</div>
<!-- ���벿��end -->
</body>
<!-- ���벿��2 -->
<script type="text/javascript">
try {
var urlhash = window.location.hash;
if (!urlhash.match("fromapp"))
{
if ((navigator.userAgent.match(/(iPhone|iPod|Android|ios|iPad)/i)))
{
window.location="http://m.yangshuo.cm/"; //�������ַ���Ϊ���ֻ�վ����ַ
}
}
}
catch(err)
{
}
</script>
<!-- ���벿��2end -->
</html>