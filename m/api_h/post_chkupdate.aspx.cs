using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    public partial class post_chkupdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //变量
            AppChkUpdate result = new AppChkUpdate();
            string josnStr = "";

            //当前最新版本
            int currVersion = 4;

            //app版本
            int version = PubFun.QueryInt("version");
            //app系统
            string platform = PubFun.QueryString("platform");

            NJiaSu.Libraries.LogHelper.LogResult("app_chkupdate", "version:" + version + "|" + "platform:" + platform);

            //0620,修改android才有升级
            if (currVersion > version && platform.ToLower().Contains("android"))
            {
                //需要升级
                result = new AppChkUpdate();
                josnStr = "";

                result.Status = "1";
                if (platform.ToLower().Contains("android"))
                {
                    result.Platform = "1";
                    result.UpdateUrl = "http://www.tianshuntour.com/shj_android.apk";
                }
                else
                {
                    result.Platform = "2";
                    result.UpdateUrl = "https://itunes.apple.com/cn/app/xin-ke-wang/id955075050?mt=8";
                }

                josnStr = JsonHelper.GetJson<AppChkUpdate>(result);
                Response.Write(josnStr);
                Response.End();
                return;
            }
            else
            {
                //不需要升级
                result = new AppChkUpdate();
                josnStr = "";

                result.Status = "0";
                result.Platform = "";
                result.UpdateUrl = "";

                josnStr = JsonHelper.GetJson<AppChkUpdate>(result);
                Response.Write(josnStr);
                Response.End();
                return;
            }

        }
    }
}