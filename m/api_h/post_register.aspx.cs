using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    public partial class post_register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = PubFun.QueryString("username");
            string txtPassword = PubFun.QueryString("txtPassword");
            string txtPassword2 = PubFun.QueryString("txtPassword2");
            string txtPhone = PubFun.QueryString("txtPhone");
            string appos = PubFun.QueryString("appos");

            if (appos != "")
                appos = ApiHelper.WashOS(appos);
            else
                appos = "未知";

            LoginModel loginModel = new LoginModel();
            string jsonStr = "";

            #region 验证
            if (username == "")
            {
                loginModel = new LoginModel();
                jsonStr = "";

                loginModel.Status = "0";
                loginModel.StatusMsg = "登录名不能为空";
                jsonStr = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if (txtPassword == "")
            {
                loginModel = new LoginModel();
                jsonStr = "";

                loginModel.Status = "0";
                loginModel.StatusMsg = "密码不能为空";
                jsonStr = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if (txtPassword != txtPassword2)
            {
                loginModel = new LoginModel();
                jsonStr = "";

                loginModel.Status = "0";
                loginModel.StatusMsg = "两次输入的密码不一致";
                jsonStr = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if (txtPhone == "")
            {
                loginModel = new LoginModel();
                jsonStr = "";

                loginModel.Status = "0";
                loginModel.StatusMsg = "请输入手机号码";
                jsonStr = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserName == username);
            if (user != null)
            {
                loginModel = new LoginModel();
                jsonStr = "";

                loginModel.Status = "0";
                loginModel.StatusMsg = "该用户名已经存在，无法注册";
                jsonStr = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(jsonStr);
                Response.End();
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
            newUser.RegTime = DateTime.Now;
            newUser.RegIP = PubFun.GetClientIP();
            newUser.Account = 0;
            newUser.EnableCashPay = false;
            newUser.EnableOnlinePay = true;
            newUser.EnableAccountPay = false;
            newUser.ClientType = ETicket.Utility.ClientTypeEnum.app.ToString();
            newUser.ClientName = appos;

            bool regSucc = false;
            try
            {
                BLL.UserBLL.Instance.AddObject(newUser);
                regSucc = true;
            }
            catch
            {
                regSucc = false;
                
            }

            if (regSucc)
            {
                loginModel = new LoginModel();
                jsonStr = "";

                //自动登录
                var  model = BLL.UserBLL.Instance.GetEntity(p => p.UserName == username && p.UserCategory != "valid");
                //加密后的Token
                string token = BLL.UserBLL.Instance.Model2EncryptJosn(model);
                loginModel.Status = "1";
                loginModel.StatusMsg = "登录成功";
                loginModel.UserID = model.UserID.ToString();
                loginModel.UserName = model.UserName;
                loginModel.UserCategory = model.UserCategory;
                loginModel.Token = token;
                jsonStr = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(jsonStr);
            }
            else
            {
                loginModel = new LoginModel();
                jsonStr = "";

                loginModel.Status = "0";
                loginModel.StatusMsg = "注册用户失败，请稍后重试";
                jsonStr = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
        }
    }
}