using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiTuo
{
    public delegate void ZuoCai(string name, string val);
    public class EventFaBu
    {

        public event Action<string, string> beicai;
        public event ZuoCai chaocai;
        public event ZuoCai zhuangpan;
        public EventHandler shangcai;

        public void FaBu(string name, string val)
        {
            beicai?.Invoke(name, val);
            chaocai?.Invoke(name, val);
            zhuangpan?.Invoke(name, val);
            EventView eventView = new EventView
            {
                name = name,
                val = val
            };
            shangcai?.Invoke(this, eventView);
        }
    }
    public class EventView : EventArgs
    {
        public string name { get; set; }
        public string val { get; set; }

    }
    public class FangFa
    {

        public void BeiCai(string name, string val)
        {
            Console.WriteLine($"通知{name}准备做{val}");
        }
        public void ChaoCai(string name, string val)
        {
            Console.WriteLine($"通知{name}准备炒{val}");
        }
        public void ZhuangPan(string name, string val)
        {
            Console.WriteLine($"通知{name},{val}装盘");
        }

        public void ShangCai(object obj, EventArgs e)
        {
            var newe = (EventView)e;
            Console.WriteLine($"通知{newe.name},{newe.val}上菜");
        }

    }
}
