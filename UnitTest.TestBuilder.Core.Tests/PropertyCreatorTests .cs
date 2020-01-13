using Moq;
using System;
using UnitTest.TestBuilder.Core.Abstracts;
using Xunit;

namespace UnitTest.TestBuilder.Core.Tests
{
    public class PropertyCreatorTests
    {
        private class ObjectA
        {
            public string Description { get; }
            public IServiceA ServiceA { get; }

            public ObjectA(string description) : this(description, null) { }

            public ObjectA(string description, IServiceA serviceA)
            {
                Description = description;
                ServiceA = serviceA;
            }
        }

        public interface IServiceA { }


        private class Builder
        {
            public Mock<IObjectBuilder> ObjectBuilder { get; } = new Mock<IObjectBuilder>();
            public PropertyCreator Build()
            {
                return new PropertyCreator(ObjectBuilder.Object);
            }
        }

        [Fact]
        public void Create_ValueType_ReturnNotNull()
        {
            var builder = new Builder();
            var creator = builder.Build();

            //act
            var result = creator.Create(typeof(int));

            //assert
            Assert.Null(result);
        }

        [Fact]
        public void Create_StringType_ReturnNotNull()
        {
            var builder = new Builder();
            var creator = builder.Build();

            //act
            var result = creator.Create(typeof(string));

            //assert
            Assert.Null(result);
        }

        [Fact]
        public void Create_Interface_ReturnNull()
        {
            var builder = new Builder();
            var creator = builder.Build();

            //act
            var result = creator.Create(typeof(IServiceA));

            //assert
            Assert.Null(result);
        }

        [Fact]
        public void Create_ClassNotInterface_ReturnNotNull()
        {
            var builder = new Builder();

            builder.ObjectBuilder.Setup(a => a.Create(typeof(IServiceA))).Returns(new Mock<IServiceA>().Object).Verifiable();

            var creator = builder.Build();

            //act
            var result = (ObjectA)creator.Create(typeof(ObjectA));

            //assert
            Assert.NotNull(result);
            Assert.Equal("description - Test", result.Description);
            Assert.NotNull(result.ServiceA);
            builder.ObjectBuilder.Verify();
        }

        [Fact]
        public void Create_ClassAndCantCreateParameterType_ThrowsException()
        {
            var builder = new Builder();
            var creator = builder.Build();

            //act
            var result = Assert.Throws<NullReferenceException>(() => creator.Create(typeof(ObjectA)));

            //assert
            Assert.NotNull(result);
            Assert.Equal("Can not create object for parameter Type: IServiceA when instantiating object UnitTest.TestBuilder.Core.Tests.PropertyCreatorTests+ObjectA", result.Message);
            builder.ObjectBuilder.Verify(a => a.Create(typeof(IServiceA)), Times.Once);
        }
    }
}
