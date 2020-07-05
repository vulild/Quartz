using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vulild.Service.TaskService;

namespace Vulild.Service.Quartz
{
    public delegate void JobExecution<TI>(TaskInfo task) where TI : TaskInfo;
    public class SimpleJobListener<TI> : IJobListener
        where TI : TaskInfo
    {
        public string Name => this.GetType().FullName;

        internal event JobExecution<TI> JobExecutionVetoedEvent;

        internal event JobExecution<TI> JobWasExecutedEvent;

        internal event JobExecution<TI> JobTobeExecutedEVent;

        /// <summary>
        /// Scheduler在JobDetail即将被执行，但又被TriggerListerner否决
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            TI taskInfo = (TI)JsonConvert.DeserializeObject(context.JobDetail.JobDataMap["TaskInfo"].ToString(), typeof(TI));
            return Task.Run(() => { JobExecutionVetoedEvent?.Invoke(taskInfo); });
        }

        /// <summary>
        /// Scheduler在JobDetail将要被执行时调用这个方法。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            TI taskInfo = (TI)JsonConvert.DeserializeObject(context.JobDetail.JobDataMap["TaskInfo"].ToString(), typeof(TI));
            return Task.Run(() => { JobTobeExecutedEVent?.Invoke(taskInfo); });
        }

        /// <summary>
        /// Scheduler在JobDetail被执行之后调用这个方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            TI taskInfo = (TI)JsonConvert.DeserializeObject(context.JobDetail.JobDataMap["TaskInfo"].ToString(), typeof(TI));
            return Task.Run(() => { JobWasExecutedEvent?.Invoke(taskInfo); });
        }
    }
}
