using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.ucenter
{
    public partial class chg_password : UserBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            var cookie = BLL.UserBLL.Instance.GetLoginModel();
            if (cookie.UserCategory == "valid")
            {
                lblMsg.Text = "注意：修改密码后，在验票设备上登录时请使用新密码";
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtOldPass.Text.Trim() == "")
            {
                PubFun.ShowMsg(this, "请输入旧密码");
                return;
            }
            if (this.txtPass.Text.Trim() == "")
            {
                PubFun.ShowMsg(this, "请输入新密码");
                return;
            }
            if (this.txtPass.Text.Trim() != this.txtPass2.Text.Trim())
            {
                PubFun.ShowMsg(this, "两次输入的密码不一致");
                return;
            }
            string password = Encrypt.GetMd5Hash(this.txtPass.Text.Trim());
            string passwordOld = Encrypt.GetMd5Hash(this.txtOldPass.Text.Trim());
            var cookie = BLL.UserBLL.Instance.GetLoginModel();

            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookie.UserID);
            if (user.Password.ToLower() != passwordOld.ToLower())
            {
                PubFun.ShowMsg(this, "输入的旧密码错误，请重新输入");
                return;
            }
            user.Password = password;
            try
            {
                BLL.UserBLL.Instance.UpdateObject(user);
                PubFun.ShowMsgRedirect(this, "更改密码成功", Request.RawUrl);
            }
            catch (Exception ex)
            {
                PubFun.ShowMsg(this, "更改密码错误。" + ex.Message);
            }
        }
    }
}