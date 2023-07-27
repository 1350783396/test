using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public partial class refund_cash : LoginBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            if (!Page.IsPostBack)
            {
                EFEntity.OrderSheet sheet = null;
                RefundController.Instance.isDo(Response, out sheet, base.CookiesUser);
                if (sheet.PayType != "现金支付")
                {
                    Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.RefundPageError.GetHashCode());
                }
                RefundController.Instance.Bind(this, sheet);
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string reason = txtReason.Text.Trim();
            if(reason=="")
            {
                PubFun.ShowMsg(this, "请输入申请退款原因");
                return;
            }
            int orderID = PubFun.QueryInt("id");
            EFEntity.OrderRefund refund=new EFEntity.OrderRefund();
            refund.OrderID = orderID;
            refund.RMark = reason;
            refund.RTime = DateTime.Now;
            refund.RUserID = base.CookiesUser.UserID;
            string msg = BLL.OrderSheetBLL.Instance.RefundRequest(orderID, refund);
            if(msg=="")
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.RefundSucc.GetHashCode());
            }
            else
            {
                PubFun.ShowMsg(this, "申请退款失败。"+msg);
            }

        }
    }
}