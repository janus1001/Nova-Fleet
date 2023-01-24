using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovaFleetCore.Structures;

namespace NovaFleetTests
{
    [TestClass]
    public class ContainerTests
    {
        private class TestAspect : IAspect
        {
            public IContainer container { get; set; }
        }

        [TestMethod]
        public void ContainerCreateTest()
        {
            Container container = new Container();
            container.AddAspect<TestAspect>();
            Assert.AreEqual(container.Aspects().Count, 1);
        }

        [TestMethod]
        public void ContainerCreateMultipleTest()
        {
            var container = new Container();
            container.AddAspect<TestAspect>("Test1");
            container.AddAspect<TestAspect>("Test2");
            Assert.AreEqual(container.Aspects().Count, 2);
        }
    }
}
