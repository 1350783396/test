$(function () {
    var broken_cookie = getCookie("clickmore");
    if (broken_cookie) {
        broken_cookie = broken_cookie.split(":");
        var the_visiteraccepted = unescape(broken_cookie[1])
    }
    var referurl = "";
    var back = "";
    var host = document.location.host;
    var fr = ~~getUrlParam("fr");
    try {
        referurl = document.referrer
    } catch (e) { }
    if (referurl.indexOf("lashou.com") > -1) {
        back = "yes"
    } else {
        referurl = "http://" + host + "?fr=" + fr
    }
    if (back == "yes") {
        $("#backlabel").text("返回")
    } else {
        $("#backlabel").text("首页")
    }
    $("#backhref").click(function () {
        if (back == "yes") {
            $("#backhref").attr("href", "javascript:history.go(-1)")
        } else {
            $("#backhref").attr("href", referurl)
        }
    });
    $("#backlabel").click(function () {
        if (back == "yes") {
            history.go(-1)
        } else {
            window.location.href = referurl
        }
    });
    $(".deal-item").click(function () {
        if (the_visiteraccepted == undefined) {
            var tmp = confirm("详情图片较多,可能会消耗较多流量.是否继续访问?");
            if (tmp == false) {
                return false
            } else {
                the_visiteraccepted = 1;
                the_cookie = "visiteraccepted:" + escape(the_visiteraccepted);
                setCookie("clickmore", the_cookie, 1)
            }
        }
    });
    $(".deal-box h2").click(function () {
        var cur_text = $.trim($(this).text());
        if (cur_text == "本单详情" || cur_text == "温馨提示") {
            show_content($(this), "tab-box")
        } else {
            if (cur_text == "评价") {
                show_content($(this), "goods-box")
            }
        }
        if (cur_text == "更多详情" && the_visiteraccepted == undefined) {
            if (!confirm("详情图片较多,可能会消耗较多流量.是否继续访问?")) {
                return false
            } else {
                the_visiteraccepted = 1;
                the_cookie = "visiteraccepted:" + escape(the_visiteraccepted);
                setCookie("clickmore", the_cookie, 1)
            }
        }
    });
    var flow_buybox = (function () {
        var $bar = $("#buybox");
        var $floatBar = $("#flow_buybox");
        var $win = $(window);
        var isShow = true;
        var left = $floatBar.css("left");
        return function () {
            var scrollTop = $win.scrollTop();
            var pos2 = $bar.offset().top + 8;
            var needShow = scrollTop + $win.height() < pos2;
            if (needShow !== isShow) {
                isShow = needShow;
                if (needShow) {
                    $floatBar.css("left", left)
                } else {
                    $floatBar.css("left", "9990px")
                }
            }
        }
    })();
    $(window).scroll(function () {
        flow_buybox();
        return false
    }).trigger("scroll");
    if (!isNaN(cha) && cha <= 86400) {
        var seconds = cha;
        var time_left = window.setInterval(function () {
            seconds--;
            var minutes = Math.floor(seconds / 60);
            var hours = Math.floor(minutes / 60);
            var days = Math.floor(hours / 24);
            var CDay = days;
            var CHour = hours % 24;
            var CMinute = minutes % 60;
            var CSecond = Math.floor(seconds % 60);
            var timeStr = "";
            if (CDay > 0) {
                timeStr += CDay + "天"
            }
            if (CHour > 0 || CDay > 0) {
                timeStr += CHour + "小时"
            }
            if (CMinute > 0 || CDay > 0 || CHour > 0) {
                timeStr += CMinute + "分钟"
            }
            timeStr += CSecond + "秒";
            $("#last_time").html(timeStr);
            if (seconds <= 0) {
                window.clearInterval(time_left);
                $(".oh-time").html("");
                $(".deal-buybtn").addClass("deal-nobuy");
                $(".deal-buybtn").html('<a title="已结束">已结束</a>')
            }
        }, 1000)
    }
    if (recommend_info.id > 0) {
        $.getJSON("/ajax/aj_getRecommend.php", recommend_info, function (data) {
            if (data.status === 0 && data.list.length > 0) {
                var $item = $("#recommend_show");
                var $more = $item.find("#recommend_more");
                var $section = $item.find("section.thisother");
                $item.find(".title h2").text(data.title);
                $more[data.show_more ? "show" : "hide"]();
                for (var i = 0; i < data.list.length; ++i) {
                    var $new = $section.clone();
                    if (data.show_more && i >= data.more_num) {
                        $new.hide()
                    }
                    var goods = data.list[i];
                    $new.find("a").attr("href", "detail.php?id=" + goods.goods_id + "&" + data.query);
                    if (goods.lashouPrice) {
                        if (!goods.beginword) {
                            $new.find(".all .price-act").children("em").remove();
                            $new.find(".all .price-act").children("font").html("&yen;<b>" + goods.lashouPrice + "</b>")
                        } else {
                            $new.find(".all .price-act").children("em").html("<img src=" + imgHost + "img/price/" + goods.beginword + " title=" + goods.title + ">").next("font").html("&yen;<b>" + goods.lashouPrice + "</b>")
                        }
                    } else {
                        $new.find(".all .price-act").remove()
                    }
                    $new.find(".all .price").children("font").html("&yen;<b>" + goods.price + "</b>").next().html("&yen;" + goods.value);
                    $new.find(".all .txt").text(goods.product || "");
                    $more.before($new)
                }
                $section.remove();
                $item.show("fast")
            }
        })
    }
});

function show_content(obj, className) {
    obj.siblings("div[class='" + className + "']").toggle();
    if (obj.siblings("div[class='" + className + "']").css("display") == "block") {
        if (obj.attr("class") != "cur") {
            obj.addClass("cur")
        }
    } else {
        obj.removeClass("cur")
    }
}

function recommend_show_more() {
    var count = 0;
    var turnFlag = true;
    $("#recommend_show section").each(function () {
        if (count == 4) {
            if ($(this).is(":hidden")) {
                turnFlag = true
            } else {
                turnFlag = false
            }
        }
        if (count > 3) {
            if (turnFlag) {
                $(this).show()
            } else {
                $(this).hide()
            }
        }
        count++
    });
    if (turnFlag) {
        $("#recommend_more a").html("收起")
    } else {
        $("#recommend_more a").html("查看更多");
        $("#recommend_more").show()
    }
};