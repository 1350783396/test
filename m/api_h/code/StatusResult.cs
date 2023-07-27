using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web.api_h
{
    public class StatusResult
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
        public string StatusMsg
        {
            get;
            set;
        }

    }
}