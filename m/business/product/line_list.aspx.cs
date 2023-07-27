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
    public partial class line_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnDel.Click += btnDel_Click;
            this.btnDel2.Click += btnDel_Click;

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

        void btnDel_Click(object sender, EventArgs e)
        {
            int deleteSucc = 0;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                CheckBox chkItem = this.repList.Items[i].FindControl("chkItem") as CheckBox;
                if (chkItem != null && chkItem.Checked == true)
                {
                    Label lblPKID = this.repList.Items[i].FindControl("lblPKID") as Label;
                    if (lblPKID != null)
                    {
                        int id = int.Parse(lblPKID.Text);
                        string msg = "禁止删除";// BLL.ProductBLL.Instance.DeleteProduct(id);
                        if (msg == "")
                        {
                            deleteSucc++;
                        }
                    }
                }
            }

            if (deleteSucc > 0)
            {
                //AspNetPager1.CurrentPageIndex = 1;
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
            if (e.Item.ItemType == ListItemType.Header)
            {
                CheckBox checkALL = e.Item.FindControl("chkAll") as CheckBox;
                checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAllEnable('{0}','chkItem',this);", this.form1.ClientID));
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var product = e.Item.DataItem as EFEntity.Product;

                Literal lblSupplyName = e.Item.FindControl("lblSupplyName") as Literal;
                var region = BLL.RegionBLL.Instance.GetEntity(p => p.RegionID == product.RegionID.Value);
                if (region != null)
                    lblSupplyName.Text = region.RegionName;

                Literal litSaleFlag = e.Item.FindControl("litSaleFlag") as Literal;
                litSaleFlag.Text = product.SaleFlag.Value == true ? "发布" : "未发布"; ;

                CheckBox chkItem = e.Item.FindControl("chkItem") as CheckBox;
                chkItem.Enabled = false;
                if (!product.SaleFlag.Value)
                {
                    chkItem.Enabled = true;
                }

                //Edit
                HyperLink hyEdit = e.Item.FindControl("hyEdit") as HyperLink;
                string editUrl = string.Format("/business/product/line_add.aspx?productid={0}", product.ProductID);
                hyEdit.NavigateUrl = "#";
                hyEdit.Attributes.Add("onclick", PubFun.TabNav("p_edit" + product.ProductID, "编辑-" + product.ProductName, editUrl));
            }
        }
        string Where()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append( " it.CategoryID =='line'");
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