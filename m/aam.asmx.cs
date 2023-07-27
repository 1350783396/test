using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Text;
//using System.Runtime.Serialization.Json;
using System.IO;
using NJiaSu.Libraries;

namespace ETicket.Web
{
    /// <summary>
    /// aam 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class aam : System.Web.Services.WebService
    {


        [WebMethod]
        public string GetOrder(string name)
        {
            try
            {
                if (name != "shj@123321_sms_temp")
                    return "";

                NJiaSu.Libraries.LogHelper.LogSMS.Info("短信下载IP：" + PubFun.GetClientIP());

                IEnumerable<EFEntity.SMS_Send_Order> list = BLL.SMSSendOrderBLL.Instance.GetEntities(p => p.SendStatus == "发送中" & p.SendIsRead != true).Take(20).OrderByDescending(p => p.AddTime);

                string ss = "";
                foreach (var sms in list)
                {
                    sms.SendIsRead = true;
                    sms.SendNum += 1;
                    BLL.SMSSendOrderBLL.Instance.UpdateObject(sms);

                    string s = "o_" + sms.OrderID + "$" + sms.Phone + "$" + sms.SMSContent;
                    if (ss == "")
                        ss = s;
                    else
                        ss = ss + "|" + s;
                }

                return ss;
            }
            catch
            {
                return "";
            }
        }
        [WebMethod]
        public string GetContent(string name)
        {
            try
            {
                if (name != "shj@123321_sms_temp")
                    return "";

                IEnumerable<EFEntity.SMS_Send_Content> list = BLL.SMSSendContentBLL.Instance.GetEntities(p => p.SendStatus == "发送中" & p.SendIsRead != true).Take(20).OrderByDescending(p => p.AddTime);

                string ss = "";
                foreach (var sms in list)
                {
                    sms.SendIsRead = true;
                    sms.SendNum += 1;
                    BLL.SMSSendContentBLL.Instance.UpdateObject(sms);

                    string s = "c_" + sms.PKID + "$" + sms.Phone + "$" + sms.SMSContent;
                    if (ss == "")
                        ss = s;
                    else
                        ss = ss + "|" + s;
                }

                return ss;
            }
            catch
            {
                return "";
            }
        }
        [WebMethod]
        public string Set(string name, string pkid, string status)
        {
            try
            {
                if (!name.Contains("shj@123321_sms_temp"))
                    return "0";

                if (pkid.Contains("o"))
                {
                    #region 订单短信
                    try
                    {
                        int orderID = int.Parse(pkid.Replace("o_", ""));

                        EFEntity.SMS_Send_Order model = BLL.SMSSendOrderBLL.Instance.GetEntity(p => p.OrderID == orderID);
                        //if (model.SendStatus != ETicket.Utility.SMSSendStatusEnum.发送成功.ToString())
                        //{ }
                        if (status == "发送失败")
                        {
                            model.SendStatus = ETicket.Utility.SMSSendStatusEnum.发送失败.ToString();
                        }
                        else if (status == "发送成功")
                        {
                            model.SendStatus = ETicket.Utility.SMSSendStatusEnum.发送成功.ToString();
                        }
                        BLL.SMSSendOrderBLL.Instance.UpdateObject(model);

                        return "1";
                    }
                    catch
                    {
                        return "0";
                    }
                    #endregion
                }
                else if (pkid.Contains("c"))
                {
                    #region 内容短信
                    try
                    {
                        int id = int.Parse(pkid.Replace("c_", ""));

                        EFEntity.SMS_Send_Content model = BLL.SMSSendContentBLL.Instance.GetEntity(p => p.PKID == id);
                        //if (model.SendStatus != ETicket.Utility.SMSSendStatusEnum.发送成功.ToString())
                        //{ }
                        if (status == "发送失败")
                        {
                            model.SendStatus = ETicket.Utility.SMSSendStatusEnum.发送失败.ToString();
                        }
                        else if (status == "发送成功")
                        {
                            model.SendStatus = ETicket.Utility.SMSSendStatusEnum.发送成功.ToString();
                        }
                        BLL.SMSSendContentBLL.Instance.UpdateObject(model);

                        return "1";
                    }
                    catch
                    {
                        return "0";
                    }
                    #endregion
                }
                else
                {
                    return "0";
                }
            }
            catch
            {
                return "0";
            }
        }
    }
}
