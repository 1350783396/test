using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public partial class shopcar : System.Web.UI.Page
    {
        protected EFEntity.CookiesUser CookiesUser = null;
        protected override void OnLoad(EventArgs e)
        {
            CookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            if (CookiesUser == null)
            {
                Response.Redirect(PubFun.ApplicationPath + "/shopcar_pre.aspx?reurl=" + HttpUtility.UrlEncode(Request.RawUrl));
            }
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            if (cookiesUser.UserCategory == "admin" || cookiesUser.UserCategory == "valid")
            {
                Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.account_no_buy.GetHashCode());
                return;
            }
            if (Request.RequestType == "POST")
            {
                int orderID = 0;
                //int accountLogID = 0;
                int buyNum = PubFun.QueryInt("buyNum");//购买数量
                string selPayType = PubFun.QueryString("selPayType");//支付方式
                string selValidType = PubFun.QueryString("selValidType");//取票方式
                string realName = PubFun.QueryString("realName");//姓名
                string idCard = PubFun.QueryString("idCard");//身份证
                string phone = PubFun.QueryString("phone");//手机号码
                string txtMemo = PubFun.QueryString("txtMemo");//备注
                string properties = PubFun.QueryString("Properties");//备注
                if (txtMemo.Length > 150)
                {
                    txtMemo = txtMemo.Substring(0, 150);
                }
                //以下个字段，只有专线才有
                string selAddress = PubFun.QueryString("startAddress");
                string hiddenStartTime = PubFun.QueryString("hiddenStartTime");

                DateTime startTime = new DateTime();
                DateTime lastOrderTime = new DateTime();

                //以下字段，景区才有
                string palyDate = PubFun.QueryString("palyDate");//游览日期

                //商品ID
                int productID = PubFun.QueryInt("id");
                EFEntity.ProductStock stockModel = null;

                #region 基本验证
                if (buyNum <= 0)
                {
                    PubFun.ShowMsg(this, "购买数量必须大于或者等于1");
                    return;
                }
                if (selPayType == "")
                {
                    PubFun.ShowMsg(this, "请选择支付方式");
                    return;
                }
                if (selValidType == "")
                {
                    PubFun.ShowMsg(this, "请选择取票方式");
                    return;
                }
                if (realName == "")
                {
                    PubFun.ShowMsg(this, "请输入姓名");
                    return;
                }
                //if (idCard == "")
                //{
                //    PubFun.ShowMsg(this, "请输入身份证号");
                //    return;
                //}
                if (phone == "")
                {
                    PubFun.ShowMsg(this, "请输入手机号码");
                    return;
                }
                #endregion

                EFDAO.TransactionBaseDAO trans = new EFDAO.TransactionBaseDAO();

                bool transState = false;
                try
                {
                    trans.BeginTransaction();

                    //用户
                    int userID = cookiesUser.UserID;
                    EFEntity.User user = trans.GetEntity<EFEntity.User>(p => p.UserID == userID);
                    EFEntity.UserLevel userLevel = trans.GetEntity<EFEntity.UserLevel>(p => p.UserLevelID == user.UserLevelID);//级别

                    //商品
                    EFEntity.Product product = trans.GetEntity<EFEntity.Product>(p => p.ProductID == productID);
                    //价格
                    EFEntity.ProductSUK suk = trans.GetEntity<EFEntity.ProductSUK>(p => p.ProductID == productID && p.UserLevelID == user.UserLevelID);

                    #region 逻辑验证
                    if (!product.SaleFlag.Value)
                    {
                        trans.RollBack();
                        PubFun.ShowMsg(this, "对不起，产品已经下线，无法购买");
                        return;
                    }
                    if (product.CategoryID == "line")
                    {
                        if (selAddress == "")
                        {
                            trans.RollBack();
                            PubFun.ShowMsg(this, "请选择上车地点");
                            return;
                        }
                        if (hiddenStartTime == "")
                        {
                            trans.RollBack();
                            PubFun.ShowMsg(this, "请选择发车时间");
                            return;
                        }
                    }
                    //验证库存、截止下单时间
                    if (product.CategoryID == "line")
                    {
                        //专线        
                        startTime = Convert.ToDateTime(hiddenStartTime);
                        DateTime saleDate = Convert.ToDateTime(startTime.ToString("yyyy-MM-dd 00:00:00"));
                        int startH = startTime.Hour;
                        int startM = startTime.Minute;
                        //对比库存
                        //stockModel = BLL.ProductStockBLL.Instance.GetEntity(p => p.ProductID == productID && p.SaleDate == saleDate && p.StartH == startH && p.StartM == startM);
                        stockModel = trans.GetEntity<EFEntity.ProductStock>(p => p.ProductID == productID && p.SaleDate == saleDate && p.StartH == startH && p.StartM == startM);
                        if (stockModel != null && stockModel.Stock < buyNum)
                        {
                            trans.RollBack();
                            PubFun.ShowMsg(this, "购买的数量已少与目前的库存(" + stockModel.Stock + ")");
                            return;
                        }


                        //对比最晚下单时间
                        var timeModel = BLL.ProductSaleTimeBLL.Instance.GetEntity(p => p.ProductID == productID && p.StartH == startH & p.StartM == startM);
                        if (timeModel != null)
                        {
                            //最晚下单时间
                            string strSaleOrderTime = startTime.ToString("yyyy-MM-dd") + " " + timeModel.LastOrderH + ":" + timeModel.LastOrderM;
                            lastOrderTime = Convert.ToDateTime(strSaleOrderTime);//最晚下单时间
                            if (lastOrderTime < DateTime.Now)
                            {
                                trans.RollBack();
                                PubFun.ShowMsg(this, "对不起，已经达到截止下单时间，无法购买");
                                return;
                            }
                        }
                    }
                    //else
                    //{
                    //    //景区
                    //    if (product.Stock.Value < buyNum)
                    //    {
                    //        trans.RollBack();
                    //        PubFun.ShowMsg(this, "购买的数量已少与目前的库存(" + product.Stock.Value + ")");
                    //        return;
                    //    }
                    //}

                    //积分支付 权限
                    if (selPayType == "积分支付")
                    {
                        if (user.EnableAccountPay == null)
                        {
                            trans.RollBack();
                            PubFun.ShowMsg(this, "你没有使用积分支付的权限");
                            return;
                        }
                        if (user.EnableAccountPay.Value == false)
                        {
                            trans.RollBack();
                            PubFun.ShowMsg(this, "你没有使用积分支付的权限");
                            return;
                        }
                    }
                    //现金支付 权限
                    if (selPayType == "现金支付")
                    {
                        if (user.EnableCashPay == null)
                        {
                            trans.RollBack();
                            PubFun.ShowMsg(this, "你没有使用现金支付的权限");
                            return;
                        }
                        if (user.EnableCashPay.Value == false)
                        {
                            trans.RollBack();
                            PubFun.ShowMsg(this, "你没有使用现金支付的权限");
                            return;
                        }
                    }
                    //支付总额
                    decimal totalPrice = Math.Round(buyNum * suk.ProductPrice.Value, 2);
                    //验证 积分是否足够支付
                    if (selPayType == "积分支付")
                    {
                        if (totalPrice > user.Account.Value)
                        {
                            trans.RollBack();
                            PubFun.ShowMsg(this, string.Format("你目前的积分{0}不足够支付目前订单金额{1}", user.Account.Value, totalPrice));
                            return;
                        }
                    }
                    #endregion

                    //写入数据库
                    EFEntity.OrderSheet order = new EFEntity.OrderSheet();
                    order.ProductID = product.ProductID;//商品ID  
                    order.CategoryID = product.CategoryID;//类别：专线、景区
                    order.ProductName = product.ProductName;//商品名称
                    order.Properties = properties;
                    order.NUM = buyNum;//购买数量
                    order.UnitPrice = suk.ProductPrice.Value;//单价
                    order.TotalPrice = totalPrice;//总价
                    order.OrderTime = DateTime.Now;//下单时间
                    order.PayType = selPayType;//支付方式
                    order.PayState = ETicket.Utility.PayStateEnum.未支付.ToString();
                    order.PayFailTime = DateTime.Now.AddMinutes(40);//支付过期取消时间
                    //order.TicketFailTime //票据失效时间;
                    order.ValidType = selValidType;//取票方式
                    order.ValidCode = PubFun.GetRandNumString(6);//取票验证码
                    order.QRCode = PubFun.GetRandNumString(6);//二维码-后6位
                    order.SheetID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + PubFun.GetRandNumString(4);//订单编号(外部)
                    order.SMSSendStatus = "未发送";//ETicket.Utility.SMSSendStatusEnum.发送中.ToString();//短信发送状态
                    order.Phone = phone;//短信接收号码
                    order.IDCard = idCard;//身份证号
                    order.RealName = realName;//联系人姓名
                    order.Memo = txtMemo;

                    order.UserID = user.UserID;//下单人ID
                    order.UserName = user.UserName;//下单人登录名
                    order.UserCategory = user.UserCategory;//下单用户类别
                    if (userLevel != null)
                        order.UserLevelName = userLevel.UserLevelName;
                    order.CPName = user.CPName;//分销商公司名称
                    order.CPTel = user.Tel;//座机电话
                    order.ClientType = ETicket.Utility.ClientTypeEnum.pc.ToString();
                    order.ClientName = PubFun.GetBrowserInfo();

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
                        order.StartAddress = selAddress;
                        order.StartTime = startTime;
                        order.StartH = startTime.Hour;
                        order.StartM = startTime.Minute;
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
                        order.PalyDate = Convert.ToDateTime(palyDate);
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

                    //订单提交成功
                    //if(orderID>0)
                    //{
                    if (selPayType == "在线支付")
                    {
                        Response.Redirect("pay_online.aspx?id=" + orderID);
                    }
                    else if (selPayType == "积分支付")
                    {
                        Response.Redirect("pay_account.aspx?id=" + orderID);
                    }
                    else if (selPayType == "现金支付")
                    {
                        Response.Redirect("pay_cash.aspx?id=" + orderID);
                    }
                    //}
                    //else
                    //{

                    //}
                }
                else
                {
                    //订单提交失败
                    PubFun.ShowMsg(this, "订单提交失败啦！请重试");
                }
            }


        }
       
    }
}