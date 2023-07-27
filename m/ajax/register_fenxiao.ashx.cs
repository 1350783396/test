using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NJiaSu.Libraries;
using System.Collections;
using System.Reflection;


namespace ETicket.Web.ajax
{
    /// <summary>
    /// register_fenxiao 的摘要说明
    /// </summary>
    public class register_fenxiao : IHttpHandler
    {
        protected HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            context.Response.ContentType = "text/plain";


            //获取执行方法
            string name = PubFun.QueryString("_action");
            MethodInfo method = base.GetType().GetMethod(name);
            if (method != null)
            {
                method.Invoke(this, null);
            }
        }

        public void ChkParter()
        {
            var UserName = PubFun.QueryString("username");
            UserName = PubFun.SqlFilter(UserName);

            var userExist = BLL.UserBLL.Instance.GetEntity(p => p.UserName == UserName);
            if (userExist != null)
            {
                ResponseResult(false, "该用户名已经存在，无法注册");
            }
            else
            {
                ResponseResult(true, "可以注册");
            }
        }
        public void RegParter()
        {
            //System.Threading.Thread.Sleep(10000);
            string UserName = PubFun.QueryString("username");
            string Password = PubFun.QueryString("password");
            string userlevelStr = PubFun.QueryString("userlevel");

            int UserTypeID = 0;
            //获取分销商级别
            try
            {
                var m= BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelName == userlevelStr);
                if (m != null)
                    UserTypeID = m.UserLevelID;
            }
            catch
            {

            }
            string LinkMan = UserName;
            string LinkPhone = PubFun.QueryString("phone") ;
            string Email = "";// txtEmail.Value.Trim();
            string CPName = UserName;// txtCPName.Value.Trim();
            string CPAddress = "";// txtCPAddress.Value.Trim();
            string Tel = LinkPhone;// txtTel.Value.Trim();

            #region 验证
            if (string.IsNullOrEmpty(UserName))
            {
                ResponseResult(false, "请输入用户名");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                ResponseResult(false, "请输入密码");
                return;
            }
            if (UserTypeID <= 0)
            {
                ResponseResult(false, "分销商级别已被限定，无法注册，请联系技术人员");
                return;
            }
            if (string.IsNullOrEmpty(LinkPhone))
            {
                ResponseResult(false, "请输入联系电话");
                return;
            }
            var userExist = BLL.UserBLL.Instance.GetEntity(p => p.UserName == UserName);
            if (userExist != null)
            {
                ResponseResult(false, "该用户名已经存在，无法注册");
                return;
            }
            #endregion

            EFEntity.User user = new EFEntity.User();
            user.UserName = UserName;
            user.Password = Encrypt.GetMd5Hash(Password);

            user.UserCategory = "partner";
            user.UserLevelID = UserTypeID;

            user.RealName = LinkMan;
            user.Phone = LinkPhone;
            user.Email = Email;
            user.CPName = CPName;
            user.CPAddress = CPAddress;
            user.Tel = Tel;

            user.Account = 0;
            user.EnableAccountPay = true;
            user.EnableCashPay = false;
            user.EnableOnlinePay =true;

            user.RegTime = DateTime.Now;
            user.RegIP = PubFun.GetClientIP();
            user.ClientType = ETicket.Utility.ClientTypeEnum.qr.ToString();
            user.ClientName = PubFun.GetBrowserInfo();

            var isSucc = false;
            try
            {
                BLL.UserBLL.Instance.AddObject(user);
                isSucc = true;
            }
            catch (Exception ex)
            {
                NJiaSu.Libraries.LogHelper.LogResult("qr_reg_parter", ex.ToString());
            }
            if(isSucc)
            {
                ResponseResult(true, "注册成功");
            }
            else
            {
                ResponseResult(false, "注册失败");
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ResponseResult(bool res, string msg)
        {
            //Hashtable re = new Hashtable();
            //re.Add("isOk", res);
            //re.Add("msg", msg);
            //this.Response(re);

            var s = "{" + string.Format("\"isOk\":\"{0}\",\"msg\":\"{1}\"", res, msg) + "}";
            context.Response.Write(s);
            context.Response.End();
        }
        protected void Response(object o)
        {
            HttpContext context = HttpContext.Current;
            String json = JsonHelper.GetJson<object>(o);
            context.Response.Write(json);
            context.Response.End();
        }

       
    }
}