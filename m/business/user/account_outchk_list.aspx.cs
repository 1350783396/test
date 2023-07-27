using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using NJiaSu.Libraries;


namespace ETicket.Web.business.user
{
    public partial class account_outchk_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefresh.Click += btnRefresh_Click;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
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
                var flow = e.Item.DataItem as EFEntity.vAccountOutFlow;

                Literal litLevelName = e.Item.FindControl("litLevelName") as Literal;
                EFEntity.UserLevel userLevel = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelID == flow.UserLevelID);
                litLevelName.Text = userLevel.UserLevelName;
              
                Literal litRequestUser = e.Item.FindControl("litRequestUser") as Literal;
                EFEntity.User rUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == flow.Requst_UserID.Value);
                if (rUser != null)
                    litRequestUser.Text = rUser.UserName;


                Literal litChkUser = e.Item.FindControl("litChkUser") as Literal;
                if (flow.CHK_UserID != null)
                {
                    var chkUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == flow.CHK_UserID);
                    litChkUser.Text = chkUser.UserName;
                }

                //Edit
                HyperLink hyEdit = e.Item.FindControl("hyEdit") as HyperLink;
                hyEdit.Visible = false;
                if (flow.Stauts == ETicket.Utility.AccountFlowStatusEnum.申请中.ToString())
                {
                    hyEdit.Visible = true;
                    string editUrl = string.Format("/business/user/account_outchk_exec.aspx?pkid={0}", flow.PKID);
                    hyEdit.NavigateUrl = "#";
                    hyEdit.Attributes.Add("onclick", PubFun.TabNav("alog" + flow.PKID, "积分销除审核操作", editUrl));
                }

                HyperLink hyDetail = e.Item.FindControl("hyDetail") as HyperLink;
                string detailUrl = string.Format("/business/user/account_outchk_detail.aspx?pkid={0}", flow.PKID);
                hyDetail.NavigateUrl = "#";
                hyDetail.Attributes.Add("onclick", PubFun.TabNav("alog" + flow.PKID, "积分销除审核详细", detailUrl));
            }
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
            ETicket.Utility.PageInfo<EFEntity.vAccountOutFlow> pi = null;
            pi = BLL.vAccountOutFlowBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.PKID DESC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
        }
       
    }
}