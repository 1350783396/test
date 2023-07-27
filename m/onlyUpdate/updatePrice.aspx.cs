using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETicket.Web.onlyUpdate
{
    public partial class updatePrice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var list = BLL.ProductSUKBLL.Instance.GetEntities();
            foreach(var suk in list)
            {
                var product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == suk.ProductID);
                suk.FXPrice_Mini = suk.ProductPrice + 5;
                suk.FXPrice_Recommend = product.PrimeCost;
                BLL.ProductSUKBLL.Instance.UpdateObject(suk);
            }

            Response.Write("升级价格数据完毕");
        }
    }
}