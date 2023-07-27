using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.art
{
    public partial class art_edit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnAdd.Click += btnAdd_Click;
            if(!Page.IsPostBack)
            {
                int artID = PubFun.QueryInt("artid");
                EFEntity.ArtContent model =BLL.ArtContentBLL.Instance.GetEntity(p=>p.ArtID==artID);
                if (model == null)
                    return;

                this.txtTitel.Text = model.ArtTitle;
                this.txtArtContent.Text = model.ArtContent1;
                this.txtKey.Text = model.SEOKey;
                this.txtDes.Text = model.SEODes;

                if (string.IsNullOrEmpty(model.ImgTitle))
                {
                     this.titleImg.Visible = false;
                }
                else
                {
                    this.titleImg.ImageUrl = model.ImgTitle;
                }

                //模块
                int moduleID = model.ModuleID.Value;
                var mModel = BLL.ArtModuleBLL.Instance.GetEntity(p => p.ModuleID == moduleID);
                if (mModel != null)
                {
                    this.litModeleName.Text = mModel.ModuleName;
                }
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            string Title = this.txtTitel.Text.Trim();
            string ArtContent = this.txtArtContent.Text.Trim();
            string seoKey = this.txtKey.Text.Trim();
            string seoDes = this.txtKey.Text.Trim();
            if (Title == "")
            {
                PubFun.ShowMsg(this.Page, "请输入标题");
                return;
            }
            if (ArtContent == "")
            {
                PubFun.ShowMsg(this.Page, "请输入内容");
                return;
            }

            string imgSaveDbPath = "";
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

                string abPath = "/upfile/TitleImg/";
                //保存的路径
                string physicPath = HttpContext.Current.Server.MapPath(@"~" + abPath);
                //保存的文件名
                string guid = Guid.NewGuid().ToString();
                string fileName = guid + fileExtName;
                string FilePath = physicPath + "\\" + fileName;

                //保存文件
                fpUpload.PostedFile.SaveAs(FilePath);
                imgSaveDbPath = abPath + fileName;
            }
            #endregion

            int artID = PubFun.QueryInt("artid");
            EFEntity.ArtContent model = BLL.ArtContentBLL.Instance.GetEntity(p => p.ArtID == artID);

            if (imgSaveDbPath != "")
                model.ImgTitle = imgSaveDbPath;
            
            model.ArtTitle = Title;
            model.SEOKey = seoKey;
            model.SEODes = seoDes;
            model.ArtContent1 = ArtContent;
            //model.AddTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;


            try
            {
                BLL.ArtContentBLL.Instance.UpdateObject(model);
                PubFun.ShowMsgRedirect(this.Page, "更新成功!", this.Request.RawUrl);
            }
            catch (Exception Ex)
            {
                PubFun.ShowMsg(this.Page, "更新失败：发生错误：!" + Ex.Message);
            }
        }
    }
}