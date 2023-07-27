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
    /// 根据二维码查询
    /// </summary>
    public partial class query_by_qr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderModel model = new OrderModel();

            string qrcode = PubFun.QueryString("qrcode");
            string md5 = PubFun.QueryString("md5");
            string token = PubFun.QueryString("token");
            string logMsg = string.Format("query_by_qr。qrcode:{0},md5:{1},token:{2}", qrcode, md5, token);
            NJiaSu.Libraries.LogHelper.Log.Info(logMsg);

            #region 参数必须存在
            if (qrcode == "" || token == ""||md5=="")
            {
                model.Status = "0";
                model.StatusMsg = "查询参数错误";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion

            #region 比对MD5
            string sysMd5 = Encrypt.GetMd5Hash(qrcode + ApiHelper.md5Key);
            //ETicket.Utility.LogHelper.Log.Info(sysMd5 +","+ md5);
            if (sysMd5 != md5)
            {
                model.Status = "0";
                model.StatusMsg = "参数与签名不一致";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion

            #region 二维码格式验证
            string[] strArray = qrcode.Split('-');
            if (strArray.Length != 2)
            {
                model.Status = "0";
                model.StatusMsg = "提交的二维码格式错误";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            
            string orderMD5 = strArray[0];//订单md5
            string strID = strArray[1];//订单信息
            if (strID.Length < 7)
            {
                model.Status = "0";
                model.StatusMsg = "二维码中订单参数错误";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            string rmStr = strID.Substring(0, 6);//去掉前面6位;
            strID = strID.Replace(rmStr, "");
            int orderID = PubFun.Char2Num(strID);//转换为OrderID
            if (orderID <= 0)
            {
                model.Status = "0";
                model.StatusMsg = "二维码中订单参数错误";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion
            
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

            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
            if (sheet == null)
            {
                model.Status = "0";
                model.StatusMsg = "没有查询到记录";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            #region 比对二维码MD5,防止二维码被篡改
            string md5ValidQR = Encrypt.GetMd5Hash(sheet.SheetID + sheet.QRCode + orderID.ToString());//生成MD5比对
            if (orderMD5 != md5ValidQR)
            {
                model.Status = "0";
                model.StatusMsg = "二维码签名与参数不符";
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
                return;
            }
            #endregion

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
                model.StatusMsg = string.Format("你没有权限验证[{0}]，请联系管理员",sheet.ProductName);
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
                model.StatusMsg = string.Format("抱歉！订单无法验票，状态为：{0}。", sheet.OrderStatus);
                string josn = JsonHelper.GetJson<OrderModel>(model);
                Response.Write(josn);
            }

            //model =ApiHelper.Sheet2Model(sheet);
            //string josnSucc = JsonHelper.GetJson<OrderModel>(model);
            //Response.Write(josnSucc);
        }
    }
}