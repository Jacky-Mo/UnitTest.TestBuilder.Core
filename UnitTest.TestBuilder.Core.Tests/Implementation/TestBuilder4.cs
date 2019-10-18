using System;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    internal class TestBuilder4 : BaseTestBuilder<TestObject>
    {
        public TestObject TestObject { get; private set; } = new TestObject(new TestService(), new TestObject2(), DateTime.Now, 8);
        public int Id { get; private set; } = 2;

        public TestBuilder4() : base(new ObjectBuilder())
        {

        }

        protected override TestObject CreateObject(params object[] args)
        {
            return TestObject;
        }
    }
}
