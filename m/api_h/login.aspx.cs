using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            EFEntity.User model = null;
            LoginModel loginModel = new LoginModel();

            string username = PubFun.QueryString("username");
            string password = PubFun.QueryString("password");

            string ss = Request.Form["username"];

            string md5 = PubFun.QueryString("md5");
            if (username == "" || password == "")
            {
                loginModel.Status = "0";
                loginModel.StatusMsg = "用户名或密码不能为空";
                string josn = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(josn);
                Response.End();
                return;
            }
            //比对MD5
            string sysMd5 = ApiHelper.sysMd5; //Encrypt.GetMd5Hash(username + password + ApiHelper.md5Key);
            if (sysMd5 != md5)
            {
                loginModel.Status = "0";
                loginModel.StatusMsg = "登录系统错误，授权码不对";
                string josn = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(josn);
                Response.End();
                return;
            }

            model = BLL.UserBLL.Instance.GetEntity(p => p.UserName == username && p.UserCategory != "valid");
            if (model == null)
            {
                loginModel.Status = "0";
                loginModel.StatusMsg = "用户名或密码错误";
                string josn = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(josn);
                Response.End();
                return;
            }
            string passMd5 = Encrypt.GetMd5Hash(password);
            if (passMd5 != model.Password)
            {
                loginModel.Status = "0";
                loginModel.StatusMsg = "用户名或密码错误";
                string josn = JsonHelper.GetJson<LoginModel>(loginModel);
                Response.Write(josn);
                Response.End();
                return;
            }

            //加密后的Token
            string token = BLL.UserBLL.Instance.Model2EncryptJosn(model);
            loginModel.Status = "1";
            loginModel.StatusMsg = "登录成功";
            loginModel.UserID = model.UserID.ToString();
            loginModel.UserName = model.UserName;
            loginModel.UserCategory = model.UserCategory;
            loginModel.Token = token;
            string josnSucc = JsonHelper.GetJson<LoginModel>(loginModel);
            Response.Write(josnSucc);
            Response.End();
            return;
        }
    }
}