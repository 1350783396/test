using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class line_stock : System.Web.UI.Page
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

                ddlStartTime.Items.Clear();
                #region 绑定发车时间
                var timeList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == productID);
                if (timeList==null||timeList.Count<EFEntity.ProductSaleTime>() == 0)
                {
                    ddlStartTime.Items.Add(new ListItem("请先添加发车时间"));
                }
                else
                {
                    ddlStartTime.Items.Add(new ListItem("请选择发车时间"));
                    foreach(var time in timeList)
                    {
                        string text = time.StartH + "时" + time.StartM + "分";
                        string value= time.StartH + ":" + time.StartM ;
                        ddlStartTime.Items.Add(new ListItem(text,value));
                    }
                }
                #endregion
                //加载数据
                LoadData(productID);
            }
        }

        void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                int pkid = int.Parse(e.CommandArgument.ToString());
                EFEntity.ProductStock stock = BLL.ProductStockBLL.Instance.GetEntity(p => p.PKID == pkid);

                lblPKID.Text = stock.PKID.ToString();
                txtSaleDate.Value = stock.SaleDate.Value.ToString("yyyy-MM-dd");
                ddlStartTime.SelectedValue = stock.StartH + ":" + stock.StartM; ;
                txtStock.Value = stock.Stock.ToString();

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
                var stock = e.Item.DataItem as EFEntity.ProductStock;
                LinkButton lbtnEdit = e.Item.FindControl("lbtnEdit") as LinkButton;
                lbtnEdit.CommandArgument = stock.PKID.ToString();
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

            EFEntity.ProductStock stock = null;

            string saleDate = txtSaleDate.Value.Trim();
            string startTime = ddlStartTime.SelectedValue.Trim();
            string stockStr = txtStock.Value.Trim();
            #region 验证
            if (saleDate == "")
            {
                PubFun.ShowMsg(this, "请选择销售日期");
                return;
            }
            if (startTime == "")
            {
                PubFun.ShowMsg(this, "请选择发车时间");
                return;
            }
            if (startTime == "请先添加发车时间")
            {
                PubFun.ShowMsg(this, "请在发车时间处，先录入发车时间");
                return;
            }
            if (startTime == "请选择发车时间")
            {
                PubFun.ShowMsg(this, "请选择发车时间");
                return;
            }
            DateTime startDTime = new DateTime();
            try
            {
                startDTime = Convert.ToDateTime(saleDate + " " + startTime);
            }
            catch
            {
                PubFun.ShowMsg(this, "销售日期格式错误");
                return;
            }
            if(stockStr=="")
            {
                PubFun.ShowMsg(this, "请填写库存数量");
                return;
            }
            #endregion

            bool isAdd = true;
            if (PKID > 0)
            {
                isAdd = false;
                stock = BLL.ProductStockBLL.Instance.GetEntity(p => p.PKID == PKID);
            }
            else
            {
                stock = new EFEntity.ProductStock();
            }

            stock.ProductID = productID;

            stock.SaleDate = Convert.ToDateTime(startDTime.ToString("yyyy-MM-dd 00:00:00"));
            stock.StartH = startDTime.Hour;
            stock.StartM = startDTime.Minute;

            int stockValue = 0;
            int.TryParse(stockStr, out stockValue);
            stock.Stock = stockValue;
            if (isAdd)
            {
                BLL.ProductStockBLL.Instance.AddObject(stock);
                ClearInput();
            }
            else
            {
                BLL.ProductStockBLL.Instance.UpdateObject(stock);
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
                            EFEntity.ProductStock stock = new EFEntity.ProductStock();
                            stock.PKID = id;
                            BLL.ProductStockBLL.Instance.DeleteObject(stock);

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
            this.lblPKID.Text = "";
            txtSaleDate.Value="";
            ddlStartTime.SelectedValue = "请选择发车时间";
            txtStock.Value="";


            this.btnSave.Text = "新增";
            btnCancel.Visible = false;
        }

        void LoadData(int productID)
        {
            var stockList = BLL.ProductStockBLL.Instance.GetEntities(p => p.ProductID == productID);
            this.repList.DataSource = stockList;
            this.repList.DataBind();

            this.litCount.Text = string.Format("共{0}条记录", stockList.Count<EFEntity.ProductStock>());
        }

        void InitNav(int productID)
        {
            hyProduct.NavigateUrl = "line_add.aspx?productid=" + productID;
            hyPrice.NavigateUrl = "line_price.aspx?productid=" + productID;
            hyTickTime.NavigateUrl = "line_ticktime.aspx?productid=" + productID;
            //hyStock.NavigateUrl = "line_stock.aspx?productid=" + productID;
            hyAddress.NavigateUrl = "line_address.aspx?productid=" + productID;
            hyPublish.NavigateUrl = "line_publish.aspx?productid=" + productID;

            divProduct.Attributes.Add("class", "tab01");
            divPrice.Attributes.Add("class", "tab01");
            divTickTime.Attributes.Add("class", "tab01");
            divStock.Attributes.Add("class", "tab02");
            divAddress.Attributes.Add("class", "tab01");
            divPublish.Attributes.Add("class", "tab01");
        }
    }
}