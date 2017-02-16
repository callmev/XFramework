using Quartz;

namespace XFramework.Common.Quartz
{
    internal class ScheduleProxy
    {
        private IScheduler _scheduler;

        private ScheduleManager _scheduleManager;
        public ScheduleManager ScheduleManager
        {
            get
            {
                if (_scheduler == null)
                    return null;

                return _scheduleManager ?? (_scheduleManager = new ScheduleManager(_scheduler));
            }
        }

        public bool IsScheduleActive
        {
            get
            {
                return _scheduleManager != null;
            }
        }

        private static ScheduleProxy _scheduleProxy;

        private ScheduleProxy()
        { }

        public static ScheduleProxy Instance
        {
            get { return _scheduleProxy ?? (_scheduleProxy = new ScheduleProxy()); }
        }

        public void SetScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }
    }
}
