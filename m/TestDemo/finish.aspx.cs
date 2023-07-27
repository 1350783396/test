using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries;

using System.IO;
using System.Text;
using System.Net;
using System.Security.Cryptography;


namespace ETicket.Web.TestDemo
{
    public partial class finish : System.Web.UI.Page
    {
        string key = "4B22B6C064B2D9B0D1636E1D84EAD266";
        protected void Page_Load(object sender, EventArgs e)
        {
            NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "info_", this.Request.RawUrl);

            var order_code = PubFun.QueryString("order_code");
            var status = PubFun.QueryString("status");
            var checkNum = PubFun.QueryString("checkNum");
            var returnNum = PubFun.QueryString("returnNum");
            var total = PubFun.QueryString("total");
            var sign = PubFun.QueryString("sign");

            if(order_code==""&&sign==""&&status=="")
            {
                NJiaSu.Libraries.LogHelper.LogResult("yzy_jk","fail_","参数为空");
                return;
            }

            var md5= GetMd5Hash("order_code=" + order_code + key).ToLower();
            if(md5!=sign)
            {
                NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "fail_", "签名不匹配我方：" + md5 + "|sign："+sign);
                return;
            }

            NJiaSu.Libraries.LogHelper.LogResult("yzy_jk", "succ_", "回调成功");
            Response.Write("success");

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
    }
}