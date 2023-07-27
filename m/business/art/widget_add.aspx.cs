using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.art
{
    public partial class widget_add : AdminBase
    {
        string moduleID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            moduleID = PubFun.QueryString("mid");
            if (moduleID=="link")
            {
                this.trUpLoad.Attributes.Add("style","display:none");
            }
            this.btnAdd.Click += btnAdd_Click;
            if (!Page.IsPostBack)
            {
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            string Title = this.txtTitel.Text.Trim();
            string txtHref = this.txtHref.Text.Trim();
            
            if (Title == "")
            {
                PubFun.ShowMsg(this.Page, "请输入标题");
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

                string abPath = "/upfile/carousel/";
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

            //if(imgSaveDbPath=="")
            //{
            //    PubFun.ShowMsg(this.Page, "请上传标题图");
            //    return;
            //}

            EFEntity.PageWidget model = new EFEntity.PageWidget();

            if (imgSaveDbPath != "")
                model.TitleImg = imgSaveDbPath;

            if (txtHref != "")
                model.TitleHref = txtHref;

            model.Title = Title;
            model.AddTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.Category = PubFun.QueryString("mid");

            try
            {
                BLL.PageWidgetBLL.Instance.AddObject(model);
                PubFun.ShowMsgRedirect(this.Page, "添加成功!", this.Request.RawUrl);
            }
            catch (Exception Ex)
            {
                PubFun.ShowMsg(this.Page, "添加失败：发生错误：!" + Ex.Message);
            }
        }

    }
}