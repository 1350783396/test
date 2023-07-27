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
    public partial class ticket_publish : AdminBase
    {
        const string greenMsgTempl = "<h4>{0}</h4>";
        const string regMsgTempl = "<h4 style='color:red'>{0}</h4>";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            int productID = PubFun.QueryInt("productid");
            InitNav(productID);


            if (!Page.IsPostBack)
            {
                bool isPublish = true;
                //基本信息
                EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(greenMsgTempl, "景区基本信息：已录入");
                //库存
                //if (product.Stock > 0 )
                //{
                //    sb.AppendFormat(greenMsgTempl, "库存数量：" + product.Stock.ToString());
                //    isPublish = isPublish & true;
                //}
                //else
                //{
                //    sb.AppendFormat(regMsgTempl, "库存信息：库存数量不能为0");
                //    isPublish = isPublish & false;
                //}
                //价格
                IEnumerable<EFEntity.ProductSUK> sukList = BLL.ProductSUKBLL.Instance.GetEntities(p => p.ProductID == productID);
                if (sukList.Count<EFEntity.ProductSUK>() > 0)
                {
                    sb.AppendFormat(greenMsgTempl, "价格：已录入");
                    isPublish = isPublish & true;
                }
                else
                {
                    sb.AppendFormat(regMsgTempl, "价格：未录入[必须录入]");
                    isPublish = isPublish & false;
                }
               

                litChk.Text = sb.ToString();
                chkSale.Checked = product.SaleFlag.Value;

                if (isPublish)
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
            product.SaleFlag = this.chkSale.Checked;
            try
            {
                BLL.ProductBLL.Instance.UpdateObject(product);
                PubFun.ShowMsgRedirect(this, "保存成功！", Request.RawUrl);
            }
            catch (Exception ex)
            {
                PubFun.ShowMsg(this, "保存失败！" + ex.Message);
            }
        }

        void InitNav(int productID)
        {


            hyProduct.NavigateUrl = "ticket_add.aspx?productid=" + productID;
                hyPrice.NavigateUrl = "ticket_price.aspx?productid=" + productID;
                //hyPublish.NavigateUrl = "ticket_publish.aspx?productid=" + productID;
            

            divProduct.Attributes.Add("class", "tab01");
            divPrice.Attributes.Add("class", "tab01");
            divPublish.Attributes.Add("class", "tab02");

        }
    }
}