using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class Properties1_edit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int typeID = PubFun.QueryInt("id");
                EFEntity.Properties1 properties1 = BLL.Properties1BLL.Instance.GetEntity(p => p.ID == typeID);
                if (properties1 != null)
                {
                    txtName.Text = properties1.Name;
                    txtSort.Text = properties1.Sort.ToString();
                    txtMemo.Value = properties1.Memo;
                }
            }
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int typeID = PubFun.QueryInt("id");
            EFDAO.TransactionBaseDAO trans = new EFDAO.TransactionBaseDAO();
            if (typeID > 0)
            {
                //更新
                EFEntity.Properties1 properties1 = BLL.Properties1BLL.Instance.GetEntity(p => p.ID == typeID);
                properties1.Name = this.txtName.Text.Trim();
                properties1.Sort = int.Parse(this.txtSort.Text.Trim());
                properties1.Memo = this.txtMemo.Value.Trim();
                BLL.Properties1BLL.Instance.UpdateObject(properties1);

            }
            else
            {
                EFEntity.Properties1 properties1 = new EFEntity.Properties1();
                properties1.Name = this.txtName.Text.Trim();
                properties1.Sort = int.Parse(this.txtSort.Text.Trim());
                properties1.Memo = this.txtMemo.Value.Trim();
                BLL.Properties1BLL.Instance.AddObject(properties1);
            }
            Response.Redirect("Properties1_list.aspx");
        }
    }
}