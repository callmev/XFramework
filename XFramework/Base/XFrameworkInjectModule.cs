using Ninject.Modules;
using XFramework.Common.Cache;
using XFramework.Common.Quartz;
using XFramework.Common.RabbitMQ;
using XFramework.Common.RabbitMQ.Implement;

namespace XFramework.Base
{
    public class XFrameworkInjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICache>().To<CacheProvider>().Named("CACHE");
            Bind<ICache>().To<RedisProvider>().Named("REDIS");

            Bind<IMqManager>().To<MqManager>();
            Bind<IMqReceiver>().To<MqReceiver>();
            Bind<IMqSender>().To<MqSender>();

            Bind<IQuartzServer>().To<QuartzServer>();
        }
    }
}
