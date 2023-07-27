using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WxPayAPI;

namespace ETicket.Web.wap
{
    public partial class pay_weixin_notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResultNotify resultNotify = new ResultNotify(this);
            resultNotify.ProcessNotify();
        }
    }
}