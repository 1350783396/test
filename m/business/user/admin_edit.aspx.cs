using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class admin_edit : AdminBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                this.txtUserName.Disabled = true;

                IEnumerable<EFEntity.Role> roleList = BLL.RoleBLL.Instance.GetEntities();
                ddlRole.Items.Clear();
                ddlRole.Items.Add(new ListItem("请选择", "0"));
                foreach (var role in roleList)
                {
                    ddlRole.Items.Add(new ListItem(role.RoleName, role.RoleID.ToString()));
                }

                int userid = PubFun.QueryInt("userid");
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
                if (user!=null)
                {
                    this.txtUserName.Value=user.UserName;
                    this.ddlRole.SelectedValue=  user.RoleID.ToString();
                    this.txtRealName.Value= user.RealName;
                    this.txtPhone.Value=user.Phone;
                    this.txtEmail.Value= user.Email ;
                }
            }

        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            string UserName = txtUserName.Value.Trim();
            string Password = txtPassword.Value.Trim();

            int RoleID = int.Parse(ddlRole.SelectedValue);
            string RealName = txtRealName.Value.Trim();
            string Phone = txtPhone.Value.Trim();
            string Email = txtEmail.Value.Trim();
         

            #region 验证
            if (string.IsNullOrEmpty(UserName))
            {
                PubFun.ShowMsg(this, "请输入用户名");
                return;
            }
            if (RoleID <= 0)
            {
                PubFun.ShowMsg(this, "请选择所属角色");
                return;
            }
            if (string.IsNullOrEmpty(RealName))
            {
                PubFun.ShowMsg(this, "请输入真实姓名");
                return;
            }
            if (string.IsNullOrEmpty(Phone))
            {
                PubFun.ShowMsg(this, "请输入手机号码");
                return;
            }
            #endregion

            int userid = PubFun.QueryInt("userid");
            EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
            if (user==null)
            {
                PubFun.ShowMsg(this, "更新分销商失败，数据不存在。");
                return;
            }
            //user.UserName = UserName;
            if(!string.IsNullOrEmpty(Password))
                user.Password = Encrypt.GetMd5Hash(Password);

            user.RoleID = RoleID;
            user.RealName = RealName;
            user.Phone = Phone;
            user.Email = Email;
           
            try
            {
                BLL.UserBLL.Instance.UpdateObject(user);
                PubFun.ShowMsgRedirect(this, "更新数据成功！", Request.RawUrl);
            }
            catch (Exception ex)
            {
                PubFun.ShowMsg(this, "更新数据出错：" + ex.Message);
            }
        }
    }
}