using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    internal class BuilderWithOneInterfaceProperty : BaseBuilder<TestObjectA>
    {
        public ITestServiceA TestService { get; private set; }
        public TestObjectB TestObjectB { get; private set; }
        public int Id { get; private set; } = 2;

        public BuilderWithOneInterfaceProperty() : this(null)
        {

        }

        public BuilderWithOneInterfaceProperty(IContainer container) : base(container, new ObjectBuilder())
        {

        }
    }
}
