using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using nsChinaPay; 

using ETicket.EFEntity;
using ETicket.BLL;
using ETicket.Utility;
using NJiaSu.Libraries;



namespace ETicket.Web.payapi.chinapay
{
    public partial class go : System.Web.UI.Page
    {
        OrderPayLog payLog = null;
        OrderSheet sheet = null;
        CookiesUser cookiesUser = UserBLL.Instance.GetLoginModel();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            int orderID = PubFun.QueryInt("id");
            payLog = OrderPayLogBLL.Instance.GetEntity(p => p.OrderID == orderID);
            sheet = OrderSheetBLL.Instance.GetEntity(p => p.OrderID == payLog.OrderID);

            #region 再验证
            if (sheet == null)
            {
                Response.Write("订单ID非法！");
                return;
            }
            if (payLog == null)
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
            if (sheet.PayState == ETicket.Utility.PayStateEnum.已支付.ToString())
            {
                Response.Write("订单支付状态为：" + sheet.PayState + ",不能在支付！");
                return;
            }
            if (sheet.OrderStatus != ETicket.Utility.OrderStatusEnum.待付款.ToString())
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

            GoPay();
        }

        protected void GoPay()
        {

            Chinapay cpy = new Chinapay();
            //获取传递给银联chinapay的各个参数-----------------------------------------------
            //string cpyUrl = "http://payment-test.chinapay.com/pay/TransGet"; //测试地址，测试的时候用这个地址，应用到网站时用下面那个地址
            string cpyUrl = "https://payment.chinapay.com/pay/TransGet";   //支付地址                         
            string cpyMerId = "808080201305492"; //ChinaPay统一分配给商户的商户号，15位长度，必填
            string cpyOrdId = "05492";           //商户提交给ChinaPay的交易订单号，订单号的第五至第九位必须是商户号的最后五位，即“12345”；16位长度，必填
            string cpyTransAmt = "";             //订单交易金额，12位长度，左补0，必填,单位为分，000000001234 表示 12.34 元
            string cpyCuryId = "156";            //订单交易币种，3位长度，固定为人民币156，必填
            string cpyTransDate = "";            //订单交易日期，8位长度，必填，格式yyyyMMdd
            string cpyTransType = "0001";        //交易类型，4位长度，必填，0001表示消费交易，0002表示退货交易
            string cpyVersion = "20040916";      //支付接入版本号，808080开头的商户用此版本，必填,另一版本为"20070129"
            string cpyBgRetUrl = "http://www.tianshuntour.com/payapi/chinapay/Chinapay_Bgreturn.aspx";   //后台交易接收URL，为后台接受应答地址，用于商户记录交易信息和处理，对于使用者是不可见的，长度不要超过80个字节，必填
            string cpyPageRetUrl = "http://www.tianshuntour.com/payapi/chinapay/Chinapay_Pgreturn.aspx"; //页面交易接收URL，为页面接受应答地址，用于引导使用者返回支付后的商户网站页面，长度不要超过80个字节，必填
            string cpyGateId = ""; //支付网关号，可选，参看银联网关类型，如填写GateId（支付网关号），则消费者将直接进入支付页面，否则进入网关选择页面
            string cpyPriv1 =  sheet.SheetID.ToString();  //商户私有域，长度不要超过60个字节,商户通过此字段向Chinapay发送的信息，Chinapay依原样填充返回给商户
            /* Priv1说明：用户ID,订单ID,订单号,支付方式ID */

            cpyGateId=PubFun.QueryString("gateId");

            string strCountMoney = "", strOrdid = ""; //sql语句，实付款，订单自增id
            strCountMoney = sheet.TotalPrice.Value.ToString("f2");
            strOrdid = sheet.OrderID.ToString();

            ///订单时间必须为当前时间，且格式必须为yyyyMMdd
            cpyTransDate = DateTime.Now.ToString("yyyyMMdd");// Convert.ToDateTime(DateTime.Now).GetDateTimeFormats('D')[1].ToString().Trim().Replace("-", "");
            ///订单号，16位长度，左补0，0000 02449 0000000
            int intO = strOrdid.Length;
            for (int i = 0; i < 7 - intO; i++)
            {
                strOrdid = "0" + strOrdid;
            }
            cpyOrdId = "0000" + cpyOrdId + strOrdid; //银联支付id="0000"+银联商户后五位+补0后的订单自增id
            ///订单交易金额，12位长度，左补0
            strCountMoney = strCountMoney.Replace(".", "");
            int intM = strCountMoney.Length;
            for (int i = 0; i < 12 - intM; i++)
            {
                strCountMoney = "0" + strCountMoney;
            }
            cpyTransAmt = strCountMoney; //获取传递的实付款

            //////////////////////////////////////////////////////////////////////
            string strChkValue = ""; //256字节长的ASCII码,此次交易所提交的关键数据的数字签名，必填
            strChkValue = cpy.getSign(cpyMerId, cpyOrdId, cpyTransAmt, cpyCuryId, cpyTransDate, cpyTransType);

            if (strChkValue != "")
            {
                Response.Write("<form name='chinapayForm' method='post' action='" + cpyUrl + "'>");         //支付地址
                Response.Write("<input type='hidden' name='MerId' value='" + cpyMerId + "' />");            //商户号
                Response.Write("<input type='hidden' name='OrdId' value='" + cpyOrdId + "' />");            //订单号
                Response.Write("<input type='hidden' name='TransAmt' value='" + cpyTransAmt + "' />");      //支付金额
                Response.Write("<input type='hidden' name='CuryId' value='" + cpyCuryId + "' />");          //交易币种
                Response.Write("<input type='hidden' name='TransDate' value='" + cpyTransDate + "' />");    //交易日期
                Response.Write("<input type='hidden' name='TransType' value='" + cpyTransType + "' />");    //交易类型
                Response.Write("<input type='hidden' name='Version' value='" + cpyVersion + "' />");        //支付接入版本号
                Response.Write("<input type='hidden' name='BgRetUrl' value='" + cpyBgRetUrl + "' />");      //后台接受应答地址
                Response.Write("<input type='hidden' name='PageRetUrl' value='" + cpyPageRetUrl + "' />");  //为页面接受应答地址
                Response.Write("<input type='hidden' name='GateId' value='" + cpyGateId + "' />");          //支付网关号
                Response.Write("<input type='hidden' name='Priv1' value='" + cpyPriv1 + "' />");            //商户私有域，这里将订单自增编号放进去了
                Response.Write("<input type='hidden' name='ChkValue' value='" + strChkValue + "' />");      //此次交易所提交的关键数据的数字签名
                Response.Write("<script>");
                Response.Write("document.chinapayForm.submit();");
                Response.Write("</script></form>");
            }
            else
            { 
                Response.Write("读取关键数据数字签名MerPrK.key错误！");
            }
        }
    }
}