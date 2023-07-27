using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.art
{
    public partial class help_add : AdminBase
    {
        int moduleID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            moduleID = PubFun.QueryInt("mid");
            this.btnAdd.Click += btnAdd_Click;
            if (!Page.IsPostBack)
            {
                var mModel = BLL.HelpCategoryBLL.Instance.GetEntity(p => p.CategoryID == moduleID);
                if (mModel != null)
                {
                    this.litModeleName.Text = mModel.CategoryName;
                }
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            string Title = this.txtTitel.Text.Trim();
            string ArtContent = this.txtArtContent.Text.Trim();
            //string seoKey = this.txtKey.Text.Trim();
            //string seoDes = this.txtKey.Text.Trim();
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



            EFEntity.HelpContent model = new EFEntity.HelpContent();
            model.CategoryID = PubFun.QueryInt("MID");
            model.HelpTitle = Title;
            model.HelpContent1 = ArtContent;
            model.AddTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;


            try
            {
                BLL.HelpContentBLL.Instance.AddObject(model);
                PubFun.ShowMsgRedirect(this.Page, "添加成功!", this.Request.RawUrl);
            }
            catch (Exception Ex)
            {
                PubFun.ShowMsg(this.Page, "添加失败：发生错误：!" + Ex.Message);
            }
        }

    }
}