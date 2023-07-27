using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using System.Text;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

using NJiaSu.Libraries;

namespace ETicket.Web
{
    public partial class QRCode : System.Web.UI.Page
    {
        public string productName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string strID = PubFun.QueryString("id");
            if (strID.Length < 3)
            {
                productName="地址非法";
                imgQRCode.Visible = false;
                return;
            }
            string rmStr = strID.Substring(0, 3);//去掉前面3位;
            strID = strID.Replace(rmStr, "");
            int orderID = PubFun.Char2Num(strID);
            if (orderID <=0)
            {
                productName="地址非法";
                imgQRCode.Visible = false;
                return;
            }
            var sheet= BLL.OrderSheetBLL.Instance.GetEntity(p=>p.OrderID==orderID);
            if (sheet==null)
            {
                productName="二维码不存在";
                imgQRCode.Visible = false;
                return;
            }
            if (sheet.ValidType != "二维码")
            {
                productName = "二维码不存在";
                imgQRCode.Visible = false;
                return;
            }

            #region 状态验证
            //if (sheet.OrderStatus==ETicket.Utility.OrderStatusEnum.已验证.ToString())
            //{
            //    productName = string.Format("你购买的[{0}]已验票", sheet.ProductName);
            //    imgQRCode.Visible = false;
            //    return;
            //}
            if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.已过期.ToString())
            {
                productName = string.Format("你购买的[{0}]已过期,不能再使用，请到购买处申请退款", sheet.ProductName);
                imgQRCode.Visible = false;
                return;
            }
            else if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.退款审核不通过.ToString() || 
                sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.退款审核通过.ToString()||
                sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.退款审核中.ToString())
            {
                productName = string.Format("你购买的[{0}]已申请退款,不能验票", sheet.ProductName);
                imgQRCode.Visible = false;
                return;
            }
            else if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.已支付.ToString() || sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.已验票.ToString())
            {
                string qrString = PubFun.GetRandString(6)+ PubFun.Num2Char(orderID.ToString());
                string md5 = NJiaSu.Libraries.Encrypt.GetMd5Hash(sheet.SheetID + sheet.QRCode + orderID.ToString());
                string encodeQRString = md5 + "-" + qrString;

                string md5Login = NJiaSu.Libraries.Encrypt.GetMd5Hash(ETicket.Web.api.ApiHelper.md5Key + "2");

                string state = "";
                if (sheet.OrderStatus == ETicket.Utility.OrderStatusEnum.已支付.ToString())
                    state = "未验票";
                else
                    state = "已验票";

                //productName = string.Format("你购买的[{0}]" + state, sheet.ProductName) + encodeQRString;
                productName = string.Format("你购买的[{0}]" + state, sheet.ProductName);

                Bitmap image = QRCodeEncoderUtil(encodeQRString);
                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                string pPath = Server.MapPath("/tempfile/");
                string savePath = pPath + orderID + ".jpg";
                string viewPath = "/tempfile/" + orderID + ".jpg";
                try
                {
                    if (File.Exists(savePath))
                        File.Delete(savePath);
                }
                catch
                {

                }
                image.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //Response.ClearContent();
                //Response.ContentType = "image/jpg";
                //Response.BinaryWrite(ms.ToArray());
                imgQRCode.ImageUrl = viewPath;
                imgQRCode.Visible = true;
            }
            else
            {
                productName = string.Format("你购买的[{0}]不在可验票范围内", sheet.ProductName);
                imgQRCode.Visible = false;
                return;
            }
            #endregion

        }

        public static Bitmap QRCodeEncoderUtil(string qrCodeContent)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeVersion = 0;
            Bitmap img = qrCodeEncoder.Encode(qrCodeContent, Encoding.UTF8);//指定utf-8编码， 支持中文
            return img;
        }
    }
}