using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETicket.Web.openapi
{
    public partial class product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.repLine.ItemDataBound += repLine_ItemDataBound;
            if(!Page.IsPostBack)
            {
                var listTicket = BLL.ProductBLL.Instance.GetEntities(p => p.CategoryID == "ticket" && p.SaleFlag == true);//.OrderByDescending(p => p.AddTime);
                this.repTicket.DataSource = listTicket;
                this.repTicket.DataBind();

                var listLine = BLL.ProductBLL.Instance.GetEntities(p => p.CategoryID == "line" && p.SaleFlag == true);
                this.repLine.DataSource = listLine;
                this.repLine.DataBind();
            }
        }

        void repLine_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var product = e.Item.DataItem as EFEntity.Product;
                int productID=product.ProductID;
                var timeList= BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == productID);
                string TimeBuild = "";
                foreach(var time in timeList)
                {
                    string m=time.StartM.ToString();
                    if(m=="0")
                        m="00";
                    if(TimeBuild=="")
                    {
                        TimeBuild = time.StartH + ":" + m;
                    }
                    else
                    {
                        TimeBuild = TimeBuild + "，" + time.StartH + ":" + m;
                    }
                }

                string addressBuild = "";
                var addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == productID);
                foreach (var address in addressList)
                {
                    if (addressBuild == "")
                    {
                        addressBuild = address.Address;
                    }
                    else
                    {
                        addressBuild = addressBuild + "，" + address.Address;
                    }
                }
                if(addressBuild=="")
                {
                    addressBuild="注：没有固定用户可自由填";
                }

                Literal litTime = e.Item.FindControl("litTime") as Literal;
                litTime.Text = TimeBuild;
                
                Literal litAddress = e.Item.FindControl("litAddress") as Literal;
                litAddress.Text = addressBuild;
            }
        }
    }
}