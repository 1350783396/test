using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using NJiaSu.Libraries;

namespace ETicket.Web.business.partner
{
    public partial class account_query_list : PartnerBase
    {

        EFEntity.CookiesUser cookieUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            cookieUser = BLL.UserBLL.Instance.GetLoginModel();
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnLoadAccount.Click += btnLoadAccount_Click;
            if (!Page.IsPostBack)
            {
                LoadAccount();
                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }

        void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtStartTime.Text.Trim()!="")
            {
                try
                {
                    Convert.ToDateTime(txtStartTime.Text.Trim());
                    Convert.ToDateTime(txtEndTime.Text.Trim());
                }
                catch
                {
                    PubFun.ShowMsg(this, "请输入正确的日期格式:yyyy-MM-dd");
                    return;
                }
            }
            AspNetPager1.CurrentPageIndex = 1;
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void btnLoadAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var log = e.Item.DataItem as EFEntity.AccountLog;
                Literal litUserName = e.Item.FindControl("litUserName") as Literal;
                litUserName.Text = cookieUser.UserName;

                Literal litACT = e.Item.FindControl("litACT") as Literal;
                if (log.ActType == "in")
                {
                    litACT.Text = "<span style='background-color:green;color:#fff;width:100%;padding:2px 5px;'>收入</span>";
                }
                else if (log.ActType == "out")
                {
                    litACT.Text = "<span style='background-color:red;color:#fff;width:100%;padding:2px 5px;'>支出</span>";
                }

                Literal litACTRMB = e.Item.FindControl("litACTRMB") as Literal;
                litACTRMB.Text = log.ActAmount.ToString();

                Literal litTime = e.Item.FindControl("litTime") as Literal;
                litTime.Text = log.ActTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

                Literal litMemo = e.Item.FindControl("litMemo") as Literal;
                litMemo.Text = log.Memo;

            }
        }
        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            sb.Append(" it.UserID=" + cookiesUser.UserID);
            //类型
            if (ddlACT.SelectedValue != "")
            {
                sb.AppendFormat(" and it.ActType='{0}'", ddlACT.SelectedValue);
            }
            if (txtStartTime.Text.Trim() != "")
            {
                sb.Append(" and it.ActTime>=DATETIME'" + txtStartTime.Text.Trim() + " 00:00:00'");
            }
            if (txtEndTime.Text.Trim() != "")
            {
                sb.Append(" and it.ActTime<=DATETIME'" + txtEndTime.Text.Trim() + " 23:59:59'");
            }

            return sb.ToString();
        }
        void LoadData(int currentPage, int pageSize)
        {
            ETicket.Utility.PageInfo<EFEntity.AccountLog> pi = null;
            pi = BLL.AccountLogBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.PKID DESC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
        }
        void LoadAccount()
        {
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookieUser.UserID);
            litUserAccount.Text = string.Format("你当前积分余额为：{0}", user.Account);
        }
    }
}