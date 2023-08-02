using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnLogin.ServerClick += btnLogin_ServerClick;
        }

        void btnLogin_ServerClick(object sender, EventArgs e)
        {
            string userName = PubFun.QueryString("txtUserName");//用户名
            string password = PubFun.QueryString("txtPassword");//密码
            string validCode = PubFun.QueryString("txtValidCode");//验证码
            string url = PubFun.QueryString("reurl");

            #region 验证
            if (string.IsNullOrEmpty(userName))
            {
                PubFun.ShowMsg(this, "请输入用户名");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                PubFun.ShowMsg(this, "请输入密码");
                return;
            }
            if (string.IsNullOrEmpty(validCode))
            {
                PubFun.ShowMsg(this, "请输入验证码");
                return;
            }

            //验证码
            string sysCode = "";
            if (Session["UserCCode"] != null)
            {
                sysCode = Session["UserCCode"].ToString();
            }
            else
            {
                PubFun.ShowMsg(this, "验证码错误，请重新输入");
                return;
            }
            if (validCode.ToLower() != sysCode.ToLower())
            {
                PubFun.ShowMsg(this, "验证码错误，请重新输入");
                return;
            }
            #endregion

            string msg = BLL.UserBLL.Instance.Login(userName, password, false);
            if (string.IsNullOrEmpty(msg))
            {
                EFEntity.CookiesUser cUser = BLL.UserBLL.Instance.GetLoginModel();
                var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cUser.UserID);
                int r = PubFun.QueryInt("r");
                if(r==1&url!="")
                {
                    Response.Redirect(url);
                }
                else if (user.UserCategory == "partner")
                {
                    Response.Redirect(PubFun.ApplicationPath + "/business/partner.aspx");
                }
                else if (user.UserCategory == "operator")//计调
                {
                    Response.Redirect(PubFun.ApplicationPath + "/business/jiDiaoMain.aspx");
                }
                else if (user.UserCategory == "admin")
                {
                    Response.Redirect(PubFun.ApplicationPath + "/business/main.aspx");
                }
                else if (user.UserCategory == "valid")
                {
                    Response.Redirect(PubFun.ApplicationPath + "/business/valid.aspx");
                }
                else if (user.UserCategory == "orderview")
                {
                    Response.Redirect(PubFun.ApplicationPath + "/business/orderview.aspx");
                }
                else
                {
                    if (string.IsNullOrEmpty(url))
                    {
                        Response.Redirect(PubFun.ApplicationPath + "/ucenter/order.aspx");
                    }
                    else
                    {
                        Response.Redirect(url);
                    }
                }
            }
            else
            {
                PubFun.ShowMsg(this, "用户名或密码错误");
                return;
            }
        }
    }
}