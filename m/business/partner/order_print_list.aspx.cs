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
    public partial class order_print_list : PartnerBase
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
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.待付款.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已取消.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已失效.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已支付.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已验票.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已过期.ToString()));
                #endregion

                #region 绑定验类型
                ddlValid.Items.Clear();
                //ddlValid.Items.Add(new ListItem("所有类型"));
                ddlValid.Items.Add(new ListItem("纸质"));
               
                

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

                Literal litIsPrint = e.Item.FindControl("litIsPrint") as Literal;
                if(order.IsPrint!=null&&order.IsPrint.Value==true)
                {
                    litIsPrint.Text = "已打印";
                }
                else
                {
                    //litIsPrint.Text = "未打印";
                    litIsPrint.Text = string.Format("<a href=\"#\" onclick=\"PrintTicket('{0}')\">打印</a>",order.OrderID);
                }

                HyperLink hyDetail = e.Item.FindControl("hyDetail") as HyperLink;
                //HyperLink hyPrint = e.Item.FindControl("hyPrint") as HyperLink;
              
                hyDetail.Visible = true;
                //hyPrint.Visible = false;
             
                //详情
                string detailUrl = string.Format("/business/partner/order_detail.aspx?orderid={0}", order.OrderID);
                hyDetail.NavigateUrl = "#";
                hyDetail.Attributes.Add("onclick", PubFun.TabNav("od_" + order.OrderID, order.ProductName, detailUrl));

                //if (BLL.OrderSheetBLL.Instance.IsDoPrint(order))
                //{
                //    hyPrint.Visible = true;

                //    string printUrl = string.Format("/print/print_order.aspx?orderid={0}", order.OrderID);
                //    hyPrint.NavigateUrl = "#";
                //    hyPrint.Attributes.Add("onclick", PubFun.TabNav("odprint_" + order.OrderID, "打印-" + order.ProductName, printUrl));
                //}
                
            }
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            sb.AppendFormat(" it.UserID={0}", cookiesUser.UserID);
            //状态筛选
            /**/
            if (ddlOrderStatus.SelectedValue == "所有状态")
            {
                //sb.Append("and it.OrderStatus!='退款审核中' and it.OrderStatus!='退款审核通过' and it.OrderStatus!='已退款' and it.OrderStatus!='退款审核不通过'");
            }
            else
            {
                sb.AppendFormat("and it.OrderStatus='{0}'", ddlOrderStatus.SelectedValue);
            }
            
            //验证类型
            if (ddlValid.SelectedValue != "所有方式")
            {
                sb.AppendFormat("and it.ValidType='{0}'", ddlValid.SelectedValue);
            }
            if (ddlProductCategory.SelectedValue != "所有类型")
            {
                sb.AppendFormat("and it.CategoryID='{0}'", ddlProductCategory.SelectedValue);
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
            //产品名称
            if (this.txtProductName.Text.Trim() != "")
            {
                sb.AppendFormat("and it.ProductName like '%{0}%'", this.txtProductName.Text.Trim());
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
            return ret;
        }
    }
}