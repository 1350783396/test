using System;
using System.Configuration;

namespace WebConfig
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string config = ConfigurationManager.AppSettings["ab"];
            var asd = config;
        }
    }
}