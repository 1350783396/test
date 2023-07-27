using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public class PayBase:LoginBase
    {
        protected EFEntity.OrderSheet sheet = null;
        protected override void OnLoad(EventArgs e)
        {
            //先验证登录
            base.OnLoad(e);

            int orderID = PubFun.QueryInt("id");
            if(orderID<=0)
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_noexits.GetHashCode());
            }
            sheet = BLL.OrderSheetBLL.Instance.GetEntity(p=>p.OrderID==orderID);
            if (sheet==null)
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_noexits.GetHashCode());
            }
            if (sheet.UserID != base.CookiesUser.UserID)//不是自己的订单
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_noexits.GetHashCode());
            }
            if (sheet.PayState != ETicket.Utility.PayStateEnum.未支付.ToString())
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_ispay.GetHashCode());
            }
            if(sheet.OrderStatus != ETicket.Utility.OrderStatusEnum.待付款.ToString())
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_ispay.GetHashCode());
            }
            
        }
    }
}