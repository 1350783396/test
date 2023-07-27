<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="set_price_edit.aspx.cs" Inherits="ETicket.Web.business.partner.set_price_edit" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
     <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script  type="text/javascript" src="/js/chkSelectAll.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content" class="clearfix">
                <div id="main">
                    <table class="formTable" >
                        <tr style="height:30px;">
                            <td colspan="2">
                                <h2>修改我的售价</h2>
                            </td>
                        </tr>
                        <tr style="height:30px;">
                            <td class="label">产品类型 :</td>
                            <td style="width:30px;">
                                <asp:Literal ID="litCategory" runat="server"/>
                            </td>
                        </tr>
                        <tr style="height:30px;">
                            <td class="label">产品名称 :</td>
                            <td style="width:30px;">
                                <asp:Literal ID="litProductName" runat="server"/>
                            </td>
                        </tr>
                        <tr style="height:30px;">
                            <td class="label">进货价格 :</td>
                            <td >
                               <asp:Literal ID="litPrice" runat="server" />
                            </td>
                        </tr>
                        <tr style="height:30px;">
                            <td class="label">限定最低分销售价 :</td>
                            <td >
                                <asp:Literal ID="litMiniPrice" runat="server" />
                            </td>
                        </tr>
                        <tr style="height:30px;">
                            <td class="label">我的售价:</td>
                            <td >
                               <input type="text" id="txtMyPrice" name="txtMyPrice"  runat="server" style="width:150px;"/>
                            </td>
                        </tr>
                       
                      
                       
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <div style="color:red">注：差价通过积分返回，可联系客服销除积分提现。如：进货价为20，售价为50，每扫码卖出一张票赚30元</div>
                                <br />
                                <input type="submit" id="btnSave" class="btn" value="保存" style="width:100px;" onclick="sAlert('正在保存数据。。。。。。。。。请稍等！');" runat="server" />
                                &nbsp;&nbsp;
                            
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
    <br />
    <br />
</body>
</html>
