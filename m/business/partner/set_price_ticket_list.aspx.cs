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
    public partial class set_price_ticket_list :PartnerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;


            if (!Page.IsPostBack)
            {
                //加载地区到下拉列表
                IEnumerable<EFEntity.Region> regionList = BLL.RegionBLL.Instance.GetEntities();
                ddlRegion.Items.Clear();
                ddlRegion.Items.Add(new ListItem("所有地区", "所有地区"));
                foreach (var region in regionList)
                {
                    ddlRegion.Items.Add(new ListItem(region.RegionName, region.RegionID.ToString()));
                }

                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
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
            /*
            if (e.Item.ItemType == ListItemType.Header)
            {
                CheckBox checkALL = e.Item.FindControl("chkAll") as CheckBox;
                checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAllEnable('{0}','chkItem',this);", this.form1.ClientID));
            }
            */
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var product = e.Item.DataItem as EFEntity.Product;

                Literal lblReionName = e.Item.FindControl("lblReionName") as Literal;
                var region = BLL.RegionBLL.Instance.GetEntity(p => p.RegionID == product.RegionID.Value);
                if (region != null)
                    lblReionName.Text = region.RegionName;

                Literal litPrice = e.Item.FindControl("litPrice") as Literal;
                Literal litMiniPrice = e.Item.FindControl("litMiniPrice") as Literal;
                Literal litMyPrice = e.Item.FindControl("litMyPrice") as Literal;

                //suk价格
                decimal price = 0, miniPrice = 0, suggestPrice = 0;
                EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID);
                var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == user.UserLevelID);
                if (suk != null)
                {
                    price = suk.ProductPrice.Value;
                    miniPrice = suk.FXPrice_Mini == null ? price + 5 : suk.FXPrice_Mini.Value;
                    suggestPrice = suk.FXPrice_Recommend == null ? price + 10 : suk.FXPrice_Recommend.Value;
                }
                litPrice.Text = price.ToString();
                litMiniPrice.Text = miniPrice.ToString();
                //我的价格
                var myModel = BLL.FXPriceBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID && p.ProductID == product.ProductID);
                if (myModel == null)
                {
                    litMyPrice.Text = suggestPrice.ToString();
                }
                else
                {
                    litMyPrice.Text = myModel.MyPrice.ToString();
                }

                //Edit
                HyperLink hyEdit = e.Item.FindControl("hyEdit") as HyperLink;
                string editUrl = string.Format("/business/partner/set_price_edit.aspx?productid={0}", product.ProductID);
                hyEdit.NavigateUrl = "#";
                hyEdit.Attributes.Add("onclick", PubFun.TabNav("p_edit" + product.ProductID, "售价-" + product.ProductName, editUrl));
            }
        }
        string Where()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" it.CategoryID =='ticket' and it.SaleFlag=true");
            //状态筛选
            if (ddlRegion.SelectedValue != "所有地区")
            {
                sb.AppendFormat(" and it.RegionID={0}", ddlRegion.SelectedValue);
            }

            //产品名称
            if (this.txtProductName.Text.Trim() != "")
            {
                sb.AppendFormat("and it.ProductName like '%{0}%'", this.txtProductName.Text.Trim());
            }

            return sb.ToString();
        }

        void LoadData(int currentPage, int pageSize)
        {
            ETicket.Utility.PageInfo<EFEntity.Product> pi = null;
            pi = BLL.ProductBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.ProductID DESC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
        }
    }
}