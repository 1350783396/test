<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myshop_2.aspx.cs" Inherits="ETicket.Web.wap.myshop_2" %>

<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
    <head>
        <title></title>
        <meta charset="utf-8">
        <meta name="viewport" content=" width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
        <link rel="stylesheet" href="/wap/cssapp/fonts/font-awesome.min.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-box.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-base.css">
        <link rel="stylesheet" href="/wap/cssapp/ui-color.css">
        <link rel="stylesheet" href="/wap/cssapp/appcan.icon.css">
        <link rel="stylesheet" href="/wap/cssapp/appcan.control.css">
        <link rel="stylesheet" href="/wap/cssapp/myshopcss/main.css">
    </head>
    <body class="um-vp bc-bg">
       
        <div class="umar-a" id="">
            <div id="listview1"  class="ubt bc-border c-wh" style="border-top:none;">

             </div>
            <div id="tabview" class="uf sc-bg ubb bc-border"></div>
            <div id="goodsList" class="ub-f1 tx-l"></div>

        </div>
        <script src="/wap/cssapp/js/appcan.js"></script>
        <script src="/wap/cssapp/js/appcan.control.js"></script>
        <script src="/wap/cssapp/js/appcan.listview.js"></script>
        <script src="/wap/cssapp/js/appcan.tab.js"></script>
    </body>
    <script>
        showHead();
        showTicket();

        appcan.ready(function () {
            appcan.initBounce();
            //showGoods();
        });

        var tabview = appcan.tab({
            selector: "#tabview",
            hasIcon: false,
            hasAnim: true,
            hasLabel: true,
            hasBadge: false,
            data: [{
                label: "景区门票",
            }, {
                label: "专线线路",
            }]
        });
        tabview.on("click", function (obj, index) {
            if (index == 0) {
                showTicket();
            }
            else if (index == 1) {
                showLine();
            }
        });

        function showTicket() {
           
            var arrData = [
                <%=ticketJosn%>
            ];

            var listData = [];
            for (var i = 0,
                len = arrData.length; i < len; i++) {
                var list = {
                    title: arrData[i].synopsis,
                    icon: arrData[i].goods,
                    describe: '<div class="ub ub-ae"><div class="sc-text-warn ">' + arrData[i].price + '</div><div class="t-a2 ulev-4 umar-l tdlt">' + arrData[i].price2 + '</div></div>',
                    note: '',
                    q: arrData[i].q
                }
                listData.push(list);
            }
            var lv1 = appcan.listview({
                selector: "#goodsList",
                type: "thickLine",
                hasIcon: true,
                hasAngle: true,
                hasSubTitle: false,
                multiLine: 1,
                hasCheckbox: false,
                align: 'left'
            });
            lv1.set(listData);
            lv1.on('click', function (ele, context, obj, subobj) {
                //alert(context.q);
                window.location.href = "/wap_view_" + context.q + ".html";
            })
        }

        function showLine() {

            var arrData = [
                <%=lineJosn%>
            ];

                    var listData = [];
                    for (var i = 0,
                        len = arrData.length; i < len; i++) {
                        var list = {
                            title: arrData[i].synopsis,
                            icon: arrData[i].goods,
                            describe: '<div class="ub ub-ae"><div class="sc-text-warn ">' + arrData[i].price + '</div><div class="t-a2 ulev-4 umar-l tdlt">' + arrData[i].price2 + '</div></div>',
                            note: '',
                            q: arrData[i].q
                        }
                        listData.push(list);
                    }
                    var lv1 = appcan.listview({
                        selector: "#goodsList",
                        type: "thickLine",
                        hasIcon: true,
                        hasAngle: true,
                        hasSubTitle: false,
                        multiLine: 1,
                        hasCheckbox: false,
                        align: 'left'
                    });
                    lv1.set(listData);
                    lv1.on('click', function (ele, context, obj, subobj) {
                        //alert(context.q);
                        window.location.href = "/wap_view_" + context.q + ".html";
                    })
        }

        function showHead()
        {
            //头部
            var shopName = "<%=cpName%>";
            var shopTel = "<%=cpTel%>";
            //第一部分
            var lv_head = appcan.listview({
                selector: "#listview1",
                type: "thickLine",
                hasIcon: true,
                hasAngle: false,
                hasSubTitle: false,
                multiLine: 1
            });

            var logoIcon = "<%=headLogo%>";
            if (logoIcon == "")
                logoIcon = "/wap/cssapp/myshopcss/myImg/mylogo.png";
            lv_head.set([{
                icon: logoIcon,
                title: '<div class="ub"><div class="umar-ar3"></div><div class=" uc-a1 uinn3 bc-text-head" style="color:#000;">' + shopName + '</div></div>',
                describe: '电话：' + shopTel
            }]);
        }

        
    </script>
</html>
