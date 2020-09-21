﻿using System;
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
                myRequest.Timeout = 60000;
                myRequest.Headers.Add("cookie", "t=1808081c2af89cdada7b1966c047b2a9; _tb_token_=e7f3e6036733b; _m_h5_tk=e4c9dd9084f43d3ce30f573f71d6f99b_1600339020742; _m_h5_tk_enc=7e5a287bb77d974c17fd75447d7be0db; unb=2200804602352; sn=%E6%9C%A8%E9%9F%A9%E6%97%97%E8%88%B0%E5%BA%97%3A%E6%9B%BC%E6%9B%BC; csg=63d2a4c2; skt=cde6190c082e128e; _cc_=V32FPkk%2Fhw%3D%3D; cna=Z9DoF+rDElICASQYz0No29Wa; uc1=cookie14=Uoe0bUt%2BIRlB7A%3D%3D&cookie21=V32FPkk%2FhSg%2F; tfstk=cRJlBPXqnQ5SUg5D1Y6WfbtEc2RhZdwFSpJX3KDdHbYkwIJVi6V4_bZ-maYkpW1..; l=eBEM1njqOJ5cxTbkXOfwnurza77OIIRAguPzaNbMiOCP9WCW5ztCWZr083LXCnGVh6j6R3JSqGkXBeYBqIb981IhwmCj_fHmn; isg=BEZGIT42ZTKRMjHWfb_4QssVlzzIp4ph9Bk3OzBvImlEM-ZNmDUIcUvBD2__m4J5");

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
