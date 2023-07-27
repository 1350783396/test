using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// chk_stock 的摘要说明
    /// </summary>
    public class chk_stock : IHttpHandler
    {
        //下单验证库存
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string startDate = PubFun.QueryString("saleDate");
            string startTime = PubFun.QueryString("saleTime");
            int productID = PubFun.QueryInt("id");
            if(productID<=0)
            { 
                context.Response.Write("有票");
                return;
            }
            //产品不存在
            var product = BLL.ProductBLL.Instance.GetEntity(p=>p.ProductID==productID);
            if(product==null)
            {
                context.Response.Write("0");
                return;
            }
            //门票
            if(product.CategoryID=="ticket")
            {
                context.Response.Write("有票");
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
                context.Response.Write("有票");
                return;
            }
            DateTime saleDate=Convert.ToDateTime(stockTime.ToString("yyyy-MM-dd 00:00:00"));
            int startH=stockTime.Hour;
            int startM=stockTime.Minute;
            var stock = BLL.ProductStockBLL.Instance.GetEntity(p => p.ProductID == productID && p.SaleDate == saleDate && p.StartH == startH && p.StartM==startM);
            if(stock==null)
            {
                context.Response.Write("有票");
                return;
            }
            else
            { 
                string value=stock.Stock.ToString();
                if (value == "")
                    value = "有票";

                context.Response.Write(value);
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