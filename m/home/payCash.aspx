<%@ Page Title="" Language="C#" MasterPageFile="~/home/site.Master" AutoEventWireup="true" CodeBehind="payCash.aspx.cs" Inherits="ETicket.Web.home.payCash" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        body {
            padding-top: 60px;
            padding-bottom: 40px;
        }
        .tip_1 {
            background: url("bootstrap/img/wcdd.gif") no-repeat scroll left 10px rgba(0, 0, 0, 0);
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
                        &nbsp; &nbsp; 支付方式：现金支付
                    </p>
                </div>
                <div class="tip_6">
                   还差一步,请到财务处交付现金。（如40分钟内未付清款项,订单会被自动取消)
                </div>
            </div></div>
</asp:Content>
