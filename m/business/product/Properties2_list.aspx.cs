using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class Properties2_list : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repList.ItemDataBound += repList_ItemDataBound;
            if (!Page.IsPostBack)
            {
                int PID = PubFun.QueryInt("id");
                Button1.PostBackUrl = "Properties2_edit.aspx?PID="+ PID.ToString();
                btnSave.PostBackUrl = "Properties2_edit.aspx?PID=" + PID.ToString();
                EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == PID);
                if (product == null)
                    return;
                litMsg.Text ="产品名称："+ product.ProductName;
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
            int typeID = PubFun.QueryInt("id");
            IEnumerable<EFEntity.Properties2> regionList = BLL.Properties2BLL.Instance.GetEntities(p => p.ProductID == typeID);
            this.repList.DataSource = regionList;
            this.repList.DataBind();
        }
        public string GetProperties1(string id)
        {
            string ret = "";
            int pid = GetInt(id);
            var properties1 = BLL.Properties1BLL.Instance.GetEntity(p => p.ID == pid);
            if (properties1 != null)
            { ret = properties1.Name.ToString(); }
            return ret;
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
                            IEnumerable<EFEntity.Properties2> sukList = BLL.Properties2BLL.Instance.GetEntities(p => p.ID == id);
                            //EFEntity.UserLevel userType = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelID == id);

                            BLL.Properties2BLL.Instance.DeleteObjects(sukList);
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
        public int GetInt(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return var1;
            }
            catch
            {
                return 0;
            }
        }
    }
}