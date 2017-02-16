using Ninject;
using ServiceStack.Configuration;

namespace XFramework.Common.Ioc
{
    internal class NinjectIocAdapter : IContainerAdapter
    {
        private readonly IKernel _kernel;

        public NinjectIocAdapter(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public T Resolve<T>()
        {
            return this._kernel.Get<T>();
        }

        public T TryResolve<T>()
        {
            return this._kernel.CanResolve<T>() ? this._kernel.Get<T>() : default(T);
        }
    }
}
