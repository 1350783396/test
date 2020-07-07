using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
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
