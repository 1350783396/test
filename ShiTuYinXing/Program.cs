using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Channels;
using System.Threading.Tasks;
using CardReader;
using Furion;
using Furion.FriendlyException;
using Furion.IPCChannel;
using Furion.RemoteRequest.Extensions;
using Furion.ViewEngine;
using Furion.ViewEngine.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ShiTuYinXing
{
    class Program
    {
        static void Main(string[] args)
        {

            string aaasf = "asdasfas";
            var adsg = aaasf[2..];



            var reader = ChannelContext<string, MyChannelHandler>.BoundedChannel.Reader;



            for (int i = 0; i < 100; i++)
            {
                // 使用有限容量生产数据
                ChannelContext<string, MyChannelHandler>.BoundedChannel.Writer.WriteAsync($"Loop {i} times.");
            }



            // 创建一个服务容器
            var services = Inject.Create();
            // 注册服务
            services.AddRemoteRequest();
            services.AddViewEngine();
            // 所有服务注册完毕后调用 Build() 构建
            services.Build();

            HttpClient client = new HttpClient();
           var asda= client.GetStringAsync("http://192.168.2.211:8039/api/SystemSupport/CodeDict/GetSSDMList?page=1&limit=10&token=F67F53BB87338EC001C1ED03E7A27B6F&YQXH=1").Result;

            var jiamitext = SignGenerator.ZJ_Hmac_SM3("1", "1", "1646825799978", "{\"a\":\"11\"}");



            HttpClient httpClient = new HttpClient();
            var result5 = httpClient.GetStringAsync("http://192.168.2.211:8039/api/SystemSupport/TJ/GetBQGZRZSum?token=50A3FFD155D05F066C4616FBDF1766BA&YQXH=1");
            var result4 = "http://192.168.2.211:8039/api/SystemSupport/TJ/GetBQGZRZSum?token=50A3FFD155D05F066C4616FBDF1766BA&YQXH=1".GetAsStringAsync().Result;
            var _viewEngine = App.GetService<IViewEngine>();
            var result3 = _viewEngine.Compile("5>2");
            var result2 = @"5>2".RunCompile();
            var result = "Hello @Model.Name".RunCompile(new { Name = "Furion" });
            Console.WriteLine("Hello World!");
        }
    }
}
