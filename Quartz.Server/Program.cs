using Quartz.Tasks;
using System;
using Vulild.Core.Assmblys;
using Vulild.Service;
using Vulild.Service.Quartz;
using Vulild.Service.TaskService;

namespace Quartz.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            AssmblyUtil.SearchAllAssmbly(ServiceUtil.TypeDeal);
            ServiceUtil.InitService("QuartTaskService", new QuartOption()
            {
                ConnectionString = "Server=vulild.top;Database=Quartz;Uid=vulild;Pwd=gelz1122"
            });

            var taskService = ServiceUtil.GetService<ITaskService>();

            taskService.StartTask();

        }
    }
}
