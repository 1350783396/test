using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WxPayAPI;

namespace ETicket.Web.WxPayAPI
{

    public partial class test : System.Web.UI.Page
    {
        public string OrderDetail = "dd";
        public static string wxJsApiParam { get; set; } //H5调起JS API参数  
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info(this.GetType().ToString(), "page load");
            if (!IsPostBack)
            {
                JsApiPay jsApiPay = new JsApiPay(this);
                jsApiPay.GetOpenidAndAccessToken();

                string openid = jsApiPay.openid; ;


                //付款金额  
                decimal total_fee = 1;
                //检测是否给当前页面传递了相关参数  
                if (string.IsNullOrEmpty(openid) || total_fee <= 0)
                {
                    Log.Error(this.GetType().ToString(), "This page have not get params, cannot be inited, exit...");
                    return;
                }

                //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数  
                jsApiPay.total_fee = (int)(total_fee * 100);//单位是分，不能有小数  
                //jsApiPay.orderId = this.OrderID.ToString();  
                //jsApiPay.siteName = this.GetConfig("SiteName");  

                //JSAPI支付预处理  
                try
                {
                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();

                    wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                      
                    Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);

                }
                catch (Exception ex)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                    Log.Error(this.GetType().ToString(), "下单失败，" + ex.Message + ";" + ex.StackTrace);
                }
            }
        }
    }
}