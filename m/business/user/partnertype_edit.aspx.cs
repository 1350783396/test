using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class partnertype_edit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int typeID = PubFun.QueryInt("typeid");
                EFEntity.UserLevel userType = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelID == typeID);
                if (userType != null)
                {
                    this.txtDicName.Text = userType.UserLevelName;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int typeID = PubFun.QueryInt("typeid");
            if (typeID > 0)
            {
                //更新
                EFEntity.UserLevel userType = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelID == typeID);
                userType.UserLevelName = this.txtDicName.Text.Trim();
                BLL.UserLevelBLL.Instance.UpdateObject(userType);
            }
            else
            {
                EFEntity.UserLevel userType = new EFEntity.UserLevel();
                userType.UserLevelName = this.txtDicName.Text.Trim();
                userType.UserCategory = "partner";
                BLL.UserLevelBLL.Instance.AddObject(userType);
            }
            Response.Redirect("partnertype_list.aspx");
        }
    }
}