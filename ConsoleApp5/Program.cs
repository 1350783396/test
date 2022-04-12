using Natasha.CSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            //NatashaInitializer.Initialize();
            //注册+预热组件 , 之后编译会更加快速
            NatashaInitializer.Preheating();
            string str = "return \"S1,S2,3,4,5,6\".Split(\",\").Where(u => u.StartsWith(\"D\")).Count()>0;";
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //NDelegate.CreateDomain("a", item => item.DisableSemanticCheck().ConfigCompilerOption(opt => opt.AddSupperess("CS8019").UseSuppressReportor(false)))
            //List<Task> tasks = new List<Task>();
            //for (int i = 0; i < 4; i++)
            //{
            //    tasks.Add(Task.Factory.StartNew(() => {
            //        for (int i = 0; i < 50; i++)
            //        {
            //            str = $"return \"S1,{i},3,4,5,6\".Split(\",\").Where(u => u.StartsWith(\"D\")).Count()>0;";
            //            var func = NDelegate.CreateDomain("MyDomain", item => item.DisableSemanticCheck().ConfigCompilerOption(opt => opt.AddSupperess("CS8019").UseSuppressReportor(false))).Func<bool>(str);
            //            var aa = func();
            //            //func.DisposeDomain();
            //        }
            //    }));
            //}
            //Task.WaitAll(tasks.ToArray());

            for (int i = 0; i < 200; i++)
            {
                str = $"return \"S1,{i},3,4,5,6\".Split(\",\").Where(u => u.StartsWith(\"D\")).Count()>0;";
                var func = NDelegate.CreateDomain("MyDomain", item => item.DisableSemanticCheck().ConfigCompilerOption(opt => opt.AddSupperess("CS8019").UseSuppressReportor(false))).Func<bool>(str);
                var aa = func();
                //func.DisposeDomain();
            }
            sw.Stop();
            var yb = sw.ElapsedMilliseconds;//毫秒

            Console.ReadKey();
        }
    }
}
