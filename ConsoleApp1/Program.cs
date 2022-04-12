
using CardReader;
using JiaMi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public enum QType
    {
        我来了,
        好
    }


    class Program
    {
        public static QType Gasadad(QType q)
        {
            return q;
        }

        //public static void WriteLog(string strLog)
        //{
        //    string sFilePath =  @"D:\jcLog\" + DateTime.Now.ToString("yyyyMM");
        //    string sFileName = "rizhi" + DateTime.Now.ToString("dd") + ".log";
        //    sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
        //    if (!Directory.Exists(sFilePath))//验证路径是否存在
        //    {
        //        Directory.CreateDirectory(sFilePath);
        //        //不存在则创建
        //    }
        //    FileStream fs;
        //    StreamWriter sw;
        //    if (System.IO.File.Exists(sFileName))
        //    //验证文件是否存在，有则追加，无则创建
        //    {
        //        fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
        //    }
        //    else
        //    {
        //        fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
        //    }
        //    sw = new StreamWriter(fs);
        //    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + strLog);
        //    sw.Close();
        //    fs.Close();
        //}

        public static void WriteLog(string strLog)
        {
            string sFilePath = "d:\\jcLog\\" + DateTime.Now.ToString("yyyyMM");
            string sFileName = "rizhi" + DateTime.Now.ToString("dd") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists("d:"))
            {
                sFilePath = "e:\\jcLog\\" + DateTime.Now.ToString("yyyyMM");
            }
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            FileStream fs;
            StreamWriter sw;
            if (System.IO.File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + strLog);
            sw.Close();
            fs.Close();
        }
        public static readonly object _lock = new object();

        /// <summary>
        /// 读取文本文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetText(string path)
        {



            string asd = "";
            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;

                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        asd = asd + line;
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return asd;
        }

        public class Cs1
        {
            public virtual void V() { }

        }
        public abstract class Cs2
        {
            public virtual void V() { }
            public abstract void V2();
        }

        public class Cs3 : Cs1
        {

            public override void V() { }
        }
        private static int Getaa(KeyWord2 word2)
        {
            word2.dec = 0;
            return 0;
        }
        public static T DeepCopyByXml<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = xml.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }
        public static int Testaa(int a, int b = 0)
        {
            var aa = a + b;
            return aa;

        }
        /// <summary>
        /// 返回sql建表语句
        /// </summary>
        /// <returns></returns>
        public static string GetSqlCreateTable()
        {
            string str = @"public class PreServiceData
    {
     public int id { get; set; }
        /// <summary>
        /// 上传人
        /// </summary>
        public long userid { get; set; }
        public int shopId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createDate { get; set; }
        [Description('日期')]
        public DateTime thedate { get; set; }
        [Description('店铺')]
        public string shopName { get; set; }
        [Description('旺旺')]
        public string wangwang { get; set; }
        [Description('销售额')]
        public decimal? xiaoshoue { get; set; }
        [Description('销售量')]
        public int? xiaoshouliang { get; set; }

        [Description('客单价修')]
        public decimal? kedanjiaxiu { get; set; }
        [Description('客件数修')]
        public decimal? kejianshu { get; set; }
        [Description('接待人数')]
        public int? jiedairenshu { get; set; }
        [Description('询单人数延')]
        public int? xundanrenshuyan { get; set; }
        [Description('付款流失人数')]
        public int? fukuanliushirenshu { get; set; }
        [Description('询单->最终付款人数延')]
        public int? xdzuizhongfukuanrenshu { get; set; }
        [Description('询单->最终付款成功率延')]
        public string xdzuizhongfukuanchenggonglv { get; set; }

        [Description('未回复人数')]
        public int? weihuifurenshu { get; set; }
        [Description('平均响应(秒)')]
        public decimal? pingjunxiangying { get; set; }
        [Description('买家消息数')]
        public int? maijiaxiaoxishu { get; set; }
        [Description('客服消息数')]
        public int? kefuxiaoxishu { get; set; }
        [Description('答问比')]
        public string dawenbi { get; set; }
        [Description('成交笔数')]
        public int? chengjiaobishu { get; set; }
        [Description('退款笔数')]
        public int? tuikuanbishu { get; set; }
        [Description('完成退款金额')]
        public decimal? wanchengtuikuanjine { get; set; }
        [Description('退款率')]
        public string tuikuanlv { get; set; }
    }
    ";
            str = Regex.Replace(str, @"[\r\n]", "");
            string sqlhead = "";
            string sqlbody = "";
            for (int i = 0; i < str.Length; i++)

                if (str[i] == 'p' && str[i + 1] == 'u' && str[i + 2] == 'b' && str[i + 3] == 'l' && str[i + 4] == 'i' && str[i + 5] == 'c')
                {
                    if (i == 0)
                    {
                        i += 13;
                        for (; ; )
                        {
                            if (str[i] == '{') break;
                            sqlhead += str[i];
                            i++;
                        }
                    }
                    else
                    {
                        string type = "";
                        string sqltype = "";
                        string sqlname = "";
                        i += 7;
                        for (; ; )
                        {
                            if (str[i] == ' ') break;
                            type += str[i];
                            i++;
                        }
                        switch (type)
                        {
                            case "string":
                                sqltype = " nvarchar(500),\r\n";
                                break;
                            case "bool":
                                sqltype = " BIT,\r\n";
                                break;
                            case "DateTime":
                            case "DateTime?":
                                sqltype = " datetime,\r\n";
                                break;
                            case "decimal":
                            case "decimal?":
                                sqltype = " decimal(18, 2),\r\n";
                                break;
                            case "int":
                            case "int?":
                                sqltype = " int,\r\n";
                                break;
                            case "long":
                            case "long?":
                                sqltype = " bigint,\r\n";
                                break;
                            default:
                                sqltype = " " + type + ",\r\n";
                                break;
                        }
                        i++;
                        for (; ; )
                        {
                            if (str[i] == ' ') break;
                            sqlname += str[i];
                            i++;
                        }
                        sqlbody += sqlname + sqltype;
                        i += 13;
                    }
                }

            return "create table " + sqlhead + "(\r" + sqlbody + ")";
        }

        /// <summary>
        /// 查询扩展方法
        /// </summary>
        [DllImport("CardReaderDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        //[DllImport("CardReaderDLL.dll", EntryPoint = "ZJ_Hmac_SM3", SetLastError = true)]
        public static extern string ZJ_Hmac_SM3(string key, string secret, string unix_timestamp, string request_body);
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var aaa = Convert.ToInt64(ts.TotalSeconds).ToString();
            return aaa;
        }
        private static string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        static void Main(string[] args)
        {
            var jiamitext = SignGenerator.ZJ_Hmac_SM3("1", "1", "1646825799978", "{\"a\":\"11\"}");


            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            AesManaged tdes = new AesManaged();
            tdes.Key = UTF8.GetBytes("4dbd1966f8d691d27b70be9110bb386a");//key
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform crypt = tdes.CreateEncryptor();
            byte[] plain = Encoding.UTF8.GetBytes("11".ToString());//需加密字符串
            byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
            var encryptedText = Convert.ToBase64String(cipher);//结果

            IntPtr intPtr = new IntPtr(1);
            //var result2 = ZJ_Hmac_SM3(intPtr, intPtr, intPtr, intPtr);

            var result2 = ZJ_Hmac_SM3("1", "wRhVQMudgWKxkh9", "1", "12");

            String secretAccessKey = "1";
            String data3 = "1";
            byte[] secretKey = Encoding.UTF8.GetBytes(secretAccessKey);
            HMACSHA256 hmac = new HMACSHA256(secretKey);
            hmac.Initialize();
            byte[] bytes = Encoding.UTF8.GetBytes(data3);
            byte[] rawHmac = hmac.ComputeHash(bytes);
            var resul5 = Convert.ToBase64String(rawHmac);
            Console.WriteLine();


            string assstt = "1" + "\n" + "1";
            var voo = CreateToken("1" + "\n" + "1", "1");
            int abcd = "87f75d257be3915d5a067c66afda3ab136ab2b12aad547c0a1470b008f1b2302".Length;
            abcd = "924DF1F3CD3A81C8D4A903E24FE8FA65046307ED8FC2908A4F9F2C4CB63C2325".Length;
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var aaa = Convert.ToInt64(ts.TotalSeconds).ToString();

            string str1312 = "111";
            string str312 = "111";
            var asaa = Sm3Crypto.ToSM3HexStr(str1312);

            var as1123 = Sm3Crypto.ToSM3byte(str1312);
            as1123 = Sm3Crypto.ToSM3byte(assstt, "1");
            var as112 = Sm3Crypto.ToSM3HexStr(str1312);
            var asdasq = Sm3Crypto.ToSM3HexStr(assstt, "wqe");
            //F378C5C4E3E7828F9A9DC61BB3020F9FD3FC9033F3897E91E918B45FD3AB2E2E


















            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Bar = 100,
                Nest = new
                {
                    Foobar = true
                }
            });
            clay["asd"] = "asd";
            // 更新
            clay.Foo = "Furion";
            clay["Nest"].Foobar = false;
            clay.Nest["Foobar"] = true;

            dynamic clay2 = new Clay();
            clay2.Arr = new string[] { "Furion", "Fur" };

            // 数组转换示例
            var a1 = clay2.Arr.Deserialize<string[]>(); // 通过 Deserialize 方法
            var a2 = (string[])clay2.Arr;    // 强制转换
            string[] a3 = clay2.Arr; // 声明方式

            string strCreate = GetSqlCreateTable();





            KeyWord2 keyWord21 = new KeyWord2();
            var keyWord21s = keyWord21.uv2.ToString().Replace(",", "");
            var rra11 = Testaa(10, 2);

            var adaaaa = Convert.ToInt32("");














            var data211111 = Guid.NewGuid();


            List<KeyWord2> keys13325 = new List<KeyWord2>();
            var keys1345 = new KeyWord2() { uv = 1, dec = 400, dec1 = 20, kword = "大家好" };

            for (int i = 0; i < 3; i++)
            {
                KeyWord2 word2 = DeepCopyByXml(keys1345); ;
                var cc5 = Getaa(word2);
            }















            var keys13456 = new KeyWord2() { uv = 2, dec = 400, dec1 = 20, kword = "大家好晚上" };
            var keys134567 = new KeyWord2() { uv = 3, dec = 400, dec1 = 20, kword = "大家好非常" };
            var keys1345678 = new KeyWord2() { uv = 4, dec = 400, dec1 = 20, kword = "大家好非常" };
            keys13325.Add(keys1345678);
            keys13325.Add(keys1345);
            keys13325.Add(keys13456);
            keys13325.Add(keys134567);
            keys13325 = QueryableExtension.OrderByDescending(keys13325.AsQueryable(), "uv").ToList();


            List<int> jids = new List<int>();
            Dictionary<int, int> keyValues2 = new Dictionary<int, int>();
            int a11 = 1;
            foreach (var item in "2,1,3".Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                jids.Add(Convert.ToInt32(item));
                keyValues2.Add(Convert.ToInt32(item), a11);
                a11 = +1;
            }
            keys13325 = keys13325.OrderBy(u => jids.Contains(u.uv)).ThenBy(u => keyValues2.ContainsKey(u.uv) ? keyValues2[u.uv] : 888).ToList();





            List<KeyWord2> keys13322 = new List<KeyWord2>();
            keys13322.Add(new KeyWord2());
            if (keys13322.First().uv2 > 0)
            {
                var avasvav = (keys13322.First().uv2 / 100)?.ToString("P");


            }
            var asd1222222 = keys13322.Average(u => u.uv2.GetValueOrDefault(0));
            for (int i = 0; i < 30; i++)
            {
                keys13322.Add(new KeyWord2());
            }
            var asdasd222 = keys13322[0];

            var ctrr = Convert.ToInt64("");
            string asdayyy = "s://";


            int yyyaa = Convert.ToInt32("");// 9 % 7;


            if ("data:image".IndexOf("http") == -1)
            {
                var atttta = "data:image".IndexOf("http") >= 0;//.Substring(0, 4);

            }

            Dictionary<string, string> dicList = new Dictionary<string, string>() { { "流量指数", "shop_uvIndex" }, { "收藏人气", "shop_cltHits" }, { "加购人气", "shop_cartHits" }, { "支付转化指数", "shop_payRateIndex" }, { "交易指数", "shop_tradeIndex" }, { "客群指数", "shop_payByrCntIndex" }, { "预售定金指数", "shop_preTradeIndex" }, { "预售定金商品件数", "shop_preSellItmCnt" }, { "上新商品数", "shop_fstOnsItmCnt" } };
            var dicFirst = dicList.Where(u => u.Key == "asdas").Count();


            List<string> vsss = new List<string>();
            vsss.Add("大家好非常");
            vsss.Add("大家好晚上");

            keys13325 = keys13325.Where(u => vsss.Contains(u.kword)).ToList();


            Stopwatch sw = new Stopwatch();

            // 开始测量代码运行时间
            sw.Start();
            ////请求接口
            string resoultDatasdddaaa = WebClientHelper.DownloadString("http://www.bk.caoam.cn/JCReport2/GetTestJson");
            //string resoultDatasdddaaa = HttpUtil.HttpGet2("http://www.bk.caoam.cn/JCReport2/GetTestJson");
            //dynamic myData = JsonConvert.DeserializeObject<dynamic>(resoultData);
            sw.Stop();
            var ElapsedMilliseconds = sw.ElapsedMilliseconds;

            StreamReader sr111111 = new StreamReader("D:\\新建文本文档.txt", Encoding.Default);
            string content;
            string cc333 = "";
            while ((content = sr111111.ReadLine()) != null)
            {
                cc333 += content;
            }



            if (keys1345.kword.StartsWith("@"))
            {

                keys1345 = new KeyWord2() { uv = 4, dec = 400, dec1 = 20 };
            }
            foreach (var item in keys1345.kword?.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList())
            {

            }

            var da231231ss = new DateTime(2020, 8, 29).AddDays(-29);


            var aqe22 = new System.Data.DataTable().Compute("", "");
            aqe22 = (aqe22?.ToString() == "Infinity") || (aqe22?.ToString() == "NaN") ? 0 : aqe22;

            decimal aaa441a = 0;

            decimal.TryParse("njkl", out aaa441a);

            string valstri = "ads";
            valstri = valstri.Substring(0, 7);

            var json2 = JsonConvert.SerializeObject(new List<KeyWord2>());

            var json3 = JsonConvert.DeserializeObject<List<KeyWord2>>(json2);

            json3 = json3.Where(u => u.dec == 1).ToList();

            decimal y1 = 1000 - (-5);
            decimal y2 = 0m;
            decimal y3 = 0m;
            int yCount = 0;
            while (true)
            {
                y1 = Math.Round(y1 / 2, 8);
                if (y1 == y2)
                {
                    break;
                }
                y2 = y1;
                yCount += 1;
            }





            WriteLog($"adsa");
            string strprop = "Tid	AlipayNo	BuyerMemo	BuyerNick	CommissionFee	ConsignTime	Created	CreditCardFee	DiscountFee	EndTime	Modified	PayTime	Payment	post_fee	ReceivedPayment	ReceiverAddress	ReceiverCity	ReceiverDistrict	ReceiverMobile	ReceiverName	ReceiverPhone	ReceiverState	ReceiverZip	SellerFlag	SellerMemo	Status	TotalFee	TradeFrom	Platform	src_id	AllFee	AllCost	LastAllFeeDate	LastAllCostDate	ExpressInfo	IsShua	ShuaKeyWords	ShopID	vistorNum	Title	IsO2oPassport	StepPaidFee	StepTradeStatus	Type	ShippingType	SellerRate	BuyerRate	AdjustFee	MarkDesc	HasBuyerMessage	PicPath";

            var adas12312311 = strprop.Split("	").ToList().Count();


            List<KeyWord2> keys1332 = new List<KeyWord2>();

            var keys1341 = new KeyWord2() { kword = "1", uv = 1, dec = 400.333m, dec1 = 20 };

            var keys1352 = new KeyWord2() { kword = "ww", uv = 3, dec = 500, dec1 = 20 };
            var keys1342 = new KeyWord2() { kword = "33", uv = 2, dec = 400, dec1 = 20 };
            var keys1353 = new KeyWord2() { kword = "qq", uv = 4, dec = 500, dec1 = 20 };
            keys1341.dec = Math.Round(keys1341.dec, 0);
            keys1332.Add(keys1341);
            keys1332.Add(keys1352);
            keys1332.Add(keys1342);
            keys1332.Add(keys1353);


            var fields = "ww,33".Split(",");
            foreach (var item in keys1332)
            {
                if (fields.Where(u => u == item.kword).FirstOrDefault() != null)
                {
                    item.uv = 1;
                }
            }


            keys1332.Remove(keys1341);


            List<KeyWord2> keys1335 = new List<KeyWord2>();


            var keys1355 = new KeyWord2() { uv = 2, dec = 500, dec1 = 20 };
            var keys1346 = new KeyWord2() { uv = 3, dec = 400, dec1 = 20 };
            var keys1356 = new KeyWord2() { uv = 1, dec = 500, dec1 = 20 };

            keys1335.Add(keys1345);
            keys1335.Add(keys1355);
            keys1335.Add(keys1346);
            keys1335.Add(keys1356);
            keys1332 = keys1332.OrderBy(u => u.uv).ToList();
            var idList2 = keys1332.Select(u => u.uv);
            keys1335 = keys1335.OrderBy(u => keys1332.Select(i => i.uv == u.uv)).ToList();

            for (int i = 0; i < 5; i++)
            {
                WriteLog($"{i}adsa");
            }




            string hh = "<dl class=\"tm-price-panel\" id=\"J_StrPriceModBox\">        <dt class=\"tb-metatit\">价格</dt>        <dd><em class=\"tm-yen\">¥</em> <span class=\"tm-price\">139.00</span></dd>    </dl><dl class=\"tm-promo-panel tm-promo-cur\" id=\"J_PromoPrice\" data-label=\"促销价\"><dt class=\"tb-metatit\">促销价</dt><dd><div class=\"tm-promo-price\">              <em class=\"tm-yen\">¥</em> <span class=\"tm-price\">39.60-44.60</span>                                                    &nbsp;&nbsp;                                 </div> <p>   </p></dd></dl><script type=\"data/tpl\" id=\"J_PromoHintText\"><!--rullBanner ids:$ids true--></script>                  <dl class=\"tm-shopPromo-panel\"><div class=\"tm-shopPromotion-title tm-gold \"><dt class=\"tb-metatit\">本店活动</dt><dd>满61件6折</dd><a class=\"more\">更多优惠<s></s></a></div><div class=\"tm-floater-Box  hidden\"><div class=\"floater fold\">     <div class=\"hd\">         <em class=\"title \">本店活动</em>         11.15                               到2020-11-30 23:59:59结束                  <a class=\"more unmore\">收起<s></s></a>         <a class=\"more\">更多优惠<s></s></a>              </div>     <ul class=\"bd\">                  <li class=\"noCoupon\" data-index=\"0\">             <p>                                      满<em>61</em>件                     <em>6</em>折                                                                                                                                                                                                                                                </p>                      </li>              </ul>     <div class=\"ft\">                       </div> </div></div></dl>";
            var hhList = hh.Split("¥").ToList();
            hh = hhList[hhList.Count() - 1];
            hh = hh.Split("price\">")[1].Split("</span>")[0];

            hh = "<dl class=\"tm-price-panel\" id=\"J_StrPriceModBox\">        <dt class=\"tb-metatit\">价格</dt>        <dd><em class=\"tm-yen\">¥</em> <span class=\"tm-price\">139.00</span></dd>    </dl><dl class=\"tm-promo-panel tm-promo-cur\" id=\"J_PromoPrice\" data-label=\"促销价\"><dt class=\"tb-metatit\">促销价</dt><dd><div class=\"tm-promo-price\">              <em class=\"tm-yen\">¥</em> <span class=\"tm-price\">39.60-44.60</span>                                                    &nbsp;&nbsp;                                 </div> <p>   </p></dd></dl><script type=\"data/tpl\" id=\"J_PromoHintText\"><!--rullBanner ids:$ids true--></script>                  <dl class=\"tm-shopPromo-panel\"><div class=\"tm-shopPromotion-title tm-gold \"><dt class=\"tb-metatit\">本店活动</dt><dd>满61件6折</dd><a class=\"more\">更多优惠<s></s></a></div><div class=\"tm-floater-Box  hidden\"><div class=\"floater fold\">     <div class=\"hd\">         <em class=\"title \">本店活动</em>         11.15                               到2020-11-30 23:59:59结束                  <a class=\"more unmore\">收起<s></s></a>         <a class=\"more\">更多优惠<s></s></a>              </div>     <ul class=\"bd\">                  <li class=\"noCoupon\" data-index=\"0\">             <p>                                      满<em>61</em>件                     <em>6</em>折                                                                                                                                                                                                                                                </p>                      </li>              </ul>     <div class=\"ft\">                       </div> </div></div></dl>";
            hhList = hh.Split("本店活动").ToList();
            hh = hhList[hhList.Count() - 1];



            hh = hh.Split("price\">")[1].Split("</span>")[0];

            string asdadaaa = Math.Round(5m / 2).ToString("P");
            var ads = CamCrypto.Encrypt("123456");
            List<KeyWord2> keys13 = new List<KeyWord2>();

            var asd123 = Convert.ToInt64("12313  ");

            var keys133 = new KeyWord2() { dec = 0, dec1 = 20, kword = "a1d" };
            asdadaaa = (keys133.kword?.Contains("ad")).GetValueOrDefault(true) ? "1" : "2";
            asdadaaa = keys133.uv.ToString().Replace("+", "");
            asdadaaa = keys133.uv2.ToString().Replace("+", "");

            var keys134 = new KeyWord2() { dec = 400, dec1 = 20 };
            var keys135 = new KeyWord2() { dec = 500, dec1 = 20 };
            keys13.Add(new KeyWord2 { });


            var aa22 = keys13.Average(u => u.uv);

            keys13.Add(keys133);
            keys13.Add(keys134);
            keys13.Add(keys135);



            foreach (var item in keys13)
            {
                var ad33 = item.getSum();
            }

            var keys131 = new KeyWord2() { dec = -200 };
            var keys132 = new KeyWord2() { dec = -100 };

            var keys136 = new KeyWord2() { dec = 600 };
            var keys137 = new KeyWord2() { dec = (keys131.dec + keys132.dec) / 2 };

            keys13.Add(keys131);
            keys13.Add(keys132);

            keys13.Add(keys136);
            keys13.Add(keys137);
            var asda15 = keys13.Skip(2).FirstOrDefault();


            keys13 = keys13.OrderBy(u => u.dec).ToList();


            string strss = "4.5万+";
            strss = strss.Replace("+", "");

            if (strss.Contains("万"))
            {

                decimal dec123a = Convert.ToDecimal(strss.Replace("万", "")) * 10000;

            }
            int a222 = 12;

            var asdddd = Math.Round(a222 / 2m).ToString("P");

            List<Student> students15 = new List<Student>();
            for (int i = 0; i < 10000000; i++)
            {
                students15.Add(new Student());
            }

            List<Task> tasks = new List<Task>();

            var wl = true;
            var skip = 1;
            while (wl)
            {
                var students152 = students15.Skip((skip - 1) * 20000).Take(200);
                if (students152.Count() < 20000)
                {
                    wl = false;
                    if (students152.Count() == 0)
                    {
                        break;
                    }
                }
                tasks.Add(Task.Run(() =>
                {
                    foreach (var item in students152)
                    {
                        item.numb1 = 12;
                    }
                }));

                skip = skip + 1;
            }
            Task.WaitAll(tasks.ToArray());


            Console.WriteLine("总运行时间：" + sw.Elapsed);







            //StreamWriter sw = new StreamWriter(@"D:\cookies.dll");
            //sw.Write("21");
            //sw.Close();



            DateTime daasd12 = new DateTime(2020, 2, 29).AddMonths(-1);


            var newdate23 = DateTime.Now.AddDays(-30);

            List<long> adslong = new List<long>();
            adslong.Add(1289767320547214357); adslong.Add(1289767320541214357);
            List<Student> students12 = new List<Student>();

            students12.Add(new Student() { dt = "aa", thedate = DateTime.Now });
            students12.Add(new Student() { dt = "aa", thedate = DateTime.Now.AddHours(-1) });
            students12.Add(new Student() { dt = "bb", thedate = DateTime.Now });
            students12.Add(new Student() { dt = "cc", thedate = DateTime.Now });

            var end = DateTime.Now;
            var start = end.AddDays(-30).Date;
            var data2 = students12.GroupBy(u => new { u.dt, u.thedate.Date });
            var newEnd = end;
            while (newEnd >= start)
            {
                var dt = data2.Where(u => u.Key.Date == newEnd.Date);
                Dictionary<string, int> kv = new Dictionary<string, int>();

                foreach (var item in dt)
                {
                    kv.Add(item.Key.dt, item.Count());
                }

                var count = kv.Sum(u => u.Value);
                newEnd = newEnd.AddDays(-1);
            }








            students12 = students12.Where(u => adslong.Contains(u.numb1)).ToList();
            List<PublicN5<string, int, decimal, int, decimal>> qudaoList = new List<PublicN5<string, int, decimal, int, decimal>>();
            qudaoList.AddRange(JsonConvert.DeserializeObject<List<PublicN5<string, int, decimal, int, decimal>>>("[]"));


            string asd = "";

            asd = GetText(@"C:\Users\Admin\Downloads\333.txt");
            var returnData1 = JsonConvert.DeserializeObject<dynamic>(asd);
            JArray jarray2;
            //DateTime newDate = date;
            int sum = 29;
            dynamic keyWords_data2 = returnData1["content"] == null ? null : returnData1["content"];
            if (null == keyWords_data2)
            {
            }
            jarray2 = keyWords_data2["data"] == null ? null : keyWords_data2["data"];
            if (jarray2 == null)
            {

            }



            //for (int i = 0; i < 12; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        WriteLog("khg2");
            //    });
            //}

            //Thread.Sleep(5000);


            var asda222 = JsonConvert.DeserializeObject<List<KeyWord3>>("[]");

            asda222.Where(u => u.dec5 == 5);
            //  Student student161 = new Student();
            //  student161.dt = "asd";
            //  string asd214 = null;

            //var asd2222= student161.dt.Length;

            //  var datet12 = new DateTime(2020,9,25);
            //  var datet2 = new DateTime(2020, 9, 20);


            //  var datet3 = (datet12 - datet2).Days + 1;


            //string data35 = "3424+" + DateTime.Now + "+" + asd;
            //string urlEncode = System.Web.HttpUtility.UrlEncode(data35);


            Console.WriteLine(QType.好);


            var datetime1 = DateTime.Now.Date.AddDays(-8);
            ////生意参谋解密
            //var str = CookieHelper.sk(asd);

            //dynamic returnData1 = JsonConvert.DeserializeObject<dynamic>(asd);
            //string keyWords_data1 = returnData1["data"]?.ToString();

            //if (null == keyWords_data1)
            //{

            //}
            //解密
            string keyWords_str1 = CookieHelper.sk(asd);
            dynamic keyWords_json1 = JsonConvert.DeserializeObject<dynamic>(keyWords_str1);
            if (keyWords_json1 == null)
            {

            }
            JArray jarray1 = keyWords_json1["data"]; ;
            foreach (JToken j in jarray1)
            {
                if (j["shop"] != null)
                {
                    var ad5553 = j["itemId"]["value"] == null ? 0 : Convert.ToInt64(j["itemId"]["value"].ToString());
                    //JZItem jzData = new JZItem();
                    //jzData.item = 0;
                    //jzData.ShopID = upLodate.shopId;
                    //jzData.jzItem = j["itemId"]["value"] == null ? 0 : Convert.ToInt64(j["itemId"]["value"].ToString());
                    //jzData.title = j["item"]["title"]?.ToString();
                    //jzData.catId = catId.ToString();
                    //jzData.pic = j["item"]["pictUrl"]?.ToString();
                    //jzData.url = j["item"]["detailUrl"]?.ToString();
                    //jzData.price = string.Empty;
                    //jzData.CreateDate = DateTime.Now;
                    //jzData.shopName = j["shop"]["title"]?.ToString();
                    //jzData.isMonitoring = 1;
                    //jzData.sort = null;
                    //jzData.isOpen = 0;
                    //jzData.remark = string.Empty;
                    //jzData.province = j["shop"]["province"]?.ToString();
                    //jzData.city = j["shop"]["city"]?.ToString();
                    //jzData.b2CShop = j["shop"]["b2CShop"]?.ToString() == "true" ? 1 : 0;
                    //jzData.pictureUrl = j["shop"]["pictureUrl"]?.ToString();
                    //jzData.shopUrl = j["shop"]["shopUrl"]?.ToString();
                    //dataList.Add(jzData);
                }
            }






            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            var props = typeof(KeyWord);
            foreach (var item in props.GetProperties().ToList())
            {
                var asd44 = item.PropertyType.Name;
                object[] objs = item.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objs.Length > 0)
                {
                    keyValues.Add(((DescriptionAttribute)objs[0]).Description, item.Name);
                }
            }
            Dictionary<string, string> valuePairs = new Dictionary<string, string>() { { "日期", "2018-04-09 21:33:08.770" }, { "访客", "23" }, { "访客2", "12" }, { "测试", "123" } };
            KeyWord keyWord = new KeyWord();
            foreach (var item in keyValues)
            {
                var tName = props.GetProperty(item.Value).PropertyType.FullName;
                if (tName.Contains("String"))
                {
                    props.GetProperty(item.Value).SetValue(keyWord, valuePairs[item.Key]);
                }
                else if (tName.Contains("Int32"))
                {
                    props.GetProperty(item.Value).SetValue(keyWord, Convert.ToInt32(valuePairs[item.Key]));
                }
                else if (tName.Contains("Decimal"))
                {
                    props.GetProperty(item.Value).SetValue(keyWord, Convert.ToDecimal(valuePairs[item.Key]));
                }
                else if (tName.Contains("DateTime"))
                {
                    props.GetProperty(item.Value).SetValue(keyWord, Convert.ToDateTime(valuePairs[item.Key]));
                }
                //props.GetProperty(item.Value).SetValue(keyWord, valuePairs[item.Key]);
            }
            KeyWord keyWordtest = new KeyWord() { kword = "ada", uv = 12 };
            var ccc2 = props.GetProperty("uv").GetValue(keyWordtest);
            string strkasf2 = "asd" + ccc2;
            props.GetProperty("dec1").SetValue(keyWordtest, 12m);
            int decuv = (int)keyWordtest.dec;
            var props2 = typeof(KeyWord);
            dynamic dyc = new { title = "asdasd", avg = 12 };



            List<KeyWord> keyWord1 = new List<KeyWord>();

            keyWord1.Add(keyWordtest);

            string serobj = Newtonsoft.Json.JsonConvert.SerializeObject(keyWord1).Replace("\\\"", "\"");

            var keyWord2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KeyWord2>>(serobj);

            List<KeyWord3> word3s = new List<KeyWord3>();
            word3s.Add(new KeyWord3() { dec3 = 1, dec4 = 2, uv = 12 });
            Type type8 = typeof(KeyWord3);

            Type type7 = typeof(KeyWord2);
            foreach (var item in keyWord2)
            {
                var word3 = word3s.Where(u => u.uv == item.uv).FirstOrDefault();
                if (word3 != null)
                {
                    foreach (var prop in type8.GetProperties())
                    {
                        if (prop.Name == "uv")
                        {
                            continue;
                        }
                        if (type7.GetProperty(prop.Name) != null)
                        {
                            type7.GetProperty(prop.Name).SetValue(item, prop.GetValue(word3));
                        }

                    }

                }
            }


            var a = new System.Data.DataTable().Compute("1/0", "");
            a = a?.ToString() == "Infinity" ? a : 0;
            var ad555 = null * 0.6m;

            List<Student2> student2s = new List<Student2>();
            var ceshi234 = student2s.Average(u => u.ba).GetValueOrDefault(0);
            student2s.Add(new Student2() { b = 0, a = 5, student2s = new List<Student2>() });
            student2s.Add(new Student2() { b = 1, a = 5 });
            student2s.Add(new Student2() { b = 1, a = 5 });
            student2s.Add(new Student2() { b = 0, a = 5 });
            foreach (var item in student2s)
            {
                item.student2s.Add(new Student2() { b = 1, a = 5 });
            }


            var asdad1 = student2s.Where(u => u.b == 0).Select(u => u.a).FirstOrDefault();


            student2s = student2s.OrderByDescending(u => u.a == 0).ToList();
            foreach (var item in student2s.GroupBy(u => u.b))
            {
                double ceshi23 = item.Average(u => u.ba).GetValueOrDefault(0);
            }




            Guid guid12 = Guid.Parse("931E1F27-3C01-44B4-A205-FE7DADEEDC17");


            string str123 = "//img.alicdn.com/bao/uploaded/i4/3177173659/TB1slsnSFXXXXaeXVXXXXXXXXXX_!!0-item_pic.jpg_430x430q90.jpg";
            str123 = str123.Split("jpg")[0] + "jpg";
            DateTime dateTime5 = DateTime.Parse("2020-07-02");

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

            var testList5 = students.OrderByDescending(u => u.numb2 > 0).ThenBy(u => u.numb2).ToList();


            var testList = students.Where(u => u.numb1 == 10).ToList();

            //var testList3 = testList.Where(u => u.Key == 2);
            var testList4 = testList.Sum(u => u.numb1);
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
            var countspi = sad12312.Split(';').ToList()[0].Split(':');
            long long1 = 12;
            int long3 = (int)long1;
            //SkuClientSoapClient skuClient = new SkuClientSoapClient();
            List<long> vsasdw = new List<long>();
            List<KeyWord> keyWord232 = new List<KeyWord>();
            //keyWord232.Add(new KeyWord());
            var Averagesa1 = keyWord232.OrderByDescending(u => u.dec).FirstOrDefault();


            var asda999 = keyWord232.Select(u => (long)u.dec).ToList();


            keyWord232.Add(new KeyWord { dec = 0.1m, dec1 = 12m, uv = 1, kword = "" });
            var ads2a1 = keyWord232.Where(u => vsasdw.Contains((long)u.uv)).ToList();
            KeyWord keyWord23 = new KeyWord();

            Type t23 = typeof(KeyWord);

            var vvalue = t23.GetProperty("dec1").PropertyType.FullName;

            if (vvalue.Contains("String"))
            {

            }
            else if (vvalue.Contains("Int32"))
            {

            }
            else if (vvalue.Contains("Decimal"))
            {

            }
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
                resoultData = HttpUtil.PostData("http://www.bk.caoam.cn/JcService/MonitorJzItemChanges", data);
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
            jieshu:;
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
        public long numb1 { get; set; }
        public int? numb2 { get; set; }
        public int numb3 { get; set; }
        public int numb4 { get; set; }
        public string dt { get; set; }
        public DateTime thedate { get; set; }
    }
    public class Student2

    {
        public int a { get; set; }
        public int b { get; set; }
        public int? ba { get; set; }
        public DateTime dt { get; set; }

        public List<Student2> student2s { get; set; }
    }
    public class KeyWord
    {
        [Description("日期")]
        public DateTime thedate { get; set; }
        public string kword { get; set; }
        [Description("访客")]
        public int uv { get; set; }
        [Description("访客2")]
        public int? uv2 { get; set; }
        public decimal dec { get; set; }
        public decimal? dec1 { get; set; }
        [Description("测试")]
        public long dec2 { get; set; }


    }
    public class KeyWord2
    {
        [Description("日期")]
        public string kword { get; set; }
        [Description("访客")]
        public int uv { get; set; }
        [Description("访客2")]
        public int? uv2 { get; set; }
        public decimal dec { get; set; }
        public decimal? dec1 { get; set; }
        public long dec3 { get; set; }
        public long dec4 { get; set; }
        public long? dec5 { get; set; }
        public decimal getSum()
        {
            return dec + dec1.GetValueOrDefault(0);
        }
    }
    public class KeyWord3
    {
        public int uv { get; set; }
        public long dec2 { get; set; }
        public long dec3 { get; set; }
        public long dec4 { get; set; }
        public long? dec5 { get; set; }
    }
    public enum enumTest
    {
        你好 = 0,
        早 = 1,
        拜拜 = 2
    }


}
