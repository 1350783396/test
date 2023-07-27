using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web.api
{
    /// <summary>
    /// 客户端登录返回对象
    /// </summary>
    public class LoginModel
    {
        public string Status
        {
            get;
            set;
        }
        public string StatusMsg
        {
            get;
            set;
        }

        public string UserID
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Token
        {
            get;
            set;
        }
    }
}