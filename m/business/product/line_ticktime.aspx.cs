using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class line_ticktime : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            this.btnCancel.Click += btnCancel_Click;
            this.btnDelete.Click += btnDelete_Click;
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.repList.ItemCommand += repList_ItemCommand;
            if(!Page.IsPostBack)
            {
                int productID = PubFun.QueryInt("productid");
                InitNav(productID);

                //加载数据
                LoadData(productID);
            }
        }

        void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           if(e.CommandName=="Edit")
           {
               
               int pkid = int.Parse(e.CommandArgument.ToString());
               EFEntity.ProductSaleTime tick = BLL.ProductSaleTimeBLL.Instance.GetEntity(p => p.PKID == pkid);

               lblPKID.Text = tick.PKID.ToString();
               txtStartTime.Value = tick.StartH+":"+tick.StartM;
               txtLastOrderTime.Value = tick.LastOrderH + ":" + tick.LastOrderM; ;

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
                var tick = e.Item.DataItem as EFEntity.ProductSaleTime;
                LinkButton lbtnEdit = e.Item.FindControl("lbtnEdit") as LinkButton;
                lbtnEdit.CommandArgument = tick.PKID.ToString();
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
           
           EFEntity.ProductSaleTime saleTime=null;

           string startTime=txtStartTime.Value.Trim();
           string lastOrderTime = txtLastOrderTime.Value.Trim();

           #region 验证
           if (startTime == "")
           {
               PubFun.ShowMsg(this,"请选择发车时间");
               return;
           }
           if (lastOrderTime == "")
           {
               PubFun.ShowMsg(this, "请选择最后下单时间");
               return;
           }
           DateTime startDTime = new DateTime();
           DateTime lastDTime = new DateTime();
           try
           {
               startDTime = Convert.ToDateTime("2012-12-12 " + startTime );
           }
           catch
           {
               PubFun.ShowMsg(this, "发车时间格式错误");
               return;
           }
           try
           {
               lastDTime = Convert.ToDateTime("2012-12-12 " + lastOrderTime);
           }
           catch
           {
               PubFun.ShowMsg(this, "最后下单时间格式错误");
               return;
           }
           if (lastDTime > startDTime)
            {
                PubFun.ShowMsg(this, "最后下单时间不能大于发车时间");
                return;
            }
           #endregion

           bool isAdd = true;
           if(PKID>0)
           {
               isAdd = false;
               saleTime = BLL.ProductSaleTimeBLL.Instance.GetEntity(p => p.PKID == PKID);
           }
           else
           {
               saleTime = new EFEntity.ProductSaleTime();
           }

           saleTime.ProductID = productID;

           saleTime.LastOrderH = lastDTime.Hour;
           saleTime.LastOrderM = lastDTime.Minute;
           saleTime.StartH = startDTime.Hour;
           saleTime.StartM = startDTime.Minute;
         
           if (isAdd)
           {
               BLL.ProductSaleTimeBLL.Instance.AddObject(saleTime);
               ClearInput();
           }
           else
           {
               BLL.ProductSaleTimeBLL.Instance.UpdateObject(saleTime);
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
                            EFEntity.ProductSaleTime tick = new EFEntity.ProductSaleTime();
                            tick.PKID = id;
                            BLL.ProductSaleTimeBLL.Instance.DeleteObject(tick);
                            
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
            txtStartTime.Value = "";
            txtLastOrderTime.Value="";
        

            this.btnSave.Text = "新增";
            btnCancel.Visible = false;
        }

        void LoadData(int productID)
        {
            IEnumerable<EFEntity.ProductSaleTime> tickList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == productID);
            this.repList.DataSource = tickList;
            this.repList.DataBind();

            this.litCount.Text = string.Format("共{0}条记录", tickList.Count<EFEntity.ProductSaleTime>());
        }

        void InitNav(int productID)
        {
            hyProduct.NavigateUrl = "line_add.aspx?productid=" + productID;
            hyPrice.NavigateUrl = "line_price.aspx?productid=" + productID;
            //hyTickTime.NavigateUrl = "line_ticktime.aspx?productid=" + productID;
            hyStock.NavigateUrl = "line_stock.aspx?productid=" + productID;
            hyAddress.NavigateUrl = "line_address.aspx?productid=" + productID;
            hyPublish.NavigateUrl = "line_publish.aspx?productid=" + productID;

            divProduct.Attributes.Add("class", "tab01");
            divPrice.Attributes.Add("class", "tab01");
            divTickTime.Attributes.Add("class", "tab02");
            divStock.Attributes.Add("class", "tab01");
            divAddress.Attributes.Add("class", "tab01");
            divPublish.Attributes.Add("class", "tab01");

        }
        
    }
}