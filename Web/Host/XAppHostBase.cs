using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Funq;
using Ninject.Modules;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;
using XFramework.Base;
using XFramework.Common.Ioc;
using XFramework.Web.Resource.GetToken;

namespace XFramework.Web.Host
{
    public class XAppHostBase : AppHostBase
    {
        private readonly string _serviceName;
        private readonly Assembly[] _assembliesWithServices;

        /// <summary>
        /// Tell ServiceStack the name and where to find your web services
        /// </summary>
        protected XAppHostBase(string serviceName, Assembly[] assembliesWithServices)
            : base(serviceName, assembliesWithServices)
        {
            _serviceName = serviceName;
            _assembliesWithServices = assembliesWithServices;
        }

        protected virtual void CustomizeConfigure(Container container)
        {
            var injectModules = LoadInjectModules();

            //添加基础框架InjectModule
            injectModules.Add(new XFrameworkInjectModule());

            //此处绑定的接口，仅提供在资源层使用跨层访问的服务接口
            container.Adapter = new NinjectIocAdapter(XKernel.RegisterKernel(injectModules.ToArray()));

            //注册资源层请求合规性验证器
            var assembliesWithServiceList = _assembliesWithServices.ToList();
            assembliesWithServiceList.Add(typeof(GetTokenRequest).Assembly);
            assembliesWithServiceList.ForEach(item =>
            {
                container.RegisterValidators(item);
            });
        }
        
        public override void Configure(Container container)
        {
            var appHostConfiger = new AppHostConfiger(this, container);

            appHostConfiger.ConfigGlobleProperty();
            appHostConfiger.ConfigureIoc();
            appHostConfiger.ConfigPlugins();

            this.CustomizeConfigure(container);

            RequestFilters.Add((httpReq, httpRes, requestDto) =>
            {
                if (httpReq.HttpMethod == "OPTIONS")
                {
                    httpRes.AddHeader("Access-Control-Allow-Origin", "*");
                    httpRes.AddHeader("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
                    httpRes.AddHeader("Access-Control-Allow-Headers", "X-Requested-With, Content-Type");
                    httpRes.End();
                }
            });
        }

        protected virtual IList<INinjectModule> LoadInjectModules()
        {
            return null;
        }
    }
}