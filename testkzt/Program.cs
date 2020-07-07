using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testkzt
{
    class Program
    {
        static void Main(string[] args)
        {

            string cc = "asd";
            var c = cc.Substring(5) == null ? null : cc.Substring(5);
            Console.ReadLine();
        }
    }
}
