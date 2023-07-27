using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using System.Net;
using System.Security.Cryptography;

namespace ETicket.Web.TestDemo
{
    public partial class seng_yzy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetMd5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }


        /// <summary>
        /// 发送POST数据，得到返回结果
        /// </summary>
        /// <returns></returns>
        public string SendPost(SortedDictionary<string, string> sParaTemp)
        {

            StringBuilder sbUrlData = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in sParaTemp)
            {
                if (sbUrlData.ToString() == "")
                    sbUrlData.Append(temp.Key + "=" + temp.Value);
                else
                    sbUrlData.Append("&" + temp.Key + "=" + temp.Value);
            }

            try
            {
                string postText = sbUrlData.ToString();

                //string formUrl = "http://mengxj.sendinfo.com.cn/boss/service/code.htm";
                string formUrl = "http://boss.zhiyoubao.com/boss/service/code.htm";
                string formData = postText;//提交的参数

                //注意提交的编码，统一使用utf8
                byte[] postData = Encoding.UTF8.GetBytes(formData);

                //设置提交的相关参数 
                HttpWebRequest request = WebRequest.Create(formUrl) as HttpWebRequest;
                Encoding myEncoding = Encoding.UTF8;
                request.Method = "POST";
                request.KeepAlive = false;
                request.AllowAutoRedirect = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR  3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.ContentLength = postData.Length;

                // 提交请求数据 
                System.IO.Stream outputStream = request.GetRequestStream();
                outputStream.Write(postData, 0, postData.Length);
                outputStream.Close();

                HttpWebResponse response;
                Stream responseStream;
                StreamReader reader;
                string srcString;
                response = request.GetResponse() as HttpWebResponse;
                responseStream = response.GetResponseStream();
                reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
                srcString = reader.ReadToEnd();
               
                reader.Close();

                return srcString;
            }
            catch (Exception ex)
            {
                return "发送数据发送错误。错误原因：" + ex.Message;
            }
        }


        //发单
        protected void Button1_Click(object sender, EventArgs e)
        {
            var xmlMsg =""+
                        "<PWBRequest>"+
                          "<transactionName>SEND_CODE_REQ</transactionName>"+
                          "<header>"+
                            "<application>SendCode</application>"+
                            "<requestTime>{requestTime}</requestTime>" +
                          "</header>"+
                          "<identityInfo>"+
                            "<corpCode>{corpCode}</corpCode>" +
                            "<userName>{userName}</userName>" +
                          "</identityInfo>"+
                          "<orderRequest>"+
                            "<order>"+
                              "<certificateNo>{certificateNo}</certificateNo>" +
                              "<linkName>{linkName}</linkName>"+
                              "<linkMobile>{linkMobile}</linkMobile>" +
                              "<orderCode>{orderCode}</orderCode>" +
                              "<orderPrice>{orderPrice}</orderPrice>" +
                              "<payMethod>{payMethod}</payMethod>" +
                              "<scenicOrders>"+
                                "<scenicOrder>"+
                                  "<orderCode>{orderCode}</orderCode>" +
                                  "<price>{price}</price>" +
                                  "<quantity>{quantity}</quantity>" +
                                  "<totalPrice>{totalPrice}</totalPrice>" +
                                  "<occDate>{occDate}</occDate>" +
                                  "<goodsCode>{goodsCode}</goodsCode>"+
                                  "<goodsName>{goodsName}</goodsName>" +
                                "</scenicOrder>"+
                               "</scenicOrders>"+
                            "</order>"+
                          "</orderRequest>"+
                        "</PWBRequest>";

            string key = "62FEF385DA3EA23B088FA68EEB71C95C";
            var requestTime = DateTime.Now.ToString("yyyy-MM-dd");//格式2015-01-06
            var corpCode = "sdzfxysts";
            var userName = "ysts";
            var certificateNo = "";
            var linkName = "张小形";
            var linkMobile = "18256931141";
            var orderCode = "18907730019";//18907730019 //25731535
            var orderPrice = 62;
            var payMethod = "vm";
            var price = 6;
            var quantity = 2;
            var totalPrice = orderPrice = price * quantity;

            var occDate = requestTime;
            var goodsCode = "PFT20160712059475";
            var goodsName = "银子岩门票";

            var sentContent = xmlMsg;
            sentContent = sentContent.Replace("{requestTime}", requestTime);
            sentContent = sentContent.Replace("{corpCode}", corpCode);
            sentContent = sentContent.Replace("{userName}", userName);
            sentContent = sentContent.Replace("{certificateNo}", certificateNo);
            sentContent = sentContent.Replace("{linkName}", linkName);
            sentContent = sentContent.Replace("{linkMobile}", linkMobile);
            sentContent = sentContent.Replace("{orderCode}", orderCode);
            sentContent = sentContent.Replace("{orderPrice}", orderPrice.ToString());
            sentContent = sentContent.Replace("{payMethod}", payMethod);
            sentContent = sentContent.Replace("{price}", price.ToString());
            sentContent = sentContent.Replace("{quantity}", quantity.ToString());
            sentContent = sentContent.Replace("{totalPrice}", totalPrice.ToString());
            sentContent = sentContent.Replace("{occDate}", occDate);
            sentContent = sentContent.Replace("{goodsCode}", goodsCode);
            sentContent = sentContent.Replace("{goodsName}", goodsName);


            var sign = GetMd5Hash("xmlMsg=" + sentContent + key);

            SortedDictionary<string, string> data = new SortedDictionary<string, string>();
            data.Add("xmlMsg", sentContent);
            data.Add("sign", sign.ToLower());


            var returnStr = SendPost(data);
            Response.Write(returnStr);
        }

        //退款
        protected void Button2_Click(object sender, EventArgs e)
        {
            var xmlMsg =""+
                    "<PWBRequest>"+
                      "<transactionName>RETURN_TICKET_NUM_NEW_REQ</transactionName>" +
                      "<header>"+
                        "<application>SendCode</application>"+
                        "<requestTime>{requestTime}</requestTime>" +
                      "</header>"+
                    "<identityInfo>"+
                        "<corpCode>{corpCode}</corpCode>" +
                        "<userName>{userName}</userName>" +
                      "</identityInfo>"+
                      "<orderRequest>"+
                        "<returnTicket>"+
                          "<orderCode>{orderCode}</orderCode>" +
                          "<orderType>scenic</orderType>"+
                          "<returnNum>{returnNum}</returnNum>" +
                        "</returnTicket>"+
                      "</orderRequest>"+
                    "</PWBRequest>";

            var requestTime = DateTime.Now.ToString("yyyy-MM-dd");//格式2015-01-06
            var corpCode = "CESHI";
            var userName = "otaceshi";
            var orderCode = "1890773001145454";
            var quantity = 2;

            var sentContent = xmlMsg;
            sentContent = sentContent.Replace("{requestTime}", requestTime);
            sentContent = sentContent.Replace("{corpCode}", corpCode);
            sentContent = sentContent.Replace("{userName}", userName);
            sentContent = sentContent.Replace("{orderCode}", orderCode);
            sentContent = sentContent.Replace("{returnNum}", quantity.ToString());

            string key = "4B22B6C064B2D9B0D1636E1D84EAD266";
            var sign = GetMd5Hash("xmlMsg=" + sentContent + key);

            SortedDictionary<string, string> data = new SortedDictionary<string, string>();
            data.Add("xmlMsg", sentContent);
            data.Add("sign", sign.ToLower());

            var returnStr = SendPost(data);
            Response.Write(returnStr);

        }

    }
}