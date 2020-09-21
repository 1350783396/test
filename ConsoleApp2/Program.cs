using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {


        static void Main(string[] args)
        {
          

            {
                Stopwatch watch = Stopwatch.StartNew();
                int w1 = 1;
                int w2 = 10;
                var w4 = 0;

                var tasks = new List<Task>();

                while (w1 < w2)
                {


                    int w7 = 0;
                    for (int i = 1; i < 1000000000; i++)
                    {
                        w7 = w7 + i;
                        //TaskClass task = new TaskClass();
                        //w4.Add(task);
                    }
                    w4 = w4 + w7;
                    w1 += 1;
                }
               // Task.WaitAll(tasks.ToArray());
                watch.Stop();

                Console.WriteLine($"耗时：{watch.ElapsedMilliseconds}总数为{w4}");//49999995

            }







            Console.ReadKey();







            int asdad1 = Convert.ToInt32(null);
            var datet = DateTime.Now;
            var startTime = new DateTime(datet.Year, datet.Month, 1);
            var endTime = startTime.AddMonths(1).AddDays(-1);

            double asd13 = double.PositiveInfinity;

            int num2 = Convert.ToInt32(5.6);
            int num3 = Convert.ToInt32(6.6);

            string url = "https://trade-acs.m.taobao.com/gw/mtop.taobao.detail.getdetail/6.0/?data=itemNumId%22%3A%22{0}%22&callback=__jp5";
            string str = HttpUtil.HttpGet2(string.Format(url, 595867371115));


            ServiceReference2.WebServiceTestSoapClient hello = new ServiceReference2.WebServiceTestSoapClient();
            SkuService.SkuClientSoapClient skuClientSoapClient = new SkuService.SkuClientSoapClient();

            string strItem = "599255126854,571556396743,611533793238,595546630465,599863833796,39343907748,612309805521,15309007203,555119643448,595305266485,585500627308,545594992098,606981332495,568036222499,561312940844,610889003362,612558679762,43435767108,538485180808,42795234640,535854761057,580034690117,520021661413,613481112799,610454732131,596912025175,523004082110,605986864982,579124035448,614309073920,611322255972,38614806551,575922228075,555185948746,614149951254,547686387122,17524300587,559566340990,602722592327,521098149180,43535678639,528295987557,540899394050";
            string sad123 = "";
            foreach (var item in strItem.Split(',').ToList())
            {
                var asdaz1 = skuClientSoapClient.GetSkuDetail(617462493511);
                if (asdaz1.Contains("message"))
                {
                    sad123 = asdaz1;
                }
                System.Threading.Thread.Sleep(1000);
            }


            //hello.WebServiceTest webServiceTest1 = new hello.WebServiceTest();
            //asdaz1 = webServiceTest1.HelloWorld();
            //hrllo.WebServiceTest webServiceTest = new hrllo.WebServiceTest();
            //asdaz1 = webServiceTest.HelloWorld();

            ConfigurationManager.RefreshSection("MonitorShop");

            string MonitorShop = ConfigurationManager.AppSettings["MonitorShop1"];
            string strShop = MonitorShop;
            MonitorShop = ConfigurationManager.AppSettings["MonitorShop"];
            //string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //string FilePath = "D:";
            //string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source="
            //     + FilePath //execel路径
            //     + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=0'";
            //DataSet ds = new DataSet();

            ////Sheet1工作表名称
            //OleDbDataAdapter oada = new OleDbDataAdapter("select * from [test$]", strConn);
            //oada.Fill(ds);
            DateTime dateTime1 = DateTime.Now;
            var asdasdadasd = DateTime.TryParse("17:48:54", out dateTime1);



            string fasdf = "[{\"日期\":\"3/20/20\",\"开始时间\":\"3/20/20\",\"结束时间\":\"3/20/20\",\"买家旺旺\":\"代贞菊66\",\"客服旺旺\":\"小娇\",\"接待类型\":\"直接接待\",\"接待发起\":\"买家发起\",\"状态\":\"落实下单落实付款\"},{\"日期\":\"3/20/20\",\"开始时间\":\"3/20/20\",\"结束时间\":\"3/20/20\",\"买家旺旺\":\"cccjjj007\",\"客服旺旺\":\"小娇\",\"接待类型\":\"直接接待\",\"接待发起\":\"买家发起\",\"状态\":\"待定\"},{\"日期\":\"3/20/20\",\"开始时间\":\"3/20/20\",\"结束时间\":\"3/20/20\",\"买家旺旺\":\"ann1229915\",\"客服旺旺\":\"小娇\",\"接待类型\":\"直接接待\",\"接待发起\":\"买家发起\",\"状态\":\"待定\"},{\"日期\":\"3/20/20\",\"开始时间\":\"3/20/20\",\"结束时间\":\"3/20/20\",\"买家旺旺\":\"真水无香小中华\",\"客服旺旺\":\"小娇\",\"接待类型\":\"直接接待\",\"接待发起\":\"买家发起\",\"状态\":\"待定\"},{\"日期\":\"3/20/20\",\"开始时间\":\"3/20/20\",\"结束时间\":\"3/20/20\",\"买家旺旺\":\"杨方2010\",\"客服旺旺\":\"小娇\",\"接待类型\":\"直接接待\",\"接待发起\":\"买家发起\",\"状态\":\"待定\"}]";

            dynamic tradingIndexData_json = JsonConvert.DeserializeObject<dynamic>(fasdf);
            foreach (var item in tradingIndexData_json)
            {
                var asdads = Convert.ToDateTime("3/20/20 00:00:01");
            }
            var asd = 343;
            //var datas = tradingIndexData_json[upSCrowd.keyWord];//交易指数
            //if (datas == null)
            //{

            //}
            //searchCrowd.tradingIndex = Convert.ToDecimal(datas["allValue"]);//交易指数
            //tradingIndexData_str = CookieHelper.sk(upSCrowd.genderData);/// 性别分布
            //tradingIndexData_json = JsonConvert.DeserializeObject<dynamic>(tradingIndexData_str);
            //datas = tradingIndexData_json[upSCrowd.keyWord];/// 性别分布
            //if (datas == null)
            //{

            //}
            // JArray jArray = datas["dataList"];
            //datas = datas["dataList"];
            //foreach (var item in datas)
            //{
            //    if (item["key"] == "女")
            //    {
            //        searchCrowd.gGirl = Convert.ToDecimal(item["value"]);
            //    }
            //    else if (item["key"] == "男")
            //    {
            //        searchCrowd.gMan = Convert.ToDecimal(item["value"]);
            //    }
            //    else if (item["key"] == "未知")
            //    {
            //        searchCrowd.gXX = Convert.ToDecimal(item["value"]);
            //    }
            //}



            //DataTable data = ds.Tables[0];//数据源表
            //using (SqlConnection con = new SqlConnection(conStr))
            //{
            //    //使用Bulk批量插入大数据
            //    Stopwatch sw = new Stopwatch();//运行时间
            //    SqlBulkCopy bulkCopy = new SqlBulkCopy(con);
            //    bulkCopy.DestinationTableName = "mainUser"; //数据库表名
            //    bulkCopy.BatchSize = data.Rows.Count;
            //    con.Open();
            //    sw.Start();//开始计时

            //    bulkCopy.WriteToServer(data);
            //    sw.Stop();
            //    Console.WriteLine(string.Format("插入{0}条记录共花费{1}毫秒，{2}分钟", data.Rows.Count, sw.ElapsedMilliseconds, Convert.ToInt32(sw.ElapsedMilliseconds) / 60 / 60));

            //}
        }
    }
}
