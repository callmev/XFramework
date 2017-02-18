using Microsoft.VisualStudio.TestTools.UnitTesting;
using XFramework.Utilities;

namespace XFramework.Test.Cases
{
    [TestClass]
    public class AutoMapperTest
    {
        [TestMethod]
        public void Map_Test()
        {
            var source = new MapSource()
            {
                UserId = "AA",
                Name = "小明"
            };

            var obj = source.MapTo<MapObject>();

            Assert.IsNotNull(obj);
            Assert.AreEqual(obj.Name, source.Name);
            Assert.IsTrue(string.IsNullOrEmpty(obj.Id));
        }
    }

    public class MapSource
    {
        public string UserId { get; set; }

        public string Name { get; set; }
    }

    public class MapObject
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
