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
    public partial class order_list : PartnerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.repList.ItemCommand += repList_ItemCommand;
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


                #region 绑定属性
                IEnumerable<EFEntity.Properties1> propertiesList = BLL.Properties1BLL.Instance.GetEntities();
                ddlProperties.Items.Clear();
                ddlProperties.Items.Add(new ListItem("不限", "0"));
                foreach (var properties in propertiesList)
                {
                    ddlProperties.Items.Add(new ListItem(properties.Name, properties.ID.ToString()));
                }
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

                CheckBox chkItem = e.Item.FindControl("chkItem") as CheckBox;
                chkItem.Enabled = false;

                HyperLink hyDetail = e.Item.FindControl("hyDetail") as HyperLink;
                HyperLink hyPay = e.Item.FindControl("hyPay") as HyperLink;
                HyperLink hyCancel = e.Item.FindControl("hyCancel") as HyperLink;
                HyperLink hyBack = e.Item.FindControl("hyBack") as HyperLink;
                hyDetail.Visible = true;
                hyPay.Visible = false;
                hyCancel.Visible = false;
                hyBack.Visible = false;

                //详情
                string detailUrl = string.Format("/business/partner/order_detail.aspx?orderid={0}", order.OrderID);
                hyDetail.NavigateUrl = "#";
                hyDetail.Attributes.Add("onclick", PubFun.TabNav("od_" + order.OrderID, order.ProductName, detailUrl));

                var bll = BLL.OrderSheetBLL.Instance;
                if (bll.IsDoCancel(order))
                {
                    hyCancel.Visible = true;
                    hyCancel.NavigateUrl = "#";
                    hyCancel.Attributes.Add("onclick", PubFun.TabNav("od_" + order.OrderID, order.ProductName, detailUrl));
                }
                if (bll.IsDoPay(order))
                {
                    hyPay.Visible = true;
                    hyPay.Target = "_blank";
                    hyPay.NavigateUrl = bll.GetPayUrl(order);
                }

                //-------2016-06-10 微信支付不显示网站支付
                if (order.ClientType == "weixin")
                {
                    hyPay.Visible = false;
                }
                //------------end------------

                if (bll.IsDoRefundRequest(order))
                {
                    hyBack.Visible = true;
                    hyBack.Target = "_blank";
                    hyBack.NavigateUrl = bll.GetRefuncUrl(order);
                }
                if (bll.IsDoDelete(order))
                {
                    chkItem.Enabled = true;
                }
            }
        }
        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int orderID = Convert.ToInt32(e.CommandArgument);
            EFEntity.OrderSheet sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
            sheet.ValidTime = DateTime.Now;
            sheet.OrderStatus = ETicket.Utility.OrderStatusEnum.已验票.ToString();
            BLL.OrderSheetBLL.Instance.UpdateObject(sheet);
            btnQuery_Click(source, e);
            ////验票成功后续
            //BLL.ValidNotifyEventBLL.Instance.ValidNotify(sheet);

        }
        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            sb.AppendFormat(" it.UserID={0}", cookiesUser.UserID);
            //状态筛选
            if (ddlOrderStatus.SelectedValue == "所有状态")
            {
                sb.Append("and it.OrderStatus!='退款审核中' and it.OrderStatus!='退款审核通过' and it.OrderStatus!='已退款' and it.OrderStatus!='退款审核不通过'");
            }
            else
            {
                sb.AppendFormat("and it.OrderStatus='{0}'", ddlOrderStatus.SelectedValue);
            }
            //姓名
            if (this.txtRealname.Text.Trim() != "")
            {
                sb.AppendFormat("and it.RealName='{0}'", this.txtRealname.Text.Trim());
            }
            //手机
            if (this.txtPhone.Text.Trim() != "")
            {
                sb.AppendFormat("and it.Phone like '%{0}%'", this.txtPhone.Text.Trim());
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

            //产品属性
            if (ddlProperties.SelectedValue.ToString() != "0" || txtProperties.Text.ToString() != "")
            {
                sb.AppendFormat("{0}", new ETicket.BLL.ProductBLL().GetProperties_sql(ddlProperties.SelectedValue.ToString(), txtProperties.Text.ToString()));
            }

            //订单时间
            if (txtOrderTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.OrderTime>=DATETIME'{0} 00:00:00'", txtOrderTime1.Text.Trim());
            }

            //订单时间 txtPalyDate
            if (txtPalyDate.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.palydate=DATETIME'{0} 00:00:00'", txtPalyDate.Text.Trim());
            }

            if (txtOrderTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.OrderTime<=DATETIME'{0} 23:59:59'", txtOrderTime2.Text.Trim());
            }
            //订单来源
            if (ddlClientType.SelectedValue != "")
            {
                sb.AppendFormat("and it.ClientType = '{0}'", ddlClientType.SelectedValue);
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
            //order.PayState = ETicket.Utility.PayStateEnum.未支付.ToString();
            //sheet.OrderStatus = ETicket.Utility.OrderStatusEnum.已验票.ToString();
            //单（ 项目1+项目2+项目3+项目4+项目5 =172人）
            //ProductName
            this.zongdanliang.Text = IntTextColor(pi.RecordCount);
            this.zongrenshu.Text = IntTextColor(pi.List.Sum(u => u.NUM).GetValueOrDefault(0));
            var yiyanpiao = pi.List.Where(u => u.OrderStatus == "已验票");
            var weiyanpiao = pi.List.Where(u => u.OrderStatus == "已支付");
            var quanpiao = pi.List.Where(u => u.OrderStatus == "已验票" || u.OrderStatus == "已支付");
            var yiyanpiaoXx = IntTextColor(0) + "单";
            var weiyanpiaoXx = IntTextColor(0) + "单";
            var quanpiaoXx = IntTextColor(0) + "单";
            if (yiyanpiao.Count() > 0)
            {
                yiyanpiaoXx = IntTextColor(yiyanpiao.Count()) + "单(";
                foreach (var item in yiyanpiao.GroupBy(u => u.ProductName))
                    yiyanpiaoXx += item.Key + ":" + IntTextColor(item.Sum(u => u.NUM).GetValueOrDefault(0)) + "+";
                yiyanpiaoXx = yiyanpiaoXx.TrimEnd('+') + "=" + IntTextColor(yiyanpiao.Sum(u => u.NUM).GetValueOrDefault(0)) + "人)";
            }
            if (weiyanpiao.Count() > 0)
            {
                weiyanpiaoXx = IntTextColor(weiyanpiao.Count()) + "单(";
                foreach (var item in weiyanpiao.GroupBy(u => u.ProductName))
                    weiyanpiaoXx += item.Key + ":" + IntTextColor(item.Sum(u => u.NUM).GetValueOrDefault(0)) + "+";
                weiyanpiaoXx = weiyanpiaoXx.TrimEnd('+') + "=" + IntTextColor(weiyanpiao.Sum(u => u.NUM).GetValueOrDefault(0)) + "人)";
            }
            if (quanpiao.Count() > 0)
            {
                quanpiaoXx = IntTextColor(quanpiao.Count()) + "单(";
                foreach (var item in quanpiao.GroupBy(u => u.ProductName))
                    quanpiaoXx += item.Key + ":" + IntTextColor(item.Sum(u => u.NUM).GetValueOrDefault(0)) + "+";
                quanpiaoXx = quanpiaoXx.TrimEnd('+') + "=" + IntTextColor(quanpiao.Sum(u => u.NUM).GetValueOrDefault(0)) + "人)";
            }
            this.yiyanpiao.Text = yiyanpiaoXx;
            this.weiyanpiao.Text = weiyanpiaoXx;
            this.quanpiao.Text = quanpiaoXx;

        }
        public string IntTextColor(int srt)
        {
            return "<span style='color:red;'>" + srt + "</span>";
        }
        public string GetProperties(string ids)
        {
            string ret = "";
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