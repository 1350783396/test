using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web.api
{
    /// <summary>
    /// 返回客户端订单对象
    /// </summary>
    public class OrderModel
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
        public string OrderID
        {
            get;
            set;
        }
        public string SheetID
        {
            get;
            set;
        }
        public string CategoryID
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }
        public string NUM
        {
            get;
            set;
        }
        public string OrderTime
        {
            get;
            set;
        }
        public string TicketFailTime
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }
        public string RealName
        {
            get;
            set;
        }
        public string StartTime
        {
            get;
            set;
        }
        public string StartAddress
        {
            get;
            set;
        }
        public string OrderStatus
        {
            get;
            set;
        }
        public string ValidCode
        {
            get;
            set;
        }

        public string PalyDate
        {
            get;
            set;
        }

    }
}