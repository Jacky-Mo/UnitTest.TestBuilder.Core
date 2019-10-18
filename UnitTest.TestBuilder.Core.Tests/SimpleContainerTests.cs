using System;
using Xunit;

namespace UnitTest.TestBuilder.Core.Tests
{
    public class SimpleContainerTests
    {
        [Fact]
        public void Add_NewKey_ObjectAdded()
        {
            var container = new SimpleContainer();

            //act
            container.Add(container);

            var obj = container.Resolve<SimpleContainer>();

            //assert
            Assert.Same(container, obj);
        }

        [Fact]
        public void Add_ExistingKey_ObjectIsOverrided()
        {
            var container = new SimpleContainer();
            var container2 = new SimpleContainer();

            //act
            container.Add(container);
            container.Add(container2);

            var obj = container.Resolve<SimpleContainer>();

            //assert
            Assert.Same(container2, obj);
        }

        [Fact]
        public void Resolve_Generic_ReturnCorrectObject()
        {
            var container = new SimpleContainer();
            container.Add(container);

            //act
            var obj = container.Resolve<SimpleContainer>();

            //assert
            Assert.Same(container, obj);
        }

        [Fact]
        public void Resolve_NonGeneric_ReturnCorrectObject()
        {
            var container = new SimpleContainer();
            container.Add(container);

            //act
            var obj = container.Resolve(typeof(SimpleContainer));

            //assert
            Assert.Same(container, obj);
        }
    }
}
