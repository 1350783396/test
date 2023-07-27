using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;
using System.Text;
using System.Data;

namespace ETicket.Web.business.orderview_dict
{
    public partial class order_query :OrderViewBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefrech.Click += btnRefrech_Click;
            this.btnExcel.Click += btnExcel_Click;
            this.btnExcelALL.Click += btnExcelALL_Click;
            //this.btnDel2.Click += btnDel_Click;

            chkAll.Attributes.Add("onclick", string.Format("javascript:FormSelectAllEnable('{0}','chkItem',this);", this.form1.ClientID));

            if (!Page.IsPostBack)
            {
                txtOrderTime1.Text = DateTime.Now.ToString("yyyy-MM-dd");

                #region 绑定DDL
                #region 绑定订单状态
                ddlOrderStatus.Items.Clear();
                ddlOrderStatus.Items.Add(new ListItem("所有状态"));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.待付款.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已取消.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已失效.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已支付.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已验票.ToString()));
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已过期.ToString()));
                #endregion

                #region 产品类型
                ddlProductCategory.Items.Clear();
                ddlProductCategory.Items.Add(new ListItem("所有类型"));
                ddlProductCategory.Items.Add(new ListItem("专线", "line"));
                ddlProductCategory.Items.Add(new ListItem("景区", "ticket"));
                #endregion


                #endregion

                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }

        #region 导出
        void btnExcelALL_Click(object sender, EventArgs e)
        {
            #region datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("销售人");
            dt.Columns.Add("销售电话");
            dt.Columns.Add("产品名称");
            dt.Columns.Add("购买数量");
            dt.Columns.Add("单价");
            dt.Columns.Add("总价");
            dt.Columns.Add("返现积分");
            dt.Columns.Add("购买时间");
            dt.Columns.Add("支付类型");

            dt.Columns.Add("发车时间");
            dt.Columns.Add("上车地点");
            dt.Columns.Add("订单号");
            dt.Columns.Add("验票时间");

            dt.Columns.Add("备注");
            #endregion

            //先检查数据都否打印10000条
            string sql = " select count(OrderID) as recordcount from OrderSheet";
            string where = WhereForSql();
            if (where != "")
                sql = sql + " where " + where;
            object obj = BLL.DbHelperSQL.GetSingle(sql);
            if (obj != null)
            {
                if (Convert.ToInt32(obj) > 10000)
                {
                    PubFun.ShowMsg(this, "导出的数据大于1万条，太多啦！无法导出。请按日期查询数据，按时间段导出。");
                    return;
                }
            }

            var pi = LoadDataReturn(1, 10000);

            foreach (var item in pi.List)
            {
                DataRow row = dt.NewRow();
                row["销售人"] = item.CPName;
                row["销售电话"] = item.CPTel;
                row["产品名称"] = item.ProductName;
                row["购买数量"] = item.NUM;
                row["单价"] = item.UnitPrice;
                row["总价"] = item.TotalPrice;
                row["返现积分"] = item.RebateTotal == null ? "0" : item.RebateTotal.ToString();
                row["购买时间"] = item.OrderTime;
                row["支付类型"] = BLL.OrderSheetBLL.Instance.GetPayTypeStr(item);

                if (item.StartTime != null)
                    row["发车时间"] = item.StartTime.Value.ToString("yyyy-MM-dd HH:mm");

                row["上车地点"] = item.StartAddress;
                row["订单号"] = item.SheetID;
                row["验票时间"] = item.ValidTime;
                row["备注"] = item.Memo;

                dt.Rows.Add(row);
            }

            int count;
            decimal sum, sumRebate;
            SumPage(pi, out count, out sum, out sumRebate);

            DataRow rowCount = dt.NewRow();
            rowCount["购买数量"] = string.Format("合计：{0}张", count);
            rowCount["总价"] = string.Format("合计：{0}元", sum);
            rowCount["返现积分"] = string.Format("合计：{0}", sumRebate);
            dt.Rows.Add(rowCount);

            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(dt);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(dt, colTxtArray);

            string fileName = "销售明细表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            DownLoadHelper.Instance.DownLoadFile(this, fileName, file);
        }

        void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable wDT = WashDate(this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(wDT);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(wDT, colTxtArray);

            string fileName = "本页-销售明细表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            DownLoadHelper.Instance.DownLoadFile(this, fileName, file);
        }
        #endregion

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
                //CheckBox checkALL = e.Item.FindControl("chkAll") as CheckBox;
                //checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAllEnable('{0}','chkItem',this);", this.form1.ClientID));
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

                Literal litPayType = e.Item.FindControl("litPayType") as Literal;
                litPayType.Text = BLL.OrderSheetBLL.Instance.GetPayTypeStr(order);

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
            if (e.Item.ItemType == ListItemType.Footer)
            {

            }
        }

        #region 查询条件2个
        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            StringBuilder sbPrint = new StringBuilder();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            //sb.AppendFormat(" it.UserID={0}", cookiesUser.UserID);
            //状态筛选
            string key1 = ETicket.Utility.OrderStatusEnum.已支付.ToString();
            string key2 = ETicket.Utility.OrderStatusEnum.已验票.ToString();
            string key3 = ETicket.Utility.OrderStatusEnum.已过期.ToString();

            if (ddlOrderStatus.SelectedValue == "所有状态")
            {
                sb.AppendFormat(" (it.OrderStatus ='{0}' or it.OrderStatus ='{1}' or it.OrderStatus ='{2}')", key1, key2, key3);
            }
            else
            {
                sb.AppendFormat(" it.OrderStatus='{0}'", ddlOrderStatus.SelectedValue);
            }

            string userBuilder = "";
            int userID=base.CookiesUser.UserID;
            IEnumerable<EFEntity.OrderView> list = BLL.OrderViewBLL.Instance.GetEntities(p => p.UserID == userID);
            foreach(var m in list)
            {
                if(userBuilder=="")
                {
                    userBuilder =" it.ProductID=" + m.ProductID;
                }
                else
                {
                    userBuilder = userBuilder + " or it.ProductID=" + m.ProductID;
                }
            }
            if (userBuilder!="")
            {
                sb.AppendFormat(" and ({0})", userBuilder);
            }

            //订单时间
            if (txtOrderTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.OrderTime>=DATETIME'{0} 00:00:00' and it.OrderTime<=DATETIME'{0} 23:59:59'", txtOrderTime1.Text.Trim());
            }
            else
            {
                sb.AppendFormat(" and it.OrderTime>=DATETIME'{0} 00:00:00' and it.OrderTime<=DATETIME'{0} 23:59:59'", DateTime.Now.ToString("yyyy-MM-dd"));
            }

            /*
            if (txtOrderTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.OrderTime<=DATETIME'{0} 23:59:59'", txtOrderTime2.Text.Trim());
            }
            */

            //验票时间
            if (txtValidTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.ValidTime>=DATETIME'{0} 00:00:00'", txtValidTime1.Text.Trim());
            }
            if (txtValidTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.ValidTime<=DATETIME'{0} 23:59:59'", txtValidTime2.Text.Trim());
            }
          


            if (ddlProductCategory.SelectedValue != "所有类型")
            {
                sb.AppendFormat("and it.CategoryID='{0}'", ddlProductCategory.SelectedValue);
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
          
            //订单来源
            if (ddlClientType.SelectedValue != "")
            {
                sb.AppendFormat("and it.ClientType = '{0}'", ddlClientType.SelectedValue);
            }
            return sb.ToString();
        }
        string WhereForSql()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            StringBuilder sbPrint = new StringBuilder();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            //sb.AppendFormat(" it.UserID={0}", cookiesUser.UserID);
            //状态筛选
            string key1 = ETicket.Utility.OrderStatusEnum.已支付.ToString();
            string key2 = ETicket.Utility.OrderStatusEnum.已验票.ToString();
            string key3 = ETicket.Utility.OrderStatusEnum.已过期.ToString();

            if (ddlOrderStatus.SelectedValue == "所有状态")
            {
                sb.AppendFormat(" ( OrderStatus ='{0}' or OrderStatus ='{1}' or OrderStatus ='{2}')", key1, key2, key3);
            }
            else
            {
                sb.AppendFormat(" OrderStatus='{0}'", ddlOrderStatus.SelectedValue);
            }

            //订单时间
            if (txtOrderTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and OrderTime>='{0} 00:00:00' and OrderTime<='{0} 23:59:59'", txtOrderTime1.Text.Trim());
            }
            else
            {
                sb.AppendFormat(" and OrderTime>='{0} 00:00:00' and OrderTime<='{0} 23:59:59'", DateTime.Now.ToString("yyyy-MM-dd"));
            }
            /*
            if (txtOrderTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and OrderTime<='{0} 23:59:59'", txtOrderTime2.Text.Trim());
            }
            */
            //验票时间
            if (txtValidTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and ValidTime>='{0} 00:00:00'", txtValidTime1.Text.Trim());
            }
            if (txtValidTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and ValidTime<='{0} 23:59:59'", txtValidTime2.Text.Trim());
            }
            
            if (ddlProductCategory.SelectedValue != "所有类型")
            {
                sb.AppendFormat("and CategoryID='{0}'", ddlProductCategory.SelectedValue);
            }
        
            //订单号
            if (this.txtSheetID.Text.Trim() != "")
            {
                sb.AppendFormat("and SheetID='{0}'", this.txtSheetID.Text.Trim());
            }
            //产品名称
            if (this.txtProductName.Text.Trim() != "")
            {
                sb.AppendFormat("and ProductName like '%{0}%'", this.txtProductName.Text.Trim());
            }
          
            //订单来源
            if (ddlClientType.SelectedValue != "")
            {
                sb.AppendFormat("and ClientType = '{0}'", ddlClientType.SelectedValue);
            }
            return sb.ToString();
        }
        #endregion

        ETicket.Utility.PageInfo<EFEntity.OrderSheet> LoadDataReturn(int currentPage, int pageSize)
        {
            ETicket.Utility.PageInfo<EFEntity.OrderSheet> pi = null;
            pi = BLL.OrderSheetBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.OrderTime DESC");

            return pi;
        }

        void LoadData(int currentPage, int pageSize)
        {
            var pi = LoadDataReturn(currentPage, pageSize);

            //统计所有
            int numTotal;
            decimal priceTotal;
            decimal rebateTotal;

            LoadTotalSum(out numTotal, out priceTotal, out rebateTotal);
            //统计本页
            int count;
            decimal sum;
            decimal sumRebate;
            SumPage(pi, out count, out sum, out sumRebate);

            /*
            litCount.Text = string.Format("本页合计：{0}张", count, numTotal);
            litSum.Text = string.Format("本页合计：{0}元", sum);
            litSumRebate.Text = string.Format("本页合计：{0}", sumRebate);

            litCountTotal.Text = string.Format("所有合计：{0}张", numTotal);
            litSumTotal.Text = string.Format("所有合计：{0}元", priceTotal);
            litSumRebateTotal.Text = string.Format("所有合计：{0}", rebateTotal);
            */

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();

            /*
            this.rptPrint.DataSource = pi.List;
            this.rptPrint.DataBind();
            int count = pi.List.Count();
            int sum = 0;
            foreach(var item in pi.List)
            {
                sum = sum + item.NUM.Value;
            }
            litPrintCount.Text = string.Format("{0}条记录，合{1}人", count, sum);
            */
        }
        DataTable WashDate(int currentPage, int pageSize)
        {
            #region datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("销售人");
            dt.Columns.Add("销售电话");
            dt.Columns.Add("产品名称");
            dt.Columns.Add("购买数量");
            dt.Columns.Add("单价");
            dt.Columns.Add("总价");
            dt.Columns.Add("返现积分");
            dt.Columns.Add("购买时间");
            dt.Columns.Add("支付类型");

            dt.Columns.Add("发车时间");
            dt.Columns.Add("上车地点");
            dt.Columns.Add("订单号");
            dt.Columns.Add("验票时间");

            dt.Columns.Add("备注");
            #endregion

            var pi = LoadDataReturn(currentPage, pageSize);
            foreach (var item in pi.List)
            {
                DataRow row = dt.NewRow();
                row["销售人"] = item.CPName;
                row["销售电话"] = item.CPTel;
                row["产品名称"] = item.ProductName;
                row["购买数量"] = item.NUM;
                row["单价"] = item.UnitPrice;
                row["总价"] = item.TotalPrice;
                row["返现积分"] = item.RebateTotal == null ? "0" : item.RebateTotal.ToString();
                row["购买时间"] = item.OrderTime;
                row["支付类型"] = BLL.OrderSheetBLL.Instance.GetPayTypeStr(item);

                if (item.StartTime != null)
                    row["发车时间"] = item.StartTime.Value.ToString("yyyy-MM-dd HH:mm");

                row["上车地点"] = item.StartAddress;
                row["订单号"] = item.SheetID;
                row["验票时间"] = item.ValidTime;
                row["备注"] = item.Memo;

                dt.Rows.Add(row);
            }

            int count=0;
            decimal sum=0, sumRebate=0;
            //SumPage(pi, out count, out sum, out sumRebate);

            DataRow rowCount = dt.NewRow();
            rowCount["购买数量"] = string.Format("本页合计：{0}张", count);
            rowCount["总价"] = string.Format("本页合计：{0}元", sum);
            rowCount["返现积分"] = string.Format("本页合计：{0}", sumRebate);
            dt.Rows.Add(rowCount);

            //统计所有
            int numTotal;
            decimal priceTotal;
            decimal rebateTotal;
            LoadTotalSum(out numTotal, out priceTotal, out rebateTotal);

            DataRow rowTotal = dt.NewRow();
            rowTotal["购买数量"] = string.Format("所有合计：{0}张", numTotal);
            rowTotal["总价"] = string.Format("所有合计：{0}元", priceTotal);
            rowTotal["返现积分"] = string.Format("所有合计：{0}", rebateTotal);
            dt.Rows.Add(rowTotal);
            return dt;

        }

        #region 计算合计
        public void LoadTotalSum(out int sumNum, out decimal sumPrice, out decimal rebateTotal)
        {
            sumNum = 0;
            sumPrice = 0;
            rebateTotal = 0;

            string sql = " select sum(NUM) as num ,sum(TotalPrice) as price ,sum(RebateTotal) as rebate from OrderSheet";
            string where = WhereForSql();
            if (where != "")
                sql = sql + " where " + where;

            DataSet ods = BLL.DbHelperSQL.Query(sql);
            if (ods.Tables[0].Rows.Count == 1)
            {
                int sumValue = 0;
                if (ods.Tables[0].Rows[0]["num"] != DBNull.Value)
                    int.TryParse(ods.Tables[0].Rows[0]["num"].ToString(), out sumValue);
                sumNum = sumValue;

                decimal priceValue = 0;
                if (ods.Tables[0].Rows[0]["price"] != DBNull.Value)
                    decimal.TryParse(ods.Tables[0].Rows[0]["price"].ToString(), out priceValue);
                sumPrice = priceValue;

                decimal rebateValue = 0;
                if (ods.Tables[0].Rows[0]["rebate"] != DBNull.Value)
                    decimal.TryParse(ods.Tables[0].Rows[0]["rebate"].ToString(), out rebateValue);
                rebateTotal = rebateValue;

            }

        }
        void SumPage(ETicket.Utility.PageInfo<EFEntity.OrderSheet> pi, out int count, out decimal sum, out decimal sumRebate)
        {
            count = 0;
            sum = 0;
            sumRebate = 0;
            foreach (var item in pi.List)
            {
                count = count + item.NUM.Value;
                sum = sum + item.TotalPrice.Value;
                sumRebate = sumRebate + (item.RebateTotal == null ? 0 : item.RebateTotal.Value);
            }
        }
        #endregion
    }
}