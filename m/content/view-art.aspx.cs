using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries; 

namespace ETicket.Web.content
{
    public partial class view_art : System.Web.UI.Page
    {
        public string title="", content = "",time="",key="",des="";
        protected void Page_Load(object sender, EventArgs e)
        {
            int artID = PubFun.QueryInt("artid");
            var model=BLL.ArtContentBLL.Instance.GetEntity(p => p.ArtID == artID);
            if(model!=null)
            {
                title = model.ArtTitle;
                content = model.ArtContent1;
                time = model.AddTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                key = model.SEOKey;
                des = model.SEODes;
            }
        }
    }
}