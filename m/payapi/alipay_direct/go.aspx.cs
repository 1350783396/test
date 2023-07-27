using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Com.Alipay;

/// <summary>
/// 功能：即时到账交易接口接入页
/// 版本：3.3
/// 日期：2012-07-05

/// /////////////////注意///////////////////////////////////////////////////////////////
/// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
/// 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
/// 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
/// 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
/// 
/// 如果不想使用扩展功能请把扩展功能参数赋空值。
/// </summary>


using ETicket.EFEntity;
using ETicket.BLL;
using ETicket.Utility;
using NJiaSu.Libraries;

namespace Com.Alipay
{
    public partial class go : System.Web.UI.Page
    {
        OrderPayLog payLog = null;
        OrderSheet sheet = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            CookiesUser cookiesUser = UserBLL.Instance.GetLoginModel();
            int orderID = PubFun.QueryInt("id");
            payLog = OrderPayLogBLL.Instance.GetEntity(p => p.OrderID == orderID);
            sheet = OrderSheetBLL.Instance.GetEntity(p=>p.OrderID==payLog.OrderID);

            #region 再验证
            if (sheet == null)
            {
                Response.Write("订单ID非法！");
                return;
            }
            if (payLog==null)
            {
                Response.Write("订单ID非法！");
                return;
            }
            if (sheet.UserID != cookiesUser.UserID)
            {
                Response.Write("订单ID非法！");
                return;
            }
            if (sheet.PayType != "在线支付")
            {
                Response.Write("订单支付类型为：" + sheet.PayType + ",不能使用在线支付！");
                return;
            }
            if (sheet.PayState==ETicket.Utility.PayStateEnum.已支付.ToString())
            {
                Response.Write("订单支付状态为：" + sheet.PayState + ",不能在支付！");
                return;
            }
            if (sheet.OrderStatus!=ETicket.Utility.OrderStatusEnum.待付款.ToString())
            {
                Response.Write("订单状态为 " + sheet.OrderStatus + ",已不处于可支付状态！");
                return;
            }
            if (payLog != null)
            {
                if (payLog.PayStatus == ETicket.Utility.PayStateEnum.已支付.ToString())
                {
                    Response.Write("订单支付状态为：" + sheet.PayState + ",不能在支付！");
                    return;
                }
            }
            #endregion

            //支付
            GoPay();
        }

        protected void GoPay()
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = "http://www.yangshuo.cm/payapi/alipay_direct/notify_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = "http://www.yangshuo.cm/payapi/alipay_direct/return_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //卖家支付宝帐户
            //string seller_email = "530678888@qq.com";
            string seller_email = "530678888@qq.com";
            //必填

            //商户订单号
            string out_trade_no = payLog.SheetID.ToString();
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = sheet.ProductName;
            //必填

            //付款金额
            string total_fee = sheet.TotalPrice.ToString();
            //必填

            //订单描述
            string body = sheet.ProductName+"，购买数量："+sheet.NUM;
            //商品展示地址
            string show_url = "";
            //需以http://开头的完整路径，例如：http://www.xxx.com/myorder.html

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //若要使用请调用类文件submit中的query_timestamp函数

            //客户端的IP地址
            string exter_invoke_ip = "";
            //非局域网的外网IP地址，如：221.0.0.1


            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            Response.Write(sHtmlText);

        }
    }
}