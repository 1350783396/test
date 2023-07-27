function checkEmail(e) {
    return testEmail.test(e) ? !0 : !1
}

function checkMobile(e) {
    return testMobile.test(e) ? !0 : !1
}

function loadScript() {
    var e = document.createElement("script");
    e.type = "text/javascript", e.src = "https://maps.google.com/maps/api/js?sensor=true", document.body.appendChild(e)
}

function initialize() {
    function e(e) {
        alert(1 == e ? "获取位置失败" : "不支持位置服务")
    }
    if (navigator.geolocation) browserSupportFlag = !0, navigator.geolocation.getCurrentPosition(function (e) {
        cur_lat = e.coords.latitude, cur_lng = e.coords.longitude, cur_pos(cur_lat, cur_lng)
    }, function (e) {
        switch (e.code) {
            case e.PERMISSION_DENIED:
                alert("用户选择了拒绝了位置服务");
                break;
            case e.POSITION_UNAVAILABLE:
                alert("位置不可知");
                break;
            case e.TIMEOUT:
                alert("获取位置信息超时")
        }
    }, {
        maximumAge: 3e3,
        timeout: 5e3,
        enableHighAccuracy: !0
    });
    else if (google.gears) {
        browserSupportFlag = !0;
        var t = google.gears.factory.create("beta.geolocation");
        t.getCurrentPosition(function (e) {
            cur_lat = e.latitude, cur_lng = e.longitude
        }, function () {
            handleNoGeoLocation(browserSupportFlag)
        })
    } else browserSupportFlag = !1, e(browserSupportFlag)
}

function bubbleSortF(e) {
    for (var t, o, r = 0, n = e.length; n > r; r++)
        for (t = 0; n > t; t++) e[r] < e[t] && (o = e[t], e[t] = e[r], e[r] = o);
    return e
}

function countdown(e, t, o, r, n) {
    function a() {
        var r = s + 1e3 * n - (new Date).getTime();
        r > 0 ? e.attr("value", Math.ceil(r / 1e3) + "秒") : (window.clearInterval(i), e.attr("value", t), e.attr("class", o), e.removeAttr("disabled"))
    }
    var i, s = (new Date).getTime();
    e.attr("disabled", "disabled"), e.attr("class", r), e.attr("value", n + "秒"), i = window.setInterval(a, 1e3)
}

function msg_tit_common(e, t, o, r) {
    e ? ($("." + t).html(e), $("." + t).show(), r && error_msg_ani(t), o && $("body,html").animate({
        scrollTop: 0
    }, 500)) : ($("." + t).html(""), $("." + t).hide())
}

function error_msg_ani(e) {
    $("." + e).addClass("entryamt"), setTimeout("$('." + e + "').removeClass('entryamt')", 500)
}

function jsType(e) {
    var t = Object.prototype.toString.call(e);
    return t.substring(8, t.length - 1)
}

function getUrlParam(e, t, o) {
    return UrlInfo.getParam(e, t, o)
}

function simulateForm(e, t, o, r) {
    return UrlInfo.simulateForm(e, t, o, r)
}

function setCookie(e, t, o) {
    var r = o,
        n = new Date;
    n.setTime(n.getTime() + 24 * r * 60 * 60 * 1e3), document.cookie = e + "=" + escape(t) + ";expires=" + n.toGMTString() + ";domain=.lashou.com"
}

function getCookie(e) {
    var t = document.cookie.match(new RegExp("(^| )" + e + "=([^;]*)(;|$)"));
    return null != t ? unescape(t[2]) : null
}

function delCookie(e) {
    var t = new Date;
    t.setTime(t.getTime() - 1);
    var o = getCookie(e);
    null != o && (document.cookie = e + "=" + o + ";expires=" + t.toGMTString() + ";domain=.lashou.com")
}

function getLocation(e, t) {
    var o = $("input[name=fr]").val();
    if (15073 == o) $.ajax({
        async: !1,
        beforeSend: function (e) {
            e.setRequestHeader("X_Requested_With", "XMLHttpRequest")
        },
        dataType: "json",
        url: "/ajax/ali_position.php",
        success: function (o) {
            if ("200" == o.msg) {
                {
                    getCookie("cur_lat"), getCookie("cur_lng")
                }
                "function" == typeof e && e()
            } else "function" == typeof t && t()
        }
    });
    else {
        if (!navigator.geolocation) return t({
            code: 100,
            msg: "",
            show_msg: "获取失败"
        });
        navigator.geolocation.getCurrentPosition(function (t) {
            setCookie("cur_lng", t.coords.longitude, 1 / 24 / 6), setCookie("cur_lat", t.coords.latitude, 1 / 24 / 6), setCookie("cur_geostatus", 0, 1 / 24 / 6), "function" == typeof e && e(t)
        }, function (e) {
            switch (e.code) {
                case e.PERMISSION_DENIED:
                    e.show_msg = "您选择了拒绝位置服务";
                    break;
                case e.POSITION_UNAVAILABLE:
                    e.show_msg = "位置信息获取失败";
                    break;
                case e.TIMEOUT:
                    e.show_msg = "位置信息获取失败";
                    break;
                default:
                    e.show_msg = "获取位置信息失败"
            }
            setCookie("cur_geostatus", e.code || 99, 1 / 24), "function" == typeof t && t(e)
        }, {
            maximumAge: 3e4,
            timeout: 5e3,
            enableHighAccuracy: !0
        })
    }
}

function convertType(e, t, o) {
    switch (o = o || 2, t) {
        case "int":
            e = Math.round(parseFloat(e)), e = isNaN(e) ? 0 : e;
            break;
        case "float":
            e = parseFloat(e), e = 1 * (isNaN(e) ? 0 : e).toFixed(o);
            break;
        case "string":
            e = String(e);
            break;
        case "boolean":
            e = !!e;
            break;
        case "money":
            e = parseFloat(e), e = (isNaN(e) ? 0 : e).toFixed(2)
    }
    return e
}
var testEmail = /^[A-Za-z0-9](([_\.\+\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$/,
    testMobile = /^(13[0-9]|14[0-9]|15[0|1|2|3|5|6|7|8|9]|17[0-9]|18[0-9])\d{8}$/;
jQuery.proxy = function (e, t) {
    var o, r, n;
    return "string" == typeof t && (n = e[t], t = e, e = n), jQuery.isFunction(e) ? (o = [].slice.call(arguments, 2), r = function () {
        return e.apply(t || this, o.concat([].slice.call(arguments)))
    }) : void 0
};
var UrlInfo = {
    keyTypeConf: {
        fr: "int",
        vt: "int",
        id: "int",
        aid: "int",
        page: "int",
        cat: "int"
    },
    keyType: {
        "int": 1,
        string: 1,
        "boolean": 1,
        money: 1,
        "float": 1
    },
    autoType: function (e) {
        return e in this.keyTypeConf ? this.keyTypeConf[e] : ""
    },
    keepParam: function (e, t, o) {
        o = "Object" === jsType(o) ? o : this.parseUrl(location.href, "query");
        var r = this.parseUrl(e);
        if ("Array" === jsType(t))
            for (var n = 0; n < t.length; n++) {
                var a = t[n];
                o[a] && (r.query[a] = o[a])
            }
        return this.buildUrl(r)
    },
    buildUrl: function (e) {
        var t = this.buildQuery(e.query),
            o = !e.path && e.host ? "/" : e.path,
            r = e.scheme ? e.scheme + ":" : "";
        return r += e.slashes + e.host + o, r += t ? "?" + t : "", r += e.fragment ? "#" + e.fragment : ""
    },
    buildQuery: function (e) {
        var t, o, r = [];
        for (var n in e)
            if (t = jsType(e[n]), "Array" !== t && "Object" !== t) r.push(encodeURIComponent(n) + "=" + encodeURIComponent(e[n]));
            else
                for (var a in e[n]) o = n + "[" + ("Object" === t ? a : "") + "]", r.push(encodeURIComponent(o) + "=" + encodeURIComponent(e[n][a]));
        var i = r.join("&");
        return i
    },
    parseUrl: function (e, t) {
        var o = /^(?:(\w+):)?(?:(\/{2,3})([\w-]+(?:\.[\w-]+)*))?([^?#]*)(?:\?([^#]*))?(?:#(.*))?$/,
            r = o.exec(e) || [],
            n = {
                url: e
            };
        return n.scheme = r[1] || "", n.slashes = r[2] || "", n.host = r[3] || "", n.path = r[4] || "", n.query = this.parseQuery(r[5] || ""), n.string = r[5] || "", n.fragment = r[6] || "", t ? n[t] || "" : n
    },
    parseQuery: function (e) {
        var t = {},
            o = /(?:^|&)([^=&]+)=?([^&]*)/g,
            r = /^([^[]*)\[(.*)\]$/;
        e = e || "";
        for (var n, a, i, s, u, c = {}, l = {}; ;) {
            if (n = o.exec(e), !n) break;
            a = decodeURIComponent(n[1]), s = decodeURIComponent(n[2] || ""), u = r.exec(a), u ? (a = u[1], i = u[2], c[a] = c[a] || i, i || (i = (a in l ? l[a] : -1) + 1, l[a] = i), "Object" !== jsType(t[a]) && (t[a] = {}), t[a][i] = s) : (a in c && (delete c[a], delete l[a]), t[a] = s)
        }
        for (a in c)
            if (!c[a]) {
                u = [];
                for (i in t[a]) u.push(t[a][i]);
                t[a] = u
            }
        return t
    },
    getParam: function (e, t, o) {
        t = t || location.href;
        var r = this.parseUrl(t, "query");
        if (e) {
            var n = r[e] || "";
            return o && (n = convertType(n, this.autoType(e))), n
        }
        return r
    },
    simulateForm: function (e, t, o, r) {
        if (o = "get" === (o || "").toLowerCase() ? "get" : "post", "" === $.trim(e)) return !1;
        if ("Object" !== jsType(t)) return !1;
        if ("get" === o) {
            var n = this.parseUrl(e, "query");
            t = $.extend({}, n, t)
        }
        if (r) {
            var a = $.ajax({
                url: e,
                type: o,
                async: !1,
                data: t
            }),
                i = a.responseText ? a.responseText : a.responseXML;
            return i ? i : ""
        }
        var s = $('<form action="' + e + '" onsubmit="return UrlInfo.directSubmit(event);" method="' + o + '">');
        for (var u in t) s.append('<input type="hidden" name="' + u + '" value="' + t[u] + '" />');
        return s.appendTo("body").submit().remove(), !0
    },
    directSubmit: function (e) {
        return e = e || window.event, e.cancelBubble = !0, e.stopPropagation && e.stopPropagation(), !0
    }
};