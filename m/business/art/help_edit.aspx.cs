using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.art
{
    public partial class help_edit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnAdd.Click += btnAdd_Click;
            if(!Page.IsPostBack)
            {
                int artID = PubFun.QueryInt("artid");
                EFEntity.HelpContent model =BLL.HelpContentBLL.Instance.GetEntity(p=>p.HelpID==artID);
                if (model == null)
                    return;

                this.txtTitel.Text = model.HelpTitle;
                this.txtArtContent.Text = model.HelpContent1;
               

               

                //模块
                int moduleID = model.CategoryID.Value;
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

           
            int artID = PubFun.QueryInt("artid");
            EFEntity.HelpContent model = BLL.HelpContentBLL.Instance.GetEntity(p => p.HelpID == artID);


            model.HelpTitle = Title;
            model.HelpContent1 = ArtContent;
            //model.AddTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;


            try
            {
                BLL.HelpContentBLL.Instance.UpdateObject(model);
                PubFun.ShowMsgRedirect(this.Page, "更新成功!", this.Request.RawUrl);
            }
            catch (Exception Ex)
            {
                PubFun.ShowMsg(this.Page, "更新失败：发生错误：!" + Ex.Message);
            }
        }
    }
}