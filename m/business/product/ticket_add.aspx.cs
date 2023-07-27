using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business
{
    public partial class ticket_add : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                //加载地区到下拉列表
                IEnumerable<EFEntity.Region> regionList = BLL.RegionBLL.Instance.GetEntities();
                ddlRegion.Items.Clear();
                ddlRegion.Items.Add(new ListItem("请选择","0"));
                foreach (var region in regionList)
                {
                    ddlRegion.Items.Add(new ListItem(region.RegionName, region.RegionID.ToString()));
                }

                int productID = PubFun.QueryInt("productid");
                InitNav(productID);

                //编辑模式
                if(productID>0)
                {
                    EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p=>p.ProductID==productID);
                    if (product == null)
                        return;

                    txtProductName.Value = product.ProductName;
                    ddlRegion.SelectedValue = product.RegionID.ToString();
                    txtSupplyName.Value = product.SupplyName;
                    txtPrimePrice.Value = product.PrimeCost.ToString();
                    txtEnableValid.Value = product.EnableValid.ToString();
                    chkEnableValidSwitch.Checked = product.EnableValidswitch.Value;
                    txtTel.Value = product.Tel;
                    txtAddress.Value = product.Address;
                    txtOpenTime.Value = product.OpenTime;

                    txtRulesNote.Text = product.RulesNote;
                    txtDetail.Text = product.Detail;
                    txtRulesNoteWap.Text = product.RulesNote_WAP;
                    txtDetailWap.Text = product.Detail_WAP;

                    txtMemo.Value = product.Memo;
                    txtPrintMemo.Value = product.PrintMemo;
                    if (string.IsNullOrEmpty(product.TitleImg))
                    {
                        this.titleImg.Visible = false;
                    }
                    else
                    {
                        this.titleImg.ImageUrl = product.TitleImg_S;
                    }


                }
                else
                {
                    this.titleImg.Visible = false;
                }
            }
        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            int productID = PubFun.QueryInt("productid");
            EFEntity.Product product = null;
            EFDAO.TransactionBaseDAO trans = new EFDAO.TransactionBaseDAO();

            bool isAdd = true;  
            if (productID <= 0)
            {
                product = new EFEntity.Product();
            }
            else
            {
                isAdd = false;
                product = trans.GetEntity<EFEntity.Product>(p => p.ProductID == productID);
                if (product == null)
                {
                    PubFun.ShowMsg(this, "更改数据失败，数据可能被另外的用户已删除" );
                    return;
                }
            }

            string ProductName = txtProductName.Value.Trim();
            int selRegion = int.Parse(ddlRegion.SelectedValue);
            string SupplyName = txtSupplyName.Value.Trim();
            decimal PrimePrice = 0;
            if (txtPrimePrice.Value.Trim() != "")
                PrimePrice = decimal.Parse(txtPrimePrice.Value.Trim());
            //int Stock = 0;
            //if (txtStock.Value.Trim() != "")
            //    Stock = int.Parse(txtStock.Value.Trim());

            int EnableValid = 0;
            if (txtEnableValid.Value.Trim() != "")
                int.TryParse(txtEnableValid.Value.Trim(),out EnableValid);


            string Tel = txtTel.Value.Trim();
            string Address=txtAddress.Value.Trim();
            string OpenTime = txtOpenTime.Value.Trim();
            string RulesNote = txtRulesNote.Text.Trim();
            string Detail = txtDetail.Text.Trim();
            string RulesNoteWap = txtRulesNoteWap.Text.Trim();
            string DetailWap = txtDetailWap.Text.Trim();

            string Memo = txtMemo.Value.Trim();
            string PrintMemo = txtPrintMemo.Value.Trim();
            bool enableValidSwitch = chkEnableValidSwitch.Checked;
           
        


            #region 验证
            if (string.IsNullOrEmpty(ProductName))
            {
                PubFun.ShowMsg(this, "请输入景区名称");
                return;
            }
            if (selRegion <= 0)
            {
                PubFun.ShowMsg(this, "请选择所属地区");
                return;
            }
            if (PrimePrice <= 0)
            {
                PubFun.ShowMsg(this, "原价必须大于0");
                return;
            }
            if(txtEnableValid.Value.Trim() == "")
            {
                PubFun.ShowMsg(this, "请填写取票规定时间");
                return;
            }
            #endregion

            string imgSaveDbPath = "";
            string abPath = "/upfile/ProductTitleImg/";
            string physicPath = HttpContext.Current.Server.MapPath(@"~" + abPath);//保存的路径
            #region 题图
            //上传并导入
            if (fpUpload.PostedFile.ContentLength > 0)
            {
                string fileExtName = System.IO.Path.GetExtension(fpUpload.PostedFile.FileName).ToLower();
                string postFileName = System.IO.Path.GetFileName(fpUpload.PostedFile.FileName);
                if (fileExtName != ".jpg" & fileExtName != ".jpge" & fileExtName != ".gif" & fileExtName != ".png")
                {
                    PubFun.ShowMsg(this.Page, "你上传的不是jpg、gif、png格式的图片！");
                    return;
                }

                //保存的文件名
                //string guid = Guid.NewGuid().ToString();
                string guid = DateTime.Now.ToString("yyMMddssfff") + "_w$h_";
                string fileName = guid + fileExtName;
                string FilePath = physicPath + "\\" + fileName;

                //保存文件
                fpUpload.PostedFile.SaveAs(FilePath);
                imgSaveDbPath = abPath + fileName;
            }
            #endregion

            if (imgSaveDbPath != "")
            {
                #region 图片处理及保存
                product.TitleImg = imgSaveDbPath;
                try
                {
                    string oPath = Server.MapPath(imgSaveDbPath);
                    string fileExtName = System.IO.Path.GetExtension(oPath).ToLower();
                    string saveFileName = System.IO.Path.GetFileNameWithoutExtension(oPath);
                    saveFileName = saveFileName.Replace("_w$h_", "");

                    //小图
                    string imgSmallName = saveFileName + string.Format("_{0}${1}_", ImgHelper.width_s, ImgHelper.height_s) + fileExtName;
                    string imgSmallphysicPath = physicPath + imgSmallName;
                    string imgSmallDBPath = abPath + imgSmallName;

                    ImgHelper.MakeThumbnail(oPath, imgSmallphysicPath, ImgHelper.width_s, ImgHelper.height_m, "NHW");
                    product.TitleImg_S = imgSmallDBPath;

                    //中图
                    string imgMediumName = saveFileName + string.Format("_{0}${1}_", ImgHelper.width_m, ImgHelper.height_m) + fileExtName;
                    string imgMediumphysicPath = physicPath + imgMediumName;
                    string imgMediumDBPath = abPath + imgMediumName;

                    ImgHelper.MakeThumbnail(oPath, imgMediumphysicPath, ImgHelper.width_m, ImgHelper.height_m, "NHW");
                    product.TitleImg_M = imgMediumDBPath;
                }
                catch
                {

                }
                #endregion
            }

            product.ProductName = ProductName;
            product.RegionID = selRegion;
            product.SupplyName = SupplyName;
            product.PrimeCost = PrimePrice;
            product.EnableValid = EnableValid;
            product.EnableValidswitch = enableValidSwitch;
            product.PrintMemo = PrintMemo;


            //product.Stock = Stock;
            if (Tel!="")
                product.Tel = Tel;
            if (Address!="")
                product.Address = Address;
            if (OpenTime!="")
                product.OpenTime = OpenTime;
            if (RulesNote != "")
                product.RulesNote = RulesNote;
            if (Detail != "")
                product.Detail = Detail;
            product.RulesNote_WAP = RulesNoteWap;
            product.Detail_WAP = DetailWap;

            if (Memo != "")
                product.Memo = Memo;

            try
            {
                trans.BeginTransaction();
                if(isAdd)
                {
                    product.CategoryID = "ticket";
                    product.AddTime = DateTime.Now;
                    product.SaleFlag = false;
                    int addProductID = trans.AddProduct(product);
                    trans.Commit();
                    //Response.Redirect("ticket_price.aspx?productid=" + addProductID);
                    PubFun.ShowMsgRedirect(this, "添加成功", "/business/product/ticket_add.aspx?productid=" + addProductID);
                }
                else
                {
                    product.UpdateTime = DateTime.Now;
                    trans.UpdateObject(product);
                    trans.Commit();
                    //Response.Redirect("ticket_price.aspx?productid=" + productID);
                    PubFun.ShowMsgRedirect(this, "保存成功", "/business/product/ticket_add.aspx?productid=" + productID);
                }
            }
            catch (Exception ex)
            {
                trans.RollBack();
                if (isAdd)
                {
                    PubFun.ShowMsg(this, "添加记录失败。" + ex.Message);
                }
                else
                {
                    PubFun.ShowMsg(this, "更新记录失败。" + ex.Message);
                }
            }
            finally
            {
                trans.Close();
            }
        }

        void InitNav(int productID)
        {
           
            if (productID <= 0)
            {
                //添加数据时
                //hyProduct.NavigateUrl = "#";

                hyPrice.NavigateUrl = "#";
                hyPrice.Attributes.Add("onclick", "alert('请先保存景区基本信息')");

                hyPublish.NavigateUrl = "#";
                hyPublish.Attributes.Add("onclick", "alert('请先保存景区基本信息')");
            }
            else
            {
                //hyProduct.NavigateUrl = "line_add.aspx?productid=" + productID;
                hyPrice.NavigateUrl = "ticket_price.aspx?productid=" + productID;
                hyPublish.NavigateUrl = "ticket_publish.aspx?productid=" + productID;
            }

            divProduct.Attributes.Add("class", "tab02");
            divPrice.Attributes.Add("class", "tab01");
            divPublish.Attributes.Add("class", "tab01");
           
        }
    }
}