using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.valid
{
    public partial class valid_man : ValidBase
    {
        EFEntity.CookiesUser cookieUser = BLL.UserBLL.Instance.GetLoginModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnValid.Click += btnValid_Click;
            this.btnQuery.Click += btnQuery_Click;
            if (!Page.IsPostBack)
            {
                //Bind();
            }
        }

        void btnQuery_Click(object sender, EventArgs e)
        {
            string sheetID = txtSheetID.Text.Trim();
            if (sheetID == "")
            {
                PubFun.ShowMsg(this, "请输入订单号");
                return;
            }
            EFEntity.OrderSheet sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.SheetID == sheetID);
            if (sheet == null)
            {
                PubFun.ShowMsg(this, "查询不到相关记录");
                return;
            }

            #region 是否可验证
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

            bool validProuduct = false;
            var validList = BLL.ProductValidBLL.Instance.GetEntities(p => p.UserID == cookieUser.UserID);
            foreach (var validItem in validList)
            {
                if (validItem.ProductID == sheet.ProductID)
                    validProuduct = true;
            }
            if (!validProuduct)
            {
                PubFun.ShowMsg(this, string.Format("你没有权限验证【{0}】", sheet.ProductName));
                return;
            }

            Bind(sheet);
        }

        void btnValid_Click(object sender, EventArgs e)
        {
            string sheetID = txtSheetID.Text.Trim();
            if (sheetID == "")
            {
                PubFun.ShowMsg(this, "请输入订单号");
                return;
            }

            try
            {
                EFEntity.OrderSheet sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.SheetID == sheetID);
                sheet.ValidUserID = cookieUser.UserID;
                sheet.ValidTime = DateTime.Now;
                sheet.OrderStatus = ETicket.Utility.OrderStatusEnum.已验票.ToString();

                BLL.OrderSheetBLL.Instance.UpdateObject(sheet);

                //验票成功后续
                BLL.ValidNotifyEventBLL.Instance.ValidNotify(sheet);

                Bind(sheet);
                PubFun.ShowMsg(this, "操作成功");
            }
            catch (Exception ex)
            {
                PubFun.ShowMsg(this, "操作失败：" + ex.Message);
            }

        }

        void Bind(EFEntity.OrderSheet sheet)
        {
            if (sheet == null)
            {
                return;
            }

            #region 按钮显示逻辑
            if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.已支付.ToString())
            {
                this.btnValid.Visible = true;
            }
            else
            {
                this.btnValid.Visible = false;
            }
            #endregion

            if (sheet.CategoryID == "line")
            {
                trLine.Visible = true;
            }
            else
            {
                trLine.Visible = false;
            }

            litProductName.Text = sheet.ProductName;
            litOrderState.Text = sheet.OrderStatus;
            litSMSStatus.Text = BLL.SMSSendOrderBLL.Instance.GetSmsStatus(sheet.OrderID);
            //litUnitPrice.Text = sheet.UnitPrice.ToString();
            litBuyNum.Text = sheet.NUM.ToString();
            //litTotalPrice.Text = sheet.TotalPrice.ToString();
            litSheetID.Text = sheet.SheetID;
            litOrderTime.Text = sheet.OrderTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            litName.Text = sheet.RealName;
            litIDCard.Text = sheet.IDCard;
            litPhone.Text = sheet.Phone;

            litPayType.Text = sheet.PayType;
            litValidType.Text = sheet.ValidType;

            //litUserLevel.Text = sheet.UserLevelName;
            //litUserName.Text = sheet.UserName;

            if (sheet.StartTime != null)
                litStartTime.Text = sheet.StartTime.Value.ToString("yyyy-MM-dd HH:mm");
            if (sheet.StartAddress != null)
                litStartAddress.Text = sheet.StartAddress;



            resultPanel.Visible = true;

        }
    }
}