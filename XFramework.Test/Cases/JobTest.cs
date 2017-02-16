using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartz;
using XFramework.Common.Quartz;

namespace XFramework.Test.Cases
{
    [TestClass]
    public class JobTest
    {
        [TestMethod]
        public void Job_Create_Test()
        {
            const string serviceName = "test";

            var job = new Job<TestJob>(serviceName);

            var job2 = new Job<TestJob>(serviceName, "0 0 12 * * ?");
            Assert.AreEqual(job2.Detail.Description, serviceName);
        }
    }

    public class TestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
