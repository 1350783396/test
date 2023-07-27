using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// search_product 的摘要说明
    /// </summary>
    public class search_product : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var t = PubFun.QueryString("t");
            var g = PubFun.QueryString("g");
            StringBuilder sb = new StringBuilder(" 1=1 ");
            if (g != "")
                sb.AppendFormat(" and it.CategoryID='{0}'", g);
            if(t!="")
                sb.AppendFormat(" and it.ProductName like '%{0}%'", t);
           
            ETicket.Utility.PageInfo<EFEntity.Product> pi = null;
            pi = BLL.ProductBLL.Instance.GetPageList(1, 100, sb.ToString(), "it.AddTime DESC");
            var str = ETicket.Web.HtmlController.Instance.ListProduct(pi.List, "/templ/index/product.html");
            if (str == "")
                str = "<h1>没有搜索到相关记录，请重新输入搜索条件！</h1>";

            context.Response.Write(str);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}