using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJiaSu.Libraries;

namespace ETicket.Web.business.user
{
    public partial class account_out_add : AdminBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.ServerClick += btnSave_ServerClick;
            if (!Page.IsPostBack)
            {
                /*
                IEnumerable<EFEntity.User> userList = BLL.UserBLL.Instance.GetEntities(p => p.UserCategory == "partner");
                ddlUser.Items.Clear();
                ddlUser.Items.Add(new ListItem("请选择", "0"));
                foreach (var user in userList)
                {
                    ddlUser.Items.Add(new ListItem(user.CPName + string.Format("[联系人：{0},联系电话：{1}]",user.RealName,user.Phone), user.UserID.ToString()));
                }
                */
            }
        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            string Account = txtAccount.Value.Trim();
            decimal accountValue = 0;

            string RemarkRequest = txtRemarkRequest.Value.Trim();
            //int userID = int.Parse(ddlUser.SelectedValue);

            int userID = 0;
            int.TryParse(txtSelValue.Value.Trim(), out userID);
            #region 验证
            if (userID<=0)
            {
                PubFun.ShowMsg(this, "请选择分销商");
                return;
            }
            if (string.IsNullOrEmpty(Account))
            {
                PubFun.ShowMsg(this, "请输入充值金额");
                return;
            }
            try
            {
                accountValue = decimal.Parse(Account);
            }
            catch
            {
                PubFun.ShowMsg(this, "请输入正确的充值金额");
                return;
            }
            #endregion


            //验证消除积分是否大于目前所有积分
            bool isAllowOut= BLL.AccountOutFlowBLL.Instance.IsAllowOut(accountValue, userID);
            if(!isAllowOut)
            {
                PubFun.ShowMsg(this, "当前积分余额少于" + accountValue+",无法销除");
                return;
            }

            EFEntity.AccountOutFlow model = new EFEntity.AccountOutFlow();
            model.UserID = userID;
            model.Requst_Amount = accountValue;
            model.Stauts = Utility.AccountFlowStatusEnum.申请中.ToString();
            model.Requst_Remark = RemarkRequest;
            model.Requst_UserID = BLL.UserBLL.Instance.GetLoginModel().UserID;
            model.Requst_Time = DateTime.Now;
            model.Requst_IP = PubFun.GetClientIP();

            try
            {
                BLL.AccountOutFlowBLL.Instance.AddObject(model);
                PubFun.ShowMsgRedirect(this, "保存成功！", Request.RawUrl);
            }
            catch (Exception ex)
            {
                PubFun.ShowMsg(this, "保存数据出错：" + ex.Message);
            }
        }
    }
}