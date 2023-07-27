using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class member_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }
        void LoadData()
        {
            this.repList.DataSource = BLL.UserBLL.Instance.GetEntities(p=>p.UserCategory=="user");
            this.repList.DataBind();
        }
        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var user = e.Item.DataItem as EFEntity.User;

                //Edit
                HyperLink hyEdit = e.Item.FindControl("hyEdit") as HyperLink;
                string editUrl = string.Format("/business/user/partner_edit.aspx?userid={0}", user.UserID);
                hyEdit.NavigateUrl = "#";
                hyEdit.Attributes.Add("onclick", PubFun.TabNav("u_edit" + user.UserID, "编辑-" + user.UserName, editUrl));
            }
        }
    }
}