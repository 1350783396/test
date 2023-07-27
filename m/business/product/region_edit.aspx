<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="region_edit.aspx.cs" Inherits="ETicket.Web.business.product.region_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <!--[if IE]><link rel="stylesheet" type="text/css" href="/business/style/ie.css" /><![endif]-->
</head>
<body>
    <form id="Form2" runat="server">
    <div id="page">
        <div id="content" class="clearfix">
            <div id="main" style="position: relative">
                <br />
                <table class="formTable" style="width: 900px;">

                    <tr>
                        <td class="label" style="height:30px; vertical-align:middle;">
                            地区名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDicName" runat="server" Width="200" Height="30"></asp:TextBox>
                            &nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDicName"
                                ErrorMessage="*请输入地区名称" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                   
                   
                    <tr>
                        <td colspan="2" align="center">
                            <input type="button" runat="server"  id="iidd"/>
                            <asp:Button ID="btnAdd" runat="server" Text="保存" Width="100" CssClass="btn" OnClick="btnAdd_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Text="返回" Width="100" CssClass="btn" CausesValidation="false" PostBackUrl="~/business/user/partnertype_list.aspx" ></asp:Button>
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

