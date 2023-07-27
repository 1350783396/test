using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using NJiaSu.Libraries;
using System.Data;


namespace ETicket.Web.business.user
{
    public partial class account_query_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefresh.Click += btnRefresh_Click;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnExcelALL.Click += btnExcelALL_Click;
            if (!Page.IsPostBack)
            {
                IEnumerable<EFEntity.UserLevel> userTypeList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "partner").OrderBy(p => p.OrderValue);
                ddlUserLevel.Items.Clear();
                ddlUserLevel.Items.Add(new ListItem("所有级别", "0"));
                foreach (var userLevel in userTypeList)
                {
                    ddlUserLevel.Items.Add(new ListItem(userLevel.UserLevelName, userLevel.UserLevelID.ToString()));
                }

                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }
        void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void btnQuery_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        //导出所有到Excel
        void btnExcelALL_Click(object sender, EventArgs e)
        {
            #region datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("用户名");
            dt.Columns.Add("联系人");
            dt.Columns.Add("联系号码");
            dt.Columns.Add("分销商名称");
            dt.Columns.Add("级别");
            dt.Columns.Add("当前可用积分");
          
            #endregion

            string sql = " select * from [User] it ";
            string where = Where();
            if (where != "")
                sql = sql + " where " + where;

            DataSet ods = BLL.DbHelperSQL.Query(sql);
            foreach (DataRow row in ods.Tables[0].Rows)
            {
                DataRow newRow = dt.NewRow();
                newRow["用户名"] = row["UserName"].ToString(); ;
                newRow["联系人"] = row["RealName"].ToString(); ;
                newRow["联系号码"] = row["Phone"].ToString(); ;
                newRow["分销商名称"] = row["CPName"].ToString(); ;
                newRow["当前可用积分"] = row["Account"].ToString();

                int UserLevelID = 0;
                int.TryParse(row["UserLevelID"].ToString(), out UserLevelID);
                EFEntity.UserLevel userLevel = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelID == UserLevelID);
                newRow["级别"] = userLevel==null?"":userLevel.UserLevelName;

                dt.Rows.Add(newRow);
            }


            //添加合计行
            decimal priceTotal;
            LoadTotalSum(out priceTotal);
            DataRow rowCount = dt.NewRow();
            rowCount["当前可用积分"] = string.Format("总共合计：{0}", priceTotal);
            dt.Rows.Add(rowCount);

            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(dt);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(dt, colTxtArray);

            string fileName = "积分余额" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            DownLoadHelper.Instance.DownLoadFile(this, fileName, file);
        }

        string Where()
        {
            StringBuilder sb = new StringBuilder();
            //自己的订单
            sb.Append(" it.UserCategory='partner'");
            //类型
            if (ddlUserLevel.SelectedValue != "0")
            {
                sb.AppendFormat(" and it.UserLevelID={0}", ddlUserLevel.SelectedValue);
            }
            if (txtUserName.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.UserName='{0}'", txtUserName.Text.Trim());
            }
            if (txtPhone.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.Phone='{0}'", txtPhone.Text.Trim());
            }
            if (txtCPName.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.CPName like '%{0}%'", txtCPName.Text.Trim());
            }
            return sb.ToString();
        }
        void LoadData(int currentPage, int pageSize)
        {

            ETicket.Utility.PageInfo<EFEntity.User> pi = null;
            pi = BLL.UserBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.UserID DESC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();


            decimal priceTotal;
            decimal pageTotal;
            LoadTotalSum(out priceTotal);
            //统计本月
            SumPage(pi, out pageTotal);
            litCount.Text = string.Format("本页合计：{0}<br/><br/>总共合计：{1}", pageTotal, priceTotal);

            
        }
        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var user = e.Item.DataItem as EFEntity.User;

                Literal litLevelName = e.Item.FindControl("litLevelName") as Literal;
                EFEntity.UserLevel userLevel= BLL.UserLevelBLL.Instance.GetEntity(p=>p.UserLevelID==user.UserLevelID);
                litLevelName.Text = userLevel.UserLevelName;

                //Edit
                HyperLink hyEdit = e.Item.FindControl("hyEdit") as HyperLink;
                string editUrl = string.Format("/business/user/partner_edit.aspx?userid={0}", user.UserID);
                hyEdit.NavigateUrl = "#";
                hyEdit.Attributes.Add("onclick", PubFun.TabNav("u_edit" + user.UserID, "编辑-" + user.UserName, editUrl));
            }
        }

        public void LoadTotalSum(out decimal sumPrice)
        {
            sumPrice = 0;

            string sql = " select sum(Account) as price  from [User] it ";
            string where = Where();
            if (where != "")
                sql = sql + " where " + where;

            DataSet ods = BLL.DbHelperSQL.Query(sql);
            if (ods.Tables[0].Rows.Count == 1)
            {
               
                decimal priceValue = 0;
                if (ods.Tables[0].Rows[0]["price"] != DBNull.Value)
                    decimal.TryParse(ods.Tables[0].Rows[0]["price"].ToString(), out priceValue);
                sumPrice = priceValue;
            }

        }
        void SumPage(ETicket.Utility.PageInfo<EFEntity.User> pi,  out decimal sum)
        {
           
            sum = 0;
            
            foreach (var item in pi.List)
            {
                sum = sum + item.Account.Value;
            }
        }


    }
}