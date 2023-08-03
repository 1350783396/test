<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ETicket.Web.business.jidiao.index" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1" />
    <meta name="viewport" id="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0,maximum-scale=1.0, user-scalable=no" />
    <title>智慧游</title>
    <script type="text/javascript" src="/css/jidiao/jq183.min.js"></script>
    <script type="text/javascript" src="/css/jidiao/fun.js?&amp;ver=1.03&amp;date=20230731"></script>
    <link href="/css/jidiao/basic.css?ver=2.02" type="text/css" rel="stylesheet" />
    <link href="/css/jidiao/pages.css?ver=2.02" type="text/css" rel="stylesheet" />
    <link href="/css/jidiao/main.css" type="text/css" rel="stylesheet" />
    <link href="/css/jidiao/mob.css" type="text/css" rel="stylesheet" />
    <link href="/css/jidiao/animate.css" type="text/css" rel="stylesheet" />
    <link href="/css/jidiao/font.css" type="text/css" rel="stylesheet" />

    <link href="/css/jidiao/vipcenter.css?ver=2&amp;date=20230731" type="text/css" rel="stylesheet" />
    <link href="/css/jidiao/topflash.css" type="text/css" rel="stylesheet" />


    <link href="/css/jidiao/css.css?ver=1.2" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <style type="text/css">
        .newlist {
            overflow: hidden;
            border: 0px solid #ddd;
            height: auto;
            padding: 5px;
            background-color: #FFF;
            text-align: left;
            line-height: 1.3
        }
    </style>

    <script type="text/javascript" src="/css/jidiao/jquery.event.drag-1.5.min.js"></script>
    <script type="text/javascript" src="/css/jidiao/jquery.touchSlider.js"></script>
    <script type="text/javascript" src="/css/jidiao/topflash.js"></script>
    <script type="text/javascript" src="/css/jidiao/clipboard.min.js"></script>
    <script type="text/javascript" src="/plugin/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/getscript?v=2.0&amp;ak=EULqYtfkmgppOaPGiDHPQBpL&amp;services=&amp;t="></script>


    <link href="/css/jidiao/diary1.css?ver=7550" rel="stylesheet" type="text/css" />
    <link href="/css/jidiao/diary2.css?ver=7550" rel="stylesheet" type="text/css" />
    <link href="/css/jidiao/diary3.css?ver=7550" rel="stylesheet" type="text/css" />
    <script src="/css/jidiao/d1.js?ver=1" type="text/javascript"></script>

    <style type="text/css">
        body {
            -moz-user-select: text
        }
    </style>

</head>


<body>
    <style type="text/css">
        .STYLE2 {
            color: #006666
        }

        .STYLE3 {
            color: #FF0000
        }

        .STYLE5 {
            color: #FF00FF
        }

        .STYLE6 {
            color: #FFFFFF
        }

        .sxmore {
            overflow: hidden;
            display: inline-block;
            box-sizing: border-box;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            font-size: 12px;
            color: #333;
            vertical-align: middle;
            padding: 5px;
            border: 1px solid #CCC;
            width: 200px;
            position: relative;
            padding-right: 20px
        }

            .sxmore:after {
                content: "\f078";
                font-family: FontAwesome;
                display: block;
                margin-left: 5px;
                position: absolute;
                top: 0px;
                right: 0px;
                padding: 5px
            }

        .moreline {
            padding: 8px;
            border-bottom: 1px dashed #ddd;
        }

            .moreline:before {
                display: inline-block;
                padding: 8px;
                content: "\f096";
                font-family: FontAwesome;
            }

        .chked:before {
            content: "\f046"
        }

        .opt {
            display: inline-block;
            margin: 0px;
            height: auto;
            padding: 0px;
            text-align: center;
            background-color: #F0F0F0;
            color: #777;
            font-size: 12px;
            font-weight: 100;
            border-right: 1px solid #CCC;
            overflow: hidden;
            width: 55px;
            height: 35px;
            line-height: 35px;
            box-sizing: border-box;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .opton {
            background-color: #008AB0;
            color: #FFF;
        }

        #sxsightlines .chkonebd {
            display: inline-block;
            border: 1px solid #AAA;
            background-color: #F0F0F0;
            color: #000;
            padding: 5px 10px;
            margin: 5px
        }

        #sxsightlines .chked {
            border: 1px solid #39C;
            background-color: #39C;
            color: #fff
        }

        .selectsight {
            display: inline-block;
            padding: 4px 8px;
            text-align: center;
            border: 1px solid #999;
            margin: 3px;
            color: #999;
            border-radius: 5px
        }

        .selectsighton {
            border: 1px solid #093;
            background-color: #093;
            color: #fff
        }

        #selusers {
            display: inline-block;
            padding: 0px;
            border: 1px solid #ccc;
            border-radius: 0px;
            color: #333;
            vertical-align: middle;
            overflow: hidden;
            line-height: 1;
            width: 180px;
            height: 32px;
            line-height: 32px;
            position: relative
        }

            #selusers:after {
                display: block;
                position: absolute;
                overflow: hidden;
                width: 20px;
                height: 30px;
                top: 0px;
                right: 0px;
                content: "\f078";
                font-family: "FontAwesome"
            }

        .selsm {
            display: inline-block;
            background-color: #999;
            color: #fff;
            padding: 5px 0px;
            margin: 2px;
            border: 1px solid #999;
            width: 20px;
            text-align: center
        }

        .selusers {
            padding: 6px;
            border-bottom: 1px dashed #ddd
        }
    </style>
    <form id="form1" runat="server">
        <input type="text" class="txtcopy" />
        <input type="hidden" id="hid_my" value="" />
        <div class="indextop" style="position: fixed; left: 0px; top: 0px; width: 100%; z-index: 99999; background-color: #008AB0">
            <span class="logo">报单统计 
           </span>
        </div>
        <div id="sxbox" style="padding: 10px; background-color: #fff; position: relative; box-sizing: border-box; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; margin: 0px">
            <span style="display: inline-block">产品类型
                <asp:DropDownList CssClass="pubtxt" ID="sx_th" runat="server" Width="100">
                </asp:DropDownList>
            </span>
            <span style="display: inline-block">订单状态
            <asp:DropDownList CssClass="pubtxt" ID="sx_ykyes" runat="server" Width="100">
            </asp:DropDownList>
            </span>
            <div style="overflow: hidden; height: 1px; font-size: 0px"></div>
            <span style="display: inline-block">下单人级别
                <asp:DropDownList CssClass="pubtxt" ID="ddlUserLevel" runat="server" Width="100">
                </asp:DropDownList>
            </span>
            <span style="display: inline-block">分销商
            <asp:DropDownList CssClass="pubtxt" ID="txtSelValue" runat="server" Width="100">
            </asp:DropDownList>
            </span>
            <div style="overflow: hidden; height: 1px; font-size: 0px"></div>
            关键字<input type="text" id="sx_tckey" runat="server" class="pubtxt" style="width: 170px" placeholder="套餐/手机号/姓名/订单号" /><span id="tc1" class="cmd tckey" style="width: 40px">观众</span><span id="tc2" class="cmd tckey" style="width: 40px">贵宾</span><span id="tckeycls" class="cmd" style="width: 40px; background-color: #f63">清空</span>
            <div style="overflow: hidden; height: 1px; font-size: 0px"></div>

            <span style="display: inline-block">
                <asp:DropDownList CssClass="selectsight" ID="riQiLx" runat="server" Width="100">
                </asp:DropDownList>
            </span>
            <input type="text" id="sx_date1" runat="server" class="pubtxt" style="width: 100px" placeholder="起始日期" readonly="" />
            - 
           
            <input type="text" id="sx_date2" runat="server" class="pubtxt" style="width: 100px" placeholder="终止日期" readonly="" />
            <br />
            <div style="overflow: hidden; height: 3px; font-size: 0px"></div>
             <span style="display: inline-block">产品名称
                <asp:DropDownList CssClass="pubtxt" ID="txtProductName" runat="server" Width="100">
                </asp:DropDownList>
            </span>
            <span id="cmdyestoday" class="cmd" style="width: 50px;margin-left:20px">昨日</span><span id="cmdtoday" class="cmd" style="width: 50px">今日</span><span id="cmdtomorrow" class="cmd" style="width: 50px">明日</span>
            <div style="overflow: hidden; height: 1px; font-size: 0px"></div>
            <span style="display: inline-block">票类
            <asp:DropDownList CssClass="pubtxt" AutoPostBack="true" ID="ddlProperties" runat="server" Width="100">
            </asp:DropDownList>
            </span>
            <span style="display: inline-block">场次
            <asp:DropDownList CssClass="pubtxt" ID="txtProperties" runat="server" Width="100">
            </asp:DropDownList>
            </span>
            <div style="overflow: hidden; height: 1px; font-size: 0px"></div>
            发车时间
            <input type="text" id="txtStartTime1" runat="server" class="pubtxt" style="width: 100px" placeholder="起始日期" readonly="" />
            - 
           
            <input type="text" id="txtStartTime2" runat="server" class="pubtxt" style="width: 100px" placeholder="终止日期" readonly="" />
            - 
            <input type="text" id="txtStartHM" runat="server" class="pubtxt" style="width: 50px" placeholder="时间" readonly="" />
            <br />

            <div style="overflow: hidden; height: 3px; font-size: 0px"></div>
            <div style="text-align: center; padding-top: 3px; border-top: 1px solid #ddd">
              <asp:Button ID="btnQuery"  BackColor="#009933"  CssClass="cmd" Text="查询" Width="70" runat="server" CausesValidation="false" />
                <asp:Button ID="btnRefrech" BackColor="#000F90" Text="刷新" Width="70px" runat="server" CausesValidation="false"
                    CssClass="cmd" />
             </div>
            <div style="overflow: hidden; height: 3px; font-size: 0px"></div>
        </div>
        <div style="padding-left: 10px; background-color: #fff; position: relative; box-sizing: border-box; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; margin: 0px">
            <div>
                <span style="font-size:12px;font-weight:700">共</span>
                <asp:Literal ID="zongdanliang" runat="server"></asp:Literal>单 ；
                <span style="font-size:12px;font-weight:700">共</span>
                <asp:Literal ID="zongrenshu" runat="server"></asp:Literal>人
            </div>
            <div style="overflow: hidden; height: 5px; font-size: 0px"></div>
            <div ><span style="font-size:12px;font-weight:700">全票：</span><asp:Literal ID="quanpiao" runat="server"></asp:Literal></div>
            <div style="overflow: hidden; height: 5px; font-size: 0px"></div>
            <div><span style="font-size:12px;font-weight:700">已验票：</span><asp:Literal ID="yiyanpiao" runat="server"></asp:Literal></div>
            <div style="overflow: hidden; height: 5px; font-size: 0px"></div>
            <div ><span style="font-size:12px;font-weight:700">未验票：</span><asp:Literal ID="weiyanpiao" runat="server"></asp:Literal></div>
        </div>
        <h3 style="padding-left: 10px; background-color: #fff; position: relative; box-sizing: border-box; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; margin: 0px">
            <asp:Literal ID="lblCount" runat="server" /></h3>
        <div style="overflow: scroll; padding-left: 10px; background-color: #fff; position: relative; box-sizing: border-box; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; margin: 0px">
            <asp:Repeater ID="repList" runat="server" OnItemCommand="repList_ItemCommand">
                <HeaderTemplate>
                    <table class="table" style="width: 800px" cellspacing="0" border="1">
                        <thead>
                            <tr>
                                <!-- <th>
                                        <asp:CheckBox id="chkAll" Text="全选" runat="server" />
                                    </th>-->
                                <th>游览日期
                                </th>
                                <th>场次
                                </th>
                                <th>票类
                                </th>
                                <th>姓名
                                </th>
                                <th>电话
                                </th>
                                <th>数量
                                </th>
                                <th>总价
                                </th>
                                <th>备注
                                </th>
                                <!--<th>
                                     详情
								   </th>-->
                                <th>操作
                                </th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("palyDate","{0:d}") %>
                            <%# Eval("startTime") %>
                        </td>
                        <td>
                            <span class="STYLE3"><%# GetProperties(Eval("Properties").ToString()) %></span>
                        </td>
                        <td>
                            <%# Eval("ProductName") %>
                        </td>
                        <td>
                            <p><%# Eval("RealName") %></p>
                        </td>
                        <td>
                            <%# Eval("Phone") %>
                        </td>
                        <td>
                            <span class="STYLE1">
                                <span class="STYLE3"><%# Eval("NUM") %></span>
                            </span>
                        </td>
                        <td>
                            <%# Eval("TotalPrice") %>
                        </td>
                        <td>
                            <span class="STYLE3"><%# Eval("Memo") %></span>
                        </td>

                        <td align="left">
                            <asp:LinkButton ID="lbtnReset" CommandArgument='<%# Eval("orderid") %>' runat="server" OnClientClick="return confirm('确认要对此订单进行核销？');">核销</asp:LinkButton>
                            <a href="/dingdan/order_detail.aspx?orderid=<%# Eval("orderid") %>">
                                <span class="STYLE3">详情</span></a>
                            <asp:LinkButton ID="lbtnDuanXin" runat="server" OnClientClick="return confirm('确认要重新发送该短信？');">短信重发</asp:LinkButton>
                            <!--<asp:HyperLink ID="hyDetail" runat="server" Visible="false" CssClass="linkAction" >详情</asp:HyperLink>-->
                            <asp:HyperLink ID="hyPay" runat="server" Visible="false" CssClass="linkAction">付款</asp:HyperLink>
                            <!--<asp:HyperLink ID="hyCancel" runat="server" Visible="false" CssClass="linkAction">取消</asp:HyperLink>-->
                            <asp:HyperLink ID="hyBack" runat="server" Visible="false" CssClass="linkAction"><span class="STYLE2"><br/>退款<br/></span></asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <div style="text-align: center">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="16">
            </webdiyer:AspNetPager>
        </div>
    </form>
    <!--底部空行-->
    <script type="text/javascript" src="/css/jidiao/pub.js?ver=1.009&amp;date=20230731"></script>
    <script type="text/javascript" src="/css/jidiao/stat.js?ver=1.23"></script>
    <div id="noclose_bg" style="/* position: fixed; */width: 100%; height: 100%; left: 0px; top: 0px; background-color: rgb(51, 51, 51); z-index: 9999998; opacity: 0.8;"></div>
</body>
</html>
