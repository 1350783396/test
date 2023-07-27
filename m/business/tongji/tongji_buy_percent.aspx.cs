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
    public partial class tongji_buy_percent : AdminBase
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
            //DataTable wDT = WashTable(dt);
            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(dt);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(dt, colTxtArray);

            string fileName="购买比例" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
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

            sb.AppendFormat(" and OrderStatus in ('{0}','{1}','{2}')", key1, key2, key3);
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
            dt.Columns.Add("会员级别");
            dt.Columns.Add("订单数量");
            dt.Columns.Add("订单比例");
            dt.Columns.Add("购买产品数量");
            dt.Columns.Add("产品比例");
            dt.Columns.Add("购买总金额");
            dt.Columns.Add("金额比例");

            string where=Where();

            #region 获取数据
            IEnumerable<EFEntity.UserLevel> userLevelList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "user" | p.UserCategory == "partner").OrderBy(p => p.OrderValue);
            foreach(var userLevel in userLevelList)
            {
                string levelName = userLevel.UserLevelName;
                string whereItem = where + string.Format(" and UserLevelName='{0}'", levelName);
                string sql = " select count(*) as OrderCount,sum(NUM) as BuySum,sum(TotalPrice) as PriceSum from OrderSheet {0} ";
                sql = String.Format(sql, whereItem);
                DataSet ods = BLL.DbHelperSQL.Query(sql);

                DataRow newRow = dt.NewRow();
                if(ods.Tables[0].Rows.Count>=1)
                {
                    DataRow row = ods.Tables[0].Rows[0];
                    //订单量
                    string OrderCount=row["OrderCount"].ToString();
                    if(OrderCount=="")
                        OrderCount="0";
                    //产品数量
                    string BuySum = row["BuySum"].ToString();
                    if (BuySum == "")
                        BuySum = "0";
                    //总额
                    string PriceSum= row["PriceSum"].ToString();
                    if(PriceSum=="")
                        PriceSum="0";

                    newRow["会员级别"] = levelName;
                    newRow["订单数量"] = OrderCount;
                    newRow["购买产品数量"] = BuySum;
                    newRow["购买总金额"] = PriceSum;
                }
                else
                {
                    newRow["会员级别"] = levelName;
                    newRow["订单数量"] = 0;
                    newRow["购买产品数量"] = 0;
                    newRow["购买总金额"] = 0;
                }
                dt.Rows.Add(newRow);
            }
            #endregion

            #region 统计 计算比例
            int sumOrderCount=0, sumBuySum=0;
            decimal sumPriceSum=0;
            foreach(DataRow row in dt.Rows)
            {
                int OrderCount = 0, BuySum = 0;
                decimal PriceSum = 0;

                int.TryParse(row["订单数量"].ToString(), out OrderCount);
                int.TryParse(row["购买产品数量"].ToString(), out BuySum);
                decimal.TryParse(row["购买总金额"].ToString(), out PriceSum);

                sumOrderCount += OrderCount;
                sumBuySum += BuySum;
                sumPriceSum += PriceSum;
            }
            foreach (DataRow row in dt.Rows)
            {
                int OrderCount = 0, BuySum = 0;
                decimal PriceSum = 0;

                int.TryParse(row["订单数量"].ToString(), out OrderCount);
                int.TryParse(row["购买产品数量"].ToString(), out BuySum);
                decimal.TryParse(row["购买总金额"].ToString(), out PriceSum);

                //订单比例
                if(sumOrderCount==0)
                {
                    row["订单比例"] = "0%";
                }
                else
                {
                    row["订单比例"] =Math.Round(Convert.ToDecimal(OrderCount)/Convert.ToDecimal(sumOrderCount),2)*100+"%";
                }
                //产品比例
                if(sumBuySum==0)
                {
                    row["产品比例"] = "0%";
                }
                else
                {
                    row["产品比例"] = Math.Round(Convert.ToDecimal(BuySum) / Convert.ToDecimal(sumBuySum), 2) * 100 + "%";
                }
                //金额比例
                if(sumPriceSum==0)
                {
                    row["金额比例"] = "0%";
                }
                else
                {
                    row["金额比例"] = Math.Round(PriceSum / sumPriceSum, 2) * 100 + "%";
                }
            }

            #endregion

            return dt;
        }

      
    }
}