using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETicket.Web.wap
{
    public partial class shopcar : System.Web.UI.Page
    {
        public string html = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int productID = 0;
            int userID = 0;
            WapHelper.GetUserID_ProductID("q",out userID, out productID);

            if(productID==0&&userID==0)
            {
                Response.Write("信息错误");
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

            EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userID);

            html = HtmlController.Instance.CarWap(user, product);
        }
    }
}