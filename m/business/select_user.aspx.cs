using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ETicket.Web.business
{
    public partial class select_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rbtnList.SelectedIndexChanged += rbtnList_SelectedIndexChanged;
            btnClear.Click += btnClear_Click;
            btnQuery.Click += btnQuery_Click;
            AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            if(!Page.IsPostBack)
            {
                #region 绑定分销商级别
                /*
                var userLevelList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "partner");
                ddlUserLevel.Items.Clear();
                ddlUserLevel.Items.Add(new ListItem("所有级别", "所有级别"));
                foreach(var item in userLevelList)
                {
                    ddlUserLevel.Items.Add(new ListItem(item.UserLevelName,item.UserLevelID.ToString()));
                }
                */
                #endregion

                //var userList=BLL.UserBLL.Instance.GetEntities(p => p.UserCategory == "partner");
                //foreach(var user in userList)
                //{
                //    //+ string.Format("[联系人：{0},联系电话：{1}]", user.RealName, user.Phone)
                //    rbtnList.Items.Add(new ListItem(user.CPName , user.UserID.ToString()));
                //}

                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
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

        void btnClear_Click(object sender, EventArgs e)
        {
            foreach(ListItem item in rbtnList.Items)
            {
                item.Selected = false;
            }

            string str = @"window.parent.SelectReValue = '{0}'; window.parent.SelectReText = '{1}';";
            str = string.Format(str, "", "");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(UpdatePanel), "add", str, true);
        }

        void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = @"window.parent.SelectReValue = '{0}'; window.parent.SelectReText = '{1}';";
            str = string.Format(str, rbtnList.SelectedItem.Value, rbtnList.SelectedItem.Text);
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(UpdatePanel), "add", str, true);
        }

        string Where()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" it.UserCategory='partner'");
            if(txtCPName.Text.Trim()!="")
            {
                sb.AppendFormat(" and it.CPName like '%{0}%'", txtCPName.Text.Trim());
            }
            return sb.ToString();
        }
        void LoadData(int currentPage, int pageSize)
        {
            ETicket.Utility.PageInfo<EFEntity.User> pi = null;
            pi = BLL.UserBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.UserID ");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            //this.repList.DataSource = pi.List;
            //this.repList.DataBind();

            rbtnList.Items.Clear();
            foreach (var user in pi.List)
            {
                //+ string.Format("[联系人：{0},联系电话：{1}]", user.RealName, user.Phone)
                rbtnList.Items.Add(new ListItem(user.CPName, user.UserID.ToString()));
            }
        }
    }
}