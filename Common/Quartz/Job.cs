using System;
using Quartz;

namespace XFramework.Common.Quartz
{
    public class Job<T> where T : IJob
    {
        public IJobDetail Detail { get; set; }

        public ICronTrigger Trigger { get; set; }

        public Job()
        {
        }

        public Job(IJobDetail detail, ICronTrigger trigger)
        {
            Detail = detail;
            Trigger = trigger;
        }

        /// <summary>
        /// 该任务只执行一次
        /// </summary>
        /// <param name="jobName"></param>
        public Job(string jobName)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentNullException("jobName is null");

            Detail = JobBuilder.Create<T>()
                .WithIdentity(jobName)
                .Build();

            Trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity(string.Format("{0}_Trigger", jobName))
                .StartAt(new DateTimeOffset())
                .WithSchedule(
                    SimpleScheduleBuilder.Create()
                        .WithIntervalInSeconds(3)
                        //重复执行的次数，因为加入任务的时候马上执行了，所以不需要重复，否则会多一次。  
                        .WithRepeatCount(0))
                        .Build();
        }

        public Job(string jobName, string workTime) : this(jobName, jobName, workTime)
        {
        }

        public Job(string jobName, string jobDesc, string workTime)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentNullException("jobName is null");

            if (string.IsNullOrEmpty(workTime))
                throw new ArgumentNullException("workTime is null");

            //创建一个作业
            Detail = JobBuilder.Create<T>()
              .WithIdentity(jobName)
              .WithDescription(jobDesc)
              .Build();

            //创建一个触发器
            Trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity(string.Format("{0}_Trigger", jobName))
                .WithCronSchedule(workTime)
                .Build();
        }
    }
}
