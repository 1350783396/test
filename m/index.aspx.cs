using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETicket.Web
{
    public partial class index : System.Web.UI.Page
    {
        public string key = "", des = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //获取站点SEO配置信息
               string congfigKey = ETicket.Utility.ConfigKeyEnum.seo_key_index.ToString();
               string congfigDes = ETicket.Utility.ConfigKeyEnum.seo_des_index.ToString();
               key = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == congfigKey).ConfigValue;
               des = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == congfigDes).ConfigValue;

            }
        }
    }
}