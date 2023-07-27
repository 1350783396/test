using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class one_update : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnQuery.Click += btnQuery_Click;
        }

        void btnQuery_Click(object sender, EventArgs e)
        {
            bool isCheeck = false;  
            #region 批量更新专线销售日期
            if (chkLineUpdateTick.Checked)
            {
                if (txtLineSaleDate.Text.Trim() == "")
                {
                    PubFun.ShowMsg(this, "请选择日期");
                    return;
                }
                try
                {
                    Convert.ToDateTime(txtLineSaleDate.Text.Trim());
                }
                catch
                {
                    PubFun.ShowMsg(this, "日期格式错误，请重新选择");
                    return;
                }
                isCheeck = true;
                string sql = string.Format("update ProductTickTime set SaleDate='{0}'", Convert.ToDateTime(txtLineSaleDate.Text.Trim()).ToString("yyyy-MM-dd HH:mm:ss"));
                BLL.DbHelperSQL.ExecuteSql(sql);
            }
            #endregion

            #region 批量更新专线库存
            if (chkLineStock.Checked)
            {
                if (txtLineStock.Text.Trim() == "")
                {
                    PubFun.ShowMsg(this, "请输入库存");
                    return;
                }
                try
                {
                    int.Parse(txtLineStock.Text.Trim());
                }
                catch
                {
                    PubFun.ShowMsg(this, "库存格式错误，请重新输入");
                    return;
                }
                isCheeck = true;
                string sql = string.Format("update ProductTickTime set Stock={0}",txtLineStock.Text.Trim());
                BLL.DbHelperSQL.ExecuteSql(sql);
            }
            #endregion

            #region 批量更新景区库存
            if (chkTicketStock.Checked)
            {
                if (txtTicketStock.Text.Trim() == "")
                {
                    PubFun.ShowMsg(this, "请输入库存");
                    return;
                }
                try
                {
                    int.Parse(txtTicketStock.Text.Trim());
                }
                catch
                {
                    PubFun.ShowMsg(this, "库存格式错误，请重新输入");
                    return;
                }
                isCheeck = true;
                string sql = string.Format("update Product set Stock={0} where CategoryID='ticket'", txtTicketStock.Text.Trim());
                BLL.DbHelperSQL.ExecuteSql(sql);
            }
            #endregion

            if(isCheeck)
            {
                PubFun.ShowMsgRedirect(this, "执行完毕", Request.RawUrl);
            }
            else
            {
                PubFun.ShowMsg(this, "没有勾选需要维护的项目");
            }
        }
    }
}