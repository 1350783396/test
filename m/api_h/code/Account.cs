using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web.api_h
{
    public class Account
    {
       public int  PKID{
           get;
           set;
       }
       public int UserID{
           get;
           set;
       }

      public string ActType{
          get;
          set;
      }
      public decimal ActAmount{
          get;
          set;
      }
      public string ActTime{
          get;
          set;
      }
      public string Memo{
          get;
          set;
      }
      public string FormType{
          get;
          set;
      }
      public string FormOrderID
      {
          get;
          set;
      }
    }
}