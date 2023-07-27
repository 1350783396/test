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
    public partial class order_sale_report :AdminBase
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
                #region 绑定DDL
                #region 绑定订单状态
                ddlOrderStatus.Items.Clear();
                ddlOrderStatus.Items.Add(new ListItem("所有状态"));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.待付款.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已取消.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已失效.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已支付.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已验票.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已过期.ToString()));
                #endregion

               
                ddlProductCategory.Items.Clear();
                ddlProductCategory.Items.Add(new ListItem("所有类型"));
                ddlProductCategory.Items.Add(new ListItem("专线","line"));
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
                        string msg= BLL.OrderSheetBLL.Instance.DeleteSheet(id);
                        if(msg=="")
                        {
                            deleteSucc++;
                        }
                    }
                }
            }

            if (deleteSucc>0)
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

                //var bll=BLL.OrderSheetBLL.Instance;
                //CheckBox chkItem = e.Item.FindControl("chkItem") as CheckBox;
                //chkItem.Enabled = false;
                //if (bll.IsDoDelete(order))
                //{
                //    chkItem.Enabled = true;
                //}

                Literal litProductCategory = e.Item.FindControl("litProductCategory") as Literal;
                if (order.CategoryID == "line")
                    litProductCategory.Text = "专线";
                else if (order.CategoryID == "ticket")
                    litProductCategory.Text = "专线";
                else
                    litProductCategory.Text = "其他";

              

                HyperLink hyDetail = e.Item.FindControl("hyDetail") as HyperLink;
                hyDetail.Visible = true;
             
                //详情
                string detailUrl = string.Format("/business/order/order_detail.aspx?orderid={0}", order.OrderID);
                hyDetail.NavigateUrl = "#";
                hyDetail.Attributes.Add("onclick", PubFun.TabNav("od_" + order.OrderID, order.ProductName, detailUrl));

             
                
            }
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            StringBuilder sbPrint = new StringBuilder();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            //sb.AppendFormat(" it.UserID={0}", cookiesUser.UserID);
            //状态筛选
            if (ddlOrderStatus.SelectedValue == "所有状态")
            {
                string key1 = ETicket.Utility.OrderStatusEnum.已支付.ToString();
                string key2 = ETicket.Utility.OrderStatusEnum.已验票.ToString();
                sb.AppendFormat(" (it.OrderStatus='{0}' or it.OrderStatus='{1}')",key1,key2);
            }
            else
            {
                sb.AppendFormat(" it.OrderStatus='{0}'", ddlOrderStatus.SelectedValue);
            }
            sbPrint.Append("&ddlOrderStatus=" + ddlOrderStatus.SelectedValue);
            //订单时间
            if (txtOrderTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.OrderTime>=DATETIME'{0} 00:00:00'", txtOrderTime1.Text.Trim());
            }
            sbPrint.Append("&txtOrderTime1=" + txtOrderTime1.Text.Trim());

            if (txtOrderTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.OrderTime<=DATETIME'{0} 23:59:59'", txtOrderTime2.Text.Trim());
            }
            sbPrint.Append("&txtOrderTime2=" + txtOrderTime2.Text.Trim());

            //发车日期
            if (txtStartTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.StartTime>=DATETIME'{0} 00:00:00'", txtStartTime1.Text.Trim());
            }
            sbPrint.Append("&txtStartTime1=" + txtStartTime1.Text.Trim());

            if (txtStartTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.StartTime<=DATETIME'{0} 23:59:59'", txtStartTime2.Text.Trim());
            }
            sbPrint.Append("&txtStartTime2=" + txtStartTime2.Text.Trim());

            //发车时间点
            if(txtStartHM.Text.Trim()!="")
            {
                try
                {
                    DateTime t= Convert.ToDateTime("2014-06-22 " + txtStartHM.Text.Trim());
                    int h = t.Hour;
                    int m = t.Minute;

                    sb.AppendFormat("and it.StartH={0}", h.ToString());
                    sb.AppendFormat("and it.StartM={0}", m.ToString());
                }
                catch
                {

                }
            }
            sbPrint.Append("&txtStartHM=" + txtStartHM.Text.Trim());

            if (ddlProductCategory.SelectedValue != "所有类型")
            {
                sb.AppendFormat("and it.CategoryID='{0}'", ddlProductCategory.SelectedValue);
            }
            sbPrint.Append("&ddlProductCategory=" + ddlProductCategory.SelectedValue);

            //姓名
            if (this.txtRealname.Text.Trim() != "")
            {
                sb.AppendFormat("and it.RealName='{0}'", this.txtRealname.Text.Trim());
            }
            sbPrint.Append("&txtRealname=" + txtRealname.Text.Trim());

            //手机
            if (this.txtPhone.Text.Trim() != "")
            {
                sb.AppendFormat("and it.Phone='{0}'", this.txtPhone.Text.Trim());
            }
            sbPrint.Append("&txtPhone=" + txtPhone.Text.Trim());

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
            //上车地点
            if (txtStartAddress.Text.Trim() != "")
            {
                sb.AppendFormat("and it.StartAddress like '%{0}%'", txtStartAddress.Text.Trim());
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

            this.rptPrint.DataSource = pi.List;
            this.rptPrint.DataBind();
            int count = pi.List.Count();
            int sum = 0;
            foreach(var item in pi.List)
            {
                sum = sum + item.NUM.Value;
            }
            litViewCount.Text = litPrintCount.Text = string.Format("{0}条记录，合{1}人", count, sum);
            
        }
    }
}