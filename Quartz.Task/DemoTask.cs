using Quartz;
using System;
using System.Threading.Tasks;

namespace Quartz.Tasks
{
    public class DemoTask : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => { Console.WriteLine(DateTime.Now); });
        }
    }
}
