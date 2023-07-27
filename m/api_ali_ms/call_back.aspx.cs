using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETicket.Web.api_ali_ms
{
    public partial class test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "test2");
            Response.Write(NJiaSu.Libraries.PubFun.QueryString("top_session"));
        }
    }
}