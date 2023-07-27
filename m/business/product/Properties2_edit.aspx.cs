using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class Properties2_edit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int typeID = PubFun.QueryInt("id");
                int PID = PubFun.QueryInt("PID");
                IEnumerable<EFEntity.Properties1> propertiesList = BLL.Properties1BLL.Instance.GetEntities();
                ddlProperties.Items.Clear();
                foreach (var properties in propertiesList)
                {
                    ddlProperties.Items.Add(new ListItem(properties.Name, properties.ID.ToString()));
                }
                if (typeID > 0)
                {
                    EFEntity.Properties2 properties2 = BLL.Properties2BLL.Instance.GetEntity(p => p.ID == typeID);
                    if (properties2 != null)
                    {
                        txtName.Text = properties2.Name;
                        txtSort.Text = properties2.Sort.ToString();
                        txtMemo.Value = properties2.Memo;
                        ProductID.Value = properties2.ProductID.ToString();
                        ddlProperties.SelectedValue = properties2.Pid.ToString();
                    }
                }
                else
                {
                    ProductID.Value = PID.ToString();
                }
                EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == PID);
                if (product == null)
                    return;
                txtProductName.Text =product.ProductName;
            }
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int typeID = PubFun.QueryInt("id");
            EFDAO.TransactionBaseDAO trans = new EFDAO.TransactionBaseDAO();
            if (typeID > 0)
            {
                //更新
                EFEntity.Properties2 properties2 = BLL.Properties2BLL.Instance.GetEntity(p => p.ID == typeID);
                properties2.Name = this.txtName.Text.Trim();
                properties2.Pid = int.Parse(ddlProperties.SelectedValue.ToString());
                properties2.ProductID = int.Parse(ProductID.Value.ToString());
                properties2.Sort = int.Parse(this.txtSort.Text.Trim());
                properties2.Memo = this.txtMemo.Value.Trim();
                BLL.Properties2BLL.Instance.UpdateObject(properties2);

            }
            else
            {
                EFEntity.Properties2 properties2 = new EFEntity.Properties2();
                properties2.Name = this.txtName.Text.Trim();
                properties2.Pid = int.Parse(ddlProperties.SelectedValue.ToString());
                properties2.ProductID = int.Parse(ProductID.Value.ToString());
                properties2.Sort = int.Parse(this.txtSort.Text.Trim());
                properties2.Memo = this.txtMemo.Value.Trim();
                BLL.Properties2BLL.Instance.AddObject(properties2);
            }
            Response.Redirect("Properties2_list.aspx?id="+ ProductID.Value.ToString());
        }
    }
}