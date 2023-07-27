using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// send_resetpass_code 的摘要说明
    /// </summary>
    public class send_resetpass_code : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string rtnStr = string.Empty;


            string username = PubFun.QueryString("username");
            if (username=="")
            {
                context.Response.Write("短信发送失败");
                return;
            }
            var userModel = BLL.UserBLL.Instance.GetEntity(p => p.UserName == username);
            if (userModel == null)
            {
                context.Response.Write("短信发送失败");
                return;
            }
            
            string phone = userModel.Phone;
            if (phone == "")
            {
                context.Response.Write("手机号码不存在，短信发送失败");
                return;
            }

            string msg = BLL.SMSVerifyCodeBLL.Instance.CreateSMS(phone);
            if (msg == "")
            {
                rtnStr = "短信发送成功";
            }
            else
            {
                rtnStr = "短信发送失败";// +msg;
            }
            context.Response.Write(rtnStr);
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