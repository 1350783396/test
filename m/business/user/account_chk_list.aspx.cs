using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Data;
using System.Text;
using NJiaSu.Libraries;


namespace ETicket.Web.business.user
{
    public partial class account_chk_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefresh.Click += btnRefresh_Click;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnExcel.Click += btnExcel_Click;
            this.btnExcelALL.Click += btnExcelALL_Click;

            if (!Page.IsPostBack)
            {
                #region 级别
                IEnumerable<EFEntity.UserLevel> userTypeList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "partner").OrderBy(p => p.OrderValue);
                ddlUserLevel.Items.Clear();
                ddlUserLevel.Items.Add(new ListItem("所有级别", "0"));
                foreach (var userLevel in userTypeList)
                {
                    ddlUserLevel.Items.Add(new ListItem(userLevel.UserLevelName, userLevel.UserLevelID.ToString()));
                }
                #endregion

                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }

        #region 导出
        void btnExcelALL_Click(object sender, EventArgs e)
        {
            #region datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("账号");
            dt.Columns.Add("联系号码");
            dt.Columns.Add("分销商名称");
            dt.Columns.Add("级别");
            dt.Columns.Add("充值金额");
            dt.Columns.Add("申请日期");
            dt.Columns.Add("申请人");
            dt.Columns.Add("当前状态");
            dt.Columns.Add("审核人");
            dt.Columns.Add("审核时间");
            #endregion

            //先检查数据都否打印10000条
            string sql = " select count(PKID) as recordcount from [vAccountFlow]";
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
                //申请人级别
                var UserLevelID = item.UserLevelID.Value;
                EFEntity.UserLevel userLevel = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelID == UserLevelID);
                var LevelName =userLevel==null?"": userLevel.UserLevelName;

                //申请人
                var rUserID = item.Requst_UserID.Value;
                EFEntity.User rUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == rUserID);
                var RequestUser =rUser==null?"": rUser.UserName;

                //审核人
                var chkUserID=item.CHK_UserID.Value;
                EFEntity.User chkUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == chkUserID);
                var chkUserName =chkUser==null?"": chkUser.UserName;
                DataRow row = dt.NewRow();
                row["账号"] = item.UserName;
                row["联系号码"] = item.Phone;
                row["分销商名称"] = item.CPName;
                row["级别"] = LevelName;
                row["充值金额"] = item.Requst_Amount;
                row["申请日期"] = item.Requst_Time;
                row["申请人"] = RequestUser;
                row["当前状态"] = item.Stauts;
                row["审核人"] = chkUserName;
                row["审核时间"] = item.CHK_Time;

                dt.Rows.Add(row);
            }

            //统计
            decimal sumTotal;
            decimal sumPage;

            LoadTotalSum(out sumTotal);
            SumPage(pi, out sumPage);

            DataRow rowPage = dt.NewRow();
            rowPage["充值金额"] = string.Format("本页合计：{0}", sumPage);

            DataRow rowSum = dt.NewRow();
            rowSum["充值金额"] = string.Format("所有合计：{0}", sumTotal);
            dt.Rows.Add(rowPage);
            dt.Rows.Add(rowSum);

            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(dt);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(dt, colTxtArray);

            string fileName = "充值审核" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            DownLoadHelper.Instance.DownLoadFile(this, fileName, file);
        }

        void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable wDT = WashDate(this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
            //列头
            ArrayList colTxtArray = PubFun.GetDataTableColName(wDT);
            //生成excel
            System.IO.MemoryStream file = Data2Excel.AutoAddCol(wDT, colTxtArray);

            string fileName = "本页-充值审核" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            DownLoadHelper.Instance.DownLoadFile(this, fileName, file);
        }
        #endregion

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
        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var flow = e.Item.DataItem as EFEntity.vAccountFlow;

                Literal litLevelName = e.Item.FindControl("litLevelName") as Literal;
                EFEntity.UserLevel userLevel = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelID == flow.UserLevelID);
                if (userLevel!=null)
                    litLevelName.Text = userLevel.UserLevelName;
              
                Literal litRequestUser = e.Item.FindControl("litRequestUser") as Literal;
                EFEntity.User rUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == flow.Requst_UserID.Value);
                if (rUser != null)
                    litRequestUser.Text = rUser.UserName;


                Literal litChkUser = e.Item.FindControl("litChkUser") as Literal;
                if (flow.CHK_UserID != null)
                {
                    var chkUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == flow.CHK_UserID);
                    if (chkUser!=null)
                    litChkUser.Text = chkUser.UserName;
                }

                //Edit
                HyperLink hyEdit = e.Item.FindControl("hyEdit") as HyperLink;
                hyEdit.Visible = false;
                if (flow.Stauts == ETicket.Utility.AccountFlowStatusEnum.申请中.ToString())
                {
                    hyEdit.Visible = true;
                    string editUrl = string.Format("/business/user/account_chk_exec.aspx?pkid={0}", flow.PKID);
                    hyEdit.NavigateUrl = "#";
                    hyEdit.Attributes.Add("onclick", PubFun.TabNav("alog" + flow.PKID, "积分审核", editUrl));
                }

                HyperLink hyDetail = e.Item.FindControl("hyDetail") as HyperLink;
                string detailUrl = string.Format("/business/user/account_chk_detail.aspx?pkid={0}", flow.PKID);
                hyDetail.NavigateUrl = "#";
                hyDetail.Attributes.Add("onclick", PubFun.TabNav("alog" + flow.PKID, "积分审核详细", detailUrl));
            }
        }

        #region 查询条件
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
        string WhereForSql()
        {
            StringBuilder sb = new StringBuilder();
            //自己的订单
            sb.Append(" UserCategory='partner'");
            //类型
            if (ddlUserLevel.SelectedValue != "0")
            {
                sb.AppendFormat(" and UserLevelID={0}", ddlUserLevel.SelectedValue);
            }
            if (txtUserName.Text.Trim() != "")
            {
                sb.AppendFormat(" and UserName='{0}'", txtUserName.Text.Trim());
            }
            if (txtPhone.Text.Trim() != "")
            {
                sb.AppendFormat(" and Phone='{0}'", txtPhone.Text.Trim());
            }
            if (txtCPName.Text.Trim() != "")
            {
                sb.AppendFormat(" and CPName like '%{0}%'", txtCPName.Text.Trim());
            }
            return sb.ToString();
        }
        #endregion

        ETicket.Utility.PageInfo<EFEntity.vAccountFlow> LoadDataReturn(int currentPage, int pageSize)
        {
            ETicket.Utility.PageInfo<EFEntity.vAccountFlow> pi = null;
            pi = BLL.vAccountFlowBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.PKID DESC");

            return pi;
        }

        void LoadData(int currentPage, int pageSize)
        {
            var pi = LoadDataReturn(currentPage, pageSize);

            //统计
            decimal sumTotal;
            decimal sumPage;

            LoadTotalSum(out sumTotal);
            SumPage(pi, out sumPage);

            litCount.Text = string.Format("本页合计：{0}", sumPage);
            litSum.Text = string.Format("所有合计：{0}", sumTotal);

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
        }

        DataTable WashDate(int currentPage, int pageSize)
        {
            #region datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("账号");
            dt.Columns.Add("联系号码");
            dt.Columns.Add("分销商名称");
            dt.Columns.Add("级别");
            dt.Columns.Add("充值金额");
            dt.Columns.Add("申请日期");
            dt.Columns.Add("申请人");
            dt.Columns.Add("当前状态");
            dt.Columns.Add("审核人");
            dt.Columns.Add("审核时间");
            #endregion

            var pi = LoadDataReturn(currentPage, pageSize);
            foreach (var item in pi.List)
            {
                //申请人级别
                var UserLevelID = item.UserLevelID.Value;
                EFEntity.UserLevel userLevel = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelID == UserLevelID);
                var LevelName = userLevel == null ? "" : userLevel.UserLevelName;

                //申请人
                var rUserID = item.Requst_UserID.Value;
                EFEntity.User rUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == rUserID);
                var RequestUser = rUser == null ? "" : rUser.UserName;

                //审核人
                var chkUserID = item.CHK_UserID.Value;
                EFEntity.User chkUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == chkUserID);
                var chkUserName = chkUser == null ? "" : chkUser.UserName;

                DataRow row = dt.NewRow();
                row["账号"] = item.UserName;
                row["联系号码"] = item.Phone;
                row["分销商名称"] = item.CPName;
                row["级别"] = LevelName;
                row["充值金额"] = item.Requst_Amount;
                row["申请日期"] = item.Requst_Time;
                row["申请人"] = RequestUser;
                row["当前状态"] = item.Stauts;
                row["审核人"] = chkUserName;
                row["审核时间"] = item.CHK_Time;

                dt.Rows.Add(row);
            }

            //统计
            decimal sumTotal;
            decimal sumPage;

            LoadTotalSum(out sumTotal);
            SumPage(pi, out sumPage);

            DataRow rowPage = dt.NewRow();
            rowPage["充值金额"] = string.Format("本页合计：{0}", sumPage);

            DataRow rowSum = dt.NewRow();
            rowSum["充值金额"] = string.Format("所有合计：{0}", sumTotal);
            dt.Rows.Add(rowPage);
            dt.Rows.Add(rowSum);

            return dt;
        }

        #region 计算合计
        public void LoadTotalSum(out decimal sumValue)
        {
            sumValue = 0;

            string sql = " select sum(Requst_Amount) as num  from vAccountFlow";
            string where = WhereForSql();
            if (where != "")
                sql = sql + " where " + where;

            DataSet ods = BLL.DbHelperSQL.Query(sql);
            if (ods.Tables[0].Rows.Count == 1)
            {
                sumValue = 0;
                if (ods.Tables[0].Rows[0]["num"] != DBNull.Value)
                    sumValue=Convert.ToDecimal(ods.Tables[0].Rows[0]["num"].ToString());
            }
        }
        void SumPage(ETicket.Utility.PageInfo<EFEntity.vAccountFlow> pi, out decimal sum)
        {
            sum = 0;
            foreach (var item in pi.List)
            {
                sum = sum + item.Requst_Amount.Value;
            }
        }
        #endregion
       
    }
}