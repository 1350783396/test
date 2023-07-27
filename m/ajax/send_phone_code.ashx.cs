using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// send_phone_code 的摘要说明
    /// </summary>
    public class send_phone_code : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string rtnStr = string.Empty;
            string phone = PubFun.QueryString("phone");
            string validExits = PubFun.QueryString("validExits");//是否验证手机号码存在，用于已注册用户

            if (!string.IsNullOrEmpty(phone))
            {
                //是否验证手机号码存在,不存在返回
                if (!string.IsNullOrEmpty(validExits) && validExits.ToLower() == "true")
                {
                    EFEntity.User model = BLL.UserBLL.Instance.GetEntity(p => p.Phone == phone);
                    if (model == null)
                    {
                        context.Response.Write("该号码没有注册，短信发送失败");
                        return;
                    }
                }
                string msg = BLL.SMSVerifyCodeBLL.Instance.CreateSMS(phone);
                if (msg=="")
                {
                    rtnStr = "短信发送成功";
                }
                else
                {
                    rtnStr = "短信发送失败";
                }
            }
            else
            {
                rtnStr = "短信发送失败";
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