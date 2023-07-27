using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    public partial class get_account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JsonResult<Account> jsonModel = new JsonResult<Account>();
            string jsonStr = "";
            string action = PubFun.QueryString("action");

            //参数
            string token = PubFun.QueryString("token");
            EFEntity.User user = new EFEntity.User();
            if (token != "")
                user = BLL.UserBLL.Instance.GetUserForCookie(token);

            if (user == null || user.UserID <= 0)
            {
                jsonModel.Status = "0";
                jsonModel.Msg = "请先登录系统后再提交";
                jsonStr = JsonHelper.GetJson<JsonResult<Account>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }

            if (action == "list")
            {
                int pageIndex = PubFun.QueryInt("pageindex");
                int pageSize = PubFun.QueryInt("pagesize");
                if (pageIndex <= 0)
                    pageIndex = 1;
                if (pageSize <= 0)
                    pageSize = ApiHelper.DefaultPageSize;

                //自己的订单
                StringBuilder sb = new StringBuilder();
                sb.Append(" it.UserID=" + user.UserID);

                ETicket.Utility.PageInfo<EFEntity.AccountLog> pi = null;
                pi = BLL.AccountLogBLL.Instance.GetPageList(pageIndex, pageSize, sb.ToString(), "it.PKID DESC");

                List<Account> resultList = new List<Account>();
                //转换为接口Model
                foreach (var accItem in pi.List)
                {
                    var resultItem = ApiHelper.AccountLog2Result(accItem);
                    resultList.Add(resultItem);
                }

                //生成json
                jsonModel.Status = "1";
                jsonModel.RecordCount = pi.RecordCount;
                jsonModel.PageCount = pi.PageCount;
                jsonModel.PageIndex = pageIndex;
                jsonModel.PageSize = pageSize;
                jsonModel.Msg = user.Account==null?"0":user.Account.ToString();
                jsonModel.List = resultList;

                jsonStr = JsonHelper.GetJson<JsonResult<Account>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
            }
        }
    }
}