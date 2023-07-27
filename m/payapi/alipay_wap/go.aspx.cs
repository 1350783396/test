using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;
using ETicket.BLL;

namespace Com.Alipay_Wap
{
    public partial class go : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                var orderID = PubFun.QueryInt("zo");
                var sheet = ETicket.BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
                if(sheet!=null)
                {
                    this.txt_TotalFee.Text = sheet.FxTotalPrice.ToString();
                    this.txtBuyNum.Text = sheet.NUM.ToString();
                    this.txtProductName.Text = sheet.ProductName;

                    if (sheet.PayState == ETicket.Utility.PayStateEnum.已支付.ToString())
                    {
                        Response.Redirect("nopay.aspx");
                        return;
                    }
                    if (sheet.OrderStatus != ETicket.Utility.OrderStatusEnum.待付款.ToString())
                    {
                        Response.Redirect("nopay.aspx");
                        return;
                    }
                }
                else
                {
                    Response.Redirect("nopay.aspx");
                    return;
                }
            }
        }

        protected void BtnAlipay_Click(object sender, EventArgs e)
        {
             ////////////////////////////////////////////请求参数////////////////////////////////////////////
             
             var orderID = PubFun.QueryInt("zo");
             var sheet = ETicket.BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
             if (sheet== null)
             {
                 Response.Redirect("nopay.aspx");
                 return;
             }

             //商户订单号，商户网站订单系统中唯一订单号，必填
             string out_trade_no = sheet.SheetID;

            //订单名称，必填
            string subject = sheet.ProductName;

            //付款金额，必填
            string total_fee = sheet.FxTotalPrice.ToString();

            //收银台页面上，商品展示的超链接，必填
            string show_url = "http://www.yangshuo.cm/";

            //商品描述，可空
            string body = "";// WIDbody.Text.Trim();


            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.partner);
            sParaTemp.Add("seller_id", Config.seller_id);
            sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
            sParaTemp.Add("service", Config.service);
            sParaTemp.Add("payment_type", Config.payment_type);
            sParaTemp.Add("notify_url", Config.notify_url);
            sParaTemp.Add("return_url", Config.return_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("show_url", show_url);
            //sParaTemp.Add("app_pay","Y");//启用此参数可唤起钱包APP支付。
            sParaTemp.Add("body", body);
            //其他业务参数根据在线开发文档，添加参数.文档地址:https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.2Z6TSk&treeId=60&articleId=103693&docType=1
            //如sParaTemp.Add("参数名","参数值");

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            Response.Write(sHtmlText);

        }
    }
}