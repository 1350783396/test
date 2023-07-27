using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Script.Serialization;

namespace ETicket.Web
{
    public partial class wx_auth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var appid = "wx64878ab63c3b08ff";
            //var secret = "8e4fd010a681118005067ec733739b9c";

            var appid = "wx8e8e2387b145ae2c";
            var secret = "60f89250718d8f90f58d113b62607da4";

            var code =Request.QueryString["Code"];
            if (string.IsNullOrEmpty(code))
            {
                var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fwww.tianshuntour.com%2fwx_auth.aspx&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect", appid);
                Response.Redirect(url);
            }
            else
            {
                var client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.UTF8;

                var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
                var data = client.DownloadString(url);

                var serializer = new JavaScriptSerializer();
                var obj = serializer.Deserialize<Dictionary<string, string>>(data);
                string accessToken;
                if (!obj.TryGetValue("access_token", out accessToken))
                    return;

                var opentid = obj["openid"];
                url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", accessToken, opentid);
                //url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", accessToken, opentid);
                data = client.DownloadString(url);
                var userInfo = serializer.Deserialize<Dictionary<string, object>>(data);
                foreach (var key in userInfo.Keys)
                {
                    Response.Write(string.Format("{0}: {1}", key, userInfo[key]) + "<br/>");
                }
            }
        }
    }
}