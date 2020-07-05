using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using Vulild.Service;

namespace Vulild.Service.Quartz
{
    public class QuartOption : Option
    {
        public string TablePrefix { get; set; } = "QRTZ_";

        public string SerializerType { get; set; } = "binary";

        public string DriverDelegateType { get; set; } = "Quartz.Impl.AdoJobStore.MySQLDelegate,Quartz";

        public string ConnectionString { get; set; }

        public string Provider { get; set; } = "MySql";

        /// <summary>
        /// Scheduler在JobDetail即将被执行，但又被TriggerListerner否决触发的事件
        /// </summary>
        public event JobExecution<QuartzTask> JobExecutionVetoedEvent;

        /// <summary>
        /// Scheduler在JobDetail被执行之后调用触发的事件
        /// </summary>
        public event JobExecution<QuartzTask> JobWasExecutedEvent;

        /// <summary>
        /// Scheduler在JobDetail将要被执行时触发的事件。
        /// </summary>
        public event JobExecution<QuartzTask> JobTobeExecutedEVent;

        IScheduler _Scheduler;
        public override IService CreateService()
        {
            if (_Scheduler == null)
            {
                //1.首先创建一个作业调度池
                var properties = new NameValueCollection();
                //存储类型
                properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX,Quartz";
                //表明前缀
                properties["quartz.jobStore.tablePrefix"] = TablePrefix;

                properties["quartz.serializer.type"] = SerializerType;

                //驱动类型
                properties["quartz.jobStore.driverDelegateType"] = DriverDelegateType;
                //数据源名称
                properties["quartz.jobStore.dataSource"] = "myDS";
                //连接字符串
                properties["quartz.dataSource.myDS.connectionString"] = ConnectionString;
                //版本
                properties["quartz.dataSource.myDS.provider"] = Provider;

                properties["quartz.jobStore.clustered"] = "true";
                properties["quartz.scheduler.instanceId"] = "AUTO";

                var schedulerFactory = new StdSchedulerFactory(properties);
                _Scheduler = schedulerFactory.GetScheduler().Result;
                var taskListener = new SimpleJobListener<QuartzTask>();
                taskListener.JobExecutionVetoedEvent += this.JobExecutionVetoedEvent;
                taskListener.JobTobeExecutedEVent += this.JobTobeExecutedEVent;
                taskListener.JobWasExecutedEvent += this.JobWasExecutedEvent;

                _Scheduler.ListenerManager.AddJobListener(taskListener);
            }
            return new QuartzTaskService(_Scheduler);
        }
    }
}
