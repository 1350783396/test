using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.RequestType=="POST")
            {
                string username = PubFun.QueryString("username");
                string txtPassword = PubFun.QueryString("txtPassword");
                string txtPassword2 = PubFun.QueryString("txtPassword2");
                string txtPhone = PubFun.QueryString("txtPhone");

                #region 验证
                if (username=="")
                {
                    PubFun.ShowMsg(this,"登录名不能为空");
                    return;
                }
                if(txtPassword=="")
                {
                    PubFun.ShowMsg(this, "密码不能为空");
                    return;
                }
                if(txtPassword!=txtPassword2)
                {
                    PubFun.ShowMsg(this, "两次输入的密码不一致");
                    return;
                }
                if(txtPhone=="")
                {
                    PubFun.ShowMsg(this, "请输入手机号码");
                    return;
                }
                var user = BLL.UserBLL.Instance.GetEntity(p => p.UserName == username);
                if (user != null)
                {
                    PubFun.ShowMsg(this, "该用户名已经存在，无法注册");
                    return;
                }
                #endregion

                var userLevel = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelName == "普通会员");
                EFEntity.User newUser = new EFEntity.User();
                newUser.UserName = username;
                newUser.UserLevelID = userLevel.UserLevelID;
                newUser.UserCategory = userLevel.UserCategory;
                newUser.Password = NJiaSu.Libraries.Encrypt.GetMd5Hash(txtPassword);
                newUser.Phone = txtPhone;
                newUser.RegTime=DateTime.Now;
                newUser.RegIP=PubFun.GetClientIP();
                newUser.Account=0;
                newUser.EnableCashPay=false;
                newUser.EnableOnlinePay=true;
                newUser.EnableAccountPay = false;
                newUser.ClientType = ETicket.Utility.ClientTypeEnum.pc.ToString();
                newUser.ClientName = PubFun.GetBrowserInfo();

                bool regSucc = false;
                try
                {
                    BLL.UserBLL.Instance.AddObject(newUser);
                    regSucc = true;
                }
                catch
                {
                    PubFun.ShowMsg(this,"注册用户失败");
                }

                if(regSucc)
                {
                    //自动登录
                    BLL.UserBLL.Instance.Login(username, txtPassword, false);
                    Response.Redirect("register_2.aspx");
                }
            }
        }
    }
}