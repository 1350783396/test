<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account_out_add.aspx.cs" Inherits="ETicket.Web.business.user.account_out_add" %>


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

    <script type="text/javascript" src="/plugin/jbox-v2.3/jBox/jquery.jBox-2.3.min.js"></script>
    <script type="text/javascript" src="/plugin/jbox-v2.3/jBox/i18n/jquery.jBox-zh-CN.js"></script>
    <link type="text/css" rel="stylesheet" href="/plugin/jbox-v2.3/jBox/Skins/Blue/jbox.css" />
    
    <script  type="text/javascript">
           var SelectReValue = "";
           var SelectReText = "";
           function SelectUser() {
               $.jBox("iframe:../select_user.aspx?list=" + $("#txtJoinmanValue").val(), {
                   title: "选择分销商",
                   width: 650,
                   height: 430,
                   buttons: { '确定': true, '取消': false },
                   submit: function (v) {
                       if (v) {
                           $("#txtSelText").attr("value", SelectReText);
                           $("#txtSelValue").attr("value", SelectReValue);
                       }
                   }
               });
           }
           function ClearSel()
           {
               $("#txtSelText").attr("value", "");
               $("#txtSelValue").attr("value", "");
           }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content" class="clearfix">
                <div id="main">
                    <table class="formTable" style="width: 900px;">
                        <tr>
                            <td colspan="2">
                                <h4>积分销除</h4>
                            </td>
                        </tr>
                        <tr runat="server" id="tabUser" visible="true">
                            <td class="label"><s>*</s>选择分销商</td>
                            <td>
                                <input name='txtSelText' id="txtSelText" runat="server" style="width:300px;" readonly="readonly"/>
                                <a href="javascript:SelectUser()">选择分销商</a>
                                &nbsp;&nbsp;
                                <a href="javascript:ClearSel()">清空选择</a>
                                
                                <input name='txtSelValue' id='txtSelValue' type='hidden' runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="label"><s>*</s>销除积分数:
                            </td>
                            <td>
                                <input type="text" id="txtAccount" name="txtAccount" runat="server"/> &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="label">申请说明 :</td>
                            <td>
                                <textarea rows="4" id="txtRemarkRequest" name="txtRemarkRequest"  style="width:500px; height: 58px;" runat="server"></textarea>
                            </td>
                        </tr>
                       
                        <tr>
                            <td colspan="2" style="text-align: center;">
                              
                            <input type="submit" id="btnSave" class="btn" value="保存" style="width:100px;" onclick=" sAlert('正在保存数据。。。。。。。。。请稍等！');" runat="server" />
                                &nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Text="返回" style="width:100px;"  PostBackUrl="~/business/user/account_out_list.aspx" />
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

