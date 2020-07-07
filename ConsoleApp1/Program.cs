
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        public static string Tongbi(decimal a, decimal b)
        {
            return "0";
        }

        static void Main(string[] args)
        {


            var sda123 = Math.Round(0.2543, 0);




            List<int> inList = new List<int>();
            List<Student> students = new List<Student>();
            List<Student2> students222 = new List<Student2>();
            var wolai = students222.OrderByDescending(u => u.dt.Year == 2).ThenByDescending(u => u.dt).ToList();
            students.Add(new Student
            {
                numb1 = 1
                ,
                numb2 = 3,
                numb3 = 3,
                dt = "2020-03-30 00:00:00.000"
            });
            students.Add(new Student
            {
                numb1 = 2
              ,
                numb2 = 1,
                numb3 = 3,
                dt = "2020-03-30"
            });
            students.Add(new Student
            {
                numb1 = 3
               ,
               
                numb3 = 3,
                dt = "2020-03-31"
            });

            var sadas = new Student()
            {
                numb1 = 2
               ,
                numb2 = 2,
                numb3 = 3,
                dt = "2020-03-31"
            };
            students.Insert(0, sadas);

            var testList5 =  students.OrderByDescending(u => u.numb2>0).ThenBy(u=>u.numb2).ToList();


            var testList = students.Where(u=>u.numb1==10).ToList();

            //var testList3 = testList.Where(u => u.Key == 2);
          var testList4= testList.Sum(u => u.numb1);
           // var testList2 = students.ToDictionary(u => u.numb1);

            List<Student2> students2 = new List<Student2>();
            students2.Add(new Student2
            {
                a = 1,
                b = 2,
                dt = DateTime.Now.Date
            });


            var asdasd = students.GroupJoin(students2, u => Convert.ToDateTime(u.dt), i => i.dt.Date, (u, i) => new { u, i });

           
            //inList.AddRange(testList.Select(u => u.Key));

            //foreach (var item in inList)
            //{
            //    var ca = testList.Where(u => u.Key == 1).ToList();
            //   // var sum = ca.Sum(u => u.Sum(m => m.numb2));
            //}







            int asd23 = 2016 % 4;

            string sad12312 = "123:12;13:123;12:5";
           var countspi= sad12312.Split(';').ToList()[0].Split(':');
            long long1 = 12;
            int long3 = (int)long1;
            //SkuClientSoapClient skuClient = new SkuClientSoapClient();
            List<long> vsasdw = new List<long>();
            List<KeyWord> keyWord232 = new List<KeyWord>();
            keyWord232.Add(new KeyWord());
         int Averagesa1 =   (int)keyWord232.Average(u => u.dec);


          var asda999 =  keyWord232.Select(u => (long)u.dec).ToList();


            keyWord232.Add(new KeyWord { dec=0.1m, dec1=12m, uv=1, kword=""  });
          var ads2a1 =  keyWord232.Where(u => vsasdw.Contains((long)u.uv)).ToList();
            KeyWord keyWord23 = new KeyWord();

            Type t23 = typeof(KeyWord);

            var vvalue = t23.GetProperty("kwo2rd");


            List<dynamic> dycList = new List<dynamic>();

            dycList = dycList.OrderByDescending(u => u.isStick).ThenByDescending(u => u.stickTime).ToList();




            var a123 = 45.4562.ToString("0.##");



            //string asdadaaa=  AppConifg.MonitorShop;
            List<long> jzItemList = new List<long>();
            string resoultData = HttpUtil.HttpGet2("http://www.caoam.cn/JcService/GetMonitorJzItem");
            //string resoultData = HttpUtil.HttpGet2("http://www.bk.caoam.cn/JcService/GetMonitorJzItem");
            dynamic myData = JsonConvert.DeserializeObject<dynamic>(resoultData);
            if (myData.msg == "操作成功")
            {
                foreach (var item in myData.data)
                {
                    jzItemList.Add(Convert.ToInt64(item));
                }
            }
            foreach (var item in jzItemList)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                string url1 = "https://trade-acs.m.taobao.com/gw/mtop.taobao.detail.getdetail/6.0/?data=itemNumId%22%3A%22{0}%22&callback=__jp5";
                string result11 = HttpUtil.HttpGet2(string.Format(url1, item));
                stopwatch.Stop();
                var newdata = new { jzitem = item, result = result11 };
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(newdata);
               // resoultData = HttpUtil.PostData("http://localhost:12850/JcService/MonitorJzItemChanges", data);
                //resoultData = HttpUtil.PostData("http://www.caoam.cn/JcService/MonitorJzItemChanges", data);
            }
            myData = JsonConvert.DeserializeObject<dynamic>(resoultData);

            string url = "https://trade-acs.m.taobao.com/gw/mtop.taobao.detail.getdetail/6.0/?data=itemNumId%22%3A%22{0}%22&callback=__jp5";
            string result = HttpUtil.HttpGet2(string.Format(url, "585421808782"));
            result = result.Replace("__jp5(", "").TrimEnd(')');
            dynamic returnData = JsonConvert.DeserializeObject<dynamic>(result);
            dynamic keyWords_data = returnData["data"];
            if (null == keyWords_data)
            {

            }
            var aasdpp = keyWords_data["apiStack"][0]?["value"]?.ToString();
            dynamic dicapiStack = JsonConvert.DeserializeObject<dynamic>(aasdpp);
            var sellCount = dicapiStack["price"]["shopProm"];//["price"]?["priceText"];
            foreach (var myitem in sellCount)
            {
                //upData.qudao.Add(new PublicN5<string, int, decimal, int, decimal>()
                //{
                //    N = myitem["pageName"]["value"],
                //    N2 = myitem["rivalItem1Uv"]["value"],//访客
                //    N3 = myitem["rivalItem1TradeIndex"]["value"],//交易指数
                //    N4 = myitem["rivalItem1PayByrCntIndex"]["value"],//客群指数
                //    N5 = myitem["rivalItem1PayRateIndex"]["value"]//支付转化指数
                //});
            }
            var asdasdq = dicapiStack["asd"];
            if (keyWords_data["apiStack"] == null)
            {
            }
            dynamic dicdynamicm = keyWords_data["item"]["images"];
            List<string> asdList = new List<string>();

            foreach (var item in dicdynamicm)
            {
                asdList.Add(item.ToString());
            }
            string dicItem = keyWords_data["item"]["images"]?.ToString().Replace("[", "").Replace("]", "").Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            //var title12 = dicItem["title"];
            dicItem = string.IsNullOrEmpty(dicItem) ? "" : dicItem;
            var dicItemList = dicItem.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var item in dicItemList)
            {
                var testmap = dicItemList.Count() > 8 ? dicItemList[7] : "";
            }


            dynamic skus_data = keyWords_data["item"];
            JArray jarray;
            jarray = skus_data;
            if (jarray == null)
            {

            }
            foreach (JToken j in jarray)
            {
                var asda121 = j["propPath"].ToString();
                //LostDetailDto JZLostDetail = new LostDetailDto();
                //JZLostDetail.item = j["itemId"]["value"] == null ? 0 : Convert.ToInt64(j["itemId"]["value"].ToString());
            }
            dynamic props_data = keyWords_data["skuBase"]["props"];
            jarray = props_data;
            if (jarray == null)
            {
            }

            ////生意参谋解密
            //var str = CookieHelper.sk("D86E64E948F99333ADACCD676779B37A0153C88A84641E158EE53219BB0A0FCDB5EBE12823D9AA9BA108FA2AD1ED7A19BD4AA858014E1A9AC8B5F699368902E7F92A416566959D70D91FCD5C8FABBBAAF1AFFD11819D4FA56842116E5966FEABE55CF8D99D1A614130BCCB7349AD55B07B71FF1954030FFD2878F448A54FB3D8");

            ////请求接口
            //string resoultData = HttpUtil.HttpGet2("http://www.bk.caoam.cn/JCApiCore/TestMethod");
            //dynamic myData = JsonConvert.DeserializeObject<dynamic>(resoultData);




            using (FileStream stream = new FileStream("D:\\lo12.txt", FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine("asdasd");
            }





            {

            }


            // WriteLogs("lll");
            List<string> vsasd = new List<string>();
            vsasd.Add("2020-05-17 12:00:00");
            vsasd.Add("2020-05-16 12:00:00");
            vsasd.Add("2020-05-18 12:00:00");
            vsasd.Add("2020-05-17 12:01:58");
            vsasd.Add("2020-05-17 12:05:00");
            vsasd = vsasd.OrderByDescending(u => u).ToList();
            decimal de12 = -12.56m;

            string rateas = Math.Round(-0.1m / 100, 4).ToString("P");


            DateTime dateTime1 = Convert.ToDateTime("2020-05-17 12:00:00");
            DateTime dateTime2 = Convert.ToDateTime("2020-05-11 12:00:00");
            var xiangcha1 = (dateTime2 - dateTime1).TotalDays;

            Dictionary<string, int> keyValuePairs1 = new Dictionary<string, int>() { { "asd", 1 }, { "ads", 2 } };
            var json1 = JsonConvert.SerializeObject(keyValuePairs1);



            int asd123 = 15;


            int a = 1;

            int b = 1;

            int n;  //　　声明一个变量用来定义数列的长度;

            for (int i = 2; i < 5; i++)
            {

                b = a + b;    //　　得到b的值;

                a = b - a;    //　　得到上一次a的值;

            }











            List<Student2> listNewss1 = new List<Student2>();

            listNewss1.Add(new Student2() { dt = DateTime.Now });

            var datet1 = DateTime.Now.Date;
            listNewss1 = listNewss1.Where(u => u.dt.Date == datet1).ToList();
            List<int> cids = new List<int>();
            string oPlanItemConfig = ",";
            foreach (var dt in oPlanItemConfig.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList())
            {
                cids.Add(Convert.ToInt32(dt));
            }

            var keyword1 = "asd1111";

            var props = typeof(KeyWord);
            KeyWord keyWordtest = new KeyWord() { kword = "ada", uv = 12 };
            props.GetProperty("dec1").SetValue(keyWordtest,12m);
            int decuv = (int)keyWordtest.dec;
            var props2 = typeof(KeyWord);
            dynamic dyc = new { title = "asdasd", avg = 12 };

            var asdasd1231 = props2.GetProperty("uv").GetValue(keyWordtest) == null ? 0 : Convert.ToDecimal(props2.GetProperty("uv").GetValue(keyWordtest));

            var asd1231 = props2.GetProperty("uv").PropertyType.Name == "Int32" ? Convert.ToInt32(props2.GetProperty("uv").GetValue(keyWordtest)) : Convert.ToDecimal(props2.GetProperty("uv").GetValue(keyWordtest));
            var asd12312 = props2.GetProperty("dec").PropertyType.Name == "Int32" ? Convert.ToInt32(props2.GetProperty("dec").GetValue(keyWordtest)) : Convert.ToDecimal(props2.GetProperty("dec").GetValue(keyWordtest));
            foreach (var item in props.GetProperties())
            {
                var attr = item.GetCustomAttribute(typeof(DisplayNameAttribute));
                if (attr != null)
                {
                    var displayName = ((DisplayNameAttribute)attr).DisplayName;
                    if (displayName == "测试")
                    {
                        var getvalue = item.GetValue(keyWordtest);
                        item.SetValue(keyWordtest, keyword1);
                    }

                }
            }

            //var propValue = props.GetProperty("").GetValue(keyWordtest);











            int asd2 = 5 % 100;

            Dictionary<string, string> dicList = new Dictionary<string, string>() { { "流量指数", "shop_uvIndex" }, { "收藏人气", "shop_cltHits" }, { "加购人气", "shop_cartHits" }, { "支付转化指数", "shop_payRateIndex" }, { "交易指数", "shop_tradeIndex" }, { "客群指数", "shop_payByrCntIndex" }, { "预售定金指数", "shop_preTradeIndex" }, { "预售定金商品件数", "shop_preSellItmCnt" }, { "上新商品数", "shop_fstOnsItmCnt" } };

            //var sad122 = dicList.ContainsKey["ads"];


            var descimal = (Convert.ToDecimal("-0.2848106949405796") * 100);


            List<KeyWord> kWordList = new List<KeyWord>();

            kWordList.Add(new KeyWord() { kword = "下", uv = 2 });
            kWordList.Add(new KeyWord() { kword = "上", uv = 2 });
            kWordList.Add(new KeyWord() { kword = "中间", uv = 2 });
            kWordList.Add(new KeyWord() { kword = "全部", uv = 2 });


            var asdads1 = kWordList.Where(u => u.kword == "asdad").ToList();
            var ads124 = asdads1.Sum(u => u.uv);

            List<string> oredrList = new List<string>() { "上", "全部", "中间", "下" };

            foreach (var item in oredrList)
            {
                kWordList = kWordList.OrderBy(u => u.kword == item).ToList();
            }







            Assembly assembly1 = Assembly.Load("testModel");
            var assemblyClass = assembly1.CreateInstance("testModel.AssemblyClass");

            var asProperties = assemblyClass.GetType().GetProperties();

            //AssemblyClass obj = (AssemblyClass)Activator.CreateInstance(assemblyClass.GetType());


            //var asdas123 = obj.Name;







            //var keyWords_json = JsonConvert.DeserializeObject<List<dynamic>>(jsonstr);
            //JArray jarray;
            //jarray = keyWords_json;
            List<dynamic> listNew = new List<dynamic>();
            //foreach (JToken j in jarray)
            //{
            //    listNew.Add(JObject.Parse(j.ToString()));
            //}

            Console.WriteLine(Getjnt(5));
            List<Student2> listNewss = new List<Student2>();

            var asda1 = listNewss.Where(u => u.b == 1).Sum(u => u.ba);

            Student2 student22 = new Student2();
            if ((int)student22.ba > 0)
            {

            }














            var enumTests = (int)enumTest.你好;
            string strEnum = enumTest.你好.ToString();
            Console.WriteLine(enumTest.你好.ToString());
            string str1 = "22";
            string str2 = "22";
            if (str1.Equals(str2))
            {
                str1 = "asd";
            }
            int[] arrayints = new int[] { 0, 56, 1 };
            arrayints[2] = 12;
            var asdadaaqq = arrayints[3];
            Student2 students3 = new Student2();
            List<Student2> students4 = null;
            var students4students4 = students4.Where(u => u.b == 112).FirstOrDefault();






            students3.a = 12;
            students3 = students4[0];
            var asdasdasdw = students3.a;
            int asdasdasd1 = students3.ba.Value;
            var asdasasdad = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            var wolaile = DateTime.Now.Date;
            var asdasdasd = asdasasdad.Substring(asdasasdad.IndexOf("星期") + 2);
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            weekOfYear = weekOfYear - 1;
            int week = (int)DateTime.Now.DayOfWeek;

            var dt1 = Convert.ToDateTime("2020-02-03 12:00:00");
            var dat2 = DateTime.Now - dt1;
            string xiangcha = dat2.Days + "天" + dat2.Hours + "小时";
            //var dt2 = "2020-03-30";
            //var dt1 = "2020-03-30";
            //var dt1 = "2020-03-30";
            var newdt1 = dt1.AddDays(-6);


            // string cc = "adasd";
            //var bb=cc is int;
            //var aa = cc.Substring(6) == null ? null : cc.Substring(6);

            //int a = 1;
            //int b = a;
            //a = 5;
            //string aa = "asd";
            //string bb = aa;
            //aa = "asdasd";
            //Console.WriteLine(b);
            //Console.WriteLine(bb);
            //List<string> nameList =new List<string>() { "大盘-搜索占比", "大盘-搜索人均点击量", "大盘-转化率", "大盘-人均浏览量", "大盘-UV价值", "大盘-加购率", "大盘-收藏率" };

            //double aa = Math.Round((30.739921950481922775- 31.591308317886502036)/ 31.591308317886502036,4);
            //Console.WriteLine(aa);
            //if (nameList.Contains("大盘-搜 索占比")) {
            //    Console.WriteLine("adsasd");
            //}


            //string cc = "按摩内裤女震动 上班";
            //if (cc.Length >= 4){
            //    Console.WriteLine(cc.Length);

            //}else {
            //    Console.WriteLine(cc.Length);

            //}
            //int a = 5;
            //int c = 6;
            //if (a == 5 && c == 1) {
            //    Console.WriteLine("asdsd");
            //}
            //DateTime dateTime = new DateTime(2019, 10, 31);
            //var cc = dateTime.AddMonths(-1);
            // var dd = cc.AddMonths(-1);
            //var vv = dd.AddMonths(-1);
            //var ww = vv.AddMonths(-1);
            //int? a = null;
            //int cc = 5* (int)a;
            //Console.WriteLine(cc);
            //Console.WriteLine(5/3);
            //Console.WriteLine(Convert.ToDouble(null));
            //Console.WriteLine((22- Convert.ToDouble(null))/ Convert.ToDouble(null));
            //Console.WriteLine(Convert.ToDouble("25.66324234234234234423424"));
            //Console.WriteLine(Convert.ToInt32(25.26m));
            //List<string> strList = new List<string>() {"123","23","3" };
            //List<string> cstrList = new List<string>() { "1", "13", "3" };
            //var newList = strList.Where(u=> cstrList.Exists(t => t ==u)).ToList() ;
            //string testStr = "asda:asd:";
            //var cc = testStr.Split(',').Length;

            //var sc = testStr.Split(':');

            //int a = 5;
            //string b= "0/" + a;

            //var b = DateTime.Now.Date;
            //Dictionary<int, Dictionary<int, Dictionary<int,int>>> keyValuePairs = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
            //Dictionary<int, Dictionary<int, int>> cc = new Dictionary<int, Dictionary<int, int>>();

            //Dictionary<int, int> keyValues = new Dictionary<int, int>();
            //keyValues.Add(5, 5);
            //keyValuePairs.Add(5, cc);
            //foreach (var item in keyValuePairs.Values)
            //{
            //    item.Add(2, 2);
            //}
            //Console.WriteLine(b);
            //int? c = null;
            //var cc = c > 0;
            // List<Student> students=new List<Student>();
            // students.Add(new Student {
            //  numb1=0
            // });
            // students.Add(new Student
            // {
            //     numb1 = 1
            // });
            //var  cc = students.Sum(u => u.numb2).ToString();
            string asda2sd = "2008/9/4";
            //"2008-9-4"
            var asdads = Convert.ToDateTime(asda2sd.Replace("/", "-") + " " + "00:19:14.000");

            //var asdasd = DateTime.Now.Date;
            var testde = Convert.ToDecimal(null);
            int? asdadsad = null;
            string asda = asdadsad.ToString();
            Student student = new Student();
            Console.WriteLine(student.numb1);
            var asdas = Convert.ToDecimal("a");

            List<string> sssss = new List<string>();
            string cc = null;
            sssss.AddRange(cc.Split('|'));
            //dynamic cc = 5521321.43541125453453435453435434;
            //string ccc = cc.ToString();
            //Convert.ToInt64("58741065301666605707091933");
            //Student student1 = new Student() { 
            //};
            //student.numb1 = 5;
            //student.numb2= student.numb1+12;
            //List<Student> students = new List<Student>();
            //string aa = "asd";
            //aa.TrimStart()
            //Console.WriteLine(student.numb2);
            Console.ReadLine();
            Console.WriteLine("Hello World!");

        }
        public static int Getjnt(int c, int? a = 1)
        {
            return a.Value;
        }
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="fileName"></param>
        public static void WriteLogs(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + fileName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMM") + ".txt";
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    sw.WriteLine("1成功");
                    sw.WriteLine("-----------------华丽的分割线-----------------------");
                    sw.Close();
                }
            }
        }
    }
    public class Student

    {
        public int numb1 { get; set; }
        public int? numb2 { get; set; }
        public int numb3 { get; set; }
        public int numb4 { get; set; }
        public string dt { get; set; }
    }
    public class Student2

    {
        public int a { get; set; }
        public int b { get; set; }
        public int? ba { get; set; }
        public DateTime dt { get; set; }
    }
    public class KeyWord
    {
        [DisplayName("测试")]
        public string kword { get; set; }
        public int uv { get; set; }
        public decimal dec { get; set; }
        public decimal? dec1 { get; set; }
    }

    public enum enumTest
    {
        你好 = 0,
        早 = 1,
        拜拜 = 2
    }


}
