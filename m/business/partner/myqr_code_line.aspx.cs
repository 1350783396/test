using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

namespace ETicket.Web.business.partner
{
    public partial class myqr_code_line : PartnerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSerch.ServerClick += btnSerch_ServerClick;
            this.btnClear.ServerClick += btnClear_ServerClick;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            if (!Page.IsPostBack)
            {
                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }

        void btnClear_ServerClick(object sender, EventArgs e)
        {
            this.txtProductName.Value = "";
            AspNetPager1.CurrentPageIndex = 1;
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void btnSerch_ServerClick(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            sb.AppendFormat(" it.SaleFlag={0}", true);
            sb.AppendFormat(" and it.CategoryID='{0}'", "line");
            //状态筛选
            if (this.txtProductName.Value.Trim() != "")
            {
                sb.AppendFormat("and it.ProductName like '%{0}%'", this.txtProductName.Value.Trim());
            }

            return sb.ToString();
        }

        void LoadData(int currentPage, int pageSize)
        {

            ETicket.Utility.PageInfo<EFEntity.Product> pi = null;
            pi = BLL.ProductBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.ProductID DESC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);

            litResultList.Text = HtmlController.Instance.MyQRCode_Html(pi.List);
        }
    }
}