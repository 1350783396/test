using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.order
{
    public partial class order_refund_cz : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnExec.Click += btnExec_Click;
        }

        void btnExec_Click(object sender, EventArgs e)
        {
            string sheetID = txtSheetID.Text.Trim();
            if (sheetID == "")
            {
                PubFun.ShowMsg(this, "请输入订单号");
                return;
            }
            
            var cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            var userID=cookiesUser.UserID;

            string sql = "update [ValidLog] set [ClearFlag]=1,[ClearTime]='{0}',[ClearUserID]={1} where [SheetID]='{2}'";
            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userID, sheetID);

            try
            {
                ETicket.BLL.DbHelperSQL.ExecuteSql(sql);
                PubFun.ShowMsgRedirect(this, "执行成功！",this.Request.RawUrl);
            }
            catch
            {
                PubFun.ShowMsg(this, "执行发生异常，请联系管理员");
            }

        }
    }
}