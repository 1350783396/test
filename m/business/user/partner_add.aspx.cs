using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class partner_add : AdminBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                IEnumerable<EFEntity.UserLevel> userTypeList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "partner").OrderBy(p => p.OrderValue);
                ddlUserLevel.Items.Clear();
                ddlUserLevel.Items.Add(new ListItem("请选择", "0"));
                foreach (var userLevel in userTypeList)
                {
                    ddlUserLevel.Items.Add(new ListItem(userLevel.UserLevelName, userLevel.UserLevelID.ToString()));
                }
            }

        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            string UserName = txtUserName.Value.Trim();
            string Password = txtPassword.Value.Trim();
            string Password2 = this.txtPassword2.Value.Trim();
            int UserTypeID = int.Parse(ddlUserLevel.SelectedValue);
            string LinkMan = txtLinkMan.Value.Trim();
            string LinkPhone = txtLinkPhone.Value.Trim();
            string Email = txtEmail.Value.Trim();
            string CPName = txtCPName.Value.Trim();
            string CPAddress = txtCPAddress.Value.Trim();
            string Tel = txtTel.Value.Trim();

            bool QR_IsLock = chkQRIsLock.Checked;
            string QR_LockIP = txtQRLockIP.Value;


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
            if (UserTypeID <= 0)
            {
                PubFun.ShowMsg(this, "请选择分销商级别");
                return;
            }
            if (string.IsNullOrEmpty(LinkMan))
            {
                PubFun.ShowMsg(this, "请输入联系人");
                return;
            }
            if (string.IsNullOrEmpty(LinkPhone))
            {
                PubFun.ShowMsg(this, "请输入联系电话");
                return;
            }
            if (string.IsNullOrEmpty(CPName))
            {
                PubFun.ShowMsg(this, "请输入分销商名称");
                return;
            }
            if (chkOnline.Checked == false && chkAccount.Checked == false && chkCash.Checked == false)
            {
                PubFun.ShowMsg(this, "支付方式至少选择一项");
                return;
            }
            var userExist = BLL.UserBLL.Instance.GetEntity(p => p.UserName == UserName);
            if (userExist != null)
            {
                PubFun.ShowMsg(this, "该用户名已经存在，无法注册");
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
            user.EnableAccountPay = chkAccount.Checked;
            user.EnableCashPay = chkCash.Checked;
            user.EnableOnlinePay = chkOnline.Checked;

            user.QR_IsLock = QR_IsLock;
            user.QR_LockIP = QR_LockIP;

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