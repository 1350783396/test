using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using NJiaSu.Libraries;

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

        public string LineOrderHtml()
        {
            string linkTempl = "<a target=\"_blank\" href=\"/shopcar.aspx?id={0}&tick={1}\">购买</a>";

            StringBuilder sb = new StringBuilder();

            IEnumerable<EFEntity.Product> productList = BLL.ProductBLL.Instance.GetEntities(p => p.CategoryID == "line" && p.SaleFlag == true);
            foreach (var product in productList)
            {
                string itemHtml = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/line-order-item.html"));
                itemHtml = itemHtml.Replace("{ProductName}", product.ProductName);

                StringBuilder sbItem = new StringBuilder();
                IEnumerable<EFEntity.ProductTickTime> tickList = BLL.ProductTickTimeBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                foreach (var tick in tickList)
                {
                    string tr = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/line-order-tr.html"));

                    string saleDate = tick.SaleDate.Value.ToString("yyyy-MM-dd");
                    string startDate = Convert.ToDateTime(saleDate + string.Format(" {0}:{1}", tick.StartH, tick.StartM)).ToString("yyyy-MM-dd HH:mm");
                    DateTime lastOrderTime = Convert.ToDateTime(saleDate + string.Format(" {0}:{1}", tick.LastOrderH, tick.LastOrderM));
                    //下单链接
                    string link = "";
                    if (tick.Stock == 0)
                    {
                        link = "售磬";
                    }
                    else if (DateTime.Now > lastOrderTime)
                    {
                        link = "截止";
                    }
                    else
                    {
                        link = string.Format(linkTempl, product.ProductID, tick.PKID);
                    }
                    //价格
                    decimal price = 0;
                    EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                    EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p=>p.UserID==cookiesUser.UserID);
                    var suk=  BLL.ProductSUKBLL.Instance.GetEntity(p=>p.ProductID==product.ProductID&&p.UserLevelID==user.UserLevelID);
                    if (suk != null)
                        price = suk.ProductPrice.Value;

                    tr = tr.Replace("{date}", startDate);
                    tr = tr.Replace("{price}", price.ToString());
                    tr = tr.Replace("{stock}", tick.Stock.ToString());
                    tr = tr.Replace("{link}", link);
                    
                    sbItem.Append(tr);
                }

                itemHtml = itemHtml.Replace("{trs}", sbItem.ToString());
                sb.Append(itemHtml);

            }
            return sb.ToString();
        }
        public string LineOrderHtml(IEnumerable<EFEntity.Product> productList )
        {
            string linkTempl = "<a target=\"_blank\" href=\"/shopcar.aspx?id={0}&tick={1}\">购买</a>";

            StringBuilder sb = new StringBuilder();

            foreach (var product in productList)
            {
                string itemHtml = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/line-order-item.html"));
                itemHtml = itemHtml.Replace("{ProductName}", product.ProductName);

                StringBuilder sbItem = new StringBuilder();
                IEnumerable<EFEntity.ProductTickTime> tickList = BLL.ProductTickTimeBLL.Instance.GetEntities(p => p.ProductID == product.ProductID);
                foreach (var tick in tickList)
                {
                    string tr = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/line-order-tr.html"));

                    string saleDate = tick.SaleDate.Value.ToString("yyyy-MM-dd");
                    string startDate = Convert.ToDateTime(saleDate + string.Format(" {0}:{1}", tick.StartH, tick.StartM)).ToString("yyyy-MM-dd HH:mm");
                    DateTime lastOrderTime = Convert.ToDateTime(saleDate + string.Format(" {0}:{1}", tick.LastOrderH, tick.LastOrderM));
                    //下单链接
                    string link = "";
                    if (tick.Stock == 0)
                    {
                        link = "售磬";
                    }
                    else if (DateTime.Now > lastOrderTime)
                    {
                        link = "截止";
                    }
                    else
                    {
                        link = string.Format(linkTempl, product.ProductID, tick.PKID);
                    }
                    //价格
                    decimal price = 0;
                    EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                    EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID);
                    var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == user.UserLevelID);
                    if (suk != null)
                        price = suk.ProductPrice.Value;

                    tr = tr.Replace("{date}", startDate);
                    tr = tr.Replace("{price}", price.ToString());
                    tr = tr.Replace("{stock}", tick.Stock.ToString());
                    tr = tr.Replace("{link}", link);

                    sbItem.Append(tr);
                }

                itemHtml = itemHtml.Replace("{trs}", sbItem.ToString());
                sb.Append(itemHtml);

            }
            return sb.ToString();
        }
        public string TickOrderHtml()
        {
            string linkTempl = "<a target=\"_blank\" href=\"/shopcar.aspx?id={0}&tick={1}\">购买</a>";

            StringBuilder sb = new StringBuilder();
            string itemHtml = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/ticket-order-item.html"));

            IEnumerable<EFEntity.Product> productList = BLL.ProductBLL.Instance.GetEntities(p => p.CategoryID == "ticket" && p.SaleFlag == true);
            foreach (var product in productList)
            {

                //每一个行
                string tr = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/ticket-order-tr.html"));
                string link = "";
                if (product.Stock == 0)
                {
                    link = "售磬";
                }
                else
                {
                    link = string.Format(linkTempl, product.ProductID, "0");
                }

                //价格
                decimal price = 0;
                EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID);
                var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == user.UserLevelID);
                if (suk != null)
                    price = suk.ProductPrice.Value;

                tr = tr.Replace("{product}", product.ProductName);
                tr = tr.Replace("{price}", price.ToString());
                tr = tr.Replace("{stock}", product.Stock.ToString());
                tr = tr.Replace("{link}", link);

                sb.Append(tr);

            }

            itemHtml = itemHtml.Replace("{trs}", sb.ToString());
            return itemHtml;
        }

        public string TickOrderHtml(IEnumerable<EFEntity.Product> productList)
        {
            string linkTempl = "<a target=\"_blank\" href=\"/shopcar.aspx?id={0}&tick={1}\">购买</a>";

            StringBuilder sb = new StringBuilder();
            string itemHtml = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/ticket-order-item.html"));

            foreach (var product in productList)
            {

                //每一个行
                string tr = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/ticket-order-tr.html"));
                string link = "";
                if (product.Stock == 0)
                {
                    link = "售磬";
                }
                else
                {
                    link = string.Format(linkTempl, product.ProductID, "0");
                }

                //价格
                decimal price = 0;
                EFEntity.CookiesUser cookiesUser = BLL.UserBLL.Instance.GetLoginModel();
                EFEntity.User user = BLL.UserBLL.Instance.GetEntity(p => p.UserID == cookiesUser.UserID);
                var suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == user.UserLevelID);
                if (suk != null)
                    price = suk.ProductPrice.Value;

                tr = tr.Replace("{product}", product.ProductName);
                tr = tr.Replace("{price}", price.ToString());
                tr = tr.Replace("{stock}", product.Stock.ToString());
                tr = tr.Replace("{link}", link);

                sb.Append(tr);

            }

            itemHtml = itemHtml.Replace("{trs}", sb.ToString());
            return itemHtml;
        }

        public string Car()
        {
            EFEntity.CookiesUser cookiesUser= BLL.UserBLL.Instance.GetLoginModel();
            if(cookiesUser==null)
            {
                
            }
            int userID = cookiesUser.UserID;
            var userModel=BLL.UserBLL.Instance.GetEntity(p=>p.UserID==userID);
            decimal account = 0;
            if (userModel.Account != null)
                account = userModel.Account.Value;

            string carTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car/car.html"));
            string lineColTempl = PubFun.ReadHtmlFile(HttpContext.Current.Server.MapPath("/templ/car/line-col.html"));

            //StringBuilder sb = new StringBuilder();

            int productID = PubFun.QueryInt("id");
            int tickID = PubFun.QueryInt("tick");
            if (productID < 0)
            {
                //产品不存在
            }
            EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID);
            if (product == null)
            {
                //产品不存在
            }

            //用户积分
            carTempl = carTempl.Replace("{Account}", account.ToString());
            
            //产品信息
            carTempl = carTempl.Replace("{ProductName}", product.ProductName);
            carTempl = carTempl.Replace("{RulesNote}", product.RulesNote);
            carTempl = carTempl.Replace("{Detail}", product.Detail);

            #region 专线
            string lineCol = "";
            decimal stock = 0;
            if (tickID > 0 && product.CategoryID == "line")
            {
                //地址
                string addressHtml = "";
                IEnumerable<EFEntity.ProductAddress> addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == productID);
                foreach (var address in addressList)
                {
                    addressHtml = addressHtml + string.Format("<option value=\"{0}\">{0}</option>", address.Address);
                }
                lineColTempl = lineColTempl.Replace("{address}", addressHtml);
                //销售时间
                EFEntity.ProductTickTime tickModel = BLL.ProductTickTimeBLL.Instance.GetEntity(p=>p.PKID==tickID);
                string saleDate = tickModel.SaleDate.Value.ToString("yyyy-MM-dd");
                string startDate = Convert.ToDateTime(saleDate + string.Format(" {0}:{1}", tickModel.StartH, tickModel.StartM)).ToString("yyyy-MM-dd HH:mm");
                lineColTempl = lineColTempl.Replace("{startTime}", startDate);
                //库存
                stock = tickModel.Stock.Value;
                lineCol=lineColTempl;
            }
            else
            {
                stock = product.Stock.Value;
            }
            carTempl = carTempl.Replace("{line-col}", lineCol);
            #endregion

            //库存
            carTempl = carTempl.Replace("{stock}", stock.ToString());

            //单价
            EFEntity.ProductSUK suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == productID && p.UserLevelID == userModel.UserLevelID.Value);
            if(suk!=null)
            {
                carTempl = carTempl.Replace("{unitPrice}", suk.ProductPrice.ToString());
            }

            //支付方式
            if(userModel.UserCategory=="partner")
            {
                string payList="";
                if(userModel.EnableOnlinePay.Value)
                    payList=payList+"<option  value=\"在线支付\">在线支付</option>";
                if(userModel.EnableAccountPay.Value)
                    payList = payList +" <option value=\"积分支付\">积分支付</option>";
                if (userModel.EnableCashPay.Value)
                    payList = payList + "<option value=\"现金支付\">现金支付</option>";

                carTempl = carTempl.Replace("{payType}", payList);
            }
            else 
            {
                carTempl=carTempl.Replace("{payType}","<option value=\"在线支付\">在线支付</option>");
            }
            return carTempl;
        }
    }
}