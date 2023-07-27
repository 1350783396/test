using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.content
{
    public partial class view_ticket : System.Web.UI.Page
    {
        public string PageHtml = "";
        public string ProductName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int productID = PubFun.QueryInt("id");
            if(productID<=0)
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.product_noexits.GetHashCode());
            }
            //产品
            var product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID);
            ProductName = product.ProductName;
            //页面
            string viewPath = "/templ/view/ticket.html";
            PageHtml=HtmlController.Instance.ViewProduct(productID, viewPath);
            
        }
    }
}