using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NJiaSu.Libraries;

namespace ETicket.Web.wap
{
    public class WapHelper
    {
        /// <summary>
        /// 获取传递过来的参数
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ProductID"></param>
        public static string GetUserID_ProductID(string qName,out int userID, out int productID)
        {
            userID = 0;
            productID = 0;

            var qContent = PubFun.QueryString(qName);
            if (qContent == "")
                return "签名信息为空";

            var strAry = qContent.Split('_');
            var md5 = strAry[0];
            var userIDStr = strAry[1];
            var productIDStr = strAry[2];

            var md5Valid = NJiaSu.Libraries.Encrypt.GetMd5Hash("XKRQ_View_" + userIDStr + productIDStr);
            if (md5Valid == md5)
            {
                int.TryParse(userIDStr, out userID);
                int.TryParse(productIDStr, out productID);
                return "";
            }
            else
            {
                return "核对签名失败";
            }
        }


    }
}