using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;
using ETicket.Utility;

namespace ETicket.Web.api_h
{
    public partial class post_setpass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = PubFun.QueryString("username");
            string code = PubFun.QueryString("code");
            string newpwd = PubFun.QueryString("newpwd");
            string newpwd2 = PubFun.QueryString("newpwd2");

            if (string.IsNullOrEmpty(username))
            {
                Response.Write("请输入登录账号");
                Response.End();
                return;
            }
            if (string.IsNullOrEmpty(code))
            {
                Response.Write("请输入手机验证码");
                Response.End();
                return;
            }
            if (string.IsNullOrEmpty(newpwd))
            {
                Response.Write("请输入新密码");
                Response.End();
                return;
            }
            if (newpwd.Length < 6)
            {
                Response.Write("密码长度不得少于6个字符！");
                Response.End();
                return;
            }
            if (newpwd != newpwd2)
            {
                Response.Write("两次输入的密码不一致，请重新输入");
                Response.End();
                return;
            }
            EFEntity.User userModel = BLL.UserBLL.Instance.GetEntity(p => p.UserName == username);
            if (userModel == null)
            {
                Response.Write("重置失败，账号有误。");
                Response.End();
                return;
            }

            //验证手机码是否正确
            OperationResult result = BLL.PhoneSmsValidateBLL.Instance.Validate(userModel.Phone, code);
            if (result.OperateResult == OperateResultEnum.Fail)
            {
                Response.Write(result.Message);
                Response.End();
                return;
            }
            userModel.Password = Encrypt.GetMd5Hash(newpwd);
            BLL.UserBLL.Instance.UpdateObject(userModel);

            //重置密码成功
            Response.Write("ok");
            Response.End();
            return;
            
        }
    }
}