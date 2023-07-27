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

using NJiaSu.Libraries;

using nsChinaPay;


namespace ETicket.Web.payapi.chinapay
{
    public partial class payonline_chinapay_Chinapay_Pgreturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //YX_sql Exsql = new YX_sql();
            //ChangHope_DB db = new ChangHope_DB();
            Chinapay cpy = new Chinapay();
            string TransDate = "", MerId = "", OrdId = "", TransType = "", TransAmt = "", CuryId = "", ChkValue = "", OrderStatus = "", GateId = "", Priv1 = "";
            bool bolCheck = false;

            TransDate = Request["transdate"].Trim();
            MerId = Request["merid"].Trim();
            OrdId = Request["orderno"].Trim();
            TransType = Request["transtype"].Trim();
            TransAmt = Request["amount"].Trim();
            CuryId = Request["currencycode"].Trim(); //交易币种
            ChkValue = Request["checkvalue"].Trim();
            OrderStatus = Request["status"].Trim();
            GateId = Request["GateId"].Trim();       //支付网关号
            Priv1 = Request["Priv1"].Trim();         //商户私有域，内容是订单自增编号
            //string[] selfInfo = Priv1.Split(','); /* Priv1说明：用户名,支付类型,订单ID,订单号,支付方式ID */
            //decimal amount = Convert.ToDecimal(decimal.Parse(TransAmt) / 100);//元
            ///检验是否是银联chinapay返回的交易数据
            bolCheck = cpy.getCheck(MerId, OrdId, TransAmt, CuryId, TransDate, TransType, OrderStatus, ChkValue);
            if (bolCheck) //bolCheck=true，检测返回参数是银联发送的，进入处理流程
            {
                try
                {
                    ///********************************
                    ///这里写成功接收到银联支付成功后你自己要处理的流程，比如修改买、卖家金额等，订单状态等
                    ///********************************
                    string msg = ETicket.BLL.OrderSheetBLL.Instance.PayOnline(Priv1, ETicket.Utility.OnlinePayEnum.银联.ToString(), "", "", "");
                    Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_paySucc.GetHashCode());
                }
                catch
                {
                    Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_payFail.GetHashCode());
                }
            }
            else
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_payFail.GetHashCode());
            }
        }
    }
}