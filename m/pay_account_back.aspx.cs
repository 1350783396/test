using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;
using ETicket.Utility;

namespace ETicket.Web
{
    public partial class pay_account : PayBase
    {
        public decimal account = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;

            int userID = base.CookiesUser.UserID;
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userID);
            account = user.Account.Value;

        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if(this.txtValidCode.Value.Trim()=="")
            {
                PubFun.ShowMsg(this,"请输入短信验证码");
                return;
            }

            int orderID = PubFun.QueryInt("id");
            var sheetModel = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
            if (sheetModel.PayType != "积分支付")
            {
                PubFun.ShowMsg(this, "你提交的订单不是使用积分支付");
                return;
            }

            int userID = base.CookiesUser.UserID;
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userID);
            var code = this.txtValidCode.Value.Trim();

            if (user.Account < sheetModel.TotalPrice)
            {
                PubFun.ShowMsg(this, string.Format("目前账号积分{0}不足支付订单金额{1}", user.Account, sheetModel.TotalPrice));
                return;
            }

            //验证手机码是否正确
            OperationResult result = BLL.PhoneSmsValidateBLL.Instance.Validate(user.Phone, code);
            if (result.OperateResult == OperateResultEnum.Fail)
            {
                PubFun.ShowMsg(this.Page, "输入的验证码错误");
                return;
            }

            string msg = BLL.OrderSheetBLL.Instance.PayAccount(orderID, user.UserID);
            if(msg!="")
            {
                PubFun.ShowMsg(this.Page, msg);
                return;
            }
            else
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.order_paySucc.GetHashCode());
            }
        }
    }
}