using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using NJiaSu.Libraries;
using System.IO;

using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;



namespace ETicket.Web
{
    public class HtmlController
    {
        #region 实例本身
        private static HtmlController _Instance;

        /// <summary>
        /// 获取静态对象(单件模式)
        /// </summary>
        public static HtmlController Instance
        {
            get
            {
                if (_Instance != null) return _Instance;
                _Instance = new HtmlController();
                return _Instance;
            }
        }
        #endregion

        #region 下单
        public string LineOrderHtml(IEnumerable<EFEntity.Product> productList)
        {
            string urlTempl = PubFun.ApplicationPath + "/shopcar.aspx?id={0}";

            StringBuilder sb = new StringBuilder();
            string itemTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/line-order-item.html"));
            foreach (var product in productList)
            {
                string itemHtml = itemTempl;

                itemHtml = itemHtml.Replace("{ProductName}", product.ProductName);
                itemHtml = itemHtml.Replace("{url}", string.Format(urlTempl, product.ProductID));

                //价格

                /*decimal price = 0;
                EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID);
                var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == user.UserLevelID);
                if (suk != null)
                    price = suk.ProductPrice.Value;
                itemHtml = itemHtml.Replace("{Price}", price.ToString());*/

                var priceStr = product.PrimeCost == null ? "未知" : product.PrimeCost.ToString();
                itemHtml = itemHtml.Replace("{Price}", priceStr);

                //发车时间
                string strStartTime = "";
                var timeList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                foreach (var time in timeList)
                {
                    string timeStr = PubFun.FormatHourMintue(time.StartH.ToString()) + ":" + PubFun.FormatHourMintue(time.StartM.ToString());
                    if (strStartTime == "")
                        strStartTime = timeStr;
                    else
                        strStartTime = strStartTime + "，" + timeStr;
                }
                //strStartTime = strStartTime + strStartTime + strStartTime + strStartTime + strStartTime;
                if (strStartTime.Length > 50)
                    strStartTime = strStartTime.Substring(0, 50) + "......";

                itemHtml = itemHtml.Replace("{StartTime}", strStartTime);
                //上车地点
                string strStartAddress = "";
                var addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                foreach (var address in addressList)
                {
                    if (strStartAddress == "")
                        strStartAddress = address.Address;
                    else
                        strStartAddress = strStartAddress + "，" + address.Address;
                }
                //strStartAddress = strStartAddress + strStartAddress + strStartAddress + strStartAddress + strStartAddress + strStartAddress;
                if (strStartAddress == "")
                    strStartAddress = "下单时由客人录入";

                if (strStartAddress.Length > 40)
                    strStartAddress = strStartAddress.Substring(0, 40) + "......";
                itemHtml = itemHtml.Replace("{StartAddress}", strStartAddress);
                sb.Append(itemHtml);
            }

            return sb.ToString();

        }

        public string TickOrderHtml(IEnumerable<EFEntity.Product> productList)
        {
            string urlTempl = PubFun.ApplicationPath + "/shopcar.aspx?id={0}";

            StringBuilder sb = new StringBuilder();
            string itemHtmlTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/ticket-order-item.html"));

            foreach (var product in productList)
            {
                string itemHtml = itemHtmlTempl;
                string url = string.Format(urlTempl, product.ProductID);

                //价格
                /*
                decimal price = 0;
                EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID);
                var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == user.UserLevelID);
                if (suk != null)
                    price = suk.ProductPrice.Value;
                itemHtml = itemHtml.Replace("{Price}", price.ToString());
                */

                var priceStr = product.PrimeCost == null ? "未知" : product.PrimeCost.ToString();
                itemHtml = itemHtml.Replace("{Price}", priceStr);

                itemHtml = itemHtml.Replace("{ProductName}", product.ProductName);
               
                itemHtml = itemHtml.Replace("{Tel}", product.Tel);
                string memo = product.Memo;

                if (!string.IsNullOrEmpty(memo) && memo.Length > 30)
                    memo = memo.Substring(0, 30) + "........";
                itemHtml = itemHtml.Replace("{Memo}", memo);
                itemHtml = itemHtml.Replace("{url}", url);

                sb.Append(itemHtml);
            }

            return sb.ToString();
        }
        #endregion

        #region 我的分销二维码
        public string MyQRCode_Html(IEnumerable<EFEntity.Product> productList)
        {
            string urlTempl = PubFun.ApplicationPath + "/business/partner/myqr_code_down.aspx?c={0}&n={1}";

            StringBuilder sb = new StringBuilder();
            string itemTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/myqrcofe-item.html"));
            foreach (var product in productList)
            {
                string itemHtml = itemTempl;

                itemHtml = itemHtml.Replace("{ProductName}", product.ProductName);
                

                //价格
                decimal myPrice = 0,  suggestPrice = 0;
                EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID);

                var productID = product.ProductID;
                //我的价格
                var myModel = BLL.FXPriceBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID && p.ProductID == productID);
                if (myModel == null)
                {
                    //suk建议价格
                    var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == productID && p.UserLevelID == user.UserLevelID);
                    if (suk != null)
                    {
                        var price = suk.ProductPrice.Value;
                        suggestPrice = suk.FXPrice_Recommend == null ? price + 10 : suk.FXPrice_Recommend.Value;
                        myPrice = suggestPrice;
                    }
                }
                else
                {
                    myPrice = myModel.MyPrice.Value;
                }
                itemHtml = itemHtml.Replace("{MyPrice}", myPrice.ToString());
                //市场价
                var priceStr = product.PrimeCost == null ? "未知" : product.PrimeCost.ToString();
                itemHtml = itemHtml.Replace("{Price}", priceStr);

                //生成二维码
                //string webhost = "http://www.tianshuntour.com";
                //string webhost = "http://192.168.2.105";
                string webhost = PubFun.ServerHost();
                var imgPath= CreateFXQRCode(webhost, user.UserID.ToString(), product.ProductID.ToString());

                itemHtml = itemHtml.Replace("{MyQRCodePath}", imgPath);

                //下载路径
                var enProductName= HttpContext.Current.Server.UrlEncode(product.ProductName);
                var fileNameNoPath=System.IO.Path.GetFileNameWithoutExtension(imgPath);
                itemHtml = itemHtml.Replace("{url}", string.Format(urlTempl, enProductName, fileNameNoPath));

                sb.Append(itemHtml);
            }

            return sb.ToString();

        }

        //生成二维码
        public string CreateFXQRCode(string webhost,string userID,string productID)
        {
           
            //生成图片的唯一ID
            var imgName = NJiaSu.Libraries.Encrypt.GetMd5Hash("XinKe_CreateFXQRCode_" + userID + productID + webhost);
            string pPath = HttpContext.Current.Server.MapPath("/tempfile/");
            string savePath = pPath + imgName + ".jpg";
            string viewPath = "/tempfile/" + imgName + ".jpg";
            try
            {
                if (File.Exists(savePath))
                {
                    return viewPath;
                }
                else
                {
                    //var qrContentStr ="http://www.tianshuntour.com/wap_view_{0}.html";
                    var qrContentStr = webhost + "/wap_view_{0}.html";


                    var qContent = CreateWapQContent(userID, productID);
                    qrContentStr = string.Format(qrContentStr, qContent);

                    Bitmap image = QRCodeEncoderUtil(qrContentStr);
                    image.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return viewPath;
                }  
            }
            catch(Exception ex)
            {
                NJiaSu.Libraries.LogHelper.LogResult("error", ex.ToString());
                return "";
            }
            //image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
       }

        public string CreateMyShopQRCode(string webhost,string userID)
        {
            //生成图片的唯一ID
            var imgName = NJiaSu.Libraries.Encrypt.GetMd5Hash("XinKe_CreateFXQRCode_" + userID + "0" + webhost);
            string pPath = HttpContext.Current.Server.MapPath("/tempfile/");
            string savePath = pPath + imgName + ".jpg";
            string viewPath = "/tempfile/" + imgName + ".jpg";
            try
            {
                if (File.Exists(savePath))
                {
                    return viewPath;
                }
                else
                {
                   
                    var qrContentStr = webhost + "/wx_myshop_{0}.html";

                    var qContent = CreateWapQContent(userID, "0");
                    qrContentStr = string.Format(qrContentStr, qContent);

                    Bitmap image = QRCodeEncoderUtil(qrContentStr);
                    image.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return viewPath;
                }
            }
            catch (Exception ex)
            {
                NJiaSu.Libraries.LogHelper.LogResult("error", ex.ToString());
                return "";
            }
        }
        public static string CreateWapQContent(string userID, string productID)
        {
            var md5 = NJiaSu.Libraries.Encrypt.GetMd5Hash("XKRQ_View_" + userID + productID);
            var qContent = md5 + "_" + userID + "_" + productID;
            return qContent;
        }

        public static Bitmap QRCodeEncoderUtil(string qrCodeContent)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeVersion = 0;
            Bitmap img = qrCodeEncoder.Encode(qrCodeContent, Encoding.UTF8);//指定utf-8编码， 支持中文
            return img;
        }

        #endregion

        #region 购物车
        /// <summary>
        /// 购物车
        /// </summary>
        /// <returns></returns>
        public string Car()
        {
            EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
            if (cookiesUser == null)
            {

            }
            int userID = cookiesUser.UserID;
            //用户
            var userModel = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userID);
            decimal account = 0;//积分
            if (userModel.Account != null)
                account = userModel.Account.Value;

            string carTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car/car.html"));
            string lineColTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car/line-col.html"));
            string ticketTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car/ticket-col.html"));

            int productID = PubFun.QueryInt("id");
            //int tickID = PubFun.QueryInt("tick");
            
            if (productID < 0)
            {
                //产品不存在
                HttpContext.Current.Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.product_noexits.GetHashCode());
            }
            EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID && p.SaleFlag == true);
            if (product == null)
            {
                //产品不存在
                HttpContext.Current.Response.Redirect(PubFun.ApplicationPath + "/msgpage.aspx?t=" + ETicket.Utility.ErrorMsgEnum.product_noexits.GetHashCode());
            }
            string _propertiesHtml = GetPropStr(productID.ToString());
            //用户积分
            carTempl = carTempl.Replace("{Account}", account.ToString());
            carTempl = carTempl.Replace("{Properties}", _propertiesHtml.ToString());
            //产品信息
            carTempl = carTempl.Replace("{ProductName}", product.ProductName);
            carTempl = carTempl.Replace("{RulesNote}", product.RulesNote);
            carTempl = carTempl.Replace("{Detail}", product.Detail);

            #region 专线、景区区别

            string lineCol = "";
            if (product.CategoryID == "line")
            {
                //地址
                string addressHtml = "";
                var addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == productID);
                if (addressList == null || addressList.Count<EFEntity.ProductAddress>() == 0)
                {
                    addressHtml = "<input type='text' id=\"startAddress\" name=\"startAddress\"/>";
                }
                else
                {
                    addressHtml = addressHtml + "<select id=\"startAddress\" name=\"startAddress\"><option value=\"\">请选择</option>";
                    foreach (var address in addressList)
                    {
                        addressHtml = addressHtml + string.Format("<option value=\"{0}\">{0}</option>", address.Address);
                    }
                    addressHtml = addressHtml + "</select>";
                }
                lineColTempl = lineColTempl.Replace("{address}", addressHtml);

                //销售时间
                string timeOption = "";
                var timeList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == productID);
                foreach (var time in timeList)
                {
                    timeOption = timeOption + string.Format("<option value=\"{0}\">{0}</option>", PubFun.FormatHourMintue(time.StartH.ToString()) + ":" + PubFun.FormatHourMintue(time.StartM.ToString()));
                }
                lineColTempl = lineColTempl.Replace("{startTime}", timeOption);
                //销售日期
                ///----------最近能下单的日期-------------
                DateTime miniDate = DateTime.Now;
                var maxTime = BLL.ProductSaleTimeBLL.Instance.GetMaxTime(timeList);
                if (maxTime != null)
                {
                    string strToday = DateTime.Now.ToString("yyyy-MM-dd");
                    DateTime todayLast = Convert.ToDateTime(strToday + " " + maxTime.StartH + ":" + maxTime.StartM);
                    if (todayLast < miniDate)
                        miniDate = miniDate.AddDays(1);
                }
                //------------最多可预定多少天-------------
                int maxOrderNum = 15;
                string configKey = ETicket.Utility.ConfigKeyEnum.order_day_max.ToString();
                var configModel = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == configKey);
                if (configModel != null)
                {
                    int.TryParse(configModel.ConfigValue, out maxOrderNum);
                }
                string dateOption = "";
                for (int i = 0; i < maxOrderNum; i++)
                {
                    dateOption = dateOption + string.Format("<option value=\"{0}\">{0}</option>", miniDate.AddDays(i).ToString("yyyy-MM-dd"));
                }
                lineColTempl = lineColTempl.Replace("{startDate}", dateOption);

                lineCol = lineColTempl;
            }
            else
            {
                //stock = product.Stock.Value;
                //------------游览日期---最多可预定多少天-------------
                int maxOrderNum = 15;
                string configKey = ETicket.Utility.ConfigKeyEnum.order_day_max.ToString();
                var configModel = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == configKey);
                if (configModel != null)
                {
                    int.TryParse(configModel.ConfigValue, out maxOrderNum);
                }
                string dateOption = "";
                for (int i = 0; i < maxOrderNum; i++)
                {
                    dateOption = dateOption + string.Format("<option value=\"{0}\">{0}</option>", DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                }
                ticketTempl = ticketTempl.Replace("{startDate}", dateOption);

                lineCol = ticketTempl;
            }
            carTempl = carTempl.Replace("{line-col}", lineCol);

            #endregion

            //库存
            carTempl = carTempl.Replace("{stock}", "有票");

            //单价
            EFEntity.ProductSUK suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == productID && p.UserLevelID == userModel.UserLevelID.Value);
            if (suk != null)
            {
                carTempl = carTempl.Replace("{unitPrice}", suk.ProductPrice.ToString());
            }
            //返现积分
            decimal rebateUnit = suk.Rebate == null ? 0 : suk.Rebate.Value;
            carTempl = carTempl.Replace("{rebateUnit}", rebateUnit.ToString());

            //支付方式
            if (userModel.UserCategory == "partner"|| userModel.UserCategory == "operator")
            {
                string payList = "";
                if (userModel.EnableOnlinePay.Value)
                    payList = payList + "<option  value=\"在线支付\">在线支付</option>";
                if (userModel.EnableAccountPay.Value)
                    payList = payList + " <option value=\"积分支付\">积分支付</option>";
                if (userModel.EnableCashPay.Value)
                    payList = payList + "<option value=\"现金支付\">现金支付</option>";

                carTempl = carTempl.Replace("{payType}", payList);
            }
            else
            {
                carTempl = carTempl.Replace("{payType}", "<option value=\"在线支付\">在线支付</option>");
            }
            //取票方式
            if (userModel.UserCategory == "partner")
            {
                string validType = "";
                validType = validType + "<option selected value=\"二维码\">二维码</option>";
                //validType = validType + "<option value=\"身份证\">身份证</option>";
                validType = validType + "<option value=\"纸质\">纸质</option>";
                carTempl = carTempl.Replace("{selValidType}", validType);
            }
            else
            {
                string validType = "";
                validType = validType + "<option selected value=\"二维码\">二维码</option>";
                carTempl = carTempl.Replace("{selValidType}", validType);
            }
            return carTempl;
        }
        public string GetPropStr(string id)
        {
            int _id = int.Parse(id);
            string ret = "",ppid="", ppidStr="";
            int k = 1;
            IEnumerable<EFEntity.vProperties2> propertiesList = BLL.vProperties2BLL.Instance.GetEntities(p => p.ProductID == _id);
            IEnumerable<EFEntity.vProperties2> list1 = propertiesList.Distinct().ToList();
            List<string> list = new List<string>();
            list = propertiesList.Select(p => p.PName).ToList();  //只取name字段，重新生成新的List集合
            list = list.Distinct().ToList();
            foreach (var p1 in list)
            {
                ret += PropStr(propertiesList, p1.ToString(), k,out ppid);
                ppidStr = ppidStr + "&" + ppid;
                k++;
            }
            if(ppidStr.Substring(0,1)=="&")
            {
                ppidStr = ppidStr.Substring(1, ppidStr.Length - 1);
            }
            ret = ret + "<input type=\"hidden\"  name=\"Properties\" id=\"Properties\"/><input type=\"hidden\" name=\"Pcount\" id=\"Pcount\" value=\""+ k.ToString()+ "\"/><input type=\"hidden\" name=\"PidStr\" id=\"PidStr\" value=\"" + ppidStr+ "\"/>";
            return ret;
        }
        public string PropStr(IEnumerable<EFEntity.vProperties2> vp, string name, int k,out string pidStr)
        {
            string myRet ="<div>"+name + ":";
            pidStr = k.ToString()+ "%"+name+"$";
            foreach (var vp1 in vp)
            {
                if (vp1.PName == name)
                {
                    myRet += "<a id=\"ppid_"+ vp1.ID + "\" onclick=\"ChangePro('c" + k.ToString() + "'," + vp1.ID + ")\">" + vp1.Name.ToString() + "</a>";
                    pidStr += vp1.Pid.ToString() + "|" + vp1.ID.ToString()+"*";
                }
            }
            myRet = myRet + "</div>";
            return myRet;
        }

        public string CarApp(EFEntity.User userModel, EFEntity.Product product)
        {
            string carTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car_h/car.html"));
            string lineColTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car_h/line-col.html"));
            string ticketTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car_h/ticket-col.html"));
            
            decimal account = 0;//积分
            if (userModel.Account != null)
                account = userModel.Account.Value;

            //用户积分
            carTempl = carTempl.Replace("{Account}", account.ToString());

            //产品信息
            carTempl = carTempl.Replace("{ProductName}", product.ProductName);
            carTempl = carTempl.Replace("{RulesNote}", product.RulesNote);
            carTempl = carTempl.Replace("{Detail}", product.Detail);
            carTempl = carTempl.Replace("{ProductID}", product.ProductID.ToString());
            
            #region 专线、景区区别

            string lineCol = "";
            if (product.CategoryID == "line")
            {
                //上车地点
                string addressHtml = "";
                var addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                if (addressList == null || addressList.Count<EFEntity.ProductAddress>() == 0)
                {
                    addressHtml = "<div class=\"uinput uinn4\">" +
                                        "<input value=\"\" type=\"text\" class=\"tx-r\" id=\"startAddress\" name=\"startAddress\"  placeholder=\"必填，上车地点\"/>" +
                                  "</div>";
                }
                else
                {
                    addressHtml = "<div class=\"select uba bc-border bc-text\" id=\"divStartAddress\">" +
                                    "<div class=\"text\">" +
                                        "{startAddressFrist}" +
                                    "</div>" +
                                    "<div class=\"icon\"></div>" +
                                    "<select selectedindex=\"0\" id=\"startAddress\">" +
                                        "{startAddress}" +
                                    "</select>" +
                                "</div>";

                    string addressOption ="";
                    string addressFrsit = "";
                    int addressNum = 0;
                    foreach (var address in addressList)
                    {
                        addressOption = addressOption + string.Format("<option value=\"{0}\">{0}</option>", address.Address);
                        if (addressNum == 0)
                            addressFrsit = address.Address;
                        addressNum++;
                    }
                    addressHtml = addressHtml.Replace("{startAddressFrist}", addressFrsit);
                    addressHtml = addressHtml.Replace("{startAddress}", addressOption);
                }
                lineColTempl = lineColTempl.Replace("{address}", addressHtml);

                //销售时间
                string timeOption = "";
                string timeFrist = "";
                int timeNum = 0;
                var timeList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                foreach (var time in timeList)
                {
                    timeOption = timeOption + string.Format("<option value=\"{0}\">{0}</option>", PubFun.FormatHourMintue(time.StartH.ToString()) + ":" + PubFun.FormatHourMintue(time.StartM.ToString()));
                    if (timeNum == 0)
                        timeFrist = PubFun.FormatHourMintue(time.StartH.ToString()) + ":" + PubFun.FormatHourMintue(time.StartM.ToString());
                    timeNum++;
                }
                lineColTempl = lineColTempl.Replace("{startTimeFrist}", timeFrist);
                lineColTempl = lineColTempl.Replace("{startTime}", timeOption);

                //销售日期
                ///----------最近能下单的日期-------------
                DateTime miniDate = DateTime.Now;
                var maxTime = BLL.ProductSaleTimeBLL.Instance.GetMaxTime(timeList);
                if (maxTime != null)
                {
                    string strToday = DateTime.Now.ToString("yyyy-MM-dd");
                    DateTime todayLast = Convert.ToDateTime(strToday + " " + maxTime.StartH + ":" + maxTime.StartM);
                    if (todayLast < miniDate)
                        miniDate = miniDate.AddDays(1);
                }
                //------------最多可预定多少天-------------
                int maxOrderNum = 15;
                string configKey = ETicket.Utility.ConfigKeyEnum.order_day_max.ToString();
                var configModel = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == configKey);
                if (configModel != null)
                {
                    int.TryParse(configModel.ConfigValue, out maxOrderNum);
                }
                string dateOption = "";
                string dateFrist = "";
                int dateNum = 0;
                for (int i = 0; i < maxOrderNum; i++)
                {
                    dateOption = dateOption + string.Format("<option value=\"{0}\">{0}</option>", miniDate.AddDays(i).ToString("yyyy-MM-dd"));
                    if(dateNum==0)
                        dateFrist =  miniDate.AddDays(i).ToString("yyyy-MM-dd");
                    dateNum++;
                }
                lineColTempl = lineColTempl.Replace("{startDateFrist}", dateFrist);
                lineColTempl = lineColTempl.Replace("{startDate}", dateOption);

                lineCol = lineColTempl;
            }
            else
            {
                //stock = product.Stock.Value;
                //------------游览日期---最多可预定多少天-------------
                int maxOrderNum = 15;
                string configKey = ETicket.Utility.ConfigKeyEnum.order_day_max.ToString();
                var configModel = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == configKey);
                if (configModel != null)
                {
                    int.TryParse(configModel.ConfigValue, out maxOrderNum);
                }
                string dateOption = "";
                string startDateFrist = "";
                for (int i = 0; i < maxOrderNum; i++)
                {
                    dateOption = dateOption + string.Format("<option value=\"{0}\">{0}</option>", DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    if (i == 0)
                        startDateFrist = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                }
                ticketTempl = ticketTempl.Replace("{startDateFrist}", startDateFrist);
                ticketTempl = ticketTempl.Replace("{startDate}", dateOption);

                lineCol = ticketTempl;
            }
            carTempl = carTempl.Replace("{line-col}", lineCol);

            #endregion

            //库存
            carTempl = carTempl.Replace("{stock}", "有票");

            //单价
            EFEntity.ProductSUK suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == userModel.UserLevelID.Value);
            if (suk != null)
            {
                carTempl = carTempl.Replace("{unitPrice}", suk.ProductPrice.ToString());
            }
            //返现积分
            decimal rebateUnit = suk.Rebate == null ? 0 : suk.Rebate.Value;
            carTempl = carTempl.Replace("{rebateUnit}", rebateUnit.ToString());

            //支付方式
            if (userModel.UserCategory == "partner")
            {
                string payList = "";
                string payTypeFrist = "";
                if (userModel.EnableOnlinePay.Value)
                {
                    payList = payList + "<option  value=\"在线支付\">在线支付</option>";
                    if (payTypeFrist=="")
                        payTypeFrist = "在线支付";
                }
  
                if (userModel.EnableAccountPay.Value)
                {
                    payList = payList + " <option value=\"积分支付\">积分支付</option>";
                    if (payTypeFrist == "")
                        payTypeFrist = "积分支付";
                }
                //if (userModel.EnableCashPay.Value)
                //    payList = payList + "<option value=\"现金支付\">现金支付</option>";
                carTempl = carTempl.Replace("{payTypeFrist}", payTypeFrist);
                carTempl = carTempl.Replace("{payType}", payList);
            }
            else
            {
                carTempl = carTempl.Replace("{payType}", "<option value=\"在线支付\">在线支付</option>");
                carTempl = carTempl.Replace("{payTypeFrist}", "在线支付");
            }
            //取票方式
            if (userModel.UserCategory == "partner")
            {
                string validType = "";
                validType = validType + "<option selected value=\"二维码\">二维码</option>";
                //validType = validType + "<option value=\"身份证\">身份证</option>";
                //validType = validType + "<option value=\"纸质\">纸质</option>";
                //carTempl = carTempl.Replace("{selValidType}", validType);
            }
            else
            {
                string validType = "";
                validType = validType + "<option selected value=\"二维码\">二维码</option>";
                //carTempl = carTempl.Replace("{selValidType}", validType);
            }
            return carTempl;
        }

        public string CarWap(EFEntity.User userModel, EFEntity.Product product)
        {
            string carTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car_h/car_wap.html"));
            string lineColTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car_h/line-col.html"));
            string ticketTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car_h/ticket-col.html"));

            decimal account = 0;//积分
            if (userModel.Account != null)
                account = userModel.Account.Value;

            //用户积分
            //carTempl = carTempl.Replace("{Account}", account.ToString());
            carTempl = carTempl.Replace("{Account}", "0");

            //产品信息
            carTempl = carTempl.Replace("{ProductName}", product.ProductName);
            carTempl = carTempl.Replace("{RulesNote}", product.RulesNote);
            carTempl = carTempl.Replace("{Detail}", product.Detail);
            carTempl = carTempl.Replace("{ProductID}", product.ProductID.ToString());

            #region 专线、景区区别

            string lineCol = "";
            if (product.CategoryID == "line")
            {
                //上车地点
                string addressHtml = "";
                var addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                if (addressList == null || addressList.Count<EFEntity.ProductAddress>() == 0)
                {
                    addressHtml = "<div class=\"uinput uinn4\">" +
                                        "<input value=\"\" type=\"text\" class=\"tx-r\" id=\"startAddress\" name=\"startAddress\"  placeholder=\"必填，上车地点\"/>" +
                                  "</div>";
                }
                else
                {
                    addressHtml = "<div class=\"select uba bc-border bc-text\" id=\"divStartAddress\">" +
                                    "<div class=\"text\">" +
                                        "{startAddressFrist}" +
                                    "</div>" +
                                    "<div class=\"icon\"></div>" +
                                    "<select selectedindex=\"0\" id=\"startAddress\">" +
                                        "{startAddress}" +
                                    "</select>" +
                                "</div>";

                    string addressOption = "";
                    string addressFrsit = "";
                    int addressNum = 0;
                    foreach (var address in addressList)
                    {
                        addressOption = addressOption + string.Format("<option value=\"{0}\">{0}</option>", address.Address);
                        if (addressNum == 0)
                            addressFrsit = address.Address;
                        addressNum++;
                    }
                    addressHtml = addressHtml.Replace("{startAddressFrist}", addressFrsit);
                    addressHtml = addressHtml.Replace("{startAddress}", addressOption);
                }
                lineColTempl = lineColTempl.Replace("{address}", addressHtml);

                //销售时间
                string timeOption = "";
                string timeFrist = "";
                int timeNum = 0;
                var timeList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                foreach (var time in timeList)
                {
                    timeOption = timeOption + string.Format("<option value=\"{0}\">{0}</option>", PubFun.FormatHourMintue(time.StartH.ToString()) + ":" + PubFun.FormatHourMintue(time.StartM.ToString()));
                    if (timeNum == 0)
                        timeFrist = PubFun.FormatHourMintue(time.StartH.ToString()) + ":" + PubFun.FormatHourMintue(time.StartM.ToString());
                    timeNum++;
                }
                lineColTempl = lineColTempl.Replace("{startTimeFrist}", timeFrist);
                lineColTempl = lineColTempl.Replace("{startTime}", timeOption);

                //销售日期
                ///----------最近能下单的日期-------------
                DateTime miniDate = DateTime.Now;
                var maxTime = BLL.ProductSaleTimeBLL.Instance.GetMaxTime(timeList);
                if (maxTime != null)
                {
                    string strToday = DateTime.Now.ToString("yyyy-MM-dd");
                    DateTime todayLast = Convert.ToDateTime(strToday + " " + maxTime.StartH + ":" + maxTime.StartM);
                    if (todayLast < miniDate)
                        miniDate = miniDate.AddDays(1);
                }
                //------------最多可预定多少天-------------
                int maxOrderNum = 15;
                string configKey = ETicket.Utility.ConfigKeyEnum.order_day_max.ToString();
                var configModel = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == configKey);
                if (configModel != null)
                {
                    int.TryParse(configModel.ConfigValue, out maxOrderNum);
                }
                string dateOption = "";
                string dateFrist = "";
                int dateNum = 0;
                for (int i = 0; i < maxOrderNum; i++)
                {
                    dateOption = dateOption + string.Format("<option value=\"{0}\">{0}</option>", miniDate.AddDays(i).ToString("yyyy-MM-dd"));
                    if (dateNum == 0)
                        dateFrist = miniDate.AddDays(i).ToString("yyyy-MM-dd");
                    dateNum++;
                }
                lineColTempl = lineColTempl.Replace("{startDateFrist}", dateFrist);
                lineColTempl = lineColTempl.Replace("{startDate}", dateOption);

                lineCol = lineColTempl;
            }
            else
            {
                //stock = product.Stock.Value;
                //------------游览日期---最多可预定多少天-------------
                int maxOrderNum = 15;
                string configKey = ETicket.Utility.ConfigKeyEnum.order_day_max.ToString();
                var configModel = BLL.SysConfigBLL.Instance.GetEntity(p => p.ConfigKey == configKey);
                if (configModel != null)
                {
                    int.TryParse(configModel.ConfigValue, out maxOrderNum);
                }
                string dateOption = "";
                string startDateFrist = "";
                for (int i = 0; i < maxOrderNum; i++)
                {
                    dateOption = dateOption + string.Format("<option value=\"{0}\">{0}</option>", DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    if (i == 0)
                        startDateFrist = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                }
                ticketTempl = ticketTempl.Replace("{startDateFrist}", startDateFrist);
                ticketTempl = ticketTempl.Replace("{startDate}", dateOption);

                lineCol = ticketTempl;
            }
            carTempl = carTempl.Replace("{line-col}", lineCol);

            #endregion

            //库存
            carTempl = carTempl.Replace("{stock}", "有票");

            //单价
            EFEntity.ProductSUK suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == userModel.UserLevelID.Value);
            EFEntity.FX_Price fxPrice = BLL.FXPriceBLL.Instance.GetEntity(p => p.UserID == userModel.UserID && p.ProductID == product.ProductID);
            if (fxPrice!=null)
            {
                carTempl = carTempl.Replace("{unitPrice}", fxPrice.MyPrice.ToString());
            }
            else if (suk != null)
            {
                carTempl = carTempl.Replace("{unitPrice}", suk.FXPrice_Recommend.ToString());
            }
            //返现积分
            decimal rebateUnit = suk.Rebate == null ? 0 : suk.Rebate.Value;
            carTempl = carTempl.Replace("{rebateUnit}", rebateUnit.ToString());

            //支付方式
            /*
            if (userModel.UserCategory == "partner")
            {
                string payList = "";
                string payTypeFrist = "";
                if (userModel.EnableOnlinePay.Value)
                {
                    payList = payList + "<option  value=\"在线支付\">在线支付</option>";
                    if (payTypeFrist == "")
                        payTypeFrist = "在线支付";
                }

                if (userModel.EnableAccountPay.Value)
                {
                    payList = payList + " <option value=\"积分支付\">积分支付</option>";
                    if (payTypeFrist == "")
                        payTypeFrist = "积分支付";
                }
                //if (userModel.EnableCashPay.Value)
                //    payList = payList + "<option value=\"现金支付\">现金支付</option>";
                carTempl = carTempl.Replace("{payTypeFrist}", payTypeFrist);
                carTempl = carTempl.Replace("{payType}", payList);
            }
            else
            {
                carTempl = carTempl.Replace("{payType}", "<option value=\"在线支付\">在线支付</option>");
                carTempl = carTempl.Replace("{payTypeFrist}", "在线支付");
            }
            */
            carTempl = carTempl.Replace("{payType}", "<option value=\"在线支付\">在线支付</option>");
            carTempl = carTempl.Replace("{payTypeFrist}", "在线支付");

            //取票方式
            if (userModel.UserCategory == "partner")
            {
                string validType = "";
                validType = validType + "<option selected value=\"二维码\">二维码</option>";
                //validType = validType + "<option value=\"身份证\">身份证</option>";
                //validType = validType + "<option value=\"纸质\">纸质</option>";
                //carTempl = carTempl.Replace("{selValidType}", validType);
            }
            else
            {
                string validType = "";
                validType = validType + "<option selected value=\"二维码\">二维码</option>";
                //carTempl = carTempl.Replace("{selValidType}", validType);
            }
            return carTempl;
        }
        #endregion

        #region 订单打印
        /// <summary>
        /// 订单打印
        /// </summary>
        public string OrderPrint(int orderID)
        {
            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
            return OrderPrint(sheet);
        }
        /// <summary>
        /// 订单打印
        /// </summary>
        public string OrderPrint(EFEntity.OrderSheet sheet)
        {
            if (!BLL.OrderSheetBLL.Instance.IsDoPrint(sheet))
            {
                return "订单" + sheet.SheetID + "已经打印<br/>如需要再次打印请联系管理员。";
            }

            string html = "";
            if (sheet.CategoryID == "line")
            {
                html = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/print/line.html"));
            }
            else
            {
                html = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/print/ticket.html"));
            }

            html = html.Replace("{UserID}", sheet.UserID.ToString());
            html = html.Replace("{CPName}", sheet.CPName);
            html = html.Replace("{ProductName}", sheet.ProductName);
            html = html.Replace("{NUM}", sheet.NUM.ToString());
            html = html.Replace("{RealName}", sheet.RealName);
            html = html.Replace("{Phone}", sheet.Phone);
            html = html.Replace("{SheetID}", sheet.SheetID.ToString());

            string UserName = "", CPTel = "";
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == sheet.UserID.Value);
            if (user != null)
            {
                UserName = user.UserName;
                CPTel = user.Tel;
            }
            html = html.Replace("{CPTel}", CPTel);

            if (sheet.CategoryID == "line")
            {
                //专线
                string startTime = sheet.StartTime.Value.ToString("yyyy-MM-dd <br/> HH:mm");
                html = html.Replace("{startTime}", startTime);
                html = html.Replace("{startAddress}", sheet.StartAddress);
            }
            else
            {
                //景区
                string payDate = "";
                if (sheet.PalyDate != null)
                    payDate = sheet.PalyDate.Value.ToString("yyyy-MM-dd");
                html = html.Replace("{PayDate}", payDate);
            }
            //打印备注
            string printMemo = "";
            var productModel = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == sheet.ProductID.Value);
            if (productModel!=null)
            {
                if(!string.IsNullOrEmpty(productModel.PrintMemo))
                {
                    string[] printList = productModel.PrintMemo.Split('$');
                    foreach(var printItem in printList)
                    {
                        printMemo= printMemo + string.Format("<p>{0}</p>", printItem);
                    }
                }
            }
            html = html.Replace("{PrintMemo}", printMemo);

            return html;
        }

        #region 用户网页的打印
        public string OrderPrintForMulNum(int orderID)
        {
            var sheet = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID);
            return OrderPrintForMulNum(sheet);
        }
        public string OrderPrintForMulNum(EFEntity.OrderSheet sheet)
        {
            if (!BLL.OrderSheetBLL.Instance.IsDoPrintMulNum(sheet))
            {
                return "订单" + sheet.SheetID + "已经打印<br/>如需要再次打印请联系管理员。";
            }

            string html = "";
            if (sheet.CategoryID == "line")
            {
                html = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/print/line.html"));
            }
            else
            {
                html = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/print/ticket.html"));
            }

            html = html.Replace("{UserID}", sheet.UserID.ToString());
            html = html.Replace("{CPName}", sheet.CPName);
            html = html.Replace("{ProductName}", sheet.ProductName);
            html = html.Replace("{NUM}", sheet.NUM.ToString());
            html = html.Replace("{RealName}", sheet.RealName);
            html = html.Replace("{Phone}", sheet.Phone);
            html = html.Replace("{SheetID}", sheet.SheetID.ToString());

            string UserName = "", CPTel = "";
            var user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == sheet.UserID.Value);
            if (user != null)
            {
                UserName = user.UserName;
                CPTel = user.Tel;
            }
            html = html.Replace("{CPTel}", CPTel);

            if (sheet.CategoryID == "line")
            {
                //专线
                string startTime = sheet.StartTime.Value.ToString("yyyy-MM-dd <br/> HH:mm");
                html = html.Replace("{startTime}", startTime);
                html = html.Replace("{startAddress}", sheet.StartAddress);
            }
            else
            {
                //景区
                string payDate = "";
                if (sheet.PalyDate != null)
                    payDate = sheet.PalyDate.Value.ToString("yyyy-MM-dd");
                html = html.Replace("{PayDate}", payDate);
            }
            //打印备注
            string printMemo = "";
            var productModel = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == sheet.ProductID.Value);
            if (productModel != null)
            {
                if (!string.IsNullOrEmpty(productModel.PrintMemo))
                {
                    string[] printList = productModel.PrintMemo.Split('$');
                    foreach (var printItem in printList)
                    {
                        printMemo = printMemo + string.Format("<p>{0}</p>", printItem);
                    }
                }
            }
            html = html.Replace("{PrintMemo}", printMemo);

            return html;
        }
        #endregion

        #endregion

        #region 门户
        /// <summary>
        /// 文章列表
        /// </summary>
        public string ArtListHtml(IEnumerable<EFEntity.ArtContent> artList)
        {
           string templPath="/templ/art/tr.html";
           return ArtListHtml(artList,templPath);
        }
        public string ArtListHtml(IEnumerable<EFEntity.ArtContent> artList,string templ)
        {
            string table = "<table class=\"table\">{0}</table>";
            string html = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath(templ));
            StringBuilder sb = new StringBuilder();
            foreach (var art in artList)
            {
                string item = html;
                item = item.Replace("{title}", art.ArtTitle);
                item = item.Replace("{time}", art.AddTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                item = item.Replace("{url}", BLL.RewriteMapBLL.Instance.ViwePath(art.ArtID));

                sb.Append(item);
            }
            return string.Format(table, sb.ToString());
        }
        #region 头部
        public string TopBarHtml(string templPath)
        {
            return TopBarHtml(templPath, "color:#ffffff;");
        }
        public string TopBarHtml(string templPath, string bg)
        {
            string html = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath(templPath));
            EFEntity.CookiesUser cookieUser = BLL.UserBLL.Instance.GetLoginModel();

            string helpUrl = "/help.html";
            string userName = "";
            StringBuilder sb = new StringBuilder();
            if (cookieUser == null)
            {
                //没有登录
                sb.AppendFormat("<a href=\"/login.aspx\" style=\"margin-right: 5px;{0}\">登录</a>", bg);
                sb.AppendFormat("<a href=\"/register.aspx\" style=\"margin-right: 5px;{0}\">注册</a>", bg);
                sb.AppendFormat("<a href=\"/login.aspx\" style=\"margin-right: 5px;{0}\">我的订单</a>", bg);
                sb.AppendFormat("<a href=\"{0}\" style=\"margin-right: 5px;{1}\">帮助中心</a>", helpUrl, bg);
            }
            else if (cookieUser.UserCategory == "admin")
            {
                //后台用户
                userName = cookieUser.UserName + "，";

                sb.AppendFormat("<a href=\"/logout.aspx\" style=\"margin-right: 5px;{0}\">退出登录</a>", bg);
                sb.AppendFormat("<a href=\"/business/main.aspx\" style=\"margin-right: 5px;{0}\">进入管理后台</a>", bg);
                sb.AppendFormat("<a href=\"{0}\" style=\"margin-right: 5px;{1}\">帮助中心</a>", helpUrl, bg);
            }
            else if (cookieUser.UserCategory == "valid")
            {
                //验票用户
                userName = cookieUser.UserName + "，";

                sb.AppendFormat("<a href=\"/logout.aspx\" style=\"margin-right: 5px;{0}\">退出登录</a>", bg);
                sb.AppendFormat("<a href=\"/business/valid.aspx\" style=\"margin-right: 5px;{0}\">进入管理后台</a>", bg);
                sb.AppendFormat("<a href=\"{0}\" style=\"margin-right: 5px;{1}\">帮助中心</a>", helpUrl, bg);
            }
            else if (cookieUser.UserCategory == "partner")
            {
                //分销商
                userName = cookieUser.UserName + "，";
                sb.AppendFormat("<a href=\"/logout.aspx\" style=\"margin-right: 5px;{0}\">退出登录</a>", bg);
                sb.AppendFormat("<a href=\"/business/partner.aspx\" style=\"margin-right: 5px;{0}\">进入分销系统</a>", bg);
                sb.AppendFormat("<a href=\"{0}\" style=\"margin-right: 5px;{1}\">帮助中心</a>", helpUrl, bg);
            }
            else if (cookieUser.UserCategory == "user")
            {
                //普通会员
                userName = cookieUser.UserName + "，";
                sb.AppendFormat("<a href=\"/logout.aspx\" style=\"margin-right: 5px;{0}\">退出登录</a>", bg);
                sb.AppendFormat("<a href=\"/ucenter/order.aspx\" style=\"margin-right: 5px;{0}\">我的订单</a>", bg);
                sb.AppendFormat("<a href=\"{0}\" style=\"margin-right: 5px;{1}\">帮助中心</a>", helpUrl, bg);
            }
            html = html.Replace("{User}", userName);
            html = html.Replace("{URL}", sb.ToString());
            return html;
        }
        #endregion

        /// <summary>
        /// 首页菜单-动态部分
        /// </summary>
        public string IndexMenu()
        {
            EFEntity.CookiesUser cookieUser = BLL.UserBLL.Instance.GetLoginModel();
            StringBuilder sb = new StringBuilder();
            if (cookieUser == null)
            {
                //没有登录

                sb.Append(" <li><a href=\"/login.aspx\">登录</a></li>");
                sb.Append(" <li><a href=\"/register.aspx\">注册</a></li>");
            }
            else if (cookieUser.UserCategory == "admin")
            {
                sb.Append(" <li><a href=\"/business/main.aspx\">进入管理后台</a></li>");
            }
            else if (cookieUser.UserCategory == "valid")
            {
                sb.Append(" <li><a href=\"/business/valid.aspx\">进入管理后台</a></li>");
            }
            else if (cookieUser.UserCategory == "partner")
            {
                //分销商
                sb.Append(" <li><a href=\"/business/partner.aspx\">进入分销系统</a></li>");

            }
            else if (cookieUser.UserCategory == "user")
            {
                sb.Append(" <li><a href=\"/ucenter/order.aspx\">我的订单</a></li>");
            }

            return sb.ToString();
        }

        public string IndexListArt(int moduleID, int top, int length, string templPath)
        {
            //var list = BLL.ArtContentBLL.Instance.GetEntities(p => p.ModuleID == moduleID).Take(top).OrderByDescending(p => p.AddTime);
            //return ListArt(list, length, templPath);

            ETicket.Utility.PageInfo<EFEntity.ArtContent> pi = null;
            pi = BLL.ArtContentBLL.Instance.GetPageList(1, top, " it.ModuleID=" + moduleID, "it.AddTime DESC");
            return ListArt(pi.List, length, templPath);
        }
        public string ListArt(IEnumerable<EFEntity.ArtContent> list, int length, string templPath)
        {
            string htmlTeml = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath(templPath));
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                string url = ETicket.BLL.RewriteMapBLL.Instance.ViwePath(item.ArtID);
                string title = item.ArtTitle;
                if (title.Length > length)
                    title = title.Substring(0, length) + "......";

                string time = "";
                if (item.AddTime != null)
                {
                    time = item.AddTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                string html = htmlTeml;
                html = html.Replace("{Title}", title);
                html = html.Replace("{AltTitle}", item.ArtTitle);
                html = html.Replace("{Time}", time);
                html = html.Replace("{Url}", url);
                sb.Append(html);
            }
            return sb.ToString();
        }

        #region 商品列表
        public string ListProduct(string category, int top, string templPath)
        {
            var list = BLL.ProductBLL.Instance.GetEntities(p => p.CategoryID == category && p.SaleFlag == true).Take(top).OrderByDescending(p => p.AddTime);
            return ListProduct(list, templPath);
        }
        public string ListProduct(IEnumerable<EFEntity.Product> productList, string templPath)
        {
            string buyUrl = PubFun.ApplicationPath + "/shopcar.aspx?id={0}";

            StringBuilder sb = new StringBuilder();
            string itemTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath(templPath));
            foreach (var product in productList)
            {
                string itemHtml = itemTempl;

                itemHtml = itemHtml.Replace("{ProductName}", product.ProductName);
                itemHtml = itemHtml.Replace("{BuyUrl}", string.Format(buyUrl, product.ProductID));
                //详细浏览URL
                string viewUrl = "";
                if (product.CategoryID == "line")
                {
                    viewUrl = BLL.RewriteMapBLL.Instance.ViewLineUrl(product.ProductID);
                }
                else
                {
                    viewUrl = BLL.RewriteMapBLL.Instance.ViewTicketUrl(product.ProductID);
                }
                itemHtml = itemHtml.Replace("{ViewUrl}", viewUrl);

                //价格
                decimal price = 0;
                //EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                //EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID);
                var userLevel = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelName == "普通会员");
                var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == userLevel.UserLevelID);
                if (suk != null)
                    price = suk.ProductPrice.Value;
                itemHtml = itemHtml.Replace("{Price}", price.ToString());

                //图片
                string path = "/noimg.jpg";
                if (!string.IsNullOrEmpty(product.TitleImg))
                {
                    path = product.TitleImg_S;
                }
                itemHtml = itemHtml.Replace("{ImgUrl}", path);

                sb.Append(itemHtml);
            }

            return sb.ToString();

        }
        #endregion

        #region 商品详情
        public string ViewProduct(int productID,string templPath)
        {
            var product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID);
            return ViewProduct(product, templPath);
        }
        public string ViewProduct(EFEntity.Product product, string templPath)
        {
            string buyUrl = PubFun.ApplicationPath + "/shopcar.aspx?id={0}";

            StringBuilder sb = new StringBuilder();
            string itemTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath(templPath));
            string itemHtml = itemTempl;
            //商品名称
            itemHtml = itemHtml.Replace("{ProductName}", product.ProductName);
            //购买地址
            itemHtml = itemHtml.Replace("{BuyUrl}", string.Format(buyUrl, product.ProductID));

            /*
            //详细浏览URL
            string viewUrl = "";
            if (product.CategoryID == "line")
            {
                viewUrl = BLL.RewriteMapBLL.Instance.ViewLineUrl(product.ProductID);
            }
            else
            {
                viewUrl = BLL.RewriteMapBLL.Instance.ViewTicketUrl(product.ProductID);
            }
            itemHtml = itemHtml.Replace("{ViewUrl}", viewUrl);
            */

            //价格
            decimal price = 0;
            var userLevel = BLL.UserLevelBLL.Instance.GetEntity(p => p.UserLevelName == "普通会员");
            var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == userLevel.UserLevelID);
            if (suk != null)
                price = suk.ProductPrice.Value;
            itemHtml = itemHtml.Replace("{Price}", price.ToString());

            //门市价
            string PrimeCost = "";
            if (product.PrimeCost!=null)
                PrimeCost=product.PrimeCost.ToString();
            itemHtml = itemHtml.Replace("{PrimeCost}", PrimeCost);
            
            //图片
            string path = "/noimg.jpg";
            if (!string.IsNullOrEmpty(product.TitleImg))
            {
                path = product.TitleImg_M;
            }
            itemHtml = itemHtml.Replace("{ImgUrl}", path);

            //预定须知
            itemHtml = itemHtml.Replace("{RulesNote}", product.RulesNote);
            //详细介绍
            itemHtml = itemHtml.Replace("{Detail}", product.Detail);
            
            if(product.CategoryID=="ticket")
            {
                //景区
                itemHtml = itemHtml.Replace("{Address}", product.Address);
                itemHtml = itemHtml.Replace("{Tel}", product.Tel);
                itemHtml = itemHtml.Replace("{OpenTime}", product.OpenTime);
            }
            else if (product.CategoryID == "line")
            {
                string strStartTime = "";

                //发车时间
                var timeList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                foreach (var time in timeList)
                {
                    string timeStr = PubFun.FormatHourMintue(time.StartH.ToString()) + ":" + PubFun.FormatHourMintue(time.StartM.ToString());
                    if (strStartTime == "")
                        strStartTime = timeStr;
                    else
                        strStartTime = strStartTime + "<br/>" + timeStr;
                }
              
                //上车地点
                string strStartAddress = "";
                var addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                foreach (var address in addressList)
                {
                    if (strStartAddress == "")
                        strStartAddress = address.Address;
                    else
                        strStartAddress = strStartAddress + "<br/>" + address.Address;
                }
                if (strStartAddress == "")
                    strStartAddress = "无固定地点，下单时由客人填写";

                itemHtml = itemHtml.Replace("{StartTime}", strStartTime);
                itemHtml = itemHtml.Replace("{StartAddress}", strStartAddress);
            }
            sb.Append(itemHtml);

            return sb.ToString();
        }
        #endregion

        #region 最新订单
        public string ListNewOrder(int top, string templPath)
        {
            StringBuilder sb = new StringBuilder();
            string itemTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath(templPath));
            var list = BLL.OrderSheetBLL.Instance.GetEntities().Take(top).OrderByDescending(p => p.OrderTime);
            foreach (var sheet in list)
            {
                string realName = sheet.RealName;
                string phone = sheet.Phone;
                if (phone.Length > 4)
                {
                    int end = phone.Length - 4;
                    phone = phone.Substring(0, end) + "xxxx";
                }
                string num = sheet.NUM.ToString();
                string product = sheet.ProductName;

                string ss = string.Format("{0}购买{1}数量{2}", phone, product, num);
                string itemHtml = itemTempl;
                itemHtml = itemHtml.Replace("{Title}", ss);

                sb.Append(itemHtml);
            }

            return sb.ToString();
        }
        #endregion

        #region 帮助
        public string ListHelpTitle(int categoryID, int top)
        {
            if (top <= 0)//取所有数据
                top = 100000;

            var list = BLL.HelpContentBLL.Instance.GetEntities(p => p.CategoryID == categoryID).Take(top);
            return ListHelpTitle(list);
        }
        public string ListHelpTitle(IEnumerable<EFEntity.HelpContent> list)
        {
            StringBuilder sb = new StringBuilder();
            string templ = "<li><a href=\"{0}\">{1}</a></li>";

            foreach (var item in list)
            {
                string url = BLL.RewriteMapBLL.Instance.HelpUrl(item.HelpID);
                string itemHtml = string.Format(templ, url, item.HelpTitle);
                sb.Append(itemHtml);
            }
            return sb.ToString();
        }

        public string ListHelpCotent(int categoryID)
        {
            var list = BLL.HelpContentBLL.Instance.GetEntities(p => p.CategoryID == categoryID);
            StringBuilder sb = new StringBuilder();
            string itemTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/help/content.html"));
            foreach (var item in list)
            {
                string url = BLL.RewriteMapBLL.Instance.HelpUrl(item.HelpID);
                string itemHtml = itemTempl;
                itemHtml = itemHtml.Replace("{id}", item.HelpID.ToString());
                itemHtml = itemHtml.Replace("{tilte}", item.HelpTitle);
                itemHtml = itemHtml.Replace("{content}", item.HelpContent1);
                sb.Append(itemHtml);
            }
            return sb.ToString();
        }

        #endregion

        #region 图片轮播
        public string ListCarouse(int top)
        {
            var list = BLL.PageWidgetBLL.Instance.GetEntities(p => p.Category == "carousel" ).Take(5);
            StringBuilder sb = new StringBuilder();
            string itemTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/index/carousel.html"));
            int num = 0;
            foreach (var item in list)
            {
                string url = "#";
                if (!string.IsNullOrEmpty(item.TitleHref))
                    url = item.TitleHref;
                string css = "";
                if (num == 0)
                    css = "active";

                string itemHtml = itemTempl;
                itemHtml = itemHtml.Replace("{url}", url);
                itemHtml = itemHtml.Replace("{css}", css);
                itemHtml = itemHtml.Replace("{title}", item.Title);
                itemHtml = itemHtml.Replace("{imgurl}", item.TitleImg);

                sb.Append(itemHtml);

                num++;
            }
            return sb.ToString();
        }
        #endregion

        #region 友情链接
        public string ListLink()
        {
            var list = BLL.PageWidgetBLL.Instance.GetEntities(p => p.Category == "link");
            StringBuilder sb = new StringBuilder();
            string itemTempl = "<a href=\"{0}\" style=\"margin-left:10px;\" target=\"_blank\">{1}</a>";
            int num = 0;
            foreach (var item in list)
            {
                string url = "#";
                if (!string.IsNullOrEmpty(item.TitleHref))
                    url = item.TitleHref;
            

                string itemHtml = string.Format(itemTempl,url,item.Title);
                sb.Append(itemHtml);

                num++;
            }
            return sb.ToString();
        }
        #endregion

        #endregion

    }
}