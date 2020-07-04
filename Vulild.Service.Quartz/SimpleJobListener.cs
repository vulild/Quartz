using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vulild.Service.Quartz
{
    public class SimpleJobListener : IJobListener
    {
        public string Name => this.GetType().FullName;

        /// <summary>
        /// Scheduler在JobDetail即将被执行，但又被TriggerListerner否决
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => { Console.WriteLine(context.JobDetail.Key); });
        }

        /// <summary>
        /// Scheduler在JobDetail将要被执行时调用这个方法。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => { Console.WriteLine(context.JobDetail.Key); });
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
            return Task.Run(() => { Console.WriteLine(context.JobDetail.Key); });
        }
    }
}
