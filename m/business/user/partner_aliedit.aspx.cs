using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;


namespace ETicket.Web.business.user
{
    public partial class partner_aliedit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                int userid = PubFun.QueryInt("userid");
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
                if (user != null)
                {
                    this.litUserName.Text = user.UserName;
                    this.litLinkPhone.Text = user.Phone;
                    this.litLinkMan.Text = user.RealName;
                    this.litCPName.Text = user.CPName;

                    if (user.AliStatus != null && user.AliStatus.Value)
                    {
                        ddlAliStatus.SelectedValue = "是";
                    }
                    else
                    {
                        ddlAliStatus.SelectedValue = "否";
                    }

                    this.txtSellerID.Value = user.AliSellerID;
                }
            }
        }


        void btnSave_ServerClick(object sender, EventArgs e)
        {
            var sellerID = this.txtSellerID.Value.Trim();

            if (ddlAliStatus.SelectedValue == "是")
            {
                if (sellerID == "")
                {
                    PubFun.ShowMsg(this.Page, "请输入淘宝卖家ID");
                    return;
                }
            }

            //验证该seller如否在其他用户上绑定
           
            int userid = PubFun.QueryInt("userid");
            var otherUserIsExit= BLL.UserBLL.Instance.GetCount(p => p.UserID != userid && p.AliSellerID == sellerID);
            if(otherUserIsExit>0)
            {
                PubFun.ShowMsg(this.Page, "该淘宝卖家ID已在其他分销商上绑定，无法再绑定");
                return;
            }

            EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
            if (user != null)
            {
                user.AliSellerID = sellerID;
                if (ddlAliStatus.SelectedValue == "是")
                    user.AliStatus = true;
                else
                    user.AliStatus = false;

                BLL.UserBLL.Instance.UpdateObject(user);
                PubFun.ShowMsgRedirect(this.Page, "保存完成", this.Request.RawUrl);
            }
        }

    }
}