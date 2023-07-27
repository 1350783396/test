<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="widget_edit.aspx.cs" Inherits="ETicket.Web.business.art.widget_edit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
    <script  type="text/javascript" src="/business/js/OpenTab.js"></script>
    <script  type="text/javascript" src="/business/js/btnReSubmit.js"></script>
    <script  type="text/javascript" src="/business/js/chkSelectAll.js"></script>
    <style type="text/css">
        .linkAction
        {
            padding:0px 5px;
        }
    </style>
    <script type="text/javascript">
        function back()
        {
            window.location.href = "widget_list.aspx?mid="+<%=NJiaSu.Libraries.PubFun.QueryString("mid")%>;
            return false
        }
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
                        <td class="label">
                            标题
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTitel" runat="server" Width="600"/><br />
                            <font color="red">*最多支持100个汉字</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            链接地址
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtHref" runat="server" TextMode="MultiLine"  Width="400"/>
                        </td>
                    </tr>
                    <tr id="trUpLoad" runat="server">
                        <td class="label">
                            标题图：
                        </td>
                        <td align="left">
                             <asp:Image ID="titleImg" runat="server" Width="740" Height="350" /><br />
                             <asp:FileUpload ID="fpUpload" runat="server" Style="width: 300px;" /> <font color="red">*重复上传将覆盖原来图片，支持jpg、gif、png图，大小建议50K以下</font>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                             <asp:Button ID="btnAdd" runat="server" Text="提交" Width="100"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
               
            </div>
        </div>
    </div>
    </form>
    <br />
    <br />
</body>
</html>
