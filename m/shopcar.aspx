﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopcar.aspx.cs" Inherits="ETicket.Web.shopcar" %>

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
    <link rel="stylesheet" type="text/css" href="/bootstrap/css/bootstrap.css">
    <style type="text/css">
        #logo {
            float: none;
            margin: 0;
            padding: 10px 0;
            position: relative;
        }

            #logo b {
                background: url("/bootstrap/logo/head-info-2.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
                height: 75px;
                left: 110px;
                position: absolute;
                top: 13px;
                width: 350px;
            }

        .error {
            color: red;
        }

        td.title {
            vertical-align: middle;
            line-height: 40px;
        }
    </style>

    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/common.js"></script>
    <script type="text/javascript" src="/js/btnReSubmit.js"></script>

    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>
    <link rel="stylesheet" type="text/css" href="/css/Car.css">
    <script type="text/javascript" src="/js/Car.js"></script>
</head>
<body class="um-vp " ontouchstart>
    <div id="page_0" class="up ub ub-ver bc-bg" tabindex="0">
        <!--header开始-->
        <div id="header" class="uh bc-text-head ub bc-head">
            <div class="nav-btn" id="nav-left">
                <div class="fa fa-angle-left fa-2x" onclick="window.open('/');"></div>
            </div>
            <h1 class="ut ub-f1 ulev-3 ut-s tx-c" tabindex="0">下单预订</h1>
            <div class="nav-btn ">
                <div class=" nav-bt" id="nav-right">
                    <!--<div class="ub-img icon-home umw2 umh4"></div>-->
                </div>
                <!--
			        <div class=" nav-bt" id="nav-right1">
			            <div class="ub-img icon-call umw2 umh4"></div>
			
			        </div>
			        -->
            </div>
        </div>
        <!--header结束-->
        <!--content开始-->
        <div id="content" class="ub-f1 tx-l">
            <form id="car" runat="server" method="post">
                <input type="hidden" id="txtProductID" name="txtProductID" value="<%=NJiaSu.Libraries.PubFun.QueryInt("id") %>" />

                <%=ETicket.Web.HtmlController.Instance.Car() %>
            </form>
        </div>
        <!--content结束-->

    </div>
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
            //计算总价
            keyCal();
            //加载库存
            sysLoadStock();

            //验证
            jQuery.validator.addMethod(function (value, element) {
                return this.optional(element) || /^[\u4E00-\u9FA5]+$/.test($.trim(value));
            }, "姓名只能为中文");

            /*
            jQuery.validator.addMethod("isIdCardNo", function (value, element) {
                value = $.trim(value);
                return this.optional(element) || checkCard(value);
            }, "请正确填写身份证号码");
            */

            jQuery.validator.addMethod("isMobile", function (value, element) {
                var length = value.length;
                var mobile = /^(((13[0-9]{1})|(14[0-9]{1})|(15[0-9]{1})|(16[0-9]{1})|(17[0-9]{1})|(18[0-9]{1})|(19[0-9]{1}))+\d{8})$/;
                return this.optional(element) || (length == 11 && mobile.test(value));
            }, "请正确填写手机号码");

            $("#znjx").on("click", function () {
                var jxtext = $("#znjxstr").val();
                var xingming = jxtext.replace(/\d/g, "");
                var shouji = jxtext.replace(/[^0-9]/ig, "");
                console.log(xingming);
                console.log(shouji);
                $("#realName").val(xingming);
                $("#phone").val(shouji);
            })
            jQuery.validator.addMethod("overStock", function (value, element) {
                var stockStr = $("#stockNum").html();
                if (stockStr == "有票") {
                    return true;
                }
                else {
                    var stock = parseInt(stockStr);
                    value = parseInt(value);
                    return stock >= value;
                }
            }, "购买数量超出库存");


            jQuery.validator.addMethod("overAccount", function (value, element) {
                if (value == "积分支付") {
                    var total = parseFloat($("#totalPrice").html());
                    var account = parseFloat($("#txtAccount").val());
                    if (total > account) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else {
                    return true;
                }
            }, "积分不足支付");

            jQuery.validator.addMethod("isNum", function (value, element) {
                var regNum = /^\d+$/;
                return value.match(regNum);
            }, "请输入正确的购买数量");

            //用户注册页字段验证
            $("#<%=car.ClientID%>").validate({
                submitHandler: function (form) {
                    sAlert("订单正在提交中..........请稍等");
                    form.submit();
                },
                ignore: "",
                rules: {

                    hiddenStartTime: {
                        remote: {
                            type: "post",
                            url: "/ajax/chk_saletime.ashx",
                            data: {
                                "startDate": function () {
                                    return $("#startDate").val();
                                },
                                "startTime": function () {
                                    return $("#startTime").val();
                                },
                                "id": function () {
                                    return $("#txtProductID").val();
                                }
                            }
                        }
                    },
                    startAddress: {
                        required: true
                    },
                    buyNum: {
                        required: true,
                        isNum: true,
                        overStock: true
                    },
                    selPayType: {
                        required: true,
                        overAccount: true
                        /*
                        remote: {
                            type:"post",
                            url: "/ajax/chk_account.ashx",
                            data: {
                                "totalPrice": function() {
                                    return $("#totalPrice").html();
                                }}
                        }
                        */
                    },
                    realName: {
                        required: true,
                        isName: true
                    },
                    /*
                    idCard: {
                        required: true,
                        isIdCardNo:true
                    },
                    */
                    phone: {
                        required: true,
                        isMobile: true
                    }
                },
                messages: {
                    hiddenStartTime: {
                        remote: "已超最后下单时间，无法购买"
                    },
                    startAddress: {
                        required: "上车地点不能为空"
                    },
                    buyNum: {
                        required: "请输入购买数量"
                    },
                    selPayType: {
                        required: "请选择支付方式",
                        overAccount: "积分不足，当前为" + $("#txtAccount").val()
                    },
                    realName: {
                        required: "请输入姓名"
                    },
                    /*
                    idCard:{
                        required: "请输入身份证号"
                    },
                    */
                    phone: {
                        required: "请输入手机号码"
                    }
                }
            });//验证

        });

        //身份证-检查
        function checkCard(cId) {
            var pattern;
            if (cId.length == 15) {
                pattern = /^\d{15}$/; // 正则表达式,15位且全是数字
                if (pattern.exec(cId) == null) {
                    return false;
                }
                if (!isdate("19" + cId.substring(6, 8), cId.substring(8, 10), cId
                    .substring(10, 12))) {
                    return false;
                }
            } else if (cId.length == 18) {
                pattern = /^\d{17}(\d|x|X)$/; // 正则表达式,18位且前17位全是数字，最后一位只能数字,x,X
                if (pattern.exec(cId) == null) {
                    return false;
                }
                if (!isdate(cId.substring(6, 10), cId.substring(10, 12), cId.substring(
                    12, 14))) {
                    return false;
                }
                var strJiaoYan = ["1", "0", "X", "9", "8", "7", "6", "5", "4", "3",
                    "2"];
                var intQuan = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];
                var intTemp = 0;
                for (i = 0; i < cId.length - 1; i++)
                    intTemp += cId.substring(i, i + 1) * intQuan[i];
                intTemp %= 11;
                if (cId.substring(cId.length - 1, cId.length).toUpperCase() != strJiaoYan[intTemp]) {
                    return false;
                }
            }
            else {
                return false;
            }
            return true;
        };
        //身份证-检查年月日是否是合法日期
        function isdate(intYear, intMonth, intDay) {
            if (isNaN(intYear) || isNaN(intMonth) || isNaN(intDay))
                return false;
            if (intMonth > 12 || intMonth < 1)
                return false;
            if (intDay < 1 || intDay > 31)
                return false;
            if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11)
                && (intDay > 30))
                return false;
            if (intMonth == 2) {
                if (intDay > 29)
                    return false;
                if ((((intYear % 100 == 0) && (intYear % 400 != 0)) || (intYear % 4 != 0))
                    && (intDay > 28))
                    return false;
            }
            return true;
        }
        /*
        function check()
        {
            var isSumbit = true;
            //购买数量
            var numStr = $("#buyNum").val();
            var num = parseFloat(numStr);
            var regNum = /^\d+$/;
            if (!numStr.match(regNum))
            {
                $("#forBuyNum").html("购买数量请填数字");
                isSumbit = isSumbit && false;
            }
            else
            {
                if (num <= 0)
                {
                    $("#forBuyNum").html("购买数量必须大于0");
                    isSumbit = isSumbit && false;
                }
                else
                {
                    $("#forBuyNum").html("");
                    isSumbit = isSumbit && true;
                }
            }
            //库存
            var stock = parseInt($("#stockNum").html());
            if (isNaN(stock) || stock <= 0) {
                $("#forStock").html("库存目前不足，无法下单");
                isSumbit = isSumbit && false;
            }
            else {
                if (num > stock) {
                    $("#forStock").html("购买数量已经超出目前库存数量");
                    isSumbit = isSumbit && false;
                }
                else {
                    $("#forStock").htm();
                    isSumbit = isSumbit && true;
                }
            }
            return isSumbit;
        }
        */
        //计算总价
        function keyCal() {
            var unit = $("#unitPrice").html();
            var num = parseInt($("#buyNum").val());
            if (!isNaN(num) || num > 0) {
                $("#totalPrice").html(parseFloat(unit) * num);

                var rebate = parseFloat($("#rebateUnit").val() * num);
                if (rebate > 0) {
                    $("#rebateInfo").html("支付后返积分：" + rebate);
                }
            }
        }
        /*
        function calTotal()
        {
            var unit = $("#unitPrice").html();
            var num = parseInt($("#buyNum").val());
            if (isNaN(num) || num <= 0)
            {
                alert("购买数量请输入大于1");
                return;
            }
            var total = parseFloat(unit) * num;
            $("#totalPrice").html(total);
        }
        */
        //根据选择的发车时间，加载库存
        function sysLoadStock() {
            //选择日期时候，清空
            $("#forStartTime").html("");

            var startDate = $("#startDate").val();
            var startTime = $("#startTime").val();
            var productID = $("#txtProductID").val();

            $("#hiddenStartTime").val(startDate + " " + startTime);

            $.ajax({
                type: 'POST', url: "/ajax/chk_stock.ashx",
                dataType: "text",
                data: { saleDate: startDate, saleTime: startTime, id: productID },
                error: function () { },
                success: function (data) {
                    $("#stockNum").html(data);
                    $("#forBuyNum").html("");
                }
            });
            //stockNum
        }
    </script>

</body>

</html>


