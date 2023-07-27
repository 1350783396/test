using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.order
{
    public partial class order_detail_chk : AdminBase
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
            if (this.ddlResult.SelectedValue == "请选择")
            {
                PubFun.ShowMsg(this, "请选择审核结果");
                return;
            }
            else if (this.ddlResult.SelectedValue == "审核不通过")
            {
                if (this.txtReason.Text.Trim() == "")
                {
                    PubFun.ShowMsg(this, "审核不通过,请填写不通过的理由");
                    return;
                }
            }

            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (sheet.ProductID == ETicket.Utility.SiteConfigHelper.YZYTicket && sheet.UserLevelName != "阳朔分销商")
            {
                /*
                if (DateTime.Compare(sheet.OrderTime.Value, ETicket.Utility.SiteConfigHelper.YZY_EndTime) < 0)
                {
                    if (sheet.Out_Status != null)
                    {
                        PubFun.ShowMsg(this, "银子岩门票退款不需要人工审核，系统自动提交到智游宝系统申请退款，请等待");
                        return;
                    }
                }
                */

                if (DateTime.Compare(sheet.OrderTime.Value, ETicket.Utility.SiteConfigHelper.YZYTime) > 0)
                {
                    if (sheet.Out_Status != null)
                    {
                        PubFun.ShowMsg(this, "银子岩门票退款不需要人工审核，系统自动提交到智游宝系统申请退款，请等待");
                        return;
                    }
                }

            }
            EFEntity.OrderRefund refund = new EFEntity.OrderRefund();
            if (this.ddlResult.SelectedValue == "审核不通过")
            {
                refund.PResult = ETicket.Utility.OrderStatusEnum.退款审核不通过.ToString();
            }
            else if (this.ddlResult.SelectedValue == "审核通过")
            {
                refund.PResult = ETicket.Utility.OrderStatusEnum.退款审核通过.ToString();
            }
            else
            {
                PubFun.ShowMsg(this, "审核失败，无法找到定义的审核状态");
                return;
            }
            if (txtReason.Text.Trim() != "")
                refund.PMark = txtReason.Text.Trim();
            refund.PIP = PubFun.GetClientIP();
            refund.PUserID = BLL.UserBLL.Instance.GetLoginModel().UserID;
            refund.RTime = DateTime.Now;

            string msg = BLL.OrderSheetBLL.Instance.RefundCheck(orderid, refund);
            if (msg == "")
            {
                Bind();
                PubFun.ShowMsgRedirect(this, "操作成功", "/business/order/order_detail_chk_view.aspx?orderid=" + orderid);
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

            if (!BLL.OrderSheetBLL.Instance.IsDoRefundCheck(sheet))
            {
                Response.Redirect("order_detail_chk_view.aspx?orderid=" + orderid);
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

            var refund = BLL.OrderRefundBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (refund != null)
            {
                litRTime.Text = refund.RTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                litRMark.Text = refund.RMark;
            }


        }
    }
}