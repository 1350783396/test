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
    public partial class partner_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefresh.Click += btnRefresh_Click;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnDel.Click += btnDel_Click;
            this.btnDel2.Click += btnDel_Click;

            if (!Page.IsPostBack)
            {
                IEnumerable<EFEntity.UserLevel> userTypeList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "partner").OrderBy(p => p.OrderValue);
                ddlUserLevel.Items.Clear();
                ddlUserLevel.Items.Add(new ListItem("所有级别", "0"));
                foreach (var userLevel in userTypeList)
                {
                    ddlUserLevel.Items.Add(new ListItem(userLevel.UserLevelName, userLevel.UserLevelID.ToString()));
                }

                LoadData(AspNetPager1.CurrentPageIndex,AspNetPager1.PageSize);
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }


        void btnDel_Click(object sender, EventArgs e)
        {
            bool delete = false;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                CheckBox chkItem = this.repList.Items[i].FindControl("chkItem") as CheckBox;
                if (chkItem != null && chkItem.Checked == true)
                {
                    Label lblPKID = this.repList.Items[i].FindControl("lblPKID") as Label;
                    if (lblPKID != null)
                    {
                        int id = int.Parse(lblPKID.Text);
                        EFEntity.User user = new EFEntity.User();
                        user.UserID = id;
                        BLL.UserBLL.Instance.DeleteObject(user);
                        delete = true;
                    }
                }
            }

            if (delete)
            {
                LoadData(1, AspNetPager1.PageSize);
            }
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
            //开发平台
            if(this.ddlOpenStatus.SelectedValue=="是")
            {
                sb.AppendFormat(" and it.OpenStatus=true");
            }
            else if (this.ddlOpenStatus.SelectedValue == "否")
            {
                sb.AppendFormat(" and it.OpenStatus!=true");
            }
            //阿里接口
            if (this.ddlAliStatus.SelectedValue == "是")
            {
                sb.AppendFormat(" and it.AliStatus=true");
            }
            else if (this.ddlAliStatus.SelectedValue == "否")
            {
                sb.AppendFormat(" and it.AliStatus!=true");
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
        }
        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var user = e.Item.DataItem as EFEntity.User;

                Literal litLevelName = e.Item.FindControl("litLevelName") as Literal;
                EFEntity.UserLevel userLevel= BLL.UserLevelBLL.Instance.GetEntity(p=>p.UserLevelID==user.UserLevelID);
                litLevelName.Text = userLevel.UserLevelName;

                //开发平台状态
                Literal litOpenStatus = e.Item.FindControl("litOpenStatus") as Literal;
                if (user.OpenStatus!=null&&user.OpenStatus.Value == true)
                    litOpenStatus.Text = "<b style='color:green'>已开通</b>";
                else
                    litOpenStatus.Text = "未开通";

                //阿里接口状态
                Literal litAliStatus = e.Item.FindControl("litAliStatus") as Literal;
                if (user.AliStatus != null && user.AliStatus.Value == true)
                    litAliStatus.Text = "<b style='color:green'>已开通</b>";
                else
                    litAliStatus.Text = "未开通";

                //编辑
                HyperLink hyEdit = e.Item.FindControl("hyEdit") as HyperLink;
                string editUrl = string.Format("/business/user/partner_edit.aspx?userid={0}", user.UserID);
                hyEdit.NavigateUrl = "#";
                hyEdit.Attributes.Add("onclick", PubFun.TabNav("u_edit" + user.UserID, "编辑-" + user.UserName, editUrl));

                //开发平台
                HyperLink hyOpenEdit = e.Item.FindControl("hyOpenEdit") as HyperLink;
                string openEditUrl = string.Format("/business/user/partner_openedit.aspx?userid={0}", user.UserID);
                hyOpenEdit.NavigateUrl = "#";
                hyOpenEdit.Attributes.Add("onclick", PubFun.TabNav("u_openedit" + user.UserID, "开放平台-" + user.UserName, openEditUrl));

                //阿里接口
                HyperLink hyAliEdit = e.Item.FindControl("hyAliEdit") as HyperLink;
                string aliEditUrl = string.Format("/business/user/partner_aliedit.aspx?userid={0}", user.UserID);
                hyAliEdit.NavigateUrl = "#";
                hyAliEdit.Attributes.Add("onclick", PubFun.TabNav("u_aliedit" + user.UserID, "阿里接口-" + user.UserName, aliEditUrl));

                CheckBox chkItem = e.Item.FindControl("chkItem") as CheckBox;
                chkItem.Enabled = false;
                if(BLL.UserBLL.Instance.IsDelete(user.UserID))
                {
                    chkItem.Enabled = true;
                }
            }
        }
    }
}