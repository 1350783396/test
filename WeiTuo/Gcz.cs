using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiTuo
{
    public class Cat
    {
        public Cat(Gcz gcz)
        {
            gcz.ev += Jineng;
        }
        public static void Jineng()
        {
            Console.WriteLine("mao跑了");
        }
    }
    public class Gou
    {
        public Gou(Gcz gcz)
        {
            gcz.ev += Jineng;
        }
        public static void Jineng()
        {
            Console.WriteLine("gou叫了");
        }
    }
    public delegate void Weit();
    public class Gcz
    {
        public event Weit ev;
        public void Jineng() 
        {
            Console.WriteLine("我回来了");
            ev?.Invoke();
        }
    }
}
