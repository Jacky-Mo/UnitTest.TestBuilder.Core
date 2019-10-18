using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    internal class TestBuilder2 : BaseTestBuilder<TestObject>
    {
        public ITestService TestService { get; private set; }
        public TestObject2 TestObject2 { get; private set; }
        public int Id { get; private set; } = 2;

        public TestBuilder2() : this(null)
        {

        }

        public TestBuilder2(IContainer container) : base(container, new ObjectBuilder())
        {
            TestObject2 = new TestObject2();
        }
    }
}
