using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using NJiaSu.Libraries;

namespace ETicket.Web.ajax
{
    /// <summary>
    /// search_news 的摘要说明
    /// </summary>
    public class search_news : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var t = PubFun.QueryString("t");
            var g = PubFun.QueryString("g");

            //分销系统内部公告
            StringBuilder sb = new StringBuilder(" it.ModuleID!=1 ");
            if (g != ""&&PubFun.IsUInt(g))
                sb.AppendFormat(" and it.ModuleID='{0}'", g);
            if (t != "")
                sb.AppendFormat(" and it.ArtTitle like '%{0}%'", t);

            ETicket.Utility.PageInfo<EFEntity.ArtContent> pi = null;
            pi = BLL.ArtContentBLL.Instance.GetPageList(1, 100, sb.ToString(), "it.AddTime DESC");
            var str = ETicket.Web.HtmlController.Instance.ListArt(pi.List, 500, "/templ/index/tab_art.html");
            if (str == "")
                str = "<h1>没有搜索到相关记录，请重新输入搜索条件！</h1>";
            else
                str = "<table class=\"table table-hover\">" + str + "</table>";

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