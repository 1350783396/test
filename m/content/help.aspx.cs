using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public partial class help : System.Web.UI.Page
    {
        public string HelpTitle = "", HelpContent = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = PubFun.QueryInt("id");
                if (id >= 0)
                {
                   var model= BLL.HelpContentBLL.Instance.GetEntity(p => p.HelpID == id);
                   if (model == null)
                       return;
                   HelpTitle = model.HelpTitle;
                   HelpContent = model.HelpContent1;

                }
            }
        }
    }
}