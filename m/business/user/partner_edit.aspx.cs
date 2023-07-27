using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class partner_edit : AdminBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtUserName.Disabled = true;
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

                int userid = PubFun.QueryInt("userid");
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
                if (user!=null)
                {
                    this.txtUserName.Value=user.UserName;
                    this.ddlUserLevel.SelectedValue=  user.UserLevelID.ToString();
                    this.txtLinkMan.Value= user.RealName;
                    this.txtLinkPhone.Value=user.Phone;
                    this.txtEmail.Value= user.Email ;
                    this.txtCPName.Value=user.CPName;
                    if (user.CPAddress!=null)
                        this.txtCPAddress.Value = user.CPAddress;
                    if (user.Tel!=null)
                        txtTel.Value = user.Tel;

                    if (user.EnableAccountPay!=null)
                        chkAccount.Checked=user.EnableAccountPay.Value;
                    if (user.EnableCashPay != null)
                        chkCash.Checked= user.EnableCashPay.Value;
                    if (user.EnableOnlinePay!=null)
                        chkOnline.Checked=  user.EnableOnlinePay.Value;

                    if (user.QR_IsLock != null)
                        chkQRIsLock.Checked = user.QR_IsLock.Value;
                    txtQRLockIP.Value = user.QR_LockIP;
                }
            }

        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            string UserName = txtUserName.Value.Trim();
            string Password = txtPassword.Value.Trim();
          
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
            #endregion

            int userid = PubFun.QueryInt("userid");
            EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
            if (user==null)
            {
                PubFun.ShowMsg(this, "更新分销商失败，数据不存在。");
                return;
            }
            //user.UserName = UserName;
            if(!string.IsNullOrEmpty(Password))
                user.Password = Encrypt.GetMd5Hash(Password);

            user.UserLevelID = UserTypeID;
            user.RealName = LinkMan;
            user.Phone = LinkPhone;
            user.Email = Email;
            user.CPName = CPName;
            user.CPAddress = CPAddress;
            user.Tel = Tel;

            user.QR_IsLock = QR_IsLock;
            user.QR_LockIP = QR_LockIP;

            user.EnableAccountPay = chkAccount.Checked;
            user.EnableCashPay = chkCash.Checked ;
            user.EnableOnlinePay = chkOnline.Checked;

            try
            {
                BLL.UserBLL.Instance.UpdateObject(user);
                PubFun.ShowMsgRedirect(this, "更新数据成功！", Request.RawUrl);
            }
            catch (Exception ex)
            {
                PubFun.ShowMsg(this, "更新数据出错：" + ex.Message);
            }
        }
    }
}