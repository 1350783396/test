using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.business.setting
{
    public partial class accout_checkin_code : AdminBase
    {
        string key = ETicket.Utility.ConfigKeyEnum.accout_checkin_code.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            if (!Page.IsPostBack)
            {


            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            var model = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == key);
            if (model == null)
            {
                PubFun.ShowMsg(this, "系统没有初始化积分审核授权码，请联系技术人员初始化");
                return;
            }

            //string keyValue = "888888";
            string oldVallue = this.txtOld.Value.Trim();
            string keyValue = this.txtNew.Value.Trim();
            string keyValue2 = this.txtNew2.Value.Trim();

            string oldVallueMd5 = Encrypt.GetMd5Hash(Encrypt.GetMd5Hash(oldVallue));
            if (oldVallueMd5 != model.ConfigValue)
            {
                PubFun.ShowMsg(this, "输入的旧授权码不正确，请重新输入");
                return;
            }
            if (keyValue == "")
            {
                PubFun.ShowMsg(this, "请输入新的授权码");
                return;
            }
            if (keyValue != keyValue2)
            {
                PubFun.ShowMsg(this, "两次输入的授权码不一致");
                return;
            }
            if (oldVallue == keyValue)
            {
                PubFun.ShowMsg(this, "新设置授权码不能和旧的授权码一样，请更改");
                return;
            }
            
            model.ConfigValue = Encrypt.GetMd5Hash(Encrypt.GetMd5Hash(keyValue));
            BLL.SysConfigBLL.Instance.UpdateObject(model);

            /*
            model = new EFEntity.SysConfig();
            model.ConfigKey = key;
            model.ConfigValue = Encrypt.GetMd5Hash(Encrypt.GetMd5Hash(keyValue));
            BLL.SysConfigBLL.Instance.AddObject(model);
            */
            PubFun.ShowMsgRedirect(this, "更改授权码成功", Request.RawUrl);
        }
    }
}