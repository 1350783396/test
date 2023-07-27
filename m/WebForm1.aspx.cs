using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;

namespace ETicket.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string xml = "<vmarket_eticket_send_response><ret_code>1</ret_code></vmarket_eticket_send_response>";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            var note = xmlDoc.SelectSingleNode("/vmarket_eticket_send_response/ret_code");
            if (note != null && note.InnerText.Trim() == "1")
            {
                Response.Write(note.InnerText.Trim());
            }
            else
            {

            }

        }
    }
}