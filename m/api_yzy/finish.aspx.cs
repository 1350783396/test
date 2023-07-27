using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;
using System.Text;

namespace ETicket.Web.api_yzy
{
    public partial class finish : System.Web.UI.Page
    {
        string key = "62FEF385DA3EA23B088FA68EEB71C95C";
        protected void Page_Load(object sender, EventArgs e)
        {
            NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "info_", this.Request.RawUrl);

            var order_code = PubFun.QueryString("order_code");
            var status = PubFun.QueryString("status");
            var checkNum = PubFun.QueryString("checkNum");
            var returnNum = PubFun.QueryString("returnNum");
            var total = PubFun.QueryString("total");
            var sign = PubFun.QueryString("sign");

            StringBuilder sbLog = new StringBuilder();
            sbLog.AppendLine("order_code:" + order_code);
            sbLog.AppendLine("status:" + status);
            sbLog.AppendLine("checkNum:" + checkNum);
            sbLog.AppendLine("returnNum:" + returnNum);
            sbLog.AppendLine("total:" + total);
            sbLog.AppendLine("sign:" + sign);

            if (order_code == "" && sign == "" && status == "")
            {
                sbLog.AppendLine("参数为空");
                NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "fail_", sbLog.ToString());
                return;
            }

            var md5 =APIHelper.GetMd5Hash("order_code=" + order_code + key).ToLower();
            if (md5 != sign)
            {
                sbLog.AppendLine("签名不匹配我方：" + md5 + "|sign：" + sign);
                NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "fail_", sbLog.ToString());
                return;
            }

            

            try
            {
                if (status == "success")
                {
                    //验票
                    var sheet = ETicket.BLL.OrderSheetBLL.Instance.GetEntity(p => p.SheetID == order_code);

                    //更新订单为已验证
                    var f = ETicket.Utility.OrderStatusEnum.已验票.ToString();
                    f = "已验票";
                    sheet.OrderStatus = f;
                    sheet.ValidUserID = 0;
                    sheet.ValidTime = DateTime.Now;
                    BLL.OrderSheetBLL.Instance.UpdateObject(sheet);

                    //记录到日志表,验票成功
                    BLL.ValidLogBLL.Instance.Log(sheet.OrderID, sheet.SheetID, sheet.OrderStatus, "恭喜，验票成功！");
                    //验票成功后续
                    BLL.ValidNotifyEventBLL.Instance.ValidNotify(sheet);
                    BLL.TaoBaoMSBLL.Instance.ValidNotify(sheet);

                    sbLog.AppendLine("回调成功");
                    NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "succ_", sbLog.ToString());

                    Response.Write("success");
                }
            }
            catch(Exception ex)
            {
                sbLog.AppendLine(ex.ToString());
                NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "error_imp", sbLog.ToString());
            }
        }
    }
}