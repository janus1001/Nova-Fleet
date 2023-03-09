using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovaFleetCore.Structures;
using NovaFleetCore;

namespace NovaFleetTests
{
    [TestClass]
    public class ContainerTests
    {
        private class TestAspect : IAspect
        {
            public IContainer parentContainer { get; set; }
        }
        private class AltTestAspect : IAspect
        {
            public IContainer parentContainer { get; set; }
        }

        [TestMethod]
        public void ContainerCreateTest()
        {
            Container container = new Container();
            container.CreateNewAspect<TestAspect>();
            Assert.AreEqual(container.Aspects().Count, 1);
        }

        [TestMethod]
        public void ContainerCreateMultipleTest()
        {
            var container = new Container();
            container.CreateNewAspect<TestAspect>("Test1");
            container.CreateNewAspect<TestAspect>("Test2");
            Assert.AreEqual(container.Aspects().Count, 2);
        }

        [TestMethod]
        public void ContainerCreateDifferentTest()
        {
            var container = new Container();
            container.CreateNewAspect<TestAspect>();
            container.CreateNewAspect<AltTestAspect>();
            Assert.AreEqual(container.Aspects().Count, 2);
        }

        [TestMethod]
        public void ContainerRetrieveWithKeyTest()
        {
            var container = new Container();
            var original = container.CreateNewAspect<TestAspect>("Test");
            var retrieved = container.GetAspect<TestAspect>("Test");

            Assert.AreSame(original, retrieved);
        }

        [TestMethod]
        public void ContainerRetrieveWithoutKeyTest()
        {
            var container = new Container();
            var original = container.CreateNewAspect<TestAspect>();
            var retrieved = container.GetAspect<TestAspect>();

            Assert.AreSame(original, retrieved);
        }

        [TestMethod]
        public void ContainerCanTryGetMissingAspectTest()
        {
            var container = new Container();
            var retrieved = container.GetAspect<TestAspect>();
            Assert.IsNull(retrieved);
        }

        [TestMethod]
        public void ContainerCanAddPreCreatedAspectTest()
        {
            var container = new Container();
            var aspect = new TestAspect();
            container.AddAspect<TestAspect>(aspect);
            Assert.AreEqual(container.Aspects().Count, 1);
        }

        [TestMethod]
        public void ContainerCanGetPreCreatedAspectTest()
        {
            var container = new Container();
            var original = new TestAspect();
            container.AddAspect<TestAspect>(original);
            var retrieved = container.GetAspect<TestAspect>();
            Assert.AreSame(original, retrieved);
        }

        [TestMethod]
        public void AspectTracksContainerReferenceTest()
        {
            var container = new Container();
            var original = container.CreateNewAspect<TestAspect>();
            Assert.IsNotNull(original.parentContainer);
        }
    }
}
