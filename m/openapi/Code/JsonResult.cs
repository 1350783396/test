using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web.openapi
{
    public class JsonResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get;
            set;
        }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string Msg
        {
            get;
            set;
        }
    }
}