using System;
using System.Collections.Generic;
using System.Text;
using Vulild.Service.TaskService;

namespace Vulild.Service.Quartz
{
    public class QuartzTask : TaskInfo
    {
        public string GroupName { get; set; }

        public string JobName { get; set; }

        public string Description { get; set; }

        public string Cron { get; set; }

        public Type TaskType { get; set; }
    }
}
