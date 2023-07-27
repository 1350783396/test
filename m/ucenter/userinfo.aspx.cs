using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.ucenter
{
    public partial class userinfo : UserBase
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            if(!Page.IsPostBack)
            {
                 var cookieUser = BLL.UserBLL.Instance.GetLoginModel();
                 var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookieUser.UserID);
                 if(user!=null)
                 {
                     this.txtRealName.Text = user.RealName;
                     this.txtEmail.Text = user.Email;
                     this.txtIDCard.Text = user.IDCard;
                     this.txtPhone.Text = user.Phone;
                 }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string Phone=txtPhone.Text.Trim();
            string RealName=txtRealName.Text.Trim();
            string Email=txtEmail.Text.Trim();
            string IDCard=txtIDCard.Text.Trim();

            if (Phone == "")
            {
                PubFun.ShowMsg(this, "请输入手机号码");
                return;
            }
            if (!PubFun.IsMobile(Phone))
            {
                PubFun.ShowMsg(this, "请输入正确的手机号码格式");
                return;
            }

            var cookieUser = BLL.UserBLL.Instance.GetLoginModel();
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookieUser.UserID);
            user.Phone = Phone;
            if (RealName != "")
                user.RealName = RealName;
            else
                user.RealName = null;

            if (Email != "")
                user.Email = Email;
            else
                user.Email = null;

            if (IDCard != "")
                user.IDCard = IDCard;
            else
                user.IDCard = null;

            try
            {
                BLL.UserBLL.Instance.UpdateObject(user);
                PubFun.ShowMsgRedirect(this, "保存成功。" ,Request.RawUrl);
            }
            catch(Exception ex)
            {
                PubFun.ShowMsg(this, "保存失败。"+ex.Message);
            }

        }
    }
}