using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class partner_openedit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnGengerID.ServerClick += btnGengerID_ServerClick;
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

                    if (user.OpenStatus != null && user.OpenStatus.Value)
                    {
                        ddlOpenStatus.SelectedValue = "是";
                    }
                    else
                    {
                        ddlOpenStatus.SelectedValue = "否";
                    }

                    this.txtKey.Value = user.OpenKey;
                    this.txtParterID.Value = user.OpenParterID;
                }
            }
        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            var parterID = this.txtParterID.Value.Trim();
            var key=this.txtKey.Value.Trim();

            if(ddlOpenStatus.SelectedValue == "是")
            {
                if (parterID == "")
                {
                    PubFun.ShowMsg(this.Page,"请输入ParterID");
                    return;
                }
                if (key == "")
                {
                    PubFun.ShowMsg(this.Page, "请输入Key");
                    return;
                }
            }

            /*
            var obj= BLL.UserBLL.Instance.GetEntity(p => p.OpenKey == key);
            if(obj!=null)
            {
                PubFun.ShowMsg(this.Page, "重新生成Key，该Key已经被使用");
                return;
            }
            */

            int userid = PubFun.QueryInt("userid");
            EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
            if (user != null)
            {
                user.OpenKey = key;
                user.OpenParterID = parterID;
                if (ddlOpenStatus.SelectedValue == "是")
                    user.OpenStatus = true;
                else
                    user.OpenStatus = false;

                BLL.UserBLL.Instance.UpdateObject(user);
                PubFun.ShowMsgRedirect(this.Page, "保存完成",this.Request.RawUrl);
            }
        }

        void btnGengerID_ServerClick(object sender, EventArgs e)
        {
            int userid = PubFun.QueryInt("userid");
            EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
            if (user != null)
            {
                this.txtParterID.Value = "20" + PubFun.GetRandNumString(4) + "0" + user.UserID.ToString();
                this.txtKey.Value = Encrypt.GetMd5Hash(Guid.NewGuid().ToString());
            }
        }
    }
}