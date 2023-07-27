using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ETicket.Web.business.art
{
    public partial class view_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnRefresh.Click += btnRefresh_Click;
            if (!Page.IsPostBack)
            {
                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }
        void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //通知公告
            sb.AppendFormat(" it.ModuleID={0}", "1");
            return sb.ToString();
        }
        void LoadData(int currentPage, int pageSize)
        {

            ETicket.Utility.PageInfo<EFEntity.ArtContent> pi = null;
            pi = BLL.ArtContentBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.AddTime DESC");

            AspNetPager1.RecordCount = pi.RecordCount;

            string path = "/templ/art/admin_tr.html";
            litResultList.Text = HtmlController.Instance.ArtListHtml(pi.List, path);
        }
    }
}