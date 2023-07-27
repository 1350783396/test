using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class region_edit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int typeID = PubFun.QueryInt("regionid");
                EFEntity.Region region = BLL.RegionBLL.Instance.GetEntity(p => p.RegionID == typeID);
                if (region != null)
                {
                    txtDicName.Text = region.RegionName;
                    
                }
            }
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int typeID = PubFun.QueryInt("regionid");
            if (typeID > 0)
            {
                //更新
                EFEntity.Region region = BLL.RegionBLL.Instance.GetEntity(p => p.RegionID == typeID);
                region.RegionName = this.txtDicName.Text.Trim();
                BLL.RegionBLL.Instance.UpdateObject(region);
            }
            else
            {
                EFEntity.Region region = new EFEntity.Region();
                region.RegionName = this.txtDicName.Text.Trim();
                BLL.RegionBLL.Instance.AddObject(region);
            }
            Response.Redirect("region_list.aspx");
        }
    }
}