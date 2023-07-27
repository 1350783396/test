using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.openapi
{
    public partial class _interface : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var method = PubFun.QueryString("method");//接口
            switch (method)
            {
                case "buy_ticket":
                    //新订单
                    buy_ticket();
                    break;
                case "cancel_ticket":
                    //退款订单
                    cancel_ticket();
                    break;
                default:
                    break;

            }
        }

        void buy_ticket()
        {
            //返回结果
            JsonResult jsonModel = new JsonResult();
            string jsonStr = "";

            #region 参数
            //获取合作者
            string parterid = PubFun.QueryString("parterid");

            //购买必选参数
            int productID = PubFun.QueryInt("productid");//产品ID
            int buyNum = PubFun.QueryInt("buynum");//购买数量
            string realName = PubFun.QueryString("realname");//姓名
            string phone = PubFun.QueryString("phone");//手机号码

            //门票 游玩日期
            string playDate = PubFun.QueryString("playdate");
            //专线 发生时间发车地点
            string startDate = PubFun.QueryString("startdate");
            string startTime = PubFun.QueryString("starttime");
            string startAddress = PubFun.QueryString("startaddr");

            //验票回调地址
            string notifyurl = PubFun.QueryString("notifyurl");

            string signtype = PubFun.QueryString("signtype");
            string token = PubFun.QueryString("token");

            string payType = "积分支付";
            string validType = "二维码";
            string appos = "开放平台";

            StringBuilder sbLog=new StringBuilder();
            sbLog.AppendLine("parterid"+parterid+"");
            sbLog.AppendLine("productID"+productID+"");
            sbLog.AppendLine("buyNum"+buyNum+"");
            sbLog.AppendLine("realName"+realName+"");
            sbLog.AppendLine("phone"+phone+"");
            sbLog.AppendLine("-------------------门票游玩日期-----------------------");
            sbLog.AppendLine("playDate"+playDate+"");
            sbLog.AppendLine("-------------------专线发车相关-----------------------");
            sbLog.AppendLine("startDate" + startDate + "");
            sbLog.AppendLine("startTime"+startTime+"");
            sbLog.AppendLine("startAddress"+startAddress+"");
            sbLog.AppendLine("-------------------回调及签名-----------------------");
            sbLog.AppendLine("notifyurl"+notifyurl+"");
            sbLog.AppendLine("signtype"+signtype+"");
            sbLog.AppendLine("token"+token+"");

            NJiaSu.Libraries.LogHelper.LogResult("openapi", sbLog.ToString());

            #endregion

            #region 验证
          

            #region 身份验证
            EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.OpenParterID == parterid);
            if (user == null || user.UserID == 0)
            {
                //生成json
                jsonModel.Status = "0";
                jsonModel.Msg = "合作者ID错误";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if (user.OpenStatus == false)
            {
                //生成json
                jsonModel.Status = "0";
                jsonModel.Msg = "合作者接口没有开通";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if (user.UserCategory == "partner" || user.UserCategory == "user")
            {

            }
            else
            {
                jsonModel.Status = "0";
                jsonModel.Msg = "只有分销商才能使用该接口";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            #endregion

            #region 基本验证
            if (buyNum <= 0)
            {
                jsonModel.Status = "0";
                jsonModel.Msg = "购买数量必须大于或者等于1";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if (realName == "")
            {
                jsonModel.Status = "0";
                jsonModel.Msg = "提交失败，没有填写姓名";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if (phone == "")
            {
                jsonModel.Status = "0";
                jsonModel.Msg = "提交失败，没有填写手机号码";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            #endregion

            #region 签名认证
            if (signtype.ToLower() == "md5")
            {
                string signContent = "buy_ticket" + parterid + productID + buyNum + realName + phone + playDate + startDate + startTime + startAddress + notifyurl;//注：各参数的顺利不要弄乱了
                string m1 = Encrypt.GetMd5Hash(signContent);
                string m2 = Encrypt.GetMd5Hash(user.OpenKey + m1);

                if(m2.ToLower()!=token.ToLower())
                {
                    jsonModel.Status = "0";
                    jsonModel.Msg = "签名错误";
                    jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                    Response.Write(jsonStr);
                    Response.End();
                    return;
                }
            }
            else
            {
                jsonModel.Status = "0";
                jsonModel.Msg = "签名错误";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            #endregion

            #endregion

            #region 生成订单
            EFEntity.ProductStock stockModel = null;
            EFDAO.TransactionBaseDAO trans = new EFDAO.TransactionBaseDAO();

            int orderID = 0;

            DateTime stTime = new DateTime();
            DateTime lastOrderTime = new DateTime();
            bool transState = false;
            try
            {
                trans.BeginTransaction();

                //级别
                EFEntity.UserLevel userLevel = trans.GetEntity<EFEntity.UserLevel>(p => p.UserLevelID == user.UserLevelID);

                //商品
                EFEntity.Product product = trans.GetEntity<EFEntity.Product>(p => p.ProductID == productID);
                //价格
                EFEntity.ProductSUK suk = trans.GetEntity<EFEntity.ProductSUK>(p => p.ProductID == productID && p.UserLevelID == user.UserLevelID);

                #region 逻辑验证
                if (!product.SaleFlag.Value)
                {
                    trans.RollBack();

                    //生成json
                    jsonModel.Status = "0";
                    jsonModel.Msg = "对不起，产品已经下线，无法购买";
                    jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                    Response.Write(jsonStr);
                    Response.End();
                    return;
                }

                #region 专线验证
                if (product.CategoryID == "line")
                {
                    if (startAddress == "")
                    {
                        trans.RollBack();//回滚

                        jsonModel.Status = "0";
                        jsonModel.Msg = "提交失败，没有提交上车地点";
                        jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                        Response.Write(jsonStr);
                        Response.End();
                        return;
                    }
                    if (startDate == "" || startTime == "")
                    {
                        trans.RollBack();//回滚

                        jsonModel.Status = "0";
                        jsonModel.Msg = "提交失败，没有提交发车时间";
                        jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                        Response.Write(jsonStr);
                        Response.End();
                        return;
                    }
                }
                //验证库存、截止下单时间
                if (product.CategoryID == "line")
                {
                    //专线        
                    stTime = Convert.ToDateTime(startDate + " " + startTime);
                    DateTime saleDate = Convert.ToDateTime(stTime.ToString("yyyy-MM-dd 00:00:00"));
                    int startH = stTime.Hour;
                    int startM = stTime.Minute;
                    //对比库存
                    //stockModel = BLL.ProductStockBLL.Instance.GetEntity(p => p.ProductID == productID && p.SaleDate == saleDate && p.StartH == startH && p.StartM == startM);
                    stockModel = trans.GetEntity<EFEntity.ProductStock>(p => p.ProductID == productID && p.SaleDate == saleDate && p.StartH == startH && p.StartM == startM);
                    if (stockModel != null && stockModel.Stock < buyNum)
                    {
                        trans.RollBack();

                        jsonModel.Status = "0";
                        jsonModel.Msg = "购买的数量已少与目前的库存(" + stockModel.Stock + ")";
                        jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                        Response.Write(jsonStr);
                        Response.End();
                        return;
                    }

                    //对比最晚下单时间
                    var timeModel = BLL.ProductSaleTimeBLL.Instance.GetEntity(p => p.ProductID == productID && p.StartH == startH & p.StartM == startM);
                    if (timeModel != null)
                    {
                        //最晚下单时间
                        string strSaleOrderTime = stTime.ToString("yyyy-MM-dd") + " " + timeModel.LastOrderH + ":" + timeModel.LastOrderM;
                        lastOrderTime = Convert.ToDateTime(strSaleOrderTime);//最晚下单时间
                        if (lastOrderTime < DateTime.Now)
                        {
                            trans.RollBack();

                            jsonModel.Status = "0";
                            jsonModel.Msg = "对不起，专线车已经达到截止下单时间，无法购买，请购买下一趟";
                            jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                            Response.Write(jsonStr);
                            Response.End();
                            return;
                        }
                    }
                    else
                    {
                        trans.RollBack();

                        jsonModel.Status = "0";
                        jsonModel.Msg = "订单所提交的发车时间错误，该专线没有该时刻的车";
                        jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                        Response.Write(jsonStr);
                        Response.End();
                        return;
                    }
                }
                #endregion

                #region 景区验证
                if (product.CategoryID == "ticket")
                {
                    try
                    {
                        Convert.ToDateTime(playDate);
                    }
                    catch (Exception ex)
                    {
                        trans.RollBack();//回滚

                        jsonModel.Status = "0";
                        jsonModel.Msg = "提交失败，游玩日期格式错误";
                        jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                        Response.Write(jsonStr);
                        Response.End();
                        return;
                    }

                }
                #endregion

                #region 支付权限、支付额度验证
                //积分支付 权限
                if (payType == "积分支付")
                {
                    if (user.EnableAccountPay == null || user.EnableAccountPay.Value == false)
                    {
                        trans.RollBack();

                        jsonModel.Status = "0";
                        jsonModel.Msg = "你没有使用积分支付的权限，请联系管理员开通";
                        jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                        Response.Write(jsonStr);
                        Response.End();
                        return;
                    }
                }
                //现金支付 权限
                if (payType == "现金支付")
                {
                    if (user.EnableCashPay == null || user.EnableCashPay.Value == false)
                    {
                        trans.RollBack();

                        jsonModel.Status = "0";
                        jsonModel.Msg = "你没有使用现金支付的权限";
                        jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                        Response.Write(jsonStr);
                        Response.End();
                        return;
                    }
                }
                //支付总额
                decimal totalPrice = Math.Round(buyNum * suk.ProductPrice.Value, 2);
                //验证 积分是否足够支付
                if (payType == "积分支付")
                {
                    if (totalPrice > user.Account.Value)
                    {
                        trans.RollBack();

                        jsonModel.Status = "0";
                        jsonModel.Msg = string.Format("你目前的积分{0}不足够支付目前订单金额{1}", user.Account.Value, totalPrice);
                        jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                        Response.Write(jsonStr);
                        Response.End();
                        return;
                    }
                }
                #endregion

                #endregion

                //写入数据库
                EFEntity.OrderSheet order = new EFEntity.OrderSheet();
                order.ProductID = product.ProductID;//商品ID  
                order.CategoryID = product.CategoryID;//类别：专线、景区
                order.ProductName = product.ProductName;//商品名称
                order.NUM = buyNum;//购买数量
                order.UnitPrice = suk.ProductPrice.Value;//单价
                order.TotalPrice = totalPrice;//总价
                order.OrderTime = DateTime.Now;//下单时间
                order.PayType = payType;//支付方式
                order.PayState = ETicket.Utility.PayStateEnum.未支付.ToString();
                order.PayFailTime = DateTime.Now.AddMinutes(40);//支付过期取消时间
                //order.TicketFailTime //票据失效时间;
                order.ValidType = validType;//取票方式
                order.ValidCode = PubFun.GetRandNumString(6);//取票验证码
                order.QRCode = PubFun.GetRandNumString(6);//二维码-后6位
                order.SheetID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + PubFun.GetRandNumString(4);//订单编号(外部)
                order.SMSSendStatus = "未发送";//ETicket.Utility.SMSSendStatusEnum.发送中.ToString();//短信发送状态
                order.Phone = phone;//短信接收号码
                order.IDCard = "";//身份证号
                order.RealName = realName;//联系人姓名

                order.UserID = user.UserID;//下单人ID
                order.UserName = user.UserName;//下单人登录名
                order.UserCategory = user.UserCategory;//下单用户类别
                if (userLevel != null)
                    order.UserLevelName = userLevel.UserLevelName;
                order.CPName = user.CPName;//分销商公司名称
                order.CPTel = user.Tel;//座机电话
                order.ClientType = ETicket.Utility.ClientTypeEnum.openapi.ToString();
                order.ClientName = appos;
                if(notifyurl!="")
                {
                    order.OpenApiSignType = signtype;
                    order.OpenApiNotifyUrl = notifyurl;
                }
                
                //返现积分值
                if (suk.Rebate != null && suk.Rebate > 0)
                {
                    order.RebateUnit = suk.Rebate;
                    order.RebateTotal = 0;
                }
                else
                {
                    order.RebateUnit = 0;
                    order.RebateTotal = 0;
                }

                order.OrderStatus = ETicket.Utility.OrderStatusEnum.待付款.ToString();
                if (product.CategoryID == "line")
                {
                    //专线订单
                    order.StartAddress = startAddress;
                    order.StartTime = stTime;
                    order.StartH = stTime.Hour;
                    order.StartM = stTime.Minute;
                    order.LastOrderTime = lastOrderTime;

                    //如果存在销量限制
                    if (stockModel != null)
                    {
                        order.TickID = stockModel.PKID;
                        //更改库存
                        stockModel.Stock = stockModel.Stock - buyNum;
                        trans.UpdateObject<EFEntity.ProductStock>(stockModel);
                    }
                }
                else
                {
                    //景区订单
                    //product.Stock = product.Stock - buyNum;
                    //trans.UpdateObject<EFEntity.Product>(product);//更改库存
                    order.PalyDate = Convert.ToDateTime(playDate);
                    order.EnableValid = product.EnableValid.Value;
                }
                orderID = trans.AddOrderSheet(order);//添加订单

                trans.Commit();//提交事务
                transState = true;
            }
            catch (Exception ex)
            {
                transState = false;
                trans.RollBack();//回滚事务
                NJiaSu.Libraries.LogHelper.Log.Info(ex.ToString());
            }
            finally
            {
                trans.Close();//关闭事务
            }

            if (transState)
            {
                //更新订单号
                var updateModel = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
                updateModel.SheetID = PubFun.GetRandNumString(2) + Utility.CovertHelper.FormatOrderID(orderID.ToString());
                BLL.OrderSheetBLL.Instance.UpdateObject(updateModel);

                /*
                //更新积分日志
                if (accountLogID>0)
                {
                    var accountLog = BLL.AccountLogBLL.Instance.GetEntity(p => p.PKID == accountLogID);
                    accountLog.FormOrderID = orderID;
                    accountLog.Memo = string.Format("订单{0}积分返现：{1}", updateModel.SheetID, accountLog.ActAmount);
                    BLL.AccountLogBLL.Instance.UpdateObject(accountLog);
                }
                */
                string msg = BLL.OrderSheetBLL.Instance.PayAccount(orderID, user.UserID);
                if (msg != "")
                {
                    jsonModel.Status = "0";
                    jsonModel.Msg = "订单支付失败。" + msg;
                }
                else
                {
                    jsonModel.Status = "1";
                    jsonModel.Msg = updateModel.OrderID.ToString();
                }
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            else
            {
                //订单提交失败
                jsonModel.Status = "0";
                jsonModel.Msg = "订单提交失败啦！请重试";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            #endregion

        }

        void cancel_ticket()
        {
            //返回结果
            JsonResult jsonModel = new JsonResult();
            string jsonStr = "";

            //获取合作者
            string parterid = PubFun.QueryString("parterid");
            
            int orderid = PubFun.QueryInt("orderid");//订单id
            var reason = PubFun.QueryString("reason");//退款理由
            var notifyurl = PubFun.QueryString("notifyurl");
            //签名
            string signtype = PubFun.QueryString("signtype");
            string token = PubFun.QueryString("token");

            #region 签名认证

            #region 身份验证
            EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.OpenParterID == parterid);
            if (user == null || user.UserID == 0)
            {
                //生成json
                jsonModel.Status = "0";
                jsonModel.Msg = "合作者ID错误";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            EFEntity.OrderSheet sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (sheet == null || sheet.OrderID == 0)
            {
                //生成json
                jsonModel.Status = "0";
                jsonModel.Msg = "订单不存在";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            if(sheet.UserID!=user.UserID)
            {
                //生成json
                jsonModel.Status = "0";
                jsonModel.Msg = "订单合法性验证失败";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            #endregion

            #region 签名认证
            if (signtype.ToLower() == "md5")
            {
                string signContent = "cancel_ticket" + parterid +orderid+ reason;//注：各参数的顺利不要弄乱了
                string m1 = Encrypt.GetMd5Hash(signContent);
                string m2 = Encrypt.GetMd5Hash(user.OpenKey + m1);

                if (m2.ToLower() != token.ToLower())
                {
                    jsonModel.Status = "0";
                    jsonModel.Msg = "签名错误";
                    jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                    Response.Write(jsonStr);
                    Response.End();
                    return;
                }
            }
            else
            {
                jsonModel.Status = "0";
                jsonModel.Msg = "签名错误";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            #endregion

            #endregion


            StringBuilder sbLog = new StringBuilder();
            sbLog.AppendLine("parterid" + parterid + "");
            sbLog.AppendLine("orderid" + orderid + "");
            sbLog.AppendLine("reason" + reason + "");
            sbLog.AppendLine("-------------------回调及签名-----------------------");
            sbLog.AppendLine("notifyurl" + notifyurl + "");
            sbLog.AppendLine("signtype" + signtype + "");
            sbLog.AppendLine("token" + token + "");

            NJiaSu.Libraries.LogHelper.LogResult("openapi", sbLog.ToString());

            if(reason=="")
            {
                reason = "申请退款来自开放平台";
            }

            EFEntity.OrderRefund refund = new EFEntity.OrderRefund();
            refund.OrderID = orderid;
            refund.RMark = reason;
            refund.RTime = DateTime.Now;
            refund.RUserID = user.UserID;
            refund.NotifyUrl = notifyurl;

            string msg = BLL.OrderSheetBLL.Instance.RefundRequest(orderid, refund);

            if (msg == "")
            {
                jsonModel.Status = "1";
                jsonModel.Msg = "申请退款成功";
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }
            else
            {
                jsonModel.Status = "0";
                jsonModel.Msg = msg;
                jsonStr = JsonHelper.GetJson<JsonResult>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }

        }
    }
}