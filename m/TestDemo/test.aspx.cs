using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;

namespace ETicket.Web.TestDemo
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var s = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><PWBResponse><transactionName>SEND_CODE_RES</transactionName><code>0</code><description>成功</description><orderResponse>" +
    "<order><certificateNo></certificateNo><linkName>张小形</linkName><linkMobile>18256931141</linkMobile><orderCode>201606290006739817</orderCode><orderPrice>12</orderPrice><payMethod>third_vm</payMethod>" +
      "<assistCheckNo>01569301</assistCheckNo><src>interface</src><scenicOrders><scenicOrder><orderCode>86787687878787878789999990</orderCode>" +
          "<totalPrice>12</totalPrice><price>6</price><quantity>2</quantity><occDate>2016-06-29 00:00:00</occDate><goodsCode>PST20160315008871</goodsCode><goodsName>旅游测试票</goodsName></scenicOrder>" +
      "</scenicOrders></order></orderResponse></PWBResponse>";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(s);
            var nodeCode= xmlDoc.SelectSingleNode("/PWBResponse/code");
            var hodeChkCode = xmlDoc.SelectSingleNode("/PWBResponse/orderResponse/order/assistCheckNo");

            Response.Write(nodeCode.InnerText);
            Response.Write(hodeChkCode.InnerText);
        }
    }
}