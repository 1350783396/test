using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.order
{
    public partial class order_detail_chk_view : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnRefresh.Click += btnRefresh_Click;
            this.btnRefund.Click += btnRefund_Click;
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        void btnRefund_Click(object sender, EventArgs e)
        {
            int orderid = PubFun.QueryInt("orderid");
            if(orderid<=0)
            {
                PubFun.ShowMsg(this, "操作失败。订单信息错误" );
                return;
            }
            string msg = BLL.OrderSheetBLL.Instance.RefundFinish(orderid);
            if(msg=="")
            {
                PubFun.ShowMsgRedirect(this, "操作成功", Request.RawUrl);
            }
            else
            {
                PubFun.ShowMsg(this, "操作失败。" + msg);
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            Bind();
        }


        void Bind()
        {
            int orderid = PubFun.QueryInt("orderid");
            EFEntity.OrderSheet sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (sheet == null)
                return;

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

            #region 退款原因
            var refund = BLL.OrderRefundBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (refund != null)
            {
                litRTime.Text = refund.RTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                litRMark.Text = refund.RMark;

                if (refund.PTime != null)
                {
                    litPTime.Text = refund.PTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    litPTime.Text = "退款尚未审核";
                }
                if (!string.IsNullOrEmpty(refund.PResult))
                {
                    litPResult.Text = refund.PResult;
                }
                else
                {
                    litPResult.Text = "退款尚未审核";
                }
                if (!string.IsNullOrEmpty(refund.PMark))
                {
                    litPMark.Text = refund.PMark;
                }
            }
            #endregion

            if(sheet.OrderStatus==ETicket.Utility.OrderStatusEnum.已退款.ToString())
            {
                litIsFinish.Text = "已完成";
            }
            else
            {
                litIsFinish.Text = "未完成";
            }
            if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.退款审核通过.ToString())
            {
                this.btnRefund.Visible =true;
            }
        }
    }
}