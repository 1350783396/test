using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using NJiaSu.Libraries;
using System.Text;

namespace ETicket.Web.business.valid
{
    public partial class valid_list : ValidBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefrech.Click += btnRefrech_Click;
            this.btnDel.Click += btnDel_Click;
            this.btnExcel.Click += btnExcel_Click;
            if (!Page.IsPostBack)
            {
                #region 绑定订单状态
                ddlOrderStatus.Items.Clear();
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已验票.ToString()));
               
                #endregion

                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }
        void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("订单号");
            dt.Columns.Add("产品名称");
            dt.Columns.Add("客人姓名");
            dt.Columns.Add("客人手机");
            dt.Columns.Add("票据数量");
            dt.Columns.Add("状态");
            dt.Columns.Add("验票时间");

            var pi = LoadDataPageInfo(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            foreach(var item in pi.List)
            {
                DataRow row = dt.NewRow();
                row["订单号"] = item.SheetID;
                row["产品名称"] =item.ProductName;
                row["客人姓名"] =item.RealName;
                row["客人手机"] =item.Phone;
                row["票据数量"] =item.NUM;
                row["状态"] = item.OrderStatus;
                row["验票时间"] =item.ValidTime;

                dt.Rows.Add(row);
            }
            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(dt);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(dt, colTxtArray);

            string fileName = "已验票明细" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            DownLoadHelper.Instance.DownLoadFile(this, fileName, file);
        }

        void btnDel_Click(object sender, EventArgs e)
        {
            /*
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
            */
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
            //if (e.Item.ItemType == ListItemType.Header)
            //{
            //    CheckBox checkALL = e.Item.FindControl("chkAll") as CheckBox;
            //    checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAllEnable('{0}','chkItem',this);", this.form1.ClientID));
            //}
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var order = e.Item.DataItem as EFEntity.OrderSheet;

                /*
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

                var bll=BLL.OrderSheetBLL.Instance;
                if (bll.IsDoCancel(order))
                {
                    hyCancel.Visible = true;
                    hyCancel.NavigateUrl = "#";
                    hyCancel.Attributes.Add("onclick", PubFun.TabNav("od_" + order.OrderID, order.ProductName, detailUrl));
                }
                if(bll.IsDoPay(order))
                {
                    hyPay.Visible = true;
                    hyPay.Target = "_blank";
                    hyPay.NavigateUrl = bll.GetPayUrl(order);
                }
                if(bll.IsDoRefundRequest(order))
                {
                    hyBack.Visible = true;
                    hyBack.Target = "_blank";
                    hyBack.NavigateUrl = bll.GetRefuncUrl(order);
                }
                if (bll.IsDoDelete(order))
                {
                    chkItem.Enabled = true;
                }
                */
            }
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            sb.AppendFormat(" it.ValidUserID={0}", cookiesUser.UserID);
            //状态筛选
            if (ddlOrderStatus.SelectedValue == "所有状态")
            {
                sb.Append("and it.OrderStatus!='退款审核中' and it.OrderStatus!='退款审核通过' and it.OrderStatus!='已退款' and it.OrderStatus!='退款审核不通过'");
            }
            else
            {
                sb.AppendFormat("and it.OrderStatus='{0}'", ddlOrderStatus.SelectedValue);
            }
            //验票时间
            if (txtValidTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.ValidTime>= DATETIME'{0} 00:00:00'", txtValidTime1.Text.Trim());
            }
            if (txtValidTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.ValidTime<=DATETIME'{0} 23:59:59'", txtValidTime2.Text.Trim());
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

        ETicket.Utility.PageInfo<EFEntity.OrderSheet>  LoadDataPageInfo(int currentPage, int pageSize)
        {
            ETicket.Utility.PageInfo<EFEntity.OrderSheet> pi = null;
            pi = BLL.OrderSheetBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.OrderTime DESC");
            return pi;
        }
        void LoadData(int currentPage, int pageSize)
        {
            var pi = LoadDataPageInfo(currentPage, pageSize);
            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
        }
    }
}