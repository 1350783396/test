using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.api
{
    /// <summary>
    /// 从客户端返回订单ID，更改状态
    /// </summary>
    public partial class post_chg_status : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StatusModel model = new StatusModel();
            int orderID = PubFun.QueryInt("orderid");
            string md5 = PubFun.QueryString("md5");
            string token = PubFun.QueryString("token");
            string logMsg = string.Format("post_chg_status。orderid:{0},md5:{1},token:{2}", orderID, md5, token);
            NJiaSu.Libraries.LogHelper.Log.Info(logMsg);

            #region 必须存在参数
            if (orderID <= 0 || md5 == "" || token=="")
            {
                model.Status = "0";
                model.StatusMsg = "验票失败，提交参数错误";
                string josn = JsonHelper.GetJson<StatusModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion

            #region 比对MD5
            string sysMd5 = Encrypt.GetMd5Hash(orderID + ApiHelper.md5Key);
            if (sysMd5 != md5)
            {
                model.Status = "0";
                model.StatusMsg = "参数与签名不一致";
                string josn = JsonHelper.GetJson<StatusModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion

            #region 验证用户
            var user = BLL.UserBLL.Instance.GetLoginModel(token);
            if (user == null)
            {
                model.Status = "0";
                model.StatusMsg = "授权错误，无法获取操作账号";
                string josn = JsonHelper.GetJson<StatusModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion

            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
            if (sheet == null)
            {
                model.Status = "0";
                model.StatusMsg = "验票失败，没有找到相关记录";
                string josn = JsonHelper.GetJson<StatusModel>(model);
                Response.Write(josn);
                return;
            }

            //记录到日志表
            BLL.ValidLogBLL.Instance.Log(sheet.OrderID, sheet.SheetID, sheet.OrderStatus, logMsg);

            try 
            { 
                //更新订单为已验证
                var f=ETicket.Utility.OrderStatusEnum.已验票.ToString();
                f="已验票";
                sheet.OrderStatus =f;
                sheet.ValidUserID = user.UserID;
                sheet.ValidTime = DateTime.Now;
                BLL.OrderSheetBLL.Instance.UpdateObject(sheet);

                //记录到日志表,验票成功
                BLL.ValidLogBLL.Instance.Log(sheet.OrderID, sheet.SheetID, sheet.OrderStatus, "恭喜，验票成功！");
                //验票成功后续
                BLL.ValidNotifyEventBLL.Instance.ValidNotify(sheet);
                BLL.TaoBaoMSBLL.Instance.ValidNotify(sheet);

                model.Status = "1";
                model.StatusMsg = "恭喜，验票成功！";
                string josn = JsonHelper.GetJson<StatusModel>(model);
                Response.Write(josn);
                return;
            }
            catch
            {
                model.Status = "0";
                model.StatusMsg = "验票失败，没有找到相关记录";
                string josn = JsonHelper.GetJson<StatusModel>(model);
                Response.Write(josn);
                return;
            }
        }

    }
}