using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;
namespace ETicket.Web.print
{
    public partial class print_order : System.Web.UI.Page
    {
        public string PrintHTML = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int orderID = PubFun.QueryInt("orderid");
            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
            PrintHTML = HtmlController.Instance.OrderPrint(sheet);
        }
    }
}