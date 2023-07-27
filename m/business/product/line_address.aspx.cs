using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class line_address : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            this.btnCancel.Click += btnCancel_Click;
            this.btnDelete.Click += btnDelete_Click;
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.repList.ItemCommand += repList_ItemCommand;

            if (!Page.IsPostBack)
            {
                int productID = PubFun.QueryInt("productid");
                InitNav(productID);

                LoadData(productID);
            }
        }

        void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int pkid = int.Parse(e.CommandArgument.ToString());
                EFEntity.ProductAddress address = BLL.ProductAddressBLL.Instance.GetEntity(p => p.PKID == pkid);

                lblPKID.Text = address.PKID.ToString();
                txtAddress.Value = address.Address;
                txtPrice.Value = address.Price.ToString();

                this.btnSave.Text = "更新";
                btnCancel.Visible = true;

            }
        }

        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                CheckBox checkALL = e.Item.FindControl("chkAll") as CheckBox;
                checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAll('{0}','chkItem',this);", this.form1.ClientID));
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var address = e.Item.DataItem as EFEntity.ProductAddress;
                LinkButton lbtnEdit = e.Item.FindControl("lbtnEdit") as LinkButton;
                lbtnEdit.CommandArgument = address.PKID.ToString();
                lbtnEdit.CommandName = "Edit";
            }
        }

        //保存
        void btnSave_Click(object sender, EventArgs e)
        {
            //产品ID
            int productID = PubFun.QueryInt("productid");
            //主键
            int PKID = 0;
            if (lblPKID.Text != "")
                PKID = int.Parse(lblPKID.Text);

            EFEntity.ProductAddress model = null;

            string Address = txtAddress.Value.Trim();
            //string Price = txtPrice.Value.Trim();

            #region 验证
            if (Address == "")
            {
                PubFun.ShowMsg(this, "请输入上车地点");
                return;
            }
            //if (Price == "")
            //{
            //    PubFun.ShowMsg(this, "请输入价格");
            //    return;
            //}
            #endregion

            bool isAdd = true;
            if (PKID > 0)
            {
                isAdd = false;
                model = BLL.ProductAddressBLL.Instance.GetEntity(p => p.PKID == PKID);
            }
            else
            {
                model = new EFEntity.ProductAddress();
            }

            model.ProductID = productID;
            model.Address = txtAddress.Value.Trim();
            //decimal priceValue = 0;
            //decimal.TryParse(Price, out priceValue);
            //model.Price = priceValue;

            if (isAdd)
            {
                BLL.ProductAddressBLL.Instance.AddObject(model);
                ClearInput();
            }
            else
            {
                BLL.ProductAddressBLL.Instance.UpdateObject(model);
                ClearInput();
            }


            LoadData(productID);
        }
        //取消编辑
        void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            int productID = PubFun.QueryInt("productid");
            bool delete = false;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                CheckBox chkItem = this.repList.Items[i].FindControl("chkItem") as CheckBox;
                if (chkItem != null && chkItem.Checked == true)
                {
                    Label lblPKID = this.repList.Items[i].FindControl("lblPKID") as Label;
                    if (lblPKID != null)
                    {
                        int id = int.Parse(lblPKID.Text);
                        try
                        {
                            EFEntity.ProductAddress model = new EFEntity.ProductAddress();
                            model.PKID = id;
                            BLL.ProductAddressBLL.Instance.DeleteObject(model);

                            delete = true;
                        }
                        catch
                        {

                        }
                    }
                }
            }

            if (delete)
            {
                LoadData(productID);
            }
        }

        //清除输入
        void ClearInput()
        {
            lblPKID.Text = "";
            txtAddress.Value = "";
            //txtPrice.Value = "";

            this.btnSave.Text = "新增";
            btnCancel.Visible = false;
        }

        void LoadData(int productID)
        {
            IEnumerable<EFEntity.ProductAddress> tickList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == productID);
            this.repList.DataSource = tickList;
            this.repList.DataBind();

            this.litCount.Text = string.Format("共{0}条记录", tickList.Count<EFEntity.ProductAddress>());
        }

        void InitNav(int productID)
        {
            
            hyProduct.NavigateUrl = "line_add.aspx?productid=" + productID;
            hyPrice.NavigateUrl = "line_price.aspx?productid=" + productID;
            hyTickTime.NavigateUrl = "line_ticktime.aspx?productid=" + productID;
            hyStock.NavigateUrl = "line_stock.aspx?productid=" + productID;
            //hyAddress.NavigateUrl = "line_address.aspx?productid=" + productID;
            hyPublish.NavigateUrl = "line_publish.aspx?productid=" + productID;

            divProduct.Attributes.Add("class", "tab01");
            divPrice.Attributes.Add("class", "tab01");
            divTickTime.Attributes.Add("class", "tab01");
            divStock.Attributes.Add("class", "tab01");
            divAddress.Attributes.Add("class", "tab02");
            divPublish.Attributes.Add("class", "tab01");

        }
    }
}