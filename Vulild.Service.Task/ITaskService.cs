using System;

namespace Vulild.Service.Task
{
    public interface ITaskService : IService
    {
        void AddTask(TaskInfo config);

        void UpdateTask(TaskInfo config);

        void StartTask();

        void DeleteTask(TaskInfo config);
    }
}
