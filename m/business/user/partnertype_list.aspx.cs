using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class partnertype_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                CheckBox checkALL = e.Item.FindControl("chkDeleteALL") as CheckBox;
                checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAllEnable('{0}','chkDelete',this);", this.form1.ClientID));
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var level=e.Item.DataItem as EFEntity.UserLevel;
                CheckBox chkDelete = e.Item.FindControl("chkDelete") as CheckBox;
                chkDelete.Enabled = false;

                if(BLL.UserLevelBLL.Instance.IsDelete(level.UserLevelID))
                {
                    chkDelete.Enabled = true;
                }
            }
        }
        void LoadData()
        {
            IEnumerable<EFEntity.UserLevel> userTypeList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "partner");
            this.repList.DataSource = userTypeList;
            this.repList.DataBind();
        }

        protected void btnDelete2_Click(object sender, EventArgs e)
        {
            bool delete = false;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                CheckBox chkItem = this.repList.Items[i].FindControl("chkDelete") as CheckBox;
                if (chkItem != null && chkItem.Checked == true)
                {
                    Label lblPKID = this.repList.Items[i].FindControl("lblPKID") as Label;
                    if (lblPKID != null)
                    {
                        int id = int.Parse(lblPKID.Text);
                        string msg= BLL.UserLevelBLL.Instance.Delete(id);
                        if (msg == "")
                            delete = true;
                    }
                }
            }

            if (delete)
            {
                LoadData();
            }
        }
    }
}