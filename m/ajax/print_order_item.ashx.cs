using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// 打印是更改打印状态
    /// </summary>
    public class print_order_item : IHttpHandler
    {
        /// <summary>
        /// 更打印状态
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int orderID = PubFun.QueryInt("id");
            if (orderID <= 0)
            {
                context.Response.Write("false");
                return;
            }
            else
            {
                
                var sheet=BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
                //var cookieUser = BLL.UserBLL.Instance.GetLoginModel();
                //if (sheet.UserID==cookieUser.UserID)
                //{
                    
                //}
                sheet.IsPrint = true;
                BLL.OrderSheetBLL.Instance.UpdateObject(sheet);
                context.Response.Write("succ");
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