using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using NJiaSu.Libraries;

namespace ETicket.Web.api_yzy
{
    public partial class tuikuan : System.Web.UI.Page
    {
        string key = "62FEF385DA3EA23B088FA68EEB71C95C";
        protected void Page_Load(object sender, EventArgs e)
        {
            NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "info_", this.Request.RawUrl);

            var order_code = PubFun.QueryString("orderCode");
            var retreatBatchNo = PubFun.QueryString("retreatBatchNo");
            var subOrderCode = PubFun.QueryString("subOrderCode");
            var auditStatus = PubFun.QueryString("auditStatus");
            var returnNum = PubFun.QueryString("returnNum");
            var sign = PubFun.QueryString("sign");

            StringBuilder sbLog = new StringBuilder();
            sbLog.AppendLine("order_code:" + order_code);
            sbLog.AppendLine("retreatBatchNo:" + retreatBatchNo);
            sbLog.AppendLine("subOrderCode:" + subOrderCode);
            sbLog.AppendLine("auditStatus:" + auditStatus);
            sbLog.AppendLine("returnNum:" + returnNum);
            sbLog.AppendLine("sign:" + sign);


            if (order_code == "" && sign == "" && auditStatus == "")
            {
                sbLog.AppendLine("退款通知，参数为空");
                NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "fail_", sbLog.ToString());
                return;
            }

            var md5 = APIHelper.GetMd5Hash(order_code + key).ToLower();
            if (md5 != sign)
            {
                sbLog.AppendLine("退款通知，签名不匹配,我方：" + md5 + "|sign：" + sign);
                NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "fail_", sbLog.ToString());
                return;
            }

            try
            {
                var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.SheetID == order_code);
                if (sheet.OrderStatus == "已过期")
                {

                }
                else
                {
                    EFEntity.OrderRefund refund = new EFEntity.OrderRefund();
                    if (auditStatus == "success")
                        refund.PResult = ETicket.Utility.OrderStatusEnum.退款审核通过.ToString();
                    else
                        refund.PResult = ETicket.Utility.OrderStatusEnum.退款审核不通过.ToString();
                    refund.PIP = "0.0.0.0";
                    refund.PUserID = 0;
                    refund.RTime = DateTime.Now;
                    string msg = BLL.OrderSheetBLL.Instance.RefundCheck(sheet.OrderID, refund, false);
                    if (msg == "")
                    {
                        sbLog.AppendLine("退款通知执行成功");
                        NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "succ_", sbLog.ToString());
                    }
                    else
                    {
                        sbLog.AppendLine("退款通知执行失败");
                        NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "fail_", sbLog.ToString());
                    }
                    Response.Write("success");
                }
            }
            catch (Exception ex)
            {
                sbLog.AppendLine(ex.ToString());
                NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "error_imp", sbLog.ToString());
            }

        }
    }
}