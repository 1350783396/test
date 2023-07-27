using System;

namespace WeiTuo
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<int> oneCs = (int a) => { Console.WriteLine(a + "1"); };
            oneCs+= (int a) => { Console.WriteLine(a + "2"); };
            oneCs?.Invoke(2);
            //FangFa fangFa = new FangFa();
            //EventFaBu eventFa = new EventFaBu();
            //eventFa.beicai +=(name, val) => { Console.WriteLine($"通知{name}准备做{val}"); };
            //eventFa.zhuangpan += fangFa.ZhuangPan;
            //eventFa.shangcai += fangFa.ShangCai;
            //eventFa.FaBu("cheng", "排骨");
            Gcz gcz = new Gcz();
            Gou gou = new Gou(gcz);
            Cat cat = new Cat(gcz);
            gcz.Jineng();
            Console.WriteLine("Hello World!");
        }

        public V GetT<T, V>(T ttt)
        {


            return default(V);

        }
    }

}
