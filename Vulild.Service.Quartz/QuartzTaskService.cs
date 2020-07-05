using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using Vulild.Service.Attributes;
using Vulild.Service.TaskService;

namespace Vulild.Service.Quartz
{
    [ServiceOption(Type = typeof(QuartOption))]
    public class QuartzTaskService : ITaskService
    {
        internal IScheduler _Scheduler;

        public QuartzTaskService(IScheduler scheduler)
        {
            this._Scheduler = scheduler;
        }

        public Option Option { get; set; }

        public void AddTask(TaskInfo config)
        {
            var task = (QuartzTask)config;
            JobDataMap jdm = new JobDataMap();
            foreach (var dic in config.Tag)
            {
                jdm.Add(dic);
            }
            jdm.Add("TaskInfo", config);

            IJobDetail job = JobBuilder.Create(task.TaskType)
                                .WithIdentity(task.JobName, task.GroupName)
                                .WithDescription(task.Description)
                                .SetJobData(jdm)
                                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                                        .WithIdentity(task.JobName, task.GroupName)
                                        .WithCronSchedule(task.Cron)//运行模式
                                        .WithDescription(task.Description).Build();

            _Scheduler.ScheduleJob(job, trigger);
        }

        public void DeleteTask(TaskInfo config)
        {
            var task = (QuartzTask)config;
            _Scheduler.DeleteJob(new JobKey(task.JobName, task.GroupName));
        }

        public void StartTask()
        {
            _Scheduler.Start();
        }

        public void UpdateTask(TaskInfo config)
        {
            DeleteTask(config);
            AddTask(config);
        }
    }
}
