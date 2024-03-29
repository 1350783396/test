﻿function getDistanceList(e, t) {
    var i = getCookie("cur_lat"),
        n = getCookie("cur_lng");
    getDistanceList.isLoading || (getDistanceList.isLoading = !0, t.text("正在获取位置信息..."), i > 0 && n > 0 ? (getDistanceList.isLoading = !1, t.text("成功获取,正在跳转..."), setTimeout(function () {
        t.text("离我最近")
    }, 3e3), location.href = e) : getLocation(function () {
        getDistanceList.isLoading = !1, t.text("成功获取,正在跳转..."), setTimeout(function () {
            t.text("离我最近")
        }, 3e3), location.href = e
    }, function (e) {
        t.text(e.show_msg), setTimeout(function () {
            getDistanceList.isLoading = !1, t.text("离我最近")
        }, 1e3)
    }))
}
$(function () {
    var e = new SlideNav;
    if ($(".mall-cate-box a").click(function () {
            var t = $(this),
                i = t.attr("data-type");
            if (i) {
                var n = $(e.options.menuSelector.wrap).filter('[data-type="' + i + '"]'),
                    o = n.find(e.options.menuSelector.top);
                if (o.length) {
                    var s = e.isShowedMenu(o, 1);
                    if ($(".pop-shade").trigger("click"), !s) {
                        t.parents("li").addClass("select");
                        var r = e.controllerToMenu(o, 1, !0);
                        window.scrollTo(0, $("header").height()), o.data("initIScroll") || (n.show(), e.fixHeight(o, !0)), e.showTopMenu(r, o)
    }
    }
    }
    }), $(".sort-view section a").bind("iscrollTap click", function (t) {
            var i = $(this),
                n = i.attr("data-main"),
                o = n && "emptyMenu" === n.split("_")[1];
            if ("click" === t.type) return n && !o ? !1 : "";
            var s = i.parent();
            if (s.siblings(".select").removeClass("select"), s.addClass("select"), n && !o) {
                if ($.inArray(n, e.options.noTriggerMenus) > -1) return n === e.options.distanceHandle && getDistanceList(i.attr("href"), i.find("span")), !1;
                var r = i.parents(e.options.menuSelector.top).siblings(e.options.menuSelector.sub).filter('[data-main="' + n + '"]'),
                    h = e.isShowedMenu(r, 2);
                return h || (e.hideMenu(null, 2), e.fixHeight(r), e.showSubMenu(null, r)), !1
    }
    }), $(".sort-sider section a").bind("iscrollTap click", function (t) {
            var i = $(this),
                n = i.is(e.options.menuSelector.subHandle);
            if ("click" === t.type) return n && i.attr("data-sub") ? !1 : "";
            if (n) {
                var o = e.controllerToMenu(i, 2),
                    s = e.controllerToMenu(i, 3),
                    r = e.isShowedMenu(s, 3);
                if (e.controllerToMenu(o, 2, !0).removeClass("select"), e.hideMenu(null, 3), r ? o.showIScroll(!0, "refresh") : (i.addClass("select"), e.showThirdMenu(null, s)), s.length) return !1
    }
    }), $(".sort-sub-nav a").click(function () {
            var e = $(this),
                t = e.attr("data-type");
            t && $("#districtLine").attr("data-type", t).trigger("click")
    }), $(".pop-shade").click(function () {
            e.hideMenu(null, 3), e.hideMenu(null, 2), e.hideMenu(null, 1), $(".mall-cate-box li").removeClass("select"), $(this).hide().css("height", "100%")
    }), window.initSort) {
        var t = $(e.options.menuSelector.wrap + '[data-type="' + window.initSort + '"]');
        if (t.length) {
            var i = t.find(e.options.menuSelector.top),
                n = Math.min(i.height(), $(window).height() - $("header").height());
            /qqbrowser/i.test(navigator.userAgent) && /iphone|ipod/i.test(navigator.userAgent) && (n -= 45), t.height(n), t.find(".sort-wrapper").height(n), i.height(n);
            var o = e.controllerToMenu(i, 1, !0);
            e.showTopMenu(o, i)
        }
    }
}), $.fn.extend({
    showIScroll: function (e, t) {
        if (0 === this.length) return this;
        var i = this.data("initIScroll");
        if (i) t && i instanceof IScroll && "refresh" === t && (i.refresh(), SlideNav.prototype.displayMoreSign(i));
        else {
            var i = new IScroll(this.get(0), {
                mouseWheel: !0,
                tap: "iscrollTap",
                click: !0,
                disableMouse: "ontouchend" in document
            });
            SlideNav.prototype.displayMoreSign(i), i.on("scrollEnd", function () {
                SlideNav.prototype.displayMoreSign(i)
            }), this.data("initIScroll", e ? i : "ok")
        }
        return this
    }
});
var SlideNav = function (e) {
    this.options = $.extend({}, this.options, e), this.showedMenus = {}
};
SlideNav.prototype = {
    options: {
        noTriggerMenus: ["distanceHandle"],
        distanceHandle: "distanceHandle",
        menuSignKey: "slideMenuKey",
        menuSelector: {
            wrap: ".sortdrop-wrapper",
            top: ".sort-view",
            sub: ".sort-sider",
            third: ".sort-shop-sider",
            subHandle: ".submenu_handle",
            moreSign: ".m-down",
            noExists: "#noneExistsElement"
        },
        thirdMenus: ["category_3"],
        showEmptySubMenu: !1
    },
    showedMenus: {},
    isShowedMenu: function (e, t) {
        var i = e.data(this.options.menuSignKey);
        if (i && this.showedMenus)
            for (var n in this.showedMenus)
                if (!t || t === ~~n) {
                    var o = this.showedMenus[n].data(this.options.menuSignKey);
                    if (o === i) return ~~n
                }
        return 0
    },
    ctrlShowedMenu: function (e, t, i) {
        switch (e) {
            case "get":
                return t in this.showedMenus ? this.showedMenus[t] : $(this.options.menuSelector.noExists);
            case "show":
                i && (this.showedMenus[t] = i);
                break;
            case "hide":
                delete this.showedMenus[t]
        }
    },
    showMenu: function (e, t, i, n) {
        return 0 === e.length ? !1 : (1 === t && !n && this.options.menuSelector.wrap && e.parents(this.options.menuSelector.wrap).show(), e.data(this.options.menuSignKey, Math.random()), i && e.show() || e.show().showIScroll(), void this.ctrlShowedMenu("show", t, e))
    },
    hideMenu: function (e, t, i) {
        e = e && e.length ? e : this.ctrlShowedMenu("get", t);
        var n = this.isShowedMenu(e, t);
        return n > 0 && (this.ctrlShowedMenu("hide", n), 1 === n && !i && this.options.menuSelector.wrap ? e.parents(this.options.menuSelector.wrap).hide() : e.hide()), e
    },
    showTopMenu: function (e, t, i) {
        if (t = t && t.length ? t : this.controllerToMenu(e, 1), this.showMenu(t, 1), $(".pop-shade").show().height($(".pop-shade").height() + $("footer").height()), e && e.length && !i) {
            var n = e.attr("data-main"),
                o = n && "emptyMenu" === n.split("_")[1];
            if (n && -1 === $.inArray(n, this.options.noTriggerMenus) && (!o || this.options.showEmptySubMenu)) {
                var s = t.parents(this.options.menuSelector.wrap).find(this.options.menuSelector.sub).filter('[data-main="' + n + '"]'),
                    r = this.controllerToMenu(s, 2, !0);
                this.showSubMenu(r, s)
            }
        }
    },
    showSubMenu: function (e, t, i) {
        t = t && t.length ? t : this.controllerToMenu(e, 2), this.showMenu(t, 2, !0), t.showIScroll($.inArray(t.attr("data-main"), this.options.thirdMenus) > -1), e && !i && this.showThirdMenu(e)
    },
    showThirdMenu: function (e, t) {
        t = t && t.length ? t : this.controllerToMenu(e, 3), this.showMenu(t, 3, !0), t.parents(this.options.menuSelector.sub).showIScroll(!0, "refresh")
    },
    controllerToMenu: function (e, t, i) {
        if (!e || "object" != typeof e) return $(this.options.menuSelector.noExists);
        switch (t) {
            case 1:
                return i ? e.find(".select a") : e.parents(this.options.menuSelector.top);
            case 2:
                return i ? e.find("a.select") : e.parents(this.options.menuSelector.sub);
            case 3:
                return e.siblings(i ? this.options.menuSelector.subHandle : this.options.menuSelector.third);
            default:
                return $(this.options.menuSelector.noExists)
        }
    },
    displayMoreSign: function (e) {
        var t = !0;
        e.scrollerHeight <= e.wrapperHeight ? t = !1 : e.y - e.maxScrollY < 20 && (t = !1), $(e.wrapper).find(this.options.menuSelector.moreSign)[t ? "show" : "hide"]()
    },
    fixHeight: function (e, t) {
        var i = e.closest(".sort-wrapper");
        if (t) {
            var n = i.closest(this.options.menuSelector.wrap),
                o = i.siblings(".sort-sub-nav"),
                s = Math.min(o.height() + i.height(), $(window).height() - $(".mall-cate-box").height() - 50),
                r = s - o.height();
            n.height(s), i.height(r), e.height(r)
        } else e.height(i.height())
    }
};