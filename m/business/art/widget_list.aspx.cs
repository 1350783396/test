using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using NJiaSu.Libraries;

namespace ETicket.Web.business.art
{
    public partial class widget_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.AspNetPager1.PageChanged += AspNetPager1_PageChanged;
            this.btnQuery.Click += btnQuery_Click;
            this.btnRefrech.Click += btnRefrech_Click;
            this.btnDel.Click += btnDel_Click;
            this.btnDel2.Click += btnDel_Click;
            if (!Page.IsPostBack)
            {
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
                        EFEntity.PageWidget model = new EFEntity.PageWidget();
                        model.PKID = id;
                        bool msg = BLL.PageWidgetBLL.Instance.DeleteObject(model);
                        if (msg)
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

        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                CheckBox checkALL = e.Item.FindControl("chkAll") as CheckBox;
                checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAllEnable('{0}','chkItem',this);", this.form1.ClientID));
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var art = e.Item.DataItem as EFEntity.PageWidget;

                HyperLink hyEdit = e.Item.FindControl("hyEdit") as HyperLink;
                string detailUrl = string.Format("/business/art/widget_edit.aspx?artid={0}", art.PKID);
                hyEdit.NavigateUrl = "#";
                hyEdit.Attributes.Add("onclick", PubFun.TabNav("widgetedit_" + art.PKID, art.Title.Replace("'","").Replace("\"",""), detailUrl));
            }
        }

        string Where()
        {

            string mID = PubFun.QueryString("MID");
            StringBuilder sb = new StringBuilder();
            //模块
            sb.AppendFormat(" it.Category='{0}'",mID);

            if (txtStartTime.Text.Trim() != "")
            {
                sb.Append(" and it.AddTime>=DATETIME'" + txtStartTime.Text.Trim() + " 00:00:00'");
            }
            if (txtEndTime.Text.Trim() != "")
            {
                sb.Append(" and it.AddTime<=DATETIME'" + txtEndTime.Text.Trim() + " 23:59:59'");
            }
            //产品名称
            if (this.txtTitel.Text.Trim() != "")
            {
                sb.AppendFormat("and it.Title like '%{0}%'", this.txtTitel.Text.Trim());
            }
            return sb.ToString();
        }
        void LoadData(int currentPage, int pageSize)
        {

            ETicket.Utility.PageInfo<EFEntity.PageWidget> pi = null;
            pi = BLL.PageWidgetBLL.Instance.GetPageList(currentPage, pageSize, Where(), "it.AddTime ASC");

            AspNetPager1.RecordCount = pi.RecordCount;
            this.lblCount.Text = string.Format("共{0}条记录，共{1}页/当前第{2}页", pi.RecordCount, AspNetPager1.PageCount, currentPage);
            this.repList.DataSource = pi.List;
            this.repList.DataBind();
        }
    }
}