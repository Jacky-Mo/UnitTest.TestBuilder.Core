using System;
using UnitTest.TestBuilder.Core.Tests.Implementation;
using Xunit;

namespace UnitTest.TestBuilder.Core.Tests
{
    public class BuilderTests
    {
        [Fact]
        public void TestBuilder_PropertiesArePopulated_ObjectIsCreatedUsingBuilderProperties()
        {
            var builder = new Implementation.BuilderWithOneInterfaceProperty();

            var testObject = builder.Build();

            //Assert the builder properties
            Assert.NotNull(builder.TestService);
            Assert.NotNull(builder.TestObjectB);
            Assert.Equal(2, builder.Id);

            //Assert the reference Property was instanitated using its constructor
            Assert.NotNull(builder.TestObjectB.TestServiceB);

            //Assert the object properties
            Assert.Equal(0, testObject.Number);
            Assert.Equal(DateTime.MinValue, testObject.Date);
            Assert.Same(builder.TestService, testObject.TestServiceA); //use the same object from container
            Assert.NotNull(testObject.TestObjectB); //create a new object
        }

        [Fact]
        public void TestBuilder_PropertyInstantiatedInConstructor_ObjectIsCreatedUsingBuilderProperties()
        {
            var builder = new Implementation.BuilderInstantiatePropertyInConstructor();

            var testObject = builder.Build();

            //Assert the builder properties
            Assert.NotNull(builder.TestService);
            Assert.NotNull(builder.TestObjectB);
            Assert.Equal(2, builder.Id);

            //Assert the object properties
            Assert.Equal(0, testObject.Number);
            Assert.Equal(DateTime.MinValue, testObject.Date);
            Assert.Same(builder.TestService, testObject.TestServiceA); //use the same object from container
            Assert.Same(builder.TestObjectB, testObject.TestObjectB); //use the same object from container
        }

        [Fact]
        public void TestBuilder_WithContainer_ObjectIsCreatedUsingBuilderProperties()
        {
            var container = new SimpleContainer();
            var testService = new TestServiceA();

            container.Add<ITestServiceA>(testService);

            var builder = new Implementation.BuilderWithOneInterfaceProperty(container);

            var testObject = builder.Build();

            //Assert the builder properties
            Assert.Same(testService, builder.TestService); //use the object from container
            Assert.Equal(2, builder.Id);

            //Assert the object properties
            Assert.Equal(0, testObject.Number);
            Assert.Equal(DateTime.MinValue, testObject.Date);
            Assert.Same(builder.TestService, testObject.TestServiceA); //use the same object from container
            Assert.NotNull(testObject.TestObjectB); //create a new object
        }

        [Fact]
        public void TestBuilder_ObjectConstructorParameterIsNull_ThrowsException()
        {
            var builder = new Implementation.BuilderThatCantCreateDependents();

            var exception = Assert.Throws<NullReferenceException>(() => builder.Build());

            Assert.Equal("Object was not created for parameter Type: ITestServiceC when instantiating TestObjectC", exception.Message);
        }

        [Fact]
        public void TestBuilder_OverrideCreateMethod_ObjectIsCreatedFromCreateMethod()
        {
            var builder = new Implementation.BuilderWithOverrideMethod();

            var testObject = builder.Build();

            Assert.Same(builder.TestObject, testObject);
        }
    }
}
