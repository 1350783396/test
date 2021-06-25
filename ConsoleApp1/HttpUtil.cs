using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    public static class QueryableExtension
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName)
        {
            return _OrderBy<T>(query, propertyName, false);
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName)
        {
            return _OrderBy<T>(query, propertyName, true);
        }

        static IOrderedQueryable<T> _OrderBy<T>(IQueryable<T> query, string propertyName, bool isDesc)
        {
            string methodname = (isDesc) ? "OrderByDescendingInternal" : "OrderByInternal";

            var memberProp = typeof(T).GetProperty(propertyName);

            var method = typeof(QueryableExtension).GetMethod(methodname)
                                       .MakeGenericMethod(typeof(T), memberProp.PropertyType);

            return (IOrderedQueryable<T>)method.Invoke(null, new object[] { query, memberProp });
        }
        public static IOrderedQueryable<T> OrderByInternal<T, TProp>(IQueryable<T> query, System.Reflection.PropertyInfo memberProperty)
        {//public
            return query.OrderBy(_GetLamba<T, TProp>(memberProperty));
        }
        public static IOrderedQueryable<T> OrderByDescendingInternal<T, TProp>(IQueryable<T> query, System.Reflection.PropertyInfo memberProperty)
        {//public
            return query.OrderByDescending(_GetLamba<T, TProp>(memberProperty));
        }
        static Expression<Func<T, TProp>> _GetLamba<T, TProp>(System.Reflection.PropertyInfo memberProperty)
        {
            if (memberProperty.PropertyType != typeof(TProp)) throw new Exception();

            var thisArg = Expression.Parameter(typeof(T));
            var lamba = Expression.Lambda<Func<T, TProp>>(Expression.Property(thisArg, memberProperty), thisArg);

            return lamba;
        }
    }

    public class HttpUtil
    {
        public static string HttpGet(string url)
        {
            try
            {
                var myRequest  = (HttpWebRequest)WebRequest.Create(url);
                myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322)";
                myRequest.Method = "GET";
                using (WebResponse wr = myRequest.GetResponse())
                {

                    string content = new StreamReader(wr.GetResponseStream(), Encoding.UTF8).ReadToEnd();

                    return content;
                }
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }
        public static string HttpGet2(string url)
        {
            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322)";
                myRequest.Method = "GET";
                myRequest.Timeout = 60000;
                myRequest.Headers.Add("cookie", "t=476e879a877668bce81a02e4ccd7d458; _tb_token_=363455e89638e; _m_h5_tk=8f084d48cbd00840062ed6352383f10a_1603883242868; _m_h5_tk_enc=4cef010064a865e49cf47da68d4d5908; unb=2200730404342; sn=%E6%99%BA%E5%8D%A1%E6%95%B0%E7%A0%81%E4%B8%93%E8%90%A5%E5%BA%97%3A%E9%98%BF%E7%9E%92; csg=547e42ef; skt=ca50b1bff5f9fea3; _cc_=U%2BGCWk%2F7og%3D%3D; cna=wNMfGPrg0DMCAXrpqErF7gb4; uc1=cookie21=UtASsssmfufd&cookie14=Uoe0bkFCroxgxw%3D%3D; v=0; tfstk=cdacB00ProofsYouAZgb98I_QpBRajLZKPz7zzPQMl-iwLargsDD4HSXM_D0nPK1.; l=eBjNWoVlOAFS5fcSBOfwourza77OSIRAguPzaNbMiOCP_yCe51qlWZW1rqTwC3GVh6VBR3JSqGkXBeYBq3tSnxvtIosM_Ckmn; isg=BNXVDeeGJo3tigLx7riNkbfZ5NGP0onk-3Tkxld6-sybrvWgHyTYtOFmfLIYrqGc");

                using (WebResponse wr = myRequest.GetResponse())
                {

                    string content = new StreamReader(wr.GetResponseStream(), Encoding.UTF8).ReadToEnd();

                    return content;
                }
            }
            catch (WebException ex)
            {

                return new StreamReader(ex.Response.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            }
        }
        public static String PostDataSim(string url, string data, CookieContainer cookie)
        {
            HttpWebResponse response = null;
            try
            {
                // throw new Exception(data);
                byte[] postdata = Encoding.UTF8.GetBytes(data);

                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.92 Safari/537.36";
                myRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                myRequest.CookieContainer = cookie;
                myRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
                myRequest.Referer = "https://subway.simba.taobao.com/";
                myRequest.Timeout = 60000;
                Stream newStream = myRequest.GetRequestStream();

                newStream.Write(postdata, 0, postdata.Length);
                newStream.Close();
                // Get response
                response = (HttpWebResponse)myRequest.GetResponse();


                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }


        }
        public static String PostData(string url, string data)
        {
            HttpWebResponse response = null;
            try
            {
                // throw new Exception(data);
                byte[] postdata = Encoding.UTF8.GetBytes(data);

                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                myRequest.Timeout = 60000;
                Stream newStream = myRequest.GetRequestStream();

                newStream.Write(postdata, 0, postdata.Length);
                newStream.Close();
                // Get response
                response = (HttpWebResponse)myRequest.GetResponse();


                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }


        }
    }
}
