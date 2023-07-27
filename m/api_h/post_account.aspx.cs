using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    public partial class post_account : System.Web.UI.Page
    {
        //积分支付受理接口
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = PubFun.QueryString("token");
            EFEntity.User user = new EFEntity.User();
            if (token != "")
                user = BLL.UserBLL.Instance.GetUserForCookie(token);

            //返回结果
            JsonResult<Order> jsonModel = new JsonResult<Order>();
            string jsonStr = "";

            //用户登录
            if (user==null||user.UserID == 0)
            {
                //生成json
                jsonModel = new JsonResult<Order>();
                jsonModel.Status = "0";
                jsonModel.Msg = "请先登录系统后再提交";
                jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            int orderID = PubFun.QueryInt("orderid");
            var sheetModel = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
            if (sheetModel == null)
            {
                jsonModel = new JsonResult<Order>();
                jsonModel.Status = "0";
                jsonModel.Msg = "订单不存在，无法完成支付！";
                jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if (sheetModel.PayType != "积分支付")
            {
                jsonModel = new JsonResult<Order>();
                jsonModel.Status = "0";
                jsonModel.Msg = "该订单不是使用积分支付,无法完成支付！";
                jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if (user.Account < sheetModel.TotalPrice)
            {
                jsonModel = new JsonResult<Order>();
                jsonModel.Status = "0";
                jsonModel.Msg =string.Format("目前账号积分{0}不足支付订单金额{1}", user.Account, sheetModel.TotalPrice);
                jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }

            string msg = BLL.OrderSheetBLL.Instance.PayAccount(orderID, user.UserID);
            if (msg != "")
            {
                jsonModel = new JsonResult<Order>();
                jsonModel.Status = "0";
                jsonModel.Msg = msg;
                jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            else
            {
                jsonModel = new JsonResult<Order>();
                jsonModel.Status = "1";
                jsonModel.Msg = "支付成功";
                jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
            }
        }
    }
}