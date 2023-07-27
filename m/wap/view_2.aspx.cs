using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

namespace ETicket.Web.wap
{
    public partial class view_2 : System.Web.UI.Page
    {

        public string imgPath = "", productName = "", rulesNote_WAP="", detail_WAP="", primeCost="",myPrice="",productType="",productIntr="";
        protected void Page_Load(object sender, EventArgs e)
        {
            int productID = 0;
            int userID = 0;
            WapHelper.GetUserID_ProductID("q",out userID, out productID);

            if (productID == 0 && userID == 0)
            {
                Response.Write("信息错误");
                Response.End();
                return;
            }

            EFEntity.Product product = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID && p.SaleFlag == true);
            if (product == null)
            {
                Response.Write("该产品不存在或者已经下架");
                Response.End();
                return;
            }


            var userModel = BLL.UserBLL.Instance.GetEntity(p => p.UserID == userID);
            if(userModel==null)
            {
                Response.Write("哦！该产品不存在或者已经下架。");
                Response.End();
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
            
            if(product.CategoryID=="line")
            {
                productType = "专线";

                //发车时间
                string strStartTime = "";
                var timeList = BLL.ProductSaleTimeBLL.Instance.GetEntities(p => p.ProductID == productID);
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
                var addressList = BLL.ProductAddressBLL.Instance.GetEntities(p => p.ProductID == productID);
                foreach (var address in addressList)
                {
                    if (strStartAddress == "")
                        strStartAddress = address.Address;
                    else
                        strStartAddress = strStartAddress + "，" + address.Address;
                }

                if (string.IsNullOrEmpty(strStartAddress))
                {
                    strStartAddress = "无固定地址，由用户自行选择";
                }

                productIntr = "<p>发车时间：" + strStartTime + "</p>";
                productIntr += "<p>上车地点：" + strStartAddress + "</p>";

            }
            else if (product.CategoryID == "ticket")
            {
                productType = "门票";
                productIntr = "<p>营业时间：" +product.OpenTime+ "</p>";
                productIntr += "<p>景点电话：" + product.Tel+"</p>";
                productIntr += "<p>景点地址：" +product.Address+ "</p>";

            }
            imgPath = product.TitleImg_M;
            productName = product.ProductName;
            rulesNote_WAP = string.IsNullOrEmpty(product.RulesNote_WAP) ? "暂无说明" : product.RulesNote_WAP.Trim();
            detail_WAP = string.IsNullOrEmpty(product.Detail_WAP) ? "暂无说明" : product.Detail_WAP.Trim();
            primeCost = product.PrimeCost == null ? "0" : product.PrimeCost.ToString();

            //单价
            EFEntity.FX_Price fxPrice = BLL.FXPriceBLL.Instance.GetEntity(p => p.UserID == userID && p.ProductID == productID);
            if (fxPrice != null)
            {
                myPrice = fxPrice.MyPrice.ToString();
            }
            else 
            {
                EFEntity.ProductSUK suk = BLL.ProductSUKBLL.Instance.GetEntity(p => p.ProductID == product.ProductID && p.UserLevelID == userModel.UserLevelID.Value);
                if (suk!=null)
                    myPrice=suk.FXPrice_Recommend.ToString();

            }
        }
    }
}