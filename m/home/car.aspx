<%@ Page Title="" Language="C#" MasterPageFile="~/home/site.Master" AutoEventWireup="true" CodeBehind="car.aspx.cs" Inherits="ETicket.Web.home.car" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceMain" runat="server">
    <div class="row" style="background-color: #fff;">
            <div class="span12"><h2>丰岩台专线</h2></div>
            <div class="span6" >
                <h4>预定须知：</h4>
                <p style="min-height:120px;">
                1、此滑雪券只限非周末、非法定假日使用；2、此滑雪券包括滑雪场门票、4小时滑雪（含滑雪用具、租用费及缆车、拖牵和雪道使用费）；3、使用方法：凭门票副券入场，在票务中心散客区办理滑雪手续，交纳押金300元，请保管好押金单，结账时凭押金单退还。4、此券仅限一人使用，一经售出概不退换。
                </p>
                <h4>产品详情：</h4>
                <p style="min-height:120px;">自驾路线：1.沿京顺路至枯柳树环岛右转至平谷方向,达平谷城关过渔阳酒店第二个红绿灯平谷至三河方向(平三路口)右转,直行既到。公交路线：东直门乘918路(10分钟一班)世纪广场下车,换乘出租车到雪场
                </p>

            </div>
            <div class="span6">

                <table>
                    <tr style="height:40px;">
                        <td class="title">
                            发车时间：
                        </td>
                        <td>
                            2014-06-11 11:00
                        </td>
                    </tr>
                    <tr>
                        <td class="title">上车地点：</td>
                        <td><select><option>请选择</option></select></td>
                    </tr>
                    <tr>
                        <td class="title">单价：</td>
                        <td>￥100</td>
                    </tr>
                    <tr>
                        <td class="title">购买数量：</td>
                        <td style=" vertical-align:middle;"><input type="text" style="width: 50px;" /><span style="margin-left:5px;">库存：100</span></td>
                    </tr>
                    <tr>
                        <td class="title">总价：</td>
                        <td><h4 style="color:red">￥100</h4></td>
                    </tr>
                    <tr>
                        <td class="title">
                            支付方式：
                        </td>
                        <td>
                            <select>
                                <option selected value="在线支付">在线支付</option>
                                <option value="积分支付">积分支付</option>
                                <option value="现金支付">现金支付</option>
                            </select>
                        </td>
                    </tr>
                   
                    <tr>
                        <td class="title">
                            取票方式：
                        </td>
                        <td>
                            <select>
                                <option selected value="二维码">二维码</option>
                                <option value="身份证">身份证</option>
                                <option value="纸质">纸质</option>
                            </select>

                        </td>
                    </tr>
                    <tr>
                        <td class="title">姓名：</td>
                        <td><input type="text" /></td>
                    </tr>
                    <tr>
                        <td class="title">身份证：</td>
                        <td><input type="text" /></td>
                    </tr>
                    <tr>
                        <td class="title">手机号码： </td>
                        <td><input type="text" /></td>
                    </tr>
                   
                    <tr>
                        <td colspan="2" style="text-align:right;"><button class="btn btn-primary" type="submit">提交订单</button></td>
                    </tr>
                </table>
            </div>
        </div>
</asp:Content>
