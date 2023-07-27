using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using NJiaSu.Libraries;

namespace ETicket.Web.business.order
{
    public partial class order_sms_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.repList.ItemCommand += repList_ItemCommand;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefrech.Click += btnRefrech_Click;
            if (!Page.IsPostBack)
            {
                #region 短信状态
                ddlSendStatus.Items.Clear();
                ddlSendStatus.Items.Add(new ListItem("所有状态"));
                ddlSendStatus.Items.Add(new ListItem(ETicket.Utility.SMSSendStatusEnum.发送失败.ToString()));
                ddlSendStatus.Items.Add(new ListItem(ETicket.Utility.SMSSendStatusEnum.发送中.ToString()));
                ddlSendStatus.Items.Add(new ListItem(ETicket.Utility.SMSSendStatusEnum.发送成功.ToString()));

                #endregion

                #region 验证方式
                ddlValid.Items.Clear();
                ddlValid.Items.Add(new ListItem("所有类型"));
                ddlValid.Items.Add(new ListItem(ETicket.Utility.SMSCategoryEnum.二维码.ToString()));
                ddlValid.Items.Add(new ListItem(ETicket.Utility.SMSCategoryEnum.身份证.ToString()));

                #endregion

                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }

        void btnRefrech_Click(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void btnQuery_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
        }

        void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "reset")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                var sms = BLL.SMSSendOrderBLL.Instance.GetEntity(p => p.OrderID == orderID);
                if (sms != null)
                {
                    sms.SendStatus = ETicket.Utility.SMSSendStatusEnum.发送中.ToString();
                    sms.SendNum = sms.SendNum + 1;
                    sms.SendIsRead = false;
                    BLL.SMSSendOrderBLL.Instance.UpdateObject(sms);
                }
                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }
        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var sms = e.Item.DataItem as EFEntity.SMS_Send_Order;

                LinkButton lbtnReset = e.Item.FindControl("lbtnReset") as LinkButton;
                Literal litReset = e.Item.FindControl("litReset") as Literal;
                if (sms.SendStatus == ETicket.Utility.SMSSendStatusEnum.发送中.ToString())
                {
                    lbtnReset.Visible = false;
                    litReset.Visible = true;
                    litReset.Text = "无";
                }
                else
                {
                    lbtnReset.Visible = true;
                    litReset.Visible = false;
                    litReset.Text = "";

                    lbtnReset.CommandArgument = sms.OrderID.ToString();
                    lbtnReset.CommandName = "reset";

                }
            }
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            //sb.AppendFormat(" it.UserID={0}", cookiesUser.UserID);
            //状态筛选
            if (ddlSendStatus.SelectedValue == "所有状态")
            {
                sb.Append(" it.SendStatus!=''");
            }
            else
            {
                sb.AppendFormat(" it.SendStatus='{0}'", ddlSendStatus.SelectedValue);
            }
            if (ddlValid.SelectedValue != "所有类型")
            {
                sb.AppendFormat(" and it.SMSCategory='{0}'",ddlValid.SelectedValue);
            }
            //姓名
            if (this.txtRealname.Text.Trim() != "")
            {
                sb.AppendFormat("and it.OrderRealName='{0}'", this.txtRealname.Text.Trim());
            }
            //手机
            if (this.txtPhone.Text.Trim() != "")
            {
                sb.AppendFormat("and it.Phone='{0}'", this.txtPhone.Text.Trim());
            }
            //订单号
            if (this.txtSheetID.Text.Trim() != "")
            {
                sb.AppendFormat("and it.SheetID='{0}'", this.txtSheetID.Text.Trim());
            }
            //短信发送时间
            if (txtOrderTime1.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.AddTime>=DATETIME'{0} 00:00:00'", txtOrderTime1.Text.Trim());
            }
            if (txtOrderTime2.Text.Trim() != "")
            {
                sb.AppendFormat(" and it.AddTime<=DATETIME'{0} 23:59:59'", txtOrderTime2.Text.Trim());
            }
            return sb.ToString();
        }
        void LoadData(int currentPage, int pageSize)
        {

            ETicket.Utility.PageInfo<EFEntity.SMS_Send_Order> pi = null;
            pi = BLL.SMSSendOrderBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.AddTime DESC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
        }
    }
}