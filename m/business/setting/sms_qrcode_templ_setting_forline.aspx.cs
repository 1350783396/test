using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.setting
{
    public partial class sms_qrcode_templ_setting_forline : AdminBase
    {
        string key = ETicket.Utility.ConfigKeyEnum.line_sms_qrcode_templ.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            if(!Page.IsPostBack)
            {
                var model = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey ==key);
                if(model==null)
                {
                    this.txtDay.Value = "";
                }
                else
                {
                    this.txtDay.Value = model.ConfigValue;
                    this.litMemo.Text = model.ConfigMemo;
                }
                
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string value = this.txtDay.Value.Trim();
            if(value=="")
            {
                PubFun.ShowMsg(this,"请输入内容");
                return;
            }
            var model = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == key);
            if (model != null)
            {
                model.ConfigValue = value;
                 BLL.SysConfigBLL.Instance.UpdateObject(model);
            }
            else
            {
                model = new EFEntity.SysConfig();
                model.ConfigKey = key;
                model.ConfigValue = value;
                BLL.SysConfigBLL.Instance.AddObject(model);
            }

            PubFun.ShowMsgRedirect(this,"保存成功",Request.RawUrl);
        }
    }
}