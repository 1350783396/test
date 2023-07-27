using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.partner
{
    public partial class partner_edit :PartnerBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
         
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                EFEntity.CookiesUser cookie = BLL.UserBLL.Instance.GetLoginModel();
                int userid = cookie.UserID;
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
                if (user!=null)
                {
                    this.litUserName.Text = user.UserName;

                    var userLevel = BLL.UserLevelBLL.Instance.GetEntity(p=>p.UserLevelID==user.UserLevelID);
                    if (userLevel != null)
                        this.litLevel.Text = userLevel.UserLevelName;

                    this.txtLinkMan.Value= user.RealName;
                    this.txtLinkPhone.Value=user.Phone;
                    this.txtEmail.Value= user.Email ;
                    this.txtCPName.Value=user.CPName;

                    if(user.CPAddress!=null)
                        this.txtCPAddress.Value = user.CPAddress;
                    if (user.Tel!=null)
                        txtTel.Value = user.Tel;

                    if (user.EnableAccountPay!=null)
                        chkAccount.Checked=user.EnableAccountPay.Value;
                    if (user.EnableCashPay != null)
                        chkCash.Checked= user.EnableCashPay.Value;
                    if (user.EnableOnlinePay!=null)
                        chkOnline.Checked=  user.EnableOnlinePay.Value;
                }
            }

        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
           
            string LinkMan = txtLinkMan.Value.Trim();
            string LinkPhone = txtLinkPhone.Value.Trim();
            string Email = txtEmail.Value.Trim();
            string CPName = txtCPName.Value.Trim();
            string CPAddress = txtCPAddress.Value.Trim();
            string Tel = txtTel.Value.Trim();

            #region 验证
          
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
           
            #endregion

            EFEntity.CookiesUser cookie = BLL.UserBLL.Instance.GetLoginModel();
            int userid = cookie.UserID;

            EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
            if (user==null)
            {
                PubFun.ShowMsg(this, "更新资料失败，数据不存在。");
                return;
            }
          
            user.RealName = LinkMan;
            user.Phone = LinkPhone;
            user.Email = Email;
            user.CPName = CPName;
            user.CPAddress = CPAddress;
            user.Tel = Tel;

            //user.EnableAccountPay = chkAccount.Checked;
            //user.EnableCashPay = chkCash.Checked ;
            //user.EnableOnlinePay = chkOnline.Checked;

            try
            {
                BLL.UserBLL.Instance.UpdateObject(user);
                PubFun.ShowMsgRedirect(this, "更新资料成功！", Request.RawUrl);
            }
            catch (Exception ex)
            {
                PubFun.ShowMsg(this, "更新资料出错：" + ex.Message);
            }
        }
    }
}