using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework.ServiceHost
{
    public class ServiceRegister
    {
        public static void Run()
        {
            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();

                x.SetDescription(UtilHelper.GetAppSetting("ZcbListening_ServiceName"));
                x.SetDisplayName(UtilHelper.GetAppSetting("ZcbListening_ServiceDisplayName"));
                x.SetServiceName(UtilHelper.GetAppSetting("ZcbListening_ServiceDescription"));

                x.Service(factory =>
                {
                    var server = new QuartzServer();
                    server.Initialize();

                    return server;
                });
            });
        }
    }
}
