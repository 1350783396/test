using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web.api
{
    public class ApiHelper
    {
        public static string md5Key
        {
            get
            {
                return "ET@SoftQQ864170220.com";
            }
        }

        /// <summary>
        /// 转换为返回客户端对象
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static OrderModel Sheet2Model(EFEntity.OrderSheet sheet)
        {
            OrderModel model = new OrderModel();
            model.Status = "1";
            model.StatusMsg="";
            model.OrderID = sheet.OrderID.ToString();
            model.SheetID = sheet.SheetID;
            model.CategoryID = sheet.CategoryID;
            model.ProductName = sheet.ProductName;
            model.NUM = sheet.NUM.ToString();
            model.OrderTime = sheet.OrderTime.Value.ToString("yyyy-MM-dd HH:mm");
            if (sheet.TicketFailTime != null)
                model.TicketFailTime = sheet.TicketFailTime.Value.ToString("yyyy-MM-dd HH:mm");
            else
                model.TicketFailTime = "";

            model.Phone = sheet.Phone;
            model.RealName = sheet.RealName;

            //发车时间
            if (sheet.StartTime != null)
                model.StartTime = sheet.StartTime.Value.ToString("yyyy-MM-dd HH:mm");
            else
                model.StartTime = "";
            //发车点的
            if (sheet.StartAddress != null)
                model.StartAddress = sheet.StartAddress;
            else
                model.StartAddress = "";

            //游玩时间
            if (sheet.PalyDate != null)
                model.PalyDate = sheet.PalyDate.Value.ToString("yyyy-MM-dd");
            else
                model.PalyDate = "";

            model.OrderStatus = sheet.OrderStatus;
            model.ValidCode = sheet.ValidCode;

            return model;
        }
    }
}