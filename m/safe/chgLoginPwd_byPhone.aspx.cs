using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;
using ETicket.Utility;

namespace ETicket.Web.safe
{
    public partial class chgLoginPwd_byPhone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "POST")
            {
                string username = PubFun.QueryString("username");
                string code = PubFun.QueryString("code");
                string newpwd = PubFun.QueryString("newpwd");
                string newpwd2 = PubFun.QueryString("newpwd2");

                if (string.IsNullOrEmpty(username))
                {
                    PubFun.ShowMsg(this.Page, "请输入登录账号");
                    return;
                }
                if (string.IsNullOrEmpty(code))
                {
                    PubFun.ShowMsg(this.Page, "请输入手机验证码");
                    return;
                }
                if (string.IsNullOrEmpty(newpwd))
                {
                    PubFun.ShowMsg(this.Page, "请输入新密码");
                    return;
                }
                if (newpwd.Length < 6)
                {
                    PubFun.ShowMsg(this.Page, "密码长度不得少于6个字符！");
                    return;
                }
                if (newpwd != newpwd2)
                {
                    PubFun.ShowMsg(this.Page, "两次输入的密码不一致，请重新输入");
                    return;
                }
                EFEntity.User userModel = BLL.UserBLL.Instance.GetEntity(p => p.UserName == username);
                if (userModel == null)
                {
                    PubFun.ShowMsg(this, "对不起，该登录账号不存在。");
                    return;
                }

                //验证手机码是否正确
                OperationResult result = BLL.PhoneSmsValidateBLL.Instance.Validate(userModel.Phone, code);
                if (result.OperateResult == OperateResultEnum.Fail)
                {
                    PubFun.ShowMsg(this.Page, result.Message);
                    return;
                }
                userModel.Password = Encrypt.GetMd5Hash(newpwd);
                BLL.UserBLL.Instance.UpdateObject(userModel);
                PubFun.ShowMsgRedirect(this, "重置密码成功。", PubFun.ApplicationPath+"/login.aspx");
            }
        }
    }
}