using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.payapi
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            string dir = "";

            string type = PubFun.QueryString("type");
            int orderID = PubFun.QueryInt("id");
            string gateid = PubFun.QueryString("gateId");

            switch (type)
            {
                case "chinapay":
                    dir = "chinapay";
                    break;
                case "alipay":
                    dir = "alipay_direct";
                    break;
            }
            if (dir=="")
            {
                Response.Write("参数错误，无法支付！");
                return;
            }
            if (cookiesUser==null)
            {
                Response.Write("请先登录系统！");
                return;
            }
            if (orderID<=0)
            {
                Response.Write("订单ID非法！");
                return;
            }
            EFEntity.OrderSheet sheet =BLL.OrderSheetBLL.Instance.GetEntity(p=>p.OrderID==orderID);
            if (sheet == null)
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
            var payLog = BLL.OrderPayLogBLL.Instance.GetEntity(p => p.OrderID == orderID);
            if(payLog!=null)
            {
                if(payLog.PayStatus==ETicket.Utility.PayStateEnum.已支付.ToString())
                {
                    Response.Write("订单支付状态为：" + payLog.PayStatus + ",不能在支付！");
                    return;
                }

                payLog.PayStatus = ETicket.Utility.PayStateEnum.未支付.ToString();
                payLog.PayStatusUpdateTime = DateTime.Now;
                payLog.OP_UserID = cookiesUser.UserID;
                payLog.OP_UserName = cookiesUser.UserName;
                ETicket.BLL.OrderPayLogBLL.Instance.UpdateObject(payLog);
            }
            else
            {
                payLog = new EFEntity.OrderPayLog();
                payLog.OrderID = sheet.OrderID;
                payLog.SheetID = sheet.SheetID;
                payLog.PayAmount = sheet.TotalPrice;
                payLog.PayType = sheet.PayType;
                payLog.PayStatus = ETicket.Utility.PayStateEnum.未支付.ToString();
                payLog.PayStatusUpdateTime = DateTime.Now;
                payLog.OP_UserID = cookiesUser.UserID;
                payLog.OP_UserName = cookiesUser.UserName;
                payLog.AddTime = payLog.PayStatusUpdateTime;
                ETicket.BLL.OrderPayLogBLL.Instance.AddObject(payLog);
            }
            Response.Redirect(dir + "/go.aspx?id=" + orderID + "&gateid=" + gateid);
            
        }
    }
}