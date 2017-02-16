using Quartz;

namespace XFramework.Common.Quartz
{
    internal class ScheduleManager
    {
        private readonly IScheduler _myScheduler;
        public IScheduler Scheduler
        {
            get
            {
                return _myScheduler;
            }
        }

        public bool Invalid
        {
            get
            {
                return _myScheduler == null;
            }
        }

        public ScheduleManager(IScheduler myScheduler)
        {
            _myScheduler = myScheduler;
        }

        public void StartScheduler()
        {
            if (Scheduler != null)
                Scheduler.Start();
        }

        public void PauseAll()
        {
            if (Scheduler != null)
                Scheduler.PauseAll();
        }

        public void ResumeAll()
        {
            if (Scheduler != null)
                Scheduler.ResumeAll();
        }

        public void ShutdownScheduler()
        {
            if (Scheduler != null)
                Scheduler.Shutdown();
        }

        public void ShutdownScheduler(bool waitForJobsToComplete)
        {
            if (Scheduler != null)
                Scheduler.Shutdown(waitForJobsToComplete);
        }
    }
}
