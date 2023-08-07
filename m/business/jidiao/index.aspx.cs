using NJiaSu.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETicket.Web.business.jidiao
{
    public partial class index : jiDiaoBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.repList.ItemCommand += repList_ItemCommand;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.ddlProperties.SelectedIndexChanged += ddlProperties_SelectedIndexChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefrech.Click += btnRefrech_Click;
            //this.btnDel.Click += btnDel_Click;
            //this.btnDel2.Click += btnDel_Click;
            if (!Page.IsPostBack)
            {

                #region 绑定日期类型
                riQiLx.Items.Clear();
                riQiLx.Items.Add(new ListItem("游览日期", "llrq"));
                riQiLx.Items.Add(new ListItem("下单时间", "xdrq"));
                riQiLx.Items.Add(new ListItem("验票时间", "yprq"));
                #endregion
                #region 绑定产品类型
                sx_th.Items.Clear();
                sx_th.Items.Add(new ListItem("所有类型"));
                sx_th.Items.Add(new ListItem("专线", "line"));
                sx_th.Items.Add(new ListItem("景区", "ticket"));
                #endregion


                #region 绑定订单状态
                sx_ykyes.Items.Clear();
                sx_ykyes.Items.Add(new ListItem("所有状态"));
                sx_ykyes.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.待付款.ToString()));
                sx_ykyes.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已取消.ToString()));
                sx_ykyes.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已失效.ToString()));
                sx_ykyes.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已支付.ToString()));
                sx_ykyes.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已验票.ToString()));
                sx_ykyes.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.已过期.ToString()));
                sx_ykyes.Items.Add(new ListItem(ETicket.Utility.OrderStatusEnum.过期已退.ToString()));
                #endregion

                #region 绑定下单人类型
                ddlUserLevel.Items.Clear();
                ddlUserLevel.Items.Add(new ListItem("所有类型"));
                ddlUserLevel.Items.Add(new ListItem("所有分销商"));
                IEnumerable<EFEntity.UserLevel> levelList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "user" | p.UserCategory == "partner").OrderBy(p => p.OrderValue);
                foreach (var level in levelList)
                {
                    ddlUserLevel.Items.Add(new ListItem(level.UserLevelName));
                }
                #endregion
                #region 绑定分销商
                txtSelValue.Items.Clear();
                txtSelValue.Items.Add(new ListItem("全部"));
                var pi = BLL.UserBLL.Instance.GetEntities(u => u.UserCategory == "partner");
                foreach (var level in pi)
                {
                    txtSelValue.Items.Add(new ListItem(level.CPName, level.UserID.ToString()));
                }
                #endregion
                #region 绑定产品
                txtProductName.Items.Clear();
                txtProductName.Items.Add(new ListItem("全部"));
                var product = BLL.ProductBLL.Instance.GetEntities("", null);
                foreach (var level in product)
                {
                    txtProductName.Items.Add(new ListItem(level.ProductName));
                }
                #endregion

                #region 绑定属性
                IEnumerable<EFEntity.Properties1> propertiesList = BLL.Properties1BLL.Instance.GetEntities();
                ddlProperties.Items.Clear();
                ddlProperties.Items.Add(new ListItem("不限", "0"));
                foreach (var properties in propertiesList)
                {
                    ddlProperties.Items.Add(new ListItem(properties.Name, properties.ID.ToString()));
                }
                #endregion
                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
            }
        }

        void btnDel_Click(object sender, EventArgs e)
        {
            int deleteSucc = 0;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                CheckBox chkItem = this.repList.Items[i].FindControl("chkItem") as CheckBox;
                if (chkItem != null && chkItem.Checked == true)
                {
                    Label lblPKID = this.repList.Items[i].FindControl("lblPKID") as Label;
                    if (lblPKID != null)
                    {
                        int id = int.Parse(lblPKID.Text);
                        string msg = BLL.OrderSheetBLL.Instance.DeleteSheet(id);
                        if (msg == "")
                        {
                            deleteSucc++;
                        }
                    }
                }
            }

            if (deleteSucc > 0)
            {
                //AspNetPager1.CurrentPageIndex = 1;
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
        void ddlProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProperties.SelectedValue.ToString() != "0")
            {
                var val = Convert.ToInt32(ddlProperties.SelectedValue);
                IEnumerable<EFEntity.Properties2> regionList = BLL.Properties2BLL.Instance.GetEntities(p => p.Pid == val);
                txtProperties.Items.Clear();
                foreach (var properties in regionList)
                {
                    txtProperties.Items.Add(new ListItem(properties.Name, properties.ID.ToString()));
                }
            }
            else
            {
                txtProperties.Items.Clear();
            }
        }
        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                CheckBox checkALL = e.Item.FindControl("chkAll") as CheckBox;
                checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAllEnable('{0}','chkItem',this);", this.form1.ClientID));
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var sheet = e.Item.DataItem as EFEntity.OrderSheet;
                LinkButton lbtnReset = e.Item.FindControl("lbtnDuanXin") as LinkButton;
                lbtnReset.CommandArgument = sheet.OrderID.ToString();
                lbtnReset.CommandName = "reset";
            }
        }
        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
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
            else
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                EFEntity.OrderSheet sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
                sheet.ValidTime = DateTime.Now;
                sheet.OrderStatus = ETicket.Utility.OrderStatusEnum.已验票.ToString();
                BLL.OrderSheetBLL.Instance.UpdateObject(sheet);
                //btnQuery_Click(source, e);
                LoadData(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize);
                ////验票成功后续
                //BLL.ValidNotifyEventBLL.Instance.ValidNotify(sheet);
            }
        }

        string Where()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();

            StringBuilder sb = new StringBuilder();
            //自己的订单
            //sb.AppendFormat(" it.UserID={0}", cookiesUser.UserID);
            //sb.Append("1=1 ");
            //状态筛选
            if (sx_ykyes.SelectedValue == "所有状态")
            {
                sb.Append(" it.OrderStatus!='退款审核中' and it.OrderStatus!='退款审核通过' and it.OrderStatus!='已退款' and it.OrderStatus!='退款审核不通过'");
            }
            else
            {
                sb.AppendFormat(" it.OrderStatus='{0}'", sx_ykyes.SelectedValue);
            }
            //分销商
            if (txtSelValue.SelectedValue.Trim() != "全部")
            {
                sb.AppendFormat(" and it.UserID={0}", txtSelValue.SelectedValue.Trim());
            }
            //产品分类
            if (sx_th.SelectedValue != "所有类型")
            {
                sb.AppendFormat("and it.CategoryID='{0}'", sx_th.SelectedValue);
            }
            //下单人类型
            if (ddlUserLevel.SelectedValue == "所有类型")
            {

            }
            else if (ddlUserLevel.SelectedValue == "所有分销商")
            {
                sb.AppendFormat("and it.UserCategory='{0}'", "partner");
            }
            else
            {
                sb.AppendFormat("and it.UserLevelName='{0}'", this.ddlUserLevel.SelectedValue);
            }
            //产品名称
            if (this.txtProductName.SelectedValue.Trim() != "全部")
            {
                sb.AppendFormat("and it.ProductName='{0}'", this.txtProductName.SelectedValue.Trim());
            }
            //关键字
            if (this.sx_tckey.Value.Trim() != "")
            {
                sb.AppendFormat("and (it.ProductName like '%{0}%'", this.sx_tckey.Value.Trim());
                sb.AppendFormat("or it.RealName like '%{0}%'", this.sx_tckey.Value.Trim());
                sb.AppendFormat("or it.Phone like '%{0}%'", this.sx_tckey.Value.Trim());
                sb.AppendFormat("or it.UserName like '%{0}%'", this.sx_tckey.Value.Trim());
                sb.AppendFormat("or it.SheetID like '%{0}%')", this.sx_tckey.Value.Trim());
            }
            //产品属性
            if (ddlProperties.SelectedValue.ToString() != "0" && txtProperties.SelectedValue.ToString() != "不限")
            {
                sb.AppendFormat("and it.Properties='{0}'", txtProperties.SelectedValue.ToString());
            }
            //日期
            if (sx_date1.Value.Trim() != "")
            {
                if (riQiLx.SelectedValue == "llrq")
                    sb.AppendFormat(" and it.PalyDate>=DATETIME'{0} 00:00:00'", sx_date1.Value.Trim());
                else if (riQiLx.SelectedValue == "xdrq")
                    sb.AppendFormat(" and it.OrderTime>=DATETIME'{0} 00:00:00'", sx_date1.Value.Trim());
                else if (riQiLx.SelectedValue == "yprq")
                    sb.AppendFormat(" and it.ValidTime>=DATETIME'{0} 00:00:00'", sx_date1.Value.Trim());
            }
            //日期
            if (sx_date2.Value.Trim() != "")
            {
                if (riQiLx.SelectedValue == "llrq")
                    sb.AppendFormat(" and it.PalyDate<=DATETIME'{0} 23:59:59'", sx_date2.Value.Trim());
                else if (riQiLx.SelectedValue == "xdrq")
                    sb.AppendFormat(" and it.OrderTime<=DATETIME'{0} 23:59:59'", sx_date2.Value.Trim());
                else if (riQiLx.SelectedValue == "yprq")
                    sb.AppendFormat(" and it.ValidTime<=DATETIME'{0} 23:59:59'", sx_date2.Value.Trim());
            }
            //发车日期
            if (txtStartTime1.Value.Trim() != "")
            {
                sb.AppendFormat(" and it.StartTime>=DATETIME'{0} 00:00:00'", txtStartTime1.Value.Trim());
            }

            if (txtStartTime2.Value.Trim() != "")
            {
                sb.AppendFormat(" and it.StartTime<=DATETIME'{0} 23:59:59'", txtStartTime2.Value.Trim());
            }
            //发车时间点
            if (txtStartHM.Value.Trim() != "")
            {
                try
                {
                    DateTime t = Convert.ToDateTime("2014-06-22 " + txtStartHM.Value.Trim());
                    int h = t.Hour;
                    int m = t.Minute;

                    sb.AppendFormat("and it.StartH={0}", h.ToString());
                    sb.AppendFormat("and it.StartM={0}", m.ToString());
                }
                catch
                {

                }
            }
            return sb.ToString();
        }
        void LoadData(int currentPage, int pageSize)
        {

            ETicket.Utility.PageInfo<EFEntity.OrderSheet> pi = null;
            pi = BLL.OrderSheetBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.OrderTime DESC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
            //order.PayState = ETicket.Utility.PayStateEnum.未支付.ToString();
            //sheet.OrderStatus = ETicket.Utility.OrderStatusEnum.已验票.ToString();
            //单（ 项目1+项目2+项目3+项目4+项目5 =172人）
            //ProductName
            this.zongdanliang.Text = IntTextColor(pi.RecordCount);
            this.zongrenshu.Text = IntTextColor(pi.List.Sum(u => u.NUM).GetValueOrDefault(0));
            var yiyanpiao = pi.List.Where(u => u.OrderStatus == "已验票");
            var weiyanpiao = pi.List.Where(u => u.OrderStatus == "已支付");
            var quanpiao = pi.List.Where(u => u.OrderStatus == "已验票" || u.OrderStatus == "已支付");
            var yiyanpiaoXx = IntTextColor(0) + "单";
            var weiyanpiaoXx = IntTextColor(0) + "单";
            var quanpiaoXx = IntTextColor(0) + "单";
            if (yiyanpiao.Count() > 0)
            {
                yiyanpiaoXx = IntTextColor(yiyanpiao.Count()) + "单(";
                foreach (var item in yiyanpiao.GroupBy(u => u.ProductName))
                    yiyanpiaoXx += item.Key + ":" + IntTextColor(item.Sum(u => u.NUM).GetValueOrDefault(0)) + "+";
                yiyanpiaoXx = yiyanpiaoXx.TrimEnd('+') + "=" + IntTextColor(yiyanpiao.Sum(u => u.NUM).GetValueOrDefault(0)) + "人)";
            }
            if (weiyanpiao.Count() > 0)
            {
                weiyanpiaoXx = IntTextColor(weiyanpiao.Count()) + "单(";
                foreach (var item in weiyanpiao.GroupBy(u => u.ProductName))
                    weiyanpiaoXx += item.Key + ":" + IntTextColor(item.Sum(u => u.NUM).GetValueOrDefault(0)) + "+";
                weiyanpiaoXx = weiyanpiaoXx.TrimEnd('+') + "=" + IntTextColor(weiyanpiao.Sum(u => u.NUM).GetValueOrDefault(0)) + "人)";
            }
            if (quanpiao.Count() > 0)
            {
                quanpiaoXx = IntTextColor(quanpiao.Count()) + "单(";
                foreach (var item in quanpiao.GroupBy(u => u.ProductName))
                    quanpiaoXx += item.Key + ":" + IntTextColor(item.Sum(u => u.NUM).GetValueOrDefault(0)) + "+";
                quanpiaoXx = quanpiaoXx.TrimEnd('+') + "=" + IntTextColor(quanpiao.Sum(u => u.NUM).GetValueOrDefault(0)) + "人)";
            }
            this.yiyanpiao.Text = yiyanpiaoXx;
            this.weiyanpiao.Text = weiyanpiaoXx;
            this.quanpiao.Text = quanpiaoXx;

        }
        public string IntTextColor(int srt)
        {
            return "<span style='color:red;'>" + srt + "</span>";
        }
        public string GetProperties(string ids)
        {
            string ret = "";
            string[] contains = ids.Split(',');
            IEnumerable<EFEntity.vProperties2> propertiesList = BLL.vProperties2BLL.Instance.GetEntities();
            List<EFEntity.vProperties2> lstCom = propertiesList.ToList<EFEntity.vProperties2>().FindAll(new Predicate<EFEntity.vProperties2>(r => contains.Contains(r.ID.ToString()))).ToList();
            foreach (var properties in lstCom)
            {
                ret += properties.PName + ":" + properties.Name.ToString() + "<br>";
            }
            return ret;
        }
    }
}