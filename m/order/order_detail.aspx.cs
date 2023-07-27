using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.order
{
    public partial class order_detail : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnPay.Click += btnPay_Click;
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

        void btnPay_Click(object sender, EventArgs e)
        {
            int orderid = PubFun.QueryInt("orderid");

            string msg = BLL.OrderSheetBLL.Instance.PayCash(orderid);
            if (msg == "")
            {
                Bind();
                PubFun.ShowMsgRedirect(this, "操作成功", this.Request.RawUrl);
            }
            else
            {
                PubFun.ShowMsg(this, "操作失败：" + msg);
            }
            //
        }

        void Bind()
        {
            int orderid = PubFun.QueryInt("orderid");
            EFEntity.OrderSheet sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderid);
            if (sheet == null)
                return;

            //按钮显示逻辑
            if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.待付款.ToString() && sheet.PayType == "现金支付")
            {
                this.btnPay.Visible = true;
            }
            else
            {
                this.btnPay.Visible = false;
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
                if (sheet.EnableValid==null||sheet.EnableValid <= 0)
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


            //显示过期退多少
            if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.过期已退.ToString())
            {
                litTotalPrice.Text += string.Format("<span style='color:red'>[{0}]</span>", "已退" + sheet.OverRefundPrice);
            }

            if (sheet.Out_Code1!=null&&sheet.Out_Code1 != "")
            {
                litSheetID.Text = litSheetID.Text + string.Format("<span style='color:red'>[{0}]</span>", "验票辅助码" + sheet.Out_Code1);
            }
        }
    }
}