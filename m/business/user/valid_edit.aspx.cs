using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class valid_edit : AdminBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                this.txtUserName.Disabled = true;

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

                int userid = PubFun.QueryInt("userid");
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userid);
                if (user!=null)
                {
                    this.txtUserName.Value=user.UserName;
                    
                    this.txtRealName.Value= user.RealName;
                    this.txtPhone.Value=user.Phone;
                    this.txtEmail.Value= user.Email ;

                    //绑定选择数据
                    var validList = BLL.ProductValidBLL.Instance.GetEntities(p => p.UserID == user.UserID);
                    foreach(var validItem in validList)
                    {
                        foreach (ListItem item in chkListLine.Items)
                        {
                            if (item.Value == validItem.ProductID.ToString())
                                item.Selected = true;
                        }
                        foreach (ListItem item in chkListTicket.Items)
                        {
                            if (item.Value == validItem.ProductID.ToString())
                                item.Selected = true;
                        }
                    }
                }
            }
        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            string UserName = txtUserName.Value.Trim();
            string Password = txtPassword.Value.Trim();

           
            string RealName = txtRealName.Value.Trim();
            string Phone = txtPhone.Value.Trim();
            string Email = txtEmail.Value.Trim();
         

            #region 验证
            if (string.IsNullOrEmpty(UserName))
            {
                PubFun.ShowMsg(this, "请输入用户名");
                return;
            }
           
            /*
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
            */
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


            if (RealName != "")
                user.RealName = RealName;
            if (Phone != "")
                user.Phone = Phone;
            if (Email != "")
                user.Email = Email;
           
            try
            {
                BLL.UserBLL.Instance.UpdateObject(user);
                foreach (ListItem item in chkListLine.Items)
                {
                    if (item.Selected)
                    {
                        int productID=int.Parse(item.Value);
                        EFEntity.ProductValid valid = BLL.ProductValidBLL.Instance.GetEntity(p=>p.UserID==user.UserID&&p.ProductID==productID);
                        if(valid==null)
                        {
                            valid = new EFEntity.ProductValid();
                            valid.ProductID = productID;
                            valid.UserID = user.UserID;
                            BLL.ProductValidBLL.Instance.AddObject(valid);
                        }
                    }
                }
                foreach (ListItem item in chkListTicket.Items)
                {
                    if (item.Selected)
                    {
                        int productID = int.Parse(item.Value);
                        EFEntity.ProductValid valid = BLL.ProductValidBLL.Instance.GetEntity(p => p.UserID == user.UserID && p.ProductID == productID);
                        if (valid == null)
                        {
                            valid = new EFEntity.ProductValid();
                            valid.ProductID = productID;
                            valid.UserID = user.UserID;
                            BLL.ProductValidBLL.Instance.AddObject(valid);
                        }
                    }
                }

                PubFun.ShowMsgRedirect(this, "更新数据成功！", Request.RawUrl);
            }
            catch (Exception ex)
            {
                PubFun.ShowMsg(this, "更新数据出错：" + ex.Message);
            }
        }
    }
}