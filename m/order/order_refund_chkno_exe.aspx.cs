using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;


namespace ETicket.Web.business.order
{
    public partial class order_refund_chkno_exe : System.Web.UI.Page
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
            if (this.rbtZhiFu.Checked == false&&this.rbtTKDaiShen.Checked==false)
            {
                PubFun.ShowMsg(this, "请选择处理结果");
                return;
            }
            if(txtChkCode.Text.Trim()=="")
            {
                PubFun.ShowMsg(this, "请输入授权码");
                return;
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

            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderid);

            /*
            if (sheet.ProductID == ETicket.Utility.SiteConfigHelper.YZYTicket)
            {
                if (DateTime.Compare(sheet.OrderTime.Value, ETicket.Utility.SiteConfigHelper.YZYTime) > 0)
                {
                    if (sheet.Out_Status != null)
                    {
                        PubFun.ShowMsg(this, "银子岩门票退款不需要人工审核，系统自动提交到智游宝系统申请退款，请等待");
                        return;
                    }
                }
            }
            */

            if(this.rbtZhiFu.Checked )
            {
                sheet.OrderStatus = ETicket.Utility.OrderStatusEnum.已支付.ToString();
                sheet.TicketFailTime = DateTime.Now.AddDays(10);
                BLL.OrderSheetBLL.Instance.UpdateObject(sheet);
            }
            else if(this.rbtTKDaiShen.Checked)
            {
                sheet.OrderStatus = ETicket.Utility.OrderStatusEnum.退款审核中.ToString();
                BLL.OrderSheetBLL.Instance.UpdateObject(sheet);
            }
            else
            {
                PubFun.ShowMsg(this, "请选择处理结果");
                return;
            }

            var refund = BLL.OrderRefundBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (refund != null)
            {
                refund.PResult ="";
                if (this.rbtZhiFu.Checked)
                {
                    refund.PMark = refund.PMark + "|财务恢复为可验票状态";
                }
                else if (this.rbtTKDaiShen.Checked)
                {
                    refund.PMark = refund.PMark + "|财务恢复为可退款状态";
                }
            }
            BLL.OrderRefundBLL.Instance.UpdateObject(refund);

            PubFun.ShowMsgRedirect(this, "操作成功", "/business/order/order_detail.aspx?orderid=" + orderid);
        }

        void Bind()
        {
            int orderid = PubFun.QueryInt("orderid");
            EFEntity.OrderSheet sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (sheet == null)
                return;

            if (sheet.OrderStatus != ETicket.Utility.OrderStatusEnum.退款审核不通过.ToString())
            {
                Response.Redirect("order_detail.aspx?orderid=" + orderid);
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

        }
    }
}