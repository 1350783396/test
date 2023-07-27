using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Script.Serialization;

namespace ETicket.Web
{
    public partial class wx_auth2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var appid = "wx64878ab63c3b08ff";
            var secret = "8e4fd010a681118005067ec733739b9c";

            var code =Request.QueryString["Code"];
            if (string.IsNullOrEmpty(code))
            {
                //var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fwww.tianshuntour.com%2fwx_auth.aspx&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect", appid);
                var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fwww.tianshuntour.com%2fwx_auth2.aspx&response_type=code&scope=snsapi_base&state=1#wechat_redirect", appid);
                Response.Redirect(url);
            }
            else
            {
                //主要获取openID
                var client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.UTF8;

                var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
                var data = client.DownloadString(url);

                var serializer = new JavaScriptSerializer();
                var obj = serializer.Deserialize<Dictionary<string, string>>(data);

                /*
                string accessToken;
                if (!obj.TryGetValue("access_token", out accessToken))
                    return;
                */
                var opentid = obj["openid"];

                //获取全局 accessToken
                var url2 = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
                var data2 = client.DownloadString(url2);
                var obj2 = serializer.Deserialize<Dictionary<string, string>>(data2);

                var accessToken = obj2["access_token"];

                var url3 = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", accessToken, opentid);
                var data3 = client.DownloadString(url3);
                var userInfo = serializer.Deserialize<Dictionary<string, object>>(data3);

                var subscribe = userInfo["subscribe"];

                foreach (var key in userInfo.Keys)
                {
                    Response.Write(string.Format("{0}: {1}", key, userInfo[key]) + "<br/>");
                }

                if(subscribe.ToString()=="0")
                {
                    Response.Redirect("http://mp.weixin.qq.com/s?__biz=MzI3MTMwOTEzNQ==&mid=2247483656&idx=1&sn=16235b696c5929203a528a90ce59c779#rd");
                }
            }
        }
    }
}