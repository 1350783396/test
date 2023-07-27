using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;
using System.Text;

namespace ETicket.Web.business.partner
{
    public partial class set_price_edit : PartnerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                var productID = PubFun.QueryInt("productid");
                var productModel = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID);
                if (productModel == null)
                    return;

                if (productModel.CategoryID == "line")
                {
                    litCategory.Text = "专线";
                }
                else if (productModel.CategoryID == "ticket")
                {
                    litCategory.Text = "门票";
                }
                litProductName.Text = productModel.ProductName;

                //suk价格
                decimal price = 0, miniPrice = 0, suggestPrice = 0;
                EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID);
                var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == productID && p.UserLevelID == user.UserLevelID);
                if (suk != null)
                {
                    price = suk.ProductPrice.Value;
                    miniPrice = suk.FXPrice_Mini == null ? price + 5 : suk.FXPrice_Mini.Value;
                    suggestPrice = suk.FXPrice_Recommend == null ? price + 10 : suk.FXPrice_Recommend.Value;
                }
                litPrice.Text = price.ToString();
                litMiniPrice.Text = miniPrice.ToString();
                //我的价格
                var myModel = BLL.FXPriceBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID && p.ProductID == productID);
                if (myModel == null)
                {
                    txtMyPrice.Value = suggestPrice.ToString();
                }
                else
                {
                    txtMyPrice.Value = myModel.MyPrice.ToString();
                }
            }
        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            if (txtMyPrice.Value == "")
            {
                PubFun.ShowMsg(this.Page, "设置销售价格不能为空");
                return;
            }

            var myPrice = Convert.ToDecimal(txtMyPrice.Value);
            var miniPrice = Convert.ToDecimal(litMiniPrice.Text);
            if (myPrice < miniPrice)
            {
                PubFun.ShowMsg(this.Page, "提示：设置销售价格不能低于最低分销售价");
                return;
            }

            var productID = PubFun.QueryInt("productid");
            var userID = base.CookiesUser.UserID;

            try
            {
                var myModel = BLL.FXPriceBLL.Instance.GetEntity(p => p.UserID == userID && p.ProductID == productID);
                if (myModel == null)
                {
                    myModel = new EFEntity.FX_Price();
                    myModel.ProductID = productID;
                    myModel.UserID = userID;
                    myModel.MyPrice = myPrice;
                    myModel.UpdateTime = DateTime.Now;

                    BLL.FXPriceBLL.Instance.AddObject(myModel);
                }
                else
                {
                    myModel.MyPrice = myPrice;
                    myModel.UpdateTime = DateTime.Now;
                    BLL.FXPriceBLL.Instance.UpdateObject(myModel);
                }

                PubFun.ShowMsg(this.Page,"保存成功！");
            }
            catch
            {
                PubFun.ShowMsg(this.Page, "保存失败！");
            }
        }
    }
}