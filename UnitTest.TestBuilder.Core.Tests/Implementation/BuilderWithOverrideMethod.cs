using System;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    internal class BuilderWithOverrideMethod : BaseBuilder<TestObjectA>
    {
        public TestObjectA TestObject { get; private set; } = new TestObjectA(new TestServiceA(), new TestObjectB(), DateTime.Now, 8);
        public int Id { get; private set; } = 2;

        public BuilderWithOverrideMethod() : base(new ObjectBuilder())
        {

        }

        protected override TestObjectA CreateObject(params object[] args)
        {
            return TestObject;
        }
    }
}
