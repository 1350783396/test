<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="line_price.aspx.cs" Inherits="ETicket.Web.business.product.line_price" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <link rel="stylesheet" type="text/css" href="/business/style/nav/nav.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
    <style>
        .error
        {
            color:red;
        }
    </style>
    
    <script  type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script  type="text/javascript" src="/js/common.js"></script>
    <script  type="text/javascript" src="/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/js/btnReSubmit.js"></script>
    <script  type="text/javascript" src="/js/chkSelectAll.js"></script>

    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>

    <script type="text/javascript">
        function check() {
            var s = document.getElementById("txtCost_1");
            alert(s.value);
            if (s.value == "") {
                alert('请输入用户名');
                return false;
            }
            return true;
        }

      
        $(function () {
            jQuery.validator.addMethod("isPrice", function (value, element) {
                var v1= this.optional(element) || (/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1})?$/.test(value));
                var v2 = this.optional(element) || (/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){2})?$/.test(value));
                return v1 || v2;
            }, "价格格式不对。注：小数位最多支持2位");

            //用户注册页字段验证
            $("#form1").validate({
                submitHandler: function (form) {
                    sAlert('正在保存数据。。。。。。。。。请稍等！');
                    form.submit();
                },
                rules: {
                   <%=ruleResult%>
                },
                messages: {
                    <%=msgResult%>
                }
            });
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content" class="clearfix">
                <div id="main">
                    <br />
                    <br />
                    <table class="formTable" >
                        <tr>
                            <td colspan="8">
                              <div>
                                <div id="divProduct" runat="server"><asp:HyperLink ID="hyProduct" runat="server" Text="专线基本信息" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div id="divPrice" runat="server"><asp:HyperLink ID="hyPrice" runat="server" Text="价格" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div  id="divTickTime" runat="server"><asp:HyperLink ID="hyTickTime" runat="server" Text="销售时间" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div  id="divStock" runat="server"><asp:HyperLink ID="hyStock" runat="server" Text="库存" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div  id="divAddress" runat="server"><asp:HyperLink ID="hyAddress" runat="server" Text="上车地点" /></div>
                                <div class="jiantou"><img src="/business/style/nav/jiantou.jpg" width="15" height="14"/></div>
                                <div id="divPublish" runat="server"><asp:HyperLink ID="hyPublish" runat="server" Text="发布" /></div>
                                <div style="clear: both"></div>
                             </div>
                            <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" style="height:20px;">
                                
                            </td>
                        </tr>
                        <%=DataHtml %>
                        <tr>
                            <td colspan="8" style="text-align: center;">
                                <input type="submit" id="btnSave" class="btn" value="保存" style="width:100px;"/>
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