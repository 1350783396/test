using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public class PartnerBase: System.Web.UI.Page
    {
        protected EFEntity.CookiesUser CookiesUser = null;
        protected override void OnLoad(EventArgs e)
        {
            CookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            if (CookiesUser == null)
            {
                Response.Redirect(PubFun.ApplicationPath + "/login.aspx?reurl=" + HttpUtility.UrlEncode(Request.RawUrl));
            }
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == CookiesUser.UserID);
            if(user==null)
            {
                Response.Redirect(PubFun.ApplicationPath + "/login.aspx?reurl=" + HttpUtility.UrlEncode(Request.RawUrl));
            }
            if (user.UserCategory != "partner")
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.NotPartnerAccess.GetHashCode());
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