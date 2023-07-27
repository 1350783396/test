using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// chk_username 的摘要说明
    /// </summary>
    public class chk_username : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username = PubFun.QueryString("username");
            if (username=="")
            { 
                context.Response.Write("false");
                return;
            }
            var user=BLL.UserBLL.Instance.GetEntity(p => p.UserName == username);
            if(user!=null)
            {
                context.Response.Write("false");
                return;
            }
            //if(string.IsNullOrEmpty(user.Phone))
            //{
            //    context.Response.Write("false");
            //    return;
            //}
            context.Response.Write("true");
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