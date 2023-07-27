using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.content
{
    public partial class list_art : System.Web.UI.Page
    {
        public string ModuleName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int mid = PubFun.QueryInt("mid");
                var moduleModel = BLL.ArtModuleBLL.Instance.GetEntity(p => p.ModuleID == mid);
                if (moduleModel != null)
                    ModuleName = moduleModel.ModuleName;

                string html = ETicket.Web.HtmlController.Instance.IndexListArt(mid, 200, 200, "/templ/index/tab_art.html");
                litHtml.Text = html;
            }
        }
    }
}