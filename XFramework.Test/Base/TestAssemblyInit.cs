using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.Modules;
using XFramework.Base;
using XFramework.Common.Ioc;

namespace XFramework.Test.Base
{
    [TestClass]
    public class TestAssemblyInit
    {
        [AssemblyInitialize]
        public static void InitAssemblyInitialize(TestContext ctx)
        {
            try
            {
                XmlConfigurator.Configure();

                XKernel.RegisterKernel(LoadInjectModules().ToArray());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static IEnumerable<INinjectModule> LoadInjectModules()
        {
            //填写所有层的Ninject关系接口
            return new List<INinjectModule>()
            {
                new XFrameworkInjectModule(),
                new TestInjectModule(),
            };
        }
    }
}