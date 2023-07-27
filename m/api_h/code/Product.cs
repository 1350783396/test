using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web.api_h
{
    public class Product
    {
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
       public decimal PrimeCost
       {
            get;
            set;
        }
       public string RulesNote_WAP
       {
           get;
           set;
       }
       public string Detail_WAP
       {
           get;
           set;
       }
       public bool SaleFlag
       {
           get;
           set;
       }
       public string Tel
       {
           get;
           set;
       }
       public string TitleImg_S
       {
           get;
           set;
       }
       public string TitleImg_M
       {
           get;
           set;
       }
       public string TitleImg_B
       {
           get;
           set;
       }
       public string Address
       {
           get;
           set;
       }
       public string OpenTime
       {
           get;
           set;
       }
       public decimal Price
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

    }
}