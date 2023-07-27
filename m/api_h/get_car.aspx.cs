using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    public partial class get_car : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int productID = PubFun.QueryInt("productid");
            string token = PubFun.QueryString("token");
            EFEntity.User user = new EFEntity.User();
            if (token != "")
                user = BLL.UserBLL.Instance.GetUserForCookie(token);

            if(user.UserID==0)
            {
                Response.Write("notlogin");
                Response.End();
                return;
            }

            EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID && p.SaleFlag == true);
            if (product == null)
            {
                Response.Write("该产品不存在或者已经下架");
                Response.End();
                return;
            }

            string html = HtmlController.Instance.CarApp(user, product);
            Response.Write(html);
            Response.End();
        }
    }
}