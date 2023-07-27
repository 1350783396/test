using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class Properties1_list : AdminBase
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
                checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAll('{0}','chkDelete',this);", this.form1.ClientID));
            }
        }
        void LoadData()
        {
            IEnumerable<EFEntity.Properties1> regionList = BLL.Properties1BLL.Instance.GetEntities();
            this.repList.DataSource = regionList;
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

                        EFDAO.TransactionBaseDAO trans = new EFDAO.TransactionBaseDAO();
                        try
                        {
                            trans.BeginTransaction();
                            IEnumerable<EFEntity.Properties1> sukList = BLL.Properties1BLL.Instance.GetEntities(p => p.ID == id);
                            //EFEntity.UserLevel userType = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelID == id);

                            BLL.Properties1BLL.Instance.DeleteObjects(sukList);
                            //BLL.UserLevelBLL.Instance.DeleteObject(userType);
                            trans.Commit();
                            delete = true;
                        }
                        catch
                        {
                            trans.RollBack();
                        }
                        finally
                        {
                            trans.Close();
                        }
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