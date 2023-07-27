using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.content
{
    public partial class search : System.Web.UI.Page
    {
        public string seartT = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = PubFun.QueryString("type");
            string content = PubFun.QueryString("content");


            if (type == "line" || type == "ticket")
            {
                string url = string.Format("/search-product.html?g={0}&t={1}", type, Server.UrlEncode(content));
                Response.Redirect(PubFun.ApplicationPath + url);
            }
            else
            {
                string url = string.Format("/search-news.html?g={0}&t={1}", type, Server.UrlEncode(content));
                Response.Redirect(PubFun.ApplicationPath + url);
            }
        }
    }
}