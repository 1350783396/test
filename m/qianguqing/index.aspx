﻿<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=0;"/>
<title>智慧游</title>
<meta name="keywords" content="阳朔景点门票,蝴蝶泉,图腾古道,遇龙河,金水岩,漓江漂流,漓江竹筏漂流,银子岩,银子岩专线,龙颈河漂流,阳朔,阳朔旅游,阳朔一日游，阳朔景点门票分销，阳朔旅游专线车，印象刘三姐,大榕树,阳朔电子票务,聚龙潭-奇石宫,阳朔网,阳朔智慧游 www.yangshuo.cm 阳朔景点门票网，0773-2300400" />
<meta name="description" content="阳朔电子票务,阳朔景点门票，阳朔旅游咨询服务，"/>
<script type="text/javascript" src="/shouye/js/Jquery/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="/shouye/js/Phone/phone.js"></script>
<script type="text/javascript" src="/shouye/js/TouchSlide/TouchSlide.1.1.js"></script>
<link href="/shouye/css/style.css" type="text/css" rel="stylesheet" />
<style type="text/css">
<!--
.STYLE1 {
	font-size: 24px;
	font-weight: bold;
}
-->
</style>
</head>

<body>
<div class="whole">

	<div class="header">
    	<div class="left-icon"></div>
        <div class="center-title"><img src="/shouye/image/logo80.png" /></div>
        <div class="right-search"></div>
        <div class="search">
        	<input type="text" name="search" value="" oninput="upperCase.call(this)" placeholder="请输入您要搜索的关键词" />
            <i class="empty"><img src="/shouye/image/close-1.png" /></i>
            <button type="button" name="sear"><img src="shouye/image/search.png" /></button>
            <span class="close"><img src="/shouye/image/close-1.png" /></span>
        </div>
        <script type="text/javascript">
        	$(function(){
				$(window).scroll(function(){		
					if($(window).scrollTop()>=2){
						$('.header').removeClass('header-search');
					}
				});	
			})
        </script>
    </div>

    
    <div class="source">
        <div id="imgs">
          <div class="bg"><img src="/wapimages/ad001.jpg" width="100%" /></div>
          <div class="bg"><img src="/wapimages/ad002.jpg" width="100%" /></div>
          <div class="bg"><img src="/wapimages/ad003.jpg" width="100%" /></div>
        </div>
    </div>
    
    <script>
		//封装的对象接受所有图片的盒元素与触发切换的最小滑动距离作为参数
		var ImageSwiper = function(imgs, minRange) {
		  this.imgBox = imgs
		  this.imgs = imgs.children
		  this.cur_img = 1  //起始图片设为1 ,而非0,将在图片显示方法中作-1处理
		  this.ready_moved = true  //判断每次滑动开始的标记变量
		  this.imgs_count = this.imgs.length
		  this.touchX  //触控开始的手指最初落点
		  this.minRange = Number(minRange)
		  this.fadeIn  //图片切换的方式,这里使用淡入淡出
		  this.fadeOut
		  this.bindTouchEvn() //初始化绑定滑动事件
		  //this.showPic(this.cur_img) //显示图片方法,注意其中图片编号的-1处理
		}
		ImageSwiper.prototype.bindTouchEvn = function() {
		  this.imgBox.addEventListener('touchstart', this.touchstart.bind(this), false)
		  this.imgBox.addEventListener('touchmove', this.touchmove.bind(this), false)
		  this.imgBox.addEventListener('touchend', this.touchend.bind(this), false)
		  this.imgBox.addEventListener('click', this.touchend.bind(this), false)	
		}
		ImageSwiper.prototype.touchstart = function(e) {
		  if (this.ready_moved) {
			var touch = e.touches[0];
			this.touchX = touch.pageX;
			this.ready_moved = false;
		  }
		
		}
		
		ImageSwiper.prototype.touchmove = function(e) {
		  e.preventDefault();
		  var minRange = this.minRange
		  var touchX = this.touchX
		  var imgs_count = this.imgs_count
		  //console.log(this.imgs[0]);
		  if (!this.ready_moved) {
		
			var release = e.changedTouches[0];
			var releasedAt = release.pageX;
			if (releasedAt + minRange < touchX) {
			  this.ready_moved = true;
			  if (this.cur_img > (imgs_count - 1)) {
				this.cur_img = 0;
			  }
			  this.cur_img++;
			  //this.showPic(this.cur_img);
		
			} else if (releasedAt - minRange > touchX) {
			  if (this.cur_img <= 1) {
				this.cur_img = imgs_count + 1
			  }
			  this.cur_img--;
			  //this.showPic(this.cur_img);
			  this.ready_moved = true;
			}
		  }
		
		}
		
		ImageSwiper.prototype.touchend = function(e) {
		  e.preventDefault();
		  img_append();
		  var minRange = this.minRange
		  var touchX = this.touchX
		  var imgs_count = this.imgs_count
		  if (!this.ready_moved) {
			var release = e.changedTouches[0];
			var releasedAt = release.pageX;
			if (releasedAt + minRange < touchX) {
			  this.ready_moved = true;
			  if (this.cur_img > (imgs_count - 1)) {
				this.cur_img = 0;
			  }
			  this.cur_img++;
			  showPic(this.cur_img);
		
			} else if (releasedAt - minRange > touchX) {
			  if (this.cur_img <= 1) {
				this.cur_img = imgs_count + 1
			  }
			  this.cur_img--;
			  showPic(this.cur_img);
			  this.ready_moved = true;
			}
		  }
		
		}
		//在样式表中设置好 .fadeIn 的透明度为0
		ImageSwiper.prototype.fadeIn = function(e) {
		  //e.classList.add("fadeIn");
		  //e.classList.add("act");
		}
		
		ImageSwiper.prototype.fadeOut = function(e) {
		  Array.prototype.forEach.call(e, function(e) {
			e.className = "bg";
		  })
		}
		
		//轮播点功能
		//ImageSwiper.prototype.showPic = function(cur_img) {
		  //this.hidePics(this.imgs)
		//得到图片元素的真实索引
		 // var index = cur_img - 1
		
		 // if (document.getElementsByClassName("active")[0]) {
			//var active = document.getElementsByClassName("active")[0];
			//active.classList.remove("active")
		 // }
		 // document.getElementById("dot_" + index).classList.add("active");
		
		 // this.fadeIn(this.imgs[index]);
		
		//}
		ImageSwiper.prototype.hidePics = function(e) {
		  this.fadeOut(e)
		
		}
		//传参
		var imgs = new ImageSwiper(document.getElementById('imgs'), 30)
		
		//自动轮播		
		function play(){
            timer=setInterval(function(e){
				$("#imgs").click();
            },4000);
        }
		
		play();
		
		function img_append(){
			var firstCard = $('#imgs').children('.bg').first();
			firstCard.addClass('fadeIn');
			$('#imgs').append(firstCard);
			img_change();	
		}
		
		function img_change(){
			$(".bg").each(function(index, element) {
				$(this).css({"z-index":-index,"margin-top":-(index*10)+'px',"width":(730-index*20)+'px',"margin-left":(index*10)+'px'});
			});
		}
		img_change();
		
	</script>
    
    <!--头部轮播END-->
    <!--分类-->
    <div class="food-list">
    <!---最新订单滚动文字模块开始---><!---最新订单滚动文字模块结束--->
		    <!---抢购模块--->
            <!---抢购模块END--->
            <!----门票推荐模块开始--->
<div class="recommend">
        	<div align="center"><a href="#" class="STYLE1">千古情专题</a> </div>
        	<div class="content-list">
            	<a href="#">
                	<div class="recom-img">
                    	<img src="qgq123.jpg" width="240" height="197" />
                        <div class="jian">
                        	<img src="/shouye/image/tuijian.png" />
                        </div>
                </div>
                    <div class="recom-tit">桂林千古情 12:00场
                      <p>￥99<i>抢</i></p>
                </div>
                </a>
                <a href="#">
                	<div class="recom-img">
                    	<img src="qgq123.jpg" width="240" height="197" />
                        <div class="jian">
                        	<img src="/shouye/image/tuijian.png" />
                        </div>
                </div>
                    <div class="recom-tit">桂林千古情 15:30场
                      <p>￥99<i>抢</i></p>
                    </div>
               
                <a href="#">
                	<div class="recom-img">
                    	<img src="qgq123.jpg" width="240" height="197" />
                        <div class="jian">
                        	<img src="/shouye/image/tuijian.png" />
                        </div>
                </div>
                    <div class="recom-tit">桂林千古情 19:30场
                      <p>￥99<i>抢</i></p>
                    </div>
                </a>
            </div>
</div>
        <!----门票推荐模块结束--->
		<!----线路推荐模块开始--->
		<!----线路推荐模块结束--->
        <!---底部广告图片开始--->
        <!---
		  <div class="content">
        	<div class="con-img">
            	<img src="shouye/image/im-1.jpg" />
            </div> 
 --->
        <!---底部广告图片结束--->
        <!---底部广告图片2开始--->
        <div class="content">
<div class="con-img">
            	<img src="/shouye/image/im-2.jpg" />
</div>
          <!---底部广告图片2结束--->   
         <!---底部导航开始--->
        <div class="footer">
        	<ul>
            	<li>
                	<a href="/index.aspx">
                    	<p><img src="/shouye/image/home.png" /></p>
                        <p>首页</p>
                    </a>
                </li>
                <li>
                	<a href="/content/list-ticket.aspx">
                    	<p><img src="/shouye/image/store-1.png" /></p>
                        <p>门票</p>
                    </a>
                </li>
                <li>
                	<a href="/content/list-line.aspx">
                    	<p><img src="/shouye/image/shopcar.png" /></p>
                        <p>线路</p>
                    </a>
                </li>
                <li>
                	<a href="/business/partner.aspx">
                    	<p><img src="/shouye/image/my.png" /></p>
                        <p>我的</p>
                    </a>
                </li>
            </ul>
        </div>
        <!---底部导航结束--->
</body>
</html>
