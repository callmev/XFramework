using System.Reflection;
using Ninject;
using Ninject.Components;
using Ninject.Selection.Heuristics;

namespace XFramework.Common.Ioc
{
    /// <summary>
    /// This class is NOT required by Ninject.Fody, 
    /// this is only used to avoid using [Inject] attributes
    /// </summary>
    public class PropertyInjectionHeuristic : NinjectComponent, IInjectionHeuristic
    {
        private IKernel kernel;

        public PropertyInjectionHeuristic(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public bool ShouldInject(MemberInfo member)
        {
            if (member.ReflectedType == null) return false;

            var propertyInfo = member.ReflectedType.GetProperty(member.Name);
            if (propertyInfo == null) return false;

            if (propertyInfo.GetCustomAttributes(false).Length == 0) return false;
            if (propertyInfo.GetCustomAttributes(false)[0].GetType() != typeof(FodyInjectAttribute)) return false;

            if (propertyInfo.CanWrite)
            {
                var service = kernel.TryGet(propertyInfo.PropertyType);
                return service != null;
            }

            return false;
        }
    }
}