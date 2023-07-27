using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public partial class msgpage : System.Web.UI.Page
    {
        public string msg = "";
        public string style = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            int code=PubFun.QueryInt("t");
            msg = GetMsg(code);
        }

        string GetMsg(int code)
        {
            msg="";
            switch(code)
            {
                case 1:
                    msg="出错啦，订单不存在";
                    style = "ErrorInfo";
                    break;
                case 2:
                   msg="订单已经支付";
                   style = "SuccInfo";
                   break;
                case 3:
                   msg = "恭喜你，订单已支付成功！";
                   style = "SuccInfo";
                   break;
                case 4:
                   msg = "错误啦，申请退款页面错误，请重新打开申请退款！";
                   style = "ErrorInfo";
                   break;
                case 5:
                   msg = "错误啦，该订单不符合申请退款条件！";
                   style = "ErrorInfo";
                   break;
                case 6:
                   msg = "申请退款操作成功，系统将审核你的退款请求！";
                   style = "SuccInfo";
                   break;
                case 7:
                   msg = "你没有权限，请更换账号登录！";
                   style = "SuccInfo";
                   break;
                case 8:
                   msg = "抱歉！支付失败，请重新支付。";
                   style = "ErrorInfo";
                   break;
                case 9:
                   msg = "产品不存在或者已下架停售";
                   style = "ErrorInfo";
                   break;
                case 10:
                   msg = "你的账号无法下单购买，只有普通会员、分销商账号才能下单购买";
                   style = "ErrorInfo";
                   break;
            }
            return msg;
        }
    }
}