using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public class RefundController
    {
        #region 实例本身
        private static RefundController _Instance;

        /// <summary>
        /// 获取静态对象(单件模式)
        /// </summary>
        public static RefundController Instance
        {
            get
            {
                if (_Instance != null) return _Instance;
                _Instance = new RefundController();
                return _Instance;
            }
        }
        #endregion

        public void isDo(HttpResponse Response, out EFEntity.OrderSheet sheet, EFEntity.CookiesUser cookiesUser)
        {
            int orderID = PubFun.QueryInt("id");
            if (orderID <= 0)
            {
                Response.Redirect(PubFun.ApplicationPath + "msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_noexits.GetHashCode());
            }
            sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
            if (sheet == null)
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_noexits.GetHashCode());
            }
            if (sheet.UserID != cookiesUser.UserID)//不是自己的订单
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_noexits.GetHashCode());
            }
            if(!BLL.OrderSheetBLL.Instance.IsDoRefundRequest(sheet))
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.RefundNoCondition.GetHashCode());
            }
        }

        public void Bind(ETicket.Web.LoginBase page, EFEntity.OrderSheet sheet)
        {
            Literal litProductName = page.FindControl("litProductName") as Literal;
            Literal litUnitPrice = page.FindControl("litUnitPrice") as Literal;
            Literal litNum = page.FindControl("litNum") as Literal;
            Literal litTotalPrice = page.FindControl("litTotalPrice") as Literal;

            litProductName.Text = sheet.ProductName;
            litUnitPrice.Text = sheet.UnitPrice.ToString();
            litNum.Text = sheet.NUM.ToString();
            litTotalPrice.Text = sheet.TotalPrice.ToString();

            //产品表
            Literal litRule = page.FindControl("litRule") as Literal;
            Literal litDetail = page.FindControl("litDetail") as Literal;
            var product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == sheet.ProductID);
            if (product != null)
            {
                litRule.Text = product.RulesNote;
                litDetail.Text = product.Detail;
            }
        }

    }
}