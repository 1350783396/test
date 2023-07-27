using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.content
{
    public partial class search_news : System.Web.UI.Page
    {
        public string seartT = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var t = PubFun.QueryString("t");
            string msg = "";
            if (t != "")
                msg = "【" + t + "】";
            seartT = string.Format("搜索{0}结果", msg);
        }
    }
}