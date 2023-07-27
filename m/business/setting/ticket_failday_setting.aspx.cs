using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.setting
{
    public partial class ticket_failday_setting : AdminBase
    {
        string key = ETicket.Utility.ConfigKeyEnum.ticket_fail_day.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            if(!Page.IsPostBack)
            {
                var model = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == key);
                if(model==null)
                {
                    this.txtDay.Value = "3";
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
            int day = 3;
            try
            {
                day = int.Parse(this.txtDay.Value.Trim());
            }
            catch
            {
                PubFun.ShowMsg(this,"请输入正确的数字");
                return;
            }
            if(day<=0)
            {
                PubFun.ShowMsg(this, "请输入正确的数字，必须大于0");
                return;
            }
            var model = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == key);
            if (model != null)
            {
                 model.ConfigValue=day.ToString();
                 BLL.SysConfigBLL.Instance.UpdateObject(model);
            }
            else
            {
                model = new EFEntity.SysConfig();
                model.ConfigKey = key;
                model.ConfigValue = day.ToString();
                BLL.SysConfigBLL.Instance.AddObject(model);
            }

            PubFun.ShowMsgRedirect(this,"保存成功",Request.RawUrl);
        }
    }
}