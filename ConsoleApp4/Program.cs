
using CardReader;
using CSScriptLib;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ConsoleApp4
{
    class Program
    {
        [DllImport("CardReader.dll")]  //最关键的，导入该dll
        public extern static string MessageBoxShow();
        public class Student
        {
            public string Str1 { get; set; }

            public string Str2 { get; set; }
        }
        static bool Bmi()
        {
            int num = 0;
            foreach (var item in "S1,S2,3,4,5,6".Split(","))
            {
                if (item.Contains("S"))
                    num += 1;
            }
            return num > 1;
        }
        public static void Main(string[] args)
        {
            string var12 = "测试长度";
            string var13 = var12.Substring(4);
            DateTime? datethe = Convert.ToDateTime(null);
            List<string> strList = new List<string>();
            strList.Add("次诊断1");
            strList.Add("次诊断2");
            strList.Add("次诊断31");
            strList.Add("次诊断23");
            strList.Add("次诊断13");
            strList = strList.OrderBy(u => u.Length).ThenBy(u => u).ToList();
            var t = string.Compare("D22", "D13");
            var bo7 = CSharpScript.EvaluateAsync<bool>("false || true || false");
            var b08 = true && false || true;
            var bo5 = ((string.Compare("D10.000", "C16.900") >= 0 || string.Compare("D10.000", "V00.500") >= 0 || string.Compare("D10.000", "S81.500") >= 0) && (string.Compare("D36.999", "C16.900") <= 0 || string.Compare("D36.999", "V00.500") <= 0 || string.Compare("D36.999", "S81.500") <= 0));
            bo5 = (1 > 2 || 5 > 2 || 6 > 2) && (1 < 5 || 5 < 5 || 6 < 5);

            var bo4 = Bmi();
            string field = "2;3";
            string sty = "public bool Bmi()" +
            "{" +
                " int num=0; " +
              "foreach (var item in \"" + field + "\".Split(\";\"))" +
              "{" +
                 "foreach (var item2 in \"S1,S2,3,4,5,6\".Split(\",\"))" +
                 "{" +
                   "if(item2.Contains(item))" +
                    "num+=1;" +
                 "}" +
              "}" +
              "return num>1;" +
            "}";
            var bo3 = CSharpScript.EvaluateAsync<bool>(sty + " return Bmi();");
            var bo2 = ("asc".Length) > 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 200; i++)
            {
                var result = CSharpScript.EvaluateAsync<bool>("\"S1,S2,3,4,5,6\".Split(\",\").Where(u => u.StartsWith(\"D\")).Count()>0");
            }
            sw.Stop();
            var yb = sw.ElapsedMilliseconds;//毫秒
            var asd = "S1,S2,3,4,5,6".Split(",").Where(u => u.StartsWith("S")).Count();
            string asg = "\"S1,S2,3,4,5,6\".Split(\",\").Where(u => u.StartsWith(\"S\")).Count()";
            //       var result = CSharpScript.EvaluateAsync<bool>(asg + ">0",
            //ScriptOptions.Default.FilePath);
            var ap = string.Compare("a", "b");
            decimal dec = Convert.ToDecimal(null);
            string ss = "";
            ss = ss.Replace("'", "");
            string zf = "a";
            string bs = "a";
            bool boo = zf.Equals(bs);

            DateTime start = new DateTime(2022, 3, 14);
            DateTime end = new DateTime(2022, 3, 7);
            var a3 = (start - end).TotalDays;
            var e66 = end.DayOfYear / 7 + 1;
            var bli = start.Subtract(end).TotalDays;
            //var b = CodeHelper.Validate("\"女\"!=\"男\"");
            //bool b2 = b.Result;
            //string stt = "";
            //var jiamitext = SignGenerator.ZJ_Hmac_SM3("H33108300201", "t8kUh3ReUC0oyzJ", "1647420242", stt);

            //bool asd = CodeHelper.Validate("\"女\"==\"男\"");


            //var hello = MessageBoxShow();
            string value = "";

            Console.WriteLine("Hello World!");

            Assembly compilemethod = CSScript.RoslynEvaluator.CompileMethod(
                       @"using System;
                          public static int CompileMethod(string greeting)
                          {
                              Console.WriteLine(""CompileMethod:"" + greeting);
return 1;
                          }");
            var p = compilemethod.GetType("css_root+DynamicClass");
            var me = p.GetMethod("CompileMethod");
            var cc = me.Invoke(null, new object[] { "1" });
            for (int i = 0; i < 100; i++)
            {
                var cc2 = me.Invoke(null, new object[] { "1" });
            }

            //eval = CSScript.Evaluator.ReferenceAssembly(sqr);
            dynamic loadmethod = CSScript.Evaluator.LoadMethod(@"using System;
                          public void LoadMethod(string greeting)
                          {
                              Console.WriteLine(""LoadMethod:"" +greeting);
                          }");
            loadmethod.LoadMethod("Hello World!");



            Console.ReadKey();
        }

        public static void Write()
        {
            Console.WriteLine("REFERENCE OK");
        }
    }

}
