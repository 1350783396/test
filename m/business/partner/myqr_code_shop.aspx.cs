using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.partner
{
    public partial class myqr_code_shop :PartnerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                var userID=base.CookiesUser.UserID;

                var userModel = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userID);
                //图片
                string webhost = PubFun.ServerHost();
                var imgPath = HtmlController.Instance.CreateMyShopQRCode(webhost, userID.ToString());
                imgQR.ImageUrl = imgPath;

                var myShopUrl = webhost + "/wx_myshop_{0}.html";
                var qContent = ETicket.Web.HtmlController.CreateWapQContent(userID.ToString(), "0");
                myShopUrl = string.Format(myShopUrl, qContent);

                //litResultList.Text = "微店首页地址：<br/>"+myShopUrl+"<br/>注：可把该地址配置你公司的微信公众号下";

                //下载地址
                string urlTempl = PubFun.ApplicationPath + "/business/partner/myqr_code_down.aspx?c={0}&n={1}";

                var enProductName = HttpContext.Current.Server.UrlEncode(userModel.CPName);
                var fileNameNoPath = System.IO.Path.GetFileNameWithoutExtension(imgPath);
                var downLoadUrl=string.Format(urlTempl, enProductName, fileNameNoPath);

                hyDownLoad.Text = "下载";
                hyDownLoad.NavigateUrl = downLoadUrl;
                hyDownLoad.Target = "_blank";

            }
        }
    }
}