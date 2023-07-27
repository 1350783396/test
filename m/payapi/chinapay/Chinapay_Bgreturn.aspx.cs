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

namespace ETicket.Web.payapi.chinapay
{
    public partial class payonline_chinapay_Chinapay_Bgreturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            Priv1 = Request["Priv1"].Trim();         //商户私有域

            string status=Request["status"].Trim();

           // string[] selfInfo = Priv1.Split(','); /* Priv1说明：用户名,支付类型,订单ID,订单号,支付方式ID */
            ///检验是否是银联chinapay返回的交易数据
            bolCheck = cpy.getCheck(MerId, OrdId, TransAmt, CuryId, TransDate, TransType, OrderStatus, ChkValue);
            if (bolCheck)
            {
                if (status == "1001")
                {

                    ///********************************
                    ///这里写成功接收到银联支付成功后你自己要处理的流程，比如修改买、卖家金额等，订单状态等
                    ///********************************
                    string msg = ETicket.BLL.OrderSheetBLL.Instance.PayOnline(Priv1,ETicket.Utility.OnlinePayEnum.银联.ToString(), "", "", "");
                }
                //Response.Write("<script>alert('后台接收到应答');</script>");
            }
            else
            {
                //Response.Write("<script>alert('后台没有接收到应答');</script>");
            }
        }
    }
}