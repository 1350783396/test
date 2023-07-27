<%@ Page Title="" Language="C#" MasterPageFile="~/home/site.Master" AutoEventWireup="true" CodeBehind="payOnline.aspx.cs" Inherits="ETicket.Web.home.payOnline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        body {
            padding-top: 60px;
            padding-bottom: 40px;
        }
        .tip_1 {
            background: url("../bootstrap/img/wcdd.gif") no-repeat scroll left 10px rgba(0, 0, 0, 0);
            font-size: 16px;
            font-weight: bold;
            line-height: 2em;
            padding: 10px 10px 10px 50px;
        }
        .tip_5 {
            margin-bottom: 3px;
        }
        .tip_6 {
            font-size: 16px;
            font-weight: bold;
            margin: 20px 0 10px;
        }
        .banks dt {
            clear: both;
            margin-bottom: 10px;
        }
        .banks dd {
            float: left;
            height: 65px;
            margin-bottom: 0;
            overflow: hidden;
            text-align: center;
            width:200px;
        }
        td.title {
            vertical-align: middle;
           line-height:40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceMain" runat="server">
    <div class="row" style="background-color: #fff;">
            <div class="span12">
                <div class="tip_1"> 订单已提交，请尽快付款！</div>
                <div class="tip_5">
                    <p>
                        您的订单号：
                        <strong style="color:red;">135876278</strong>
                        应付金额：
                        <strong style="color:red;">4057.00</strong>
                        元
                        &nbsp; &nbsp; 支付方式：在线支付
                    </p>
                </div>
                <div class="tip_6">
                   还差一步,请立即支付(请您在40分钟内付清款项,否则订单会被自动取消)
                </div>
            </div>
           
            <div class="span12">
                <div class="tip_3">
                    <dl class="banks">
                        <dt style="padding: 8px 10px;">请选择银行或机构支付：</dt>
                        <dd id="ddUnionpay">
                            <a target="_blank" href="#">
                                <img alt="银联支付" title="银联支付" src="../bootstrap/img/payment/Unionpay.gif">
                            </a><br>
                            <a href="#none">支持大部分银行</a>
                        </dd>
                        <dd>
                            <a target="_blank" href="#">
                                <img alt="中国工商银行" title="中国工商银行" src="../bootstrap/img/payment/gs.gif">
                            </a>
                        </dd>
                        <dd>
                            <a target="_blank" href="#">
                                <img alt="中国建设银行" title="中国建设银行" src="../bootstrap/img/payment/js.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="招商银行" title="招商银行" src="../bootstrap/img/payment/zs.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="交通银行" title="交通银行" src="../bootstrap/img/payment/jt.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="中国农业银行" title="中国农业银行" src="../bootstrap/img/payment/ny.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="广发银行" title="广发银行" src="../bootstrap/img/payment/gf.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="兴业银行" title="兴业银行" src="../bootstrap/img/payment/xy.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="中国光大银行" title="中国光大银行" src="../bootstrap/img/payment/gd.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="中信银行" title="中信银行" src="../bootstrap/img/payment/zx.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="浦发银行" title="浦发银行" src="../bootstrap/img/payment/pf.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="中国银行" title="中国银行" src="../bootstrap/img/payment/zg.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="深圳发展银行" title="深圳发展银行" src="../bootstrap/img/payment/sz.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="民生银行" title="民生银行" src="../bootstrap/img/payment/ms.gif">
                            </a>
                        </dd>
                        <dd>
                            <a target="_blank" href="#">
                                <img alt="北京银行" title="北京银行" src="../bootstrap/img/payment/bj.gif">
                            </a>
                        </dd>
                        <dd>
                            <a target="_blank" href="#">
                                <img alt="华夏银行" title="华夏银行" src="../bootstrap/img/payment/hx.gif">
                            </a>
                        </dd>
                        <dd>
                            <a  target="_blank" href="#">
                                <img alt="中国邮政银行" title="中国邮政银行" src="../bootstrap/img/payment/post.gif">
                            </a>
                        </dd>
                        <dd>
                            <a target="_blank" href="#">
                                <img alt="渤海银行" title="渤海银行" src="../bootstrap/img/payment/bohai.gif">
                            </a>
                        </dd>
                        <dd>
                            <a target="_blank" href="#">
                                <img alt="平安银行" title="平安银行" src="../bootstrap/img/payment/pingan.gif">
                            </a>
                        </dd>
                        <dd>
                            <a target="_blank" href="#">
                                <img alt="南京银行" title="南京银行" src="../bootstrap/img/payment/nj.gif">
                            </a>
                        </dd>
                    </dl>
                    <dl class="banks">
                        <dt style="padding: 8px 10px;">请选择以下支付平台支付：</dt>
                        <dd>
                            <a href="#"
                               target="_blank">
                                <img align="middle" title="支付宝" alt="支付宝" src="../bootstrap/img/payment/zfb.gif">
                            </a><br>

                        </dd>
                        <!--
                        <dd>
                            <a  href="#"
                               target="_blank">
                                <img align="middle" title="中国移动手机支付" alt="中国移动手机支付" src="../bootstrap/img/payment/mobile.gif">
                            </a><br>
                            
                        </dd>
                        <dd>
                            <a  href="#"
                               target="_blank">
                                <img align="middle" title="汇付天下" alt="汇付天下" src="../bootstrap/img/payment/Chinapnr.gif">
                            </a><br>
                        </dd>
                        <dd>
                            <a  href="#"
                               target="_blank">
                                <img title="快钱" alt="快钱" src="../bootstrap/img/payment/kq.gif">
                            </a><br>
                        </dd>
                        <dd>
                            <a  href="#"
                               target="_blank">
                                <img align="middle" title="嗖付" alt="嗖付" src="../bootstrap/img/payment/umpay.gif">
                            </a><br>
                        </dd>
                        <dd>
                            <a  href="#" target="_blank">
                                <img align="middle" title="财付通" alt="财付通" src="../bootstrap/img/payment/Tenpay.gif">
                            </a><br>
                        </dd>
                        -->
                    </dl>
                </div>
              
            </div>
        </div>
</asp:Content>
