using System.Linq;
using Xunit;

namespace UnitTest.TestBuilder.Core.Tests
{
    public class ParameterCreatorTests
    {
        private class ObjectA
        {
            public void MethodInt(int a) { }

            public void MethodString(string value) { }

            public void MethodObject(object obj) { }
        }

        [Fact]
        public void Create_ValueType_ReturnNotNull()
        {
            var creator = new ParameterCreator();

            var parameter = typeof(ObjectA).GetMethod("MethodInt").GetParameters().First();

            //act
            var result = creator.Create(parameter);

            //assert
            Assert.NotNull(result);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Create_StringType_ReturnNotNull()
        {
            var creator = new ParameterCreator();

            var parameter = typeof(ObjectA).GetMethod("MethodString").GetParameters().First();

            //act
            var result = creator.Create(parameter);

            //assert
            Assert.NotNull(result);
            Assert.Equal("value - Test", result);
        }

        [Fact]
        public void Create_ReferenceType_ReturnNull()
        {
            var creator = new ParameterCreator();

            var parameter = typeof(ObjectA).GetMethod("MethodObject").GetParameters().First();

            //act
            var result = creator.Create(parameter);

            //assert
            Assert.Null(result);
        }
    }
}
