using System.Collections.Generic;
using Quartz;
using Quartz.Impl;
using Topshelf;

namespace XFramework.Common.Quartz
{
    /// <summary>
    /// The main server logic.
    /// </summary>
    public class QuartzServer : ServiceControl, IQuartzServer
    {
        private ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        /// <summary>
        /// Returns the current scheduler instance (usually created in <see cref="Initialize" />
        /// using the <see cref="GetScheduler" /> method).
        /// </summary>
        protected virtual IScheduler Scheduler
        {
            get { return _scheduler; }
        }

        /// <summary>
        /// Initializes the instance of the <see cref="QuartzServer"/> class.
        /// </summary>
        public virtual void Initialize()
        {
            _schedulerFactory = CreateSchedulerFactory();
            _scheduler = GetScheduler();

            ScheduleProxy.Instance.SetScheduler(_scheduler);
        }

        /// <summary>
        /// ×¢²áJob
        /// </summary>
        /// <param name="job"></param>
        public virtual void RegisterJob<T>(Job<T> job) where T : IJob
        {
            _scheduler.ScheduleJob(job.Detail, job.Trigger);
        }

        /// <summary>
        /// ×¢²áJobs
        /// </summary>
        /// <param name="jobs"></param>
        public virtual void RegisterJobs<T>(IEnumerable<Job<T>> jobs) where T : IJob
        {
            jobs.ForEach(job =>
            {
                _scheduler.ScheduleJob(job.Detail, job.Trigger);
            });
        }

        /// <summary>
        /// Gets the scheduler with which this server should operate with.
        /// </summary>
        /// <returns></returns>
        protected virtual IScheduler GetScheduler()
        {
            return _schedulerFactory.GetScheduler();
        }

        /// <summary>
        /// Creates the scheduler factory that will be the factory
        /// for all schedulers on this instance.
        /// </summary>
        /// <returns></returns>
        protected virtual ISchedulerFactory CreateSchedulerFactory()
        {
            return new StdSchedulerFactory();
        }

        #region Scheduler' Start\Stop\Pause\Resume\Continue

        /// <summary>
        /// Starts this instance, delegates to scheduler.
        /// </summary>
        public virtual void Start()
        {
            _scheduler.Start();
        }

        /// <summary>
        /// Stops this instance, delegates to scheduler.
        /// </summary>
        public virtual void Stop()
        {
            _scheduler.Shutdown(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            // no-op for now
        }

        /// <summary>
        /// Pauses all activity in scheudler.
        /// </summary>
        public virtual void Pause()
        {
            _scheduler.PauseAll();
        }

        /// <summary>
        /// Resumes all acitivity in server.
        /// </summary>
        public void Resume()
        {
            _scheduler.ResumeAll();
        }

        /// <summary>
        /// TopShelf's method delegated to <see cref="Start()"/>.
        /// </summary>
        public bool Start(HostControl hostControl)
        {
            Start();
            return true;
        }

        /// <summary>
        /// TopShelf's method delegated to <see cref="Stop()"/>.
        /// </summary>
        public bool Stop(HostControl hostControl)
        {
            Stop();
            return true;
        }

        /// <summary>
        /// TopShelf's method delegated to <see cref="Pause()"/>.
        /// </summary>
        public bool Pause(HostControl hostControl)
        {
            Pause();
            return true;
        }

        /// <summary>
        /// TopShelf's method delegated to <see cref="Resume()"/>.
        /// </summary>
        public bool Continue(HostControl hostControl)
        {
            Resume();
            return true;
        }
        #endregion

    }
}
