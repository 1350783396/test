using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// chk_account 的摘要说明
    /// </summary>
    public class chk_account : IHttpHandler
    {
        //当为积分支付时，验证积分是否够支付
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            decimal totalPrice = PubFun.QueryDecimal("totalPrice");
            string selPayType = PubFun.QueryString("selPayType");
            if (selPayType=="积分支付")
            {
                EFEntity.CookiesUser cookie= BLL.UserBLL.Instance.GetLoginModel();
                if (cookie==null)
                {
                    context.Response.Write("false");
                    return;
                }
                var user= BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookie.UserID);
                if(user==null)
                {
                    context.Response.Write("false");
                    return;
                }
                if(user.Account==null)
                {
                    context.Response.Write("false");
                    return;
                }
                if(totalPrice>user.Account.Value)
                {
                    context.Response.Write("false");
                    return;
                }
                else
                {
                    context.Response.Write("true");
                }
            }
            else
            {
                context.Response.Write("true");
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