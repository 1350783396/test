using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web.api_h
{
    public class Order
    {

       public int OrderID
       {
           get;
           set;
       }
       public string SheetID
       {
           get;
           set;
       }
       public int ProductID
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
       public int NUM
       {
           get;
           set;
       }
       public decimal UnitPrice
       {
           get;
           set;
       }
       public decimal TotalPrice
       {
           get;
           set;
       }
       public decimal RebateUnit
       {
           get;
           set;
       }
       public decimal RebateTotal
       {
           get;
           set;
       }
       public string OrderTime
       {
           get;
           set;
       }
       public string PayType
       {
           get;
           set;
       }
       public string PayTypeSub
       {
           get;
           set;
       }
       public string PayState
       {
           get;
           set;
       }
       public string PayFailTime
       {
           get;
           set;
       }
       public string TicketFailTime
       {
           get;
           set;
       }
       public string ValidType
       {
           get;
           set;
       }
       public string SMSSendStatus
       {
           get;
           set;
       }
       public string Phone
       {
           get;
           set;
       }
       public string IDCard
       {
           get;
           set;
       }
       public string RealName
       {
           get;
           set;
       }
       public int UserID
       {
           get;
           set;
       }
       public string UserName
       {
           get;
           set;
       }
       public string UserCategory
       {
           get;
           set;
       }
       public string UserLevelName
       {
           get;
           set;
       }
       public string StartAddress
       {
           get;
           set;
       }
       public string StartTime
       {
           get;
           set;
       }
       public int StartH
       {
           get;
           set;
       }
       public int StartM
       {
           get;
           set;
       }
       public string LastOrderTime
       {
           get;
           set;
       }
       public int TickID
       {
           get;
           set;
       }
       public string PalyDate
       {
           get;
           set;
       }
       public string OrderStatus
       {
           get;
           set;
       }
       public string Memo
       {
           get;
           set;
       }
       public string ValidTime
       {
           get;
           set;
       }
       public int EnableValid
       {
           get;
           set;
       }
       public string EnableValidTime
       {
           get;
           set;
       }
       public string ClientType
       {
           get;
           set;
       }
       public string ScanKey
       {
           get;
           set;
       }
    }
}