using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETicket.Web.business
{
    public partial class main : AdminBase
    {
        public int roleid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            int userid=base.CookiesUser.UserID;
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
            roleid = user.RoleID.Value;
        }
    }
}