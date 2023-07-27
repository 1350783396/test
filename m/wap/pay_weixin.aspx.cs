using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

using WxPayAPI;

namespace ETicket.Web.wap
{
    public partial class pay_weixin : System.Web.UI.Page
    {
        public string zhName = "", phone = "", proName = "", num = "", fxUnit = "", fxTotal = "";
        public static string wxJsApiParam { get; set; } //H5调起JS API参数
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 保存参数到Session
            if (PubFun.QueryString("freq") != "")
            {
                var orderID = PubFun.QueryInt("zo");
                var qContet = PubFun.QueryString("zq");

                if (orderID <= 0)
                {
                    return;
                }
                if (qContet == "")
                {
                    return;
                }

                int productID = 0;
                int userID = 0;
                WapHelper.GetUserID_ProductID("zq", out userID, out productID);
                if (productID == 0 && userID == 0)
                {
                    return;
                }
                Session["pay_orderID"] = orderID;
            }
            #endregion

            JsApiPay jsApiPay = new JsApiPay(this);
            jsApiPay.GetOpenidAndAccessToken();

            var orderIDStr = Session["pay_orderID"] == null ? "0" : Session["pay_orderID"].ToString();
            var orderIDValue = int.Parse(orderIDStr);
            if(orderIDValue<=0)
            {
                return;
            }
            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderIDValue);
            //支付金额，转换为分
            var total_fee = sheet.FxTotalPrice.Value * 10 * 10;
            //total_fee = 1;

            jsApiPay.total_fee = Convert.ToInt32(total_fee);

            //JSAPI支付预处理
            try
            {
                string uniBody = "", uniAttach = "", uniTag = "", out_OrderID = "";
                uniBody = uniAttach = uniTag = sheet.ProductName;
                //out_OrderID = PubFun.GetRandNumString(5) + "_" + sheet.OrderID.ToString();
                out_OrderID =sheet.SheetID.ToString();
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(uniBody, uniAttach, out_OrderID, uniTag);
                //获取H5调起JS API参数
                wxJsApiParam = jsApiPay.GetJsApiParameters();
                Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                //在页面上显示订单信息
                //Response.Write("<span style='color:#00CD00;font-size:20px'>订单ID：" + Session["pay_orderID"] + "</span><br/>");
                //Response.Write("<span style='color:#00CD00;font-size:20px'>订单详情：</span><br/>");
                //Response.Write("<span style='color:#00CD00;font-size:20px'>" + unifiedOrderResult.ToPrintStr() + "</span>");
            }
            catch (Exception ex)
            {
                //Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                Log.Debug(this.GetType().ToString(), "JSAPI支付预处理发生错误。" + ex.ToString());
            }

            proName = sheet.ProductName;
            zhName = sheet.RealName;
            phone = sheet.Phone;
            num = sheet.NUM == null ? "" : sheet.NUM.Value.ToString();
            fxUnit = sheet.FxUnitPrice == null ? "" : sheet.FxUnitPrice.ToString();
            fxTotal = sheet.FxTotalPrice == null ? "" : sheet.FxTotalPrice.ToString();


        }
    }
}