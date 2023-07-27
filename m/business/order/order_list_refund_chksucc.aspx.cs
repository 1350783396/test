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

namespace ETicket.Web.business.order
{
    public partial class order_list_refund_chksucc : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefrech.Click += btnRefrech_Click;
            this.btnExcel.Click += btnExcel_Click;
            this.btnExcelALL.Click += btnExcelALL_Click;

            if (!Page.IsPostBack)
            {
                #region 绑定订单状态
                ddlOrderStatus.Items.Clear();
                
                ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已退款.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已取消.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已失效.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已支付.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已验证.ToString()));
                //ddlOrderStatus.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已过期.ToString()));
                #endregion

                #region 绑定下单人类型
                ddlUserLevel.Items.Clear();
                ddlUserLevel.Items.Add(new ListItem("所有类型"));
                IEnumerable<EFEntity.UserLevel> levelList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "user" | p.UserCategory == "partner").OrderBy(p => p.OrderValue);
                foreach(var level in levelList)
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

        #region 导出
        void btnExcelALL_Click(object sender, EventArgs e)
        {
            #region datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("订单号");
            dt.Columns.Add("产品名称");
            dt.Columns.Add("姓名");
            dt.Columns.Add("手机");
            dt.Columns.Add("购买数量");
            dt.Columns.Add("总价");
            dt.Columns.Add("状态");
            dt.Columns.Add("支付类型");
            dt.Columns.Add("级别");
            dt.Columns.Add("下单人");
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
                row["订单号"] = item.SheetID;
                row["产品名称"] = item.ProductName;
                row["姓名"] = item.RealName;
                row["手机"] = item.Phone;
                row["购买数量"] = item.NUM;
                row["总价"] = item.TotalPrice;
                row["状态"] = item.OrderStatus;
                row["支付类型"] = BLL.OrderSheetBLL.Instance.GetPayTypeStr(item);
                row["级别"] = row["级别"] = item.UserLevelName;
                row["下单人"] = item.CPName;

                dt.Rows.Add(row);
            }

            int count;
            decimal sum;
            SumPage(pi, out count, out sum);

            DataRow rowCount = dt.NewRow();
            rowCount["购买数量"] = string.Format("合计：{0}张", count);
            rowCount["总价"] = string.Format("合计：{0}元", sum);
            dt.Rows.Add(rowCount);

            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(dt);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(dt, colTxtArray);

            string fileName = "已退款" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            DownLoadHelper.Instance.DownLoadFile(this, fileName, file);
        }

        void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable wDT = WashDate(this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(wDT);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(wDT, colTxtArray);

            string fileName = "本页-已退款" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
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
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var order = e.Item.DataItem as EFEntity.OrderSheet;

                HyperLink hyDetail = e.Item.FindControl("hyDetail") as HyperLink;

                
                string detailUrl = string.Format(" /business/order/order_detail_chk_view.aspx?orderid={0}", order.OrderID);
                hyDetail.NavigateUrl = "#";
                hyDetail.Attributes.Add("onclick", PubFun.TabNav("aodc_" + order.OrderID, "退款-"+order.ProductName, detailUrl));
            }
        }

        #region 2个查询条件
        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            
            //sb.AppendFormat(" it.PayType='{0}'", "现金支付");
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
            if (ddlUserLevel.SelectedValue != "所有类型")
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
        string WhereForSql()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            StringBuilder sb = new StringBuilder();
            if (ddlOrderStatus.SelectedValue == "所有状态")
            {
                sb.Append(" OrderStatus!='退款审核中' and OrderStatus!='退款审核通过' and OrderStatus!='已退款' and OrderStatus!='退款审核不通过'");
            }
            else
            {
                sb.AppendFormat(" OrderStatus='{0}'", ddlOrderStatus.SelectedValue);
            }
            //姓名
            if (this.txtRealname.Text.Trim() != "")
            {
                sb.AppendFormat("and RealName='{0}'", this.txtRealname.Text.Trim());
            }
            //手机
            if (this.txtPhone.Text.Trim() != "")
            {
                sb.AppendFormat("and Phone='{0}'", this.txtPhone.Text.Trim());
            }
            //订单号
            if (this.txtSheetID.Text.Trim() != "")
            {
                sb.AppendFormat("and SheetID='{0}'", this.txtSheetID.Text.Trim());
            }
            //分销商
            if (txtSelValue.Value.Trim() != "")
            {
                sb.AppendFormat(" and UserID={0}", txtSelValue.Value.Trim());
            }
            //产品分类
            if (ddlProductCategory.SelectedValue != "所有类型")
            {
                sb.AppendFormat("and CategoryID='{0}'", ddlProductCategory.SelectedValue);
            }
            //下单人类型
            if (ddlUserLevel.SelectedValue != "所有类型")
            {
                sb.AppendFormat("and UserLevelName='{0}'", this.ddlUserLevel.SelectedValue);
            }
            //下单人账号
            if (this.txtUserName.Text.Trim() != "")
            {
                sb.AppendFormat("and UserName='{0}'", this.txtUserName.Text.Trim());
            }
            //产品名称
            if (this.txtProductName.Text.Trim() != "")
            {
                sb.AppendFormat("and ProductName like '%{0}%'", this.txtProductName.Text.Trim());
            }
            //订单时间
            if (txtOrderTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and OrderTime>='{0} 00:00:00'", txtOrderTime1.Text.Trim());
            }
            if (txtOrderTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and OrderTime<='{0} 23:59:59'", txtOrderTime2.Text.Trim());
            }
            return sb.ToString();
        }
        #endregion
        void LoadData(int currentPage, int pageSize)
        {
            var pi = LoadDataReturn(currentPage, pageSize);
            //统计所有
            int numTotal;
            decimal priceTotal;

            LoadTotalSum(out numTotal, out priceTotal);
            //统计本页
            int count;
            decimal sum;

            SumPage(pi, out count, out sum);
            litCount.Text = string.Format("本页合计：{0}张", count, numTotal);
            litSum.Text = string.Format("本页合计：{0}元", sum);
            

            litCountTotal.Text = string.Format("所有合计：{0}张", numTotal);
            litSumTotal.Text = string.Format("所有合计：{0}元", priceTotal);


            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();

        }

        ETicket.Utility.PageInfo<EFEntity.OrderSheet> LoadDataReturn(int currentPage, int pageSize)
        {
            ETicket.Utility.PageInfo<EFEntity.OrderSheet> pi = null;
            pi = BLL.OrderSheetBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.OrderTime DESC");

            return pi;
        }

        DataTable WashDate(int currentPage, int pageSize)
        {
            #region datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("订单号");
            dt.Columns.Add("产品名称");
            dt.Columns.Add("姓名");
            dt.Columns.Add("手机");
            dt.Columns.Add("购买数量");
            dt.Columns.Add("总价");
            dt.Columns.Add("状态");
            dt.Columns.Add("支付类型");
            dt.Columns.Add("级别");
            dt.Columns.Add("下单人");
            #endregion

            var pi = LoadDataReturn(currentPage, pageSize);
            foreach (var item in pi.List)
            {
                DataRow row = dt.NewRow();
                row["订单号"] = item.SheetID;
                row["产品名称"] = item.ProductName;
                row["姓名"] = item.RealName;
                row["手机"] = item.Phone;
                row["购买数量"] = item.NUM;
                row["总价"] = item.TotalPrice;
                row["状态"] = item.OrderStatus;
                row["支付类型"] = BLL.OrderSheetBLL.Instance.GetPayTypeStr(item);
                row["级别"] = item.UserLevelName;
                row["下单人"] = item.CPName;
              

                dt.Rows.Add(row);
            }

            int count;
            decimal sum;
            SumPage(pi, out count, out sum);

            DataRow rowCount = dt.NewRow();
            rowCount["购买数量"] = string.Format("本页合计：{0}张", count);
            rowCount["总价"] = string.Format("本页合计：{0}元", sum);
          
            dt.Rows.Add(rowCount);

            //统计所有
            int numTotal;
            decimal priceTotal;

            LoadTotalSum(out numTotal, out priceTotal);

            DataRow rowTotal = dt.NewRow();
            rowTotal["购买数量"] = string.Format("所有合计：{0}张", numTotal);
            rowTotal["总价"] = string.Format("所有合计：{0}元", priceTotal);
            dt.Rows.Add(rowTotal);
            return dt;

        }

        #region 计算合计
        public void LoadTotalSum(out int sumNum, out decimal sumPrice)
        {
            sumNum = 0;
            sumPrice = 0;

            string sql = " select sum(NUM) as num ,sum(TotalPrice) as price  from OrderSheet";
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

            }

        }
        void SumPage(ETicket.Utility.PageInfo<EFEntity.OrderSheet> pi, out int count, out decimal sum)
        {
            count = 0;
            sum = 0;
            foreach (var item in pi.List)
            {
                count = count + item.NUM.Value;
                sum = sum + item.TotalPrice.Value;
            }
        }
        #endregion
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