using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// get_account 的摘要说明
    /// </summary>
    public class get_account : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var userID=PubFun.QueryInt("userid");

            //登录判断
            var CookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            if (CookiesUser == null)
            {
                context.Response.Write("");
                return;
            }
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == CookiesUser.UserID);
            if (user == null)
            {
                context.Response.Write("");
                return;
            }
            if (user.UserCategory != "admin")
            {
                context.Response.Write("");
                return;
            }

            //查询对象
            var userObj = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userID);
            if (userObj != null && userObj.Account>0)
            {
                context.Response.Write(userObj.Account.Value);
            } 
            else
            {
                context.Response.Write("");
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