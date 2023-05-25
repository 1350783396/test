using Furion.IPCChannel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiTuYinXing
{
    /// <summary>
    /// 创建管道处理程序（处理 String 类型消息）
    /// </summary>
    public class MyChannelHandler : ChannelHandler<string>
    {
        /// <summary>
        /// 接受到管道消息后处理程序
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override Task InvokeAsync(string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
