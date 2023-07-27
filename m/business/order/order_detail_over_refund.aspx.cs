using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.order
{
    public partial class order_detail_over_refund : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnChk.Click += btnChk_Click;
            this.btnRefresh.Click += btnRefresh_Click;
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            Bind();
        }

        void btnChk_Click(object sender, EventArgs e)
        {
            int orderid = PubFun.QueryInt("orderid");

            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (sheet.ProductID == ETicket.Utility.SiteConfigHelper.YZYTicket && sheet.UserLevelName != "阳朔分销商")
            {
                
                if (DateTime.Compare(sheet.OrderTime.Value, ETicket.Utility.SiteConfigHelper.YZYTime) > 0)
                {
                    PubFun.ShowMsg(this, "银子岩门票不支持过期退款，无法知道智游宝订单是否退款。");
                    return;
                }
                

                /*
                //---2017.7.12 停止使用接口
                if (DateTime.Compare(sheet.OrderTime.Value, ETicket.Utility.SiteConfigHelper.YZY_EndTime) < 0)
                {
                    if (sheet.Out_TuiKStatus == null || sheet.Out_TuiKStatus.Value != 1)
                    {
                        PubFun.ShowMsg(this, "智游宝订单尚未退款。无法完成操作，请等待");
                        return;
                    }
                }
                */
            }

            //验证授权码是否正确
            string configKey = ETicket.Utility.ConfigKeyEnum.accout_checkin_code.ToString();
            var modelCofig = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == configKey);
            if (modelCofig == null)
            {
                PubFun.ShowMsg(this, "系统没有初始化积分审核授权码，请联系技术人员初始化");
                return;
            }
            string chkValeMD5 = Encrypt.GetMd5Hash(Encrypt.GetMd5Hash(txtChkCode.Text.Trim()));
            if (chkValeMD5 != modelCofig.ConfigValue)
            {
                PubFun.ShowMsg(this, "输入授权码不正确，请重新输入");
                return;
            }
            if(this.txtRefundPrice.Text.Trim()=="")
            {
                PubFun.ShowMsg(this, "请输入退款金额");
                return;
            }
            var totalPrice = Convert.ToDecimal(this.litSheetPrice.Text);
            var overRefundPrice=Convert.ToDecimal(this.txtRefundPrice.Text.Trim());
            if(overRefundPrice>totalPrice)
            {
                PubFun.ShowMsg(this, "退款金额不能大于订单金额");
                return;
            }

            string msg = BLL.OrderSheetBLL.Instance.OverRefund(orderid, overRefundPrice,base.CookiesUser.UserID,this.txtReason.Text.Trim());
            if (msg == "")
            {
                Bind();
                PubFun.ShowMsgRedirect(this, "操作成功", "/business/order/order_detail.aspx?orderid=" + orderid);
            }
            else
            {
                PubFun.ShowMsg(this, "操作失败：" + msg);
            }
        }

        void Bind()
        {
            int orderid = PubFun.QueryInt("orderid");
            EFEntity.OrderSheet sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (sheet == null)
                return;

            if(sheet.OrderStatus==ETicket.Utility.OrderStatusEnum.过期已退.ToString())
            {
                Response.Redirect("order_detail.aspx?orderid=" + orderid);
            }
            if(sheet.ClientType==ETicket.Utility.ClientTypeEnum.weixin.ToString())
            {
                PubFun.ShowMsgRedirect(this, "微信/扫码订单不支持过期退", "/business/order/order_detail.aspx?orderid=" + orderid);
                return;
            }

            if (sheet.CategoryID == "line")
            {
                trLine.Visible = true;
                trTicket.Visible = false;
            }
            else if (sheet.CategoryID == "ticket")
            {
                trLine.Visible = false;
                trTicket.Visible = true;
            }

            var clientStr = ETicket.Utility.CovertHelper.ConvertClientType2Zh(sheet.ClientType);
            litProductName.Text = sheet.ProductName + string.Format("<span style='color:red'>[{0}]</span>", clientStr);

            litOrderState.Text = sheet.OrderStatus;
            litSMSStatus.Text = BLL.SMSSendOrderBLL.Instance.GetSmsStatus(sheet.OrderID);
            litUnitPrice.Text = sheet.UnitPrice.ToString();
            litBuyNum.Text = sheet.NUM.ToString();
            litTotalPrice.Text = sheet.TotalPrice.ToString();
            litSheetID.Text = sheet.SheetID;
            litOrderTime.Text = sheet.OrderTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            litName.Text = sheet.RealName;
            litIDCard.Text = sheet.IDCard;
            litPhone.Text = sheet.Phone;

            litPayType.Text = sheet.PayType;
            if (!string.IsNullOrEmpty(sheet.PayTypeSub))
                litPayType.Text += "&nbsp;&nbsp;" + sheet.PayTypeSub;
            litValidType.Text = sheet.ValidType;

            litUserLevel.Text = sheet.UserLevelName;
            litUserName.Text = sheet.UserName;

            if (sheet.StartTime != null)
                litStartTime.Text = sheet.StartTime.Value.ToString("yyyy-MM-dd HH:mm");
            if (sheet.StartAddress != null)
                litStartAddress.Text = sheet.StartAddress;

            if (sheet.EnableValidTime != null)
            {
                string enableValid = "";
                if (sheet.EnableValid == null || sheet.EnableValid <= 0)
                {
                    enableValid = "立刻";
                }
                else
                {
                    enableValid = sheet.EnableValid + "小时后";
                }
                string msgEnableTime = "下单成功{0}可取票，即{1}起可取票";
                string enableTime = sheet.EnableValidTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

                this.litEnableValidTime.Text = string.Format(msgEnableTime, enableValid, enableTime);
            }
            this.litPlayDate.Text = sheet.PalyDate == null ? "" : sheet.PalyDate.Value.ToString("yyyy-MM-dd");

            /*
            var refund = BLL.OrderRefundBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (refund != null)
            {
                litRTime.Text = refund.RTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                litRMark.Text = refund.RMark;
            }
            */

           //默认退款80%
            litSheetPrice.Text = sheet.TotalPrice.Value.ToString();
            this.txtRefundPrice.Text = Math.Round(sheet.TotalPrice.Value * (decimal)0.8, 2).ToString();

           

        }
    }
}