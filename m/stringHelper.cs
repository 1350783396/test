using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web
{
    public static class stringHelper
    {
        public static string IntTextColor(this int srt)
        {
            return "<span style='color:red;'>" + srt + "</span>";
        }
    }
}