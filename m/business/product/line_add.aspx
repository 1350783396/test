<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="line_add.aspx.cs" Inherits="ETicket.Web.business.line_add" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/business/style/theme.css" />
    <link rel="stylesheet" type="text/css" href="/business/style/nav/nav.css" />
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
                    <br />
                    <br />
                    <table class="formTable">
                        <tr>
                            <td colspan="2">
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
                        <tr runat="server" id="tabUser" visible="true">
                            <td class="label"><s>*</s>产品名称:</td>
                            <td>
                                <input type="text" id="txtProductName" name="txtProductName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label"><s>*</s>地区:
                            </td>
                            <td>
                              <asp:DropDownList ID="ddlRegion" runat="server">

                              </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <s>*</s>供应商名称:
                            </td>
                            <td>
                                <input type="text" id="txtSupplyName" name="txtSupplyName" value="票务网" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <s>*</s>产品原价：
                            </td>
                            <td>
                                <input type="text" id="txtPrimePrice" name="txtPrimePrice" runat="server"/>元
                            </td>
                        </tr>
                        <tr>
                            <td class="label">车牌号 :</td>
                            <td>
                               <textarea  id="txtCarNumber" name="txtCarNumber"  style="width:500px; height: 30px;" runat="server"></textarea>
                            </td>
                        </tr>
                        <tr >
                            <td class="label">打印小票备注 :</td>
                            <td>
                               <textarea  id="txtPrintMemo" name="txtPrintMemo"  style="width:500px; height:50px;" runat="server"></textarea>
                               <br />
                               <s>$代表单行结束</s>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                标题图：
                            </td>
                            <td align="left">
                                 <asp:Image ID="titleImg" runat="server" Width="220" Height="130"/><br />
                                 <asp:FileUpload ID="fpUpload" runat="server" Style="width: 300px;" /> <font color="red">*支持jpg、gif、png图，大小建议50K以下。</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">预订须知(网站) :</td>
                            <td>
                                <CKEditor:CKEditorControl ID="txtRulesNote"  runat="server" Language="zh-cn" EnterMode="BR" CustomConfig="/ckeditor/config.js" ></CKEditor:CKEditorControl>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">预订须知(手机) :</td>
                            <td>
                                <CKEditor:CKEditorControl ID="txtRulesNoteWap"  runat="server" Language="zh-cn" EnterMode="BR" Width="400px" Height="400px" CustomConfig="/ckeditor/config_wap.js" ></CKEditor:CKEditorControl>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">专线介绍(网站) :</td>
                            <td>
                                <CKEditor:CKEditorControl ID="txtDetail"  runat="server" Language="zh-cn" EnterMode="BR" CustomConfig="/ckeditor/config.js"></CKEditor:CKEditorControl>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">专线介绍(手机) :</td>
                            <td>
                                <CKEditor:CKEditorControl ID="txtDetailWap"  runat="server" Language="zh-cn" EnterMode="BR" Width="400px" Height="400px" CustomConfig="/ckeditor/config_wap.js" ></CKEditor:CKEditorControl>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="label">备注 :</td>
                            <td>
                               <textarea  id="txtMemo" name="txtMemo"  style="width:500px; height: 30px;" runat="server"></textarea>
                            </td>
                        </tr>
                         
                        <tr>
                            <td colspan="2" style="text-align: center;">
                              
                            <input type="submit" id="btnSave" class="btn" value="保存" style="width:100px;" runat="server"  onclick="sAlert('正在保存数据。。。。。。。。。请稍等！');"/>

                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </form>
    <br />
    <br />
</body>
</html>
