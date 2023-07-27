using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;


namespace ETicket.Web.business.partner
{
    public partial class myqr_code_down : PartnerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var cnName = PubFun.QueryString("c");
            var flieName = PubFun.QueryString("n");
            if (cnName != "" & flieName != "")
            {
                var filePath = Server.MapPath("/tempfile/" + flieName + ".jpg");
                DownLoadHelper.Instance.DownLoadFile(this.Page, cnName+".jpg", filePath);
            }
        }
    }
}