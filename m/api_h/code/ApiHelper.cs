using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    public class ApiHelper
    {
        public static int DefaultPageSize = 20;
        public static string sysMd5 = "21232f297a57a5a743894a0e4a801fc3";

        #region 商品

        public static Product Product2Result(EFEntity.Product inPro, EFEntity.User user, bool isLoadLine)
        {
            return Product2Result(inPro, user, isLoadLine, false);
        }
        public static Product Product2Result(EFEntity.Product inPro, EFEntity.User user, bool isLoadLine,bool isLoadFxPrice)
        {
            Product pro = new Product();

            pro.ProductID = inPro.ProductID;
            pro.CategoryID = inPro.CategoryID;
            pro.ProductName = inPro.ProductName;
            pro.PrimeCost = inPro.PrimeCost == null ? 0 : inPro.PrimeCost.Value;
            pro.RulesNote_WAP = string.IsNullOrEmpty(inPro.RulesNote_WAP) ? "暂无说明" : inPro.RulesNote_WAP.Trim();
            pro.Detail_WAP = string.IsNullOrEmpty(inPro.Detail_WAP) ? "暂无说明" : inPro.Detail_WAP.Trim();



            pro.SaleFlag = inPro.SaleFlag == null ? false : inPro.SaleFlag.Value;
            pro.Tel = inPro.Tel;
            pro.TitleImg_S = PubFun.ServerHost() + "" + inPro.TitleImg_S;
            pro.TitleImg_M = PubFun.ServerHost() + "" + inPro.TitleImg_M;
            pro.TitleImg_B = PubFun.ServerHost() + "" + inPro.TitleImg_B;
            pro.Address = inPro.Address;
            pro.OpenTime = inPro.OpenTime;

            //价格
            pro.Price = ProductPrice(inPro.ProductID,user,isLoadFxPrice);

            if (pro.CategoryID == "line" & isLoadLine)
            {
                //发车时间
                string strStartTime = "";
                var timeList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == inPro.ProductID);
                foreach (var time in timeList)
                {
                    string timeStr = PubFun.FormatHourMintue(time.StartH.ToString()) + ":" + PubFun.FormatHourMintue(time.StartM.ToString());
                    if (strStartTime == "")
                        strStartTime = timeStr;
                    else
                        strStartTime = strStartTime + "，" + timeStr;
                }
                //上车地点
                string strStartAddress = "";
                var addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == inPro.ProductID);
                foreach (var address in addressList)
                {
                    if (strStartAddress == "")
                        strStartAddress = address.Address;
                    else
                        strStartAddress = strStartAddress + "，" + address.Address;
                }

                pro.StartTime = strStartTime;
                pro.StartAddress = strStartAddress;
                if (string.IsNullOrEmpty(pro.StartAddress))
                {
                    pro.StartAddress = "无固定地址，由用户自行选择";
                }

            }


            /*
            string[] imgNote = WachImgSrc(pro.RulesNote_WAP);
            foreach (string img in imgNote)
            {
                if (img.StartsWith("/upfile"))
                {
                    string imgReplace = PubFun.ServerHost() + "" + img;
                    pro.RulesNote_WAP = pro.RulesNote_WAP.Replace(img, imgReplace);
                }
            }
            string[] imgDetail = WachImgSrc(pro.Detail_WAP);
            foreach (string img in imgDetail)
            {
                if (img.StartsWith("/upfile"))
                {
                    string imgReplace = PubFun.ServerHost() + "" + img;
                    pro.Detail_WAP = pro.Detail_WAP.Replace(img, imgReplace);
                }
            }
            */

           string[] imgNote = WachImgSrc2(pro.RulesNote_WAP);
           foreach (string img in imgNote)
           {
               string newImg = ImgNews(img);
               pro.RulesNote_WAP = pro.RulesNote_WAP.Replace(img, newImg);
           }
           string[] imgDetail = WachImgSrc2(pro.Detail_WAP);
           foreach (string img in imgDetail)
           {
               string newImg = ImgNews(img);
               pro.Detail_WAP = pro.Detail_WAP.Replace(img, newImg);
           }

            //转换像素
            pro.RulesNote_WAP = px2em.Convert(pro.RulesNote_WAP);
            pro.Detail_WAP = px2em.Convert(pro.Detail_WAP);

            return pro;
        }

        public static decimal ProductPrice(int productID, EFEntity.User user, bool isLoadFxPrice)
        {
            decimal price = 0;
            int userLevelID = 0;
            if (user.UserID > 0 && (user.UserCategory == "user" || user.UserCategory == "partner"))
            {
                userLevelID = user.UserLevelID.Value;
            }
            else
            {
                var userLevel = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelName == "普通会员");
                userLevelID = userLevel.UserLevelID;
            }

            var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == productID && p.UserLevelID == userLevelID);

            if (isLoadFxPrice)
            {
                //加载分销价格
                EFEntity.FX_Price fxPrice = BLL.FXPriceBLL.Instance.GetEntity(p => p.UserID == user.UserID && p.ProductID == productID);
                 if (fxPrice != null)
                 {
                     price = fxPrice.MyPrice.Value;
                 }
                 else
                 {
                     //没有设置自己的价格，加载推荐价格
                     if (suk != null)
                        price = suk.FXPrice_Recommend.Value;
                 }
            }
            else
            {
                //加载suk价格
                if (suk != null)
                    price = suk.ProductPrice.Value;
            }

            return price;
        }

        #endregion

        #region 订单
        public static Order OrderSheet2Result(EFEntity.OrderSheet inSheet, EFEntity.User user, bool isLoadFull)
        {
            Order order = new Order();

            order.OrderID = inSheet.OrderID;
            order.SheetID = inSheet.SheetID;
            order.ProductID = inSheet.ProductID == null ? 0 : inSheet.ProductID.Value;
            order.CategoryID = inSheet.CategoryID;
            order.ProductName = inSheet.ProductName;
            order.NUM = inSheet.NUM == null ? 0 : inSheet.NUM.Value;
            order.UnitPrice = inSheet.UnitPrice == null ? 0 : inSheet.UnitPrice.Value;
            order.TotalPrice = inSheet.TotalPrice == null ? 0 : inSheet.TotalPrice.Value;
            order.OrderTime = inSheet.OrderTime == null ? "" : inSheet.OrderTime.ToString();
            order.OrderStatus = inSheet.OrderStatus;

            if (order.OrderStatus == "已支付" || order.OrderStatus == "已验票")
            {
                order.ScanKey = PubFun.GetRandString(3) + PubFun.Num2Char(order.OrderID.ToString());
            }

            //加载完整订单信息
            if (isLoadFull)
            {
                order.RebateUnit = inSheet.RebateUnit == null ? 0 : inSheet.RebateUnit.Value;
                order.RebateTotal = inSheet.RebateTotal == null ? 0 : inSheet.RebateTotal.Value;

                order.PayType = inSheet.PayType;
                order.PayTypeSub = inSheet.PayTypeSub;
                order.PayState = inSheet.PayState;
                order.PayFailTime = inSheet.PayFailTime == null ? "" : inSheet.PayFailTime.ToString();
                order.TicketFailTime = inSheet.TicketFailTime == null ? "" : inSheet.TicketFailTime.ToString();
                order.ValidType = inSheet.ValidType;
                order.SMSSendStatus = inSheet.SMSSendStatus;
                order.Phone = inSheet.Phone;
                order.IDCard = inSheet.IDCard;
                order.RealName = inSheet.RealName;
                order.UserID = inSheet.UserID == null ? 0 : inSheet.UserID.Value;
                order.UserName = inSheet.UserName;
                order.UserCategory = inSheet.UserCategory;
                order.UserLevelName = inSheet.UserLevelName;
                order.StartAddress = inSheet.StartAddress;
                order.StartTime = inSheet.StartTime == null ? "" : inSheet.StartTime.ToString();
                order.StartH = inSheet.StartH == null ? 0 : inSheet.StartH.Value;
                order.StartM = inSheet.StartM == null ? 0 : inSheet.StartM.Value;
                order.LastOrderTime = inSheet.LastOrderTime == null ? "" : inSheet.LastOrderTime.ToString();
                order.TickID = inSheet.TickID == null ? 0 : inSheet.TickID.Value;
                order.PalyDate = inSheet.PalyDate == null ? "" : inSheet.PalyDate.Value.ToString("yyyy-MM-dd");

                order.Memo = inSheet.Memo;
                order.ValidTime = inSheet.ValidTime == null ? "" : inSheet.ValidTime.ToString();
                order.EnableValid = inSheet.EnableValid == null ? 0 : inSheet.EnableValid.Value;
                order.EnableValidTime = inSheet.EnableValidTime == null ? "" : inSheet.EnableValidTime.ToString();
                order.ClientType = inSheet.ClientType;


            }
            return order;
        }
        #endregion

        #region 新闻
        public static News News2Result(EFEntity.ArtContent art, bool isLoadContent)
        {
            News news = new News();

            news.ArtID = art.ArtID;
            news.ArtTitle = art.ArtTitle;
            if (isLoadContent)
            {
                news.ArtContent = art.ArtContent1 == null ? "" : art.ArtContent1;

                /*
                string[] imgContent = WachImgSrc(news.ArtContent);
                foreach (string img in imgContent)
                {
                    if (img.StartsWith("/upfile"))
                    {
                        string imgReplace = PubFun.ServerHost() + "" + img;
                        news.ArtContent = news.ArtContent.Replace(img, imgReplace);
                    }
                }
                 */

                string[] imgContent = WachImgSrc2(news.ArtContent);
                foreach (string img in imgContent)
                {
                    string newImg = ImgNews(img);
                    news.ArtContent = news.ArtContent.Replace(img, newImg);
                }

                news.ArtContent = px2em.Convert(news.ArtContent);

            }

            news.ModuleName = "";
            news.AddTime = art.AddTime == null ? "" : art.AddTime.ToString();



            return news;
        }
        #endregion

        #region 积分账号
        public static Account AccountLog2Result(EFEntity.AccountLog log)
        {
            Account acc = new Account();

            acc.PKID = log.PKID;
            acc.UserID = log.UserID == null ? 0 : log.UserID.Value;

            if (log.ActType == "in")
                acc.ActType = "收入";
            else if (log.ActType == "out")
                acc.ActType = "支出";

            acc.ActAmount = log.ActAmount == null ? 0 : log.ActAmount.Value;
            acc.ActTime = log.ActTime == null ? "" : log.ActTime.ToString();
            acc.Memo = log.Memo;
            acc.FormType = log.FormType;
            acc.FormOrderID = log.FormOrderID == null ? "" : log.FormOrderID.ToString();

            return acc;
        }
        #endregion

        public static string WashOS(string inStr)
        {
            string outStr = inStr.Replace("\"", "").Replace(":", "").Replace("os", "").Replace("{", "").Replace("}", "");
            return outStr;
        }

        public static string[] WachImgSrc(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            return sUrlList;
        }
        public static string[] WachImgSrc2(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList[i++] = match.Value;

            return sUrlList;
        }
        public static string ImgNews(string inImg)
        {
            StringBuilder sb = new StringBuilder();
            string[] imgs = WachImgSrc(inImg);
            if (imgs.Length == 1)
            {
                string img = imgs[0];
                if (img.StartsWith("/upfile"))
                {
                    return string.Format("<img src=\"{0}\" style=\"width:100%;\" />", PubFun.ServerHost() + ""+img);
                }
                else
                {
                    return string.Format("<img src=\"{0}\" style=\"width:100%;\" />", img);
                }
            }
            else
            {
                return inImg;
            }
        }


    }
}