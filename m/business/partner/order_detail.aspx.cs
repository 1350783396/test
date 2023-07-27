using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.partner
{
    public partial class order_detail : PartnerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnCalcel.Click += btnCalcel_Click;
            this.btnRefresh.Click += btnRefresh_Click;
            if(!Page.IsPostBack)
            {
                Bind();
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            Bind();
        }

        void btnCalcel_Click(object sender, EventArgs e)
        {
            int orderid = PubFun.QueryInt("orderid");

            string msg = BLL.OrderSheetBLL.Instance.Cancel(orderid);
            if (msg=="")
            {
                Bind();
                PubFun.ShowMsgRedirect(this, "取消订单成功",this.Request.RawUrl);
            }
            else
            {
                PubFun.ShowMsg(this, "取消订单失败：" + msg);
            }
            //
        }

        void Bind()
        {
            int orderid = PubFun.QueryInt("orderid");
            EFEntity.OrderSheet sheet= BLL.OrderSheetBLL.Instance.GetEntity(p=>p.OrderID==orderid);
            if (sheet == null)
                return;

            //按钮显示逻辑
            if (sheet.OrderStatus==ETicket.Utility.OrderStatusEnum.待付款.ToString())
            {
                this.btnCalcel.Visible = true;
            }
            else
            {
                this.btnCalcel.Visible = false;
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
            litIDCard.Text=sheet.IDCard;
            litPhone.Text = sheet.Phone;

            litPayType.Text = sheet.PayType;
            litValidType.Text = sheet.ValidType;

            if (sheet.StartTime!=null)
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


            if(sheet.OrderStatus==ETicket.Utility.OrderStatusEnum.过期已退.ToString())
            {
                litTotalPrice.Text += string.Format("<span style='color:red'>[{0}]</span>", "已退"+sheet.OverRefundPrice);
            }
        }
    }
}