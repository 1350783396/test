using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

namespace ETicket.Web.business.partner
{
    public partial class back_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            if(!Page.IsPostBack)
            {
                #region 绑定订单状态
                ddlOrderStatus.Items.Clear();
                ddlOrderStatus.Items.Add(new ListItem("所有退款状态"));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.退款审核中.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.退款审核通过.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.退款审核不通过.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已退款.ToString()));
                #endregion

                LoadData(AspNetPager1.CurrentPageIndex,AspNetPager1.PageSize);
            }
        }

        void btnQuery_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser=BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb=new StringBuilder();
            //自己的订单
            sb.AppendFormat(" it.UserID={0}", cookiesUser.UserID);
            //状态筛选
            if (ddlOrderStatus.SelectedValue == "所有退款状态")
            {
                sb.Append("and (it.OrderStatus ='退款审核中' or it.OrderStatus ='退款审核通过' or it.OrderStatus ='已退款' or it.OrderStatus ='退款审核不通过')"); 
            }
            else
            {
                sb.AppendFormat("and it.OrderStatus='{0}'",ddlOrderStatus.SelectedValue);
            }
            //姓名
            if(this.txtRealname.Text.Trim()!="")
            {
                sb.AppendFormat("and it.RealName='{0}'", this.txtRealname.Text.Trim());
            }
            //手机
            if (this.txtPhone.Text.Trim() != "")
            {
                sb.AppendFormat("and it.Phone='{0}'", this.txtPhone.Text.Trim());
            }
            //订单号
            if (this.txtSheetID.Text.Trim() != "")
            {
                sb.AppendFormat("and it.SheetID='{0}'", this.txtSheetID.Text.Trim());
            }
            //产品名称
            if (this.txtProductName.Text.Trim() != "")
            {
                sb.AppendFormat("and it.ProductName like '%{0}%'", this.txtProductName.Text.Trim());
            }
            return sb.ToString();
        }
        void LoadData(int currentPage,int pageSize)
        {

            ETicket.Utility.PageInfo<EFEntity.OrderSheet> pi = null;
            pi = BLL.OrderSheetBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.OrderTime DESC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
        }
    }
}