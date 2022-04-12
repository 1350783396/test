using insterFace;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class APIDataService : BackgroundService
    {
        private readonly IStudent student1;
        public APIDataService(IStudent student) 
        {
            student1 = student;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var data = DateTime.Now;
                    if (data.Minute == 0 && data.Second == 0)
                    { 
                    
                    }
                    //需要执行的任务

                }
                catch (Exception ex)
                {
                    //LogHelper.Error(ex.Message);
                }
                await Task.Delay(1000, stoppingToken);//等待1秒
            }
        }
    }
}
