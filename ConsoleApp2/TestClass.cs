using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
  
    public class TestClass
    {
        public static List<string> listStr=new List<string>();
        static TestClass()
        {
            for (int i = 0; i < 3; i++)
            {
                listStr.Add(i.ToString());
            }
        }
        public static void DatebaseCM()
        {
            string str = "http://www.bk.caoam.cn/,172.26.23.231;http://www.caoam.cn/,172.26.23.231;http://wwwkader.caoam.cn/,172.26.23.233;http://wwwckds.caoam.cn/,172.26.23.235;http://wwwshengke.caoam.cn/,172.26.23.237;http://wwwqby.caoam.cn/,172.26.23.239;http://wwwyt.caoam.cn/,172.26.23.240;http://wwwarb.caoam.cn/,172.26.23.241;http://wwwkeb.caoam.cn/,172.26.23.242";
              //var asd =  str.Split(new[] {';' }, StringSplitOptions.RemoveEmptyEntries).ToDictionary(u=>u.Split);


           var as00d = listStr.Count();
        }
    }
}
