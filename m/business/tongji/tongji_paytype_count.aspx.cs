using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using System.Data;
using System.Collections;
using NJiaSu.Libraries;



namespace ETicket.Web.business.tongji
{
    public partial class tongji_paytype_count : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnExcel.Click += btnExcel_Click;
            if (!Page.IsPostBack)
            {
                #region 绑定DDL
           
                ddlProductCategory.Items.Clear();
                ddlProductCategory.Items.Add(new ListItem("所有类型"));
                ddlProductCategory.Items.Add(new ListItem("专线", "line"));
                ddlProductCategory.Items.Add(new ListItem("景区", "ticket"));
                #endregion

                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }

        void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = LoadDateTable(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            DataTable wDT = WashTable(dt);
            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(wDT);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(wDT, colTxtArray);

            string fileName="支付方式统计" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            DownLoadHelper.Instance.DownLoadFile(this,fileName, file);
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
                //var order = e.Item.DataItem as EFEntity.OrderSheet;

            }
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            //sb.AppendFormat(" where UserID={0}", cookiesUser.UserID);
            sb.Append(" where 1=1 ");
            //状态筛选
            string key1=ETicket.Utility.OrderStatusEnum.已支付.ToString();
            string key2 = ETicket.Utility.OrderStatusEnum.已验票.ToString();
            string key3 = ETicket.Utility.OrderStatusEnum.已过期.ToString();

            sb.AppendFormat(" and OrderStatus in ('{0}','{1}','{2}')",key1,key2,key3);
            //订单时间
            if (txtOrderTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and OrderTime>='{0} 00:00:00'", txtOrderTime1.Text.Trim());
            }
            if (txtOrderTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and OrderTime<='{0} 23:59:59'", txtOrderTime2.Text.Trim());
            }
            //产品类型
            if (ddlProductCategory.SelectedValue != "所有类型")
            {
                sb.AppendFormat("and CategoryID='{0}'", ddlProductCategory.SelectedValue);
            }
            //支付方式
            //if (ddlPayType.SelectedValue != "所有方式")
            //{
            //    sb.AppendFormat("and PayType='{0}'", ddlPayType.SelectedValue);
            //}
            return sb.ToString();
        }
        void LoadData(int currentPage, int pageSize)
        {

          
            //this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            DataTable dt = LoadDateTable(currentPage, pageSize);
            this.repList.DataSource = dt;
            this.repList.DataBind();
        }

        //查询数据库
        DataTable LoadDateTable(int currentPage, int pageSize)
        {
            DataTable dt = new DataTable();
            string where=Where();
            string sql = "select COUNT(*) as OrderCount,sum(TotalPrice) as PriceSum,PayType from OrderSheet {0} group by PayType";
            sql = String.Format(sql, where);
            DataSet ods=  BLL.DbHelperSQL.Query(sql);
            if (ods != null)
                dt = ods.Tables[0];

            #region 补缺少的方式
            bool isOnline = false, isAccount = false, isCash = false;
            foreach(DataRow row in dt.Rows)
            {
                string payType=row["PayType"].ToString();
                if (payType == "现金支付")
                    isCash = true;
                else if (payType == "积分支付")
                    isAccount = true;
                else if (payType == "在线支付")
                    isOnline = true;
            }
            if (!isOnline)
            {
                DataRow row = dt.NewRow();
                row["PayType"] = "在线支付";
                row["OrderCount"] = 0;
                row["PriceSum"] = 0;
                dt.Rows.Add(row);
            }
            if (!isAccount)
            {
                DataRow row = dt.NewRow();
                row["PayType"] = "积分支付";
                row["OrderCount"] = 0;
                row["PriceSum"] = 0;
                dt.Rows.Add(row);
            }
            if (!isCash)
            {
                DataRow row = dt.NewRow();
                row["PayType"] = "现金支付";
                row["OrderCount"] = 0;
                row["PriceSum"] = 0;
                dt.Rows.Add(row);
            }
            #endregion

            return dt;
        }

        //整理格式
        DataTable WashTable(DataTable inDT)
        {
            DataTable outDT = new DataTable();
            outDT.Columns.Add("支付方式");
            outDT.Columns.Add("订单数量");
            outDT.Columns.Add("订单总额");

            foreach (DataRow inRow in inDT.Rows)
            {
                DataRow outRow = outDT.NewRow();

                outRow["支付方式"] = inRow["PayType"].ToString();
                outRow["订单数量"] = inRow["OrderCount"].ToString();
                outRow["订单总额"] = inRow["PriceSum"].ToString();
                outDT.Rows.Add(outRow);
           }

            return outDT;
        }
    }
}