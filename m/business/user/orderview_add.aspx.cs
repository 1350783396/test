using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class orderview_add : AdminBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                //IEnumerable<EFEntity.Role> roleList = BLL.RoleBLL.Instance.GetEntities();
                //ddlRole.Items.Clear();
                //ddlRole.Items.Add(new ListItem("请选择", "0"));
                //foreach (var role in roleList)
                //{
                //    ddlRole.Items.Add(new ListItem(role.RoleName, role.RoleID.ToString()));
                //}
                var listLine = BLL.ProductBLL.Instance.GetEntities(p => p.CategoryID == "line");
                foreach (var item in listLine)
                {
                    this.chkListLine.Items.Add(new ListItem(item.ProductName, item.ProductID.ToString()));
                }
                var listTicket = BLL.ProductBLL.Instance.GetEntities(p => p.CategoryID == "ticket");
                foreach (var item in listTicket)
                {
                    this.chkListTicket.Items.Add(new ListItem(item.ProductName, item.ProductID.ToString()));
                }
            }

        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            string UserName = txtUserName.Value.Trim();
            string Password = txtPassword.Value.Trim();
            string Password2 = this.txtPassword2.Value.Trim();
            //int RoleID = int.Parse(ddlRole.SelectedValue);
            string RealName = txtRealName.Value.Trim();
            string Phone = txtPhone.Value.Trim();
            string Email = txtEmail.Value.Trim();

            #region 验证
            if (string.IsNullOrEmpty(UserName))
            {
                PubFun.ShowMsg(this, "请输入用户名");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                PubFun.ShowMsg(this, "请输入密码");
                return;
            }
            if (Password2 != Password)
            {
                PubFun.ShowMsg(this, "两次输入的密码不一致");
                return;
            }
            var userExist = BLL.UserBLL.Instance.GetEntity(p => p.UserName == UserName);
            if (userExist != null)
            {
                PubFun.ShowMsg(this, "该用户名已经存在，无法注册");
                return;
            }

            int count = 0;
            foreach (ListItem item in chkListLine.Items)
            {
                if (item.Selected)
                {
                    count++;
                }
            }
            foreach (ListItem item in chkListTicket.Items)
            {
                if (item.Selected)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                PubFun.ShowMsg(this, "至少选择一个专线或者景区");
                return;
            }
            //if (RoleID <= 0)
            //{
            //    PubFun.ShowMsg(this, "请选择所属角色");
            //    return;
            //}
            //if (string.IsNullOrEmpty(RealName))
            //{
            //    PubFun.ShowMsg(this, "请输入真实姓名");
            //    return;
            //}
            //if (string.IsNullOrEmpty(Phone))
            //{
            //    PubFun.ShowMsg(this, "请输入手机号码");
            //    return;
            //}

            #endregion

            EFEntity.User user = new EFEntity.User();
            user.UserName = UserName;
            user.Password = Encrypt.GetMd5Hash(Password);

            user.UserCategory = "orderview";
            //user.RoleID = RoleID;

            if (RealName != "")
                user.RealName = RealName;
            if (Phone != "")
                user.Phone = Phone;
            if (Email != "")
                user.Email = Email;

            user.Account = 0;
            user.EnableAccountPay = false;
            user.EnableCashPay = false;
            user.EnableOnlinePay = false;
            user.RegTime = DateTime.Now;
            user.RegIP = PubFun.GetClientIP();
            user.ClientType = ETicket.Utility.ClientTypeEnum.pc.ToString();
            user.ClientName = PubFun.GetBrowserInfo();

            bool transState = false;
            EFDAO.TransactionBaseDAO trans = new EFDAO.TransactionBaseDAO();
            try
            {
                trans.BeginTransaction();

                int userID = trans.AddUser(user);
                foreach (ListItem item in chkListLine.Items)
                {
                    if (item.Selected)
                    {
                        EFEntity.OrderView valid = new EFEntity.OrderView();
                        valid.ProductID = int.Parse(item.Value);
                        valid.UserID = userID;
                        trans.AddObject(valid);
                    }
                }
                foreach (ListItem item in chkListTicket.Items)
                {
                    if (item.Selected)
                    {
                        EFEntity.OrderView valid = new EFEntity.OrderView();
                        valid.ProductID = int.Parse(item.Value);
                        valid.UserID = userID;
                        trans.AddObject(valid);
                    }
                }
                trans.Commit();//提交事务
                transState = true;
            }
            catch (Exception ex)
            {
                transState = false;
                trans.RollBack();//回滚事务
                NJiaSu.Libraries.LogHelper.Log.Info(ex.ToString());
            }
            finally
            {
                trans.Close();//关闭事务
            }
            if (transState)
            {
                PubFun.ShowMsgRedirect(this, "保存成功！", Request.RawUrl);
            }
            else
            {
                PubFun.ShowMsg(this, "保存失败！");
            }
        }
    }
}