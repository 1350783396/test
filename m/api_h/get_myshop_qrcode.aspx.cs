using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    public partial class get_myshop_qrcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                string token = PubFun.QueryString("token");
                EFEntity.User user = new EFEntity.User();
                if (token != "")
                    user = BLL.UserBLL.Instance.GetUserForCookie(token);

                if (user.UserID == 0)
                {
                    Response.Write("notlogin");
                    Response.End();
                    return;
                }

                //图片
                string webhost = PubFun.ServerHost();
                var imgPath = HtmlController.Instance.CreateMyShopQRCode(webhost, user.UserID.ToString());

                Response.Write(imgPath);

            }
        }
    }
}