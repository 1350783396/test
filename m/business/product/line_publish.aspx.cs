using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class line_publish : AdminBase
    {
        const string greenMsgTempl ="<h4>{0}</h4>";
        const string regMsgTempl = "<h4 style='color:red'>{0}</h4>";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            int productID = PubFun.QueryInt("productid");
            InitNav(productID);


            if(!Page.IsPostBack)
            {
                bool isPublish = true;
                //基本信息
                EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(greenMsgTempl,"专线基本信息：已录入");
                //价格
                IEnumerable<EFEntity.ProductSUK> sukList=BLL.ProductSUKBLL.Instance.GetEntities(p => p.ProductID == productID);
                if(sukList.Count<EFEntity.ProductSUK>()>0)
                {
                    sb.AppendFormat(greenMsgTempl, "价格：已录入");
                    isPublish = isPublish & true;
                }
                else
                {
                    sb.AppendFormat(regMsgTempl,"价格：未录入[必须录入]" );
                    isPublish = isPublish & false;
                }
                //时间
                IEnumerable<EFEntity.ProductSaleTime> tickList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == productID);
                if (tickList.Count<EFEntity.ProductSaleTime>() > 0)
                {
                    sb.AppendFormat(greenMsgTempl, "销售时间：已录入");
                    isPublish = isPublish & true;
                }
                else
                {
                    sb.AppendFormat(regMsgTempl, "销售时间：未录入[必须录入]");
                    isPublish = isPublish & false;
                }
                //库存
                IEnumerable<EFEntity.ProductStock> stockList = BLL.ProductStockBLL.Instance.GetEntities(p => p.ProductID == productID);
                if (stockList.Count<EFEntity.ProductStock>() > 0)
                {
                    sb.AppendFormat(greenMsgTempl, "库存：已录入");
                }
                else
                {
                    sb.AppendFormat(regMsgTempl, "库存：未录入[可选录入，无库存则无销量限制]");
                    
                }
                //上车地点
                IEnumerable<EFEntity.ProductAddress> addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == productID);
                if (addressList.Count<EFEntity.ProductAddress>() > 0)
                {
                    sb.AppendFormat(greenMsgTempl,"上车地点：已录入");
                    //isPublish = isPublish & true;
                }
                else
                {
                    sb.AppendFormat(regMsgTempl, "上车地点：未录入[可选录入，不录入则下单时手动输入]");
                    //isPublish = isPublish & false;
                }

                litChk.Text = sb.ToString();
                chkSale.Checked = product.SaleFlag.Value;

                if(isPublish)
                {
                    this.chkSale.Enabled = true;
                    this.btnSave.Enabled = true;
                }
                else
                {
                    this.chkSale.Enabled = false;
                    this.btnSave.Enabled = false;
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            int productID = PubFun.QueryInt("productid");
            EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID);
            product.SaleFlag=this.chkSale.Checked;
            try 
            { 
                BLL.ProductBLL.Instance.UpdateObject(product);
                PubFun.ShowMsgRedirect(this,"保存成功！",Request.RawUrl);
            }
            catch(Exception ex)
            {
                PubFun.ShowMsg(this, "保存失败！"+ex.Message);
            }
        }

        void InitNav(int productID)
        {
            hyProduct.NavigateUrl = "line_add.aspx?productid=" + productID;
            hyPrice.NavigateUrl = "line_price.aspx?productid=" + productID;
            hyTickTime.NavigateUrl = "line_ticktime.aspx?productid=" + productID;
            hyStock.NavigateUrl = "line_stock.aspx?productid=" + productID;
            hyAddress.NavigateUrl = "line_address.aspx?productid=" + productID;
            //hyPublish.NavigateUrl = "line_publish.aspx?productid=" + productID;

            divProduct.Attributes.Add("class", "tab01");
            divPrice.Attributes.Add("class", "tab01");
            divTickTime.Attributes.Add("class", "tab01");
            divStock.Attributes.Add("class", "tab01");
            divAddress.Attributes.Add("class", "tab01");
            divPublish.Attributes.Add("class", "tab02");
        }
    }
}