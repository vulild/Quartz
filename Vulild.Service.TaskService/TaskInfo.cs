using System;
using System.Collections.Generic;
using System.Text;

namespace Vulild.Service.TaskService
{
    public abstract class TaskInfo
    {
        public Dictionary<string, object> Tag = new Dictionary<string, object>();
    }
}
