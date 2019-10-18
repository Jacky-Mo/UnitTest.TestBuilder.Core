using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.TestBuilder.Core.Tests.Implementation;
using Xunit;

namespace UnitTest.TestBuilder.Core.Tests
{
    public class TestBuilderTests
    {
        [Fact]
        public void TestBuilder_PropertiesArePopulated_ObjectIsCreatedUsingBuilderProperties()
        {
            var builder = new Implementation.TestBuilder();

            var testObject = builder.Build();

            //Assert the builder properties
            Assert.NotNull(builder.TestService);
            Assert.Equal(2, builder.Id);

            //Assert the object properties
            Assert.Equal(0, testObject.Number);
            Assert.Equal(DateTime.MinValue, testObject.Date);
            Assert.Same(builder.TestService, testObject.TestService); //use the same object from container
            Assert.NotNull(testObject.TestObject2); //create a new object
        }

        [Fact]
        public void TestBuilder_PropertyInstantiatedInConstructor_ObjectIsCreatedUsingBuilderProperties()
        {
            var builder = new Implementation.TestBuilder2();

            var testObject = builder.Build();

            //Assert the builder properties
            Assert.NotNull(builder.TestService);
            Assert.NotNull(builder.TestObject2);
            Assert.Equal(2, builder.Id);

            //Assert the object properties
            Assert.Equal(0, testObject.Number);
            Assert.Equal(DateTime.MinValue, testObject.Date);
            Assert.Same(builder.TestService, testObject.TestService); //use the same object from container
            Assert.Same(builder.TestObject2, testObject.TestObject2); //use the same object from container
        }

        [Fact]
        public void TestBuilder_WithContainer_ObjectIsCreatedUsingBuilderProperties()
        {
            var container = new SimpleContainer();
            var testService = new TestService();

            container.Add<ITestService>(testService);

            var builder = new Implementation.TestBuilder(container);

            var testObject = builder.Build();

            //Assert the builder properties
            Assert.Same(testService, builder.TestService); //use the object from container
            Assert.Equal(2, builder.Id);

            //Assert the object properties
            Assert.Equal(0, testObject.Number);
            Assert.Equal(DateTime.MinValue, testObject.Date);
            Assert.Same(builder.TestService, testObject.TestService); //use the same object from container
            Assert.NotNull(testObject.TestObject2); //create a new object
        }

        [Fact]
        public void TestBuilder_ObjectConstructorParameterIsNull_ThrowsException()
        {
            var builder = new Implementation.TestBuilder3();

            var exception = Assert.Throws<NullReferenceException>(() => builder.Build());

            Assert.Equal("no object created for Type: UnitTest.TestBuilder.Core.Tests.Implementation.ITestService2", exception.Message);
        }

        [Fact]
        public void TestBuilder_OverrideCreateMethod_ObjectIsCreatedFromCreateMethod()
        {
            var builder = new Implementation.TestBuilder4();

            var testObject = builder.Build();

            Assert.Same(builder.TestObject, testObject);
        }
    }
}
