using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using NJiaSu.Libraries;

namespace ETicket.Web.wap
{
    public partial class myshop_2 : System.Web.UI.Page
    {
        public string ticketJosn = "", lineJosn = "",cpName="",cpTel="",headLogo="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                int productID = 0;
                int userID = 0;
                WapHelper.GetUserID_ProductID("q", out userID, out productID);
                if(userID<=0)
                {
                    return;
                }
                //var userID = NJiaSu.Libraries.PubFun.QueryInt("u");
                var userModel = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userID);
                if (userModel==null)
                {
                    return;
                }
                if (userModel.UserCategory != "partner")
                {
                    return;
                }

                //判断是否锁定IP
                if (userModel.QR_IsLock != null && userModel.QR_IsLock == true)
                {
                    string client = PubFun.GetClientIP();
                    LogHelper.LogResult("qr_lock_log", userModel.UserName + "," + userModel.RealName + "客户IP" + client + "设置IP" + userModel.QR_LockIP);
                    if (userModel.QR_LockIP != "")
                    {
                        if (userModel.QR_LockIP != client)
                        {
                            Response.Redirect("ip_lock.html");
                        }
                    }
                }

                cpName = userModel.CPName;
                cpTel = userModel.Tel;

                if(userModel.UserID==387)
                {
                    headLogo = "/wap/cssapp/myshopcss/myImg/xinke.png";
                }
                else
                {
                    headLogo = "/wap/cssapp/myshopcss/myImg/mylogo.png";
                }
                var templ = "{\"goods\": \"{img}\",\"synopsis\": \"{title}\",\"price\": \"￥{price}\",\"price2\": \"￥{price2}\",\"q\": \"{q}\"}";

                //门票
                var listTicket= LoadData("ticket",userModel);
                foreach(var ticketModel in listTicket)
                {
                    var itemStr = templ;
                    itemStr = itemStr.Replace("{img}", ticketModel.TitleImg_S);
                    itemStr = itemStr.Replace("{title}", ticketModel.ProductName);
                    itemStr = itemStr.Replace("{price}", ticketModel.Price.ToString());
                    itemStr = itemStr.Replace("{price2}", ticketModel.PrimeCost.ToString());
                    var qContent = ETicket.Web.HtmlController.CreateWapQContent(userID.ToString(), ticketModel.ProductID.ToString());
                    itemStr = itemStr.Replace("{q}", qContent);

                    if(ticketJosn=="")
                    {
                        ticketJosn = itemStr;
                    }
                    else
                    {
                        ticketJosn = ticketJosn + "," + itemStr;
                    }
                }

                //专线
                var listLine = LoadData("line", userModel);
                foreach (var lineModel in listLine)
                {
                    var itemStr = templ;
                    itemStr = itemStr.Replace("{img}", lineModel.TitleImg_S);
                    itemStr = itemStr.Replace("{title}", lineModel.ProductName);
                    itemStr = itemStr.Replace("{price}", lineModel.Price.ToString());
                    itemStr = itemStr.Replace("{price2}", lineModel.PrimeCost.ToString());

                    var qContent=ETicket.Web.HtmlController.CreateWapQContent(userID.ToString(),lineModel.ProductID.ToString());
                    itemStr = itemStr.Replace("{q}", qContent);

                    if (lineJosn == "")
                    {
                        lineJosn = itemStr;
                    }
                    else
                    {
                        lineJosn = lineJosn + "," + itemStr;
                    }
                }
            }
        }

        List<ETicket.Web.api_h.Product> LoadData(string categoryID,ETicket.EFEntity.User user)
        {
            #region 列表
            int pageIndex =0;
            int pageSize = 100;
            if (pageIndex <= 0)
                pageIndex = 1;
        

            //取数条件
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" it.SaleFlag={0}", true);
            sb.AppendFormat(" and it.CategoryID='{0}'", categoryID);

            ETicket.Utility.PageInfo<EFEntity.Product> pi = null;
            pi = BLL.ProductBLL.Instance.GetPageList(pageIndex, pageSize, sb.ToString(), "it.ProductID DESC");

            List<ETicket.Web.api_h.Product> resultList = new List<ETicket.Web.api_h.Product>();
            //转换为接口Model
            foreach (var proItem in pi.List)
            {
                var resultItem = ETicket.Web.api_h.ApiHelper.Product2Result(proItem, user, false,true);
                resultList.Add(resultItem);
            }

            return resultList;

            #endregion
        }
    }
}