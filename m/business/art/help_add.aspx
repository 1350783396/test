<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="help_add.aspx.cs" Inherits="ETicket.Web.business.art.help_add" %>

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
            window.location.href = "help_list.aspx?mid="+<%=NJiaSu.Libraries.PubFun.QueryInt("mid")%>;
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
                 <asp:Button ID="Button1" Text="返回管理" Width="200"  runat="server" CausesValidation="false"
                    CssClass="btn" OnClientClick="return back()" />
                <br />
                <br />
                <table class="formTable" >
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <h2><asp:Literal ID="litModeleName" runat="server"></asp:Literal></h2>
                        </td>
                    </tr>
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
                            内容
                        </td>
                        <td align="left">
                         <CKEditor:CKEditorControl ID="txtArtContent" runat="server" Language="zh-cn" EnterMode="BR"></CKEditor:CKEditorControl>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                             <asp:Button ID="btnAdd" runat="server" Text="提交" Width="100"></asp:Button>
                    &nbsp; &nbsp; &nbsp; &nbsp;
                  
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