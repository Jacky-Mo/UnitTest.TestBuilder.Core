using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    internal class BuilderInstantiatePropertyInConstructor : BaseBuilder<TestObjectA>
    {
        public ITestServiceA TestService { get; private set; }
        public TestObjectB TestObjectB { get; private set; }
        public int Id { get; private set; } = 2;

        public BuilderInstantiatePropertyInConstructor() : this(null)
        {

        }

        public BuilderInstantiatePropertyInConstructor(IContainer container) : base(container, new ObjectBuilder())
        {
            TestObjectB = new TestObjectB();
        }
    }
}
