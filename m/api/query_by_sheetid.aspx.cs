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
    /// 根据订单ID查询
    /// </summary>
    public partial class query_by_sheetid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderModel model = new OrderModel();

            string sheetid = PubFun.QueryString("sheetid");
            string validcode = PubFun.QueryString("validcode");
            string md5 = PubFun.QueryString("md5");
            string token = PubFun.QueryString("token");
            var logMsg= string.Format("query_by_sheetid。sheetid:{0},validcode:{1},md5:{2},token:{3}", sheetid, validcode, md5, token);
            NJiaSu.Libraries.LogHelper.Log.Info(logMsg);

            if (sheetid == "" || validcode == "" || md5 == "" || token=="")
            {
                model.Status = "0";
                model.StatusMsg = "查询参数错误";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }

            #region 比对MD5
            string sysMd5 = Encrypt.GetMd5Hash(sheetid +validcode+ ApiHelper.md5Key);
            if (sysMd5 != md5)
            {
                model.Status = "0";
                model.StatusMsg = "参数与签名不一致";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion

            //sheetid = "2014070411335196637758";
            //ETicket.Utility.LogHelper.Log.Info(token);
            #region 验证用户
            var user = BLL.UserBLL.Instance.GetLoginModel(token);
            if(user==null)
            {
                model.Status = "0";
                model.StatusMsg = "授权错误，无法获取操作账号";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion

            //var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.SheetID == sheetid && p.ValidCode == validcode);
            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.SheetID == sheetid);
            if(sheet==null)
            {
                model.Status = "0";
                model.StatusMsg = "没有查询到记录";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }

            #region 是否到达验票时间
            if (sheet.CategoryID == "ticket")
            {
                if (BLL.ProductBLL.Instance.IsEnableValidSwitch(sheet.ProductID.Value))
                {
                    if (DateTime.Now < sheet.EnableValidTime.Value)
                    {
                        PubFun.ShowMsg(this, string.Format("下单后需过{0}小时方可取票游玩，请{1}后再来取票", sheet.EnableValid, sheet.EnableValidTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                        return;
                    }
                }
            }
            #endregion

            #region 用户是否有权验该票
            bool validProuduct = false;
            var validList = BLL.ProductValidBLL.Instance.GetEntities(p => p.UserID == user.UserID);
            foreach (var validItem in validList)
            {
                if (validItem.ProductID == sheet.ProductID)
                    validProuduct = true;
            }
            if (!validProuduct)
            {
                model.Status = "0";
                model.StatusMsg = string.Format("你没有权限验证[{0}]，请联系管理员", sheet.ProductName);
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion

            //记录到日志表
            BLL.ValidLogBLL.Instance.Log(sheet.OrderID, sheet.SheetID, sheet.OrderStatus, logMsg);

            //只有已支付状态，才返回信息
            if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.已支付.ToString())
            {
                model = ApiHelper.Sheet2Model(sheet);
                string josnSucc = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josnSucc);
            }
            else if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.已验票.ToString())
            {
                //已验票10分钟内可以再打印
                if (sheet.ValidTime.Value.AddMinutes(10) > DateTime.Now)
                {
                    model = ApiHelper.Sheet2Model(sheet);
                    string josnSucc = JsonHelper.GetJson<OrderModel>(model);
                    Response.Write(josnSucc);
                }
                else
                {
                    model.Status = "0";
                    model.StatusMsg = string.Format("抱歉！订单无法验票，状态为：{0}。", sheet.OrderStatus);
                    string josn = JsonHelper.GetJson<OrderModel>(model);
                    Response.Write(josn);
                }
            }
            else
            {
                model.Status = "0";
                model.StatusMsg = string.Format("抱歉！订单无法验票，状态为：{0}。" , sheet.OrderStatus);
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
            }
        }
    }
}