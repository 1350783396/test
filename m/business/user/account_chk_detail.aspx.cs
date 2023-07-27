using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class account_chk_detail : AdminBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }

        }

    
        void Bind()
        {
            int pkid = PubFun.QueryInt("pkid");
            EFEntity.AccountFlow flow = BLL.AccountFlowBLL.Instance.GetEntity(p => p.PKID == pkid);

            litAccount.Text = flow.Requst_Amount.ToString();
            litRemarkRequest.Text = flow.Requst_Remark;

            var opUser = BLL.UserBLL.Instance.GetEntity(p=>p.UserID==flow.Requst_UserID);
            litOPUser.Text = opUser.UserName;

            var toUser = BLL.UserBLL.Instance.GetEntity(p => p.UserID == flow.UserID);
            litCPName.Text = toUser.CPName;
            litUserName.Text = toUser.UserName;
            litRealName.Text = toUser.RealName;
            litPhone.Text = toUser.Phone;

            if (flow.Stauts == ETicket.Utility.AccountFlowStatusEnum.申请中.ToString())
            { 
                litStatus.Text = "尚无进行审核";
                litRemak.Text = "";
            }
            else
            {
                litStatus.Text = flow.Stauts;
                if (string.IsNullOrEmpty(flow.CHK_Remark))
                    litRemak.Text = "无";
                else
                    litRemak.Text = flow.CHK_Remark;
            }

        }
    }
}