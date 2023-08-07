using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class admin_add : AdminBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                IEnumerable<EFEntity.Role> roleList = BLL.RoleBLL.Instance.GetEntities();
                ddlRole.Items.Clear();
                ddlRole.Items.Add(new ListItem("请选择", "0"));
                foreach (var role in roleList)
                {
                    ddlRole.Items.Add(new ListItem(role.RoleName, role.RoleID.ToString()));
                }
            }

        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            string UserName = txtUserName.Value.Trim();
            string Password = txtPassword.Value.Trim();
            string Password2 = this.txtPassword2.Value.Trim();
            int RoleID = int.Parse(ddlRole.SelectedValue);
            string RealName = txtRealName.Value.Trim();
            string Phone = txtPhone.Value.Trim();
            string Email = txtEmail.Value.Trim();

            #region 验证
            if (string.IsNullOrEmpty(UserName))
            {
                PubFun.ShowMsg(this, "请输入用户名");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                PubFun.ShowMsg(this, "请输入密码");
                return;
            }
            if (Password2 != Password)
            {
                PubFun.ShowMsg(this, "两次输入的密码不一致");
                return;
            }
            if (RoleID <= 0)
            {
                PubFun.ShowMsg(this, "请选择所属角色");
                return;
            }
            if (string.IsNullOrEmpty(RealName))
            {
                PubFun.ShowMsg(this, "请输入真实姓名");
                return;
            }
            if (string.IsNullOrEmpty(Phone))
            {
                PubFun.ShowMsg(this, "请输入手机号码");
                return;
            }

            #endregion

            EFEntity.User user = new EFEntity.User();
            user.UserName = UserName;
            user.Password = Encrypt.GetMd5Hash(Password);
            if (RoleID == 3)
                user.UserCategory = "operator";
            else
                user.UserCategory = "admin";
            user.RoleID = RoleID;
            user.RealName = RealName;
            user.Phone = Phone;
            if (Email != "")
                user.Email = Email;

            user.Account = 0;
            user.EnableAccountPay = false;
            user.EnableCashPay = false;
            user.EnableOnlinePay = false;
            user.RegTime = DateTime.Now;
            user.RegIP = PubFun.GetClientIP();
            user.ClientType = ETicket.Utility.ClientTypeEnum.pc.ToString();
            user.ClientName = PubFun.GetBrowserInfo();

            try
            {
                BLL.UserBLL.Instance.AddObject(user);
                PubFun.ShowMsgRedirect(this, "保存成功！", Request.RawUrl);
            }
            catch (Exception ex)
            {
                PubFun.ShowMsg(this, "保存数据出错：" + ex.Message);
            }
        }
    }
}