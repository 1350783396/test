using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeDomKZT
{
    class Program
    {
        static void Main(string[] args)
        {
            int vv = string.Compare("12.00", "11.1");//1前大 0相等 -1第二个参数大
            int vv2 = string.Compare("3", "4");


            //var asd = CodeHelper.Validate("\"男\"==\"男\" && (\"A00.000\".StartsWith(\"A34\")||\"A00.000\".StartsWith(\"D06\")||\"A00.000\".StartsWith(\"D39\")||\"A00.000\".StartsWith(\"E28\")||\"A00.000\".StartsWith(\"F53\")||\"A00.000\".StartsWith(\"M83\")||\"A00.000\".StartsWith(\"R87\")||\"A00.000\".StartsWith(\"Z39\")||\"A00.000\".StartsWith(\"B37.3\")||\"A00.000\".StartsWith(\"C79.6\")||\"A00.000\".StartsWith(\"E89.4\")||\"A00.000\".StartsWith(\"F52.5\")||\"A00.000\".StartsWith(\"I86.3\")||\"A00.000\".StartsWith(\"L29.2\")||\"A00.000\".StartsWith(\"P54.6\")||\"A00.000\".StartsWith(\"S31.4\")||\"A00.000\".StartsWith(\"T83.3\")||\"A00.000\".StartsWith(\"Z01.4\")||\"A00.000\".StartsWith(\"Z12.4\")||\"A00.000\".StartsWith(\"Z30.1\")||\"A00.000\".StartsWith(\"Z30.3\")||\"A00.000\".StartsWith(\"Z30.5\")||\"A00.000\".StartsWith(\"Z87.5\")||\"A00.000\".StartsWith(\"Z97.5\")||\"C16.900\".StartsWith(\"A34\")||\"C16.900\".StartsWith(\"D06\")||\"C16.900\".StartsWith(\"D39\")||\"C16.900\".StartsWith(\"E28\")||\"C16.900\".StartsWith(\"F53\")||\"C16.900\".StartsWith(\"M83\")||\"C16.900\".StartsWith(\"R87\")||\"C16.900\".StartsWith(\"Z39\")||\"C16.900\".StartsWith(\"B37.3\")||\"C16.900\".StartsWith(\"C79.6\")||\"C16.900\".StartsWith(\"E89.4\")||\"C16.900\".StartsWith(\"F52.5\")||\"C16.900\".StartsWith(\"I86.3\")||\"C16.900\".StartsWith(\"L29.2\")||\"C16.900\".StartsWith(\"P54.6\")||\"C16.900\".StartsWith(\"S31.4\")||\"C16.900\".StartsWith(\"T83.3\")||\"C16.900\".StartsWith(\"Z01.4\")||\"C16.900\".StartsWith(\"Z12.4\")||\"C16.900\".StartsWith(\"Z30.1\")||\"C16.900\".StartsWith(\"Z30.3\")||\"C16.900\".StartsWith(\"Z30.5\")||\"C16.900\".StartsWith(\"Z87.5\")||\"C16.900\".StartsWith(\"Z97.5\")||\"A04.500\".StartsWith(\"A34\")||\"A04.500\".StartsWith(\"D06\")||\"A04.500\".StartsWith(\"D39\")||\"A04.500\".StartsWith(\"E28\")||\"A04.500\".StartsWith(\"F53\")||\"A04.500\".StartsWith(\"M83\")||\"A04.500\".StartsWith(\"R87\")||\"A04.500\".StartsWith(\"Z39\")||\"A04.500\".StartsWith(\"B37.3\")||\"A04.500\".StartsWith(\"C79.6\")||\"A04.500\".StartsWith(\"E89.4\")||\"A04.500\".StartsWith(\"F52.5\")||\"A04.500\".StartsWith(\"I86.3\")||\"A04.500\".StartsWith(\"L29.2\")||\"A04.500\".StartsWith(\"P54.6\")||\"A04.500\".StartsWith(\"S31.4\")||\"A04.500\".StartsWith(\"T83.3\")||\"A04.500\".StartsWith(\"Z01.4\")||\"A04.500\".StartsWith(\"Z12.4\")||\"A04.500\".StartsWith(\"Z30.1\")||\"A04.500\".StartsWith(\"Z30.3\")||\"A04.500\".StartsWith(\"Z30.5\")||\"A04.500\".StartsWith(\"Z87.5\")||\"A04.500\".StartsWith(\"Z97.5\")||\"L81.500\".StartsWith(\"A34\")||\"L81.500\".StartsWith(\"D06\")||\"L81.500\".StartsWith(\"D39\")||\"L81.500\".StartsWith(\"E28\")||\"L81.500\".StartsWith(\"F53\")||\"L81.500\".StartsWith(\"M83\")||\"L81.500\".StartsWith(\"R87\")||\"L81.500\".StartsWith(\"Z39\")||\"L81.500\".StartsWith(\"B37.3\")||\"L81.500\".StartsWith(\"C79.6\")||\"L81.500\".StartsWith(\"E89.4\")||\"L81.500\".StartsWith(\"F52.5\")||\"L81.500\".StartsWith(\"I86.3\")||\"L81.500\".StartsWith(\"L29.2\")||\"L81.500\".StartsWith(\"P54.6\")||\"L81.500\".StartsWith(\"S31.4\")||\"L81.500\".StartsWith(\"T83.3\")||\"L81.500\".StartsWith(\"Z01.4\")||\"L81.500\".StartsWith(\"Z12.4\")||\"L81.500\".StartsWith(\"Z30.1\")||\"L81.500\".StartsWith(\"Z30.3\")||\"L81.500\".StartsWith(\"Z30.5\")||\"L81.500\".StartsWith(\"Z87.5\")||\"L81.500\".StartsWith(\"Z97.5\")||\"A30.900x002\".StartsWith(\"A34\")||\"A30.900x002\".StartsWith(\"D06\")||\"A30.900x002\".StartsWith(\"D39\")||\"A30.900x002\".StartsWith(\"E28\")||\"A30.900x002\".StartsWith(\"F53\")||\"A30.900x002\".StartsWith(\"M83\")||\"A30.900x002\".StartsWith(\"R87\")||\"A30.900x002\".StartsWith(\"Z39\")||\"A30.900x002\".StartsWith(\"B37.3\")||\"A30.900x002\".StartsWith(\"C79.6\")||\"A30.900x002\".StartsWith(\"E89.4\")||\"A30.900x002\".StartsWith(\"F52.5\")||\"A30.900x002\".StartsWith(\"I86.3\")||\"A30.900x002\".StartsWith(\"L29.2\")||\"A30.900x002\".StartsWith(\"P54.6\")||\"A30.900x002\".StartsWith(\"S31.4\")||\"A30.900x002\".StartsWith(\"T83.3\")||\"A30.900x002\".StartsWith(\"Z01.4\")||\"A30.900x002\".StartsWith(\"Z12.4\")||\"A30.900x002\".StartsWith(\"Z30.1\")||\"A30.900x002\".StartsWith(\"Z30.3\")||\"A30.900x002\".StartsWith(\"Z30.5\")||\"A30.900x002\".StartsWith(\"Z87.5\")||\"A30.900x002\".StartsWith(\"Z97.5\"))");
           
                //1.创建CSharpCodeProvider的实例
                CSharpCodeProvider cs = new CSharpCodeProvider();

                //2.创建一个ICodeComplier对象
                ICodeCompiler cc = cs.CreateCompiler();

                //3.创建一个CompilerParameters的实例
                CompilerParameters cp = new CompilerParameters();
                cp.GenerateInMemory = true;//设定在内存中创建程序集
                cp.GenerateExecutable = false;//设定是否创建可执行文件,也就是exe文件或者dll文件cp.ReferencedAssemblies.Add("System.dll");//此处代码是添加对应dll文件的引用
                cp.ReferencedAssemblies.Add("System.Core.dll");//System.Linq存在于System.Core.dll文件中

                //4.创建CompilerResults的实例
                string strExpre = "using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;namespace DynamicCompileTest" +
                    "{public class TestClass1" +
                    "{public bool CheckBool(string source)" +
                    "{ return source.Contains(\"SC\"); }" +
                    "}" +
                    "}";

                CompilerResults cr = cc.CompileAssemblyFromSource(cp, strExpre);
                if (cr.Errors.HasErrors)
                {
                    Console.WriteLine(cr.Errors.ToString());
                }
                else
                {
                    //5.创建一个Assembly对象
                    Assembly ass = cr.CompiledAssembly;//动态编译程序集
                    object obj = ass.CreateInstance("DynamicCompileTest.TestClass1");
                    MethodInfo mi = obj.GetType().GetMethod("CheckBool");
                    bool result = (bool)mi.Invoke(obj, new object[] { "SC" });
                }
          
            Console.ReadKey();
        }
    }
}
