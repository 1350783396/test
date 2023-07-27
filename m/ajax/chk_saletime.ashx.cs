using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// chk_saletime 的摘要说明
    /// </summary>
    public class chk_saletime : IHttpHandler
    {
        /// <summary>
        /// 验证是否已经超过下单时间
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //context.Response.Write("false");
            //return;

            string startDate = PubFun.QueryString("startDate");
            string startTime = PubFun.QueryString("startTime");
            int productID = PubFun.QueryInt("id");
            if (productID <= 0)
            {
                context.Response.Write("true");
                return;
            }
            var product = BLL.ProductBLL.Instance.GetEntity(p=>p.ProductID==productID);
            if (product == null)
            {
                 context.Response.Write("false");
                 return;
            }
            //门票
            if (product.CategoryID == "ticket")
            {
                context.Response.Write("true");
                return;
            }
            //时间
            DateTime stockTime = new DateTime();
            try
            {
                stockTime = Convert.ToDateTime(startDate + " " + startTime);
            }
            catch
            {
                context.Response.Write("true");
                return;
            }
            var saleTime = BLL.ProductSaleTimeBLL.Instance.GetEntity(p => p.ProductID == productID && p.StartH == stockTime.Hour & p.StartM == stockTime.Minute);
            if(saleTime==null)
            {
                context.Response.Write("true");
                return;
            }
            try
            {
                //发车时间的当天+时间
                string strSaleOrderTime =startDate + " " + saleTime.LastOrderH + ":" + saleTime.LastOrderM;
                DateTime saleOrderTime = Convert.ToDateTime(strSaleOrderTime);
                if(saleOrderTime<DateTime.Now)
                {
                    context.Response.Write("false");
                    return;
                }
                else
                {
                    context.Response.Write("true");
                    return;
                }
            }
            catch
            {
                context.Response.Write("true");
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}