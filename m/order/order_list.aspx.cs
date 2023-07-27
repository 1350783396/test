using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;
using System.Text;

namespace ETicket.Web.business.order
{
    public partial class order_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefrech.Click += btnRefrech_Click;
            this.btnDel.Click += btnDel_Click;
            this.btnDel2.Click += btnDel_Click;
            if (!Page.IsPostBack)
            {
                #region 绑定订单状态
                ddlOrderStatus.Items.Clear();
                ddlOrderStatus.Items.Add(new ListItem("所有状态"));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.待付款.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已取消.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已失效.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已支付.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已验票.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已过期.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.过期已退.ToString()));
                #endregion

                #region 绑定下单人类型
                ddlUserLevel.Items.Clear();
                ddlUserLevel.Items.Add(new ListItem("所有类型"));
                ddlUserLevel.Items.Add(new ListItem("所有分销商"));
                IEnumerable<EFEntity.UserLevel> levelList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "user" | p.UserCategory == "partner").OrderBy(p => p.OrderValue);
                foreach (var level in levelList)
                {
                    ddlUserLevel.Items.Add(new ListItem(level.UserLevelName));
                }
                #endregion

                #region 绑定产品类型
                ddlProductCategory.Items.Clear();
                ddlProductCategory.Items.Add(new ListItem("所有类型"));
                ddlProductCategory.Items.Add(new ListItem("专线", "line"));
                ddlProductCategory.Items.Add(new ListItem("景区", "ticket"));
                #endregion

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
                        string msg = BLL.OrderSheetBLL.Instance.DeleteSheet(id);
                        Response.Write(msg);
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

        void btnRefrech_Click(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
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
                var order = e.Item.DataItem as EFEntity.OrderSheet;

                HyperLink hyDetail = e.Item.FindControl("hyDetail") as HyperLink;
                string detailUrl = string.Format("/business/order/order_detail.aspx?orderid={0}", order.OrderID);
                hyDetail.NavigateUrl = "#";
                hyDetail.Attributes.Add("onclick", PubFun.TabNav("aod_" + order.OrderID, order.ProductName, detailUrl));

                CheckBox chkItem = e.Item.FindControl("chkItem") as CheckBox;
                chkItem.Enabled = false;
                if (BLL.OrderSheetBLL.Instance.IsDoDelete(order))
                {
                    chkItem.Enabled = true;
                }

                HyperLink hyOverRefund = e.Item.FindControl("hyOverRefund") as HyperLink;
                if (order.OrderStatus == ETicket.Utility.OrderStatusEnum.已过期.ToString())
                {
                    //过期后一个月内可退
                    var overTime = order.TicketFailTime.Value.AddDays(30);
                    if (DateTime.Compare(overTime, DateTime.Now) >= 0)
                    {
                        string overRefundUrl = string.Format("/business/order/order_detail_over_refund.aspx?orderid={0}", order.OrderID);
                        hyOverRefund.NavigateUrl = "#";
                        hyOverRefund.Attributes.Add("onclick", PubFun.TabNav("o_r_od_" + order.OrderID, order.ProductName, overRefundUrl));
                        hyOverRefund.Visible = true;
                    }

                    //银子岩门票，如果不是退款成功，不退款
                    if (order.ProductID == 13 && (order.Out_TuiKStatus == null || order.Out_TuiKStatus.Value != 1))
                    {
                        hyOverRefund.Visible = false;
                    }

                }
            }
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            //sb.AppendFormat(" it.UserID={0}", cookiesUser.UserID);
            //状态筛选
            if (ddlOrderStatus.SelectedValue == "所有状态")
            {
                sb.Append(" it.OrderStatus!='退款审核中' and it.OrderStatus!='退款审核通过' and it.OrderStatus!='已退款' and it.OrderStatus!='退款审核不通过'");
            }
            else
            {
                sb.AppendFormat(" it.OrderStatus='{0}'", ddlOrderStatus.SelectedValue);
            }
            //姓名
            if (this.txtRealname.Text.Trim() != "")
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
            //分销商
            if (txtSelValue.Value.Trim() != "")
            {
                sb.AppendFormat(" and it.UserID={0}", txtSelValue.Value.Trim());
            }
            //产品分类
            if (ddlProductCategory.SelectedValue != "所有类型")
            {
                sb.AppendFormat("and it.CategoryID='{0}'", ddlProductCategory.SelectedValue);
            }
            //下单人类型
            if (ddlUserLevel.SelectedValue == "所有类型")
            {

            }
            else if (ddlUserLevel.SelectedValue == "所有分销商")
            {
                sb.AppendFormat("and it.UserCategory='{0}'", "partner");
            }
            else
            {
                sb.AppendFormat("and it.UserLevelName='{0}'", this.ddlUserLevel.SelectedValue);
            }
            //下单人账号
            if (this.txtUserName.Text.Trim() != "")
            {
                sb.AppendFormat("and it.UserName='{0}'", this.txtUserName.Text.Trim());
            }
            //产品名称
            if (this.txtProductName.Text.Trim() != "")
            {
                sb.AppendFormat("and it.ProductName like '%{0}%'", this.txtProductName.Text.Trim());
            }
            //订单时间
            if (txtOrderTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.OrderTime>=DATETIME'{0} 00:00:00'", txtOrderTime1.Text.Trim());
            }
            if (txtOrderTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.OrderTime<=DATETIME'{0} 23:59:59'", txtOrderTime2.Text.Trim());
            }
            return sb.ToString();
        }
        void LoadData(int currentPage, int pageSize)
        {

            ETicket.Utility.PageInfo<EFEntity.OrderSheet> pi = null;
            pi = BLL.OrderSheetBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.OrderTime DESC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
        }
        public string GetProperties(string ids)
        {
            string ret = "";
            //int id1 = 0;
            string[] contains = ids.Split(',');
            IEnumerable<EFEntity.vProperties2> propertiesList = BLL.vProperties2BLL.Instance.GetEntities();
            List<EFEntity.vProperties2> lstCom = propertiesList.ToList<EFEntity.vProperties2>().FindAll(new Predicate<EFEntity.vProperties2>(r => contains.Contains(r.ID.ToString()))).ToList();
            foreach (var properties in lstCom)
            {
                ret += properties.PName + ":" + properties.Name.ToString() + "<br>";
            }

            //for (int i = 0; i < contains.Length; i++)
            //{
            //    id1 = int.Parse(contains[i]);
            //    List<EFEntity.vProperties2> it = propertiesList.Where(x =>x.ID== id1).ToList();
            //    foreach (var properties in it)
            //    {
            //        ret += properties.PName + ":" + properties.Name.ToString() + "<br>";
            //    }
            //}
            return ret;
        }
    }
}