using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    /// <summary>
    /// 更改订单为支付
    /// </summary>
    public partial class post_pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StatusResult result = new StatusResult();
            string josnStr="";

            string md5 = PubFun.QueryString("md5");
            string token = PubFun.QueryString("token");
            int orderID = PubFun.QueryInt("orderid");
            int orderstatus = PubFun.QueryInt("orderstatus");
            string trade_no = PubFun.QueryString("trade_no");
            if (trade_no.Length > 50)
                trade_no = trade_no.Substring(0, 50);

            //登录
            EFEntity.User user = new EFEntity.User();
            if (token != "")
                user = BLL.UserBLL.Instance.GetUserForCookie(token);
            if (user == null || user.UserID <= 0)
            {
                result = new StatusResult();
                josnStr = "";

                result.Status = "0";
                result.StatusMsg = "请先登录系统";
                josnStr = JsonHelper.GetJson<StatusResult>(result);
                Response.Write(josnStr);
                Response.End();
                return;
            }

            NJiaSu.Libraries.LogHelper.LogResult("api_h", string.Format("orderid={0}&orderstatus={1}", orderID, orderstatus));
            result.Status = "1";
            result.StatusMsg = "OK!";
            josnStr = JsonHelper.GetJson<StatusResult>(result);
            Response.Write(josnStr);
            Response.End();
            return;

            ///-----------------------以下代码不再执行，为了安全，更改状态通过支付接口后端通知
            //比对MD5
            string sysMd5 = ApiHelper.sysMd5; 
            if (sysMd5 != md5)
            {
                result = new StatusResult();
                josnStr = "";

                result.Status = "0";
                result.StatusMsg = "授权码不对";
                josnStr = JsonHelper.GetJson<StatusResult>(result);
                Response.Write(josnStr);
                Response.End();
                return;
            }

            var orderModel = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID && p.UserID == user.UserID);
            if (orderModel == null)
            {
                result = new StatusResult();
                josnStr = "";

                result.Status = "0";
                result.StatusMsg = "订单不存在";
                josnStr = JsonHelper.GetJson<StatusResult>(result);
                Response.Write(josnStr);
                Response.End();
                return;
            }
            //内部状态
            if (orderstatus != 8)
            {
                result = new StatusResult();
                josnStr = "";

                result.Status = "10";
                result.StatusMsg = "OK";
                josnStr = JsonHelper.GetJson<StatusResult>(result);
                Response.Write(josnStr);
                Response.End();
                return;
            }

            //更改系统
            string msg = ETicket.BLL.OrderSheetBLL.Instance.PayOnline(orderModel.SheetID, ETicket.Utility.OnlinePayEnum.支付宝.ToString(), "", "", trade_no);
            result = new StatusResult();
            josnStr = "";

            result.Status = "1";
            result.StatusMsg = "OK!";
            josnStr = JsonHelper.GetJson<StatusResult>(result);
            Response.Write(josnStr);
            Response.End();
            return;

        }
    }
}