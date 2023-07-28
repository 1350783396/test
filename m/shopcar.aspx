<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopcar.aspx.cs" Inherits="ETicket.Web.shopcar" %>

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
            var surname_one = "赵|钱|孙|李|周|吴|郑|王|冯|陈|楮|卫|蒋|沈|韩|杨|朱|秦|尤|许|何|吕|施|张|孔|曹|严|华|金|魏|陶|姜|戚|谢|邹|喻|柏|水|窦|章|云|苏|潘|葛|奚|范|彭|郎|鲁|韦|昌|马|苗|凤|花|方|俞|任|袁|柳|酆|鲍|史|唐|费|廉|岑|薛|雷|贺|倪|汤|滕|殷|罗|毕|郝|邬|安|常|乐|于|时|傅|皮|卞|齐|康|伍|余|元|卜|顾|孟|平|黄|和|穆|萧|尹|姚|邵|湛|汪|祁|毛|禹|狄|米|贝|明|臧|计|伏|成|戴|谈|宋|茅|庞|熊|纪|舒|屈|项|祝|董|梁|杜|阮|蓝|闽|席|季|麻|强|贾|路|娄|危|江|童|颜|郭|梅|盛|林|刁|锺|徐|丘|骆|高|夏|蔡|田|樊|胡|凌|霍|虞|万|支|柯|昝|管|卢|莫|经|房|裘|缪|干|解|应|宗|丁|宣|贲|邓|郁|单|杭|洪|包|诸|左|石|崔|吉|钮|龚|程|嵇|邢|滑|裴|陆|荣|翁|荀|羊|於|惠|甄|麹|家|封|芮|羿|储|靳|汲|邴|糜|松|井|段|富|巫|乌|焦|巴|弓|牧|隗|山|谷|车|侯|宓|蓬|全|郗|班|仰|秋|仲|伊|宫|宁|仇|栾|暴|甘|斜|厉|戎|祖|武|符|刘|景|詹|束|龙|叶|幸|司|韶|郜|黎|蓟|薄|印|宿|白|怀|蒲|邰|从|鄂|索|咸|籍|赖|卓|蔺|屠|蒙|池|乔|阴|郁|胥|能|苍|双|闻|莘|党|翟|谭|贡|劳|逄|姬|申|扶|堵|冉|宰|郦|雍|郤|璩|桑|桂|濮|牛|寿|通|边|扈|燕|冀|郏|浦|尚|农|温|别|庄|晏|柴|瞿|阎|充|慕|连|茹|习|宦|艾|鱼|容|向|古|易|慎|戈|廖|庾|终|暨|居|衡|步|都|耿|满|弘|匡|国|文|寇|广|禄|阙|东|欧|殳|沃|利|蔚|越|夔|隆|师|巩|厍|聂|晁|勾|敖|融|冷|訾|辛|阚|那|简|饶|空|曾|毋|沙|乜|养|鞠|须|丰|巢|关|蒯|相|查|后|荆|红|游|竺|权|逑|盖|益|桓|公|仉|督|晋|楚|阎|法|汝|鄢|涂|钦|岳|帅|缑|亢|况|后|有|琴|归|海|墨|哈|谯|笪|年|爱|阳|佟|商|牟|佘|佴|伯|赏";
            var danXx = surname_one.split("|");
            var surname_two = "万俟|司马|上官|欧阳|夏侯|诸葛|闻人|东方|赫连|皇甫|尉迟|公羊|澹台|公冶|宗政|濮阳|淳于|单于|太叔|申屠|公孙|仲孙|轩辕|令狐|锺离|宇文|长孙|慕容|鲜于|闾丘|司徒|司空|丌官|司寇|子车|微生|颛孙|端木|巫马|公西|漆雕|乐正|壤驷|公良|拓拔|夹谷|宰父|谷梁|段干|百里|东郭|南门|呼延|羊舌|梁丘|左丘|东门|西门|南宫";
            var shuangXx = surname_two.split("|");
            $("#znjx").on("click", function () {
                console.log("asd");
                var jxtext = $("#znjxstr").val();
                jxtext = jxtext.replace("时间", "").replace("印象刘", "").replace("印象", "").replace("席位", "").replace("千古情", "")
                    .replace("贵宾席","");
                var ximing = "";
                for (var i = 0; i < jxtext.length; i++) {
                    if (danXx.indexOf(jxtext[i]) > 0) {
                        ximing = jxtext.substring(i, i + 3)
                        break;
                    }
                }
                if (ximing == "") {
                    for (var i = 0; i < jxtext.length; i += 2) {
                        var zi = jxtext.substring(i, i + 2);
                        if (shuangXx.indexOf(zi) > 0) {
                            ximing = jxtext.substring(i, i + 4)
                            break;
                        }
                    }
                }
                if (ximing != "")
                    $("#realName").val(ximing.replace(/\d/g, ""));
                //求出里面符合要求的11位数
                var est = /\d{11}/g;
                var string2 = jxtext.match(est);
                // 求string2里面符合要求的数
                for (var i = 0; i < string2.length; i++) {
                    //判断是否符合要求   ^[1]是从1开始的意思  [358]表示符合这3个数的的第二位数 \d$ 表示以数字结尾 {9}表示出现9次
                    var cox = /^[1][358]\d{9}$/;
                    if (cox.test(string2[i]) == true) {
                        $("#phone").val(string2[i]);
                    }
                }
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


