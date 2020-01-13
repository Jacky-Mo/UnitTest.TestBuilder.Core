namespace UnitTest.TestBuilder.Core.Tests.Implementation
{
    internal class BuilderThatCantCreateDependents : BaseBuilder<TestObjectC>
    {
        public BuilderThatCantCreateDependents(): base(new ObjectBuilder())
        {

        }
    }
}
