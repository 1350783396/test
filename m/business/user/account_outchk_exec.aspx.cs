using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class account_outchk_exec : AdminBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                Bind();
            }

        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            if (ddlResutl.SelectedValue == "请选择")
            {
                PubFun.ShowMsg(this, "请选择审核结果");
                return;
            }

            int pkid = PubFun.QueryInt("pkid");
            EFEntity.AccountOutFlow flow = BLL.AccountOutFlowBLL.Instance.GetEntity(p => p.PKID == pkid);
            if (flow.Stauts != ETicket.Utility.AccountFlowStatusEnum.申请中.ToString())
            {
                PubFun.ShowMsg(this, "不在待审核状态，无法审核");
                return;
            }

            EFEntity.CookiesUser cookieUser=BLL.UserBLL.Instance.GetLoginModel();

            if (ddlResutl.SelectedValue == "审核通过")
            {

                //验证消除积分是否大于目前所有积分
                bool isAllowOut = BLL.AccountOutFlowBLL.Instance.IsAllowOut(flow.Requst_Amount.Value, flow.UserID.Value);
                if (!isAllowOut)
                {
                    PubFun.ShowMsg(this, "当前积分余额少于" + flow.Requst_Amount.Value.ToString() + ",无法销除,只能操作为审核不通过");
                    return;
                }

                //验证授权码是否正确
                string configKey=ETicket.Utility.ConfigKeyEnum.accout_checkin_code.ToString();
                var modelCofig = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == configKey);
                if(modelCofig==null)
                {
                    PubFun.ShowMsg(this, "系统没有初始化积分审核授权码，请联系技术人员初始化");
                    return;
                }
                string chkValeMD5 = Encrypt.GetMd5Hash(Encrypt.GetMd5Hash(txtChkCode.Text.Trim()));
                if (chkValeMD5 != modelCofig.ConfigValue)
                {
                    PubFun.ShowMsg(this, "输入授权码不正确，请重新输入");
                    return;
                }

                string msg = "";
                #region 审核通过，事务
                EFDAO.TransactionBaseDAO trans = new EFDAO.TransactionBaseDAO();
                try
                {
                    trans.BeginTransaction();
                    //流程
                    var flowModel= trans.GetEntity<EFEntity.AccountOutFlow>(p => p.PKID == pkid);
                    flowModel.Stauts = ETicket.Utility.AccountFlowStatusEnum.已通过.ToString();
                    flowModel.CHK_UserID = cookieUser.UserID;
                    flowModel.CHK_Time = DateTime.Now;
                    if (txtRemark.Text.Trim()!="")
                        flowModel.CHK_Remark = txtRemark.Text.Trim();
                    trans.UpdateObject<EFEntity.AccountOutFlow>(flowModel);
                    //账户日志
                    EFEntity.AccountLog accountLog = new EFEntity.AccountLog();
                    accountLog.UserID = flowModel.UserID;
                    accountLog.ActType = "out";
                    accountLog.ActAmount = flowModel.Requst_Amount;
                    accountLog.Memo = string.Format("销除积分审核通过，销除积分{0}", flowModel.Requst_Amount);
                    accountLog.ActTime = DateTime.Now;
                    accountLog.ActIP = PubFun.GetClientIP();
                    trans.AddObject<EFEntity.AccountLog>(accountLog);

                    //更新账户
                    var user = trans.GetEntity<EFEntity.User>(p => p.UserID == flowModel.UserID);
                    user.Account = user.Account - flowModel.Requst_Amount;
                    trans.UpdateObject<EFEntity.User>(user);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.RollBack();
                    NJiaSu.Libraries.LogHelper.Log.Error(ex.ToString());
                    msg = ex.Message;
                }
                finally
                {

                }
                #endregion

                if (msg == "")
                {
                    PubFun.ShowMsgRedirect(this, "操作成功", PubFun.ApplicationPath + "/business/user/account_outchk_detail.aspx?pkid=" + flow.PKID);
                }
                else
                {
                    PubFun.ShowMsgRedirect(this, "操作失败。"+msg,Request.RawUrl);
                }

            }
            else if (ddlResutl.SelectedValue == "审核不通过")
            {
                if (txtRemark.Text.Trim() == "")
                {
                    PubFun.ShowMsg(this, "审核不通，请填写审核说明");
                    return;
                }

                flow.Stauts = ETicket.Utility.AccountFlowStatusEnum.未通过.ToString();
                flow.CHK_Remark = txtRemark.Text.Trim();

                flow.CHK_UserID = cookieUser.UserID;
                flow.CHK_Time = DateTime.Now;

                BLL.AccountOutFlowBLL.Instance.UpdateObject(flow);

                PubFun.ShowMsgRedirect(this, "操作成功", PubFun.ApplicationPath + "/business/user/account_outchk_detail.aspx?pkid=" + flow.PKID);
            }
        }

        void Bind()
        {
            int pkid = PubFun.QueryInt("pkid");
            EFEntity.AccountOutFlow flow = BLL.AccountOutFlowBLL.Instance.GetEntity(p => p.PKID == pkid);

            litAccount.Text = flow.Requst_Amount.ToString();
            litRemarkRequst.Text = flow.Requst_Remark;

            var rUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == flow.Requst_UserID);
            if (rUser!=null)
                litOPUser.Text = rUser.UserName;

            var toUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == flow.UserID);
            if (toUser!=null)
            {
                litCPName.Text = toUser.CPName;
                litUserName.Text = toUser.UserName;
                litRealName.Text = toUser.RealName;
                litPhone.Text = toUser.Phone;
            }

        }
    }
}