using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public class LoginBase:System.Web.UI.Page
    {
        protected EFEntity.CookiesUser CookiesUser = null;
        protected override void OnLoad(EventArgs e)
        {
            CookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            if (CookiesUser == null)
            {
                Response.Redirect(PubFun.ApplicationPath + "/login.aspx?reurl=" + HttpUtility.UrlEncode(Request.RawUrl));
            }
            base.OnLoad(e);
        }

        public string GetBackUrl()
        {
            string url = Server.UrlDecode(PubFun.QueryString("url"));

            if (url == "")
                return Request.RawUrl;

            if (!url.Contains("viewback"))
            {
                if (url.Contains("?"))
                    url += "&viewback=true";
                else
                    url += "?viewback=true";
            }

            return url;
        }
    }
}