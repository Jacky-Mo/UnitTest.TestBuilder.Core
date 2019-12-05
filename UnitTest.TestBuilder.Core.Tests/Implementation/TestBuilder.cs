using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    internal class TestBuilder : BaseBuilder<TestObject>
    {
        public ITestService TestService { get; private set; }
        public int Id { get; private set; } = 2;

        public TestBuilder() : this(null)
        {

        }

        public TestBuilder(IContainer container) : base(container, new ObjectBuilder())
        {

        }
    }
}
