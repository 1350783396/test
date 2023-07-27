using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using NJiaSu.Libraries;



namespace ETicket.Web.business.partner
{
    public partial class tongji_order_count : PartnerBase
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
            DataTable dt= LoadDateTable(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
       
            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(dt);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(dt, colTxtArray);

            string fileName = "订单统计" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            DownLoadHelper.Instance.DownLoadFile(this, fileName, file);

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
            sb.AppendFormat(" where UserID={0}", cookiesUser.UserID);
            //状态筛选
            string key1=ETicket.Utility.OrderStatusEnum.已支付.ToString();
            string key2 = ETicket.Utility.OrderStatusEnum.已验票.ToString();
            string key3 = ETicket.Utility.OrderStatusEnum.已过期.ToString();

            //sb.AppendFormat(" and OrderStatus in ('{0}','{1}','{2}')",key1,key2,key3);
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

        DataTable LoadDateTable(int currentPage, int pageSize)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("订单状态");
            dt.Columns.Add("订单数量");

            string where=Where();
            string[] statusList = Utility.CovertHelper.ConvertOrderStatus2Array();
            foreach(string status in statusList)
            {
                string itemWhere = where + string.Format(" and OrderStatus='{0}'",status);
                string sql = " select count(*) from OrderSheet {0} ";
                sql = String.Format(sql, itemWhere);
                
                object obj = BLL.DbHelperSQL.GetSingle(sql);
                int count = 0;
                if (obj != null)
                    count = Convert.ToInt32(obj);

                DataRow row = dt.NewRow();
                row["订单状态"] = status;
                row["订单数量"] = count;
                dt.Rows.Add(row);
            }
           

            return dt;
        }
    }
}