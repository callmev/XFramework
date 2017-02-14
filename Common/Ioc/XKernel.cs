using Fody.DependencyInjection;
using Ninject;
using Ninject.Modules;
using Ninject.Selection.Heuristics;

namespace XFramework.Common.Ioc
{
    public class XKernel
    {
        public static IKernel Kernel;

        public static IKernel RegisterKernel(INinjectModule[] injectModules)
        {
            Kernel = new StandardKernel(injectModules);

            Kernel.Components.Add<IInjectionHeuristic, PropertyInjectionHeuristic>();
            ConfigurableInjection.InitializeContainer(Kernel);

            return Kernel;
        }
    }
}
